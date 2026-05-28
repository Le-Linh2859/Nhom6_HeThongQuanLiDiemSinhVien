using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLDSV.DAL;

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
    }
}