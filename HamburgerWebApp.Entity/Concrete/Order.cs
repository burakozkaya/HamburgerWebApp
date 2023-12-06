using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace HamburgerWebApp.Entity.Concrete;

public class Order : BaseEntity
{
    public int OrderSizeId { get; set; }

    [Range(1, 20, ErrorMessage = "Order piece must be between 1 and 20.")]
    public int OrderPiece { get; set; }
    public decimal OrderPrice { get; set; }
    public int MenuId { get; set; }
    public string AppUserId { get; set; }
    [ValidateNever]

    //Nav Property
    public IEnumerable<Extra> Extras { get; set; }
    [ValidateNever]
    public OrderSize OrderSize { get; set; }
    [ValidateNever]
    public Menu Menu { get; set; }
    [ValidateNever]
    public AppUser AppUser { get; set; }


}