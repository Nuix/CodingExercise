using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;

namespace InvestmentPerformanceWebApi.Test.Controllers;

internal static class ControllerTestHelper
{
    public static void MockProblemDetailFactory(this ControllerBase controller)
    {
        var problemDetailsFactoryMock = new Mock<ProblemDetailsFactory>();
        var _mockProblemDetails = new ProblemDetails();

        problemDetailsFactoryMock
            .Setup(pd => pd.CreateProblemDetails(
                It.IsAny<HttpContext>(),
                It.IsAny<int?>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>())
            )
            .Returns(_mockProblemDetails)
            .Callback(
                (HttpContext context, int? statusCode, string title, string type, string detail, string instance) =>
                {
                    _mockProblemDetails.Detail = detail;
                    _mockProblemDetails.Instance = instance;
                    _mockProblemDetails.Status = statusCode;
                    _mockProblemDetails.Type = type;
                    _mockProblemDetails.Title = title;
                }
            );

        controller.ProblemDetailsFactory = problemDetailsFactoryMock.Object;
    }
}
