﻿using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class Service
{
    public int ServiceId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? UserId { get; set; }

    public int? GroupImageId { get; set; }

    public virtual GroupImage? GroupImage { get; set; }

    public virtual User? User { get; set; }
}
