using Ordering.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ordering.Application.Contracts.Persistence
{
    public interface IOrderRepository : IAsyncRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
        Task<IEnumerable<Order>> GetOrdersByFirstName(string firstName);
        Task<IEnumerable<Order>> GetOrdersByLastName(string lastName);
        Task<IEnumerable<Order>> GetOrdersByEmail(string email);
        Task<IEnumerable<Order>> GetOrdersByCountry(string country);
    }
}
