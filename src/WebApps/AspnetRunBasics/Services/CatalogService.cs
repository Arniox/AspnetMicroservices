using AspnetRunBasics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<CatalogModel> CreateCatalog(CatalogModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalog()
        {
            throw new NotImplementedException();
        }

        public async Task<CatalogModel> GetCatalog(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
        {
            throw new NotImplementedException();
        }
    }
}
