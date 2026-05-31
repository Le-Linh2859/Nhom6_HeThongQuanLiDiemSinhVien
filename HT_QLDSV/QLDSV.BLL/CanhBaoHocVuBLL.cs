using QLDSV.DAL;
using System;
using System.Data;

namespace QLDSV.BLL
{
    public class CanhBaoHocVuBLL
    {
        private readonly CanhBaoHocVuDAL dal =
            new CanhBaoHocVuDAL();
        // LẤY DANH SÁCH CẢNH BÁO HỌC VỤ
        public DataTable GetDanhSachCanhBao()
        {
            try
            {
                return dal.GetDanhSachCanhBao();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "BLL - Lỗi lấy danh sách cảnh báo học vụ: "
                    + ex.Message);
            }
        }
        // LẤY DANH SÁCH NĂM HỌC
        public DataTable GetNamHoc()
        {
            try
            {
                return dal.GetNamHoc();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "BLL - Lỗi lấy năm học: "
                    + ex.Message);
            }
        }
        // LẤY DANH SÁCH HỌC KỲ
        public DataTable GetHocKy()
        {
            try
            {
                return dal.GetHocKy();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "BLL - Lỗi lấy học kỳ: "
                    + ex.Message);
            }
        }
        // LẤY DANH SÁCH LỚP NIÊN CHẾ
        public DataTable GetLopNienChe()
        {
            try
            {
                return dal.GetLopNienChe();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "BLL - Lỗi lấy lớp niên chế: "
                    + ex.Message);
            }
        }
        // KIỂM TRA KẾT NỐI
        public bool CheckConnection()
        {
            try
            {
                return dal.TestConnection();
            }
            catch
            {
                return false;
            }
        }
        public string GetMaSVByTaiKhoan(string maTaiKhoan)
        {
            return dal.GetMaSVByTaiKhoan(maTaiKhoan);
        }
        // PHÁT HIỆN CẢNH BÁO TỰ ĐỘNG
        // LoaiCanhBao
        private const int LOAI_KHOI_LUONG_ZERO = 2;  // TH1: 0 tín chỉ
        private const int LOAI_DIEM_THAP = 3;  // TH2: TBC < 1.5
        public (int soMoi, int soCapNhat) PhatHienVaLuuCanhBao()
        {
            int soMoi = 0;
            int soCapNhat = 0;

            try
            {
                DataTable dtHocKy = dal.GetHocKyDangHoatDong();

                foreach (DataRow rowHK in dtHocKy.Rows)
                {
                    string maHKNH = rowHK["MaHKNH"].ToString();

                    var (m1, c1) = XuLyCanhBaoKhoiLuongZero(maHKNH);
                    soMoi += m1;
                    soCapNhat += c1;

                    var (m2, c2) = XuLyCanhBaoDiemThap(maHKNH);
                    soMoi += m2;
                    soCapNhat += c2;
                }

                return (soMoi, soCapNhat);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "BLL - Lỗi phát hiện cảnh báo: " + ex.Message);
            }
        }

        /// TH1: Xử lý cảnh báo sinh viên có khối lượng = 0 TC
        private (int soMoi, int soCapNhat) XuLyCanhBaoKhoiLuongZero(
    string maHKNH)
        {
            if (dal.IsHocKyHe(maHKNH))
                return (0, 0);

            int soMoi = 0;
            int soCapNhat = 0;

            DataTable dtSV =
                dal.GetSinhVienKhoiLuongZero(maHKNH);

            string maCanhBao =
                dal.GetMaCanhBaoByLoai(LOAI_KHOI_LUONG_ZERO);

            if (string.IsNullOrEmpty(maCanhBao))
                return (0, 0);

            foreach (DataRow row in dtSV.Rows)
            {
                string maSV = row["MaSV"].ToString();

                int lanThuHienTai = dal.GetLanThuCanhBao(
                    maSV, maHKNH, LOAI_KHOI_LUONG_ZERO);

                if (lanThuHienTai == 0)
                {
                    int lanMoi =
                        dal.GetSoLanCanhBaoTruocDo(
                            maSV,
                            maCanhBao) + 1;

                    dal.InsertCanhBaoSinhVien(
                        maSV,
                        maCanhBao,
                        maHKNH,
                        lanMoi);

                    soMoi++;
                }
            }

            return (soMoi, soCapNhat);
        }

        /// TH2: Xử lý cảnh báo sinh viên có TBC học kỳ dưới 1.5
        private (int soMoi, int soCapNhat) XuLyCanhBaoDiemThap(
            string maHKNH)
        {
            int soMoi = 0;
            int soCapNhat = 0;

            DataTable dtSV =
                dal.GetSinhVienDiemThapDuoi1_5(maHKNH);

            // Lấy MaCanhBao tương ứng loại 2
            string maCanhBao =
                dal.GetMaCanhBaoByLoai(LOAI_DIEM_THAP);

            if (string.IsNullOrEmpty(maCanhBao))
                return (0, 0);

            foreach (DataRow row in dtSV.Rows)
            {
                string maSV = row["MaSV"].ToString();
                double tbcHK = Convert.ToDouble(row["TBCHocKy"]);

                int lanThuHienTai = dal.GetLanThuCanhBao(
                    maSV, maHKNH, LOAI_DIEM_THAP);

                if (lanThuHienTai == 0)
                {
                    int lanMoi =
                        dal.GetSoLanCanhBaoTruocDo(
                            maSV,
                            maCanhBao) + 1;

                    dal.InsertCanhBaoSinhVien(
                        maSV,
                        maCanhBao,
                        maHKNH,
                        lanMoi);

                    soMoi++;
                }
            }

            return (soMoi, soCapNhat);

        }
        public DataTable GetCanhBaoBySinhVien(string maSV)
        {
            return dal.GetCanhBaoBySinhVien(maSV);
        }
        public DataTable GetNamHocTheoSinhVien(string maSV)
        {
            return dal.GetNamHocTheoSinhVien(maSV);
        }
    }
}
