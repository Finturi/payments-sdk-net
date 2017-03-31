﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace CM.Payments.Client
{
    /// <summary>
    ///     Contains the authentication tokens to connect.
    /// </summary>
    public sealed class OAuth
    {
        private const string OAuthConsumerKeyKey = "oauth_consumer_key";
        private const string OAuthNonceKey = "oauth_nonce";
        private const string OAuthParameterPrefix = "oauth_";
        private const string OAuthSignatureKey = "oauth_signature";
        private const string OAuthSignatureMethodKey = "oauth_signature_method";
        private const string OAuthSignatureType = "HMAC-SHA256";
        private const string OAuthTimestampKey = "oauth_timestamp";
        private const string OAuthVersion = "1.0";
        private const string OAuthVersionKey = "oauth_version";
        private static readonly ReadOnlyCollection<string> AcceptedMethods = new ReadOnlyCollection<string>(new[] {"POST", "GET", "PUT"});
        private readonly string _oAuthConsumerKey;
        private readonly string _oAuthConsumerSecret;

        /// <summary>
        ///     Initializes a new instance of the <see cref="OAuth" /> class.
        /// </summary>
        /// <param name="consumerKey">The consumer key.</param>
        /// <param name="consumerSecret">The consumer secret.</param>
        public OAuth(string consumerKey, string consumerSecret)
        {
            this._oAuthConsumerKey = consumerKey;
            this._oAuthConsumerSecret = consumerSecret;
        }

        /// <summary>
        ///     Generates a signature for authentication purpose.
        /// </summary>
        /// <param name="method">THe HTTP method.</param>
        /// <param name="url">The full URL.</param>
        /// <param name="data">Data to send, if HTTP method is POST.</param>
        /// <param name="timeStamp">A timestamp</param>
        /// <param name="nonce">A unique sequence of alphanumeric characters.</param>
        /// <returns></returns>
        public string GenerateSignature(string method, string url, string data, string timeStamp, string nonce)
        {
            var signatureBase = this.GenerateSignatureBase(method, url, data, timeStamp, nonce);
            string key = $"{UrlEncode(this._oAuthConsumerKey)}&{UrlEncode(this._oAuthConsumerSecret)}";

            return ComputeHash(key, signatureBase);
        }

        internal string GenerateHeader([NotNull] string method, string url, string data = "")
        {
            var auth = new StringBuilder();
            var nonce = GenerateNonce();
            var timeStamp = GenerateTimeStamp();
            var signature = this.GenerateSignature(method.ToUpper(), url, data, timeStamp, nonce);

            auth.Append($"{UrlEncode(OAuthConsumerKeyKey)}=\"{UrlEncode(this._oAuthConsumerKey)}\", ");
            auth.Append($"{UrlEncode(OAuthNonceKey)}=\"{UrlEncode(nonce)}\", ");
            auth.Append($"{UrlEncode(OAuthSignatureKey)}=\"{UrlEncode(signature)}\", ");
            auth.Append($"{UrlEncode(OAuthSignatureMethodKey)}=\"{UrlEncode(OAuthSignatureType)}\", ");
            auth.Append($"{UrlEncode(OAuthTimestampKey)}=\"{UrlEncode(timeStamp)}\", ");
            auth.Append($"{UrlEncode(OAuthVersionKey)}=\"{UrlEncode(OAuthVersion)}\"");
            return auth.ToString();
        }

        private static string ComputeHash([NotNull] string key, [NotNull] string data)
        {
            var hmac = new HMACSHA256 {Key = Encoding.ASCII.GetBytes(key)};
            var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));

            var hashString = new StringBuilder();
            foreach (var t in hashBytes)
            {
                hashString.Append(t.ToString("x2"));
            }

            var base64Bytes = Encoding.UTF8.GetBytes(hashString.ToString());

            return Convert.ToBase64String(base64Bytes);
        }

        private static string GenerateNonce()
        {
            var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            var data = new byte[32];

            using (var crypto = RandomNumberGenerator.Create())
            {
                crypto.GetBytes(data);
            }

            var result = new StringBuilder(32);
            foreach (var b in data)
            {
                result.Append(chars[b % chars.Length]);
            }

            return result.ToString();
        }

        private string GenerateSignatureBase(
            [NotNull] string method,
            [NotNull] string url,
            string data,
            [NotNull] string timeStamp,
            [NotNull] string nonce)
        {
            if (!AcceptedMethods.Contains(method))
            {
                throw new ArgumentException($"{method} is not an accepted method.");
            }

            var parameters = new List<Parameter>();
            if (method == HttpMethod.Post.Method || method == HttpMethod.Put.Method)
            {
                parameters.Add(new Parameter("", data));
            }
            parameters.AddRange(GetParameters(GetQueryFromUrl(url)));
            parameters.Add(new Parameter(UrlEncode(OAuthVersionKey), UrlEncode(OAuthVersion)));
            parameters.Add(new Parameter(UrlEncode(OAuthNonceKey), UrlEncode(nonce)));
            parameters.Add(new Parameter(UrlEncode(OAuthTimestampKey), UrlEncode(timeStamp)));
            parameters.Add(new Parameter(UrlEncode(OAuthSignatureMethodKey), UrlEncode(OAuthSignatureType)));
            parameters.Add(new Parameter(UrlEncode(OAuthConsumerKeyKey), UrlEncode(this._oAuthConsumerKey)));

            parameters = parameters.OrderBy(v => v.Name).ToList();

            var normalizedRequestParameters = NormalizeRequestParameters(parameters);

            var signatureBase = new StringBuilder();
            signatureBase.Append($"{method}&");
            signatureBase.Append($"{UrlEncode(url)}&");
            signatureBase.Append($"{UrlEncode(normalizedRequestParameters)}");

            return signatureBase.ToString();
        }

        private static string GenerateTimeStamp()
        {
            // Default implementation of UNIX time of the current UTC time
            var ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        private static List<Parameter> GetParameters(string parameters)
        {
            if (parameters.StartsWith("?"))
            {
                parameters = parameters.Remove(0, 1);
            }

            var result = new List<Parameter>();

            if (string.IsNullOrEmpty(parameters))
            {
                return result;
            }
            var p = parameters.Split('&');
            foreach (var s in p)
            {
                if (string.IsNullOrEmpty(s) || s.StartsWith(OAuthParameterPrefix))
                {
                    continue;
                }
                if (s.IndexOf('=') > -1)
                {
                    var temp = s.Split('=');
                    result.Add(new Parameter(UrlEncode(temp[0]), UrlEncode(temp[1])));
                }
                else
                {
                    result.Add(new Parameter(UrlEncode(s), UrlEncode(string.Empty)));
                }
            }

            return result;
        }

        private static string GetQueryFromUrl([NotNull] string url)
        {
            return url.Contains("?") ? url.Split('?')[1] : "";
        }

        private static string NormalizeRequestParameters([NotNull] IList<Parameter> parameters)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < parameters.Count; i++)
            {
                var p = parameters[i];
                sb.Append(!string.IsNullOrEmpty(p.Name) ? $"{p.Name}={p.Value}" : p.Value);

                if (i < parameters.Count - 1)
                {
                    sb.Append("&");
                }
            }
            return sb.ToString();
        }

        private static string UrlEncode([NotNull] string value)
        {
            return new Regex(@"%[a-f0-9]{2}").Replace(Uri.EscapeDataString(value), match => match.Value.ToUpperInvariant()).Replace("+", "%20");
        }

        private sealed class Parameter
        {
            public Parameter(string name, string value)
            {
                this.Name = name;
                this.Value = value;
            }

            public string Name { get; }
            public string Value { get; }
        }
    }
}