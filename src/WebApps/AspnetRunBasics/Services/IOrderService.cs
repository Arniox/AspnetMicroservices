using AspnetRunBasics.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetRunBasics.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName);
        Task<IEnumerable<OrderResponseModel>> GetOrdersByFirstName(string firstName);
        Task<IEnumerable<OrderResponseModel>> GetOrdersByLastName(string lastName);
        Task<IEnumerable<OrderResponseModel>> GetOrdersByEmail(string email);
        Task<IEnumerable<OrderResponseModel>> GetOrdersByCountry(string country);
    }
}
