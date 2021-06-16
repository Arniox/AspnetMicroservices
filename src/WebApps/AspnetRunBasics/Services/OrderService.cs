using AspnetRunBasics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services
{
    public class OrderService : IOrderService
    {
        //Objects
        private readonly HttpClient _client;

        //Constructor
        public OrderService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
