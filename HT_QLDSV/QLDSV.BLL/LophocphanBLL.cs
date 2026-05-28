using System;
using System.Data;
using QLDSV.DAL;

namespace QLDSV.BLL
{
    public class LopHocPhanBLL
    {
        private LopHocPhanDAL dal = new LopHocPhanDAL();

        /// <summary>
        /// Mã học kỳ - năm học mặc định dùng khi tạo lớp học phần mới.
        /// </summary>
        private const string MaHKNH_Default = "HK007";

        public DataTable GetDanhSachLopHocPhan(
            string keyword,
            string maKhoa,
            string maMon)
        {
            return dal.GetDanhSach(keyword, maKhoa, maMon);
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

        /// <summary>
        /// Thêm lớp học phần mới. Nhận thu và caHoc trực tiếp từ UI.
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
            out string message)
        {
            if (dal.CheckMaExists(ma))
            {
                message = "Mã lớp học phần đã tồn tại";
                return false;
            }

            dal.Insert(ma, ten, caHoc, thu, phong, trangThai, maMon, maGV, MaHKNH_Default);

            message = "Thêm lớp học phần thành công";
            return true;
        }

        /// <summary>
        /// Cập nhật lớp học phần. Nhận thu và caHoc trực tiếp từ UI.
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
            out string message)
        {
            dal.Update(ma, ten, caHoc, thu, phong, trangThai, maMon, maGV);

            message = "Cập nhật lớp học phần thành công";
            return true;
        }

        public void Xoa(string ma)
        {
            dal.Delete(ma);
        }
    }
}


