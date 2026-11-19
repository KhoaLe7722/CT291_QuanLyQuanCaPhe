/* =====================================================================
   FILE 2: DU LIEU MAU HOA DON   (chay SAU file 01)
   Chay lai file nay bao nhieu lan cung duoc: se xoa hoa don cu roi them lai.
   ===================================================================== */
USE QuanLyQuanCaPhe;
GO

-- Xoa hoa don cu de chay lai khong bi loi
/*DELETE FROM ChiTietHoaDon;
DELETE FROM HoaDon;
DBCC CHECKIDENT ('HoaDon', RESEED, 0);
UPDATE Ban SET trang_thai = N'Đã thanh toán';
GO*/

-- Hoa don mau (de co du lieu cho form Hoa don va Thong ke)
DECLARE @hd INT, @t DATETIME;

-- HD 1: Ban 1, 2 ngay truoc, da thanh toan
SET @t = DATEADD(DAY, -2, GETDATE());
INSERT INTO HoaDon(ma_ban, tai_khoan, ngay_vao, ngay_thanh_toan)
VALUES ((SELECT ma_ban FROM Ban WHERE ten_ban = N'Bàn 1'), 'nhanvien1', @t, DATEADD(HOUR, 1, @t));
SET @hd = SCOPE_IDENTITY();
INSERT INTO ChiTietHoaDon(ma_hoa_don, ma_mon, so_luong, don_gia)
SELECT @hd, ma_mon, 2, gia FROM Mon WHERE ten_mon = N'Cà phê đen'
UNION ALL
SELECT @hd, ma_mon, 1, gia FROM Mon WHERE ten_mon = N'Tiramisu';

-- HD 2: Ban 2, hom qua, da thanh toan
SET @t = DATEADD(DAY, -1, GETDATE());
INSERT INTO HoaDon(ma_ban, tai_khoan, ngay_vao, ngay_thanh_toan)
VALUES ((SELECT ma_ban FROM Ban WHERE ten_ban = N'Bàn 2'), 'nhanvien1', @t, DATEADD(MINUTE, 45, @t));
SET @hd = SCOPE_IDENTITY();
INSERT INTO ChiTietHoaDon(ma_hoa_don, ma_mon, so_luong, don_gia)
SELECT @hd, ma_mon, 1, gia FROM Mon WHERE ten_mon = N'Cà phê sữa'
UNION ALL
SELECT @hd, ma_mon, 2, gia FROM Mon WHERE ten_mon = N'Trà đào';

-- HD 3: Ban 3, hom nay, da thanh toan
SET @t = DATEADD(HOUR, -3, GETDATE());
INSERT INTO HoaDon(ma_ban, tai_khoan, ngay_vao, ngay_thanh_toan)
VALUES ((SELECT ma_ban FROM Ban WHERE ten_ban = N'Bàn 3'), 'nhanvien2', @t, DATEADD(HOUR, 1, @t));
SET @hd = SCOPE_IDENTITY();
INSERT INTO ChiTietHoaDon(ma_hoa_don, ma_mon, so_luong, don_gia)
SELECT @hd, ma_mon, 1, gia FROM Mon WHERE ten_mon = N'Sinh tố bơ';

-- HD 4: Ban 4, dang phuc vu (chua thanh toan)
INSERT INTO HoaDon(ma_ban, tai_khoan)
VALUES ((SELECT ma_ban FROM Ban WHERE ten_ban = N'Bàn 4'), 'nhanvien2');
SET @hd = SCOPE_IDENTITY();
INSERT INTO ChiTietHoaDon(ma_hoa_don, ma_mon, so_luong, don_gia)
SELECT @hd, ma_mon, 2, gia FROM Mon WHERE ten_mon = N'Bạc xỉu';
UPDATE Ban SET trang_thai = N'Chưa thanh toán' WHERE ten_ban = N'Bàn 4';

-- Tinh tong tien cho cac hoa don da thanh toan (ngay_thanh_toan co gia tri)
UPDATE h
SET tong_tien = (SELECT SUM(c.so_luong * c.don_gia) FROM ChiTietHoaDon c WHERE c.ma_hoa_don = h.ma_hoa_don)
FROM HoaDon h
WHERE h.ngay_thanh_toan IS NOT NULL;
GO
