using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Adapi.Models
{
    public class Sale
    {
        public Sale(string articleNumber, decimal salesPrice)
        {
            ArticleNumber = articleNumber;
            SalesPrice = salesPrice;

            SaleDate = DateTime.UtcNow.Date;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string ArticleNumber { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal SalesPrice { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(DateOnly = true)]
        public DateTime SaleDate { get; set; }
    }
}