using Application.Commands.AddConfigurations;
using Application.Commands.ClearCurrencies;
using Application.Common.Rules;
using Application.Domains;
using Application.Queries.ConvertCurrencies;
using Application.Queries.GetAllCurrencies;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConvertor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CurrencyController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpDelete]
        public async Task<IActionResult> ClearCurrencies()
        {
            try
            {
                await _mediator.Send(new ClearCurrenciesCommand());
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }
        [HttpPost("AddConfigurations")]
        public async Task<IActionResult> AddConfigurations([FromBody] IEnumerable<Currency> conversionRates)
        {
            try
            {
                var newConversionRates = conversionRates.ToList()
                    .Where(x => x.SourceCurrency.IsThreeLetters() && x.TargetCurrency.IsThreeLetters() && x.Rate>0)
                    .Select(x =>
                    new Tuple<string, string, double>(x.SourceCurrency.ToUpper(), x.TargetCurrency.ToUpper(), x.Rate));
                var alaki = newConversionRates.ToList();
                await _mediator.Send(new AddConfigurationsCommand(newConversionRates));
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Currency>>> GetAllCurrencies()
        {
            try
            {
                var result= await _mediator.Send(new GetAllCurrenciesQuery());
                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        [HttpPost("ConvertCurrencies")]
        public async Task<ActionResult<double>> ConvertCurrencies(string fromCurrency, string toCurrency, double amount)
        {
            try
            {
                if (fromCurrency.IsThreeLetters() && toCurrency.IsNormalized() && amount>0)
                {
                    var result = await _mediator.Send(new ConvertCurrenciesQuery(fromCurrency, toCurrency, amount));
                    return Ok(result);
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    }
}
