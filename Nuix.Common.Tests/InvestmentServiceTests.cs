using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Nuix.Api.Data.Contexts;
using Nuix.Api.Data.Entities;
using Nuix.Common.Exceptions;
using Nuix.Common.Models;
using Nuix.Common.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nuix.Common.Tests
{
  [TestFixture]
  public class InvestmentServiceTests
  {
    private Mock<INuixContext> _nuixContextMock;
    private Lazy<IStockQuoteService> _lazyStockQuoteService;
    private Mock<IStockQuoteService> _stockQuoteServiceMock;
    private Mock<DbSet<Investment>> _investmentsDbSetMock;

    [SetUp]
    public void Setup()
    {
      _investmentsDbSetMock = new [] {
        new Investment { InvestmentId = 1, UserId = 1, Name = "investment", Symbol = "sym" },
        new Investment { InvestmentId = 2, UserId = 1, Name = "investment 2", Symbol = "sym2" },
        new Investment { InvestmentId = 3, UserId = 1, Name = "investment 3", Symbol = "sym3" }}.AsQueryable().BuildMockDbSet();

      _nuixContextMock = new Mock<INuixContext>();
      _nuixContextMock.Setup(c => c.Investments).Returns(_investmentsDbSetMock.Object);

      _stockQuoteServiceMock = new Mock<IStockQuoteService>();
      _stockQuoteServiceMock.Setup(s => s.GetQuote(It.IsAny<string>())).ReturnsAsync(new StockQuote { Quote = 1, QuotedUTC = DateTime.UtcNow });
      _lazyStockQuoteService = new Lazy<IStockQuoteService>(() => _stockQuoteServiceMock.Object);
    }

    private InvestmentService CreateService()
    {
      return new InvestmentService(_nuixContextMock.Object, _lazyStockQuoteService);
    }

    [Test]
    public async Task GetUserInvestmentDetails_InvestmentWithStockQuote_RetrievesInvestmentDetails()
    {
      InvestmentService testUnit = CreateService();

      InvestmentDetails actual = await testUnit.GetUserInvestmentDetails(1, 1);

      Assert.IsNotNull(actual);
    }

    [Test]
    public async Task GetUserInvestmentDetails_InvestmentDoesNotExist_ReturnsNull()
    {
      InvestmentService testUnit = CreateService();

      InvestmentDetails actual = await testUnit.GetUserInvestmentDetails(0, 0);

      Assert.IsNull(actual);
    }

    [Test]
    public async Task GetUserInvestmentDetails_InvestmentWithNullStockQuote_ThrowsException()
    {
      InvestmentService testUnit = CreateService();

      _stockQuoteServiceMock.Setup(s => s.GetQuote(It.IsAny<string>())).ReturnsAsync((StockQuote)null);

      StockQuoteRetrievalException ex = Assert.ThrowsAsync<StockQuoteRetrievalException>(() => testUnit.GetUserInvestmentDetails(1,1));
    }

    [Test]
    public async Task GetUserInvestments_UserHasInvestments_ReturnsInvestments()
    {
      InvestmentService testUnit = CreateService();

      IEnumerable<InvestmentDetailsLight> actual = await testUnit.GetUserInvestments(1);

      List<InvestmentDetailsLight> expected = new List<InvestmentDetailsLight>(new InvestmentDetailsLight[]{
        new InvestmentDetailsLight { InvestmentId = 1, Name = "investment", Symbol = "sym" },
        new InvestmentDetailsLight { InvestmentId = 2, Name = "investment 2", Symbol = "sym2" },
        new InvestmentDetailsLight { InvestmentId = 3, Name = "investment 3", Symbol = "sym3" }});

      Assert.IsTrue(actual.SequenceEqual(expected));
    }

    [Test]
    public async Task GetUserInvestments_UserWithoutInvestments_ReturnsEmptySet()
    {
      InvestmentService testUnit = CreateService();

      IEnumerable<InvestmentDetailsLight> actual = await testUnit.GetUserInvestments(2);

      Assert.IsEmpty(actual);
    }
  }
}