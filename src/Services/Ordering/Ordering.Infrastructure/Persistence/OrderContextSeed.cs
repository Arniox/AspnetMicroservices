using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        //Seed
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed>)
        {
            //If any any record already
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                //Save
                await orderContext.SaveChangesAsync();
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
