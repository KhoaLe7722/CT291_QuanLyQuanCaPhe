# ☕ HỆ THỐNG QUẢN LÝ QUÁN CÀ PHÊ (CT291 - LẬP TRÌNH ỨNG DỤNG)

Phần mềm **Quản Lý Quán Cà Phê** được xây dựng bằng **C# Windows Forms (.NET Framework 4.7.2)** kết hợp cơ sở dữ liệu **SQL Server (ADO.NET)** và áp dụng kiến trúc **Lập trình hướng đối tượng (OOP)**.

---

## 📁 1. Cấu trúc thư mục dự án

```text
CT291_QuanLyQuanCaPhe/
├── 00_PhanCong.docx                  # Tài liệu phân công công việc nhóm
├── 01_CSDL/
│   ├── 01_DBgoc .sql                 # Script tạo CSDL, bảng, ràng buộc và dữ liệu gốc (10 bàn, 14 món)
│   └── 02_MauHoaDon.sql              # Script dữ liệu hóa đơn mẫu để kiểm thử
├── 02_Khung/
│   └── QuanLyQuanCaPhe/              # Solution Visual Studio hoàn chỉnh (Build & Run trực tiếp)
├── 03_NopBai/                        # Thư mục nộp bài của 5 thành viên (đủ bộ 3 file .cs, .Designer.cs, .resx)
│   ├── Minh/                         # fLogin_v1.*, fMain_v1.*, KetNoi.cs, PhienDangNhap.cs, Program.cs
│   ├── Khang/                        # fQuanLyMon_v1.*, Mon.cs, DoUong.cs, ThucAn.cs, QuanLyHinhAnh.cs
│   ├── Thien/                        # fBanHang_v1.*, QuanLyHinhAnh.cs
│   ├── Khoa/                         # fThanhToan_v1.*, fHoaDon_v1.*
│   └── Cuong/                        # fThongKe_v1.*
└── menu_images/                      # Bộ 14 hình ảnh món ăn/đồ uống thực tế (.jpg)
```

---

## 🚀 2. Hướng dẫn cài đặt và chạy chương trình

1. **Khởi tạo Cơ sở dữ liệu (SQL Server)**:
   - Mở **SQL Server Management Studio (SSMS)**.
   - Mở và chạy file `01_CSDL/01_DBgoc .sql` trước.
   - Mở và chạy tiếp file `01_CSDL/02_MauHoaDon.sql`.
2. **Cấu hình chuỗi kết nối**:
   - Mở solution `02_Khung/QuanLyQuanCaPhe/QuanLyQuanCaPhe.sln` bằng Visual Studio.
   - Mở file `Ketnoi.cs`, kiểm tra biến `chuoiKetNoi` và sửa `Data Source=.` (hoặc `Data Source=.\SQLEXPRESS`) cho đúng tên SQL Server trên máy.
3. **Tài khoản đăng nhập mặc định**:
   | Tài khoản | Mật khẩu | Vai trò (`id_role`) | Quyền hạn |
   | :--- | :--- | :---: | :--- |
   | `admin` | `admin123` | `1` (Quản trị viên) | Toàn quyền (Quản lý món, Bán hàng, Hóa đơn, Thống kê) |
   | `nhanvien1` | `123456` | `2` (Nhân viên) | Chỉ thấy Bán hàng và Hóa đơn (ẩn Quản lý món & Thống kê) |
   | `nhanvien2` | `123456` | `2` (Nhân viên) | Chỉ thấy Bán hàng và Hóa đơn (ẩn Quản lý món & Thống kê) |

---

## 👥 3. Phân công nhiệm vụ nhóm

