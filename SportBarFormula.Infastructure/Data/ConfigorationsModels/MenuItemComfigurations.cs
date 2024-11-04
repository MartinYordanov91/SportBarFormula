using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportBarFormula.Infastructure.Data.Models;

namespace SportBarFormula.Infastructure.Data.ConfigorationsModels;

public class MenuItemComfigurations : IEntityTypeConfiguration<MenuItem>
{
    public void Configure(EntityTypeBuilder<MenuItem> builder)
    {
        //builder
        //   .HasData(GenerateMenuItems());
    }

    private IEnumerable<MenuItem> GenerateMenuItems()
    {
        IEnumerable<MenuItem> menuItems = new List<MenuItem>(){

             new MenuItem()
             {
                Name = "",
                Description = "",
                Price = 1,
                Category = new Category { Name = ""},
                ImageURL ="",
                IsAvailable = true,
                Ingredients ="",
                PreparationTime = 5,
                IsDeleted = false,
             },

        };


        return menuItems;
    }
}
