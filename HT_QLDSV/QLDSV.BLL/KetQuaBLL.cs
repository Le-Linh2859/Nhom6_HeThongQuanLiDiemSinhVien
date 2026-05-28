using System;
using System.Data;
using QLDSV.DAL;

namespace QLDSV.BLL
{
    public class KetQuaBLL
    {
        private readonly KetQuaDAL _dal = new KetQuaDAL();

        // ─── Danh mục ─────────────────────────────────────────────────────────────
        public DataTable GetNamHoc()       => _dal.GetNamHoc();
        public DataTable GetLoaiHocKy()    => _dal.GetLoaiHocKy();

        // ─── Lớp học phần ─────────────────────────────────────────────────────────
        public DataTable GetLopHocPhan(string maNamHoc, string maLoaiHK, string maGV)
            => _dal.GetLopHocPhanByGiangVien(maNamHoc, maLoaiHK, maGV);

        // ─── Sinh viên ────────────────────────────────────────────────────────────
        public DataTable GetSinhVien(string maLHP)
            => _dal.GetSinhVienByLopHocPhan(maLHP);

        // ─── Bảng điểm tổng hợp ──────────────────────────────────────────────────
        public DataTable GetBangDiem(string maLHP)
            => _dal.GetDiemByLopHocPhan(maLHP);

        // ─── Điểm của một sinh viên ───────────────────────────────────────────────
        public DataTable GetDiemSinhVien(string maSV, string maLHP)
            => _dal.GetDiemSinhVien(maSV, maLHP);

        // ─── Lấy mã giảng viên ────────────────────────────────────────────────────
        public string GetMaGV(string maTaiKhoan, string tenDangNhap)
        {
            string maGV = _dal.GetMaGVByTaiKhoan(maTaiKhoan);
            if (string.IsNullOrEmpty(maGV))
                maGV = _dal.GetMaGVByTenDangNhap(tenDangNhap);
            return maGV;
        }

        // ═══════════════════════════════════════════════════════════════════════════
        // SINH VIÊN – Kết quả học tập
        // ═══════════════════════════════════════════════════════════════════════════

        /// <summary>Lấy MaSV và HoTen từ MaTaiKhoan của sinh viên đang đăng nhập.</summary>
        public DataTable GetThongTinSinhVien(string maTaiKhoan)
            => _dal.GetThongTinSinhVienByTaiKhoan(maTaiKhoan);

        /// <summary>Lấy danh sách năm học mà sinh viên có đăng ký lớp học phần.</summary>
        public DataTable GetNamHocBySinhVien(string maSV)
            => _dal.GetNamHocBySinhVien(maSV);

        /// <summary>
        /// Lấy bảng điểm thô (CC, KT1, KT2, CK) của sinh viên theo năm học + học kỳ.
        /// Trả về: MaMon, TenMon, SoTC, DiemCC, DiemKT1, DiemKT2, DiemThi.
        /// </summary>
        public DataTable GetBangDiemSinhVien(string maSV, string maNamHoc, string maLoaiHK)
            => _dal.GetBangDiemSinhVien(maSV, maNamHoc, maLoaiHK);

        // ─── Lưu điểm (thêm mới hoặc cập nhật) ───────────────────────────────────
        /// <summary>
        /// Lưu một loại điểm cho sinh viên. Nếu đã tồn tại thì UPDATE, chưa có thì INSERT.
        /// </summary>
        public void LuuDiem(string maSV, string maLHP, string maLoaiDiem, decimal diem)
            => _dal.LuuHoacCapNhatDiem(maSV, maLHP, maLoaiDiem, diem);

        // ─── Kiểm tra sinh viên đã có đủ điểm thành phần chưa ────────────────────
        /// <summary>
        /// Trả về true nếu sinh viên đã có đủ CC + KT1 + KT2 trong lớp học phần.
        /// </summary>
        public bool DaDuDiemThanhPhan(string maSV, string maLHP)
        {
            return _dal.KiemTraDiemTonTai(maSV, maLHP, "CC")
                && _dal.KiemTraDiemTonTai(maSV, maLHP, "KT1")
                && _dal.KiemTraDiemTonTai(maSV, maLHP, "KT2");
        }
    }
}
