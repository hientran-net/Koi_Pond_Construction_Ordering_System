using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConstructionOdering.Repositories.Entities;

public partial class DonDatHang
{
    [Required(ErrorMessage = "Vui lòng nhập mã đơn hàng")]
    public string MaDonDatHang { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn khách hàng")]
    public string MaKhachHang { get; set; }

    [Required(ErrorMessage = "Vui lòng chọn dự án")]
    public string MaDuAn { get; set; }
    public DateTime? NgayDatHang { get; set; }

    public DateTime? NgayBatDauThiCong { get; set; }

    public DateTime? NgayKetThucThiCong { get; set; }

    public DateTime? NgayKetThucThucTe { get; set; }

    public virtual DuAn? MaDuAnNavigation { get; set; }

    public virtual KhachHang? MaKhachHangNavigation { get; set; }
}
