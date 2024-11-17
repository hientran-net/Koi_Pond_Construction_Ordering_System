using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ConstructionOdering.Repositories.Entities;

public partial class NhanVien
{
    [Required]
    [ReadOnly(true)]
    public string MaNhanVien { get; set; } = null!;

    public string? HoTen { get; set; }

    public int? Tuoi { get; set; }

    [Required]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    [DataType(DataType.Date)]
    public DateOnly? NgayThangNamSinh { get; set; }

    public bool? GioiTinh { get; set; }

    public string? DiaChia { get; set; }

    public string? QueQuan { get; set; }

    public string? SoDienThoai { get; set; }

    public string? MoTa { get; set; }

    public bool? TrangThai { get; set; }

    public string? Email { get; set; }

    public string? MatKhau { get; set; }

    public DateTime? LanDangNhapCuoi { get; set; }

    public DateTime? ThemVaoNgay { get; set; }

    public DateTime? CapNhatVaoNgay { get; set; }

    public string? ThemBoi { get; set; }

    public string? CapNhatBoi { get; set; }
}
