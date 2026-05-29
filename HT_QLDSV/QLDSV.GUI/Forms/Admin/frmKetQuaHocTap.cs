using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QLDSV.BLL;

namespace QLDSV.GUI
{
    public partial class frmKetQuaHocTap : Form, IShellChildForm
    {
        private string maSVHienTai = "";

        public frmKetQuaHocTap()
        {
            InitializeComponent();

            ThemeHelper.ApplyTheme(this);
        }

        public void OnEmbeddedInShell()
        {
            if (pnlSidebar != null) pnlSidebar.Visible = false;
            if (guna2ImageButton1 != null) guna2ImageButton1.Visible = false;
            if (guna2ImageButton2 != null) guna2ImageButton2.Visible = false;
            if (guna2ImageButton3 != null) guna2ImageButton3.Visible = false;
            if (guna2CirclePictureBox1 != null) guna2CirclePictureBox1.Visible = false;
            if (guna2HtmlLabel13 != null) guna2HtmlLabel13.Visible = false;
            if (guna2HtmlLabel14 != null) guna2HtmlLabel14.Visible = false;
            if (label3 != null) label3.Visible = false;
            if (label4 != null) label4.Visible = false;
        }

        private void frmKetQuaHocTap_Load(object sender, EventArgs e)
        {
            try
            {
                FunctionQa.ketnoi();
                
                // Lấy mã sinh viên hiện tại dựa trên mã tài khoản đang đăng nhập
                maSVHienTai = FunctionQa.getfieldvalue(
                    $"SELECT MaSV FROM SinhVien WHERE MaTaiKhoan = '{SessionHelper.MaTaiKhoan}'");

                if (string.IsNullOrEmpty(maSVHienTai))
                {
                    MessageBox.Show("Không tìm thấy thông tin sinh viên liên kết với tài khoản này.",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                LoadDiemSinhVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu học tập: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDiemSinhVien()
        {
            try
            {
                // Truy vấn điểm chi tiết của sinh viên
                string sql = $@"
                    SELECT 
                        lhp.MaLHP AS [Mã Lớp Học Phần],
                        mh.TenMon AS [Tên Môn Học],
                        mh.SoTC AS [Số TC],
                        MAX(CASE WHEN kq.MaLoaiDiem = 'CC' THEN kq.Diem END) AS [Điểm Chuyên Cần],
                        MAX(CASE WHEN kq.MaLoaiDiem = 'KT1' THEN kq.Diem END) AS [Điểm KT1],
                        MAX(CASE WHEN kq.MaLoaiDiem = 'KT2' THEN kq.Diem END) AS [Điểm KT2],
                        MAX(CASE WHEN kq.MaLoaiDiem = 'CK' THEN kq.Diem END) AS [Điểm Cuối Kỳ]
                    FROM LopHocPhan lhp
                    JOIN MonHoc mh ON lhp.MaMon = mh.MaMon
                    JOIN DangKyLopHoc dk ON lhp.MaLHP = dk.MaLHP
                    LEFT JOIN KetQua kq ON lhp.MaLHP = kq.MaLHP AND kq.MaSV = dk.MaSV
                    WHERE dk.MaSV = '{maSVHienTai}'
                    GROUP BY lhp.MaLHP, mh.TenMon, mh.SoTC";

                DataTable dt = FunctionQa.getdatatotable(sql);
                
                // Thêm các cột tính toán điểm tổng kết và điểm chữ
                dt.Columns.Add("Điểm Tổng Kết", typeof(double));
                dt.Columns.Add("Điểm Chữ", typeof(string));

                foreach (DataRow row in dt.Rows)
                {
                    double cc = 0, kt1 = 0, kt2 = 0, ck = 0;
                    bool hasGrades = false;

                    if (row["Điểm Chuyên Cần"] != DBNull.Value) { cc = Convert.ToDouble(row["Điểm Chuyên Cần"]); hasGrades = true; }
                    if (row["Điểm KT1"] != DBNull.Value) { kt1 = Convert.ToDouble(row["Điểm KT1"]); hasGrades = true; }
                    if (row["Điểm KT2"] != DBNull.Value) { kt2 = Convert.ToDouble(row["Điểm KT2"]); hasGrades = true; }
                    if (row["Điểm Cuối Kỳ"] != DBNull.Value) { ck = Convert.ToDouble(row["Điểm Cuối Kỳ"]); hasGrades = true; }

                    if (hasGrades)
                    {
                        double tongKet = KetQuaBLL.TinhDiemTongKet(cc, kt1, kt2, ck);
                        row["Điểm Tổng Kết"] = Math.Round(tongKet, 2);

                        // Quy đổi sang điểm chữ
                        if (tongKet >= 8.5) row["Điểm Chữ"] = "A";
                        else if (tongKet >= 7.0) row["Điểm Chữ"] = "B";
                        else if (tongKet >= 5.5) row["Điểm Chữ"] = "C";
                        else if (tongKet >= 4.0) row["Điểm Chữ"] = "D";
                        else row["Điểm Chữ"] = "F";
                    }
                }

                dgvKetQua.AutoGenerateColumns = true;
                dgvKetQua.DataSource = null;
                dgvKetQua.DataSource = dt;

                if (dgvKetQua.Columns.Count > 0)
                {
                    SetColHeader("Mã Lớp Học Phần", "Mã LHP");
                    SetColHeader("Tên Môn Học", "Tên môn");
                    SetColHeader("Số TC", "Số TC");
                    SetColHeader("Điểm Chuyên Cần", "CC");
                    SetColHeader("Điểm KT1", "KT1");
                    SetColHeader("Điểm KT2", "KT2");
                    SetColHeader("Điểm Cuối Kỳ", "Thi");
                    SetColHeader("Điểm Tổng Kết", "Tổng kết");
                    SetColHeader("Điểm Chữ", "Điểm chữ");
                }
                lblMaSvValue.Text = maSVHienTai;

                // Best-effort: nếu có tên SV thì hiện lên header
                string hoTen = FunctionQa.getfieldvalue($"SELECT HoTen FROM SinhVien WHERE MaSV = '{maSVHienTai}'");
                if (!string.IsNullOrWhiteSpace(hoTen))
                    lblTenSvValue.Text = hoTen;

                // Reset tổng kết (nếu cần tính sau)
                lblTongTCValue.Text = "—";
                lblDTB10Value.Text = "—";
                lblDiemChuValue.Text = "—";
                lblGPA4Value.Text = "—";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải bảng điểm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetColHeader(string columnName, string headerText)
        {
            if (dgvKetQua.Columns.Contains(columnName))
                dgvKetQua.Columns[columnName].HeaderText = headerText;
        }
    }
}
