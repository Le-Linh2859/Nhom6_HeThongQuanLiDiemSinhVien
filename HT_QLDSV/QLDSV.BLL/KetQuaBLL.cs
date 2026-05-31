using System;
using System.Collections.Generic;
using System.Data;
using QLDSV.DAL;

namespace QLDSV.BLL
{
    public class KetQuaBLL
    {
        private readonly KetQuaDAL _dal = new KetQuaDAL();
        private static Dictionary<string, decimal> _tyLePhanTram;
        private static readonly object _tyLeLock = new object();

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

        /// <summary>Thông tin đầy đủ sinh viên (xuất bảng điểm).</summary>
        public DataTable GetThongTinSinhVienDayDu(string maSV)
            => _dal.GetThongTinSinhVienDayDu(maSV);

        /// <summary>Lấy danh sách năm học mà sinh viên có đăng ký lớp học phần.</summary>
        public DataTable GetNamHocBySinhVien(string maSV)
            => _dal.GetNamHocBySinhVien(maSV);

        public DataTable GetNamHocByGiangVien(string maGV)
            => _dal.GetNamHocByGiangVien(maGV);

        /// <summary>
        /// Lấy bảng điểm thô (CC, KT1, KT2, CK) của sinh viên theo năm học + học kỳ.
        /// Trả về: MaMon, TenMon, SoTC, DiemCC, DiemKT1, DiemKT2, DiemThi.
        /// </summary>
        public DataTable GetBangDiemSinhVien(string maSV, string maNamHoc, string maLoaiHK)
            => _dal.GetBangDiemSinhVien(maSV, maNamHoc, maLoaiHK);

        // ─── Lưu điểm (thêm mới hoặc cập nhật) ───────────────────────────────────
        public void LuuDiem(string maSV, string maLHP, string maLoaiDiem, decimal diem)
            => _dal.LuuHoacCapNhatDiem(maSV, maLHP, maLoaiDiem, diem);

        public void XoaDiem(string maSV, string maLHP, string maLoaiDiem)
            => _dal.XoaDiem(maSV, maLHP, maLoaiDiem);

        public void XoaTatCaDiemSinhVienLop(string maSV, string maLHP)
            => _dal.XoaTatCaDiemSinhVienLop(maSV, maLHP);

        public bool DaDuDiemThanhPhan(string maSV, string maLHP)
        {
            return _dal.KiemTraDiemTonTai(maSV, maLHP, "CC")
                && _dal.KiemTraDiemTonTai(maSV, maLHP, "KT1")
                && _dal.KiemTraDiemTonTai(maSV, maLHP, "KT2");
        }


        //Lấy danh sách năm học 
        public DataTable GetDanhSachNamHoc()
            => _dal.GetDanhSachNamHoc();

        //Lấy danh sách loại học kỳ 
        public DataTable GetDanhSachLoaiHocKy()
            => _dal.GetDanhSachLoaiHocKy();

        //Lấy danh sách lớp niên chế 
        public DataTable GetDanhSachLopNienChe()
            => _dal.GetDanhSachLopNienChe();

        //Lấy danh sách tổng hợp kết quả học tập 
        public DataTable GetKetQuaHocTapAdmin(
            string maNamHoc, string maLoaiHK, string maLopNienChe, string keyword)
            => _dal.GetKetQuaHocTapAdmin(maNamHoc, maLoaiHK, maLopNienChe, keyword);

        //Lấy chi tiết điểm từng môn của một sinh viên
        public DataTable GetChiTietDiemSinhVien(string maSV, string maNamHoc, string maLoaiHK)
            => _dal.GetChiTietDiemSinhVien(maSV, maNamHoc, maLoaiHK);

        

        /// Quy đổi điểm hệ 10 sang điểm chữ 
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

        /// Quy đổi điểm hệ 10 - điểm hệ 4 theo quy định
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

        /// Quy đổi GPA hệ 4 → xếp loại học lực.
        public static string XepLoaiHocLuc(double gpa4)
        {
            if (gpa4 >= 3.6) return "Xuất sắc";
            if (gpa4 >= 3.2) return "Giỏi";
            if (gpa4 >= 2.5) return "Khá";
            if (gpa4 >= 2.0) return "Trung bình";
            if (gpa4 >= 1.0) return "Yếu";
            return "Kém";
        }

        
        /// Tính điểm tổng kết hệ 10 từ các điểm thành phần.
        /// Tỷ lệ lấy từ bảng LoaiDiem (TyLePhanTram).
        
