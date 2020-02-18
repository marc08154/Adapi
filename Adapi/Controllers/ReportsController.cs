using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Adapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IMongoClient _mongoClient;

        public ReportsController(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        // Number of sold articles per day
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return null;
        }

        // Total revenue per day

        // Statistics: Revenue grouped by articles


        // GET: api/Reports/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        
    }
}
