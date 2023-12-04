using HamburgerWebApp.BLL.Abstract;
using HamburgerWebApp.DAL.Abstract;
using HamburgerWebApp.Entity.Concrete;

namespace HamburgerWebApp.BLL.Concrete;

public class MenuManager : BaseManager<Menu>, IMenuService
{
    public MenuManager(IBaseRepository<Menu> baseRepository) : base(baseRepository)
    {
    }
}