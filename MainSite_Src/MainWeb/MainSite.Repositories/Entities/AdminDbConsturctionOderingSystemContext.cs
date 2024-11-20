using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MainSite.Repositories.Entities;

public partial class AdminDbConsturctionOderingSystemContext : DbContext
{
    public AdminDbConsturctionOderingSystemContext()
    {
    }

    public AdminDbConsturctionOderingSystemContext(DbContextOptions<AdminDbConsturctionOderingSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<DonDatHang> DonDatHangs { get; set; }

    public virtual DbSet<DuAn> DuAns { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-M7I7IF3;Initial Catalog=Admin_DB_Consturction_Odering_System;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Account__3214EC07A5AA6867");

            entity.ToTable("Account");

            entity.HasIndex(e => e.Username, "UQ__Account__536C85E4D02A185E").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Account__A9D105346C9084A6").IsUnique();

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastLoginDate).HasColumnType("datetime");
            entity.Property(e => e.PasswordHash).IsUnicode(false);
            entity.Property(e => e.RefreshToken).HasMaxLength(500);
            entity.Property(e => e.RefreshTokenExpiryTime).HasColumnType("datetime");
            entity.Property(e => e.Salt)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DonDatHang>(entity =>
        {
            entity.HasKey(e => e.MaDonDatHang).HasName("PK__Don_Dat___20F09D87A1C30F30");

            entity.ToTable("Don_Dat_Hang");

            entity.Property(e => e.MaDonDatHang)
                .HasMaxLength(10)
                .HasColumnName("ma_don_dat_hang");
            entity.Property(e => e.MaDuAn)
                .HasMaxLength(10)
                .HasColumnName("ma_du_an");
            entity.Property(e => e.MaKhachHang)
                .HasMaxLength(10)
                .HasColumnName("ma_khach_hang");
            entity.Property(e => e.NgayBatDauThiCong)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("ngay_bat_dau_thi_cong");
            entity.Property(e => e.NgayDatHang)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("ngay_dat_hang");
            entity.Property(e => e.NgayKetThucThiCong)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("ngay_ket_thuc_thi_cong");
            entity.Property(e => e.NgayKetThucThucTe)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("ngay_ket_thuc_thuc_te");

            entity.HasOne(d => d.MaDuAnNavigation).WithMany(p => p.DonDatHangs)
                .HasForeignKey(d => d.MaDuAn)
                .HasConstraintName("FK__Don_Dat_H__ma_du__49C3F6B7");

            entity.HasOne(d => d.MaKhachHangNavigation).WithMany(p => p.DonDatHangs)
                .HasForeignKey(d => d.MaKhachHang)
                .HasConstraintName("FK__Don_Dat_H__ma_kh__48CFD27E");
        });

        modelBuilder.Entity<DuAn>(entity =>
        {
            entity.HasKey(e => e.MaDuAn).HasName("PK__Du_An__8F784B5D90B32E64");

            entity.ToTable("Du_An");

            entity.Property(e => e.MaDuAn)
                .HasMaxLength(10)
                .HasColumnName("ma_du_an");
            entity.Property(e => e.ChinhSuaBoi)
                .HasMaxLength(255)
                .HasColumnName("chinh_sua_boi");
            entity.Property(e => e.DiaDiem)
                .HasMaxLength(255)
                .HasColumnName("dia_diem");
            entity.Property(e => e.GiaDuAn)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("gia_du_an");
            entity.Property(e => e.HinhAnhPath)
                .HasMaxLength(255)
                .HasColumnName("hinh_anh_path");
            entity.Property(e => e.MoTaDuAn).HasColumnName("mo_ta_du_an");
            entity.Property(e => e.NgayChinhSua)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("ngay_chinh_sua");
            entity.Property(e => e.NgayThemDuAn)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("ngay_them_du_an");
            entity.Property(e => e.SoNgayThiCongDuKien).HasColumnName("so_ngay_thi_cong_du_kien");
            entity.Property(e => e.TenDuAn)
                .HasMaxLength(255)
                .HasColumnName("ten_du_an");
            entity.Property(e => e.ThemBoi)
                .HasMaxLength(255)
                .HasColumnName("them_boi");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKhachHang).HasName("PK__Khach_Ha__C9817AF6F6EF10C5");

            entity.ToTable("Khach_Hang");

            entity.HasIndex(e => e.Email, "UQ__Khach_Ha__AB6E616452CD169C").IsUnique();

            entity.Property(e => e.MaKhachHang)
                .HasMaxLength(10)
                .HasColumnName("ma_khach_hang");
            entity.Property(e => e.ChinhSuaBoi)
                .HasMaxLength(255)
                .HasColumnName("chinh_sua_boi");
            entity.Property(e => e.DiaChi)
                .HasMaxLength(255)
                .HasColumnName("dia_chi");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.NgayChinhSua)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("ngay_chinh_sua");
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("ngay_tao");
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("so_dien_thoai");
            entity.Property(e => e.TaoBoi)
                .HasMaxLength(255)
                .HasColumnName("tao_boi");
            entity.Property(e => e.TenKhachHang)
                .HasMaxLength(255)
                .HasColumnName("ten_khach_hang");
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNhanVien).HasName("PK__Nhan_Vie__6781B7B987998D87");

            entity.ToTable("Nhan_Vien");

            entity.HasIndex(e => e.Email, "UQ__Nhan_Vie__AB6E61644B8C3708").IsUnique();

            entity.Property(e => e.MaNhanVien)
                .HasMaxLength(10)
                .HasColumnName("ma_nhan_vien");
            entity.Property(e => e.CapNhatBoi)
                .HasMaxLength(255)
                .HasColumnName("cap_nhat_boi");
            entity.Property(e => e.CapNhatVaoNgay)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("cap_nhat_vao_ngay");
            entity.Property(e => e.DiaChia).HasColumnName("dia_chia");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.GioiTinh).HasColumnName("gioi_tinh");
            entity.Property(e => e.HoTen)
                .HasMaxLength(255)
                .HasColumnName("ho_ten");
            entity.Property(e => e.LanDangNhapCuoi)
                .HasColumnType("datetime")
                .HasColumnName("lan_dang_nhap_cuoi");
            entity.Property(e => e.MatKhau)
                .HasMaxLength(255)
                .HasColumnName("mat_khau");
            entity.Property(e => e.MoTa).HasColumnName("mo_ta");
            entity.Property(e => e.NgayThangNamSinh).HasColumnName("ngay_thang_nam_sinh");
            entity.Property(e => e.QueQuan)
                .HasMaxLength(255)
                .HasColumnName("que_quan");
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("so_dien_thoai");
            entity.Property(e => e.ThemBoi)
                .HasMaxLength(255)
                .HasColumnName("them_boi");
            entity.Property(e => e.ThemVaoNgay)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("them_vao_ngay");
            entity.Property(e => e.TrangThai).HasColumnName("trang_thai");
            entity.Property(e => e.Tuoi).HasColumnName("tuoi");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0746BAB0B4");

            entity.Property(e => e.PasswordHash).HasMaxLength(256);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