        public static double TinhDiemTongKet(double cc, double kt1, double kt2, double ck)
        {
            decimal tong =
                (decimal)cc * LayTyLe("CC") +
                (decimal)kt1 * LayTyLe("KT1") +
                (decimal)kt2 * LayTyLe("KT2") +
                (decimal)ck * LayTyLe("CK");
            return Math.Round((double)tong, 1);
        }

        
        /// Tính điểm tổng kết từ một dòng bảng điểm (null nếu chưa đủ 4 điểm thành phần).
        
        public static double? TryTinhDiemTongKet(DataRow row,
            string colCC = "DiemCC", string colKT1 = "DiemKT1",
            string colKT2 = "DiemKT2", string colThi = "DiemThi")
        {
            if (row == null) return null;

            double? cc  = ToNullableDouble(row[colCC]);
            double? kt1 = ToNullableDouble(row[colKT1]);
            double? kt2 = ToNullableDouble(row[colKT2]);
            double? ck  = ToNullableDouble(row[colThi]);

            if (!cc.HasValue || !kt1.HasValue || !kt2.HasValue || !ck.HasValue)
                return null;

            return TinhDiemTongKet(cc.Value, kt1.Value, kt2.Value, ck.Value);
        }

        
        /// Gom các dòng theo MaMon (bỏ qua MaLHP). Mỗi mã môn chỉ giữ một SoTC.
        
        private static Dictionary<string, (double? diemTk, int soTC, DataRow row)> GomDiemTheoMaMon(
            DataTable dt,
            string colMaMon = "MaMon", string colSoTC = "SoTC",
            string colCC = "DiemCC", string colKT1 = "DiemKT1",
            string colKT2 = "DiemKT2", string colThi = "DiemThi")
        {
            var theoMon = new Dictionary<string, (double? diemTk, int soTC, DataRow row)>(
                StringComparer.OrdinalIgnoreCase);

            if (dt == null) return theoMon;

            foreach (DataRow row in dt.Rows)
            {
                string maMon = row[colMaMon]?.ToString()?.Trim() ?? "";
                if (string.IsNullOrEmpty(maMon)) continue;

                int soTC = Convert.ToInt32(row[colSoTC]);
                double? tk = TryTinhDiemTongKet(row, colCC, colKT1, colKT2, colThi);

                if (!theoMon.TryGetValue(maMon, out var hienTai))
                {
                    theoMon[maMon] = (tk, soTC, row);
                    continue;
                }

                if (tk.HasValue && (!hienTai.diemTk.HasValue || tk.Value > hienTai.diemTk.Value))
                    theoMon[maMon] = (tk, hienTai.soTC, row);
            }

            return theoMon;
        }

        
        public static bool LaDongTinhTichLuy(DataRow row, DataTable dt,
            string colMaMon = "MaMon", string colMaLHP = "MaLHP",
            string colSoTC = "SoTC", string colCC = "DiemCC", string colKT1 = "DiemKT1",
            string colKT2 = "DiemKT2", string colThi = "DiemThi")
        {
            if (row == null || dt == null) return false;

            double? tk = TryTinhDiemTongKet(row, colCC, colKT1, colKT2, colThi);
            if (!tk.HasValue) return false;

            if (!dt.Columns.Contains(colMaLHP))
                return true;

            var theoMon = GomDiemTheoMaMon(dt, colMaMon, colSoTC, colCC, colKT1, colKT2, colThi);
            string maMon = row[colMaMon]?.ToString()?.Trim() ?? "";
            if (!theoMon.TryGetValue(maMon, out var mon) || !mon.diemTk.HasValue || mon.row == null)
                return false;

            string maLhp = row[colMaLHP]?.ToString()?.Trim() ?? "";
            string maLhpTinh = mon.row[colMaLHP]?.ToString()?.Trim() ?? "";
            return string.Equals(maLhp, maLhpTinh, StringComparison.OrdinalIgnoreCase);
        }

        
        public static int DemTinChiTheoMaMon(DataTable dt, bool chiMonDaCoDiemTongKet = true,
            string colMaMon = "MaMon", string colSoTC = "SoTC",
            string colCC = "DiemCC", string colKT1 = "DiemKT1",
            string colKT2 = "DiemKT2", string colThi = "DiemThi")
        {
            var theoMon = GomDiemTheoMaMon(dt, colMaMon, colSoTC, colCC, colKT1, colKT2, colThi);
            int tong = 0;
            foreach (var mon in theoMon.Values)
            {
                if (chiMonDaCoDiemTongKet && !mon.diemTk.HasValue) continue;
                tong += mon.soTC;
            }
            return tong;
        }

        
        
