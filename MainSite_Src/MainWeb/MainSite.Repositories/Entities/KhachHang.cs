﻿using System;
using System.Collections.Generic;

namespace MainSite.Repositories.Entities;

public partial class KhachHang
{
    public string MaKhachHang { get; set; } = null!;

    public string? TenKhachHang { get; set; }

    public string? Email { get; set; }

    public string? SoDienThoai { get; set; }

    public string? DiaChi { get; set; }

    public DateTime? NgayTao { get; set; }

    public string? TaoBoi { get; set; }

    public DateTime? NgayChinhSua { get; set; }

    public string? ChinhSuaBoi { get; set; }

    public virtual ICollection<DonDatHang> DonDatHangs { get; set; } = new List<DonDatHang>();
}
