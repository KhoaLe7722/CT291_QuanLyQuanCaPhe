IF DB_ID(N'QuanLyQuanCaPhe') IS NULL
    CREATE DATABASE QuanLyQuanCaPhe;
GO
USE QuanLyQuanCaPhe;
GO
/*
DROP TABLE IF EXISTS ChiTietHoaDon;
DROP TABLE IF EXISTS HoaDon;
DROP TABLE IF EXISTS Mon;
DROP TABLE IF EXISTS LoaiMon;
DROP TABLE IF EXISTS Ban;
DROP TABLE IF EXISTS TaiKhoan;
DROP TABLE IF EXISTS Roles;
GO*/

-- 1. Roles
CREATE TABLE Roles (
    id_role  INT           NOT NULL PRIMARY KEY,
    ten_role NVARCHAR(50)  NOT NULL UNIQUE
);

-- 2. TaiKhoan
CREATE TABLE TaiKhoan (
    tai_khoan    VARCHAR(50)   NOT NULL PRIMARY KEY,
    mat_khau     VARCHAR(100)  NOT NULL,
    ten_hien_thi NVARCHAR(100) NOT NULL,
    id_role      INT           NOT NULL
        CONSTRAINT FK_TaiKhoan_Roles FOREIGN KEY REFERENCES Roles(id_role)
);

-- 3. Ban
CREATE TABLE Ban (
    ma_ban     INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    ten_ban    NVARCHAR(50)      NOT NULL UNIQUE,
    trang_thai NVARCHAR(20)      NOT NULL
        CONSTRAINT DF_Ban_TrangThai DEFAULT N'Đã thanh toán'
        CONSTRAINT CK_Ban_TrangThai CHECK (trang_thai IN (N'Chưa thanh toán', N'Đã thanh toán'))
);

-- 4. LoaiMon
CREATE TABLE LoaiMon (
    ma_loai  INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    ten_loai NVARCHAR(100)     NOT NULL UNIQUE
);

-- 5. Mon
CREATE TABLE Mon (
    ma_mon  INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    ten_mon NVARCHAR(100)     NOT NULL,
    ma_loai INT               NOT NULL
        CONSTRAINT FK_Mon_LoaiMon FOREIGN KEY REFERENCES LoaiMon(ma_loai),
    gia     DECIMAL(18,0)     NOT NULL
        CONSTRAINT CK_Mon_Gia CHECK (gia >= 0)
);

-- 6. HoaDon
CREATE TABLE HoaDon (
    ma_hoa_don      INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    ma_ban          INT           NOT NULL
        CONSTRAINT FK_HoaDon_Ban FOREIGN KEY REFERENCES Ban(ma_ban),
    tai_khoan       VARCHAR(50)   NOT NULL
        CONSTRAINT FK_HoaDon_TaiKhoan FOREIGN KEY REFERENCES TaiKhoan(tai_khoan),
    ngay_vao        DATETIME      NOT NULL CONSTRAINT DF_HoaDon_NgayVao DEFAULT GETDATE(),
    ngay_thanh_toan DATETIME      NULL,                    -- NULL = chua thanh toan, co gia tri = da thanh toan
    tong_tien       DECIMAL(18,0) NULL,
    CONSTRAINT CK_HoaDon_Ngay CHECK (ngay_thanh_toan IS NULL OR ngay_thanh_toan >= ngay_vao)
);
GO
-- Moi ban chi co toi da 1 hoa don chua thanh toan (ngay_thanh_toan IS NULL)
CREATE UNIQUE INDEX UX_HoaDon_Ban_ChuaTT ON HoaDon(ma_ban) WHERE ngay_thanh_toan IS NULL;
GO

-- 7. ChiTietHoaDon
CREATE TABLE ChiTietHoaDon (
    ma_hoa_don INT           NOT NULL
        CONSTRAINT FK_CTHD_HoaDon FOREIGN KEY REFERENCES HoaDon(ma_hoa_don),
    ma_mon     INT           NOT NULL
        CONSTRAINT FK_CTHD_Mon FOREIGN KEY REFERENCES Mon(ma_mon),
    so_luong   INT           NOT NULL CONSTRAINT CK_CTHD_SoLuong CHECK (so_luong > 0),
    don_gia    DECIMAL(18,0) NOT NULL CONSTRAINT CK_CTHD_DonGia CHECK (don_gia >= 0),
    CONSTRAINT PK_CTHD PRIMARY KEY (ma_hoa_don, ma_mon)
);
GO

/* ================= DU LIEU GOC ================= */

INSERT INTO Roles(id_role, ten_role) VALUES
(1, N'Quản trị viên'),
(2, N'Nhân viên');

INSERT INTO TaiKhoan(tai_khoan, mat_khau, ten_hien_thi, id_role) VALUES
('admin',     'admin123', N'Quản trị viên', 1),
('nhanvien1', '123456',   N'Nguyễn Văn An', 2),
('nhanvien2', '123456',   N'Trần Thị Bình', 2);

INSERT INTO Ban(ten_ban) VALUES
(N'Bàn 1'),(N'Bàn 2'),(N'Bàn 3'),(N'Bàn 4'),(N'Bàn 5'),
(N'Bàn 6'),(N'Bàn 7'),(N'Bàn 8'),(N'Bàn 9'),(N'Bàn 10');

-- LoaiMon chi co 2 loai, tuong ung 2 lop con
--   ma_loai = 1 -> Do uong (lop DoUong)    ma_loai = 2 -> Thuc an (lop ThucAn)
INSERT INTO LoaiMon(ten_loai) VALUES
(N'Đồ uống'),
(N'Thức ăn');

INSERT INTO Mon(ten_mon, ma_loai, gia)
SELECT v.ten, l.ma_loai, v.gia
FROM (VALUES
    (N'Cà phê đen',      N'Đồ uống',  25000),
    (N'Cà phê sữa',      N'Đồ uống',  29000),
    (N'Bạc xỉu',         N'Đồ uống',  32000),
    (N'Cappuccino',      N'Đồ uống',  45000),
    (N'Trà đào',         N'Đồ uống',  35000),
    (N'Trà vải',         N'Đồ uống',  35000),
    (N'Trà chanh',       N'Đồ uống',  25000),
    (N'Sinh tố bơ',      N'Đồ uống',  40000),
    (N'Sinh tố xoài',    N'Đồ uống',  40000),
    (N'Tiramisu',        N'Thức ăn',  38000),
    (N'Mousse',          N'Thức ăn',  35000),
    (N'Croissant',       N'Thức ăn',  30000),
    (N'Bánh mì ốp la',   N'Thức ăn',  30000),
    (N'Khoai tây chiên', N'Thức ăn',  35000)
) AS v(ten, loai, gia)
JOIN LoaiMon l ON l.ten_loai = v.loai;
GO
