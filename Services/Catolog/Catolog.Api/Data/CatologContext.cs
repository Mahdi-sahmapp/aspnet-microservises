using Catolog.Api.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catolog.Api.Data
{
    

    public class CatologContext : ICatologContext
    {
        public CatologContext(IConfiguration configuration)
        {
            // conect to mongodb
            var client = new MongoClient(configuration.GetValue<string>("DataBaseSetting:ConectionString"));
            // conect to database
            var database = client.GetDatabase(configuration.GetValue<string>("DataBaseSetting:DatabaseName"));
            Products = database.GetCollection<Product>(configuration.GetValue<string>("DataBaseSetting:CollectionName"));
            CatologContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get; }
    }
}
