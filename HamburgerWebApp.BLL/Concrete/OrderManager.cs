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

    public async Task<decimal> CalculateOrderTotal(Order order, string[] selectedExtra)
    {
        Menu menu = _menuRepository.GetById(order.MenuId).Result;

        OrderSize orderSize = _orderSizeRepository.GetById(order.OrderSizeId).Result;

        decimal totalPrice = menu.Price * orderSize.PriceMultiplier;

        // Extras'ı List<Extra> tipine çevir
        if (selectedExtra.Length != 0)
        {
            var extras = await _extraRepository.GetAll();

            var selectedExtras = extras.Where(e => selectedExtra.Contains(e.Id.ToString())).ToList();

            order.Extras = selectedExtras;

            foreach (var extr in selectedExtras)
            {
                totalPrice += extr.Price;
            }
        }
        totalPrice *= order.OrderPiece;

        return totalPrice;
    }

    public async Task<Order> Update(Order entity, string[] selectedExtra)
    {
        var temp = await base.GetById(entity.Id);
        await base.Delete(temp);
        entity.OrderPrice = await CalculateOrderTotal(entity, selectedExtra);
        entity.Id = default;
        return await base.Add(entity);
    }

    public async Task Add(Order entity, string[] selectedExtra)
    {
        entity.OrderPrice = await CalculateOrderTotal(entity, selectedExtra);
        await base.Add(entity);
    }
}