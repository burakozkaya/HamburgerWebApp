using Microsoft.AspNetCore.Identity;

namespace HamburgerWebApp.Entity.Concrete;

public class AppUser : IdentityUser
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public IEnumerable<Order> Orders { get; set; }
}