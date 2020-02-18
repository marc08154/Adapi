using System;
using System.Text.RegularExpressions;
using MongoDB.Driver;
using Adapi.Models;

namespace Adapi.Domain
{
    public class SalesService
    {
        private readonly SalesRepository _salesRepository;
        private readonly SalesStatisticsRepository _salesStatisticsRepository;

        public SalesService(IMongoClient mongoClient)
        {
            _salesRepository = new SalesRepository(mongoClient);
            _salesStatisticsRepository = new SalesStatisticsRepository(mongoClient);
        }

        public void Insert(string articleNumber, decimal salesPrice)
        {
            if (!Regex.IsMatch(articleNumber, "^([A-Za-z0-9]){1,32}$"))
                throw new FormatException("The article number must only consist of alphanumerical characters and contain no more than 32 characters!");

            var sale = new Sale(articleNumber, salesPrice);

            _salesRepository.Insert(sale);

            _salesStatisticsRepository.UpsertDailySalesStatistics(sale.SaleDate, 1);
            _salesStatisticsRepository.UpsertDailyRevenueStatistics(sale.SaleDate, sale.SalesPrice);
            _salesStatisticsRepository.UpsertRevenueByArticleStatistics(sale.ArticleNumber, sale.SalesPrice);
        }
    }
}
