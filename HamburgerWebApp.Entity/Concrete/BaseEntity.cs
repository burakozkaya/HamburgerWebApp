using HamburgerWebApp.Entity.Abstract;

namespace HamburgerWebApp.Entity.Concrete;

public class BaseEntity : IBaseEntity
{
    public int Id { get; set; }
}