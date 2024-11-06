using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportBarFormula.Infastructure.Data.Models;
using System.Reflection;

namespace SportBarFormula.Infrastructure.Data
{
    public class SportBarFormulaDbContext : IdentityDbContext
    {
        public SportBarFormulaDbContext(DbContextOptions<SportBarFormulaDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MenuItem> MenuItems { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Table> Tables { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
