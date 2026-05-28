using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLDSV.BLL;

namespace QLDSV.GUI.Forms.SinhVien
{
    public partial class KetQuaHocTap : Form
    {
        // ── Dependencies ──────────────────────────────────────────────────────────
        private readonly KetQuaBLL _bll = new KetQuaBLL();

        // ── State ─────────────────────────────────────────────────────────────────
        private string _maSV    = "";
        private bool   _loading = false;  // tránh trigger event khi đang nạp combo

        // ─────────────────────────────────────────────────────────────────────────
        public KetQuaHocTap()
        {
            InitializeComponent();
            // Đảm bảo kết nối DAL layer được mở trước khi truy vấn
            QLDSV.DAL.Connection.KetNoi();
            this.Load += KetQuaHocTap_Load;
            cboNamHoc.SelectedIndexChanged += CboNamHoc_SelectedIndexChanged;
            cboHocKy.SelectedIndexChanged  += CboHocKy_SelectedIndexChanged;
        }

        // ── Load ──────────────────────────────────────────────────────────────────
        private void KetQuaHocTap_Load(object sender, EventArgs e)
        {
            try
            {
                // Cố định tiêu đề cột — không cuộn theo dữ liệu
                DataGridViewKQDiem.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                DataGridViewKQDiem.Columns["colSTT"].Frozen    = true;   // cột STT luôn hiển thị

                LoadThongTinSinhVien();
                LoadHocKy();
                LoadNamHocCuaSinhVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo form: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── 1. Lấy thông tin sinh viên từ session ─────────────────────────────────
        private void LoadThongTinSinhVien()
        {
            string maTK = SessionHelper.MaTaiKhoan;
            DataTable dt = _bll.GetThongTinSinhVien(maTK);

            if (dt.Rows.Count == 0)
            {
                lblMa.Text  = "Không tìm thấy";
                lblTen.Text = "";
                return;
            }

            _maSV       = dt.Rows[0]["MaSV"].ToString().Trim();
            lblMa.Text  = _maSV;
            lblTen.Text = dt.Rows[0]["HoTen"].ToString().Trim();
        }

        // ── 2. Nạp học kỳ (luôn cố định từ DB, thêm "Tất cả" đầu danh sách) ────────
        private void LoadHocKy()
        {
            _loading = true;
            DataTable dt = _bll.GetLoaiHocKy();

            // Thêm dòng "Tất cả" vào đầu
            DataRow rowAll = dt.NewRow();
            rowAll["MaLoaiHK"]  = "ALL";
            rowAll["TenLoaiHK"] = "-- Tất cả --";
            dt.Rows.InsertAt(rowAll, 0);

            cboHocKy.DataSource    = dt;
            cboHocKy.ValueMember   = "MaLoaiHK";
            cboHocKy.DisplayMember = "TenLoaiHK";
            _loading = false;
        }

        // ── 3. Nạp năm học mà sinh viên có đăng ký (thêm "Tất cả" đầu danh sách) ──
        private void LoadNamHocCuaSinhVien()
        {
            if (string.IsNullOrEmpty(_maSV)) return;

            _loading = true;
            DataTable dt = _bll.GetNamHocBySinhVien(_maSV);

            // Thêm dòng "Tất cả" vào đầu
            DataRow rowAll = dt.NewRow();
            rowAll["MaNamHoc"]  = "ALL";
            rowAll["TenNamHoc"] = "-- Tất cả --";
            dt.Rows.InsertAt(rowAll, 0);

            cboNamHoc.DataSource    = dt;
            cboNamHoc.ValueMember   = "MaNamHoc";
            cboNamHoc.DisplayMember = "TenNamHoc";
            _loading = false;

            LoadBangDiem();   // tự động load kỳ đầu tiên
        }

        // ── 4. Sự kiện thay đổi bộ lọc ───────────────────────────────────────────
        private void CboNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_loading) LoadBangDiem();
        }

