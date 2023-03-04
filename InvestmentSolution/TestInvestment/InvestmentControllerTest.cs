namespace TestInvestment;

using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using InvestmentPerformance.Controllers;
using InvestmentPerformance.Services;
using InvestmentPerformance.Models;
using System.Collections;
using Xunit;

public class InvestmentControllerTest
{
    public readonly InvestController _controller;
    public readonly IUserRepository _service;

    public InvestmentControllerTest()
    {
        _service = new UserRepository();
        _controller = new InvestController(_service);
    }

    [Fact]
    public void TestApiType()
    {
        //Arrange
        Guid testId = System.Guid.NewGuid();

        //Act
        var result = _controller.GetInvestmentsById(testId);
        
        //Assert
        Assert.IsType<ObjectResult>(result.Result);
    }

    [Fact]
    public void TestApiSuccess()
    {
        //Arrange
        Guid testId = new Guid("f78c3d80-857d-407e-b7dd-8d2e9416fc78");

        //Act
        var result = _controller.GetInvestmentsById(testId);

        //Assert
        var list = result.Result as ObjectResult;
        Assert.IsType<List<Investment>>(list.Value);

        var listInvestments = list.Value as List<Investment>;
        Assert.Equal(2, listInvestments.Count);
    }

    [Fact]
    public void TestInvestmentDetails()
    {
        //Arrange
        Guid testId = new Guid("f78c3d80-857d-407e-b7dd-8d2e9416fc78");
        Guid investId = new Guid("480a8608-6611-46a7-a3a4-16d7b5cdfe9f");

        //Act
        var result = _controller.GetInvestmentDetails(testId, investId);
        var list = result.Result as ObjectResult;
        Assert.IsType<List<InvestmentDetails>>(list.Value);

        var listInvestments = list.Value as List<InvestmentDetails>;

        foreach(InvestmentDetails i in listInvestments)
        {
            var CurrentValue =i.CurrentValue;
            var ExpectedCurrentValue = i.CurrentPrice * i.NumberOfShares;
            Assert.Equal(CurrentValue, ExpectedCurrentValue);
        }

    }
}
