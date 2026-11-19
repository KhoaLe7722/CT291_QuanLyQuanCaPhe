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
    public partial class fBanHang : Form
    {
        private int maBanDangChon = -1;
        private string tenBanDangChon = "";
        private string trangThaiDangChon = "";
        private int maHoaDonHienTai = -1;
        private decimal tongTienHienTai = 0;

        public fBanHang()
        {
            InitializeComponent();
        }

        private void fBanHang_Load(object sender, EventArgs e)
        {
            TaiDanhSachBan();
            TaiDanhMucLoaiMon();
            TaiDanhSachMon();
        }

        /// <summary>
        /// Hiển thị danh sách tất cả các bàn lên FlowLayoutPanel dưới dạng Button màu sắc
        /// </summary>
        public void TaiDanhSachBan()
        {
            try
            {
                flpBan.Controls.Clear();
                string sql = "SELECT ma_ban, ten_ban, trang_thai FROM Ban ORDER BY ma_ban";
                DataTable dt = KetNoi.LayBang(sql);

                foreach (DataRow r in dt.Rows)
                {
                    int maBan = Convert.ToInt32(r["ma_ban"]);
                    string tenBan = r["ten_ban"].ToString();
                    string trangThai = r["trang_thai"].ToString();
                    string hienThiTrangThai = (trangThai == "Chưa thanh toán") ? "Chưa thanh toán" : "Bàn trống";

                    Button btn = new Button
                    {
                        Width = 140,
                        Height = 120,
                        Text = $"{tenBan}\n\n({hienThiTrangThai})",
                        Tag = r,
                        Font = new Font("Arial", 10.5F, FontStyle.Bold),
                        Margin = new Padding(6)
                    };

                    // Phân biệt màu sắc: Có khách = Màu cam/đỏ, Trống = Màu xanh lá
                    if (trangThai == "Chưa thanh toán")
                    {
                        btn.BackColor = Color.LightCoral;
                        btn.ForeColor = Color.DarkRed;
                    }
                    else
                    {
                        btn.BackColor = Color.LightGreen;
                        btn.ForeColor = Color.DarkGreen;
                    }

                    if (maBan == maBanDangChon)
                    {
                        btn.FlatStyle = FlatStyle.Flat;
                        btn.FlatAppearance.BorderColor = Color.Blue;
                        btn.FlatAppearance.BorderSize = 3;
                    }

                    btn.Click += BtnBan_Click;
                    flpBan.Controls.Add(btn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách bàn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Khi click chọn một bàn trong danh sách bàn
        /// </summary>
        private void BtnBan_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null || btn.Tag == null) return;

            foreach (Control ctrl in flpBan.Controls)
            {
                if (ctrl is Button b)
                {
                    if (b == btn)
                    {
                        b.FlatStyle = FlatStyle.Flat;
                        b.FlatAppearance.BorderColor = Color.Blue;
                        b.FlatAppearance.BorderSize = 3;
                    }
                    else
                    {
                        b.FlatStyle = FlatStyle.Standard;
                    }
                }
            }

            DataRow r = btn.Tag as DataRow;
            maBanDangChon = Convert.ToInt32(r["ma_ban"]);
            tenBanDangChon = r["ten_ban"].ToString();
            trangThaiDangChon = r["trang_thai"].ToString();

            label1.Text = $"Bàn đang chọn: {tenBanDangChon}";
            string hienThiTT = (trangThaiDangChon == "Chưa thanh toán") ? "Chưa thanh toán" : "Bàn trống";
            label2.Text = $"Trạng thái: {hienThiTT}";

            TaiHoaDonBanDangChon();
        }

        /// <summary>
        /// Tải hóa đơn và chi tiết các món của bàn đang chọn
        /// </summary>
        private void TaiHoaDonBanDangChon()
        {
            if (maBanDangChon <= 0) return;

            try
            {
                // Tìm hóa đơn chưa thanh toán
                string sqlTimHD = "SELECT TOP 1 ma_hoa_don FROM HoaDon WHERE ma_ban = @maBan AND ngay_thanh_toan IS NULL ORDER BY ma_hoa_don DESC";
                object res = KetNoi.LayGiaTri(sqlTimHD, KetNoi.P("@maBan", maBanDangChon));

                if (res != null && res != DBNull.Value)
                {
                    maHoaDonHienTai = Convert.ToInt32(res);
                    string sqlCT = @"SELECT m.ma_mon AS [Mã món], 
                                            m.ten_mon AS [Tên món], 
                                            c.so_luong AS [Số lượng], 
                                            c.don_gia AS [Đơn giá], 
                                            (c.so_luong * c.don_gia) AS [Thành tiền]
                                     FROM ChiTietHoaDon c
                                     JOIN Mon m ON c.ma_mon = m.ma_mon
                                     WHERE c.ma_hoa_don = @maHD";
                    DataTable dtCT = KetNoi.LayBang(sqlCT, KetNoi.P("@maHD", maHoaDonHienTai));
                    dataGridView1.DataSource = dtCT;

                    if (dataGridView1.Columns["Mã món"] != null)
                        dataGridView1.Columns["Mã món"].Visible = false;
                    if (dataGridView1.Columns["Đơn giá"] != null)
                        dataGridView1.Columns["Đơn giá"].DefaultCellStyle.Format = "#,##0";
                    if (dataGridView1.Columns["Thành tiền"] != null)
                        dataGridView1.Columns["Thành tiền"].DefaultCellStyle.Format = "#,##0";

                    tongTienHienTai = 0;
                    foreach (DataRow row in dtCT.Rows)
                        tongTienHienTai += Convert.ToDecimal(row["Thành tiền"]);

                    label8.Text = $"{tongTienHienTai:#,##0} VNĐ";
                }
                else
                {
                    maHoaDonHienTai = -1;
                    dataGridView1.DataSource = null;
                    tongTienHienTai = 0;
                    label8.Text = "0 VNĐ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp hóa đơn bàn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tải danh mục loại món vào ComboBox lọc
        /// </summary>
        private void TaiDanhMucLoaiMon()
        {
            try
            {
                string sql = "SELECT 0 AS ma_loai, N'-- Tất cả --' AS ten_loai UNION ALL SELECT ma_loai, ten_loai FROM LoaiMon";
                DataTable dt = KetNoi.LayBang(sql);
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "ten_loai";
                comboBox1.ValueMember = "ma_loai";
            }
            catch { }
        }

        /// <summary>
        /// Tải danh sách món để gọi món
        /// </summary>
        private void TaiDanhSachMon()
        {
            try
            {
                int maLoai = 0;
                if (comboBox1.SelectedValue != null && int.TryParse(comboBox1.SelectedValue.ToString(), out int val))
                    maLoai = val;

                string tuKhoa = txtTim.Text.Trim();

                string sql = "SELECT ma_mon AS [Mã món], ten_mon AS [Tên món], gia AS [Giá] FROM Mon WHERE 1=1";
                if (maLoai > 0)
                    sql += $" AND ma_loai = {maLoai}";
                if (!string.IsNullOrEmpty(tuKhoa))
                    sql += " AND ten_mon LIKE @tk";

                DataTable dt = KetNoi.LayBang(sql, KetNoi.P("@tk", "%" + tuKhoa + "%"));
                dgvDanhSachMon.DataSource = dt;

                if (dgvDanhSachMon.Columns["Mã món"] != null)
                    dgvDanhSachMon.Columns["Mã món"].Visible = false;
                if (dgvDanhSachMon.Columns["Giá"] != null)
                    dgvDanhSachMon.Columns["Giá"].DefaultCellStyle.Format = "#,##0";

                dgvDanhSachMon_SelectionChanged(null, null);
            }
            catch { }
        }

        /// <summary>
        /// Hiển thị hình ảnh của món ăn khi chọn dòng trên danh sách món
        /// </summary>
        private void dgvDanhSachMon_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvDanhSachMon.CurrentRow != null && dgvDanhSachMon.CurrentRow.Cells["Mã món"].Value != null)
                {
                    int maMon = Convert.ToInt32(dgvDanhSachMon.CurrentRow.Cells["Mã món"].Value);
                    string tenMon = dgvDanhSachMon.CurrentRow.Cells["Tên món"].Value?.ToString() ?? "";

                    picAnhMon.Image = QuanLyHinhAnh.LayAnhMon(maMon, tenMon);
                    lblTenAnhMon.Text = tenMon;
                }
                else
                {
                    picAnhMon.Image = null;
                    lblTenAnhMon.Text = "Hình ảnh món";
                }
            }
            catch { }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            TaiDanhSachMon();
        }

        private void txtTim_TextChanged(object sender, EventArgs e)
        {
            TaiDanhSachMon();
        }

        /// <summary>
        /// Nút Thêm món vào hóa đơn bàn đang chọn
        /// </summary>
        private void btnThemMon_Click(object sender, EventArgs e)
        {
            if (maBanDangChon <= 0)
            {
                MessageBox.Show("Vui lòng chọn một bàn trước khi thêm món!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvDanhSachMon.CurrentRow == null || dgvDanhSachMon.CurrentRow.Cells["Mã món"].Value == null)
            {
                MessageBox.Show("Vui lòng chọn món ăn cần thêm trong danh sách món!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maMon = Convert.ToInt32(dgvDanhSachMon.CurrentRow.Cells["Mã món"].Value);
            decimal donGia = Convert.ToDecimal(dgvDanhSachMon.CurrentRow.Cells["Giá"].Value);
            int soLuong = (int)numericUpDown1.Value;

            if (soLuong <= 0)
            {
                MessageBox.Show("Số lượng món phải lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 1. Nếu bàn chưa có hóa đơn đang mở -> Tạo hóa đơn mới
                if (maHoaDonHienTai <= 0)
                {
                    string sqlTaoHD = @"INSERT INTO HoaDon(ma_ban, tai_khoan, ngay_vao) 
                                        VALUES(@maBan, @tk, GETDATE());
                                        SELECT SCOPE_IDENTITY();";
                    object resHD = KetNoi.LayGiaTri(sqlTaoHD, 
                        KetNoi.P("@maBan", maBanDangChon),
                        KetNoi.P("@tk", string.IsNullOrEmpty(PhienDangNhap.TaiKhoan) ? "admin" : PhienDangNhap.TaiKhoan));

                    maHoaDonHienTai = Convert.ToInt32(resHD);

                    // Cập nhật bàn sang 'Chưa thanh toán'
                    KetNoi.ChayLenh("UPDATE Ban SET trang_thai = N'Chưa thanh toán' WHERE ma_ban = @maBan", KetNoi.P("@maBan", maBanDangChon));
                    trangThaiDangChon = "Chưa thanh toán";
                    label2.Text = "Trạng thái: Chưa thanh toán";
                }

                // 2. Thêm hoặc cộng dồn chi tiết món
                string sqlKiemTraMon = "SELECT COUNT(*) FROM ChiTietHoaDon WHERE ma_hoa_don = @maHD AND ma_mon = @maMon";
                int tonTai = Convert.ToInt32(KetNoi.LayGiaTri(sqlKiemTraMon, KetNoi.P("@maHD", maHoaDonHienTai), KetNoi.P("@maMon", maMon)));

                if (tonTai > 0)
                {
                    string sqlCongDon = "UPDATE ChiTietHoaDon SET so_luong = so_luong + @sl WHERE ma_hoa_don = @maHD AND ma_mon = @maMon";
                    KetNoi.ChayLenh(sqlCongDon, KetNoi.P("@sl", soLuong), KetNoi.P("@maHD", maHoaDonHienTai), KetNoi.P("@maMon", maMon));
                }
                else
                {
                    string sqlThemCT = "INSERT INTO ChiTietHoaDon(ma_hoa_don, ma_mon, so_luong, don_gia) VALUES(@maHD, @maMon, @sl, @gia)";
                    KetNoi.ChayLenh(sqlThemCT, 
                        KetNoi.P("@maHD", maHoaDonHienTai), 
                        KetNoi.P("@maMon", maMon), 
                        KetNoi.P("@sl", soLuong), 
                        KetNoi.P("@gia", donGia));
                }

                // Tải lại chi tiết và danh sách bàn
                TaiHoaDonBanDangChon();
                TaiDanhSachBan();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm món: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Nút Xóa món khỏi hóa đơn bàn
        /// </summary>
        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            if (maHoaDonHienTai <= 0 || dataGridView1.CurrentRow == null || dataGridView1.CurrentRow.Cells["Mã món"].Value == null)
            {
                MessageBox.Show("Vui lòng chọn món cần xóa trong bảng chi tiết bàn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maMon = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Mã món"].Value);
            string tenMon = dataGridView1.CurrentRow.Cells["Tên món"].Value.ToString();

            DialogResult dr = MessageBox.Show($"Bạn có chắc muốn xóa món [{tenMon}] khỏi hóa đơn?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    string sqlXoa = "DELETE FROM ChiTietHoaDon WHERE ma_hoa_don = @maHD AND ma_mon = @maMon";
                    KetNoi.ChayLenh(sqlXoa, KetNoi.P("@maHD", maHoaDonHienTai), KetNoi.P("@maMon", maMon));

                    // Kiểm tra nếu hóa đơn không còn món nào
                    string sqlDem = "SELECT COUNT(*) FROM ChiTietHoaDon WHERE ma_hoa_don = @maHD";
                    int count = Convert.ToInt32(KetNoi.LayGiaTri(sqlDem, KetNoi.P("@maHD", maHoaDonHienTai)));
                    if (count == 0)
                    {
                        // Xóa luôn hóa đơn rỗng và trả bàn về trạng thái trống
                        KetNoi.ChayLenh("DELETE FROM HoaDon WHERE ma_hoa_don = @maHD", KetNoi.P("@maHD", maHoaDonHienTai));
                        KetNoi.ChayLenh("UPDATE Ban SET trang_thai = N'Đã thanh toán' WHERE ma_ban = @maBan", KetNoi.P("@maBan", maBanDangChon));
                        maHoaDonHienTai = -1;
                        trangThaiDangChon = "Đã thanh toán";
                        label2.Text = "Trạng thái: Bàn trống";
                    }

                    TaiHoaDonBanDangChon();
                    TaiDanhSachBan();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa món: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Nút Thanh toán: Mở form fThanhToan (Khoa)
        /// </summary>
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (maBanDangChon <= 0)
            {
                MessageBox.Show("Vui lòng chọn bàn cần thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (maHoaDonHienTai <= 0)
            {
                MessageBox.Show("Bàn này hiện không có hóa đơn hoặc chưa gọi món nào để thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Mở form fThanhToan của Khoa
            fThanhToan f = new fThanhToan(maBanDangChon, maHoaDonHienTai);
            if (f.ShowDialog() == DialogResult.OK)
            {
                // Thanh toán thành công -> Làm mới lại danh sách bàn và thông tin hóa đơn
                maHoaDonHienTai = -1;
                trangThaiDangChon = "Đã thanh toán";
                label2.Text = "Trạng thái: Bàn trống";
                TaiHoaDonBanDangChon();
                TaiDanhSachBan();
            }
        }
    }
}
