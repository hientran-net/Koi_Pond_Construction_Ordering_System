using System;
using System.Collections.Generic;

namespace ConstructionOdering.Repositories.Entities;

public partial class DuAn
{
    public int MaDuAn { get; set; }

    public string? TenDuAn { get; set; }

    public decimal? GiaDuAn { get; set; }

    public int? SoNgayThiCongDuKien { get; set; }

    public string? MoTaDuAn { get; set; }

    public DateTime? NgayThemDuAn { get; set; }

    public string? ThemBoi { get; set; }

    public DateTime? NgayChinhSua { get; set; }

    public string? ChinhSuaBoi { get; set; }

    public virtual ICollection<DonDatHang> DonDatHangs { get; set; } = new List<DonDatHang>();
}
