using System;
using System.Collections.Generic;

namespace ConstructionOdering.Repositories.Entities;

public partial class Account
{
    public int Userid { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? HashPassword { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? LastLoginDate { get; set; }
}
