using System;

namespace QuanLyQuanCaPhe
{
    /// <summary>
    /// Lớp con DoUong kế thừa từ Mon (ma_loai = 1)
    /// </summary>
    public class DoUong : Mon
    {
        public DoUong() : base()
        {
            MaLoai = 1;
        }

        public DoUong(int maMon, string tenMon, decimal gia)
            : base(maMon, tenMon, 1, gia)
        {
        }

        public override string LayPhanLoai()
        {
            return "Đồ uống";
        }
    }
}
