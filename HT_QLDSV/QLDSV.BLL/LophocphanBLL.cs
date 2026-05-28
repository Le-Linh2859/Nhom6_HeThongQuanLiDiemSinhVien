using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public bool Them(
            string ma,
            string ten,
            int caHoc,
            string thu,
            string phong,
            string trangThai,
            string maMon,
            string maGV,
            string maHKNH,
            out string message)
        {
            if (dal.CheckMaExists(ma))
            {
                message = "Mã lớp học phần đã tồn tại";
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
                maHKNH);

            message = "Thêm lớp học phần thành công";

            return true;
        }

        public bool Sua(
            string ma,
            string ten,
            int caHoc,
            string thu,
            string phong,
            string trangThai,
            string maMon,
            string maGV,
            out string message)
        {
            dal.Update(
                ma,
                ten,
                caHoc,
                thu,
                phong,
                trangThai,
                maMon,
                maGV);

            message = "Cập nhật lớp học phần thành công";

            return true;
        }

        public void Xoa(string ma)
        {
            dal.Delete(ma);
        }
    }
}


