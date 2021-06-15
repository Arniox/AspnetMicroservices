using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    public class BasketService : IBasketService
    {
        //Objects
        private readonly HttpClient _client;

        //Constructor
        public BasketService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<BasketModel> GetBasket(string userName)
        {
            //Get http response
            var response = await _client.GetAsync($"/api/v1/Basket/{userName}");
            return await response.ReadAsJsonAsync<BasketModel>(); //Return
        }
    }
}
