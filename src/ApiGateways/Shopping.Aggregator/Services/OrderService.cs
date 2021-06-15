using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
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
        public async Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName)
        {
             //Get http response
             var response = await _client.GetAsync($"/api/v1/Order/{userName}");
             return await response.ReadAsJsonAsync<IEnumerable<OrderResponseModel>>();//Return
        }
        public async Task<IEnumerable<OrderResponseModel>> GetOrdersByFirstName(string firstName)
        {
             //Get http response
             var response = await _client.GetAsync($"/api/v1/Order/ByFirstName/{firstName}");
             return await response.ReadAsJsonAsync<IEnumerable<OrderResponseModel>>();//Return
        }

        public async Task<IEnumerable<OrderResponseModel>> GetOrdersByLastName(string lastName)
        {
             //Get http response
             var response = await _client.GetAsync($"/api/v1/Order/ByLastName/{lastName}");
             return await response.ReadAsJsonAsync<IEnumerable<OrderResponseModel>>();//Return
        }
        public async Task<IEnumerable<OrderResponseModel>> GetOrdersByEmail(string email)
        {
             //Get http response
             var response = await _client.GetAsync($"/api/v1/Order/ByEmail/{email}");
             return await response.ReadAsJsonAsync<IEnumerable<OrderResponseModel>>();//Return
        }

        public async Task<IEnumerable<OrderResponseModel>> GetOrdersByCountry(string country)
        {
            //Get http response
            var response = await _client.GetAsync($"/api/v1/Order/ByCountry/{country}");
            return await response.ReadAsJsonAsync<IEnumerable<OrderResponseModel>>();//Return
        }
    }
}
