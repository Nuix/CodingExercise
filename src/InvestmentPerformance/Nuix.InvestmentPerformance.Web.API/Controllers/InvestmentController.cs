using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nuix.InvestmentPerformance.Data.Dtos;
using Nuix.InvestmentPerformance.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nuix.InvestmentPerformance.Web.API.Controllers
{
    public class InvestmentController : Controller
    {
        private readonly ILogger _logger;
        private readonly IInvestmentConnector _investmentConnector;
        private readonly IStockConnector _stockConnector;
        private readonly IPriceConnector _priceConnector;
        public InvestmentController(ILogger<InvestmentController> logger,IInvestmentConnector investmentConnector,IStockConnector stockConnector,IPriceConnector priceConnector)
        {
            _logger = logger;
            _investmentConnector = investmentConnector;
            _stockConnector = stockConnector;
            _priceConnector = priceConnector;
        }

        [AcceptVerbs("GET")]
        [Route("Investments/{id}")]
        public IActionResult GetInvestment(int id)
        {
            _logger.LogInformation($"GetInvestment : {id}");
            try
            {
                var investment = _investmentConnector.GetInvestment(id);
                //mapping would normally not happen here, it would happen lower in the stack
                //doing the mapping here to save time and show the calculations
                InvestmentDto investmentDto = new InvestmentDto {
                    InvestmentId = investment.InvestmentId,
                    CostBasis = investment.CostBasis,
                    NumberOfShares = investment.NumberOfShares,
                    PurchaseDate = investment.PurchaseDate,
                    Symbol = investment.Symbol,
                    UserId = investment.UserId,
                    Term = investment.PurchaseDate <= DateTime.Now.Date.AddYears(-1) ? Data.Enums.Terms.LongTerm : Data.Enums.Terms.ShortTerm                    
                };
                investmentDto.Stock = _stockConnector.GetStock(investment.Symbol);
                investmentDto.Price = _priceConnector.GetCurrentPrice(investment.Symbol);
                investmentDto.Value = investmentDto.Price * investmentDto.NumberOfShares;
                investmentDto.TotalGain = investmentDto.Value - (investmentDto.NumberOfShares * investmentDto.CostBasis);
                if (investment?.InvestmentId == 0 || investment == null)
                {
                    return NotFound();
                }
               
                return Json(investmentDto);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
            return NotFound();
        }
    }
}