        private void CboHocKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_loading) LoadBangDiem();
        }

        // ── 5. Load và hiển thị bảng điểm ────────────────────────────────────────
        private void LoadBangDiem()
        {
            if (string.IsNullOrEmpty(_maSV)) return;
            if (cboNamHoc.SelectedValue == null || cboHocKy.SelectedValue == null) return;

            string maNamHoc = cboNamHoc.SelectedValue.ToString();
            string maLoaiHK = cboHocKy.SelectedValue.ToString();

            try
            {
                DataTable dt = _bll.GetBangDiemSinhVien(_maSV, maNamHoc, maLoaiHK);
                HienThiBangDiem(dt);
                TinhVaHienThiTongKet(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải bảng điểm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── 6. Điền dữ liệu vào DataGridView ─────────────────────────────────────
        private void HienThiBangDiem(DataTable dt)
        {
            DataGridViewKQDiem.Rows.Clear();

            int stt = 1;
            foreach (DataRow row in dt.Rows)
            {
                decimal? cc  = ToDecimal(row["DiemCC"]);
                decimal? kt1 = ToDecimal(row["DiemKT1"]);
                decimal? kt2 = ToDecimal(row["DiemKT2"]);
                decimal? thi = ToDecimal(row["DiemThi"]);
                decimal? tk  = TinhDiemTongKet(cc, kt1, kt2, thi);

                string xepLoai = tk.HasValue ? XepLoai(tk.Value) : "--";
                string tkStr   = tk.HasValue ? tk.Value.ToString("F2") : "--";

                int idx = DataGridViewKQDiem.Rows.Add(
                    stt++,
                    row["MaMon"],
                    row["TenMon"],
                    row["SoTC"],
                    FormatDiem(cc),
                    FormatDiem(kt1),
                    FormatDiem(kt2),
                    FormatDiem(thi),
                    tkStr,
                    xepLoai
                );

                // Tô màu cột xếp loại
                if (tk.HasValue)
                    DataGridViewKQDiem.Rows[idx].Cells["colXepLoai"].Style.ForeColor = MauXepLoai(tk.Value);
            }
        }

        // ── 7. Tính và hiển thị tổng kết ─────────────────────────────────────────
        private void TinhVaHienThiTongKet(DataTable dt)
        {
            int    tcDangKy    = 0;
            int    tcTichLuy   = 0;
            double sumWeighted = 0;

            foreach (DataRow row in dt.Rows)
            {
                int soTC = Convert.ToInt32(row["SoTC"]);
                tcDangKy += soTC;

                decimal? tk = TinhDiemTongKet(
                    ToDecimal(row["DiemCC"]),
                    ToDecimal(row["DiemKT1"]),
                    ToDecimal(row["DiemKT2"]),
                    ToDecimal(row["DiemThi"]));

                if (tk.HasValue)
                {
                    tcTichLuy   += soTC;
                    sumWeighted += soTC * (double)tk.Value;
                }
            }

            // ĐTB hệ 10 = Σ(TC_i × TK_i) / Σ(TC_i có điểm)
            double dtbHK10 = tcTichLuy > 0 ? sumWeighted / tcTichLuy : 0;
            double dtbHK4  = Quy4(dtbHK10);

            lblTC.Text       = tcTichLuy.ToString();
            lblTK10.Text     = tcTichLuy > 0 ? dtbHK10.ToString("F2") : "--";
            lblTK4.Text      = tcTichLuy > 0 ? dtbHK4.ToString("F2")  : "--";
            lblPhanloai.Text = tcTichLuy > 0 ? PhanLoaiGPA(dtbHK4)    : "--";
        }

        // ── Helpers tính điểm ─────────────────────────────────────────────────────

        /// <summary>TK = CC×0.1 + KT1×0.15 + KT2×0.15 + Thi×0.6 (cần đủ 4 thành phần)</summary>
        private static decimal? TinhDiemTongKet(decimal? cc, decimal? kt1, decimal? kt2, decimal? thi)
        {
            if (!cc.HasValue || !kt1.HasValue || !kt2.HasValue || !thi.HasValue)
                return null;
            return Math.Round(cc.Value * 0.1m + kt1.Value * 0.15m + kt2.Value * 0.15m + thi.Value * 0.6m, 2);
        }

        /// <summary>
        /// Xếp loại chữ và quy đổi hệ 4 từ điểm tổng kết hệ 10.
        /// Thang: 9.5-10→A+(4.0) | 8.5-9.5→A(4.0) | 8.0-8.5→B+(3.5) |
        ///        7.0-8.0→B(3.0) | 6.5-7.0→C+(2.5) | 5.5-6.5→C(2.0) |
        ///        5.0-5.5→D+(1.5) | 4.0-5.0→D(1.0) | <4.0→F(0)
        /// </summary>
        private static string XepLoai(decimal d)
        {
            if (d >= 9.5m) return "A+";
            if (d >= 8.5m) return "A";
            if (d >= 8.0m) return "B+";
            if (d >= 7.0m) return "B";
            if (d >= 6.5m) return "C+";
            if (d >= 5.5m) return "C";
            if (d >= 5.0m) return "D+";
            if (d >= 4.0m) return "D";
            return "F";
        }

        /// <summary>Quy đổi điểm hệ 10 → hệ 4 theo thang chuẩn.</summary>
        private static double Quy4(double d10)
        {
            if (d10 >= 9.5) return 4.0;
            if (d10 >= 8.5) return 4.0;
            if (d10 >= 8.0) return 3.5;
            if (d10 >= 7.0) return 3.0;
            if (d10 >= 6.5) return 2.5;
            if (d10 >= 5.5) return 2.0;
            if (d10 >= 5.0) return 1.5;
            if (d10 >= 4.0) return 1.0;
            return 0.0;
        }

        /// <summary>
        /// Phân loại học lực theo GPA hệ 4.
        /// 3.6-4.0→Xuất sắc | 3.2-3.6→Giỏi | 2.5-3.2→Khá |
        /// 2.0-2.5→Trung bình | 1.0-2.0→Yếu | <1.0→Kém
        /// </summary>
        private static string PhanLoaiGPA(double gpa4)
        {
            if (gpa4 >= 3.6) return "Xuất sắc";
            if (gpa4 >= 3.2) return "Giỏi";
            if (gpa4 >= 2.5) return "Khá";
            if (gpa4 >= 2.0) return "Trung bình";
            if (gpa4 >= 1.0) return "Yếu";
            return "Kém";
        }

        /// <summary>Màu sắc theo điểm tổng kết.</summary>
        private static Color MauXepLoai(decimal d)
        {
            if (d >= 8.5m) return Color.FromArgb(0, 140, 0);
            if (d >= 6.5m) return Color.FromArgb(0, 100, 180);
            if (d >= 5.0m) return Color.FromArgb(180, 120, 0);
            return Color.Red;
        }

        private static decimal? ToDecimal(object val)
        {
            if (val == null || val == DBNull.Value) return null;
            return decimal.TryParse(val.ToString(), out decimal d) ? d : (decimal?)null;
        }

        private static string FormatDiem(decimal? d)
            => d.HasValue ? d.Value.ToString("F1") : "--";
    }
}
