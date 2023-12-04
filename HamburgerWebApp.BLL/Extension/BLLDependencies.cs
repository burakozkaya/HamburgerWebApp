using HamburgerWebApp.BLL.Abstract;
using HamburgerWebApp.BLL.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace HamburgerWebApp.BLL.Extension;

public static class BLLDependencies
{
    //dependency injection for bll
    public static void AddBLLDependency(this IServiceCollection services)
    {
        services.AddScoped<IExtraService, ExtraManager>();
        services.AddScoped<IOrderSizeService, OrderSizeManager>();
        services.AddScoped<IMenuService, MenuManager>();
        services.AddScoped<IOrderService, OrderManager>();
    }
}