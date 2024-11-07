using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportBarFormula.Infrastructure.Data.Models;

namespace SportBarFormula.Infrastructure.Data.ConfigorationsModels;

public class PaymentConfigurations : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(p => p.PaymentId);

        //builder.HasOne(p => p.Order)
        //       .WithMany()
        //       .HasForeignKey(p => p.OrderId)
        //       .OnDelete(DeleteBehavior.Restrict);

        //builder.HasOne(p => p.User)
        //       .WithMany()
        //       .HasForeignKey(p => p.UserId)
        //       .OnDelete(DeleteBehavior.Restrict);
    }
}
