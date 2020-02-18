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
        private readonly ILogger<SalesController> _logger;

        private SalesService _salesService;

        public SalesController(ILogger<SalesController> logger, IMongoClient mongoClient)
        {
            _logger = logger;

            _salesService = new SalesService(mongoClient);
        }

        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Post(string articleNumber, decimal salesPrice)
        {
            try
            { 
                _salesService.Insert(articleNumber, salesPrice);
            }
            catch(FormatException formatEx)
            {
                return BadRequest(formatEx.Message);
            }
            

            return Ok();
        }
    }
}
