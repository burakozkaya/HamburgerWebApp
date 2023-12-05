using System.ComponentModel.DataAnnotations.Schema;

namespace HamburgerWebApp.Entity.Concrete;

public class Order : BaseEntity
{
    public int OrderSizeId { get; set; }
    public int OrderPiece { get; set; }
    public decimal OrderPrice { get; set; }
    public int MenuId { get; set; }
    public string AppUserId { get; set; }
    public IEnumerable<Extra> Extras { get; set; }

    //Nav Property
    public OrderSize OrderSize { get; set; }
    public Menu Menu { get; set; }

    public AppUser AppUser { get; set; }

  
}