using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public int? AccountId { get; set; }

    public string? Avatar { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ICollection<Following> Followings { get; set; } = new List<Following>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual ICollection<Waiting> Waitings { get; set; } = new List<Waiting>();
}
