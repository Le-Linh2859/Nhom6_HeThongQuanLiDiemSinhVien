using QLDSV.DAL;
using System;
using System.Data;
using System.Text.RegularExpressions;

namespace QLDSV.BLL
{
    public class LopNienCheBLL
    {
        private readonly LopNienCheDAL dal = new LopNienCheDAL();

        public DataTable GetLopNienChe() => dal.LoadLopNienChe();

        public DataTable GetKhoa() => dal.LoadKhoa();

        public DataTable GetGiangVien() => dal.LoadGiangVien();

        public bool CheckKeyExist(string maLop)
        {
            if (string.IsNullOrWhiteSpace(maLop)) return false;
            return dal.ExistsByMaLop(maLop.Trim());
        }

        /// <summary>Validate khi thêm mới — gồm cả mã lớp (người dùng nhập).</summary>
        public string ValidateForInsert(string maLop, string tenLop, string nienKhoa)
        {
            maLop = maLop?.Trim() ?? "";

            string common = ValidateThongTinChung(tenLop, nienKhoa);
            if (!string.IsNullOrEmpty(common))
                return common;

            if (string.IsNullOrEmpty(maLop))
                return "Mã lớp niên chế không được để trống.";

            if (maLop.Length > 20)
                return "Mã lớp niên chế không được vượt quá 20 ký tự.";

            if (dal.ExistsByMaLop(maLop))
                return $"Mã lớp niên chế '{maLop}' đã tồn tại trong hệ thống.";

            return "";
        }

        /// <summary>Validate khi sửa — mã lớp không đổi, không kiểm tra mã.</summary>
        public string ValidateForUpdate(string tenLop, string nienKhoa)
        {
            return ValidateThongTinChung(tenLop, nienKhoa);
        }

        private static string ValidateThongTinChung(string tenLop, string nienKhoa)
        {
            tenLop = tenLop?.Trim() ?? "";
            nienKhoa = nienKhoa?.Trim() ?? "";

            if (string.IsNullOrEmpty(tenLop))
                return "Tên lớp niên chế không được để trống.";

            if (tenLop.Length > 100)
                return "Tên lớp niên chế không được vượt quá 100 ký tự.";

            if (string.IsNullOrEmpty(nienKhoa))
                return "Niên khóa không được để trống.";

            if (!Regex.IsMatch(nienKhoa, @"^\d{4}-\d{4}$"))
                return "Niên khóa phải có định dạng YYYY-YYYY (ví dụ: 2022-2026).";

            if (int.TryParse(nienKhoa.Substring(0, 4), out int namBatDau) &&
                int.TryParse(nienKhoa.Substring(5, 4), out int namKetThuc) &&
                namBatDau >= namKetThuc)
            {
                return "Năm bắt đầu niên khóa phải nhỏ hơn năm kết thúc.";
            }

            return "";
        }

        public (bool success, string message) InsertLop(
            string maLop, string tenLop, string nienKhoa, string maKhoa, string maGV)
        {
            string error = ValidateForInsert(maLop, tenLop, nienKhoa);
            if (!string.IsNullOrEmpty(error))
                return (false, error);

            try
            {
                dal.InsertLopNienChe(
                    maLop.Trim(), tenLop.Trim(), nienKhoa.Trim(), maKhoa.Trim(), maGV.Trim());
                return (true, "Thêm mới lớp niên chế thành công!");
            }
            catch (Exception ex)
            {
                return (false, "Lỗi thêm lớp niên chế: " + ex.Message);
            }
        }

        
        public (bool success, string message) UpdateLop(
            string maLop, string tenLop, string nienKhoa, string maKhoa, string maGV)
        {
            if (string.IsNullOrWhiteSpace(maLop))
                return (false, "Không xác định được lớp cần cập nhật.");

            string error = ValidateForUpdate(tenLop, nienKhoa);
            if (!string.IsNullOrEmpty(error))
                return (false, error);

            try
            {
                dal.UpdateLopNienChe(
                    maLop.Trim(), tenLop.Trim(), nienKhoa.Trim(), maKhoa.Trim(), maGV.Trim());
                return (true, "Cập nhật thông tin lớp niên chế thành công!");
            }
            catch (Exception ex)
            {
                return (false, "Lỗi cập nhật lớp niên chế: " + ex.Message);
            }
        }
    }
}
