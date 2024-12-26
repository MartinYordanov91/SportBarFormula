using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Data.SeedData;

namespace SportBarFormula.Infrastructure.Data.ConfigorationsModels;

public class TableConfigurations : IEntityTypeConfiguration<Table>
{

    public void Configure(EntityTypeBuilder<Table> builder)
    {
        builder.HasKey(t => t.TableId);

        builder
            .HasData(TableSeeds.GenerateTables());
    }
}

