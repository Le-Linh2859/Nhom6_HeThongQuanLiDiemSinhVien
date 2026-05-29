using System;
using System.Data;

namespace QLDSV.DAL
{
    public class CanhBaoHocVuDAL
    {
        // =====================================================
        // LOAD DANH SÁCH CẢNH BÁO HỌC VỤ (cho form hiển thị)
        // =====================================================
        public DataTable GetDanhSachCanhBao()
        {
            try
            {
                string sql = @"
            SELECT
                cbsv.MaCanhBao,
                sv.MaSV,
                sv.HoTen,
                k.TenKhoa,
                lnc.MaLopNienChe,
                hk.TenLoaiHK,
                nh.TenNamHoc,
                cbhv.Noidung,
                cbhv.LoaiCanhBao,
                cbsv.ThoiDiem,
                cbsv.LanThu
            FROM CanhBao_SinhVien cbsv
            INNER JOIN CanhBaoHocVu cbhv
                ON cbsv.MaCanhBao = cbhv.MaCanhBao
            INNER JOIN SinhVien sv
                ON cbsv.MaSV = sv.MaSV
            INNER JOIN LopNienChe lnc
                ON sv.MaLopNienChe = lnc.MaLopNienChe
            INNER JOIN Khoa k
                ON lnc.MaKhoa = k.MaKhoa
            INNER JOIN HocKy_NamHoc hknh
                ON cbsv.MaHKNH = hknh.MaHKNH
            INNER JOIN LoaiHocKy hk
                ON hknh.MaLoaiHK = hk.MaLoaiHK
            INNER JOIN NamHoc nh
                ON hknh.MaNamHoc = nh.MaNamHoc
        ";
                return Connection.GetDataToTable(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Lỗi DAL - GetDanhSachCanhBao: " + ex.Message);
            }
        }

        // =====================================================
        // LOAD NĂM HỌC
        // =====================================================
        public DataTable GetNamHoc()
        {
            try
            {
                string sql = @"
                    SELECT MaNamHoc, TenNamHoc
                    FROM NamHoc
                ";
                return Connection.GetDataToTable(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Lỗi DAL - Load năm học: " + ex.Message);
            }
        }

        // =====================================================
        // LOAD HỌC KỲ
        // =====================================================
        public DataTable GetHocKy()
        {
            try
            {
                string sql = @"
                    SELECT MaLoaiHK, TenLoaiHK
                    FROM LoaiHocKy
                ";
                return Connection.GetDataToTable(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Lỗi DAL - Load học kỳ: " + ex.Message);
            }
        }

        // =====================================================
        // LOAD LỚP NIÊN CHẾ
        // =====================================================
        public DataTable GetLopNienChe()
        {
            try
            {
                string sql = @"
                    SELECT MaLopNienChe, TenLop
                    FROM LopNienChe
                ";
                return Connection.GetDataToTable(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Lỗi DAL - Load lớp niên chế: " + ex.Message);
            }
        }

        // =====================================================
        // KIỂM TRA KẾT NỐI
        // =====================================================
        public bool TestConnection()
        {
            try
            {
                Connection.KetNoi();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // =====================================================
        // PHÁT HIỆN CẢNH BÁO TỰ ĐỘNG
        // =====================================================

        /// <summary>
        /// Kiểm tra học kỳ có phải học kỳ hè không (Hocky_3)
        /// </summary>
        public bool IsHocKyHe(string maHKNH)
        {
            try
            {
                string sql = $@"
                    SELECT COUNT(1)
                    FROM HocKy_NamHoc hknh
                    INNER JOIN LoaiHocKy lhk
                        ON hknh.MaLoaiHK = lhk.MaLoaiHK
                    WHERE hknh.MaHKNH  = '{maHKNH}'
                      AND lhk.MaLoaiHK = 'Hocky_3'
                ";
                object result = Connection.ExecuteScalar(sql);
                return Convert.ToInt32(result) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Lỗi DAL - IsHocKyHe: " + ex.Message);
            }
        }

        /// <summary>
        /// TH1: SV có khối lượng = 0 TC trong học kỳ
        /// </summary>
        public DataTable GetSinhVienKhoiLuongZero(string maHKNH)
        {
            try
            {
                string sql = $@"
SELECT
    sv.MaSV,
    sv.HoTen,
    sv.MaLopNienChe,
    '{maHKNH}' AS MaHKNH
FROM SinhVien sv
INNER JOIN HocKy_NamHoc hknh
    ON hknh.MaHKNH = '{maHKNH}'
INNER JOIN NamHoc nh
    ON hknh.MaNamHoc = nh.MaNamHoc
WHERE
    CAST(LEFT(sv.NienKhoa,4) AS INT)
        <= CAST(SUBSTRING(nh.TenNamHoc,9,4) AS INT)

AND CAST(RIGHT(sv.NienKhoa,4) AS INT)
        >= CAST(SUBSTRING(nh.TenNamHoc,14,4) AS INT)

AND sv.MaSV NOT IN
(
    SELECT DISTINCT dkl.MaSV
    FROM DangKyLop dkl
    INNER JOIN LopHocPhan lhp
        ON dkl.MaLHP = lhp.MaLHP
    WHERE lhp.MaHKNH = '{maHKNH}'
      AND dkl.TrangThai = 1
)
";
                return Connection.GetDataToTable(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Lỗi DAL - GetSinhVienKhoiLuongZero: " + ex.Message);
            }
        }

        /// <summary>
        /// TH2: SV có TBC học kỳ dưới 1.5
        /// </summary>
        public DataTable GetSinhVienDiemThapDuoi1_5(string maHKNH)
        {
            try
            {
                string sql = $@"
                    WITH DiemMon AS (
                        SELECT
                            dkl.MaSV,
                            lhp.MaMon,
                            lhp.MaHKNH,
                            SUM(kq.Diem * ld.TyLePhanTram / 100.0) AS DiemMon
                        FROM DangKyLop dkl
                        INNER JOIN LopHocPhan lhp
                            ON dkl.MaLHP = lhp.MaLHP
                        INNER JOIN KetQua kq
                            ON kq.MaSV  = dkl.MaSV
                           AND kq.MaLHP = dkl.MaLHP
                        INNER JOIN LoaiDiem ld
                            ON ld.MaLoaiDiem = kq.MaLoaiDiem
                        WHERE lhp.MaHKNH   = '{maHKNH}'
                          AND dkl.TrangThai = 1
                        GROUP BY dkl.MaSV, lhp.MaMon, lhp.MaHKNH
                    ),
                    TBCHocKy AS (
                        SELECT
                            MaSV,
                            MaHKNH,
                            AVG(DiemMon) AS TBCHocKy,
                            COUNT(*)     AS SoMon
                        FROM DiemMon
                        GROUP BY MaSV, MaHKNH
                    )
                    SELECT
                        sv.MaSV,
                        sv.HoTen,
                        sv.MaLopNienChe,
                        tbc.MaHKNH,
                        ROUND(tbc.TBCHocKy, 2) AS TBCHocKy,
                        tbc.SoMon
FROM TBCHocKy tbc
INNER JOIN SinhVien sv
    ON sv.MaSV = tbc.MaSV

INNER JOIN HocKy_NamHoc hknh
    ON hknh.MaHKNH = tbc.MaHKNH

INNER JOIN NamHoc nh
    ON nh.MaNamHoc = hknh.MaNamHoc

WHERE tbc.TBCHocKy < 1.5

AND CAST(LEFT(sv.NienKhoa,4) AS INT)
        <= CAST(SUBSTRING(nh.TenNamHoc,9,4) AS INT)

AND CAST(RIGHT(sv.NienKhoa,4) AS INT)
        >= CAST(SUBSTRING(nh.TenNamHoc,14,4) AS INT)
                ";
                return Connection.GetDataToTable(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Lỗi DAL - GetSinhVienDiemThapDuoi1_5: " + ex.Message);
            }
        }

        /// <summary>
        /// Kiểm tra SV đã có cảnh báo chưa, trả về LanThu (0 = chưa có)
        /// </summary>
        public int GetLanThuCanhBao(string maSV, string maHKNH, int loaiCanhBao)
        {
            try
            {
                string sql = $@"
                    SELECT ISNULL(MAX(cbsv.LanThu), 0)
                    FROM CanhBao_SinhVien cbsv
                    INNER JOIN CanhBaoHocVu cbhv
                        ON cbsv.MaCanhBao = cbhv.MaCanhBao
                    WHERE cbsv.MaSV        = '{maSV}'
                      AND cbsv.MaHKNH      = '{maHKNH}'
                      AND cbhv.LoaiCanhBao  = {loaiCanhBao}
                ";
                object result = Connection.ExecuteScalar(sql);
                return result == null ? 0 : Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Lỗi DAL - GetLanThuCanhBao: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy MaCanhBao theo LoaiCanhBao
        /// </summary>
        public string GetMaCanhBaoByLoai(int loaiCanhBao)
        {
            try
            {
                string sql = $@"
                    SELECT TOP 1 MaCanhBao
                    FROM CanhBaoHocVu
                    WHERE LoaiCanhBao = {loaiCanhBao}
                ";
                object result = Connection.ExecuteScalar(sql);
                return result?.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Lỗi DAL - GetMaCanhBaoByLoai: " + ex.Message);
            }
        }

        /// <summary>
        /// Insert cảnh báo mới
        /// </summary>
        public void InsertCanhBaoSinhVien(
            string maSV, string maCanhBao, string maHKNH, int lanThu)
        {
            try
            {
                string sql = $@"
                    INSERT INTO CanhBao_SinhVien
                        (MaSV, MaCanhBao, MaHKNH, ThoiDiem, LanThu)
                    VALUES
                        ('{maSV}', '{maCanhBao}', '{maHKNH}', GETDATE(), {lanThu})
                ";
                Connection.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Lỗi DAL - InsertCanhBaoSinhVien: " + ex.Message);
            }
        }

        /// <summary>
        /// Cập nhật LanThu tăng lên
        /// </summary>
        public void UpdateLanThuCanhBao(
            string maSV, string maCanhBao, string maHKNH)
        {
            try
            {
                string sql = $@"
                    UPDATE CanhBao_SinhVien
                    SET LanThu   = LanThu + 1,
                        ThoiDiem = GETDATE()
                    WHERE MaSV      = '{maSV}'
                      AND MaCanhBao = '{maCanhBao}'
                      AND MaHKNH    = '{maHKNH}'
                ";
                Connection.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Lỗi DAL - UpdateLanThuCanhBao: " + ex.Message);
            }
        }

        /// <summary>
        /// Lấy danh sách học kỳ đang mở (TrangThai = 'DangMo')
        /// </summary>
        public DataTable GetHocKyDangHoatDong()
        {
            try
            {
                string sql = @"
                    SELECT
                        hknh.MaHKNH,
                        lhk.TenLoaiHK,
                        nh.TenNamHoc
                    FROM HocKy_NamHoc hknh
                    INNER JOIN LoaiHocKy lhk
                        ON hknh.MaLoaiHK = lhk.MaLoaiHK
                    INNER JOIN NamHoc nh
                        ON hknh.MaNamHoc = nh.MaNamHoc
                    WHERE hknh.MaHKNH IN (
                        SELECT DISTINCT MaHKNH
                        FROM LopHocPhan
                        WHERE TrangThai = 'DangMo'
                    )
                ";
                return Connection.GetDataToTable(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Lỗi DAL - GetHocKyDangHoatDong: " + ex.Message);
            }

        }
        public DataTable GetCanhBaoBySinhVien(string maSV)
        {
            try
            {
                string sql = $@"
        SELECT
            cbsv.MaCanhBao,
            hk.TenLoaiHK,
            nh.TenNamHoc,
            cbhv.Noidung,
            cbhv.LoaiCanhBao,
            cbsv.ThoiDiem,
            cbsv.LanThu
        FROM CanhBao_SinhVien cbsv
        INNER JOIN CanhBaoHocVu cbhv
            ON cbsv.MaCanhBao = cbhv.MaCanhBao
        INNER JOIN HocKy_NamHoc hknh
            ON cbsv.MaHKNH = hknh.MaHKNH
        INNER JOIN LoaiHocKy hk
            ON hknh.MaLoaiHK = hk.MaLoaiHK
        INNER JOIN NamHoc nh
            ON hknh.MaNamHoc = nh.MaNamHoc
        WHERE cbsv.MaSV = '{maSV}'
        ORDER BY cbsv.ThoiDiem DESC
        ";

                return Connection.GetDataToTable(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Lỗi DAL - GetCanhBaoBySinhVien: "
                    + ex.Message);
            }
        }
        public DataTable GetNamHocTheoSinhVien(string maSV)
        {
            string sql = $@"
    DECLARE @NienKhoa NVARCHAR(20)

    SELECT @NienKhoa = NienKhoa
    FROM SinhVien
    WHERE MaSV = '{maSV}'

    DECLARE @NamBatDau INT =
        CAST(LEFT(@NienKhoa,4) AS INT)

    DECLARE @NamKetThuc INT =
        CAST(RIGHT(@NienKhoa,4) AS INT)

    SELECT
        MaNamHoc,
        TenNamHoc
    FROM NamHoc
    WHERE
        CAST(SUBSTRING(TenNamHoc,9,4) AS INT)
            BETWEEN @NamBatDau
                AND (@NamKetThuc - 1)
    ORDER BY TenNamHoc
    ";

            return Connection.GetDataToTable(sql);
        }
        public string GetMaSVByTaiKhoan(string maTaiKhoan)
        {
            string sql = $@"
        SELECT MaSV
        FROM SinhVien
        WHERE MaTaiKhoan = '{maTaiKhoan}'
    ";

            object result = Connection.ExecuteScalar(sql);

            return result?.ToString();
        }
    }
}