| Thành viên | Phần phụ trách | Chi tiết công việc |
| :--- | :--- | :--- |
| **Minh** | `fLogin`, `fMain`, `KetNoi.cs`, `PhienDangNhap.cs`, `Program.cs` | Đăng nhập, bật/tắt hiển thị mật khẩu, trang chủ Dashboard, phân quyền (nhân viên không thấy Quản lý món và Thống kê). |
| **Khang** | `fQuanLyMon`, `Mon.cs`, `DoUong.cs`, `ThucAn.cs`, `QuanLyHinhAnh.cs` | Thêm / sửa / xóa món, tìm kiếm món, hiển thị hình ảnh món, hoàn tác `Ctrl + Z`, thiết kế cây kế thừa OOP `Mon` → `DoUong`, `ThucAn`. |
| **Thiện** | `fBanHang` | Hiển thị danh sách bàn trực quan theo màu, chọn bàn, lọc/tìm và gọi món kèm ảnh, xóa món, tính tổng tiền, mở `fThanhToan`. |
| **Khoa** | `fThanhToan`, `fHoaDon` | Xác nhận thanh toán, chốt hóa đơn, trả bàn về trạng thái trống, tự động hiện phiếu in ngay khi thanh toán; quản lý, lọc hóa đơn theo ngày, xem chi tiết và in hóa đơn. |
| **Cường** | `fThongKe` + Kiểm thử | Thống kê doanh thu theo khoảng ngày hoặc theo tháng/năm, bấm vào từng ngày để xem danh sách hóa đơn và chi tiết món của hóa đơn đó. |

---

## 📖 4. Giải thích chi tiết toàn bộ Biến và Hàm trong Mã nguồn

---

### PHẦN 1: MINH (`KetNoi.cs`, `PhienDangNhap.cs`, `Program.cs`, `fLogin`, `fMain`)

#### 1.1. Lớp tĩnh `KetNoi` (`Ketnoi.cs`)
Đóng gói toàn bộ thao tác kết nối và truy vấn SQL Server bằng ADO.NET.
- **Biến (Fields)**:
  - `private static string chuoiKetNoi`: Chuỗi kết nối đến CSDL `QuanLyQuanCaPhe` (`Data Source=.;Initial Catalog=QuanLyQuanCaPhe;Integrated Security=True`).
- **Hàm (Methods)**:
  - `public static SqlConnection TaoKetNoi()`: Khởi tạo và trả về đối tượng `SqlConnection` từ `chuoiKetNoi`.
  - `public static SqlParameter P(string ten, object giaTri)`: Tạo đối tượng `SqlParameter` an toàn (tự động chuyển `null` thành `DBNull.Value`, tránh lỗi ép kiểu khi giá trị là số `0`).
  - `public static DataTable LayBang(string sql, params SqlParameter[] ps)`: Thực thi câu lệnh `SELECT` qua `SqlDataAdapter` và trả về bảng dữ liệu `DataTable`.
  - `public static object LayGiaTri(string sql, params SqlParameter[] ps)`: Thực thi `ExecuteScalar()` để lấy 1 giá trị đơn lẻ (`COUNT`, `SUM`, `MAX`, `SCOPE_IDENTITY()`).
  - `public static int ChayLenh(string sql, params SqlParameter[] ps)`: Thực thi câu lệnh `INSERT`, `UPDATE`, `DELETE` bằng `ExecuteNonQuery()` và trả về số dòng bị ảnh hưởng.

#### 1.2. Lớp tĩnh `PhienDangNhap` (`PhienDangNhap.cs`)
Lưu trữ thông tin phiên làm việc của tài khoản đang đăng nhập để dùng chung giữa các Form.
- **Biến (Fields)**:
  - `public static string TaiKhoan`: Tên đăng nhập (`admin`, `nhanvien1`...). Dùng để ghi nhận nhân viên lập hóa đơn.
  - `public static string TenHienThi`: Họ tên hiển thị trên thanh tiêu đề và phiếu in hóa đơn (`Quản trị viên`, `Nguyễn Văn An`...).
  - `public static int IdRole`: Mã phân quyền (`1` = Quản trị viên, `2` = Nhân viên).

#### 1.3. Lớp `fLogin` (`Form1.cs` / `fLogin_v1.cs`)
Form Đăng nhập hệ thống.
- **Biến điều khiển chính (Controls)**:
  - `txbUsername`: Ô nhập tên tài khoản.
  - `textBox1`: Ô nhập mật khẩu (`UseSystemPasswordChar = true`).
  - `btnShowPass`: Nút con mắt `👁` bật/tắt hiển thị mật khẩu.
  - `btnLogin`, `btnExit`: Nút Đăng nhập và Thoát.
