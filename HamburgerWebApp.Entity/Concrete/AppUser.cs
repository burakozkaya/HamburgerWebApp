using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace HamburgerWebApp.Entity.Concrete;

public class AppUser : IdentityUser
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    [ValidateNever]
    public IEnumerable<Order> Orders { get; set; }
}