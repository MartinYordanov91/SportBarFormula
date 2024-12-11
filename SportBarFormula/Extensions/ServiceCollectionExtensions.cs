using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportBarFormula.Core.Services;
using SportBarFormula.Core.Services.Contracts;
using SportBarFormula.Core.Services.Logging;
using SportBarFormula.Infrastructure.Data;
using SportBarFormula.Infrastructure.Data.Models;
using SportBarFormula.Infrastructure.Repositorys;
using SportBarFormula.Infrastructure.Repositorys.Contracts;

namespace SportBarFormula.Extensions;


/// <summary>
/// Provides extension methods for IServiceCollection to add application-specific services, DbContext, and Identity.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds application-specific services to the service collection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <returns>The IServiceCollection with added services.</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IMenuItemService, MenuItemService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IReservationService, ReservationService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IRepository<MenuItem>, MenuItemRepository>();
        services.AddScoped<IRepository<Category>, CategoryRepository>();
        services.AddScoped<IRepository<Reservation>, ReservationRepository>();
        services.AddScoped<IRepository<Order>, OrderRepository>();
        services.AddScoped<IRepository<OrderItem>, OrderItemRepository>();
        services.AddScoped<IRepository<Table>, TableRepository>();

        services.AddScoped<IModelStateLoggerService, ModelStateLoggerServiceILogger>();

        return services;
    }

    /// <summary>
    /// Configures and adds the application's DbContext to the service collection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the DbContext to.</param>
    /// <param name="config">The IConfiguration instance to retrieve connection strings.</param>
    /// <returns>The IServiceCollection with added DbContext.</returns>
    public static IServiceCollection AddApplicationDbContext(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection");
        services.AddDbContext<SportBarFormulaDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddDatabaseDeveloperPageExceptionFilter();

        return services;
    }

    /// <summary>
    /// Configures and adds Identity services to the service collection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add Identity services to.</param>
    /// <param name="config">The IConfiguration instance to retrieve authentication settings.</param>
    /// <returns>The IServiceCollection with added Identity services.</returns>
    public static IServiceCollection AddApplicationIdentity(this IServiceCollection services, IConfiguration config)
    {
        services.AddDefaultIdentity<IdentityUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<SportBarFormulaDbContext>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
            options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
        })
        .AddFacebook(options =>
        {
            options.AppId = config["Authentication:Facebook:AppId"]!;
            options.AppSecret = config["Authentication:Facebook:AppSecret"]!;
        });

        return services;
    }
}
