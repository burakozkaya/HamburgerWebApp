using HamburgerWebApp.Entity.Concrete;

namespace HamburgerWebApp.BLL.Abstract;

public interface IOrderService : IBaseService<Order>
{
    public Task<decimal> CalculateOrderTotal(Order order, string[] selectedExtra);
}