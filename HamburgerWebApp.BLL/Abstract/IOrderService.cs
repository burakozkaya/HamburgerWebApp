using HamburgerWebApp.BLL.ResponsePattern;
using HamburgerWebApp.Entity.Concrete;

namespace HamburgerWebApp.BLL.Abstract;

public interface IOrderService : IBaseService<Order>
{
    Task<Response<decimal>> CalculateOrderTotal(Order order, string[] selectedExtra);
    Task<Response<Order>> Update(Order entity, string[] selectedExtra);
    Task<Response> Add(Order entity, string[] selectedExtra);
}