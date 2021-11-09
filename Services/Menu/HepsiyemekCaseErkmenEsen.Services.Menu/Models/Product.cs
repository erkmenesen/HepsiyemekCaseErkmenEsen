using HepsiyemekCaseErkmenEsen.Services.Menu.Dtos;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HepsiyemekCaseErkmenEsen.Services.Menu.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal price { get; set; }
        public string currency { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string categoryId { get; set; }
        [BsonIgnore]
        public Category Category { get; set; }

    }
}
