using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Adapi.Domain;
using MongoDB.Driver;
using Microsoft.AspNetCore.Http;

namespace Adapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private SalesService _salesService;

        public SalesController(IMongoClient mongoClient)
        {
            _salesService = new SalesService(mongoClient);
        }

        /// <summary>
        /// Posts a sale record for the given articleNumber with the given salesPrice at the current date at time of call
        /// </summary>
        /// <param name="articleNumber">Alphanumeric article number with a length in the range of 1 to 32</param>
        /// <param name="salesPrice">Decimal value, which must be larger than 0.00</param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status408RequestTimeout)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Post(string articleNumber, decimal salesPrice)
        {
            try
            {
                if (articleNumber == null)
                    return BadRequest("ArticleNumber must be provided!");
                if (salesPrice == default(decimal) || salesPrice.CompareTo(0) < 0)
                    return BadRequest("SalesPrice must be provided. Only positive values larger than 0.00 are allowed!");
                _salesService.Insert(articleNumber, salesPrice);
            }
            catch (FormatException formatEx)
            {
                return BadRequest(formatEx.Message);
            }
            catch (TimeoutException timeoutEx)
            {
                return StatusCode(408, "Could not connect to database");
            }

            return Ok();
        }
    }
}
