﻿using Adapi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Adapi.Domain
{
    public class SalesStatisticsRepository
    {
        private readonly IMongoDatabase _adapiDb;

        public SalesStatisticsRepository(IMongoClient mongoClient)
        { 
            _adapiDb = mongoClient.GetDatabase("AdapiDB");
        }

        private IFindFluent<T, T> GetStatistic<T>(string collectionName, FilterDefinition<T> filter)
        {
            return _adapiDb
                .GetCollection<T>(collectionName)
                .Find(filter);
        }

        #region Daily Sales Statistics
        public IEnumerable<DailySalesStatistic> GetDailySalesStatistics(DateTime date)
        {
            return GetStatistic<DailySalesStatistic>(
                    "DailySalesStatistics",
                    Builders<DailySalesStatistic>.Filter.Eq(stat => stat.Date, date)
                )   
                .ToEnumerable();
        }

        public IEnumerable<DailySalesStatistic> GetDailySalesStatistics()
        {
            return GetStatistic<DailySalesStatistic>(
                    "DailySalesStatistics",
                    Builders<DailySalesStatistic>.Filter.Empty
                )
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
            return GetStatistic<DailyRevenueStatistic>(
                    "DailyRevenueStatistics",
                    Builders<DailyRevenueStatistic>.Filter.Eq(stat => stat.Date, date)
                )
                .ToEnumerable();
        }

        public IEnumerable<DailyRevenueStatistic> GetDailyRevenueStatistics()
        {
            return GetStatistic<DailyRevenueStatistic>(
                    "DailyRevenueStatistics",
                    Builders<DailyRevenueStatistic>.Filter.Empty
                )
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
            return GetStatistic<RevenueByArticleStatistic>(
                    "RevenueByArticleStatistics",
                    Builders<RevenueByArticleStatistic>.Filter.Eq(stat => stat.ArticleNumber, articleNumber)
                )
                .ToEnumerable();
        }

        public IEnumerable<RevenueByArticleStatistic> GetRevenueByArticleStatistics(int skip, int pageSize)
        {
            return GetStatistic<RevenueByArticleStatistic>(
                    "RevenueByArticleStatistics",
                    Builders<RevenueByArticleStatistic>.Filter.Empty
                )
                .Sort(Builders<RevenueByArticleStatistic>.Sort.Ascending(document => document.ArticleNumber))
                .Skip(skip)
                .Limit(pageSize)
                .ToEnumerable();
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
