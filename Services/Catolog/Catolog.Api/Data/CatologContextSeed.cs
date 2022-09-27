

using Catolog.Api.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Catolog.Api.Data
{
    public class CatologContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productcollection)
        {
            bool existProduct = productcollection.Find(a => true).Any();

            if (!existProduct)
            {
                var u = GetSeedData();

                productcollection.InsertManyAsync(u);


                //string r = Convert.ToHexString(Encoding.Default.GetBytes("hsagdjusad"), 1, 23);
                //string r2 = Convert.ToHexString(Encoding.Default.GetBytes("hsagdjusad"), 5, 24);
                //string r4 = Convert.ToHexString(Encoding.Default.GetBytes("hsagdjusad"), 10, 24);

            }
        }

        private static IEnumerable<Product> GetSeedData()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = "546c776b3e23f5f2ebdd3b03",
                     Info = "first Product",
                     Name = "Car",
                      Price = 200.00M,
                      Category ="1"
                },
                new Product()
                {
                    Id ="602d2149e773f2a3990b47f9",
                     Info = "second Product",
                     Name = "bos",
                      Price = 123.00M,
                       Category ="1"
                },new Product()
                {
                    Id = "",
                     Info = "602d2149e773f2a3990b47fa",
                     Name = "bicycle",
                      Price = 20.00M,
                       Category ="2"
                }
            };
        }
    }
}
