using HamburgerWebApp.DAL.Abstract;
using HamburgerWebApp.DAL.Context;
using HamburgerWebApp.Entity.Concrete;

namespace HamburgerWebApp.DAL.Concrete;

public class ExtraRepository : BaseRepository<Extra>, IExtraRepository
{
    public ExtraRepository(AppDbContext context) : base(context)
    {
    }
}