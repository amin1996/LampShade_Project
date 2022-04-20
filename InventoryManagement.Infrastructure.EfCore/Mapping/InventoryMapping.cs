using InventoryManagement.Domain.Inventory.Agg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.EfCore.Mapping
{
    public class InventoryMapping : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable("InventoryOperations");
            builder.HasKey(x => x.Id);

            builder.OwnsMany(x => x.Operations, ModelBuilder =>
             {
                 ModelBuilder.ToTable("Operations");
                 ModelBuilder.HasKey(x => x.Id);
                 ModelBuilder.Property(x => x.Description).HasMaxLength(1000);
                 ModelBuilder.WithOwner(x => x.Inventory).HasForeignKey(x => x.InventoryId);
             });
        }
    }
}
