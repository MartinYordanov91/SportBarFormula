using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportBarFormula.Infastructure.Data.Models;
using System.Reflection.Emit;
using System.Reflection;

namespace SportBarFormula.Infrastructure.Data
{
    public class SportBarFormulaDbContext : IdentityDbContext
    {
        public SportBarFormulaDbContext(DbContextOptions<SportBarFormulaDbContext> options)
            : base(options)
        {
        }

        public virtual required DbSet<MenuItem> MenuItems { get; set; }
        public virtual required DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
