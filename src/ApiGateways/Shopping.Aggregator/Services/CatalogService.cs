using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    public class CatalogService : ICatalogService
    {
        //Objects
        private readonly HttpClient _client;

        //Constructor
        public CatalogService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalog()
        {
            //Get http response
            var response = await _client.GetAsync("/api/v1/Catalog");
            return await response.ReadAsJsonAsync<IEnumerable<CatalogModel>>(); //Return
        }

        public async Task<CatalogModel> GetCatalog(string id)
        {
            //Get http response
            var response = await _client.GetAsync($"/api/v1/Catalog/{id}");
            return await response.ReadAsJsonAsync<CatalogModel>(); //Return
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
        {
            //Get http response
            var response = await _client.GetAsync($"/api/v1/Catalog/GetProductByCategory/{category}");
            return await response.ReadAsJsonAsync<IEnumerable<CatalogModel>>(); //Return
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalogByName(string name)
        {
            //Get http response
            var response = await _client.GetAsync($"/api/v1/Catalog/GetProductByName/{name}");
            return await response.ReadAsJsonAsync<IEnumerable<CatalogModel>>(); //Return
        }
    }
}
