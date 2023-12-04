using HamburgerWebApp.BLL.Abstract;
using HamburgerWebApp.DAL.Abstract;
using HamburgerWebApp.Entity.Concrete;

namespace HamburgerWebApp.BLL.Concrete;

public class OrderManager : BaseManager<Order>, IOrderService
{
    public OrderManager(IBaseRepository<Order> baseRepository) : base(baseRepository)
    {
    }
}