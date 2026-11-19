using System;

namespace QuanLyQuanCaPhe
{
    /// <summary>
    /// Lớp cha Mon (Phần OOP - Khang)
    /// </summary>
    public abstract class Mon
    {
        public int MaMon { get; set; }
        public string TenMon { get; set; }
        public int MaLoai { get; set; }
        public decimal Gia { get; set; }

        public Mon() { }

        public Mon(int maMon, string tenMon, int maLoai, decimal gia)
        {
            MaMon = maMon;
            TenMon = tenMon;
            MaLoai = maLoai;
            Gia = gia;
        }

        // Phương thức trừu tượng để lớp con định nghĩa
        public abstract string LayPhanLoai();
    }
}
