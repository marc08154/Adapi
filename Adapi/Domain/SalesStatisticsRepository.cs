using Adapi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Adapi.Domain
{
    public class SalesStatisticsRepository
    {
        private readonly IMongoClient _mongoClient;

        private readonly IMongoDatabase _adapiDb;
        
        public SalesStatisticsRepository(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;

            _adapiDb = _mongoClient.GetDatabase("AdapiDB");
        }

        #region Daily Sales Statistics
        public IEnumerable<DailySalesStatistic> GetDailySalesStatistics(DateTime date)
        {
            return GetSalesStatistic(Builders<DailySalesStatistic>.Filter.Eq(stat => stat.Date, date));
        }

        public IEnumerable<DailySalesStatistic> GetDailySalesStatistics()
        {
            return GetSalesStatistic(Builders<DailySalesStatistic>.Filter.Empty);
        }

        private IEnumerable<DailySalesStatistic> GetSalesStatistic(FilterDefinition<DailySalesStatistic> filter)
        {
            return _adapiDb
                    .GetCollection<DailySalesStatistic>("DailySalesStatistics")
                    .Find(filter)
                    .ToEnumerable();
        }

        public void UpsertDailySalesStatistics(DateTime date, int salesChange)
        {
            var filter = Builders<DailySalesStatistic>.Filter.Eq(stat => stat.Date, date);
            var update = Builders<DailySalesStatistic>.Update.Inc(stat => stat.NumberOfSales, salesChange);
            var updateOptions = new UpdateOptions { IsUpsert = true };
            
            _adapiDb
                .GetCollection<DailySalesStatistic>("DailySalesStatistics")
                .UpdateOne(filter, update, updateOptions);
        }
        #endregion

        #region Daily Revenue Statistics
        public IEnumerable<DailyRevenueStatistic> GetDailyRevenueStatistics(DateTime date)
        {
            return GetRevenueStatistic(Builders<DailyRevenueStatistic>.Filter.Eq(stat => stat.Date, date));
        }

        public IEnumerable<DailyRevenueStatistic> GetDailyRevenueStatistics()
        {
            return GetRevenueStatistic(Builders<DailyRevenueStatistic>.Filter.Empty);
        }

        private IEnumerable<DailyRevenueStatistic> GetRevenueStatistic(FilterDefinition<DailyRevenueStatistic> filter)
        {
            return _adapiDb
                    .GetCollection<DailyRevenueStatistic>("DailyRevenueStatistics")
                    .Find(filter)
                    .ToEnumerable();
        }

        public void UpsertDailyRevenueStatistics(DateTime date, decimal revenueChange)
        {
            var filter = Builders<DailyRevenueStatistic>.Filter.Eq(stat => stat.Date, date);
            var update = Builders<DailyRevenueStatistic>.Update.Inc(stat => stat.Revenue, revenueChange);
            var updateOptions = new UpdateOptions { IsUpsert = true };

            _adapiDb
                .GetCollection<DailyRevenueStatistic>("DailyRevenueStatistics")
                .UpdateOne(filter, update, updateOptions);
        }
        #endregion

        #region Article Revenue Statistics
        public IEnumerable<RevenueByArticleStatistic> GetRevenueByArticleStatistics(string articleNumber)
        {
            return GetRevenueStatistic(Builders<RevenueByArticleStatistic>.Filter.Eq(stat => stat.ArticleNumber, articleNumber))
                    .ToEnumerable();
        }

        public IEnumerable<RevenueByArticleStatistic> GetRevenueByArticleStatistics(int skip, int pageSize)
        {
            return GetRevenueStatistic(Builders<RevenueByArticleStatistic>.Filter.Empty)
                    .Skip(skip)
                    .Limit(pageSize)
                    .ToEnumerable();
        }

        private IFindFluent<RevenueByArticleStatistic, RevenueByArticleStatistic> GetRevenueStatistic(FilterDefinition<RevenueByArticleStatistic> filter)
        {
            return _adapiDb
                    .GetCollection<RevenueByArticleStatistic>("RevenueByArticleStatistics")
                    .Find(filter)
                    .Sort(Builders<RevenueByArticleStatistic>.Sort.Ascending(x => x.ArticleNumber));
        }

        public void UpsertRevenueByArticleStatistics(string articleNumber, decimal revenueChange)
        {
            var filter = Builders<RevenueByArticleStatistic>.Filter.Eq(stat => stat.ArticleNumber, articleNumber);
            var update = Builders<RevenueByArticleStatistic>.Update.Inc(stat => stat.Revenue, revenueChange);
            var updateOptions = new UpdateOptions { IsUpsert = true };

            _adapiDb
                .GetCollection<RevenueByArticleStatistic>("RevenueByArticleStatistics")
                .UpdateOne(filter, update, updateOptions);
        }
        #endregion


    }
}
