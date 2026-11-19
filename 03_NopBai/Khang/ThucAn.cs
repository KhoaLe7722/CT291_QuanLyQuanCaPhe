using System;

namespace QuanLyQuanCaPhe
{
    /// <summary>
    /// Lớp con ThucAn kế thừa từ Mon (ma_loai = 2)
    /// </summary>
    public class ThucAn : Mon
    {
        public ThucAn() : base()
        {
            MaLoai = 2;
        }

        public ThucAn(int maMon, string tenMon, decimal gia)
            : base(maMon, tenMon, 2, gia)
        {
        }

        public override string LayPhanLoai()
        {
            return "Thức ăn";
        }
    }
}
