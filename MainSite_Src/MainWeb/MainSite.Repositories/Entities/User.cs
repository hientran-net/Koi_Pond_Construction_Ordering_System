﻿using System;
using System.Collections.Generic;

namespace MainSite.Repositories.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;
}
