using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportBarFormula.Infastructure.Data.Models;

namespace SportBarFormula.Infastructure.Data.ConfigorationsModels;

internal class TableConfigurations : IEntityTypeConfiguration<Table>
{

    public void Configure(EntityTypeBuilder<Table> builder)
    {
        builder.HasKey(t => t.TableId);
    }
}

