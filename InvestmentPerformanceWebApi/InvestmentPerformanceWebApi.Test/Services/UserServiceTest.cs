using InvestmentPerformanceDomain.Models;
using InvestmentPerformanceDomain.Repository;
using InvestmentPerformanceWebApi.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace InvestmentPerformanceWebApi.Test.Services;

public class UserServiceTest
{
    private readonly UserService _userService;
    private readonly Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();


    private readonly List<User> UserListSample = new List<User>
    {
        new User { Id= 1, Name= "Frank"},
        new User { Id= 2, Name="Bob"},
    };

    public UserServiceTest()
    {
        _userService = new UserService(_userRepository.Object);
    }

    [Fact]
    public async Task GetUsersAsync_WithSampleResults()
    {
        // Arrange
        _userRepository.Setup(u => u.GetUsersAsync()).ReturnsAsync(UserListSample).Verifiable();

        // Act
        var result = await _userService.GetUsersAsync();

        // Assert
        _userRepository.Verify();
        Assert.Equal(UserListSample.Count, result.Count);
        Assert.Equal(UserListSample[0].Id, result[0].Id);
        Assert.Equal(UserListSample[0].Name, result[0].Name);
        Assert.Equal(UserListSample[1].Id, result[1].Id);
        Assert.Equal(UserListSample[1].Name, result[1].Name);
    }

    [Fact]
    public async Task GetUsersAsync_WithEmptyResults()
    {
        // Arrange
        _userRepository.Setup(u => u.GetUsersAsync()).ReturnsAsync(new List<User>()).Verifiable();

        // Act
        var result = await _userService.GetUsersAsync();

        // Assert
        _userRepository.Verify();
        Assert.Empty(result);
    }


    [Fact]
    public async Task GetUsersAsync_WithException()
    {
        // Arrange
        _userRepository.Setup(u => u.GetUsersAsync()).ThrowsAsync(new Exception("Bad")).Verifiable();
        Exception? exception = null;

        // Act
        try
        {
            var result = await _userService.GetUsersAsync();
        }
        catch (Exception ex)
        {
            exception = ex;
        }

        // Assert
        _userRepository.Verify();
        Assert.NotNull(exception);
        Assert.Equal("Bad", exception?.Message);
    }
}
