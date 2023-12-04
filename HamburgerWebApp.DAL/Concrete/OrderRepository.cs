using HamburgerWebApp.DAL.Abstract;
using HamburgerWebApp.DAL.Context;
using HamburgerWebApp.Entity.Concrete;

namespace HamburgerWebApp.DAL.Concrete;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context)
    {
    }
}