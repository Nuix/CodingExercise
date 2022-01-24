using InvestmentPerformanceDomain.Models;
using InvestmentPerformanceWebApi.Controllers;
using InvestmentPerformanceWebApi.Services;
using InvestmentPerformanceWebApi.Test.Spys;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace InvestmentPerformanceWebApi.Test.Controllers;

public class UserControllerTest
{
    private readonly UserController _userController;
    private readonly Mock<IUserService> _userService = new Mock<IUserService>();
    private readonly SpyLogger<UserController> _spyLogger = new SpyLogger<UserController>();


    private readonly List<User> UserListSample = new List<User>
    {
        new User { Id= 1, Name= "Frank"},
        new User { Id= 2, Name="Bob"},
    };

    public UserControllerTest()
    {
        _userController = new UserController(_userService.Object, _spyLogger);
        _userController.MockProblemDetailFactory();
    }

    [Fact]
    public async Task GetAll_OkResult()
    {
        // Arrange
        _userService.Setup(u => u.GetUsersAsync()).ReturnsAsync(UserListSample).Verifiable();

        // Act
        var result = await _userController.GetAll();

        // Assert
        _userService.Verify();
        var statusCodeResult = Assert.IsAssignableFrom<ObjectResult>(result);
        Assert.Equal(200, statusCodeResult.StatusCode);
        var returnObject = Assert.IsAssignableFrom<IList<User>>(statusCodeResult.Value);
        Assert.Equal(UserListSample.Count, returnObject.Count);
        Assert.Equal(UserListSample[0].Id, returnObject[0].Id);
        Assert.Equal(UserListSample[0].Name, returnObject[0].Name);
        Assert.Equal(UserListSample[1].Id, returnObject[1].Id);
        Assert.Equal(UserListSample[1].Name, returnObject[1].Name);
        Assert.False(_spyLogger.LoggerWasCalled);
    }

    [Fact]
    public async Task GetAll_OkEmptyResult()
    {
        // Arrange
        _userService.Setup(u => u.GetUsersAsync()).ReturnsAsync(new List<User>()).Verifiable();

        // Act
        var result = await _userController.GetAll();

        // Assert
        _userService.Verify();
        var statusCodeResult = Assert.IsAssignableFrom<ObjectResult>(result);
        Assert.Equal(200, statusCodeResult.StatusCode);
        var returnObject = Assert.IsAssignableFrom<IList<User>>(statusCodeResult.Value);
        Assert.Empty(returnObject);
        Assert.False(_spyLogger.LoggerWasCalled);
    }


    [Fact]
    public async Task GetAll_ReturnProblem()
    {
        // Arrange
        _userService.Setup(u => u.GetUsersAsync()).ThrowsAsync(new Exception("Bad")).Verifiable();

        // Act
        var result = await _userController.GetAll();

        // Assert
        _userService.Verify();
        var statusCodeResult = Assert.IsAssignableFrom<ObjectResult>(result);
        Assert.Equal(500, statusCodeResult.StatusCode);
        var problemDetails = Assert.IsAssignableFrom<ProblemDetails>(statusCodeResult.Value);
        Assert.True(_spyLogger.LoggerWasCalled);
        Assert.Equal(LogLevel.Error, _spyLogger.Logs[0].LogLevel);
        Assert.Equal("Error getting user data", _spyLogger.Logs[0].Message);
        Assert.NotNull(_spyLogger.Logs[0].Ex);
        Assert.Equal("Bad", _spyLogger.Logs[0].Ex?.Message);
        Assert.NotNull(problemDetails);
        Assert.Equal("Error getting user data", problemDetails.Detail);
    }
}
