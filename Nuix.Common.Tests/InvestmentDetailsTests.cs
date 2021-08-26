using Nuix.Common.Models;
using NUnit.Framework;
using System;

namespace Nuix.Common.Tests
{
  [TestFixture]
  public class InvestmentDetailsTests
  {
    [Test]
    [TestCase("1/1/2021", 100, 1.25, 1.5, "6/1/2021", 150, false, 25)]
    [TestCase("1/1/2021", 100, 2.01, 1.48, "6/1/2022", 148, true, -53)]
    [TestCase("1/1/2021", .25, 160, 100, "6/1/2025", 25, true, -15)]
    public void ApplyQuote_HasStockQuote_UpdatesProperties(DateTime purchasedUtc, double quantity, decimal costBasis, decimal quote, DateTime quotedUtc,
      decimal expectedCurrentValue, bool expectedIsLongTerm, decimal expectedGainLoss)
    {
      quotedUtc = DateTime.SpecifyKind(quotedUtc, DateTimeKind.Utc);

      InvestmentDetails testUnit = new InvestmentDetails
      {
        PurchasedUtc = purchasedUtc,
        Quantity = quantity,
        CostBasis = costBasis
      };

      StockQuote stockQuote = new StockQuote
      {
        Quote = quote,
        QuotedUTC = quotedUtc
      };

      testUnit.ApplyQuote(stockQuote);

      Assert.AreEqual(expectedCurrentValue, testUnit.CurrentValue);
      Assert.AreEqual(quote, testUnit.CurrentPrice);
      Assert.AreEqual(expectedIsLongTerm, testUnit.IsLongTerm);
      Assert.AreEqual(expectedGainLoss, testUnit.GainLoss);
    }
  }
}