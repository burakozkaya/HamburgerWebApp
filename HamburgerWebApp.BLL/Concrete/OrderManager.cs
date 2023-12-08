using HamburgerWebApp.BLL.Abstract;
using HamburgerWebApp.BLL.ResponsePattern;
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

    public async Task<Response<decimal>> CalculateOrderTotal(Order order, string[] selectedExtra)
    {
        try
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

            return Response<decimal>.Success(totalPrice, "Total Price Calculated");
        }
        catch (Exception e)
        {
            return Response<decimal>.Failure("Total Price Calculation Failed");
        }
    }

    public async Task<Response<Order>> Update(Order entity, string[] selectedExtra)
    {
        try
        {
            var temp = await base.GetById(entity.Id);
            await base.Delete(temp.Data);
            entity.OrderPrice = CalculateOrderTotal(entity, selectedExtra).Result.Data;
            entity.Id = default;
            await base.Add(entity);
            return Response<Order>.Success(entity, "Order Updated");
        }
        catch (Exception e)
        {
            return Response<Order>.Failure("Order Update Failed");
        }
    }

    public async Task<Response> Add(Order entity, string[] selectedExtra)
    {
        try
        {
            entity.OrderPrice = CalculateOrderTotal(entity, selectedExtra).Result.Data;
            await base.Add(entity);
            return Response.Success("Add Success");
        }
        catch (Exception e)
        {
            return Response.Failure("Add Failed");
        }
    }
}