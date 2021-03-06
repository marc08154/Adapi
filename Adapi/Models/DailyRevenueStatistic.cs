﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adapi.Models
{
    public class DailyRevenueStatistic
    {
        public DailyRevenueStatistic(DateTime date, decimal revenue)
        {
            Date = date;
            Revenue = revenue;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(DateOnly = true)]
        public DateTime Date { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Revenue { get; set; }       
    }
}
