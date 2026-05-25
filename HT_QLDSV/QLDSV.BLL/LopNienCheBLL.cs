using QLDSV.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLDSV.BLL
{
    public class LopNienCheBLL
    {
        private LopNienCheDAL dal = new LopNienCheDAL();

        // 1. Lấy danh sách lớp niên chế
        public DataTable GetLopNienChe()
        {
            return dal.LoadLopNienChe();
        }

        // 2. Lấy danh sách khoa
        public DataTable GetKhoa()
        {
            return dal.LoadKhoa();
        }

        // 3. Lấy danh sách giảng viên
        public DataTable GetGiangVien()
        {
            return dal.LoadGiangVien();
        }

        // 4. Thêm lớp mới
        public void InsertLop(string maLop, string tenLop, string nienKhoa, string maKhoa, string maGV)
        {
            dal.InsertLopNienChe(maLop, tenLop, nienKhoa, maKhoa, maGV);
        }

        // 5. Cập nhật lớp
        public void UpdateLop(string maLop, string tenLop, string nienKhoa, string maKhoa, string maGV)
        {
            dal.UpdateLopNienChe(maLop, tenLop, nienKhoa, maKhoa, maGV);
        }

        // 6. Xóa lớp
        public void DeleteLop(string maLop)
        {
            dal.DeleteLopNienChe(maLop);
        }

        // 7. Kiểm tra sự tồn tại của mã lớp
        public bool CheckKeyExist(string maLop)
        {
            string sql = $"SELECT MaLopNienChe FROM LopNienChe WHERE MaLopNienChe = '{maLop}'";
            return dal.CheckKey(sql);
        }
    }
}
