using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyQuanCaPhe
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Xử lý đăng nhập: Kiểm tra tài khoản trong CSDL, lưu phiên đăng nhập và mở fMain
        /// </summary>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string taiKhoan = txbUsername.Text.Trim();
            string matKhau = textBox1.Text;

            if (string.IsNullOrEmpty(taiKhoan) || string.IsNullOrEmpty(matKhau))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string sql = "SELECT tai_khoan, ten_hien_thi, id_role FROM TaiKhoan WHERE tai_khoan = @tk AND mat_khau = @mk";
                DataTable dt = KetNoi.LayBang(sql, KetNoi.P("@tk", taiKhoan), KetNoi.P("@mk", matKhau));

                if (dt.Rows.Count > 0)
                {
                    PhienDangNhap.TaiKhoan = dt.Rows[0]["tai_khoan"].ToString();
                    PhienDangNhap.TenHienThi = dt.Rows[0]["ten_hien_thi"].ToString();
                    PhienDangNhap.IdRole = Convert.ToInt32(dt.Rows[0]["id_role"]);

                    fMain f = new fMain();
                    this.Hide();
                    f.ShowDialog();
                    this.Show();

                    // Xóa mật khẩu sau khi đăng xuất
                    textBox1.Clear();
                }
                else
                {
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Nút Thoát chương trình
        /// </summary>
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Nút Bật/Tắt xem mật khẩu (icon con mắt)
        /// </summary>
        private void btnShowPass_Click(object sender, EventArgs e)
        {
            textBox1.UseSystemPasswordChar = !textBox1.UseSystemPasswordChar;
            if (textBox1.UseSystemPasswordChar)
            {
                btnShowPass.Text = "👁";
                btnShowPass.BackColor = Color.White;
            }
            else
            {
                btnShowPass.Text = "🙈";
                btnShowPass.BackColor = Color.FromArgb(230, 240, 255);
            }
        }
    }
}