- **Hàm (Methods)**:
  - `private void btnLogin_Click(object sender, EventArgs e)`: Kiểm tra rỗng, truy vấn bảng `TaiKhoan` với tham số `@tk` và `@mk`. Nếu hợp lệ sẽ lưu dữ liệu vào `PhienDangNhap`, ẩn `fLogin`, mở `fMain` bằng `ShowDialog()`, và xóa trắng ô mật khẩu sau khi đăng xuất.
  - `private void btnExit_Click(object sender, EventArgs e)`: Đóng toàn bộ ứng dụng (`Application.Exit()`).
  - `private void btnShowPass_Click(object sender, EventArgs e)` *(Hàm mới)*: Đảo trạng thái `textBox1.UseSystemPasswordChar` để hiện hoặc ẩn mật khẩu đang gõ và đổi biểu tượng giữa `👁` và `🙈`.

#### 1.4. Lớp `fMain` (`Form2.cs` / `fMain_v1.cs`)
Form Trang chủ (Dashboard) và điều hướng chính.
- **Biến điều khiển chính (Controls)**:
  - `quảnLýMónToolStripMenuItem`, `bánHàngToolStripMenuItem`, `hóaĐơnToolStripMenuItem`, `thốngKêToolStripMenuItem`, `đăngXuấtToolStripMenuItem`: Các mục trên thanh `MenuStrip`.
  - `lblSoBanPhucVu`, `lblSoBanTrong`, `lblSoHoaDonHomNay`, `lblDoanhThuHomNay`: Các nhãn hiển thị số liệu trực tiếp trên 3 thẻ thống kê Dashboard.
  - `btnQuickBanHang`, `btnQuickHoaDon`, `btnQuickQuanLyMon`, `btnQuickThongKe`: 4 nút truy cập nhanh các phân hệ chính.
  - `lblClock`, `timerClock`: Đồng hồ thời gian thực cập nhật mỗi giây (`Interval = 1000`).
- **Hàm (Methods)**:
  - `private void fMain_Load(object sender, EventArgs e)`: Hiển thị tên người dùng và quyền hạn; kiểm tra `PhienDangNhap.IdRole == 2` (Nhân viên) để ẩn menu & nút **Quản lý món** cùng **Thống kê**; khởi động đồng hồ và gọi `CapNhatDashboard()`.
  - `public void CapNhatDashboard()` *(Hàm mới)*: Truy vấn CSDL thời gian thực để đếm số bàn đang có khách (`Chưa thanh toán`), số bàn trống, tổng số hóa đơn đã thanh toán trong ngày hôm nay và tổng doanh thu hôm nay.
  - `private void timerClock_Tick(object sender, EventArgs e)` *(Hàm mới)*: Cập nhật thời gian hiện tại (`HH:mm:ss - dd/MM/yyyy`) lên góc phải Header.
  - `private void quảnLýMónToolStripMenuItem_Click(...)`: Mở `fQuanLyMon` và làm mới Dashboard sau khi đóng.
  - `private void bánHàngToolStripMenuItem_Click(...)`: Mở `fBanHang` và làm mới Dashboard sau khi đóng.
  - `private void hóaĐơnToolStripMenuItem_Click(...)`: Mở `fHoaDon` và làm mới Dashboard sau khi đóng.
  - `private void thốngKêToolStripMenuItem_Click(...)`: Mở `fThongKe` và làm mới Dashboard sau khi đóng.
  - `private void đăngXuấtToolStripMenuItem_Click(...)`: Đóng `fMain` để quay lại màn hình `fLogin`.

---

### PHẦN 2: KHANG (`Mon.cs`, `DoUong.cs`, `ThucAn.cs`, `QuanLyHinhAnh.cs`, `fQuanLyMon`)

#### 2.1. Các lớp OOP (`Mon.cs`, `DoUong.cs`, `ThucAn.cs`)
Áp dụng tính đóng gói (Encapsulation), kế thừa (Inheritance) và đa hình (Polymorphism):
- **Lớp cha trừu tượng `public abstract class Mon` (`Mon.cs`)**:
  - **Thuộc tính (Properties)**:
    - `public int MaMon { get; set; }`: Mã định danh món ăn.
    - `public string TenMon { get; set; }`: Tên món.
    - `public int MaLoai { get; set; }`: Mã loại món (`1` = Đồ uống, `2` = Thức ăn).
    - `public decimal Gia { get; set; }`: Đơn giá bán.
  - **Hàm (Methods)**:
    - `public Mon()`, `public Mon(int maMon, string tenMon, int maLoai, decimal gia)`: Các hàm khởi tạo (Constructors).
    - `public abstract string LayPhanLoai()`: Phương thức trừu tượng trả về tên phân loại món để các lớp con ghi đè.
