using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adapi.Models
{
    public class DailySalesStatistic
    {
        public DailySalesStatistic(DateTime date, int numberOfSales)
        {
            Date = date;
            NumberOfSales = numberOfSales;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(DateOnly = true)]
        public DateTime Date { get; set; }

        public int NumberOfSales { get; set; }       
    }
}
