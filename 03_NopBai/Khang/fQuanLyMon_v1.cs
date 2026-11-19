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
    public partial class fQuanLyMon : Form
    {
        private enum LoaiThaoTac { Them, Sua, Xoa }

        private class LichSuThaoTac
        {
            public LoaiThaoTac Loai { get; set; }
            public int MaMon { get; set; }
            public string TenMonCu { get; set; }
            public int MaLoaiCu { get; set; }
            public decimal GiaCu { get; set; }
        }

        private readonly Stack<LichSuThaoTac> stackUndo = new Stack<LichSuThaoTac>();
        private bool dangThemMoi = false;

        // Lưu giá trị gốc của dòng đang chọn để hỗ trợ Ctrl+Z khi đang gõ dở
        private int maMonDangChon = -1;
        private string tenMonGoc = "";
        private string tenLoaiGoc = "";
        private string giaGoc = "";

        public fQuanLyMon()
        {
            InitializeComponent();
        }

        private void fQuanLyMon_Load(object sender, EventArgs e)
        {
            TaiDanhMucLoaiMon();
            TaiDanhSachMon();
            CapNhatNutUndo();
        }

        /// <summary>
        /// Bắt phím tắt Ctrl + Z trên toàn bộ Form để hoàn tác
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Z))
            {
                ThucHienHoanTac();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void CapNhatNutUndo()
        {
            if (stackUndo.Count > 0)
            {
                btnUndo.Text = $"↩️ Hoàn tác (Ctrl+Z) [{stackUndo.Count}]";
                btnUndo.BackColor = Color.FromArgb(243, 156, 18);
            }
            else
            {
                btnUndo.Text = "↩️ Hoàn tác (Ctrl+Z)";
                btnUndo.BackColor = Color.FromArgb(149, 165, 166);
            }
        }

        /// <summary>
        /// Tải danh mục Loại món vào ComboBox
        /// </summary>
        private void TaiDanhMucLoaiMon()
        {
            try
            {
                string sql = "SELECT ma_loai, ten_loai FROM LoaiMon ORDER BY ma_loai";
                DataTable dt = KetNoi.LayBang(sql);
                cboLoaiMon.DataSource = dt;
                cboLoaiMon.DisplayMember = "ten_loai";
                cboLoaiMon.ValueMember = "ma_loai";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải loại món: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tải toàn bộ danh sách món ăn lên DataGridView
        /// </summary>
        private void TaiDanhSachMon(string tuKhoa = "", int chonMaMon = -1)
        {
            try
            {
                string sql = @"SELECT m.ma_mon AS [Mã món], 
                                      m.ten_mon AS [Tên món], 
                                      l.ten_loai AS [Loại món], 
                                      m.gia AS [Giá]
                               FROM Mon m
                               JOIN LoaiMon l ON m.ma_loai = l.ma_loai";

                DataTable dt;
                if (string.IsNullOrWhiteSpace(tuKhoa))
                {
                    sql += " ORDER BY m.ma_mon DESC";
                    dt = KetNoi.LayBang(sql);
                }
                else
                {
                    sql += " WHERE m.ten_mon LIKE @tuKhoa ORDER BY m.ma_mon DESC";
                    dt = KetNoi.LayBang(sql, KetNoi.P("@tuKhoa", "%" + tuKhoa.Trim() + "%"));
                }

                dgvMon.DataSource = dt;

                if (dgvMon.Columns["Giá"] != null)
                    dgvMon.Columns["Giá"].DefaultCellStyle.Format = "#,##0";

                if (dgvMon.Rows.Count > 0 && !dangThemMoi)
                {
                    int rowIndex = 0;
                    if (chonMaMon > 0)
                    {
                        for (int i = 0; i < dgvMon.Rows.Count; i++)
                        {
                            if (dgvMon.Rows[i].Cells["Mã món"].Value != null &&
                                Convert.ToInt32(dgvMon.Rows[i].Cells["Mã món"].Value) == chonMaMon)
                            {
                                rowIndex = i;
                                break;
                            }
                        }
                    }
                    dgvMon.ClearSelection();
                    dgvMon.Rows[rowIndex].Selected = true;
                    dgvMon_CellClick(dgvMon, new DataGridViewCellEventArgs(0, rowIndex));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách món: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Tìm kiếm món khi người dùng gõ từ khóa
        /// </summary>
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            TaiDanhSachMon(txtTimKiem.Text);
        }

        /// <summary>
        /// Khi click một món trong DataGridView, chuyển về chế độ Xem/Sửa món đó
        /// </summary>
        private void dgvMon_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvMon.Rows.Count)
            {
                DataGridViewRow row = dgvMon.Rows[e.RowIndex];
                if (row.Cells["Mã món"].Value != null)
                {
                    dangThemMoi = false;
                    maMonDangChon = Convert.ToInt32(row.Cells["Mã món"].Value);
                    tenMonGoc = row.Cells["Tên món"].Value.ToString();
                    giaGoc = string.Format("{0:0}", row.Cells["Giá"].Value);
                    tenLoaiGoc = row.Cells["Loại món"].Value != null ? row.Cells["Loại món"].Value.ToString() : "";

                    txtMaMon.Text = maMonDangChon.ToString();
                    txtTenMon.Text = tenMonGoc;
                    txtGia.Text = giaGoc;
                    cboLoaiMon.Text = tenLoaiGoc;

                    lblTrangThaiNhap.Text = $"📝 ĐANG CHỌN MÓN #{maMonDangChon}: Sửa thông tin rồi bấm [✏️ Lưu sửa]";
                    lblTrangThaiNhap.BackColor = Color.FromArgb(235, 245, 255);
                    lblTrangThaiNhap.ForeColor = Color.FromArgb(41, 128, 185);

                    picAnhMonQL.Image = QuanLyHinhAnh.LayAnhMon(maMonDangChon, txtTenMon.Text);
                    lblTenAnhMonQL.Text = txtTenMon.Text;
                }
            }
        }

        /// <summary>
        /// Chuyển sang chế độ Tạo món mới hoàn toàn (không đè lên món cũ)
        /// </summary>
        private void BatCheDoThemMonMoi()
        {
            dangThemMoi = true;
            maMonDangChon = -1;
            dgvMon.ClearSelection();

            int maTiepTheo = 1;
            try
            {
                object maxObj = KetNoi.LayGiaTri("SELECT ISNULL(MAX(ma_mon), 0) + 1 FROM Mon");
                if (maxObj != null) maTiepTheo = Convert.ToInt32(maxObj);
            }
            catch { }

            txtMaMon.Text = $"{maTiepTheo} (Món mới)";
            txtTenMon.Clear();
            txtGia.Clear();
            if (cboLoaiMon.Items.Count > 0)
                cboLoaiMon.SelectedIndex = 0;

            lblTrangThaiNhap.Text = "🆕 ĐANG TẠO MÓN MỚI: Nhập Tên món, Loại, Giá rồi bấm [💾 Lưu món mới]";
            lblTrangThaiNhap.BackColor = Color.FromArgb(232, 248, 245);
            lblTrangThaiNhap.ForeColor = Color.FromArgb(39, 174, 96);

            picAnhMonQL.Image = null;
            lblTenAnhMonQL.Text = "🆕 Nhập thông tin món mới";
            txtTenMon.Focus();
        }

        /// <summary>
        /// Nút [➕ Thêm món mới] trên thanh công cụ phía trên (bên trái Tìm món)
        /// </summary>
        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            // Nếu đang xem món cũ -> Bấm nút này sẽ tạo form món mới trống để nhập
            if (!dangThemMoi)
            {
                BatCheDoThemMonMoi();
                return;
            }

            // Nếu đang ở chế độ thêm mới và đã nhập tên món -> Lưu luôn
            if (!string.IsNullOrWhiteSpace(txtTenMon.Text))
            {
                LuuMonMoiVaoCSDL();
            }
            else
            {
                txtTenMon.Focus();
            }
        }

        /// <summary>
        /// Nút [💾 Lưu món mới] ở khung bên dưới
        /// </summary>
        private void btnLuuMoi_Click(object sender, EventArgs e)
        {
            if (!dangThemMoi)
            {
                // Nếu chưa bật chế độ thêm mới thì chuyển sang chế độ tạo món mới trống
                BatCheDoThemMonMoi();
                return;
            }

            LuuMonMoiVaoCSDL();
        }

        /// <summary>
        /// Thực hiện lưu món mới vào CSDL (Sử dụng OOP Mon -> DoUong / ThucAn)
        /// </summary>
        private void LuuMonMoiVaoCSDL()
        {
            string tenMon = txtTenMon.Text.Trim();
            if (string.IsNullOrEmpty(tenMon))
            {
                MessageBox.Show("Vui lòng nhập tên món mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenMon.Focus();
                return;
            }

            if (!decimal.TryParse(txtGia.Text.Trim(), out decimal gia) || gia < 0)
            {
                MessageBox.Show("Giá món phải là một số hợp lệ không âm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGia.Focus();
                return;
            }

            int maLoai = Convert.ToInt32(cboLoaiMon.SelectedValue);

            // Ứng dụng OOP: Đa hình tạo đối tượng lớp con tương ứng
            Mon monMoi;
            if (maLoai == 1)
                monMoi = new DoUong(0, tenMon, gia);
            else
                monMoi = new ThucAn(0, tenMon, gia);

            try
            {
                string sql = @"INSERT INTO Mon(ten_mon, ma_loai, gia) 
                               VALUES(@ten, @loai, @gia);
                               SELECT SCOPE_IDENTITY();";
                object resId = KetNoi.LayGiaTri(sql,
                    KetNoi.P("@ten", monMoi.TenMon),
                    KetNoi.P("@loai", monMoi.MaLoai),
                    KetNoi.P("@gia", monMoi.Gia));

                int maMonMoi = Convert.ToInt32(resId);

                // Lưu vào lịch sử Undo (Ctrl + Z)
                stackUndo.Push(new LichSuThaoTac
                {
                    Loai = LoaiThaoTac.Them,
                    MaMon = maMonMoi,
                    TenMonCu = monMoi.TenMon,
                    MaLoaiCu = monMoi.MaLoai,
                    GiaCu = monMoi.Gia
                });
                CapNhatNutUndo();

                dangThemMoi = false;
                MessageBox.Show($"Đã thêm món mới [{monMoi.TenMon}] ({monMoi.LayPhanLoai()}) thành công!\n(Bấm Ctrl + Z nếu muốn hoàn tác)", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiDanhSachMon(txtTimKiem.Text, maMonMoi);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm món: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Nút [✏️ Lưu sửa]
        /// </summary>
        private void btnSua_Click(object sender, EventArgs e)
        {
            // Nếu người dùng đang ở chế độ Thêm món mới mà bấm nhầm Lưu sửa -> Lưu món mới luôn
            if (dangThemMoi)
            {
                LuuMonMoiVaoCSDL();
                return;
            }

            if (!int.TryParse(txtMaMon.Text, out int maMon))
            {
                MessageBox.Show("Vui lòng chọn món cần sửa trong danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string tenMon = txtTenMon.Text.Trim();
            if (string.IsNullOrEmpty(tenMon))
            {
                MessageBox.Show("Tên món không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtGia.Text.Trim(), out decimal gia) || gia < 0)
            {
                MessageBox.Show("Giá món không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maLoai = Convert.ToInt32(cboLoaiMon.SelectedValue);

            try
            {
                // Lấy dữ liệu cũ trước khi sửa để lưu vào stack Undo (Ctrl + Z)
                DataTable dtCu = KetNoi.LayBang("SELECT ten_mon, ma_loai, gia FROM Mon WHERE ma_mon = @ma", KetNoi.P("@ma", maMon));
                if (dtCu.Rows.Count > 0)
                {
                    stackUndo.Push(new LichSuThaoTac
                    {
                        Loai = LoaiThaoTac.Sua,
                        MaMon = maMon,
                        TenMonCu = dtCu.Rows[0]["ten_mon"].ToString(),
                        MaLoaiCu = Convert.ToInt32(dtCu.Rows[0]["ma_loai"]),
                        GiaCu = Convert.ToDecimal(dtCu.Rows[0]["gia"])
                    });
                    CapNhatNutUndo();
                }

                string sql = "UPDATE Mon SET ten_mon = @ten, ma_loai = @loai, gia = @gia WHERE ma_mon = @ma";
                KetNoi.ChayLenh(sql,
                    KetNoi.P("@ten", tenMon),
                    KetNoi.P("@loai", maLoai),
                    KetNoi.P("@gia", gia),
                    KetNoi.P("@ma", maMon));

                MessageBox.Show("Cập nhật thông tin món thành công!\n(Bấm Ctrl + Z nếu muốn hoàn tác)", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TaiDanhSachMon(txtTimKiem.Text, maMon);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi sửa món: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Nút [🗑️ Xóa]
        /// </summary>
        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dangThemMoi || !int.TryParse(txtMaMon.Text, out int maMon))
            {
                MessageBox.Show("Vui lòng chọn một món có sẵn trong danh sách để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show($"Bạn có chắc muốn xóa món [{txtTenMon.Text}]?\n(Bạn có thể bấm Ctrl + Z để khôi phục lại nếu lỡ xóa)", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    string sqlKiemTra = "SELECT COUNT(*) FROM ChiTietHoaDon WHERE ma_mon = @ma";
                    int count = Convert.ToInt32(KetNoi.LayGiaTri(sqlKiemTra, KetNoi.P("@ma", maMon)));
                    if (count > 0)
                    {
                        MessageBox.Show("Món này đã có trong lịch sử hóa đơn, không thể xóa để bảo toàn dữ liệu bán hàng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Lưu thông tin món trước khi xóa vào stack Undo (Ctrl + Z)
                    DataTable dtCu = KetNoi.LayBang("SELECT ten_mon, ma_loai, gia FROM Mon WHERE ma_mon = @ma", KetNoi.P("@ma", maMon));
                    if (dtCu.Rows.Count > 0)
                    {
                        stackUndo.Push(new LichSuThaoTac
                        {
                            Loai = LoaiThaoTac.Xoa,
                            MaMon = maMon,
                            TenMonCu = dtCu.Rows[0]["ten_mon"].ToString(),
                            MaLoaiCu = Convert.ToInt32(dtCu.Rows[0]["ma_loai"]),
                            GiaCu = Convert.ToDecimal(dtCu.Rows[0]["gia"])
                        });
                        CapNhatNutUndo();
                    }

                    string sql = "DELETE FROM Mon WHERE ma_mon = @ma";
                    KetNoi.ChayLenh(sql, KetNoi.P("@ma", maMon));

                    MessageBox.Show("Đã xóa món! Nếu xóa nhầm, hãy bấm Ctrl + Z hoặc nút Hoàn tác để khôi phục.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TaiDanhSachMon(txtTimKiem.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa món: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Nút [↩️ Hoàn tác (Ctrl+Z)]
        /// </summary>
        private void btnUndo_Click(object sender, EventArgs e)
        {
            ThucHienHoanTac();
        }

        /// <summary>
        /// Xử lý hoàn tác (Ctrl + Z):
        /// 1. Nếu đang gõ sửa dở trên ô nhập mà chưa bấm Lưu -> Khôi phục lại chữ gốc của món đang chọn.
        /// 2. Nếu đã Thêm / Sửa / Xóa trong CSDL -> Hoàn tác lại thao tác CSDL gần nhất.
        /// </summary>
        private void ThucHienHoanTac()
        {
            // Trường hợp 1: Đang ở chế độ Xem/Sửa và người dùng lỡ gõ sai chữ trên ô nhập (chưa bấm Lưu)
            if (!dangThemMoi && maMonDangChon > 0)
            {
                if (txtTenMon.Text != tenMonGoc || txtGia.Text != giaGoc || cboLoaiMon.Text != tenLoaiGoc)
                {
                    txtTenMon.Text = tenMonGoc;
                    txtGia.Text = giaGoc;
                    cboLoaiMon.Text = tenLoaiGoc;
                    return;
                }
            }

            // Trường hợp 2: Hoàn tác thao tác Thêm / Sửa / Xóa trong CSDL
            if (stackUndo.Count == 0)
            {
                MessageBox.Show("Không có thao tác nào gần đây để hoàn tác (Ctrl + Z)!", "Hoàn tác", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            LichSuThaoTac thaoTac = stackUndo.Pop();
            CapNhatNutUndo();

            try
            {
                if (thaoTac.Loai == LoaiThaoTac.Xoa)
                {
                    // Khôi phục lại món vừa bị xóa với đúng Mã món cũ
                    string sqlRestore = @"SET IDENTITY_INSERT Mon ON;
                                          INSERT INTO Mon(ma_mon, ten_mon, ma_loai, gia) VALUES(@ma, @ten, @loai, @gia);
                                          SET IDENTITY_INSERT Mon OFF;";
                    KetNoi.ChayLenh(sqlRestore,
                        KetNoi.P("@ma", thaoTac.MaMon),
                        KetNoi.P("@ten", thaoTac.TenMonCu),
                        KetNoi.P("@loai", thaoTac.MaLoaiCu),
                        KetNoi.P("@gia", thaoTac.GiaCu));

                    dangThemMoi = false;
                    TaiDanhSachMon(txtTimKiem.Text, thaoTac.MaMon);
                    MessageBox.Show($"Đã hoàn tác (Ctrl+Z): Khôi phục lại món [{thaoTac.TenMonCu}] (Mã #{thaoTac.MaMon})!", "Hoàn tác thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (thaoTac.Loai == LoaiThaoTac.Sua)
                {
                    // Khôi phục lại thông tin trước khi sửa
                    string sqlRevert = "UPDATE Mon SET ten_mon = @ten, ma_loai = @loai, gia = @gia WHERE ma_mon = @ma";
                    KetNoi.ChayLenh(sqlRevert,
                        KetNoi.P("@ten", thaoTac.TenMonCu),
                        KetNoi.P("@loai", thaoTac.MaLoaiCu),
                        KetNoi.P("@gia", thaoTac.GiaCu),
                        KetNoi.P("@ma", thaoTac.MaMon));

                    dangThemMoi = false;
                    TaiDanhSachMon(txtTimKiem.Text, thaoTac.MaMon);
                    MessageBox.Show($"Đã hoàn tác (Ctrl+Z): Khôi phục thông tin cũ của món [{thaoTac.TenMonCu}]!", "Hoàn tác thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (thaoTac.Loai == LoaiThaoTac.Them)
                {
                    // Xóa món vừa thêm nhầm
                    KetNoi.ChayLenh("DELETE FROM Mon WHERE ma_mon = @ma", KetNoi.P("@ma", thaoTac.MaMon));
                    KetNoi.ChayLenh("DECLARE @maxId INT = ISNULL((SELECT MAX(ma_mon) FROM Mon), 0); DBCC CHECKIDENT ('Mon', RESEED, @maxId);");

                    dangThemMoi = false;
                    TaiDanhSachMon(txtTimKiem.Text);
                    MessageBox.Show($"Đã hoàn tác (Ctrl+Z): Hủy thêm món [{thaoTac.TenMonCu}]!", "Hoàn tác thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hoàn tác: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Nút [🔄 Làm mới]
        /// </summary>
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            dangThemMoi = false;
            txtTimKiem.Clear();
            TaiDanhSachMon();
        }
    }
}
