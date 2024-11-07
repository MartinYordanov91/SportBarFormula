using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportBarFormula.Infastructure.Data.Models;
using SportBarFormula.Infastructure.Data.SeedData;

namespace SportBarFormula.Infastructure.Data.ConfigorationsModels;

public class MenuItemComfigurations : IEntityTypeConfiguration<MenuItem>
{
    public void Configure(EntityTypeBuilder<MenuItem> builder)
    {
        builder.HasKey(mi => mi.MenuItemId);

        builder
            .HasOne(mi => mi.Category)
            .WithMany(c => c.MenuItems)
            .HasForeignKey(mi => mi.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(mi => mi.OrderItems)
            .WithOne(oi => oi.MenuItem)
            .HasForeignKey(oi => oi.MenuItemId)
            .OnDelete(DeleteBehavior.Restrict);

        //builder
        //   .HasData(MenuItemsSeeds.GenerateMenuItems());
    }

  
}
