using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCaPhe
{
    public partial class fThongKe : Form
    {
        public fThongKe()
        {
            InitializeComponent();
        }

        private void fThongKe_Load(object sender, EventArgs e)
        {
            // Thiết lập giá trị mặc định cho ngày
            DateTime now = DateTime.Now;
            dateTimePicker1.Value = new DateTime(now.Year, now.Month, 1);
            dateTimePicker2.Value = now;

            // Nạp danh sách tháng (1 -> 12)
            comboBox1.Items.Clear();
            for (int i = 1; i <= 12; i++)
            {
                comboBox1.Items.Add(i);
            }
            comboBox1.SelectedItem = now.Month;

            // Nạp danh sách năm (2020 -> năm hiện tại + 2)
            comboBox2.Items.Clear();
            for (int y = 2020; y <= now.Year + 2; y++)
            {
                comboBox2.Items.Add(y);
            }
            comboBox2.SelectedItem = now.Year;

            CapNhatTrangThaiControls();
            ThongKeDoanhThu();
        }

        private void rdoTheoNgay_CheckedChanged(object sender, EventArgs e)
        {
            CapNhatTrangThaiControls();
        }

        private void rdoTheoThang_CheckedChanged(object sender, EventArgs e)
        {
            CapNhatTrangThaiControls();
        }

        /// <summary>
        /// Bật/tắt các điều khiển tương ứng khi chọn Thống kê theo ngày hoặc theo tháng
        /// </summary>
        private void CapNhatTrangThaiControls()
        {
            bool theoNgay = radioButton1.Checked;

            dateTimePicker1.Enabled = theoNgay;
            dateTimePicker2.Enabled = theoNgay;

            comboBox1.Enabled = !theoNgay;
            comboBox2.Enabled = !theoNgay;
        }

        /// <summary>
        /// Nút Xem thống kê
        /// </summary>
        private void btnXemThongKe_Click(object sender, EventArgs e)
        {
            ThongKeDoanhThu();
        }

        /// <summary>
        /// Thực hiện thống kê doanh thu theo ngày hoặc theo tháng
        /// </summary>
        private void ThongKeDoanhThu()
        {
            try
            {
                DataTable dt;
                decimal tongDoanhThu = 0;

                if (radioButton1.Checked)
                {
                    // Thống kê theo ngày
                    DateTime tuNgay = dateTimePicker1.Value.Date;
                    DateTime denNgay = dateTimePicker2.Value.Date;

                    if (tuNgay > denNgay)
                    {
                        MessageBox.Show("Từ ngày không thể lớn hơn đến ngày!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string sql = @"SELECT CONVERT(VARCHAR(10), h.ngay_thanh_toan, 103) AS [Ngày],
                                          COUNT(h.ma_hoa_don) AS [Số hóa đơn],
                                          SUM(h.tong_tien) AS [Doanh thu]
                                   FROM HoaDon h
                                   WHERE h.ngay_thanh_toan IS NOT NULL
                                     AND h.ngay_thanh_toan >= @tuNgay 
                                     AND h.ngay_thanh_toan < DATEADD(DAY, 1, @denNgay)
                                   GROUP BY CONVERT(VARCHAR(10), h.ngay_thanh_toan, 103), CAST(h.ngay_thanh_toan AS DATE)
                                   ORDER BY CAST(h.ngay_thanh_toan AS DATE) DESC";

                    dt = KetNoi.LayBang(sql, KetNoi.P("@tuNgay", tuNgay), KetNoi.P("@denNgay", denNgay));
                }
                else
                {
                    // Thống kê theo tháng
                    if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
                        return;

                    int thang = Convert.ToInt32(comboBox1.SelectedItem);
                    int nam = Convert.ToInt32(comboBox2.SelectedItem);

                    string sql = @"SELECT CONVERT(VARCHAR(10), h.ngay_thanh_toan, 103) AS [Ngày],
                                          COUNT(h.ma_hoa_don) AS [Số hóa đơn],
                                          SUM(h.tong_tien) AS [Doanh thu]
                                   FROM HoaDon h
                                   WHERE h.ngay_thanh_toan IS NOT NULL
                                     AND MONTH(h.ngay_thanh_toan) = @thang
                                     AND YEAR(h.ngay_thanh_toan) = @nam
                                   GROUP BY CONVERT(VARCHAR(10), h.ngay_thanh_toan, 103), CAST(h.ngay_thanh_toan AS DATE)
                                   ORDER BY CAST(h.ngay_thanh_toan AS DATE) DESC";

                    dt = KetNoi.LayBang(sql, KetNoi.P("@thang", thang), KetNoi.P("@nam", nam));
                }

                dataGridView1.DataSource = dt;

                if (dataGridView1.Columns["Doanh thu"] != null)
                    dataGridView1.Columns["Doanh thu"].DefaultCellStyle.Format = "#,##0";

                foreach (DataRow r in dt.Rows)
                {
                    if (r["Doanh thu"] != DBNull.Value)
                        tongDoanhThu += Convert.ToDecimal(r["Doanh thu"]);
                }

                label5.Text = $"Tổng doanh thu: {tongDoanhThu:#,##0} VNĐ";

                // Tự động chọn dòng ngày đầu tiên để hiện danh sách hóa đơn và chi tiết
                if (dataGridView1.Rows.Count > 0)
                {
                    dataGridView1.Rows[0].Selected = true;
                    TaiHoaDonTheoNgay(0);
                }
                else
                {
                    dgvHoaDonNgay.DataSource = null;
                    dgvChiTietThongKe.DataSource = null;
                    lblTieuDeHD.Text = "🧾 CÁC HÓA ĐƠN TRONG NGÀY ĐANG CHỌN:";
                    lblTieuDeCT.Text = "☕ CHI TIẾT MÓN CỦA HÓA ĐƠN:";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thống kê doanh thu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Khi bấm vào một dòng Ngày bên bảng thống kê -> Nạp danh sách các hóa đơn của ngày đó
        /// </summary>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView1.Rows.Count)
            {
                TaiHoaDonTheoNgay(e.RowIndex);
            }
        }

        private void TaiHoaDonTheoNgay(int rowIndex)
        {
            try
            {
                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                if (row.Cells["Ngày"].Value == null) return;

                string chuoiNgay = row.Cells["Ngày"].Value.ToString();
                if (!DateTime.TryParseExact(chuoiNgay, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ngayChon))
                    return;

                lblTieuDeHD.Text = $"🧾 DANH SÁCH HÓA ĐƠN NGÀY {chuoiNgay}:";

                string sql = @"SELECT h.ma_hoa_don AS [Mã HĐ], 
                                      b.ten_ban AS [Bàn], 
                                      t.ten_hien_thi AS [Nhân viên], 
                                      FORMAT(h.ngay_thanh_toan, 'HH:mm:ss') AS [Giờ thanh toán], 
                                      h.tong_tien AS [Tổng tiền]
                               FROM HoaDon h
                               JOIN Ban b ON h.ma_ban = b.ma_ban
                               JOIN TaiKhoan t ON h.tai_khoan = t.tai_khoan
                               WHERE h.ngay_thanh_toan IS NOT NULL
                                 AND CAST(h.ngay_thanh_toan AS DATE) = @ngay
                               ORDER BY h.ma_hoa_don DESC";

                DataTable dtHD = KetNoi.LayBang(sql, KetNoi.P("@ngay", ngayChon.Date));
                dgvHoaDonNgay.DataSource = dtHD;

                if (dgvHoaDonNgay.Columns["Tổng tiền"] != null)
                    dgvHoaDonNgay.Columns["Tổng tiền"].DefaultCellStyle.Format = "#,##0";

                if (dgvHoaDonNgay.Rows.Count > 0)
                {
                    dgvHoaDonNgay.Rows[0].Selected = true;
                    TaiChiTietHoaDon(0);
                }
                else
                {
                    dgvChiTietThongKe.DataSource = null;
                    lblTieuDeCT.Text = "☕ CHI TIẾT MÓN CỦA HÓA ĐƠN:";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải hóa đơn theo ngày: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Khi bấm vào một Hóa đơn trong ngày -> Hiện chi tiết các món đã gọi trong hóa đơn đó
        /// </summary>
        private void dgvHoaDonNgay_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvHoaDonNgay.Rows.Count)
            {
                TaiChiTietHoaDon(e.RowIndex);
            }
        }

        private void TaiChiTietHoaDon(int rowIndex)
        {
            try
            {
                DataGridViewRow row = dgvHoaDonNgay.Rows[rowIndex];
                if (row.Cells["Mã HĐ"].Value == null) return;

                int maHD = Convert.ToInt32(row.Cells["Mã HĐ"].Value);
                string tenBan = row.Cells["Bàn"].Value?.ToString() ?? "";
                decimal tongTien = Convert.ToDecimal(row.Cells["Tổng tiền"].Value ?? 0);

                lblTieuDeCT.Text = $"☕ CHI TIẾT HÓA ĐƠN #{maHD} ({tenBan} - Tổng: {tongTien:#,##0} VNĐ):";

                string sql = @"SELECT m.ten_mon AS [Tên món], 
                                      c.so_luong AS [Số lượng], 
                                      c.don_gia AS [Đơn giá], 
                                      (c.so_luong * c.don_gia) AS [Thành tiền]
                               FROM ChiTietHoaDon c
                               JOIN Mon m ON c.ma_mon = m.ma_mon
                               WHERE c.ma_hoa_don = @maHD";

                DataTable dtCT = KetNoi.LayBang(sql, KetNoi.P("@maHD", maHD));
                dgvChiTietThongKe.DataSource = dtCT;

                if (dgvChiTietThongKe.Columns["Đơn giá"] != null)
                    dgvChiTietThongKe.Columns["Đơn giá"].DefaultCellStyle.Format = "#,##0";
                if (dgvChiTietThongKe.Columns["Thành tiền"] != null)
                    dgvChiTietThongKe.Columns["Thành tiền"].DefaultCellStyle.Format = "#,##0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải chi tiết hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
