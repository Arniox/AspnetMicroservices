using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        //IDistributed Cache for connecting to the database
        private readonly IDistributedCache _redisCache;

        //Constructor
        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        //GET
        public async Task<ShoppingCart> GetBasket(string username)
        {
            //Get basket and check if empty
            var basket = await _redisCache.GetStringAsync(username);
            if (string.IsNullOrEmpty(basket))
                return null;

            //Return Shopping Cart List from a JSON Deserialized string
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        //PUT
        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            //Set the string (whole shopping cart object serialized as a JSON string) with key of UserName
            //This both updates and creates at the same time if not existing
            await _redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));

            //Return
            return await GetBasket(basket.UserName);
        }

        //DELETE
        public async Task DeleteBasket(string username)
        {
            //Remove the value with key of username
            await _redisCache.RemoveAsync(username);
        }

    }
}
