using CurrencyToWordConverter.Server.API.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyToWordConverter.Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConvertController : ControllerBase
    {
        private readonly ICurrencyWordConverterService _currencyService;

        public ConvertController(ICurrencyWordConverterService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet(Name = "ConvertCurrencyIntoWords")]
        public async Task<IActionResult> ConvertCurrencyIntoWords(decimal currency)
        {
            if (currency > 999_999_999.99m || currency < 0)
                return BadRequest("Amount is out of range.");
            try
            {
                string valueInWords = await _currencyService.CurrencyConversion(currency);
                return Ok(new { data = valueInWords });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
