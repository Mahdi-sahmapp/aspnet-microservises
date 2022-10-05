using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContext:DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options): base(options)
        {
        }
        public DbSet<Order> orders { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<EntityBase>())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Entity.MdifiedDate = DateTime.Now;
                        entry.Entity.LastModifyBy = "Mahdi";
                        break;
                    case EntityState.Added:
                        entry.Entity.CreateDate = DateTime.Now;
                        entry.Entity.CreateBy = "Mahdi";
                        break;                  
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