- **Lớp con `public class DoUong : Mon` (`DoUong.cs`)**:
  - Kế thừa `Mon`, cố định `MaLoai = 1`.
  - Ghi đè `public override string LayPhanLoai()` trả về `"Đồ uống"`.
- **Lớp con `public class ThucAn : Mon` (`ThucAn.cs`)**:
  - Kế thừa `Mon`, cố định `MaLoai = 2`.
  - Ghi đè `public override string LayPhanLoai()` trả về `"Thức ăn"`.

#### 2.2. Lớp tiện ích hình ảnh `QuanLyHinhAnh` (`QuanLyHinhAnh.cs`)
- **Biến (Fields)**:
  - `private static readonly string[] danhSachAnh`: Mảng ánh xạ 14 món mặc định với tên file ảnh (`01_ca_phe_den.jpg` đến `14_khoai_tay_chien.jpg`).
- **Hàm (Methods)**:
  - `public static Image LayAnhMon(int maMon, string tenMon = "")`: Tìm file ảnh trong thư mục `menu_images` dựa theo tên món không dấu hoặc mã món; đọc ảnh qua `MemoryStream` để tránh khóa file trên ổ đĩa.
  - `private static string BoDau(string input)`: Chuẩn hóa chuỗi tiếng Việt có dấu thành không dấu để so khớp tên file ảnh.

#### 2.3. Lớp `fQuanLyMon` (`Form3.cs` / `fQuanLyMon_v1.cs`)
Form Quản lý danh mục món ăn & đồ uống (Thêm, Sửa, Xóa, Tìm kiếm, Hoàn tác `Ctrl + Z`).
- **Biến thành viên (Fields)**:
  - `private enum LoaiThaoTac { Them, Sua, Xoa }` & `private class LichSuThaoTac`: Cấu trúc lưu trạng thái thao tác CSDL (`Loai`, `MaMon`, `TenMonCu`, `MaLoaiCu`, `GiaCu`) phục vụ hoàn tác.
  - `private readonly Stack<LichSuThaoTac> stackUndo`: Ngăn xếp LIFO lưu lịch sử các thao tác Thêm/Sửa/Xóa để hoàn tác bằng `Ctrl + Z`.
  - `private bool dangThemMoi`: Cờ trạng thái phân biệt rõ đang ở chế độ **Tạo món mới** (`true`) hay **Xem/Sửa món hiện có** (`false`).
  - `private int maMonDangChon`, `private string tenMonGoc`, `tenLoaiGoc`, `giaGoc`: Lưu giá trị gốc của món đang chọn để khôi phục nhanh khi người dùng gõ nhầm trên ô nhập.
