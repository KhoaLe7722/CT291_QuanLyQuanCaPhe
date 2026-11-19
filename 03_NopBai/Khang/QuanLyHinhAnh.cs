using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace QuanLyQuanCaPhe
{
    /// <summary>
    /// Tiện ích nạp và quản lý hình ảnh món ăn từ thư mục menu_images
    /// </summary>
    public static class QuanLyHinhAnh
    {
        // Ánh xạ mã món (1-14) với tên file ảnh trong thư mục menu_images
        private static readonly string[] danhSachAnh = new string[]
        {
            "",                      // 0: không dùng
            "01_ca_phe_den.jpg",      // 1: Cà phê đen
            "02_ca_phe_sua.jpg",      // 2: Cà phê sữa
            "03_bac_xiu.jpg",         // 3: Bạc xỉu
            "04_cappuccino.jpg",      // 4: Cappuccino
            "05_tra_dao.jpg",         // 5: Trà đào
            "06_tra_vai.jpg",         // 6: Trà vải
            "07_tra_chanh.jpg",       // 7: Trà chanh
            "08_sinh_to_bo.jpg",      // 8: Sinh tố bơ
            "09_sinh_to_xoai.jpg",    // 9: Sinh tố xoài
            "10_tiramisu.jpg",        // 10: Tiramisu
            "11_mousse.jpg",          // 11: Mousse
            "12_croissant.jpg",       // 12: Croissant
            "13_banh_mi_op_la.jpg",   // 13: Bánh mì ốp la
            "14_khoai_tay_chien.jpg"  // 14: Khoai tây chiên
        };

        /// <summary>
        /// Lấy hình ảnh của món ăn theo mã món hoặc tên món
        /// </summary>
        public static Image LayAnhMon(int maMon, string tenMon = "")
        {
            string tenFile = "";
            if (!string.IsNullOrEmpty(tenMon))
            {
                // Ưu tiên tìm theo tên món không dấu để luôn khớp ngay cả khi đổi mã món
                string khongDau = BoDau(tenMon).ToLower().Replace(" ", "_");
                for (int i = 1; i < danhSachAnh.Length; i++)
                {
                    if (danhSachAnh[i].Contains(khongDau))
                    {
                        tenFile = danhSachAnh[i];
                        break;
                    }
                }
            }

            if (string.IsNullOrEmpty(tenFile) && maMon >= 1 && maMon < danhSachAnh.Length)
            {
                tenFile = danhSachAnh[maMon];
            }

            if (string.IsNullOrEmpty(tenFile)) return null;

            // Kiểm tra các vị trí thư mục ảnh khả dụng (dùng đường dẫn tương đối để chạy được trên mọi máy và mọi ổ đĩa)
            string[] danhSachThuMuc = new string[]
            {
                Path.Combine(Application.StartupPath, "menu_images", tenFile),
                Path.Combine(Application.StartupPath, @"..\menu_images", tenFile),
                Path.Combine(Application.StartupPath, @"..\..\menu_images", tenFile),
                Path.Combine(Application.StartupPath, @"..\..\..\menu_images", tenFile),
                Path.Combine(Application.StartupPath, @"..\..\..\..\menu_images", tenFile)
            };

            foreach (string duongDan in danhSachThuMuc)
            {
                if (File.Exists(duongDan))
                {
                    try
                    {
                        // Dùng MemoryStream để không khóa file trên ổ đĩa
                        byte[] bytes = File.ReadAllBytes(duongDan);
                        using (var ms = new MemoryStream(bytes))
                        {
                            return Image.FromStream(ms);
                        }
                    }
                    catch
                    {
                        // Tiếp tục thử đường dẫn khác nếu có lỗi đọc
                    }
                }
            }

            return null;
        }

        private static string BoDau(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            string normalized = input.Normalize(System.Text.NormalizationForm.FormD);
            string result = Regex.Replace(normalized, @"[\p{IsCombiningDiacriticalMarks}]", "");
            return result.Replace("đ", "d").Replace("Đ", "D");
        }
    }
}
