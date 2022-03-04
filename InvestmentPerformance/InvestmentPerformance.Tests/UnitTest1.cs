using Microsoft.VisualStudio.TestTools.UnitTesting;
using InvestmentPerformance.Business;
using InvestmentPerformance.Business.Models;
using System;
using InvestmentPerformance.Data.Model;

namespace InvestmentPerformance.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var businessservice = new BusinessService();
            var listing = new Listing
            {
                CompanyName = "Alphabet",
                CurrentPrice = 123.45m,
                Id = 1
            };

            var userInvestment = new UserInvestmentDetailsVM
            {
                Id = 1,
                AmountOfShares = 54.67m,
                ListingId = 1,
                PurchaseDate = new DateTime(2022, 3, 3),
                SharePurchasePrice = 52.33m,
                UserId = 1
            };

            var currentPrice = businessservice.GetCurrentValue(listing, userInvestment);
            Assert.AreEqual(6749.0115m, currentPrice);
        }        

        [TestMethod]
        public void TestMethod2()
        {
            var businessservice = new BusinessService();

            var userInvestment = new UserInvestmentDetailsVM
            {
                Id = 1,
                AmountOfShares = 54.67m,
                ListingId = 1,
                PurchaseDate = new DateTime(2022, 3, 3),
                SharePurchasePrice = 52.33m,
                UserId = 1
            };

            Assert.AreEqual(TermEnum.Short, userInvestment.Term);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var businessservice = new BusinessService();

            var userInvestment = new UserInvestmentDetailsVM
            {
                Id = 1,
                AmountOfShares = 54.67m,
                ListingId = 1,
                PurchaseDate = new DateTime(2021, 3, 3),
                SharePurchasePrice = 52.33m,
                UserId = 1
            };
            
            Assert.AreEqual(TermEnum.Long, userInvestment.Term);
        }

        [TestMethod]
        public void TestMethod4()
        {
            var businessservice = new BusinessService();
            var listing = new Listing
            {
                CompanyName = "Alphabet",
                CurrentPrice = 123.45m,
                Id = 1
            };

            var userInvestment = new UserInvestmentDetailsVM
            {
                Id = 1,
                AmountOfShares = 54.67m,
                ListingId = 1,
                PurchaseDate = new DateTime(2022, 3, 3),
                SharePurchasePrice = 52.33m,
                UserId = 1
            };

            var gainLoss = businessservice.GetGainLoss(listing, userInvestment);
            Assert.AreEqual(3888.1304m, gainLoss);
        }
    }
}