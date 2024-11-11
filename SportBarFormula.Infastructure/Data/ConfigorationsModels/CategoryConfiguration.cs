using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Data.SeedData;

namespace SportBarFormula.Infrastructure.Data.ConfigorationsModels;

public class CategoryConfigurations : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.CategoryId);

        builder.HasMany(c => c.MenuItems)
               .WithOne(mi => mi.Category)
               .HasForeignKey(mi => mi.CategoryId)
               .OnDelete(DeleteBehavior.Restrict);

        builder
          .HasData(CategorySeeds.GenerateCategorys());
    }
}
