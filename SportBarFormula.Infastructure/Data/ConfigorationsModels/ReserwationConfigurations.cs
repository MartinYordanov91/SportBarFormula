using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportBarFormula.Infastructure.Data.Models;

namespace SportBarFormula.Infastructure.Data.ConfigorationsModels;



public class ReserwationConfigurations : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {

        builder.HasOne(r => r.User)
               .WithMany()
               .HasForeignKey(r => r.UserId)
               .OnDelete(DeleteBehavior.Restrict);

        
         builder.HasOne<Table>()
                .WithMany()
                .HasForeignKey(r => r.TableId)
                .OnDelete(DeleteBehavior.Restrict);
    }
}

