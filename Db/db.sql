CREATE DATABASE Admin_DB_Consturction_Odering_System
GO

USE Admin_DB_Consturction_Odering_System
GO


CREATE TABLE Nhan_Vien
(
	ma_nhan_vien NVARCHAR(10) PRIMARY KEY,
	ho_ten NVARCHAR(255),
	tuoi INT,
	ngay_thang_nam_sinh DATE,
	gioi_tinh BIT,
	dia_chia NVARCHAR(MAX),
	que_quan NVARCHAR(255),
	so_dien_thoai VARCHAR(15),
	mo_ta NVARCHAR(MAX),
	trang_thai BIT,
	email VARCHAR(100) UNIQUE,
	mat_khau NVARCHAR(255),
	lan_dang_nhap_cuoi DATETIME,
	them_vao_ngay DATETIME DEFAULT CURRENT_TIMESTAMP,
	cap_nhat_vao_ngay DATETIME DEFAULT CURRENT_TIMESTAMP,
	them_boi NVARCHAR(255),
	cap_nhat_boi NVARCHAR(255),
)
GO

CREATE TABLE Du_An
(
	ma_du_an NVARCHAR(10) PRIMARY KEY,
	ten_du_an NVARCHAR(255),
	gia_du_an DECIMAL(10),
	so_ngay_thi_cong_du_kien INT,
	mo_ta_du_an NVARCHAR(MAX),
	ngay_them_du_an DATETIME DEFAULT CURRENT_TIMESTAMP,
	them_boi NVARCHAR(255),
	ngay_chinh_sua DATETIME DEFAULT CURRENT_TIMESTAMP,
	chinh_sua_boi NVARCHAR(255),
)
GO


CREATE TABLE Khach_Hang
(
	ma_khach_hang NVARCHAR(10) PRIMARY KEY,
	ten_khach_hang NVARCHAR(255),
	email VARCHAR(100) UNIQUE,
	so_dien_thoai VARCHAR(15),
	dia_chi NVARCHAR(255),
	ngay_tao DATETIME DEFAULT CURRENT_TIMESTAMP,
	tao_boi NVARCHAR(255),
	ngay_chinh_sua DATETIME DEFAULT CURRENT_TIMESTAMP,
	chinh_sua_boi NVARCHAR(255),
)
GO

CREATE TABLE Don_Dat_Hang
(
	ma_don_dat_hang NVARCHAR(10) PRIMARY KEY,
	ma_khach_hang NVARCHAR(10),
	ma_du_an NVARCHAR(10), 
	ngay_dat_hang DATETIME DEFAULT CURRENT_TIMESTAMP,
	ngay_bat_dau_thi_cong DATETIME DEFAULT CURRENT_TIMESTAMP,
	ngay_ket_thuc_thi_cong DATETIME DEFAULT CURRENT_TIMESTAMP,
	ngay_ket_thuc_thuc_te DATETIME DEFAULT CURRENT_TIMESTAMP,

	FOREIGN KEY(ma_khach_hang) REFERENCES Khach_Hang(ma_khach_hang),
	FOREIGN KEY (ma_du_an) REFERENCES Du_An(ma_du_an)
)
GO

CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY,
    Username NVARCHAR(50) NOT NULL,
    PasswordHash NVARCHAR(256) NOT NULL
)
GO

ALTER TABLE Du_An
ADD 
    hinh_anh_path NVARCHAR(255),
    dia_diem NVARCHAR(255);
GO

CREATE TABLE Account
(
	Id INT PRIMARY KEY IDENTITY,
	Username NVARCHAR(100) NOT NULL,
	PasswordHash NVARCHAR(MAX) NOT NULL
)
GO