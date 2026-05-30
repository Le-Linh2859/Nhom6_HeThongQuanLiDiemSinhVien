using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QLDSV.BLL;
using QLDSV.GUI;

namespace QLDSV.GUI.Forms.Admin
{
    public partial class frmTheoDoiDiem : Form
    {
        private readonly KetQuaBLL _bll = new KetQuaBLL();
        private bool _isLoadingCombo;
        private bool _eventsWired;
        private DataTable _dtKetQua = new DataTable();

        private static readonly Color FilterLabelColor = Color.FromArgb(44, 62, 80);

        public frmTheoDoiDiem()
        {
            InitializeComponent();
            ThemeHelper.ApplyTheme(this);
            SetupVisualStyle();
        }

        private void SetupVisualStyle()
        {
            lblNamhoc.ForeColor = FilterLabelColor;
            lblhocky.ForeColor = FilterLabelColor;
            lblLop.ForeColor = FilterLabelColor;
            lblMaTen.ForeColor = FilterLabelColor;

            lblNamhoc.Font = lblhocky.Font = lblLop.Font = lblMaTen.Font =
                new Font("Segoe UI Semibold", 9F, FontStyle.Bold);

            label1.ForeColor = label2.ForeColor = label3.ForeColor =
                label4.ForeColor = label5.ForeColor = FilterLabelColor;

            txtMaTen.PlaceholderText = "Nhập mã hoặc tên sinh viên...";

            btnLoc.Cursor = Cursors.Hand;
            btnLammoi.Cursor = Cursors.Hand;
            btnLoc.Enabled = true;
            btnLammoi.Enabled = true;
        }

        private void frmTheoDoiDiem_Load(object sender, EventArgs e)
        {
            try
            {
                if (FunctionQa.conn == null ||
                    FunctionQa.conn.State != ConnectionState.Open)
                {
                    FunctionQa.ketnoi();
                    QLDSV.DAL.Connection.conn = FunctionQa.conn;
                }
                else
                {
                    QLDSV.DAL.Connection.conn = FunctionQa.conn;
                }

                WireEvents();

                LoadComboNamHoc();
                LoadComboHocKy();
                LoadComboLop();
                LoadKetQua();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo form:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void WireEvents()
        {
            if (_eventsWired) return;
            _eventsWired = true;

            btnLoc.Click -= btnLoc_Click;
            btnLoc.Click += btnLoc_Click;

            btnLammoi.Click -= btnLammoi_Click;
            btnLammoi.Click += btnLammoi_Click;

            txtMaTen.KeyDown -= txtMaTen_KeyDown;
            txtMaTen.KeyDown += txtMaTen_KeyDown;

            dataGridView1.SelectionChanged -= DataGridView1_SelectionChanged;
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
        }

        private void txtMaTen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                LoadKetQua();
            }
        }

