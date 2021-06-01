using Dapper;
using Discount.API.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Threading.Tasks;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        //Config
        private readonly IConfiguration _configuration;

        //Constructor
        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            //Create connection with NpgSQL
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            //Grab Coupon from PostgreSQL DB with Dapper and NpgSQL
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ("SELECT * FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });

            //Return default coupon information
            if(coupon == null)
                return new Coupon
                    { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };

            //Return coupon
            return coupon;
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            //Create connection with NpgSQL
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            //Get number of affected records when creating new Coupon with Dapper and NpgSQL connection
            var affected = await connection.ExecuteAsync
                ("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
                    new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

            //If could not create (number of affected records equals 0). Otherwise return true
            if (affected == 0) return false;
            return true;
        }
        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            //Create connection with NpgSQL
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            //Get number of affected records when updating old Coupon with Dapper and NpgSQL connection
            var affected = await connection.ExecuteAsync
                ("UPDATE Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
                    new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id });

            //If could not update (number of affected records equals 0). Otherwise return true
            if (affected == 0) return false;
            return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            //Create connection with NpgSQL
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            //Get number of affected records when deleting old Coupon with Dapper and NpgSQL connection
            var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductName = @ProductName",
                new { ProductName = productName });

            //If coould not delete (number of affected records equals 0). Otherwise return true
            if (affected == 0) return false;
            return true;
        }
    }
}
