using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Waiting
{
    public int WaitingId { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? CoffeeShopName { get; set; }
}