        private void LoadComboNamHoc()
        {
            _isLoadingCombo = true;
            try
            {
                DataTable dt = _bll.GetDanhSachNamHoc();
                DataRow r = dt.NewRow();
                r["MaNamHoc"] = "ALL";
                r["TenNamHoc"] = "-- Tất cả năm học --";
                dt.Rows.InsertAt(r, 0);

                cboNam.DataSource = dt;
                cboNam.ValueMember = "MaNamHoc";
                cboNam.DisplayMember = "TenNamHoc";
                cboNam.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load năm học:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally { _isLoadingCombo = false; }
        }

        private void LoadComboHocKy()
        {
            _isLoadingCombo = true;
            try
            {
                DataTable dt = _bll.GetDanhSachLoaiHocKy();
                DataRow r = dt.NewRow();
                r["MaLoaiHK"] = "ALL";
                r["TenLoaiHK"] = "-- Tất cả học kỳ --";
                dt.Rows.InsertAt(r, 0);

                cboHocky.DataSource = dt;
                cboHocky.ValueMember = "MaLoaiHK";
                cboHocky.DisplayMember = "TenLoaiHK";
                cboHocky.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load học kỳ:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally { _isLoadingCombo = false; }
        }

        private void LoadComboLop()
        {
            _isLoadingCombo = true;
            try
            {
                DataTable dt = _bll.GetDanhSachLopNienChe();
                DataRow r = dt.NewRow();
                r["MaLopNienChe"] = "ALL";
                r["TenLop"] = "-- Tất cả lớp --";
                dt.Rows.InsertAt(r, 0);

                cboLop.DataSource = dt;
                cboLop.ValueMember = "MaLopNienChe";
                cboLop.DisplayMember = "TenLop";
                cboLop.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load lớp:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally { _isLoadingCombo = false; }
        }

        private void LoadKetQua()
        {
            try
            {
                string maNamHoc = cboNam.SelectedValue?.ToString() ?? "ALL";
                string maLoaiHK = cboHocky.SelectedValue?.ToString() ?? "ALL";
                string maLop = cboLop.SelectedValue?.ToString() ?? "ALL";
                string keyword = txtMaTen.Text.Trim();

                if (string.IsNullOrEmpty(maNamHoc)) maNamHoc = "ALL";
                if (string.IsNullOrEmpty(maLoaiHK)) maLoaiHK = "ALL";
                if (string.IsNullOrEmpty(maLop)) maLop = "ALL";

                _dtKetQua = _bll.GetKetQuaHocTapAdmin(maNamHoc, maLoaiHK, maLop, keyword);

                if (!_dtKetQua.Columns.Contains("DiemChu"))
                    _dtKetQua.Columns.Add("DiemChu", typeof(string));
                if (!_dtKetQua.Columns.Contains("GPA4"))
                    _dtKetQua.Columns.Add("GPA4", typeof(double));
                if (!_dtKetQua.Columns.Contains("XepLoai"))
                    _dtKetQua.Columns.Add("XepLoai", typeof(string));

                foreach (DataRow row in _dtKetQua.Rows)
                {
                    double dtb = row["DTB10"] != DBNull.Value
                        ? Convert.ToDouble(row["DTB10"]) : 0;
                    double gpa4 = row.Table.Columns.Contains("GPA4") && row["GPA4"] != DBNull.Value
                        ? Convert.ToDouble(row["GPA4"])
                        : 0;

                    row["DiemChu"] = KetQuaBLL.QuyDoiDiemChu(dtb);
                    row["GPA4"] = gpa4;
                    row["XepLoai"] = KetQuaBLL.XepLoaiHocLuc(gpa4);
                }

                BindMainGrid();
                UpdateStatCards();
                ResetChiTiet();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetChiTiet()
        {
            dataGridView2.DataSource = null;
            txtMa.Text = "";
            txtTen.Text = "";
            txtLop.Text = "";
            txtNam.Text = "";
            txthocky.Text = "";
        }

        private void BindMainGrid()
        {
            dataGridView1.SelectionChanged -= DataGridView1_SelectionChanged;
            dataGridView1.DataSource = _dtKetQua;
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;

            if (dataGridView1.Columns.Count == 0) return;

            foreach (DataGridViewColumn col in dataGridView1.Columns)
                col.Visible = false;

            string[] colNames = { "MaSV", "HoTen", "TenLop",
                "DTB10", "DiemChu", "GPA4", "XepLoai" };
            string[] headers = { "Mã Sinh Viên", "Họ và Tên", "Lớp Niên Chế",
                "Điểm TB Hệ 10", "Điểm Chữ", "GPA Hệ 4", "Xếp Loại" };

            for (int i = 0; i < colNames.Length; i++)
            {
                if (!dataGridView1.Columns.Contains(colNames[i])) continue;
                var col = dataGridView1.Columns[colNames[i]];
                col.Visible = true;
                col.DisplayIndex = i;
                col.HeaderText = headers[i];
            }
        }

        private void UpdateStatCards()
        {
            int tongSV = 0, xuatSac = 0, gioi = 0, kha = 0, trungBinh = 0, yeuKem = 0;
            double totalGPA = 0;
            int countGPA = 0;

            foreach (DataRow row in _dtKetQua.Rows)
            {
                tongSV++;
                string rank = row["XepLoai"]?.ToString() ?? "";
                switch (rank)
                {
                    case "Xuất sắc": xuatSac++; break;
                    case "Giỏi": gioi++; break;
                    case "Khá": kha++; break;
                    case "Trung bình": trungBinh++; break;
                    case "Yếu":
                    case "Kém": yeuKem++; break;
                }
                if (row["GPA4"] != DBNull.Value)
                {
                    totalGPA += Convert.ToDouble(row["GPA4"]);
                    countGPA++;
                }
            }

            bool locTheoLop = cboLop.SelectedValue?.ToString() != "ALL"
                && !string.IsNullOrEmpty(cboLop.SelectedValue?.ToString());

            string gpaText = (locTheoLop && countGPA > 0)
                ? (totalGPA / countGPA).ToString("F2")
                : "—";

            SetCardLabel(lblTong, tongSV.ToString(), Color.FromArgb(21, 101, 192));
            SetCardLabel(lblXuatsac, xuatSac.ToString(), Color.FromArgb(27, 94, 32));
            SetCardLabel(lblGioi, gioi.ToString(), Color.FromArgb(51, 105, 30));
            SetCardLabel(lblKha, kha.ToString(), Color.FromArgb(13, 71, 161));
            SetCardLabel(lblTB, trungBinh.ToString(), Color.FromArgb(200, 100, 0));
            SetCardLabel(lblYeu, yeuKem.ToString(), Color.FromArgb(183, 28, 28));
            SetCardLabel(lblDiemTB, gpaText, Color.FromArgb(106, 27, 154));
        }

        private void SetCardLabel(Label lbl, string text, Color color)
        {
            lbl.Text = text;
            lbl.ForeColor = color;
            lbl.Font = new Font("Segoe UI", 16f, FontStyle.Bold);
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            DataGridViewRow row = dataGridView1.SelectedRows[0];

            string maSV = row.Cells["MaSV"]?.Value?.ToString() ?? "";
            string hoTen = row.Cells["HoTen"]?.Value?.ToString() ?? "";
            string lop = row.Cells["TenLop"]?.Value?.ToString() ?? "";

            txtMa.Text = maSV;
            txtTen.Text = hoTen;
            txtLop.Text = lop;
            txtNam.Text = (cboNam.SelectedValue?.ToString() == "ALL") ? "" : cboNam.Text;
            txthocky.Text = (cboHocky.SelectedValue?.ToString() == "ALL") ? "" : cboHocky.Text;

            if (!string.IsNullOrEmpty(maSV))
                LoadChiTietDiem(maSV);
        }

        private void LoadChiTietDiem(string maSV)
        {
            try
            {
                string maNamHoc = cboNam.SelectedValue?.ToString() ?? "ALL";
                string maLoaiHK = cboHocky.SelectedValue?.ToString() ?? "ALL";
                if (string.IsNullOrEmpty(maNamHoc)) maNamHoc = "ALL";
                if (string.IsNullOrEmpty(maLoaiHK)) maLoaiHK = "ALL";

                DataTable dt = _bll.GetChiTietDiemSinhVien(maSV, maNamHoc, maLoaiHK);

                if (!dt.Columns.Contains("DiemTongKet"))
                    dt.Columns.Add("DiemTongKet", typeof(double));
                if (!dt.Columns.Contains("DiemChu"))
                    dt.Columns.Add("DiemChu", typeof(string));
                if (!dt.Columns.Contains("DiemHe4"))
                    dt.Columns.Add("DiemHe4", typeof(double));

                foreach (DataRow row in dt.Rows)
                {
                    double cc = Convert.ToDouble(row["DiemCC"]);
                    double kt1 = Convert.ToDouble(row["DiemKT1"]);
                    double kt2 = Convert.ToDouble(row["DiemKT2"]);
                    double ck = Convert.ToDouble(row["DiemThi"]);

                    double tk = KetQuaBLL.TinhDiemTongKet(cc, kt1, kt2, ck);
                    double gpa4 = KetQuaBLL.QuyDoiHe4(tk);

                    row["DiemTongKet"] = tk;
                    row["DiemChu"] = KetQuaBLL.QuyDoiDiemChu(tk);
                    row["DiemHe4"] = gpa4;
                }

                dataGridView2.DataSource = dt;
                BindDetailGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải chi tiết điểm:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindDetailGrid()
        {
            if (dataGridView2.Columns.Count == 0) return;

            foreach (DataGridViewColumn col in dataGridView2.Columns)
                col.Visible = false;

            string[] colNames = { "MaLHP", "TenMon", "DiemCC", "DiemKT1",
                "DiemKT2", "DiemThi", "DiemTongKet", "DiemChu", "DiemHe4" };
            string[] headers = { "Mã LHP", "Tên Môn Học", "Chuyên Cần", "KT1",
                "KT2", "Cuối Kỳ", "Tổng Kết (Hệ 10)", "Điểm Chữ", "Hệ 4" };

            for (int i = 0; i < colNames.Length; i++)
            {
                if (!dataGridView2.Columns.Contains(colNames[i])) continue;
                var col = dataGridView2.Columns[colNames[i]];
                col.Visible = true;
                col.DisplayIndex = i;
                col.HeaderText = headers[i];
            }
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadKetQua();
        }

        private void btnLammoi_Click(object sender, EventArgs e)
        {
            _isLoadingCombo = true;
            try
            {
                if (cboNam.Items.Count > 0) cboNam.SelectedIndex = 0;
                if (cboHocky.Items.Count > 0) cboHocky.SelectedIndex = 0;
                if (cboLop.Items.Count > 0) cboLop.SelectedIndex = 0;
                txtMaTen.Text = "";
            }
            finally { _isLoadingCombo = false; }

            LoadKetQua();
        }

    }
}
