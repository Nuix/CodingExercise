using Microsoft.VisualStudio.TestTools.UnitTesting;
using InvestmentPerformance.Business;
using InvestmentPerformance.Business.Models;
using System;
using InvestmentPerformance.Data.Model;
using Shouldly;

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

            userInvestmentDetailsVM.CurrentValue.ShouldBe(6749.0115m);
        }

        [TestMethod]
        public void term_should_calculate_short()
        {
            // should use fakes to shim a specific datetime but
            // fakes are not available in Visual Studio Community
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

            userInvestmentVM.Term.ShouldBe(Constants.Short);
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

            userInvestment.Term.ShouldBe(Constants.Long);
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

            var userInvestmentDetailsVM = new UserInvestmentDetailsVM().MapFrom(ui);

            userInvestmentDetailsVM.GainLoss.ShouldBe(3888.1304m);
        }

        [TestMethod]
        public void full_name_should_map_correctly()
        {
            var u = new User
            {
                Id = 1,
                FirstName = "Shane",
                LastName = "Todd"
            };

            var userVM = new UserVM().MapFrom(u);

            userVM.FullName.ShouldBe("Shane Todd");
        }
    }
}