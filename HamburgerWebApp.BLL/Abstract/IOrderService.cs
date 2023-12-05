using HamburgerWebApp.Entity.Concrete;

namespace HamburgerWebApp.BLL.Abstract;

public interface IOrderService : IBaseService<Order>
{
    public decimal CalculateOrderTotal(Order order);
}