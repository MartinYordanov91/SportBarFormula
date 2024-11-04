using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SportBarFormula.Infrastructure.Data
{
    public class SportBarFormulaDbContext : IdentityDbContext
    {
        public SportBarFormulaDbContext(DbContextOptions<SportBarFormulaDbContext> options)
            : base(options)
        {
        }
    }
}
