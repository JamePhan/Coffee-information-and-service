using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Admin
{
    public int AdminId { get; set; }

    public int? AccountId { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public virtual Account? Account { get; set; }
}
