using HamburgerWebApp.Entity.Concrete;

namespace HamburgerWebApp.BLL.Abstract;

public interface IOrderService : IBaseService<Order>
{
    Task<decimal> CalculateOrderTotal(Order order, string[] selectedExtra);
    Task<Order> Update(Order entity, string[] selectedExtra);
    Task Add(Order entity, string[] selectedExtra);
}