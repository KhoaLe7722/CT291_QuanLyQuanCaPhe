using System;
using System.Data;
using System.Data.SqlClient;

public static class KetNoi
{
    // Mỗi người sửa Data Source cho đúng máy mình
    private static string chuoiKetNoi =
        @"Data Source=.;Initial Catalog=QuanLyQuanCaPhe;Integrated Security=True";

    public static SqlConnection TaoKetNoi()
    {
        return new SqlConnection(chuoiKetNoi);
    }

    // Tạo tham số (tránh lỗi khi giá trị là số 0 hoặc null)
    public static SqlParameter P(string ten, object giaTri)
    {
        return new SqlParameter(ten, giaTri ?? DBNull.Value);
    }

    // SELECT nhiều dòng -> DataTable
    public static DataTable LayBang(string sql, params SqlParameter[] ps)
    {
        using (var conn = TaoKetNoi())
        using (var da = new SqlDataAdapter(sql, conn))
        {
            da.SelectCommand.Parameters.AddRange(ps);
            var dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }

    // SELECT 1 giá trị (COUNT, SUM, mã...). Không có dòng -> null
    public static object LayGiaTri(string sql, params SqlParameter[] ps)
    {
        using (var conn = TaoKetNoi())
        using (var cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddRange(ps);
            conn.Open();
            return cmd.ExecuteScalar();
        }
    }

    // INSERT / UPDATE / DELETE -> số dòng bị ảnh hưởng
    public static int ChayLenh(string sql, params SqlParameter[] ps)
    {
        using (var conn = TaoKetNoi())
        using (var cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddRange(ps);
            conn.Open();
            return cmd.ExecuteNonQuery();
        }
    }
}