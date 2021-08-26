using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Nuix.Api.Controllers;
using Nuix.Common.Models;
using Nuix.Common.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Nuix.Api.Tests
{
  [TestFixture]
  public class InvestmentsControllerTests
  {
    private Mock<ILogger<InvestmentsController>> _loggerMock;
    private Lazy<ILogger<InvestmentsController>> _lazyLogger;
    private Mock<IInvestmentService> _investmentServiceMock;

    [SetUp]
    public void Setup()
    {
      _loggerMock = new Mock<ILogger<InvestmentsController>>();
      _lazyLogger = new Lazy<ILogger<InvestmentsController>>(() => _loggerMock.Object);

      _investmentServiceMock = new Mock<IInvestmentService>();
    }

    private InvestmentsController Create()
    {
      return new InvestmentsController(_lazyLogger, _investmentServiceMock.Object);
    }

    [Test]
    public async Task GetUserInvestment_NullInvestment_ReturnsNotFound()
    {
      InvestmentsController testUnit = Create();

      _investmentServiceMock.Setup(i => i.GetUserInvestmentDetails(It.IsAny<long>(), It.IsAny<long>())).ReturnsAsync((InvestmentDetails)null);

      IActionResult actual = await testUnit.GetUserInvestment(0, 0);
      NotFoundResult notFoundResult = actual as NotFoundResult;

      Assert.IsNotNull(notFoundResult);
      Assert.AreEqual((int)HttpStatusCode.NotFound, notFoundResult.StatusCode);
    }

    [Test]
    public async Task GetUserInvestment_InvestmentRetrieved_ReturnsOkObjectResult()
    {
      InvestmentsController testUnit = Create();

      _investmentServiceMock.Setup(i => i.GetUserInvestmentDetails(It.IsAny<long>(), It.IsAny<long>())).ReturnsAsync(new InvestmentDetails());

      IActionResult actual = await testUnit.GetUserInvestment(0, 0);
      OkObjectResult okObjectResult = actual as OkObjectResult;

      Assert.IsNotNull(okObjectResult);
      Assert.AreEqual((int)HttpStatusCode.OK, okObjectResult.StatusCode);
    }

    [Test]
    public async Task GetUserInvestment_InvestmentServiceThrows_ExceptionHandled()
    {
      InvestmentsController testUnit = Create();

      _investmentServiceMock.Setup(i => i.GetUserInvestmentDetails(It.IsAny<long>(), It.IsAny<long>())).ThrowsAsync(new System.Exception());

      IActionResult actual = await testUnit.GetUserInvestment(0, 0);

      _loggerMock.Verify(l => l.Log(It.IsAny<LogLevel>(),
        It.IsAny<EventId>(),
        It.Is<It.IsAnyType>((v, t) => true),
        It.IsAny<Exception>(),
        It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)));

      StatusCodeResult statusCodeResult = actual as StatusCodeResult;

      Assert.IsNotNull(statusCodeResult);
      Assert.AreEqual((int)HttpStatusCode.InternalServerError, statusCodeResult.StatusCode);
    }

    [Test]
    public async Task GetUserInvestments_InvestmentsExist_ReturnsOkObjectResult()
    {
      InvestmentsController testUnit = Create();

      _investmentServiceMock.Setup(i => i.GetUserInvestments(It.IsAny<long>())).ReturnsAsync(new InvestmentDetailsLight[]{ });

      IActionResult actual = await testUnit.GetUserInvestments(0);
      OkObjectResult okObjectResult = actual as OkObjectResult;

      Assert.IsNotNull(okObjectResult);
      Assert.AreEqual((int)HttpStatusCode.OK, okObjectResult.StatusCode);
    }

    [Test]
    public async Task GetUserInvestments_InvestmentServiceThrows_ExceptionHandled()
    {
      InvestmentsController testUnit = Create();

      _investmentServiceMock.Setup(i => i.GetUserInvestments(It.IsAny<long>())).ThrowsAsync(new System.Exception());

      IActionResult actual = await testUnit.GetUserInvestments(0);

      _loggerMock.Verify(l => l.Log(It.IsAny<LogLevel>(),
        It.IsAny<EventId>(),
        It.Is<It.IsAnyType>((v, t) => true),
        It.IsAny<Exception>(),
        It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)));

      StatusCodeResult statusCodeResult = actual as StatusCodeResult;

      Assert.IsNotNull(statusCodeResult);
      Assert.AreEqual((int)HttpStatusCode.InternalServerError, statusCodeResult.StatusCode);
    }
  }
}