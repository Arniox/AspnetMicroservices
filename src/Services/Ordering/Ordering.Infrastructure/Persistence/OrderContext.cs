using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    //Database context
    public class OrderContext : DbContext
    {
        //Constructor
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        { 
        }

        //Add sets
        public DbSet<Order> Orders { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //For each entity in the ChangeTracker (any items modifed or added)
            //Set entity CreatedDate / CreatedBy
            //Set entity LastModifiedDate / LastModifiedBy
            foreach(var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "Arniox";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "Arniox";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
