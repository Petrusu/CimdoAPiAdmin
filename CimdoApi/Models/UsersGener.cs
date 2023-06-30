using System;
using System.Collections.Generic;

namespace Cimdo2._0.Models;

public partial class UsersGener
{
    public int IdUser { get; set; }

    public int IdGener { get; set; }

    public string? Note { get; set; }

    public virtual Gener IdGenerNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
