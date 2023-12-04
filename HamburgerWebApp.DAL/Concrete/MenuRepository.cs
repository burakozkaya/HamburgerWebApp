using HamburgerWebApp.DAL.Abstract;
using HamburgerWebApp.DAL.Context;
using HamburgerWebApp.Entity.Concrete;

namespace HamburgerWebApp.DAL.Concrete;

public class MenuRepository : BaseRepository<Menu>, IMenuRepository
{
    public MenuRepository(AppDbContext context) : base(context)
    {
    }
}