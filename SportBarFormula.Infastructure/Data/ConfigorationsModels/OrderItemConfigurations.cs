using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SportBarFormula.Infastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportBarFormula.Infastructure.Data.ConfigorationsModels
{
    public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.OrderItemId);

            builder.HasOne(oi => oi.Order)
                   .WithMany(o => o.OrderItems)
                   .HasForeignKey(oi => oi.OrderId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(oi => oi.MenuItem)
                   .WithMany(mi => mi.OrderItems)
                   .HasForeignKey(oi => oi.MenuItemId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
