using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        //Constructor - Call super
        public OrderRepository(OrderContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            //Get orderedlist
            var orderList = await _dbContext.Orders
                                    .Where(o => o.UserName == userName)
                                    .ToListAsync();
            return orderList; //Return
        }

        public async Task<IEnumerable<Order>> GetOrdersByFirstName(string firstName)
        {
            //Get orderedList
            var orderList = await _dbContext.Orders
                                    .Where(o => o.FirstName == firstName)
                                    .ToListAsync();
            return orderList; //Return
        }

        public async Task<IEnumerable<Order>> GetOrdersByLastName(string lastName)
        {
            //Get orderedList
            var orderList = await _dbContext.Orders
                                    .Where(o => o.LastName == lastName)
                                    .ToListAsync();
            return orderList; //Return
        }

        public async Task<IEnumerable<Order>> GetOrdersByEmail(string email)
        {
            //Get orderedList
            var orderList = await _dbContext.Orders
                                    .Where(o => o.EmailAddress == email)
                                    .ToListAsync();
            return orderList; //Return
        }

        public async Task<IEnumerable<Order>> GetOrdersByCountry(string country)
        {
            //Get orderedList
            var orderList = await _dbContext.Orders
                                    .Where(o => o.Country == country)
                                    .ToListAsync();
            return orderList; //Return
        }
    }
}
