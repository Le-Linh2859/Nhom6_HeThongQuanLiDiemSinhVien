using System;
using System.Data;
using System.Text.RegularExpressions;
using QLDSV.DAL;

namespace QLDSV.BLL
{
    /// <summary>
    /// Business Logic Layer cho Sinh Viên.
    /// Xử lý logic nghiệp vụ và validation cho chức năng thông tin cá nhân sinh viên.
    /// </summary>
    public class SinhVienBLL
    {
        private readonly SinhVienDAL _dal = new SinhVienDAL();

        /// <summary>
        /// Lấy thông tin sinh viên theo mã tài khoản.
        /// </summary>
        public DataTable GetThongTinSinhVien(string maTaiKhoan)
        {
            return _dal.LoadThongTinSinhVien(maTaiKhoan);
        }

        /// <summary>
        /// Lấy danh sách tất cả sinh viên.
        /// </summary>
        public DataTable GetSinhVien()
        {
            return _dal.GetSinhVien();
        }

        /// <summary>
        /// Lấy danh sách khoa.
        /// </summary>
        public DataTable GetKhoa()
        {
            return _dal.GetKhoa();
        }

        /// <summary>
        /// Lấy danh sách lớp niên chế theo mã khoa.
        /// </summary>
        /// <param name="maKhoa">Mã khoa (có thể là chuỗi rỗng để trả về danh sách trống)</param>
        public DataTable GetLopNienChe(string maKhoa)
        {
            return _dal.GetLopNienChe(maKhoa);
        }

        /// <summary>
        /// Lấy danh sách lớp niên chế theo mã khoa (được sử dụng để lọc trong combobox).
        /// </summary>
        /// <param name="maKhoa">Mã khoa</param>
        public DataTable LoadLopNienCheTheoKhoa(string maKhoa)
        {
            return _dal.LoadLopNienCheTheoKhoa(maKhoa);
        }

        /// <summary>
        /// Tạo mã sinh viên mới.
        /// </summary>
        public string TaoMaSVMoi()
        {
            return _dal.TaoMaSVMoi();
        }

        /// <summary>
        /// Thêm sinh viên mới.
        /// </summary>
        public (bool success, string message) ThemSinhVien(string maSV, string hoTen, DateTime? ngaySinh, bool gioiTinh, string diaChi, string email, string soDT, string maLopNienChe, string nienKhoa)
        {
            // Validate input
            string validationError = ValidateThongTin(soDT, email);
            if (!string.IsNullOrEmpty(validationError))
            {
                return (false, validationError);
            }

            // Check for duplicate email
            if (!string.IsNullOrEmpty(email) && IsDuplicateEmail(email, maSV))
            {
                return (false, "Email đã tồn tại.");
            }

            // Check for duplicate phone number
            if (!string.IsNullOrEmpty(soDT) && IsDuplicateSoDT(soDT, maSV))
            {
                return (false, "Số điện thoại đã tồn tại.");
            }

            bool result = _dal.ThemSinhVien(maSV, hoTen, ngaySinh, gioiTinh, diaChi, email, soDT, maLopNienChe, nienKhoa);
            return (result, result ? "Thêm sinh viên thành công." : "Thêm sinh viên thất bại.");
        }

        /// <summary>
        /// Cập nhật thông tin sinh viên.
        /// </summary>
        public (bool success, string message) SuaSinhVien(string maSV, string hoTen, DateTime? ngaySinh, bool gioiTinh, string diaChi, string email, string soDT, string maLopNienChe, string nienKhoa)
        {
            // Validate input
            string validationError = ValidateThongTin(soDT, email);
            if (!string.IsNullOrEmpty(validationError))
            {
                return (false, validationError);
            }

            // Check for duplicate email (excluding current student)
            if (!string.IsNullOrEmpty(email) && IsDuplicateEmail(email, maSV))
            {
                return (false, "Email đã tồn tại.");
            }

            // Check for duplicate phone number (excluding current student)
            if (!string.IsNullOrEmpty(soDT) && IsDuplicateSoDT(soDT, maSV))
            {
                return (false, "Số điện thoại đã tồn tại.");
            }

            bool result = _dal.SuaSinhVien(maSV, hoTen, ngaySinh, gioiTinh, diaChi, email, soDT, maLopNienChe, nienKhoa);
            return (result, result ? "Cập nhật sinh viên thành công." : "Cập nhật sinh viên thất bại.");
        }

        /// <summary>
        /// Xóa sinh viên theo mã sinh viên.
        /// </summary>
        public (bool success, string message) XoaSinhVien(string maSV)
        {
            bool result = _dal.XoaSinhVien(maSV);
            return (result, result ? "Xóa sinh viên thành công." : "Xóa sinh viên thất bại.");
        }

        /// <summary>
        /// Cập nhật thông tin sinh viên.
        /// </summary>
        public void UpdateThongTinSinhVien(string soDT, string email, string diaChi, string maSV)
        {
            _dal.UpdateSinhVien(soDT, email, diaChi, maSV);
        }

        /// <summary>
        /// Cập nhật tên đăng nhập.
        /// </summary>
        public void UpdateTenDangNhap(string tenDangNhap, string maTaiKhoan)
        {
            _dal.UpdateTenDangNhap(tenDangNhap, maTaiKhoan);
        }

        /// <summary>
        /// Cập nhật mật khẩu.
        /// </summary>
        public void UpdateMatKhau(string matKhau, string maTaiKhoan)
        {
            _dal.UpdateMatKhau(matKhau, maTaiKhoan);
        }

        /// <summary>
        /// Validate thông tin cá nhân sinh viên.
        /// </summary>
        public string ValidateThongTin(string soDT, string email)
        {
            if (!string.IsNullOrWhiteSpace(email) && 
                !Regex.IsMatch(email, @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$"))
            {
                return "Email không hợp lệ.";
            }

            if (!string.IsNullOrWhiteSpace(soDT) && 
                !Regex.IsMatch(soDT, @"^0\d{9}$"))
            {
                return "Số điện thoại phải gồm 10 chữ số và bắt đầu bằng 0.";
            }

            return "";
        }

        /// <summary>
        /// Validate mật khẩu mới.
        /// </summary>
        public string ValidatePassword(string matKhau)
        {
            if (string.IsNullOrEmpty(matKhau))
                return ""; // Không đổi mật khẩu

            if (matKhau.Length < 8 || matKhau.Length > 15)
            {
                return "Mật khẩu phải từ 8-15 ký tự.";
            }

            if (!Regex.IsMatch(matKhau, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$"))
            {
                return "Mật khẩu phải có chữ hoa, chữ thường và số.";
            }

            return "";
        }

        /// <summary>
        /// Kiểm tra email đã tồn tại chưa (trừ sinh viên hiện tại).
        /// </summary>
        public bool IsDuplicateEmail(string email, string maSV)
        {
            return _dal.CheckEmailUpdate(email, maSV);
        }

        /// <summary>
        /// Kiểm tra số điện thoại đã tồn tại chưa (trừ sinh viên hiện tại).
        /// </summary>
        public bool IsDuplicateSoDT(string soDT, string maSV)
        {
            return _dal.CheckSoDTUpdate(soDT, maSV);
        }
    }
}