- **Hàm (Methods)**:
  - `private void fQuanLyMon_Load(object sender, EventArgs e)`: Tải danh mục loại món vào `cboLoaiMon`, tải danh sách món lên `dgvMon` và cập nhật trạng thái nút Undo.
  - `private void TaiDanhMucLoaiMon()`: Truy vấn bảng `LoaiMon` đổ vào `cboLoaiMon` (`DisplayMember = "ten_loai"`, `ValueMember = "ma_loai"`).
  - `private void TaiDanhSachMon(string tuKhoa = "", int chonMaMon = -1)`: Nạp danh sách món (kết hợp `JOIN LoaiMon`), hỗ trợ lọc theo từ khóa `LIKE @tuKhoa` và tự động chọn lại dòng có `chonMaMon`.
  - `private void txtTimKiem_TextChanged(object sender, EventArgs e)`: Tự động lọc danh sách món ngay khi gõ vào ô tìm kiếm.
  - `private void dgvMon_CellClick(object sender, DataGridViewCellEventArgs e)`: Khi chọn 1 dòng trên bảng, tắt chế độ thêm mới (`dangThemMoi = false`), đổ dữ liệu lên các ô `txtMaMon`, `txtTenMon`, `cboLoaiMon`, `txtGia` và hiển thị ảnh món lên `picAnhMonQL`.
  - `private void BatCheDoThemMonMoi()` *(Hàm mới)*: Bỏ chọn bảng `dgvMon`, bật `dangThemMoi = true`, dự tính mã món mới tiếp theo, xóa trắng các ô nhập để người dùng nhập món mới hoàn toàn (không bị sửa đè lên món cũ).
  - `private void btnThemMoi_Click(object sender, EventArgs e)`: Nút `➕ Thêm món mới` (đặt bên trái thanh Tìm món). Nếu đang xem món cũ sẽ gọi `BatCheDoThemMonMoi()`; nếu đã nhập thông tin món mới thì lưu vào CSDL.
  - `private void btnLuuMoi_Click(object sender, EventArgs e)` & `private void LuuMonMoiVaoCSDL()` *(Hàm mới)*: Kiểm tra dữ liệu, khởi tạo đối tượng đa hình `DoUong` hoặc `ThucAn`, thực thi `INSERT INTO Mon`, lấy mã món mới qua `SCOPE_IDENTITY()` và đẩy vào `stackUndo`.
  - `private void btnSua_Click(object sender, EventArgs e)`: Lưu thông tin cũ của món vào `stackUndo`, sau đó thực thi `UPDATE Mon SET ten_mon = @ten, ma_loai = @loai, gia = @gia WHERE ma_mon = @ma`.
  - `private void btnXoa_Click(object sender, EventArgs e)`: Kiểm tra món đã tồn tại trong `ChiTietHoaDon` chưa (nếu có thì chặn xóa để bảo toàn lịch sử hóa đơn), lưu thông tin món vào `stackUndo` rồi thực thi `DELETE FROM Mon`.
  - `protected override bool ProcessCmdKey(ref Message msg, Keys keyData)` *(Hàm mới)*: Bắt tổ hợp phím `Ctrl + Z` trên toàn Form để gọi `ThucHienHoanTac()`.
  - `private void btnUndo_Click(object sender, EventArgs e)` & `private void ThucHienHoanTac()` *(Hàm mới)*:
    - Nếu người dùng đang gõ sửa dở trên ô nhập mà chưa bấm Lưu: trả lại ngay chữ gốc (`tenMonGoc`, `giaGoc`, `tenLoaiGoc`).
    - Nếu đã thực hiện trong CSDL: lấy thao tác gần nhất từ `stackUndo` để khôi phục món vừa xóa (`SET IDENTITY_INSERT Mon ON`), hoàn nguyên thông tin trước khi sửa, hoặc xóa món vừa thêm nhầm.
  - `private void btnLamMoi_Click(object sender, EventArgs e)`: Xóa từ khóa tìm kiếm và tải lại toàn bộ danh sách món.

---

### PHẦN 3: THIỆN (`fBanHang`)

#### 3.1. Lớp `fBanHang` (`Form4.cs` / `fBanHang_v1.cs`)
Form Bán hàng tại quầy (Chọn bàn, gọi món, xóa món, xem tổng tiền, mở form Thanh toán).
- **Biến thành viên (Fields)**:
  - `private int maBanDangChon = -1`: Lưu mã bàn đang được nhân viên click chọn.
  - `private string tenBanDangChon = ""`: Tên bàn đang chọn (`Bàn 1`, `Bàn 2`...).
  - `private string trangThaiDangChon = ""`: Trạng thái bàn trong CSDL (`Đã thanh toán` = bàn trống, `Chưa thanh toán` = đang có khách).
  - `private int maHoaDonHienTai = -1`: Mã hóa đơn chưa thanh toán (`ngay_thanh_toan IS NULL`) của bàn đang chọn (`-1` nếu bàn chưa mở hóa đơn).
  - `private decimal tongTienHienTai = 0`: Tổng tiền tạm tính của bàn đang chọn.
