using System;
using System.Data;
using QLDSV.DAL;

namespace QLDSV.BLL
{
    public class LopHocPhanBLL
    {
        private LopHocPhanDAL dal = new LopHocPhanDAL();

        public DataTable GetDanhSachLopHocPhan(
            string keyword,
            string maKhoa,
            string maMon)
        {
            return dal.GetDanhSach(keyword, maKhoa, maMon);
        }

        public DataTable GetDanhSachTheoGiangVien(string maGV, string keyword, string maMon)
        {
            return dal.GetDanhSachTheoGiangVien(maGV, keyword, maMon);
        }

        public DataTable GetMonHocTheoGiangVien(string maGV)
        {
            return dal.GetMonHocTheoGiangVien(maGV);
        }

        public DataTable GetDanhSachTheoSinhVien(string maSV)
        {
            return dal.GetDanhSachTheoSinhVien(maSV);
        }

        public DataTable GetKhoa()
        {
            return dal.GetKhoa();
        }

        public DataTable GetMonHocTheoKhoa(string maKhoa)
        {
            return dal.GetMonHocTheoKhoa(maKhoa);
        }

        public DataTable GetGiangVienTheoKhoa(string maKhoa)
        {
            return dal.GetGiangVienTheoKhoa(maKhoa);
        }

        public DataTable GetHocKy()
        {
            return dal.GetHocKy();
        }

        public DataTable GetNamHoc()
        {
            return dal.GetNamHoc();
        }

        public DataTable GetLopHocPhanTheoMonVaSinhVien(string maMon, string maSV)
        {
            return dal.GetLopHocPhanTheoMonVaSinhVien(maMon, maSV);
        }

        /// <summary>
        /// Thêm lớp học phần mới.
        /// </summary>
        public bool Them(
            string ma,
            string ten,
            string thu,
            int caHoc,
            string phong,
            string trangThai,
            string maMon,
            string maGV,
            string maLoaiHK,
            string maNamHoc,
            DateTime thoiGianBD,
            DateTime thoiGianKT,
            out string message)
        {
            if (dal.CheckMaExists(ma))
            {
                message = "Mã lớp học phần đã tồn tại";
                return false;
            }

            string maHKNH = dal.GetMaHKNH(maLoaiHK, maNamHoc);

            if (string.IsNullOrEmpty(maHKNH))
            {
                message = "Không tìm thấy học kỳ - năm học";
                return false;
            }

            dal.Insert(
                ma,
                ten,
                caHoc,
                thu,
                phong,
                trangThai,
                maMon,
                maGV,
                maHKNH,
                thoiGianBD,
                thoiGianKT);

            message = "Thêm lớp học phần thành công";
            return true;
        }

        /// <summary>
        /// Cập nhật lớp học phần.
        /// </summary>
        public bool Sua(
            string ma,
            string ten,
            string thu,
            int caHoc,
            string phong,
            string trangThai,
            string maMon,
            string maGV,
            string maLoaiHK,
            string maNamHoc,
            DateTime thoiGianBD,
            DateTime thoiGianKT,
            out string message)
        {
            string maHKNH = dal.GetMaHKNH(maLoaiHK, maNamHoc);

            if (string.IsNullOrEmpty(maHKNH))
            {
                message = "Không tìm thấy học kỳ - năm học";
                return false;
            }

            dal.Update(
                ma,
                ten,
                caHoc,
                thu,
                phong,
                trangThai,
                maMon,
                maGV,
                maHKNH,
                thoiGianBD,
                thoiGianKT);

            message = "Cập nhật lớp học phần thành công";
            return true;
        }

        public void Xoa(string ma)
        {
            dal.Delete(ma);
        }
    }
}
