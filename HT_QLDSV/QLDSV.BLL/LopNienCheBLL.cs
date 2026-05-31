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

        public string ValidateLopNienChe(
            string maLop, string tenLop, string nienKhoa, string maKhoa, string maGV, bool isUpdate)
        {
            maLop = maLop?.Trim() ?? "";
            tenLop = tenLop?.Trim() ?? "";
            nienKhoa = nienKhoa?.Trim() ?? "";
            maKhoa = maKhoa?.Trim() ?? "";
            maGV = maGV?.Trim() ?? "";

            if (string.IsNullOrEmpty(maLop))
                return "Mã lớp niên chế không được để trống.";

            if (maLop.Length > 10)
                return "Mã lớp niên chế không được vượt quá 20 ký tự.";

            if (string.IsNullOrEmpty(tenLop))
                return "Tên lớp niên chế không được để trống.";

            if (tenLop.Length > 50)
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

            if (string.IsNullOrEmpty(maKhoa))
                return "Vui lòng chọn khoa quản lý của lớp.";

            if (!dal.ExistsKhoa(maKhoa))
                return "Khoa được chọn không tồn tại trong hệ thống.";

            if (string.IsNullOrEmpty(maGV))
                return "Vui lòng chọn giảng viên cố vấn học tập.";

            if (!dal.ExistsGiangVien(maGV))
                return "Giảng viên cố vấn được chọn không tồn tại trong hệ thống.";

            if (isUpdate)
            {
                if (!dal.ExistsByMaLop(maLop))
                    return "Mã lớp niên chế không tồn tại, không thể cập nhật.";
            }
            else if (dal.ExistsByMaLop(maLop))
            {
                return $"Mã lớp niên chế '{maLop}' đã tồn tại trong hệ thống.";
            }

            return "";
        }

        public (bool success, string message) InsertLop(
            string maLop, string tenLop, string nienKhoa, string maKhoa, string maGV)
        {
            string error = ValidateLopNienChe(maLop, tenLop, nienKhoa, maKhoa, maGV, isUpdate: false);
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
            string error = ValidateLopNienChe(maLop, tenLop, nienKhoa, maKhoa, maGV, isUpdate: true);
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
