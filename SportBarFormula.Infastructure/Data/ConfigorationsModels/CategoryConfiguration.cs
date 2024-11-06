using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportBarFormula.Infastructure.Data.Models;

namespace SportBarFormula.Infastructure.Data.ConfigorationsModels
{
    public class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.CategoryId);

            builder.HasMany(c => c.MenuItems)
                   .WithOne(mi => mi.Category)
                   .HasForeignKey(mi => mi.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
