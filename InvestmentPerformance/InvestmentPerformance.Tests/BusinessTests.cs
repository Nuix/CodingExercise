using Microsoft.VisualStudio.TestTools.UnitTesting;
using InvestmentPerformance.Business;
using InvestmentPerformance.Business.Models;
using System;
using InvestmentPerformance.Data.Model;

namespace InvestmentPerformance.Tests
{
    [TestClass]
    public class BusinessTests
    {
        [TestMethod]
        public void current_value_should_be_calculated_correctly()
        {
            var ui = new UserInvestment
            {
                Id = 1,
                AmountOfShares = 54.67m,
                ListingId = 1,
                PurchaseDate = new DateTime(2022, 3, 3),
                SharePurchasePrice = 52.33m,
                UserId = 1,
                Listing = new Listing
                {
                    CompanyName = "Alphabet",
                    CurrentPrice = 123.45m,
                    Id = 1
                }
            };

            var userInvestmentDetailsVM = new UserInvestmentDetailsVM().MapFrom(ui);

            Assert.AreEqual(6749.0115m, userInvestmentDetailsVM.CurrentValue);
        }

        [TestMethod]
        public void term_should_calculate_short()
        {
            var ui = new UserInvestment
            {
                Id = 1,
                AmountOfShares = 54.67m,
                ListingId = 1,
                PurchaseDate = new DateTime(2022, 3, 3),
                SharePurchasePrice = 52.33m,
                UserId = 1,
                Listing = new Listing
                {
                    CompanyName = "Sony",
                    CurrentPrice = 321.89m,
                    Id = 1
                }
            };

            var userInvestmentVM = new UserInvestmentDetailsVM().MapFrom(ui);

            Assert.AreEqual(Constants.Short, userInvestmentVM.Term);
        }

        [TestMethod]
        public void term_should_calculate_long()
        {
            var ui = new UserInvestment
            {
                Id = 1,
                AmountOfShares = 54.67m,
                ListingId = 1,
                PurchaseDate = new DateTime(2021, 3, 3),
                SharePurchasePrice = 52.33m,
                UserId = 1,
                Listing = new Listing
                {
                    CompanyName = "Meta",
                    CurrentPrice = 321.89m,
                    Id = 1                    
                }
            };

            var userInvestment = new UserInvestmentDetailsVM().MapFrom(ui);

            Assert.AreEqual(Constants.Long, userInvestment.Term);
        }

        [TestMethod]
        public void gain_loss_should_calculate_correctly()
        {
            var ui = new UserInvestment
            {
                Id = 1,
                AmountOfShares = 54.67m,
                ListingId = 1,
                PurchaseDate = new DateTime(2022, 3, 3),
                SharePurchasePrice = 52.33m,
                UserId = 1,
                Listing = new Listing
                {
                    CompanyName = "Alphabet",
                    CurrentPrice = 123.45m,
                    Id = 1
                }
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

            var userInvestmentDetailsVM = new UserInvestmentDetailsVM().MapFrom(ui);

            Assert.AreEqual(3888.1304m, userInvestmentDetailsVM.GainLoss);
        }
    }
}