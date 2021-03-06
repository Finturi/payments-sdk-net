﻿using CM.Payments.Client.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;

//<auto-generated>
//	IMPORTANT NOTE:
//	This code is generated by MessageUnitTestGenerator in this project.
//	Date and time: 06-04-2018 14:52:28
//
//	Changes to this file may cause incorrect behavior and will be lost on the next code generation.
//</auto-generated>
namespace CM.Payments.Client.Test
{
    [TestClass, ExcludeFromCodeCoverage]
	public partial class DirectDebitPaymentResponseTests : BaseJsonConvertTests
	{
		public TestContext TestContext { get; set; }

		[TestMethod]
		public void DirectDebitPaymentResponse()
		{
			var obj = new DirectDebitPaymentResponse
			{
				Amount = 32,
				ChargeId = "Lorum_349",
				CreatedAt = DateTime.Now,
				Currency = "Lorum_18",
				Details = new DirectDebitDetailsResponse
				{
					AuthenticationUrl = "Lorum_20",
					BankAccountNumber = "Lorum_638",
					CallbackUrl = "Lorum_810",
					CancelledUrl = "Lorum_741",
					Description = "Lorum_746",
					ExpiredUrl = "Lorum_911",
					FailedUrl = "Lorum_963",
					MandateId = "Lorum_468",
					MandateStartDate = DateTime.Now,
					Name = "Lorum_404",
					PurchaseId = "Lorum_823",
					ReversedOn = DateTime.Now,
					ReverseReasonCode = "Lorum_539",
					ReverseReasonDescription = "Lorum_334",
					SuccessUrl = "Lorum_412",
					TransactionDescription = "Lorum_461",
					TransactionId = "Lorum_300"
				},
				DueDate = DateTime.Now,
				ExpiresAt = DateTime.Now,
				PaymentId = "Lorum_748",
				Recurring = true,
				RecurringId = "Lorum_215",
				ShortPaymentId = "Lorum_392",
				Status = "Lorum_377",
				Test = true,
				UpdatedAt = DateTime.Now
			};
			var deserialized = ConversionTest(obj);
			Assert.IsNotNull(deserialized);
			Assert.AreEqual(obj.ChargeId, deserialized.ChargeId);
			// Check only date and time up to seconds.. Json.NET does not save milliseconds.
			Assert.AreEqual(
				new DateTime(obj.CreatedAt.Year, obj.CreatedAt.Month, obj.CreatedAt.Day, obj.CreatedAt.Hour, obj.CreatedAt.Minute, obj.CreatedAt.Second),
				new DateTime(deserialized.CreatedAt.Year, deserialized.CreatedAt.Month, deserialized.CreatedAt.Day, deserialized.CreatedAt.Hour, deserialized.CreatedAt.Minute, deserialized.CreatedAt.Second));
			Assert.AreEqual(obj.Currency, deserialized.Currency);
			Assert.AreEqual(obj.Details.AuthenticationUrl, deserialized.Details.AuthenticationUrl);
			Assert.AreEqual(obj.Details.BankAccountNumber, deserialized.Details.BankAccountNumber);
			Assert.AreEqual(obj.Details.CallbackUrl, deserialized.Details.CallbackUrl);
			Assert.AreEqual(obj.Details.CancelledUrl, deserialized.Details.CancelledUrl);
			Assert.AreEqual(obj.Details.Description, deserialized.Details.Description);
			Assert.AreEqual(obj.Details.ExpiredUrl, deserialized.Details.ExpiredUrl);
			Assert.AreEqual(obj.Details.FailedUrl, deserialized.Details.FailedUrl);
			Assert.AreEqual(obj.Details.MandateId, deserialized.Details.MandateId);
		    if (obj.Details.MandateStartDate.HasValue && deserialized.Details.MandateStartDate.HasValue)
		    {
		        // Check only date and time up to seconds.. Json.NET does not save milliseconds.
		        Assert.AreEqual(
		            new DateTime(obj.Details.MandateStartDate.Value.Year, obj.Details.MandateStartDate.Value.Month, obj.Details.MandateStartDate.Value.Day),
		            new DateTime(deserialized.Details.MandateStartDate.Value.Year, deserialized.Details.MandateStartDate.Value.Month, deserialized.Details.MandateStartDate.Value.Day));
		    }
            Assert.AreEqual(obj.Details.Name, deserialized.Details.Name);
			Assert.AreEqual(obj.Details.PurchaseId, deserialized.Details.PurchaseId);
		    if (obj.Details.ReversedOn.HasValue && deserialized.Details.ReversedOn.HasValue)
		    {
		        // Check only date and time up to seconds.. Json.NET does not save milliseconds.
		        Assert.AreEqual(
		            new DateTime(obj.Details.ReversedOn.Value.Year, obj.Details.MandateStartDate.Value.Month, obj.Details.ReversedOn.Value.Day, obj.Details.ReversedOn.Value.Hour, obj.Details.ReversedOn.Value.Minute, obj.Details.ReversedOn.Value.Second),
		            new DateTime(deserialized.Details.ReversedOn.Value.Year, deserialized.Details.ReversedOn.Value.Month, deserialized.Details.ReversedOn.Value.Day, deserialized.Details.ReversedOn.Value.Hour, deserialized.Details.ReversedOn.Value.Minute, deserialized.Details.ReversedOn.Value.Second));
		    }
			Assert.AreEqual(obj.Details.ReverseReasonCode, deserialized.Details.ReverseReasonCode);
			Assert.AreEqual(obj.Details.ReverseReasonDescription, deserialized.Details.ReverseReasonDescription);
			Assert.AreEqual(obj.Details.SuccessUrl, deserialized.Details.SuccessUrl);
			Assert.AreEqual(obj.Details.TransactionDescription, deserialized.Details.TransactionDescription);
			Assert.AreEqual(obj.Details.TransactionId, deserialized.Details.TransactionId);
		    if (obj.DueDate.HasValue && deserialized.DueDate.HasValue)
		    {
		        // Check only date and time up to seconds.. Json.NET does not save milliseconds.
		        Assert.AreEqual(
		            new DateTime(obj.DueDate.Value.Year, obj.DueDate.Value.Month, obj.DueDate.Value.Day, obj.DueDate.Value.Hour, obj.DueDate.Value.Minute, obj.DueDate.Value.Second),
		            new DateTime(deserialized.DueDate.Value.Year, deserialized.DueDate.Value.Month, deserialized.DueDate.Value.Day, deserialized.DueDate.Value.Hour, deserialized.DueDate.Value.Minute, deserialized.DueDate.Value.Second));
		    }
		    if (obj.ExpiresAt.HasValue && deserialized.ExpiresAt.HasValue)
		    {
		        // Check only date and time up to seconds.. Json.NET does not save milliseconds.
		        Assert.AreEqual(
		            new DateTime(obj.ExpiresAt.Value.Year, obj.ExpiresAt.Value.Month, obj.ExpiresAt.Value.Day, obj.ExpiresAt.Value.Hour, obj.ExpiresAt.Value.Minute, obj.ExpiresAt.Value.Second),
		            new DateTime(deserialized.ExpiresAt.Value.Year, deserialized.ExpiresAt.Value.Month, deserialized.ExpiresAt.Value.Day, deserialized.ExpiresAt.Value.Hour, deserialized.ExpiresAt.Value.Minute, deserialized.ExpiresAt.Value.Second));
		    }
            Assert.AreEqual(obj.PaymentId, deserialized.PaymentId);
			Assert.AreEqual(obj.Recurring, deserialized.Recurring);
			Assert.AreEqual(obj.RecurringId, deserialized.RecurringId);
			Assert.AreEqual(obj.ShortPaymentId, deserialized.ShortPaymentId);
			Assert.AreEqual(obj.Status, deserialized.Status);
			Assert.AreEqual(obj.Test, deserialized.Test);
		    if (obj.UpdatedAt.HasValue && deserialized.UpdatedAt.HasValue)
		    {
		        // Check only date and time up to seconds.. Json.NET does not save milliseconds.
		        Assert.AreEqual(
		            new DateTime(obj.UpdatedAt.Value.Year, obj.UpdatedAt.Value.Month, obj.UpdatedAt.Value.Day, obj.UpdatedAt.Value.Hour, obj.UpdatedAt.Value.Minute, obj.UpdatedAt.Value.Second),
		            new DateTime(deserialized.UpdatedAt.Value.Year, deserialized.UpdatedAt.Value.Month, deserialized.UpdatedAt.Value.Day, deserialized.UpdatedAt.Value.Hour, deserialized.UpdatedAt.Value.Minute, deserialized.UpdatedAt.Value.Second));
		    }
        }
	}
}