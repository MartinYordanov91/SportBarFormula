using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportBarFormula.Core.Services;
using SportBarFormula.Core.Services.Contracts;
using SportBarFormula.Core.Services.Logging;
using SportBarFormula.Infrastructure.Data;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Repositorys;
using SportBarFormula.Infrastructure.Repositorys.Contracts;

namespace Microsoft.Extensions.DependencyInjection;
public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IMenuItemService, MenuItemService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IReservationService, ReservationService>();
        services.AddScoped<IOrderService, OrderService>();

        services.AddScoped<IRepository<MenuItem>, MenuItemRepository>();
        services.AddScoped<IRepository<Category>, CategoryRepository>();
        services.AddScoped<IRepository<Reservation>, ReservationRepository>();
        services.AddScoped<IRepository<Order>, OrderRepository>();
        services.AddScoped<IRepository<OrderItem>, OrderItemRepository>();

        services.AddScoped<IModelStateLoggerService, ModelStateLoggerServiceILogger>();

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
            options.SignIn.RequireConfirmedAccount = false;
        })
        .AddEntityFrameworkStores<SportBarFormulaDbContext>();

        return services;
    }
}
