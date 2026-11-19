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
    public partial class fThanhToan : Form
    {
        private int maBan = -1;
        private int maHoaDon = -1;
        private decimal tongTien = 0;

        // Thuộc tính public để tương thích nếu fBanHang của nhóm truyền dữ liệu qua Property
        public int MaBan { get { return maBan; } set { maBan = value; } }
        public int MaHoaDon { get { return maHoaDon; } set { maHoaDon = value; } }

        // Thông tin phục vụ hiển thị / in phiếu hóa đơn ngay sau khi thanh toán
        private string tenBan = "";
        private string tenNhanVien = "";
        private DateTime ngayVao = DateTime.Now;
        private DateTime ngayThanhToan = DateTime.Now;
        private DataTable dtChiTiet = null;

        // Constructor mặc định (dùng khi mở độc lập hoặc test)
        public fThanhToan()
        {
            InitializeComponent();
        }

        // Constructor nhận mã bàn (fBanHang truyền sang)
        public fThanhToan(int maBan) : this()
        {
            this.maBan = maBan;
        }

        // Constructor nhận cả mã bàn và mã hóa đơn (nếu có sẵn)
        public fThanhToan(int maBan, int maHoaDon) : this()
        {
            this.maBan = maBan;
            this.maHoaDon = maHoaDon;
        }

        private void fThanhToan_Load(object sender, EventArgs e)
        {
            TaiThongTinThanhToan();
        }

        /// <summary>
        /// Tải thông tin bàn, hóa đơn chưa thanh toán và chi tiết các món
        /// </summary>
        private void TaiThongTinThanhToan()
        {
            try
            {
                // 1. Nếu truyền 1 tham số vào fThanhToan(id): Tự động tìm theo ma_ban trước, nếu không có thì tìm theo ma_hoa_don
                if (maHoaDon <= 0 && maBan > 0)
                {
                    string sqlTimTheoBan = "SELECT TOP 1 ma_hoa_don FROM HoaDon WHERE ma_ban = @id AND ngay_thanh_toan IS NULL ORDER BY ma_hoa_don DESC";
                    object result = KetNoi.LayGiaTri(sqlTimTheoBan, KetNoi.P("@id", maBan));
                    if (result != null && result != DBNull.Value)
                    {
                        maHoaDon = Convert.ToInt32(result);
                    }
                    else
                    {
                        // Trường hợp người gọi truyền maHoaDon vào constructor 1 tham số fThanhToan(maHoaDon)
                        string sqlTimTheoHD = "SELECT TOP 1 ma_hoa_don FROM HoaDon WHERE ma_hoa_don = @id AND ngay_thanh_toan IS NULL";
                        object resHD = KetNoi.LayGiaTri(sqlTimTheoHD, KetNoi.P("@id", maBan));
                        if (resHD != null && resHD != DBNull.Value)
                        {
                            maHoaDon = Convert.ToInt32(resHD);
                        }
                    }
                }

                // 2. Nếu fBanHang của nhóm chỉ gọi new fThanhToan() không truyền tham số -> Tự lấy hóa đơn chưa thanh toán mới nhất
                if (maHoaDon <= 0)
                {
                    string sqlTimMoiNhat = "SELECT TOP 1 ma_hoa_don FROM HoaDon WHERE ngay_thanh_toan IS NULL ORDER BY ma_hoa_don DESC";
                    object resLatest = KetNoi.LayGiaTri(sqlTimMoiNhat);
                    if (resLatest != null && resLatest != DBNull.Value)
                    {
                        maHoaDon = Convert.ToInt32(resLatest);
                    }
                }

                // Nếu vẫn chưa tìm thấy hóa đơn chưa thanh toán nào
                if (maHoaDon <= 0)
                {
                    MessageBox.Show("Hiện không có hóa đơn nào đang chờ thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                // 1. Lấy thông tin chung của hóa đơn: mã bàn, tên bàn, tên nhân viên, ngày vào
                string sqlHD = @"SELECT h.ma_hoa_don, b.ma_ban, b.ten_ban, t.ten_hien_thi, h.ngay_vao 
                                 FROM HoaDon h
                                 JOIN Ban b ON h.ma_ban = b.ma_ban
                                 JOIN TaiKhoan t ON h.tai_khoan = t.tai_khoan
                                 WHERE h.ma_hoa_don = @maHD";
                DataTable dtHD = KetNoi.LayBang(sqlHD, KetNoi.P("@maHD", maHoaDon));

                if (dtHD.Rows.Count > 0)
                {
                    DataRow r = dtHD.Rows[0];
                    this.maBan = Convert.ToInt32(r["ma_ban"]);
                    this.tenBan = r["ten_ban"].ToString();
                    this.tenNhanVien = r["ten_hien_thi"].ToString();
                    this.ngayVao = Convert.ToDateTime(r["ngay_vao"]);

                    label1.Text = $"Số hóa đơn: {maHoaDon}";
                    label2.Text = $"Bàn: {tenBan}";
                    label3.Text = $"Nhân viên: {tenNhanVien}";
                    label4.Text = $"Ngày vào: {ngayVao:dd/MM/yyyy HH:mm}";
                }

                // 2. Lấy danh sách chi tiết các món trong hóa đơn
                string sqlCT = @"SELECT m.ten_mon AS [Tên món], 
                                        c.so_luong AS [Số lượng], 
                                        c.don_gia AS [Đơn giá], 
                                        (c.so_luong * c.don_gia) AS [Thành tiền]
                                 FROM ChiTietHoaDon c
                                 JOIN Mon m ON c.ma_mon = m.ma_mon
                                 WHERE c.ma_hoa_don = @maHD";
                dtChiTiet = KetNoi.LayBang(sqlCT, KetNoi.P("@maHD", maHoaDon));
                dataGridView1.DataSource = dtChiTiet;

                // Định dạng hiển thị các cột số tiền
                if (dataGridView1.Columns["Đơn giá"] != null)
                    dataGridView1.Columns["Đơn giá"].DefaultCellStyle.Format = "#,##0";
                if (dataGridView1.Columns["Thành tiền"] != null)
                    dataGridView1.Columns["Thành tiền"].DefaultCellStyle.Format = "#,##0";

                // 3. Tính tổng tiền
                tongTien = 0;
                foreach (DataRow row in dtChiTiet.Rows)
                {
                    tongTien += Convert.ToDecimal(row["Thành tiền"]);
                }
                label5.Text = $"Tổng tiền: {tongTien:#,##0} VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xác nhận thanh toán: Chốt hóa đơn, trả bàn về trạng thái trống và hiện kèm hóa đơn luôn
        /// </summary>
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (maHoaDon <= 0)
            {
                MessageBox.Show("Không có hóa đơn hợp lệ để thanh toán!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show(
                $"Xác nhận thanh toán hóa đơn #{maHoaDon}?\nTổng số tiền: {tongTien:#,##0} VNĐ", 
                "Xác nhận thanh toán", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                try
                {
                    ngayThanhToan = DateTime.Now;

                    // 1. Chốt hóa đơn: Cập nhật ngay_thanh_toan và tong_tien
                    string sqlCapNhatHD = @"UPDATE HoaDon 
                                            SET ngay_thanh_toan = @ngayTT, tong_tien = @tongTien 
                                            WHERE ma_hoa_don = @maHD";
                    KetNoi.ChayLenh(sqlCapNhatHD, 
                        KetNoi.P("@ngayTT", ngayThanhToan),
                        KetNoi.P("@tongTien", tongTien), 
                        KetNoi.P("@maHD", maHoaDon));

                    // 2. Trả bàn về trạng thái trống (trong CSDL là N'Đã thanh toán')
                    string sqlCapNhatBan = @"UPDATE Ban 
                                             SET trang_thai = N'Đã thanh toán' 
                                             WHERE ma_ban = @maBan";
                    KetNoi.ChayLenh(sqlCapNhatBan, KetNoi.P("@maBan", maBan));

                    // 3. Thông báo thành công và hiện kèm phiếu hóa đơn luôn
                    MessageBox.Show("Thanh toán thành công! Nhấn OK để xem và in phiếu hóa đơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    HienThiPhieuHoaDon();

                    // Trả kết quả DialogResult.OK để fBanHang biết và load lại bàn
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi trong quá trình thanh toán: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Hiển thị phiếu hóa đơn ngay sau khi thanh toán thành công
        /// </summary>
        private void HienThiPhieuHoaDon()
        {
            try
            {
                PrintDocument printDoc = new PrintDocument();
                int soDongMon = dtChiTiet != null ? dtChiTiet.Rows.Count : 1;
                int chieuCaoGiay = Math.Max(680, 480 + soDongMon * 40);
                printDoc.DefaultPageSettings.PaperSize = new PaperSize("HoaDonCaPhe", 720, chieuCaoGiay);
                printDoc.PrintPage += PrintDoc_PrintPage;

                PrintPreviewDialog previewDlg = new PrintPreviewDialog();
                previewDlg.Document = printDoc;
                previewDlg.Width = 1000;
                previewDlg.Height = 780;
                previewDlg.StartPosition = FormStartPosition.CenterScreen;
                previewDlg.Text = $"Hóa đơn thanh toán #{maHoaDon} - {tenBan}";
                previewDlg.PrintPreviewControl.AutoZoom = true;

                previewDlg.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị phiếu hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

            g.DrawLine(penNhat, x, y, x + chieuRong, y);
            y += 15;

            // 3. Thông tin chung
            g.DrawString($"Mã hóa đơn: #{maHoaDon}", fontDam, brush, x, y);
            g.DrawString($"Bàn: {tenBan}", fontDam, brush, x + 360, y);
            y += 28;
            g.DrawString($"Thu ngân: {tenNhanVien}", fontThuong, brush, x, y);
            y += 28;
            g.DrawString($"Giờ vào: {ngayVao:dd/MM/yyyy HH:mm}", fontThuong, brush, x, y);
            y += 28;
            g.DrawString($"Giờ thanh toán: {ngayThanhToan:dd/MM/yyyy HH:mm}", fontThuong, brush, x, y);
            y += 32;

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
            if (dtChiTiet != null)
            {
                foreach (DataRow r in dtChiTiet.Rows)
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
            g.DrawLine(penDam, x, y, x + chieuRong, y);
            y += 18;

            // 6. Tổng tiền thanh toán
            g.DrawString("TỔNG TIỀN PHẢI TRẢ:", fontTongTien, brush, x, y);
            g.DrawString($"{tongTien:#,##0} VNĐ", fontTongTien, brush, new RectangleF(x + 320, y, 320, 32), formatPhai);
            y += 45;

            // 7. Lời chào cuối hóa đơn
            g.DrawString("Cảm ơn quý khách và hẹn gặp lại!", fontNho, brush, new RectangleF(x, y, chieuRong, 26), formatGiua);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e) { }
    }
}
