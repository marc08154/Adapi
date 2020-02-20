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
        public IEnumerable<DailySalesStatistic> DailySales(DateTime date)
        {
            if (date != DateTime.MinValue)
                return _salesStatisticsRepository.GetDailySalesStatistics(date).ToList();
            else return _salesStatisticsRepository.GetDailySalesStatistics();
        }

        /// <summary>
        /// Returns the total revenue for all products on the given date
        /// </summary>
        /// <param name="date">Desired date for statistics report in the format yyyy-MM-dd. If omitted, all available entries are returned</param>
        [HttpGet("date/revenue")]
        public IEnumerable<DailyRevenueStatistic> DailyRevenue(DateTime date)
        {
            if (date != DateTime.MinValue)
                return _salesStatisticsRepository.GetDailyRevenueStatistics(date).ToList();
            else return _salesStatisticsRepository.GetDailyRevenueStatistics().ToList();
        }

        /// <summary>
        /// Returns the total revenue by product from all sales
        /// </summary>
        /// <param name="articleNumber">Alphanumeric article number with a length in the range of 1 to 32. If omitted, all available entries are returned</param>
        [HttpGet("article/revenue")]
        public IEnumerable<RevenueByArticleStatistic> DailyRevenue(string articleNumber)
        {
            if (!string.IsNullOrWhiteSpace(articleNumber) && Regex.IsMatch(articleNumber, "^([A-Za-z0-9]){1,32}$"))
                return _salesStatisticsRepository.GetRevenueByArticleStatistics(articleNumber).ToList();
            else return _salesStatisticsRepository.GetRevenueByArticleStatistics().ToList();
        }
    }
}
