using AspnetRunBasics.Extensions;
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
        public async Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName)
        {
            //Get Response
            var response = await _client.GetAsync($"/Order/{userName}");
            //Return
            return await response.ReadAsJsonAsync<List<OrderResponseModel>>();
        }
        public async Task<IEnumerable<OrderResponseModel>> GetOrdersByFirstName(string firstName)
        {
            //Get Response
            var response = await _client.GetAsync($"/Order/ByFirstName/{firstName}");
            //Return
            return await response.ReadAsJsonAsync<List<OrderResponseModel>>();
        }

        public async Task<IEnumerable<OrderResponseModel>> GetOrdersByLastName(string lastName)
        {
            //Get Response
            var response = await _client.GetAsync($"/Order/ByLastName/{lastName}");
            //Return
            return await response.ReadAsJsonAsync<List<OrderResponseModel>>();
        }
        public async Task<IEnumerable<OrderResponseModel>> GetOrdersByEmail(string email)
        {
            //Get Response
            var response = await _client.GetAsync($"/Order/ByEmail/{email}");
            //Return
            return await response.ReadAsJsonAsync<List<OrderResponseModel>>();
        }

        public async Task<IEnumerable<OrderResponseModel>> GetOrdersByCountry(string country)
        {
            //Get Response
            var response = await _client.GetAsync($"/Order/ByCountry/{country}");
            //Return
            return await response.ReadAsJsonAsync<List<OrderResponseModel>>();
        }
    }
}
