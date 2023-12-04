using HamburgerWebApp.BLL.Abstract;
using HamburgerWebApp.DAL.Abstract;
using HamburgerWebApp.Entity.Concrete;

namespace HamburgerWebApp.BLL.Concrete;

public class ExtraManager : BaseManager<Extra>, IExtraService
{
    public ExtraManager(IBaseRepository<Extra> baseRepository) : base(baseRepository)
    {
    }
}