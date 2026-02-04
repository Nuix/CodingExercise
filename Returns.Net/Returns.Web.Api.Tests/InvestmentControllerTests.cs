using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Returns.Models;

namespace Returns.Web.Api.Tests;

public class InvestmentControllerTests
{
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _jsonOptions;

    public InvestmentControllerTests()
    {
        _client = new();
        _client.BaseAddress = new Uri("http://localhost:5299");
        _jsonOptions = new() {
            PropertyNameCaseInsensitive = true
        };
    }

    [Fact]
    public async Task GetUserInvestments_ReturnsInvestmentsForUser()
    {
        var userId = TestData.User1Id;
        var url = $"/user/{userId}/Investments";
        var response = await _client.GetAsync(url);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var investments = await response.Content.ReadFromJsonAsync<List<InvestmentRes>>(_jsonOptions);
        Assert.NotNull(investments);
        Assert.NotEmpty(investments);
        Assert.All(investments!, investment => Assert.Equal(userId, investment.UserId));
    }

    [Fact]
    public async Task QueryInvestments_WithStartAndAmount_PaginatesCorrectly()
    {
        var query = new InvestmentQuery { Start = 0, Amount = 2 };
        var url = ApiRoutes.Investment.Query;
        var response = await _client.PostAsJsonAsync(url, query);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var investments = await response.Content.ReadFromJsonAsync<List<InvestmentRes>>(_jsonOptions);
        Assert.NotNull(investments);
        Assert.True(investments!.Count <= 2);
    }
    
    //can add more tests ad needed, would suggest more complex setup/teardown, logging, etc
}

