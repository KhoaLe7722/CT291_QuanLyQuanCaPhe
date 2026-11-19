using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCaPhe
{
    public partial class fHoaDon : Form
    {
        // Lưu thông tin hóa đơn đang được chọn để in
        private int maHoaDonDangChon = -1;
        private string tenBanDangChon = "";
        private string tenNhanVienDangChon = "";
        private DateTime ngayVaoDangChon = DateTime.Now;
        private DateTime ngayThanhToanDangChon = DateTime.Now;
        private decimal tongTienDangChon = 0;
        private DataTable dtChiTietIn = null;

        public fHoaDon()
        {
            InitializeComponent();
        }

        private void fHoaDon_Load(object sender, EventArgs e)
        {
            // Mặc định lọc từ đầu tháng đến ngày hiện tại
            DateTime now = DateTime.Now;
            dtpTu.Value = new DateTime(now.Year, now.Month, 1);
            dtpDen.Value = now;

            TaiDanhSachHoaDon();
        }

        /// <summary>
        /// Tải danh sách các hóa đơn đã thanh toán trong khoảng thời gian từ dtpTu đến dtpDen
        /// </summary>
        private void TaiDanhSachHoaDon()
        {
            try
            {
                DateTime tuNgay = dtpTu.Value.Date;
                DateTime denNgay = dtpDen.Value.Date;

                if (tuNgay > denNgay)
                {
                    MessageBox.Show("Từ ngày không thể lớn hơn đến ngày!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy các hóa đơn đã thanh toán (ngay_thanh_toan IS NOT NULL)
                // DATEADD(DAY, 1, @denNgay) để lấy trọn vẹn cả ngày kết thúc (23:59:59)
                string sql = @"SELECT h.ma_hoa_don AS [Mã HĐ], 
                                      b.ten_ban AS [Bàn], 
                                      t.ten_hien_thi AS [Nhân viên], 
                                      h.ngay_vao AS [Ngày vào], 
                                      h.ngay_thanh_toan AS [Ngày thanh toán], 
                                      h.tong_tien AS [Tổng tiền]
                               FROM HoaDon h
                               JOIN Ban b ON h.ma_ban = b.ma_ban
                               JOIN TaiKhoan t ON h.tai_khoan = t.tai_khoan
                               WHERE h.ngay_thanh_toan IS NOT NULL
                                 AND h.ngay_thanh_toan >= @tuNgay 
                                 AND h.ngay_thanh_toan < DATEADD(DAY, 1, @denNgay)
                               ORDER BY h.ngay_thanh_toan DESC";

                DataTable dt = KetNoi.LayBang(sql, 
                    KetNoi.P("@tuNgay", tuNgay), 
                    KetNoi.P("@denNgay", denNgay));

                dgvHoaDon.DataSource = dt;

                // Định dạng hiển thị các cột
                if (dgvHoaDon.Columns["Tổng tiền"] != null)
                    dgvHoaDon.Columns["Tổng tiền"].DefaultCellStyle.Format = "#,##0";
                if (dgvHoaDon.Columns["Ngày vào"] != null)
                    dgvHoaDon.Columns["Ngày vào"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                if (dgvHoaDon.Columns["Ngày thanh toán"] != null)
                    dgvHoaDon.Columns["Ngày thanh toán"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

                // Nếu có dữ liệu, chọn mặc định dòng đầu tiên và nạp chi tiết
                if (dgvHoaDon.Rows.Count > 0)
                {
                    dgvHoaDon.Rows[0].Selected = true;
                    HienThiChiTietDong(0);
                }
                else
                {
                    maHoaDonDangChon = -1;
                    dgvChiTiet.DataSource = null;
                    label3.Text = "Tổng: 0 VNĐ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Nút Lọc hóa đơn theo khoảng thời gian
        /// </summary>
        private void btnLoc_Click(object sender, EventArgs e)
        {
            TaiDanhSachHoaDon();
        }

        /// <summary>
        /// Khi click chọn một hóa đơn trong dgvHoaDon -> Hiển thị chi tiết các món
        /// </summary>
        private void dgvHoaDon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvHoaDon.Rows.Count)
            {
                HienThiChiTietDong(e.RowIndex);
            }
        }

        /// <summary>
        /// Hiển thị chi tiết hóa đơn theo chỉ số dòng trong dgvHoaDon
        /// </summary>
        private void HienThiChiTietDong(int rowIndex)
        {
            try
            {
                DataGridViewRow row = dgvHoaDon.Rows[rowIndex];
                if (row.Cells["Mã HĐ"].Value == null || row.Cells["Mã HĐ"].Value == DBNull.Value)
                    return;

                maHoaDonDangChon = Convert.ToInt32(row.Cells["Mã HĐ"].Value);
                tenBanDangChon = row.Cells["Bàn"].Value?.ToString() ?? "";
                tenNhanVienDangChon = row.Cells["Nhân viên"].Value?.ToString() ?? "";
                ngayVaoDangChon = Convert.ToDateTime(row.Cells["Ngày vào"].Value);
                ngayThanhToanDangChon = Convert.ToDateTime(row.Cells["Ngày thanh toán"].Value);
                tongTienDangChon = Convert.ToDecimal(row.Cells["Tổng tiền"].Value ?? 0);

                // Lấy chi tiết món từ ChiTietHoaDon
                string sql = @"SELECT m.ten_mon AS [Tên món], 
                                      c.so_luong AS [Số lượng], 
                                      c.don_gia AS [Đơn giá], 
                                      (c.so_luong * c.don_gia) AS [Thành tiền]
                               FROM ChiTietHoaDon c
                               JOIN Mon m ON c.ma_mon = m.ma_mon
                               WHERE c.ma_hoa_don = @maHD";

                dtChiTietIn = KetNoi.LayBang(sql, KetNoi.P("@maHD", maHoaDonDangChon));
                dgvChiTiet.DataSource = dtChiTietIn;

                // Định dạng hiển thị
                if (dgvChiTiet.Columns["Đơn giá"] != null)
                    dgvChiTiet.Columns["Đơn giá"].DefaultCellStyle.Format = "#,##0";
                if (dgvChiTiet.Columns["Thành tiền"] != null)
                    dgvChiTiet.Columns["Thành tiền"].DefaultCellStyle.Format = "#,##0";

                label3.Text = $"Tổng: {tongTienDangChon:#,##0} VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị chi tiết hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// In hóa đơn: Sử dụng PrintDocument và PrintPreviewDialog có sẵn trong .NET
        /// </summary>
        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            if (maHoaDonDangChon <= 0 || dtChiTietIn == null)
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                PrintDocument printDoc = new PrintDocument();
                int soDongMon = dtChiTietIn != null ? dtChiTietIn.Rows.Count : 1;
                int chieuCaoGiay = Math.Max(680, 480 + soDongMon * 40);
                printDoc.DefaultPageSettings.PaperSize = new PaperSize("HoaDonCaPhe", 720, chieuCaoGiay);
                printDoc.PrintPage += PrintDoc_PrintPage;

                PrintPreviewDialog previewDlg = new PrintPreviewDialog();
                previewDlg.Document = printDoc;
                previewDlg.Width = 1000;
                previewDlg.Height = 780;
                previewDlg.StartPosition = FormStartPosition.CenterParent;
                previewDlg.Text = $"Xem trước Hóa đơn #{maHoaDonDangChon}";
                previewDlg.PrintPreviewControl.AutoZoom = true;

                previewDlg.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở giao diện in hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Thiết kế nội dung trang in hóa đơn to, rõ nét
        /// </summary>
        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            Font fontTieuDe = new Font("Arial", 22, FontStyle.Bold);
            Font fontPhu = new Font("Arial", 18, FontStyle.Bold);
            Font fontDam = new Font("Arial", 13, FontStyle.Bold);
            Font fontThuong = new Font("Arial", 13, FontStyle.Regular);
            Font fontNho = new Font("Arial", 11, FontStyle.Italic);
            Font fontTongTien = new Font("Arial", 16, FontStyle.Bold);

            Brush brush = Brushes.Black;
            Pen penNhat = new Pen(Color.Black, 1.5f);
            Pen penDam = new Pen(Color.Black, 2.5f);

            float x = 40;
            float y = 35;
            float chieuRong = 640;

            StringFormat formatGiua = new StringFormat { Alignment = StringAlignment.Center };
            StringFormat formatPhai = new StringFormat { Alignment = StringAlignment.Far };

            // 1. Tiêu đề quán
            g.DrawString("QUÁN CÀ PHÊ NHÓM 2", fontTieuDe, brush, new RectangleF(x, y, chieuRong, 38), formatGiua);
            y += 38;
            g.DrawString("Địa chỉ: Khu II, ĐH Cần Thơ, Đường 3/2, Ninh Kiều, Cần Thơ", fontNho, brush, new RectangleF(x, y, chieuRong, 24), formatGiua);
            y += 24;
            g.DrawString("Hotline: 0123 456 789", fontNho, brush, new RectangleF(x, y, chieuRong, 24), formatGiua);
            y += 34;

            // 2. Tên phiếu
            g.DrawString("HÓA ĐƠN THANH TOÁN", fontPhu, brush, new RectangleF(x, y, chieuRong, 32), formatGiua);
            y += 38;

            // Dòng kẻ ngang
            g.DrawLine(penNhat, x, y, x + chieuRong, y);
            y += 15;

            // 3. Thông tin chung
            g.DrawString($"Mã hóa đơn: #{maHoaDonDangChon}", fontDam, brush, x, y);
            g.DrawString($"Bàn: {tenBanDangChon}", fontDam, brush, x + 360, y);
            y += 28;
            g.DrawString($"Thu ngân: {tenNhanVienDangChon}", fontThuong, brush, x, y);
            y += 28;
            g.DrawString($"Giờ vào: {ngayVaoDangChon:dd/MM/yyyy HH:mm}", fontThuong, brush, x, y);
            y += 28;
            g.DrawString($"Giờ thanh toán: {ngayThanhToanDangChon:dd/MM/yyyy HH:mm}", fontThuong, brush, x, y);
            y += 32;

            // Dòng kẻ ngang phân cách bảng
            g.DrawLine(penDam, x, y, x + chieuRong, y);
            y += 12;

            // 4. Tiêu đề cột
            g.DrawString("Tên món", fontDam, brush, x, y);
            g.DrawString("SL", fontDam, brush, new RectangleF(x + 280, y, 60, 26), formatGiua);
            g.DrawString("Đơn giá", fontDam, brush, new RectangleF(x + 350, y, 130, 26), formatPhai);
            g.DrawString("Thành tiền", fontDam, brush, new RectangleF(x + 490, y, 150, 26), formatPhai);
            y += 28;

            g.DrawLine(penNhat, x, y, x + chieuRong, y);
            y += 14;

            // 5. Danh sách các món
            if (dtChiTietIn != null)
            {
                foreach (DataRow r in dtChiTietIn.Rows)
                {
                    string tenMon = r["Tên món"].ToString();
                    int sl = Convert.ToInt32(r["Số lượng"]);
                    decimal donGia = Convert.ToDecimal(r["Đơn giá"]);
                    decimal thanhTien = Convert.ToDecimal(r["Thành tiền"]);

                    g.DrawString(tenMon, fontThuong, brush, x, y);
                    g.DrawString(sl.ToString(), fontThuong, brush, new RectangleF(x + 280, y, 60, 26), formatGiua);
                    g.DrawString(donGia.ToString("#,##0"), fontThuong, brush, new RectangleF(x + 350, y, 130, 26), formatPhai);
                    g.DrawString(thanhTien.ToString("#,##0"), fontDam, brush, new RectangleF(x + 490, y, 150, 26), formatPhai);

                    y += 32;
                }
            }

            y += 6;
            // Dòng kẻ đậm kết thúc bảng
            g.DrawLine(penDam, x, y, x + chieuRong, y);
            y += 18;

            // 6. Tổng tiền thanh toán
            g.DrawString("TỔNG TIỀN PHẢI TRẢ:", fontTongTien, brush, x, y);
            g.DrawString($"{tongTienDangChon:#,##0} VNĐ", fontTongTien, brush, new RectangleF(x + 320, y, 320, 32), formatPhai);
            y += 45;

            // 7. Lời chào cuối hóa đơn
            g.DrawString("Cảm ơn quý khách và hẹn gặp lại!", fontNho, brush, new RectangleF(x, y, chieuRong, 26), formatGiua);
        }
    }
}
