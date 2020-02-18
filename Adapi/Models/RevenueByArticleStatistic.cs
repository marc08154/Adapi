using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adapi.Models
{
    public class RevenueByArticleStatistic
    {
        public RevenueByArticleStatistic(string articleNumber, decimal revenue)
        {
            ArticleNumber = articleNumber;
            Revenue = revenue;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string ArticleNumber { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Revenue { get; set; }
    }
}
