using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Adapi.Domain;
using Adapi.Models;
using System.Linq;
using System.Text.RegularExpressions;

namespace Adapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly SalesStatisticsRepository _salesStatisticsRepository;

        public ReportsController(IMongoClient mongoClient)
        {
            _salesStatisticsRepository = new SalesStatisticsRepository(mongoClient);
        }

        /// <summary>
        /// Returns the number of sales for all articles on a given date
        /// </summary>
        /// <param name="date">Desired date for statistics report in the format yyyy-MM-dd. If omitted, all available entries are returned</param>
        [HttpGet("date/sales")]
        public IActionResult DailySales(DateTime date)
        {
            try
            {
                if (date != DateTime.MinValue)
                    return Ok(_salesStatisticsRepository.GetDailySalesStatistics(date).ToList());
                else return Ok(_salesStatisticsRepository.GetDailySalesStatistics());
            }           

            catch (TimeoutException timeoutEx)
            {
                return StatusCode(408, "Could not connect to database");
            }
        }

        /// <summary>
        /// Returns the total revenue for all products on the given date
        /// </summary>
        /// <param name="date">Desired date for statistics report in the format yyyy-MM-dd. If omitted, all available entries are returned</param>
        [HttpGet("date/revenue")]
        public IActionResult DailyRevenue(DateTime date)
        {
            try
            {
                if (date != DateTime.MinValue)
                    return Ok(_salesStatisticsRepository.GetDailyRevenueStatistics(date).ToList());
                else return Ok(_salesStatisticsRepository.GetDailyRevenueStatistics().ToList());
            }
            catch (TimeoutException timeoutEx)
            {
                return StatusCode(408, "Could not connect to database");
            }
        }

        /// <summary>
        /// Returns the total revenue by product from all sales, sorted ascendingly by article number.
        /// The maximum number of elements returned at once is 1000.
        /// </summary>
        /// <param name="articleNumber">Alphanumeric article number with a length in the range of 1 to 32</param>
        /// <param name="page">Page number containing "pageSize" elements to be returned. If <1 or omitted, will be set to 1</param>
        /// <param name="pageSize">Number of elements per page. If >1000 or omitted, 1000 will be used instead</param>
        [HttpGet("articles/revenue")]
        public IActionResult TotalArticleRevenue(string articleNumber, int page, int pageSize)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(articleNumber) && Regex.IsMatch(articleNumber, "^([A-Za-z0-9]){1,32}$"))
                    return Ok(_salesStatisticsRepository.GetRevenueByArticleStatistics(articleNumber).ToList());
                else
                {
                    if (pageSize > 1000) pageSize = 1000;

                    if (page < 1) page = 1;

                    var skip = (page - 1) * pageSize;

                    return Ok(_salesStatisticsRepository.GetRevenueByArticleStatistics(skip, pageSize).ToList());
                }

                return BadRequest("Please either provide an article number or use pagination! Returning all entries without filtering is not supported!");
            }
            catch (TimeoutException timeoutEx)
            {
                return StatusCode(408, "Could not connect to database");
            }
        }
    }
}