- **Hàm (Methods)**:
  - `private void fBanHang_Load(object sender, EventArgs e)`: Khởi tạo giao diện bán hàng, gọi `TaiDanhSachBan()`, `TaiDanhMucLoaiMon()`, `TaiDanhSachMon()`.
  - `public void TaiDanhSachBan()`: Truy vấn bảng `Ban`, tự động sinh các nút bấm (`Button` kích thước `140x120`) vào `flpBan`. Tô màu **Xanh lá (`LightGreen`)** và ghi **`(Bàn trống)`** nếu bàn trống; tô màu **Đỏ cam (`LightCoral`)** và ghi **`(Chưa thanh toán)`** nếu bàn đang có khách; đóng khung viền xanh đậm cho bàn đang chọn.
  - `private void BtnBan_Click(object sender, EventArgs e)`: Sự kiện khi click vào 1 nút bàn. Lưu `maBanDangChon`, hiển thị tên bàn & trạng thái (`Bàn trống` / `Chưa thanh toán`) lên `label1`, `label2`, và gọi `TaiHoaDonBanDangChon()`.
  - `private void TaiHoaDonBanDangChon()`: Tìm hóa đơn có `ngay_thanh_toan IS NULL` của `maBanDangChon`. Nếu có, nạp danh sách món đã gọi từ `ChiTietHoaDon` lên `dataGridView1` và cộng tổng thành tiền hiển thị lên `label8`.
  - `private void TaiDanhMucLoaiMon()`: Nạp danh sách loại món (kèm mục `-- Tất cả --`) vào `comboBox1`.
  - `private void TaiDanhSachMon()`: Lọc danh sách món theo loại (`comboBox1`) và từ khóa tên món (`txtTim`), hiển thị lên `dgvDanhSachMon`.
  - `private void dgvDanhSachMon_SelectionChanged(object sender, EventArgs e)` *(Hàm mới)*: Khi chọn món trong bảng thực đơn, tự động nạp hình ảnh món ăn tương ứng lên `picAnhMon` và tên món lên `lblTenAnhMon`.
  - `private void comboBox1_SelectedIndexChanged(...)` & `private void txtTim_TextChanged(...)`: Tự động gọi `TaiDanhSachMon()` khi đổi loại món hoặc gõ tìm kiếm.
  - `private void btnThemMon_Click(object sender, EventArgs e)`:
    - Nếu bàn đang trống (`maHoaDonHienTai <= 0`): Tạo hóa đơn mới trong bảng `HoaDon` (`INSERT INTO HoaDon... SELECT SCOPE_IDENTITY()`) và cập nhật trạng thái bàn sang `N'Chưa thanh toán'`.
    - Kiểm tra món đã có trong `ChiTietHoaDon` của hóa đơn này chưa: nếu có rồi thì cộng dồn số lượng (`UPDATE ChiTietHoaDon SET so_luong = so_luong + @sl`), nếu chưa có thì thêm dòng mới (`INSERT INTO ChiTietHoaDon`).
    - Tải lại hóa đơn bàn và sơ đồ bàn.
  - `private void btnXoaMon_Click(object sender, EventArgs e)`: Xóa món đang chọn khỏi `ChiTietHoaDon`. Nếu sau khi xóa mà hóa đơn không còn món nào (`COUNT(*) == 0`), tự động xóa hóa đơn rỗng đó và trả bàn về trạng thái trống (`Bàn trống`).
  - `private void btnThanhToan_Click(object sender, EventArgs e)`: Kiểm tra bàn có hóa đơn hợp lệ không, sau đó khởi tạo và mở form `fThanhToan(maBanDangChon, maHoaDonHienTai)`. Nếu kết quả trả về `DialogResult.OK`, làm mới lại sơ đồ bàn và bảng chi tiết.

---

### PHẦN 4: KHOA (`fThanhToan`, `fHoaDon`)

#### 4.1. Lớp `fThanhToan` (`Form5.cs` / `fThanhToan_v1.cs`)
Form Xác nhận thanh toán, chốt hóa đơn, trả bàn về trạng thái trống và tự động xuất phiếu hóa đơn.
- **Biến thành viên (Fields)**:
  - `private int maBan = -1`: Mã bàn cần thanh toán (nhận từ `fBanHang`).
  - `private int maHoaDon = -1`: Mã hóa đơn cần thanh toán.
  - `private decimal tongTien = 0`: Tổng số tiền thanh toán của hóa đơn.
  - `private string tenBan`, `private string tenNhanVien`, `private DateTime ngayVao`, `ngayThanhToan`, `private DataTable dtChiTiet`: Lưu thông tin chi tiết hóa đơn để vẽ lên phiếu hóa đơn ngay sau khi chốt thanh toán.
