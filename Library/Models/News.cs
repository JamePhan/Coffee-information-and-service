using System;
using System.Collections.Generic;

namespace Library.Models;

public partial class News
{
    public int NewsId { get; set; }

    public int UserId { get; set; }

    public string? Title { get; set; }

    public int ImageId { get; set; }
}
