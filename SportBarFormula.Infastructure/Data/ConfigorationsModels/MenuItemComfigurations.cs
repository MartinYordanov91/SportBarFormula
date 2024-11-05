using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportBarFormula.Infastructure.Data.Models;
using SportBarFormula.Infastructure.Data.SeedData;

namespace SportBarFormula.Infastructure.Data.ConfigorationsModels;

public class MenuItemComfigurations : IEntityTypeConfiguration<MenuItem>
{
    public void Configure(EntityTypeBuilder<MenuItem> builder)
    {
        builder
           .HasData(MenuItemsSeeds.GenerateMenuItems());
    }

  
}
