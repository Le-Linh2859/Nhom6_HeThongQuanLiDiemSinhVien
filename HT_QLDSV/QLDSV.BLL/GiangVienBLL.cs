using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLDSV.DAL;
using System.Text.RegularExpressions;

namespace QLDSV.BLL
{
    public class GiangVienBLL
    {
        GiangVienDAL dal = new GiangVienDAL();

        // 1. Lấy danh sách giảng viên
        public DataTable GetGiangVien()
        {
            return dal.LoadGiangVien();
        }
        public int GetTongSoGiangVien()
        {
            return dal.LoadGiangVien().Rows.Count;
        }
        // 2. Lấy danh sách khoa
        public DataTable GetKhoa()
        {
            return dal.LoadKhoa();
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
            dal.InsertGiangVien(
            soDT,
            hoTen,
            gioiTinh,
            diaChi,
            maGV,
            email,
            maKhoa,
            maTaiKhoan);
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
            dal.UpdateGiangVien(
            soDT,
            hoTen,
            gioiTinh,
            diaChi,
            maGV,
            email,
            maKhoa);
        }

        // 5. Xóa giảng viên
        public void DeleteGiangVien(string maGV)
        {
            dal.DeleteGiangVien(maGV);
        }

        // 6. Thêm tài khoản
        public void InsertTaiKhoan(
        string maTaiKhoan,
        string maVaiTro,
        string tenDangNhap,
        string matKhau,
        int trangThai)
        {
            dal.InsertTaiKhoan(
            maTaiKhoan,
            maVaiTro,
            tenDangNhap,
            matKhau,
            trangThai);
        }

        // 7. Tạo mã tài khoản mới
        public string TaoMaTaiKhoanMoi()
        {
            return dal.TaoMaTaiKhoanMoi();
        }

        // 8. Kiểm tra mã giảng viên tồn tại
        public bool CheckKeyExist(string maGV)
        {
            return dal.CheckKey(maGV);
        }
        public string ValidateGiangVien(
        string maGV,
        string hoTen,
        string email,
        string soDT)
        {
            if (string.IsNullOrWhiteSpace(maGV))
                return "Mã giảng viên không được để trống.";

            if (string.IsNullOrWhiteSpace(hoTen))
                return "Họ tên không được để trống.";

            if (!Regex.IsMatch(
            email,
            @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$"))
            {
                return "Email không hợp lệ.";
            }

            if (!Regex.IsMatch(soDT, @"^0\d{9}$"))
            {
                return "Số điện thoại phải gồm 10 số.";
            }

            return "";
        }
        public string ValidatePassword(string matKhau)
        {
            if (matKhau.Length < 8 || matKhau.Length > 15)
            {
                return "Mật khẩu phải từ 8-15 ký tự.";
            }

            if (!Regex.IsMatch(
            matKhau,
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$"))
            {
                return "Mật khẩu phải có chữ hoa, chữ thường và số.";
            }

            return "";
        }
        public bool IsDuplicateMaGV(string maGV)
        {
            return dal.CheckMaGV(maGV);
        }

        public bool IsDuplicateEmail(string email)
        {
            return dal.CheckEmail(email);
        }

        public bool IsDuplicateSoDT(string soDT)
        {
            return dal.CheckSoDT(soDT);
        }
        public bool IsThongTinChanged(
    string maGV,
    string hoTen,
    string soDT,
    string email,
    string diaChi,
    bool gioiTinh)
        {
            DataTable dt = dal.LoadGiangVien();

            DataRow row = null;

            foreach (DataRow r in dt.Rows)
            {
                if (r["MaGV"].ToString() == maGV)
                {
                    row = r;
                    break;
                }
            }
            if (row == null)
                return true;

            return row["HoTen"].ToString().Trim() != hoTen
                || row["SoDT"].ToString().Trim() != soDT
                || row["Email"].ToString().Trim() != email
                || row["DiaChi"].ToString().Trim() != diaChi
                || Convert.ToBoolean(row["GioiTinh"]) != gioiTinh;
        }
        // Validate khi THÊM
        public string ValidateThemGiangVien(
        string maGV,
        string hoTen,
        string email,
        string soDT,
        string matKhau,
        string nhapLaiMK)
        {
            string loi = ValidateGiangVien(
            maGV,
            hoTen,
            email,
            soDT);

            if (!string.IsNullOrEmpty(loi))
                return loi;

            if (dal.CheckMaGV(maGV))
                return "Mã giảng viên đã tồn tại.";

            if (dal.CheckEmail(email))
                return "Email đã tồn tại.";

            if (dal.CheckSoDT(soDT))
                return "Số điện thoại đã tồn tại.";

            loi = ValidatePassword(matKhau);

            if (!string.IsNullOrEmpty(loi))
                return loi;

            if (matKhau != nhapLaiMK)
                return "Mật khẩu nhập lại không khớp.";

            return "";
        }

        // Validate khi SỬA
        public string ValidateCapNhatGiangVien(
        string maGV,
        string hoTen,
        string email,
        string soDT)
        {
            string loi = ValidateGiangVien(
            maGV,
            hoTen,
            email,
            soDT);

            if (!string.IsNullOrEmpty(loi))
                return loi;

            if (dal.CheckEmailUpdate(email, maGV))
                return "Email đã tồn tại.";

            if (dal.CheckSoDTUpdate(soDT, maGV))
                return "Số điện thoại đã tồn tại.";

            return "";
        }
    }
}