- **Hàm (Methods)**:
  - `public fThanhToan()`, `public fThanhToan(int maBan)`, `public fThanhToan(int maBan, int maHoaDon)`: Các Constructor nhận mã bàn và mã hóa đơn từ `fBanHang`.
  - `private void fThanhToan_Load(object sender, EventArgs e)`: Gọi `TaiThongTinThanhToan()`.
  - `private void TaiThongTinThanhToan()`: Truy vấn thông tin chung của hóa đơn (`HoaDon JOIN Ban JOIN TaiKhoan`) để hiển thị lên `label1..4`; truy vấn danh sách món (`ChiTietHoaDon JOIN Mon`) đổ vào `dataGridView1` và tính `tongTien` hiển thị lên `label5`.
  - `private void btnXacNhan_Click(object sender, EventArgs e)`:
    1. Hỏi xác nhận thanh toán.
    2. Chốt hóa đơn: `UPDATE HoaDon SET ngay_thanh_toan = @ngayTT, tong_tien = @tongTien WHERE ma_hoa_don = @maHD`.
    3. Trả bàn về trạng thái trống: `UPDATE Ban SET trang_thai = N'Đã thanh toán' WHERE ma_ban = @maBan`.
    4. Hiển thị thông báo `"Thanh toán thành công!"` và gọi ngay `HienThiPhieuHoaDon()` để bật phiếu hóa đơn lên màn hình.
    5. Gán `this.DialogResult = DialogResult.OK` và đóng form.
  - `private void HienThiPhieuHoaDon()` & `private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)` *(Hàm mới)*: Khởi tạo `PrintDocument` và `PrintPreviewDialog` khổ lớn, vẽ phiếu hóa đơn rõ nét ngay sau khi thanh toán thành công.
  - `private void btnHuy_Click(object sender, EventArgs e)`: Hủy thao tác thanh toán (`DialogResult.Cancel`) và đóng form.

#### 4.2. Lớp `fHoaDon` (`Form6.cs` / `fHoaDon_v1.cs`)
Form Quản lý lịch sử hóa đơn, lọc theo khoảng ngày, xem chi tiết và in lại hóa đơn.
- **Biến thành viên (Fields)**:
  - `private int maHoaDonDangChon = -1`: Mã hóa đơn đang được chọn trong bảng `dgvHoaDon`.
  - `private string tenBanDangChon`, `tenNhanVienDangChon`: Tên bàn và tên nhân viên thu ngân của hóa đơn đang chọn.
  - `private DateTime ngayVaoDangChon`, `ngayThanhToanDangChon`: Thời gian khách vào và thời gian thanh toán.
  - `private decimal tongTienDangChon = 0`: Tổng tiền của hóa đơn đang chọn.
  - `private DataTable dtChiTietIn = null`: Bảng chứa danh sách các món của hóa đơn đang chọn để hiển thị lên `dgvChiTiet` và truyền cho bộ phận in.
- **Hàm (Methods)**:
  - `private void fHoaDon_Load(object sender, EventArgs e)`: Mặc định đặt `dtpTu` là ngày đầu tháng hiện tại, `dtpDen` là ngày hôm nay, rồi gọi `TaiDanhSachHoaDon()`.
  - `private void TaiDanhSachHoaDon()`: Truy vấn tất cả hóa đơn đã thanh toán (`ngay_thanh_toan IS NOT NULL`) nằm trong khoảng `[dtpTu.Value.Date, dtpDen.Value.Date + 1 ngày)`, hiển thị lên `dgvHoaDon`. Tự động chọn dòng đầu tiên để nạp chi tiết.
  - `private void btnLoc_Click(object sender, EventArgs e)`: Nút `🔍 Lọc hóa đơn`, gọi lại `TaiDanhSachHoaDon()` theo khoảng ngày người dùng chọn.
  - `private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)`: Bắt sự kiện click chọn hóa đơn và gọi `HienThiChiTietDong(e.RowIndex)`.
  - `private void HienThiChiTietDong(int rowIndex)`: Lấy `Mã HĐ` từ dòng được chọn, truy vấn bảng `ChiTietHoaDon JOIN Mon` để đổ danh sách món (`Tên món`, `Số lượng`, `Đơn giá`, `Thành tiền`) sang bảng `dgvChiTiet` bên phải và cập nhật `label3` (`Tổng: ... VNĐ`).
  - `private void btnInHoaDon_Click(object sender, EventArgs e)`: Nút `🖨️ In hóa đơn`, khởi tạo `PrintDocument` với khổ giấy tự động co giãn theo số món (`720 × chiều cao`) và mở hộp thoại `PrintPreviewDialog` (`1000 × 780`, `AutoZoom = true`).
  - `private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)`: Vẽ nội dung phiếu hóa đơn bằng `Graphics` (`DrawString`, `DrawLine`) với font `Arial` cỡ lớn sắc nét.

