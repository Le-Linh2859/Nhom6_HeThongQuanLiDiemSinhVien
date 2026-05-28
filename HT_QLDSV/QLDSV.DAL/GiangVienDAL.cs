using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDSV.DAL
{
    public class GiangVienDAL
    {
        // 1. Lấy toàn bộ danh sách giảng viên
        public DataTable LoadGiangVien()
        {
            string sql = @"
                SELECT 
                    gv.MaGV,
                    gv.HoTen,
                    gv.GioiTinh,
                    gv.DiaChi,
                    gv.SoDT,
                    gv.Email,
                    gv.MaKhoa,
                    k.TenKhoa,
                    gv.MaTaiKhoan
                FROM GiangVien gv
                LEFT JOIN Khoa k
                ON gv.MaKhoa = k.MaKhoa";

            return Connection.GetDataToTable(sql);
        }

        // 2. Load danh sách khoa
        public DataTable LoadKhoa()
        {
            string sql =
                "SELECT MaKhoa, TenKhoa FROM Khoa";

            return Connection.GetDataToTable(sql);
        }

        // 3. Thêm giảng viên
        public void InsertGiangVien(
    string soDT,
    string hoTen,
    bool gioiTinh,
    string diaChi,
    string maGV,
    string email,
    string maKhoa,
    string maTaiKhoan)
        {
            string sql = $@"
        INSERT INTO GiangVien
        (
            SoDT,
            HoTen,
            GioiTinh,
            DiaChi,
            MaGV,
            Email,
            MaKhoa,
            MaTaiKhoan
        )
        VALUES
        (
            '{soDT}',
            N'{hoTen}',
            '{gioiTinh}',
            N'{diaChi}',
            '{maGV}',
            '{email}',
            '{maKhoa}',
            '{maTaiKhoan}'
        )";

            Connection.RunSql(sql);
        }
        // 4. Cập nhật giảng viên
        public void UpdateGiangVien(
    string soDT,
    string hoTen,
    bool gioiTinh,
    string diaChi,
    string maGV,
    string email,
    string maKhoa)
        {
            string sql = $@"
        UPDATE GiangVien
        SET
            SoDT = '{soDT}',
            HoTen = N'{hoTen}',
            GioiTinh = '{gioiTinh}',
            DiaChi = N'{diaChi}',
            Email = '{email}',
            MaKhoa = '{maKhoa}'
        WHERE MaGV = '{maGV}'";

            Connection.RunSql(sql);
        }

        // 5. Xóa giảng viên
        public void DeleteGiangVien(string maGV)
        {
            string sql =
                $"DELETE FROM GiangVien WHERE MaGV = '{maGV}'";

            Connection.RunSql(sql);
        }

        // 6. Thêm tài khoản
        public void InsertTaiKhoan(
            string maTaiKhoan,
            string maVaiTro,
            string tenDangNhap,
            string matKhau,
            int trangThai)
        {
            string sql = $@"
                INSERT INTO TaiKhoan
                (
                    MaTaiKhoan,
                    MaVaiTro,
                    TenDangNhap,
                    MatKhau,
                    TrangThai
                )
                VALUES
                (
                    '{maTaiKhoan}',
                    '{maVaiTro}',
                    '{tenDangNhap}',
                    '{matKhau}',
                    {trangThai}
                )";

            Connection.RunSql(sql);
        }

        // 7. Tạo mã tài khoản mới
        public string TaoMaTaiKhoanMoi()
        {
            string sql =
                "SELECT TOP 1 MaTaiKhoan FROM TaiKhoan ORDER BY MaTaiKhoan DESC";

            DataTable dt = Connection.GetDataToTable(sql);

            if (dt.Rows.Count == 0)
                return "TK001";

            string maCu =
                dt.Rows[0]["MaTaiKhoan"].ToString();

            int so =
                int.Parse(maCu.Substring(2)) + 1;

            return "TK" + so.ToString("000");
        }

        // 8. Kiểm tra mã giảng viên tồn tại
        public bool CheckKey(string maGV)
        {
            string sql =
                $"SELECT MaGV FROM GiangVien WHERE MaGV = '{maGV}'";

            DataTable table =
                Connection.GetDataToTable(sql);

            return table.Rows.Count > 0;
        }
    }
}