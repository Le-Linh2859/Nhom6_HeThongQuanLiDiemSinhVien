using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QLDSV.DAL;

namespace QLDSV.BLL
{
    public class MonHocBLL
    {
        private MonHocDAL dal = new MonHocDAL();

        // Lấy danh sách môn học hỗ trợ từ khóa tìm kiếm và bộ lọc khoa
        public DataTable GetMonHocList(string keyword = "", string maKhoa = "ALL")
        {
            return dal.GetMonHocList(keyword, maKhoa);
        }
        public DataTable GetDanhSachTheoGiangVien(string maGV, string keyword)
        {
            return dal.GetDanhSachTheoGiangVien(maGV, keyword);
        }
        // Lấy danh sách các Khoa
        public DataTable GetKhoa()
        {
            return dal.LoadKhoa();
        }

        // Kiểm tra sự tồn tại của mã môn học
        public bool CheckKeyExist(string maMon)
        {
            if (string.IsNullOrWhiteSpace(maMon)) return false;
            return dal.CheckKeyExist(maMon);
        }

        // Thêm môn học mới với nghiệp vụ kiểm tra đầu vào
        public void InsertMonHoc(string maMon, string tenMon, int soTC, string maKhoa, string moTa)
        {
            if (string.IsNullOrWhiteSpace(maMon))
                throw new ArgumentException("Mã môn học không được để trống.");
            if (string.IsNullOrWhiteSpace(tenMon))
                throw new ArgumentException("Tên môn học không được để trống.");
            if (soTC <= 0)
                throw new ArgumentException("Số tín chỉ phải là số nguyên dương lớn hơn 0.");
            if (string.IsNullOrWhiteSpace(maKhoa))
                throw new ArgumentException("Vui lòng chọn khoa quản lý.");

            dal.InsertMonHoc(maMon, tenMon, soTC, maKhoa, moTa);
        }

        // Cập nhật thông tin môn học với nghiệp vụ kiểm tra đầu vào
        public void UpdateMonHoc(string maMon, string tenMon, int soTC, string maKhoa, string moTa)
        {
            if (string.IsNullOrWhiteSpace(maMon))
                throw new ArgumentException("Mã môn học không được để trống.");
            if (string.IsNullOrWhiteSpace(tenMon))
                throw new ArgumentException("Tên môn học không được để trống.");
            if (soTC <= 0)
                throw new ArgumentException("Số tín chỉ phải là số nguyên dương lớn hơn 0.");
            if (string.IsNullOrWhiteSpace(maKhoa))
                throw new ArgumentException("Vui lòng chọn khoa quản lý.");

            dal.UpdateMonHoc(maMon, tenMon, soTC, maKhoa, moTa);
        }

        // Xóa môn học
        public void DeleteMonHoc(string maMon)
        {
            if (string.IsNullOrWhiteSpace(maMon))
                throw new ArgumentException("Mã môn học không được chọn hoặc để trống.");
            
            dal.DeleteMonHoc(maMon);
        }
    }
}
