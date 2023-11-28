using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using InvestmentWebApi.Data.Api;
using InvestmentWebApi.Data.Impl;
using InvestmentWebApi.Models;
using InvestmentWebApi.Services.Api;
using InvestmentWebApi.Services.Impl;

namespace Tests.Data;

[TestFixture]
public class PortfolioManagerTest {
  private IPortfolioManager _sut;
  private FakeDbReader _fakeDbReader;
  private FakeCurrentTimeProvider _fakeTimeProvider;

  [SetUp]
  public void Setup() {
    _fakeDbReader = new FakeDbReader();
    _fakeTimeProvider = new FakeCurrentTimeProvider();
    _sut = new PortfolioManager(_fakeDbReader, _fakeTimeProvider);
  }

  [Test]
  public void GetInvestmentsTest_EmptyDatabase() {
    var investmentSummaries = _sut.GetInvestments(4);

    Assert.That(investmentSummaries, Is.Empty);
  }

  [Test]
  public void GetInvestmentsTest_NoMatches() {
    _fakeDbReader.AddInvestment(new InvestmentRecord() {
        UserId = 3,
        StockName = "Not Matching",
    });
    _fakeDbReader.AddInvestment(new InvestmentRecord() {
        UserId = 5,
        StockName = "Not Matching",
    });

    var investmentSummaries = _sut.GetInvestments(4);

    Assert.That(investmentSummaries, Is.Empty);
  }

  [Test]
  public void GetInvestmentsTest_OneMatch() {
    _fakeDbReader.AddInvestment(new InvestmentRecord() {
        InvestmentId = 0,
        UserId = 4,
    });
    _fakeDbReader.AddInvestment(new InvestmentRecord() {
        InvestmentId = 23,
        UserId = 5,
    });
    _fakeDbReader.AddInvestment(new InvestmentRecord() {
        InvestmentId = 1,
        UserId = 6,
    });

    var investmentSummaries = _sut.GetInvestments(5);

    Assert.That(investmentSummaries, Has.Count.EqualTo(1));
    Assert.That(
        investmentSummaries.Select(
            i => i.InvestmentId),
        Has.Exactly(1).EqualTo(23));
  }

  [Test]
  public void GetInvestmentsTest_MultipleMatches() {
    _fakeDbReader.AddInvestment(new InvestmentRecord() {
        InvestmentId = 0,
        UserId = 4,
        StockName = "Not a Match",
    });
    _fakeDbReader.AddInvestment(new InvestmentRecord() {
        InvestmentId = 23,
        UserId = 5,
        StockName = "Match1",
    });
    _fakeDbReader.AddInvestment(new InvestmentRecord() {
        InvestmentId = 24,
        UserId = 5,
        StockName = "Match2",
    });
    _fakeDbReader.AddInvestment(new InvestmentRecord() {
        InvestmentId = 25,
        UserId = 5,
        StockName = "Match3",
    });
    _fakeDbReader.AddInvestment(new InvestmentRecord() {
        InvestmentId = 1,
        UserId = 6,
        StockName = "Not a Match",
    });

    var investmentSummaries = _sut.GetInvestments(5);

    Assert.That(investmentSummaries, Has.Count.EqualTo(3));
    Assert.That(investmentSummaries, Is.EquivalentTo(new InvestmentSummary[] {
        new(23, "Match1"),
        new(24, "Match2"),
        new(25, "Match3"),
    }));
  }

  [Test]
  public void GetInvestmentDetailsTest_EmptyDatabase() {
    var investmentDetails = _sut.GetInvestmentDetails(5);

    Assert.That(investmentDetails, Is.Null);
  }

