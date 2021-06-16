using AspnetRunBasics.Extensions;
using AspnetRunBasics.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services
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
            //Get Response
            var response = await _client.GetAsync($"/Basket/{userName}");
            //Return
            return await response.ReadAsJsonAsync<BasketModel>();
        }
        public async Task<BasketModel> UpdateBasket(BasketModel model)
        {
            //Get Response
            var response = await _client.PutAsJson($"/Basket", model);
            if (response.IsSuccessStatusCode)
                //Return
                return await response.ReadAsJsonAsync<BasketModel>();
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }

        public async Task CheckoutBasket(BasketCheckoutModel model)
        {
            //Get Response
            var response = await _client.PostAsJson($"Basket/Checkout", model);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }
    }
}
