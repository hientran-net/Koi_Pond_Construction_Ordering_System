using System;
using System.Collections.Generic;

namespace ConstructionOdering.Repositories.Entities;

public partial class DonDatHang
{
    public string MaDonDatHang { get; set; } = null!;

    public string? MaKhachHang { get; set; }

    public string? MaDuAn { get; set; }

    public DateTime? NgayDatHang { get; set; }

    public DateTime? NgayBatDauThiCong { get; set; }

    public DateTime? NgayKetThucThiCong { get; set; }

    public DateTime? NgayKetThucThucTe { get; set; }

    public virtual DuAn? MaDuAnNavigation { get; set; }

    public virtual KhachHang? MaKhachHangNavigation { get; set; }
}
