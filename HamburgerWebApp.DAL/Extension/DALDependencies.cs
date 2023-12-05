using HamburgerWebApp.DAL.Abstract;
using HamburgerWebApp.DAL.Concrete;
using HamburgerWebApp.DAL.Context;
using HamburgerWebApp.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HamburgerWebApp.DAL.Extension;

public static class DALDependencies
{
    public static void AddDALDependency(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString));
        services.AddScoped<IExtraRepository, ExtraRepository>();
        services.AddScoped<IOrderSizeRepository, OrderSizeRepository>();
        services.AddScoped<IMenuRepository, MenuRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        services.AddScoped<IBaseRepository<Extra>, BaseRepository<Extra>>();
        services.AddScoped<IBaseRepository<OrderSize>, BaseRepository<OrderSize>>();
        services.AddScoped<IBaseRepository<Menu>, BaseRepository<Menu>>();
        services.AddScoped<IBaseRepository<Order>, BaseRepository<Order>>();
    }
}
