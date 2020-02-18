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
        private readonly IMongoClient _mongoClient;
        private readonly SalesStatisticsRepository _salesStatisticsRepository;

        public ReportsController(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;

            _salesStatisticsRepository = new SalesStatisticsRepository(mongoClient);
        }

        // Number of sold articles per day
        [HttpGet("date/sales")]
        public IEnumerable<DailySalesStatistic> DailySales(DateTime date)
        {
            if (date != DateTime.MinValue)
                return _salesStatisticsRepository.GetDailySalesStatistics(date).ToList();
            else return _salesStatisticsRepository.GetDailySalesStatistics();
        }

        // Total revenue per day
        [HttpGet("date/revenue")]
        public IEnumerable<DailyRevenueStatistic> DailyRevenue(DateTime date)
        {
            if (date != DateTime.MinValue)
                return _salesStatisticsRepository.GetDailyRevenueStatistics(date).ToList();
            else return _salesStatisticsRepository.GetDailyRevenueStatistics().ToList();
        }

        // Statistics: Revenue per article
        [HttpGet("article/revenue")]
        public IEnumerable<RevenueByArticleStatistic> DailyRevenue(string articleNumber)
        {
            if (Regex.IsMatch(articleNumber, "^([A-Za-z0-9]){1,32}$"))
                return _salesStatisticsRepository.GetRevenueByArticleStatistics(articleNumber).ToList();
            else return _salesStatisticsRepository.GetRevenueByArticleStatistics().ToList();
        }

        
    }
}
