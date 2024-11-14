using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportBarFormula.Core.Services.Contracts;
using SportBarFormula.Infrastructure.Data;
using SportBarFormula.Core.Services;
using SportBarFormula.Infrastructure.Repositorys.Contracts;
using SportBarFormula.Infrastructure.Repositorys;
using SportBarFormula.Infrastructure.Data.Models;

namespace Microsoft.Extensions.DependencyInjection;
public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IMenuItemService , MenuItemService>();
        services.AddScoped<ICategoryService , CategoryService>();

        services.AddScoped<IRepository<MenuItem>, MenuItemRepository>();

        return services;
    }

    public static IServiceCollection AddApplicationDbcContext(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection");
        services.AddDbContext<SportBarFormulaDbContext>(options =>
             options.UseSqlServer(connectionString));

        services.AddDatabaseDeveloperPageExceptionFilter();


        return services;
    }

    public static IServiceCollection AddApplicationIdentity(this IServiceCollection services, IConfiguration config)
    {
        services.AddDefaultIdentity<IdentityUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
        })
        .AddEntityFrameworkStores<SportBarFormulaDbContext>();

        return services;
    }
}
