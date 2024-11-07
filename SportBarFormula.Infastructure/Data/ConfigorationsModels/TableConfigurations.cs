using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportBarFormula.Infrastructure.Data.Models;

namespace SportBarFormula.Infrastructure.Data.ConfigorationsModels;

internal class TableConfigurations : IEntityTypeConfiguration<Table>
{

    public void Configure(EntityTypeBuilder<Table> builder)
    {
        builder.HasKey(t => t.TableId);
    }
}

