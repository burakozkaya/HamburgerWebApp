namespace HamburgerWebApp.Entity.Concrete;

public class OrderSize : BaseEntity
{
    public int Id { get; set; }
    public string Size { get; set; }
    public decimal PriceMultiplier { get; set; }

}