        public static DataTable LocDiemTotNhatTheoMon(DataTable dt,
            string colMaMon = "MaMon", string colSoTC = "SoTC",
            string colCC = "DiemCC", string colKT1 = "DiemKT1",
            string colKT2 = "DiemKT2", string colThi = "DiemThi")
        {
            if (dt == null) return dt;

            var theoMon = GomDiemTheoMaMon(dt, colMaMon, colSoTC, colCC, colKT1, colKT2, colThi);
            var result = dt.Clone();
            foreach (var mon in theoMon.Values)
            {
                if (mon.diemTk.HasValue && mon.row != null)
                    result.ImportRow(mon.row);
            }

            return result;
        }

        
        
        public static KetQuaTongHop TinhGPATuBangDiem(DataTable dt,
            string colMaMon = "MaMon", string colSoTC = "SoTC",
            string colCC = "DiemCC", string colKT1 = "DiemKT1",
            string colKT2 = "DiemKT2", string colThi = "DiemThi")
        {
            var ketQua = new KetQuaTongHop();
            if (dt == null || dt.Rows.Count == 0) return ketQua;

            var theoMon = GomDiemTheoMaMon(dt, colMaMon, colSoTC, colCC, colKT1, colKT2, colThi);

            int tinChi = 0;
            double sumWeighted10 = 0;
            double sumWeighted4 = 0;

            foreach (var mon in theoMon.Values)
            {
                if (!mon.diemTk.HasValue) continue;

                tinChi += mon.soTC;
                sumWeighted10 += mon.soTC * mon.diemTk.Value;
                sumWeighted4 += mon.soTC * QuyDoiHe4(mon.diemTk.Value);
            }

            if (tinChi == 0) return ketQua;

            ketQua.CoDuLieu = true;
            ketQua.TinChiTichLuy = tinChi;
            ketQua.DiemHe10 = Math.Round(sumWeighted10 / tinChi, 1);
            ketQua.DiemHe4 = Math.Round(sumWeighted4 / tinChi, 2);
            ketQua.XepLoai = XepLoaiHocLuc(ketQua.DiemHe4);
            return ketQua;
        }

        
        public static double TinhGPAHe4TuCacMon(System.Collections.Generic.IEnumerable<(double diemHe10, int soTC)> cacMon)
        {
            int tinChi = 0;
            double sumWeighted = 0;
            foreach (var mon in cacMon)
            {
                tinChi += mon.soTC;
                sumWeighted += mon.soTC * QuyDoiHe4(mon.diemHe10);
            }
            return tinChi > 0 ? Math.Round(sumWeighted / tinChi, 2) : 0;
        }

        private static double? ToNullableDouble(object val)
        {
            if (val == null || val == DBNull.Value) return null;
            return double.TryParse(val.ToString(), out double d) ? d : (double?)null;
        }

        private static decimal LayTyLe(string maLoaiDiem)
        {
            if (_tyLePhanTram == null)
            {
                lock (_tyLeLock)
                {
                    if (_tyLePhanTram == null)
                        _tyLePhanTram = new KetQuaDAL().GetTyLePhanTram();
                }
            }

            if (_tyLePhanTram.TryGetValue(maLoaiDiem, out decimal tyLePhanTram))
                return tyLePhanTram / 100m;

            throw new InvalidOperationException(
                $"Không tìm thấy tỷ lệ phần trăm cho loại điểm '{maLoaiDiem}' trong bảng LoaiDiem.");
        }
    }

    public class KetQuaTongHop
    {
        public bool CoDuLieu { get; set; }
        public int TinChiTichLuy { get; set; }
        public double DiemHe10 { get; set; }
        public double DiemHe4 { get; set; }
        public string XepLoai { get; set; }
    }
}
