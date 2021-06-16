using AspnetRunBasics.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services
{
    public interface ICatalogService
    {
        Task<CatalogModel> GetCatalog(string id);
        Task<IEnumerable<CatalogModel>> GetCatalog();
        Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category);
        Task<IEnumerable<CatalogModel>> GetCatalogByName(string name);
        Task<CatalogModel> CreateCatalog(CatalogModel model);
    }
}
