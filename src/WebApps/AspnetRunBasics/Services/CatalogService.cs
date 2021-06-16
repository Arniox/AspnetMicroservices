using AspnetRunBasics.Extensions;
using AspnetRunBasics.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services
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
            //Get response
            var response = await _client.GetAsync("/Catalog");
            return await response.ReadAsJsonAsync<IEnumerable<CatalogModel>>(); //Return
        }

        public async Task<CatalogModel> GetCatalog(string id)
        {
            //Get response
            var response = await _client.GetAsync($"/Catalog/{id}");
            return await response.ReadAsJsonAsync<CatalogModel>(); //Return
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
        {
            //Get response
            var response = await _client.GetAsync($"/Catalog/GetProductByCategory/{category}");
            return await response.ReadAsJsonAsync<List<CatalogModel>>(); //Return
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalogByName(string name)
        {
            //Get response
            var response = await _client.GetAsync($"/Catalog/GetProductByName/{name}");
            return await response.ReadAsJsonAsync<List<CatalogModel>>(); //Return
        }

        public async Task<CatalogModel> CreateCatalog(CatalogModel model)
        {
            //Send Request
            var response = await _client.PostAsJson($"/Catalog", model);
            if (response.IsSuccessStatusCode)
                //Return
                return await response.ReadAsJsonAsync<CatalogModel>();
            else
            {
                throw new Exception("Something went wrong when calling an API.");
            }
        }

    }
}
