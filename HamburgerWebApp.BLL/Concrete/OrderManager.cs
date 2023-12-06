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

    public override async Task<Order> Update(Order entity)
    {
        await base.Delete(entity);

        var order = await base.GetById(entity.Id);
        order.OrderPiece = entity.OrderPiece;
        order.OrderSizeId = entity.OrderSizeId;
        order.MenuId = entity.MenuId;
        order.Extras = entity.Extras;
        order.OrderPrice = await CalculateOrderTotal(order, entity.Extras.Select(e => e.Id.ToString()).ToArray());
        return await base.Update(order);
    }
}