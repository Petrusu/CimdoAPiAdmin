using System;
using System.Collections.Generic;

namespace Cimdo2._0.Models;

public partial class BooksGener
{
    public int IdBook { get; set; }

    public int IdGener { get; set; }

    public string? Note { get; set; }

    public virtual Book IdBookNavigation { get; set; } = null!;

    public virtual Gener IdGenerNavigation { get; set; } = null!;
}
