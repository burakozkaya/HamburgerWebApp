﻿namespace HamburgerWebApp.Entity.Concrete;

public class Extra : BaseEntity
{
    public string Name { get; set; }
    public int Price { get; set; }
    public List<Order> Orders { get; set; }
}