using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCaPhe
{
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            this.Text = $"Quản Lý Quán Cà Phê - [{PhienDangNhap.TenHienThi}]";
            lblBannerSub.Text = $"👤 Người dùng: {PhienDangNhap.TenHienThi}  |  Quyền hạn: {(PhienDangNhap.IdRole == 1 ? "Quản trị viên" : "Nhân viên")}";

            // Khởi động đồng hồ thời gian thực
            timerClock_Tick(null, null);
            timerClock.Start();

            // Phân quyền: Nhân viên (IdRole = 2) không thấy Quản lý món và Thống kê
            if (PhienDangNhap.IdRole == 2)
            {
                quảnLýMónToolStripMenuItem.Visible = false;
                thốngKêToolStripMenuItem.Visible = false;

                btnQuickQuanLyMon.Visible = false;
                btnQuickThongKe.Visible = false;

                // Căn chỉnh 2 nút còn lại (Bán hàng & Hóa đơn) rộng ra và đẹp hơn cho nhân viên
                btnQuickBanHang.Location = new Point(30, 360);
                btnQuickBanHang.Size = new Size(450, 130);

                btnQuickHoaDon.Location = new Point(510, 360);
                btnQuickHoaDon.Size = new Size(450, 130);
            }
            else
            {
                quảnLýMónToolStripMenuItem.Visible = true;
                thốngKêToolStripMenuItem.Visible = true;
                btnQuickQuanLyMon.Visible = true;
                btnQuickThongKe.Visible = true;
            }

            CapNhatDashboard();
        }

        /// <summary>
        /// Cập nhật số liệu thống kê nhanh trên Dashboard từ CSDL
        /// </summary>
        public void CapNhatDashboard()
        {
            try
            {
                // 1. Tình trạng bàn: Đang phục vụ / Tổng số bàn
                string sqlBanPhucVu = "SELECT COUNT(*) FROM Ban WHERE trang_thai = N'Chưa thanh toán'";
                string sqlTongBan = "SELECT COUNT(*) FROM Ban";
                int soBanPhucVu = Convert.ToInt32(KetNoi.LayGiaTri(sqlBanPhucVu) ?? 0);
                int tongBan = Convert.ToInt32(KetNoi.LayGiaTri(sqlTongBan) ?? 10);
                int soBanTrong = tongBan - soBanPhucVu;

                lblSoBanPhucVu.Text = $"{soBanPhucVu} / {tongBan} bàn";
                lblSoBanTrong.Text = $"{soBanTrong} bàn đang còn trống";

                // 2. Số hóa đơn thanh toán hôm nay
                string sqlSoHD = @"SELECT COUNT(*) FROM HoaDon 
                                   WHERE ngay_thanh_toan >= CAST(GETDATE() AS DATE) 
                                     AND ngay_thanh_toan < DATEADD(DAY, 1, CAST(GETDATE() AS DATE))";
                int soHD = Convert.ToInt32(KetNoi.LayGiaTri(sqlSoHD) ?? 0);
                lblSoHoaDonHomNay.Text = $"{soHD} Hóa đơn";

                // 3. Doanh thu hôm nay
                string sqlDoanhThu = @"SELECT ISNULL(SUM(tong_tien), 0) FROM HoaDon 
                                       WHERE ngay_thanh_toan >= CAST(GETDATE() AS DATE) 
                                         AND ngay_thanh_toan < DATEADD(DAY, 1, CAST(GETDATE() AS DATE))";
                decimal doanhThu = Convert.ToDecimal(KetNoi.LayGiaTri(sqlDoanhThu) ?? 0);
                lblDoanhThuHomNay.Text = $"{doanhThu:#,##0} VNĐ";
            }
            catch
            {
                // Bỏ qua lỗi nếu mất kết nối tạm thời
            }
        }

        private void timerClock_Tick(object sender, EventArgs e)
        {
            lblClock.Text = $"🕒 {DateTime.Now:HH:mm:ss - dd/MM/yyyy}";
        }

        private void quảnLýMónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fQuanLyMon f = new fQuanLyMon();
            f.ShowDialog();
            CapNhatDashboard();
        }

        private void bánHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fBanHang f = new fBanHang();
            f.ShowDialog();
            CapNhatDashboard(); // Cập nhật lại số liệu bàn và doanh thu sau khi bán hàng
        }

        private void hóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fHoaDon f = new fHoaDon();
            f.ShowDialog();
            CapNhatDashboard();
        }

        private void thốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fThongKe f = new fThongKe();
            f.ShowDialog();
            CapNhatDashboard();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
