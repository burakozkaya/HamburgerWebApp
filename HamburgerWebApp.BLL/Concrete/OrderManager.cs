using HamburgerWebApp.BLL.Abstract;
using HamburgerWebApp.DAL.Abstract;
using HamburgerWebApp.Entity.Concrete;

namespace HamburgerWebApp.BLL.Concrete;

public class OrderManager : BaseManager<Order>, IOrderService
{
    private readonly IMenuRepository _menuRepository;
    private readonly IOrderSizeRepository _orderSizeRepository;
    public OrderManager(IBaseRepository<Order> baseRepository, IMenuRepository menuRepository, IOrderSizeRepository orderSizeRepository)
           : base(baseRepository)
    {
        _menuRepository = menuRepository;
        _orderSizeRepository = orderSizeRepository;
    }

    public decimal CalculateOrderTotal(Order order)
    {
        Menu menu = _menuRepository.GetById(order.MenuId).Result;
        OrderSize orderSize = _orderSizeRepository.GetById(order.OrderSizeId).Result;

        if (menu == null || orderSize == null)
        {
            throw new InvalidOperationException("Menu or order size not found.");
        }

        decimal totalPrice = menu.Price * orderSize.PriceMultiplier;

        if (order.Extras != null && order.Extras.Any())
        {
            totalPrice += order.Extras.Sum(extra => extra.Price);
        }

        return totalPrice;
    }

   
}