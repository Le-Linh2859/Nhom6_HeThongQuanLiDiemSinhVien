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
        public void LuuDiem(string maSV, string maLHP, string maLoaiDiem, decimal diem)
            => _dal.LuuHoacCapNhatDiem(maSV, maLHP, maLoaiDiem, diem);

        public bool DaDuDiemThanhPhan(string maSV, string maLHP)
        {
            return _dal.KiemTraDiemTonTai(maSV, maLHP, "CC")
                && _dal.KiemTraDiemTonTai(maSV, maLHP, "KT1")
                && _dal.KiemTraDiemTonTai(maSV, maLHP, "KT2");
        }

        // ═══════════════════════════════════════════════════════════════════════════
        // ADMIN – Theo dõi kết quả học tập
        // ═══════════════════════════════════════════════════════════════════════════

        /// <summary>Lấy danh sách năm học cho ComboBox (Admin).</summary>
        public DataTable GetDanhSachNamHoc()
            => _dal.GetDanhSachNamHoc();

        /// <summary>Lấy danh sách loại học kỳ cho ComboBox (Admin).</summary>
        public DataTable GetDanhSachLoaiHocKy()
            => _dal.GetDanhSachLoaiHocKy();

        /// <summary>Lấy danh sách lớp niên chế cho ComboBox (Admin).</summary>
        public DataTable GetDanhSachLopNienChe()
            => _dal.GetDanhSachLopNienChe();

        /// <summary>Lấy danh sách tổng hợp kết quả học tập theo bộ lọc (Admin).</summary>
        public DataTable GetKetQuaHocTapAdmin(
            string maNamHoc, string maLoaiHK, string maLopNienChe, string keyword)
            => _dal.GetKetQuaHocTapAdmin(maNamHoc, maLoaiHK, maLopNienChe, keyword);

        /// <summary>Lấy chi tiết điểm từng môn của một sinh viên (Admin).</summary>
        public DataTable GetChiTietDiemSinhVien(string maSV, string maNamHoc, string maLoaiHK)
            => _dal.GetChiTietDiemSinhVien(maSV, maNamHoc, maLoaiHK);

        // ─── Quy đổi điểm (Business Logic) ──────────────────────────────────────

        /// <summary>Quy đổi điểm hệ 10 → điểm chữ theo quy định.</summary>
        public static string QuyDoiDiemChu(double diem)
        {
            if (diem >= 9.5) return "A+";
            if (diem >= 8.5) return "A";
            if (diem >= 8.0) return "B+";
            if (diem >= 7.0) return "B";
            if (diem >= 6.5) return "C+";
            if (diem >= 5.5) return "C";
            if (diem >= 5.0) return "D+";
            if (diem >= 4.0) return "D";
            return "F";
        }

        /// <summary>Quy đổi điểm hệ 10 → điểm hệ 4 theo quy định.</summary>
        public static double QuyDoiHe4(double diem)
        {
            if (diem >= 9.5) return 4.0;
            if (diem >= 8.5) return 4.0;
            if (diem >= 8.0) return 3.5;
            if (diem >= 7.0) return 3.0;
            if (diem >= 6.5) return 2.5;
            if (diem >= 5.5) return 2.0;
            if (diem >= 5.0) return 1.5;
            if (diem >= 4.0) return 1.0;
            return 0.0;
        }

        /// <summary>Quy đổi GPA hệ 4 → xếp loại học lực.</summary>
        public static string XepLoaiHocLuc(double gpa4)
        {
            if (gpa4 >= 3.6) return "Xuất sắc";
            if (gpa4 >= 3.2) return "Giỏi";
            if (gpa4 >= 2.5) return "Khá";
            if (gpa4 >= 2.0) return "Trung bình";
            if (gpa4 >= 1.0) return "Yếu";
            return "Kém";
        }

        /// <summary>
        /// Tính điểm tổng kết hệ 10 từ các điểm thành phần.
        /// Công thức: CC*0.1 + KT1*0.15 + KT2*0.15 + CK*0.6
        /// </summary>
        public static double TinhDiemTongKet(double cc, double kt1, double kt2, double ck)
            => Math.Round((cc * 0.1) + (kt1 * 0.15) + (kt2 * 0.15) + (ck * 0.6), 2);
    }
}
