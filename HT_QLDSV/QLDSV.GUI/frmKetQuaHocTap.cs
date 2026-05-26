using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QLDSV.GUI
{
    public partial class frmKetQuaHocTap : Form, IShellChildForm
    {
        private string maSVHienTai = "";

        public frmKetQuaHocTap()
        {
            InitializeComponent();
        }

        public void OnEmbeddedInShell()
        {
            // Bước 1: Ẩn các control Sidebar & Header trùng lặp bằng tìm kiếm động an toàn
            string[] controlNames = { 
                "pnlSidebar", "pnlHeader", "guna2ImageButton1", "label3", "label4", 
                "guna2ImageButton2", "guna2CirclePictureBox1", "guna2HtmlLabel13", 
                "guna2HtmlLabel14", "guna2ImageButton3" 
            };
            foreach (var name in controlNames)
            {
                var ct = this.Controls.Find(name, true);
                foreach (var c in ct)
                {
                    c.Visible = false;
                }
            }

            // Bước 2: Tự động xác định khoảng dịch chuyển (shiftX) từ sidebar thực tế
            int shiftX = 0;
            var sidebarControls = this.Controls.Find("pnlSidebar", true);
            if (sidebarControls.Length > 0)
            {
                shiftX = sidebarControls[0].Width;
            }

            // Fallback an toàn: Nếu không tìm thấy sidebar, giữ nguyên giao diện hiện tại
            if (shiftX == 0) return;

            // Bước 3: Dịch chuyển các control cấp cao nhất (Top-Level Controls) sang trái
            foreach (Control ctrl in this.Controls)
            {
                bool isHiddenControl = false;
                foreach (var name in controlNames)
                {
                    if (ctrl.Name == name)
                    {
                        isHiddenControl = true;
                        break;
                    }
                }

                // Thực hiện dịch chuyển có Guard bảo vệ: Chỉ dịch các control có Left > 0 và không cho âm
                if (!isHiddenControl && ctrl.Left > 0)
                {
                    ctrl.Left = Math.Max(0, ctrl.Left - shiftX);
                }
            }

            // Bước 4: Đệ quy tìm kiếm mọi DataGridView bất kể tên gọi để cấu hình Anchor bốn chiều
            SetAnchorAllGrids(this.Controls);
        }

        private void SetAnchorAllGrids(Control.ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                if (c is DataGridView dgv)
                {
                    dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                }
                if (c.Controls.Count > 0)
                {
                    SetAnchorAllGrids(c.Controls); // Đệ quy sâu vào các Panel/GroupBox con
                }
            }
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
                        // Công thức tính điểm tổng kết: CC * 10% + KT1 * 15% + KT2 * 15% + CK * 60%
                        double tongKet = (cc * 0.1) + (kt1 * 0.15) + (kt2 * 0.15) + (ck * 0.6);
                        row["Điểm Tổng Kết"] = Math.Round(tongKet, 2);

                        // Quy đổi sang điểm chữ
                        if (tongKet >= 8.5) row["Điểm Chữ"] = "A";
                        else if (tongKet >= 7.0) row["Điểm Chữ"] = "B";
                        else if (tongKet >= 5.5) row["Điểm Chữ"] = "C";
                        else if (tongKet >= 4.0) row["Điểm Chữ"] = "D";
                        else row["Điểm Chữ"] = "F";
                    }
                }

                dataGridView.DataSource = dt;
                
                // Căn chỉnh giao diện cột
                if (dataGridView.Columns.Count > 0)
                {
                    dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView.AllowUserToAddRows = false;
                    dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi nạp bảng điểm: " + ex.Message);
            }
        }
    }
}
