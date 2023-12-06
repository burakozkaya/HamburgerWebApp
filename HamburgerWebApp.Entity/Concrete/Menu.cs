using System.ComponentModel.DataAnnotations;

namespace HamburgerWebApp.Entity.Concrete;

public class Menu : BaseEntity
{
    public string Name { get; set; }
    [Range(1, 10000, ErrorMessage = "Order piece must be between 1 and 10000.")]

    public int Price { get; set; }
}