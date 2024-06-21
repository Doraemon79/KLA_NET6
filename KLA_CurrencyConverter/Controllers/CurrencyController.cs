using KLA_NumberConverter_ServerSide.Logic;
using Microsoft.AspNetCore.Mvc;

namespace KLA_NumberConverter_ServerSide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ConvertLogic _convertLogic;

        public CurrencyController()
        {
            _convertLogic = new ConvertLogic();
        }

        [HttpGet("convert")]
        public IActionResult Convert([FromQuery] string amount)
        {
            if (string.IsNullOrEmpty(amount))
            {
                return BadRequest("Amount is required.");
            }

            try
            {
                var result = _convertLogic.AmountToWords(amount);
                return Ok(new { Words = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
