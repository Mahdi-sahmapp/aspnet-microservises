using Discount.Api.Entities;
using System;
using Npgsql;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Dapper;

namespace Discount.Api.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        #region Constructor
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion


        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var cocnnetion = new NpgsqlConnection(_configuration.GetValue<string>("DataBaseSetting:Connectionstring"));

            var resualt = await cocnnetion
                .ExecuteAsync("Insert Into Coupon (Amount, ProductName,Description) Values (@Amount,@ProductName,@Description)",
                new {ProductName = coupon.ProductName,Description=coupon.Description,Amount=coupon.Amount });
            if (resualt == 0) return false;
            return true;
        }

        public async Task<bool> DeleteDiscount(string productname)
        {
            using var cocnnetion = new NpgsqlConnection(_configuration.GetValue<string>("DataBaseSetting:Connectionstring"));

            var resualt = await cocnnetion
                .ExecuteAsync("delete from Coupon where ProductName = @ProductName",
                new { ProductName = productname });

            if (resualt == 0) return false;
            return true;
        }

        public async Task<Coupon> GetDiscount(string productname)
        {
            using var cocnnetion = new NpgsqlConnection(_configuration.GetValue<string>("DataBaseSetting:Connectionstring"));

            var coupon = await cocnnetion.QueryFirstOrDefaultAsync<Coupon>
                ("select * from Coupon where ProductName = @ProductName", new {ProductName = productname });

            if (coupon == null) return new Coupon { Amount = 0, ProductName = "No Discount",Description="Nothing...!" };

            return coupon;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var cocnnetion = new NpgsqlConnection(_configuration.GetValue<string>("DataBaseSetting:Connectionstring"));

            var resualt = await cocnnetion
                .ExecuteAsync("update Coupon set Amount=@Amount, ProductName=@ProductName,Description=@Description where id = @CoupId",
                new {ProductName=coupon.ProductName,Amount=coupon.Amount,Description = coupon.Description, CoupId=coupon.Id });

            if (resualt == 0) return false;
            return true;
        }
    }
}
