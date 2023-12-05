using HamburgerWebApp.BLL.Abstract;
using HamburgerWebApp.DAL.Abstract;
using HamburgerWebApp.Entity.Concrete;

namespace HamburgerWebApp.BLL.Concrete;

public class OrderManager : BaseManager<Order>, IOrderService
{
    private readonly IMenuRepository _menuRepository;
    private readonly IOrderSizeRepository _orderSizeRepository;
    private readonly IExtraRepository _extraRepository;
    public OrderManager(IBaseRepository<Order> baseRepository, IExtraRepository extraRepository, IMenuRepository menuRepository, IOrderSizeRepository orderSizeRepository)
           : base(baseRepository)
    {
        _menuRepository = menuRepository;
        _orderSizeRepository = orderSizeRepository;
        _extraRepository = extraRepository;
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

        // Extras'ı List<Extra> tipine çevir
        var extrasList = order.Extras.ToList();
        if(extrasList.Any())
        {
            foreach (var extr in extrasList)
            {
                totalPrice += extr.Price;
            }
        }

        totalPrice *= order.OrderPiece;
        return totalPrice;
    }

}