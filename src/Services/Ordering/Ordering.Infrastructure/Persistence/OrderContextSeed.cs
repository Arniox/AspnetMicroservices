using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        //Seed
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            //If any any record already
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                //Save
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seeded database associated with context {DbContextName}", typeof(OrderContext).Name);
            }
        }

        //Get pre-data
        private static IEnumerable<Order> GetPreconfiguredOrders()
        {
            //Return new list of Orders
            return new List<Order>
            {
                new Order() { UserName = "Arniox", FirstName = "Nikkolas", LastName = "Diehl", EmailAddress = "nikkdiehl@gmail.com", AddressLine = "Henderson", Country = "New Zealand", TotalPrice = 350 }
            };
        }
    }
}