---

### PHẦN 5: CƯỜNG (`fThongKe`)

#### 5.1. Lớp `fThongKe` (`Form7.cs` / `fThongKe_v1.cs`)
Form Thống kê doanh thu theo ngày / theo tháng và xem trực tiếp danh sách hóa đơn + chi tiết món của từng ngày.
- **Biến điều khiển chính (Controls)**:
  - `radioButton1` (`Theo ngày`), `radioButton2` (`Theo tháng`): Lựa chọn chế độ thống kê.
  - `dateTimePicker1` (`Từ ngày`), `dateTimePicker2` (`Đến ngày`): Chọn khoảng ngày khi thống kê theo ngày.
  - `comboBox1` (`Tháng 1..12`), `comboBox2` (`Năm`): Chọn tháng và năm khi thống kê theo tháng.
  - `dataGridView1`: Bảng bên trái hiển thị doanh thu tổng hợp theo từng ngày (`Ngày`, `Số hóa đơn`, `Doanh thu`).
  - `dgvHoaDonNgay`: Bảng bên phải (phía trên) hiển thị danh sách các hóa đơn của ngày đang chọn (`Mã HĐ`, `Bàn`, `Nhân viên`, `Giờ thanh toán`, `Tổng tiền`).
  - `dgvChiTietThongKe`: Bảng bên phải (phía dưới) hiển thị chi tiết các món đã bán trong hóa đơn đang chọn (`Tên món`, `Số lượng`, `Đơn giá`, `Thành tiền`).
  - `label5`: Nhãn hiển thị tổng doanh thu của toàn bộ kỳ thống kê.
- **Hàm (Methods)**:
  - `private void fThongKe_Load(object sender, EventArgs e)`: Khởi tạo ngày đầu tháng đến hiện tại, nạp danh sách tháng (`1..12`) vào `comboBox1`, danh sách năm vào `comboBox2`, cập nhật trạng thái điều khiển và gọi `ThongKeDoanhThu()`.
  - `private void rdoTheoNgay_CheckedChanged(...)` & `private void rdoTheoThang_CheckedChanged(...)`: Gọi `CapNhatTrangThaiControls()` khi chuyển đổi qua lại giữa 2 chế độ.
  - `private void CapNhatTrangThaiControls()`: Bật `dateTimePicker1..2` và tắt `comboBox1..2` nếu chọn `Theo ngày` (và ngược lại nếu chọn `Theo tháng`).
  - `private void btnXemThongKe_Click(object sender, EventArgs e)`: Nút `📊 Xem thống kê`, gọi `ThongKeDoanhThu()`.
  - `private void ThongKeDoanhThu()`: Thực thi câu lệnh `SELECT ... COUNT(ma_hoa_don), SUM(tong_tien) ... GROUP BY CAST(ngay_thanh_toan AS DATE)` theo khoảng ngày hoặc theo tháng/năm; hiển thị lên `dataGridView1`, tính tổng doanh thu toàn kỳ lên `label5`, và tự động chọn dòng ngày đầu tiên để nạp chi tiết.
  - `private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)` & `private void TaiHoaDonTheoNgay(int rowIndex)` *(Hàm mới)*: Khi bấm vào một dòng `Ngày` ở bảng bên trái, truy vấn toàn bộ các hóa đơn đã thanh toán trong ngày đó đổ sang bảng `dgvHoaDonNgay` bên phải.
  - `private void dgvHoaDonNgay_CellClick(object sender, DataGridViewCellEventArgs e)` & `private void TaiChiTietHoaDon(int rowIndex)` *(Hàm mới)*: Khi bấm vào một hóa đơn trong bảng `dgvHoaDonNgay`, truy vấn bảng `ChiTietHoaDon JOIN Mon` để hiển thị chi tiết từng món ăn/đồ uống của hóa đơn đó xuống bảng `dgvChiTietThongKe`.
