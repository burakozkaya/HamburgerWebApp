using HamburgerWebApp.BLL.Abstract;
using HamburgerWebApp.DAL.Abstract;
using HamburgerWebApp.Entity.Concrete;

namespace HamburgerWebApp.BLL.Concrete;

public class OrderSizeManager : BaseManager<OrderSize>, IOrderSizeService
{
    public OrderSizeManager(IBaseRepository<OrderSize> baseRepository) : base(baseRepository)
    {
    }
}