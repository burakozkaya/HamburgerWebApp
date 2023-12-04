using HamburgerWebApp.DAL.Abstract;
using HamburgerWebApp.DAL.Context;
using HamburgerWebApp.Entity.Concrete;

namespace HamburgerWebApp.DAL.Concrete;

public class OrderSizeRepository : BaseRepository<OrderSize>, IOrderSizeRepository
{
    public OrderSizeRepository(AppDbContext context) : base(context)
    {
    }
}