  [Test]
  public void GetInvestmentDetailsTest_Matches() {
    _fakeDbReader.AddInvestmentDetailed(new InvestmentDetailedRecord() {
        InvestmentId = 4,
        StockName = "NotMatch",
        CostBasis = 1m,
    });
    _fakeDbReader.AddInvestmentDetailed(new InvestmentDetailedRecord() {
        InvestmentId = 5,
        StockName = "Match",
        CostBasis = 2m,
    });
    _fakeDbReader.AddInvestmentDetailed(new InvestmentDetailedRecord() {
        InvestmentId = 6,
        StockName = "NotMatch",
        CostBasis = 3m,
    });


    var investmentDetails = _sut.GetInvestmentDetails(5);

    Assert.NotNull(investmentDetails);
    Assert.That(investmentDetails!.CostBasis, Is.EqualTo(2m));
  }

  [Test]
  public void GetInvestmentDetailsTest_BaseFields() {
    _fakeDbReader.AddInvestmentDetailed(new InvestmentDetailedRecord() {
        InvestmentId = 5,
        UserId = 6,
        StockId = 7,
        StockName = "Match",
        ShareCount = 14.53m,
        AcquireDate = new DateTime(2023, 1, 1),
        CostBasis = 24.25m,
        CurrentPrice = 27.25m,
    });

    var investmentDetails = _sut.GetInvestmentDetails(5);

    Assert.NotNull(investmentDetails);
    Assert.That(investmentDetails!.ShareCount, Is.EqualTo(14.53m));
    Assert.That(investmentDetails!.CostBasis, Is.EqualTo(24.25m));
    Assert.That(investmentDetails!.CurrentPrice, Is.EqualTo(27.25m));
  }

  [TestCase("2023-01-05T00:00:00", "2022-03-05T00:00:00", HoldingTerm.Short, "Short")]
  [TestCase("2023-03-05T00:00:00", "2022-03-05T00:00:01", HoldingTerm.Short, "Short")]
  [TestCase("2023-03-05T00:00:00", "2022-03-05T00:00:00", HoldingTerm.Long, "Long")]
  [TestCase("2023-03-05T00:00:01", "2022-03-05T00:00:00", HoldingTerm.Long, "Long")]
  [TestCase("2023-04-05T00:00:00", "2022-03-05T00:00:00", HoldingTerm.Long, "Long")]
  public void GetInvestmentDetailsTest_Term(
      string now, string acquireDate, HoldingTerm term, string termString) {
    _fakeTimeProvider.SetNow(DateTime.Parse(now));
    _fakeDbReader.AddInvestmentDetailed(new InvestmentDetailedRecord() {
        InvestmentId = 5,
        StockName = "Match",
        AcquireDate = DateTime.Parse(acquireDate),
        CostBasis = 2m,
    });

    var investmentDetails = _sut.GetInvestmentDetails(5);

    Assert.NotNull(investmentDetails);
    Assert.That(investmentDetails!.CostBasis, Is.EqualTo(2m));
    Assert.That(investmentDetails!.Term, Is.EqualTo(term));
    Assert.That(investmentDetails!.TermString, Is.EqualTo(termString));
  }

  [Test]
  public void GetInvestmentDetailsTest_CurrentValue() {
    _fakeDbReader.AddInvestmentDetailed(new InvestmentDetailedRecord() {
        InvestmentId = 5,
        StockName = "Match",
        CurrentPrice = 5m,
        ShareCount = 5.5m,
    });

    var investmentDetails = _sut.GetInvestmentDetails(5);

    Assert.NotNull(investmentDetails);
    Assert.That(investmentDetails!.CurrentValue, Is.EqualTo(27.5m));
  }

  [Test]
  public void GetInvestmentDetailsTest_TotalGainLoss() {
    _fakeDbReader.AddInvestmentDetailed(new InvestmentDetailedRecord() {
        InvestmentId = 5,
        StockName = "Match",
        CostBasis = 3m,
        CurrentPrice = 5m,
        ShareCount = 5.5m,
    });

    var investmentDetails = _sut.GetInvestmentDetails(5);

    Assert.NotNull(investmentDetails);
    Assert.That(investmentDetails!.TotalGainLoss, Is.EqualTo(11m));
  }
}