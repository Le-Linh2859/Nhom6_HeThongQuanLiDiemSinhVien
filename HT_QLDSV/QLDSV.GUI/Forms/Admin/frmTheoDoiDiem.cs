using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QLDSV.BLL;

namespace QLDSV.GUI.Forms.Admin
{
    public partial class frmTheoDoiDiem : Form
    {
        private readonly KetQuaBLL _bll = new KetQuaBLL();
        private bool _isLoadingCombo = false;
        private DataTable _dtKetQua  = new DataTable();

        public frmTheoDoiDiem()
        {
            InitializeComponent();
        }

        // ════════════════════════════════════════════════════════════════════
        //  FORM LOAD
        // ════════════════════════════════════════════════════════════════════
        private void frmTheoDoiDiem_Load(object sender, EventArgs e)
        {
            try
            {
                // frmMain đã gọi FunctionQa.ketnoi() và sync Connection.conn rồi.
                // Chỉ cần đảm bảo connection còn mở; nếu chưa thì mở lại.
                if (FunctionQa.conn == null ||
                    FunctionQa.conn.State != System.Data.ConnectionState.Open)
                {
                    FunctionQa.ketnoi();
                    QLDSV.DAL.Connection.conn = FunctionQa.conn;
                }
                else
                {
                    // Đảm bảo DAL dùng cùng connection với GUI
                    QLDSV.DAL.Connection.conn = FunctionQa.conn;
                }

                LoadComboNamHoc();
                LoadComboHocKy();
                LoadComboLop();

                btnLoc.Click    += BtnLoc_Click;
                btnLammoi.Click += BtnLammoi_Click;
                dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;

                StyleMainGrid();
                StyleDetailGrid();

                LoadKetQua();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo form:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ════════════════════════════════════════════════════════════════════
        //  LOAD COMBOBOXES  (GUI → BLL → DAL)
        // ════════════════════════════════════════════════════════════════════
        private void LoadComboNamHoc()
        {
            _isLoadingCombo = true;
            try
            {
                DataTable dt = _bll.GetDanhSachNamHoc();
                DataRow r = dt.NewRow();
                r["MaNamHoc"]  = "ALL";
                r["TenNamHoc"] = "-- Tất cả năm học --";
                dt.Rows.InsertAt(r, 0);

                cboNam.DataSource    = dt;
                cboNam.ValueMember   = "MaNamHoc";
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
                r["MaLoaiHK"]  = "ALL";
                r["TenLoaiHK"] = "-- Tất cả học kỳ --";
                dt.Rows.InsertAt(r, 0);

                cboHocky.DataSource    = dt;
                cboHocky.ValueMember   = "MaLoaiHK";
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
                r["TenLop"]       = "-- Tất cả lớp --";
                dt.Rows.InsertAt(r, 0);

                cboLop.DataSource    = dt;
                cboLop.ValueMember   = "MaLopNienChe";
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

        // ════════════════════════════════════════════════════════════════════
        //  LOAD KẾT QUẢ CHÍNH  (GUI → BLL → DAL)
        // ════════════════════════════════════════════════════════════════════
        private void LoadKetQua()
        {
            try
            {
                string maNamHoc = cboNam.SelectedValue?.ToString()   ?? "ALL";
                string maLoaiHK = cboHocky.SelectedValue?.ToString() ?? "ALL";
                string maLop    = cboLop.SelectedValue?.ToString()   ?? "ALL";
                string keyword  = txtMaTen.Text.Trim();

                if (string.IsNullOrEmpty(maNamHoc)) maNamHoc = "ALL";
                if (string.IsNullOrEmpty(maLoaiHK)) maLoaiHK = "ALL";
                if (string.IsNullOrEmpty(maLop))    maLop    = "ALL";

                // Gọi BLL → DAL, không có SQL trong GUI
                _dtKetQua = _bll.GetKetQuaHocTapAdmin(maNamHoc, maLoaiHK, maLop, keyword);

                // Thêm cột tính toán — logic quy đổi nằm trong BLL
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
                    double gpa4 = KetQuaBLL.QuyDoiHe4(dtb);

                    row["DiemChu"] = KetQuaBLL.QuyDoiDiemChu(dtb);
                    row["GPA4"]    = gpa4;
                    row["XepLoai"] = KetQuaBLL.XepLoaiHocLuc(gpa4);
                }

                BindMainGrid();
                UpdateStatCards();

                // Reset panel chi tiết mỗi khi bộ lọc được áp dụng lại
                ResetChiTiet();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu:\n" + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>Xóa trắng toàn bộ panel thông tin chi tiết sinh viên.</summary>
        private void ResetChiTiet()
        {
            dataGridView2.DataSource = null;
            txtMa.Text     = "";
            txtTen.Text    = "";
            txtLop.Text    = "";
            txtNam.Text    = "";
            txthocky.Text  = "";
        }

        // ════════════════════════════════════════════════════════════════════
        //  BIND GRID CHÍNH
        // ════════════════════════════════════════════════════════════════════
        private void BindMainGrid()
        {
            dataGridView1.SelectionChanged -= DataGridView1_SelectionChanged;
            dataGridView1.DataSource = _dtKetQua;
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;

            if (dataGridView1.Columns.Count == 0) return;

            foreach (DataGridViewColumn col in dataGridView1.Columns)
                col.Visible = false;

            // Tên cột trong DataTable → Header hiển thị
            string[] colNames = { "MaSV",        "HoTen",      "TenLop",
                                   "DTB10",       "DiemChu",    "GPA4",    "XepLoai" };
            string[] headers  = { "Mã Sinh Viên", "Họ và Tên", "Lớp Niên Chế",
                                   "Điểm TB Hệ 10","Điểm Chữ", "GPA Hệ 4","Xếp Loại" };

            for (int i = 0; i < colNames.Length; i++)
            {
                if (!dataGridView1.Columns.Contains(colNames[i])) continue;
                var col = dataGridView1.Columns[colNames[i]];
                col.Visible      = true;
                col.DisplayIndex = i;
                col.HeaderText   = headers[i];
            }

            // Căn giữa cột điểm
            foreach (string c in new[] { "DTB10", "DiemChu", "GPA4", "XepLoai" })
            {
                if (dataGridView1.Columns.Contains(c))
                    dataGridView1.Columns[c].DefaultCellStyle.Alignment =
                        DataGridViewContentAlignment.MiddleCenter;
            }

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }


        // ════════════════════════════════════════════════════════════════════
        //  STYLE GRID CHÍNH
        // ════════════════════════════════════════════════════════════════════
        private void StyleMainGrid()
        {
            dataGridView1.AllowUserToAddRows    = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly              = true;
            dataGridView1.RowHeadersVisible     = false;
            dataGridView1.SelectionMode         = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect           = false;
            dataGridView1.BorderStyle           = BorderStyle.None;
            dataGridView1.CellBorderStyle       = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.GridColor             = Color.FromArgb(220, 230, 245);
            dataGridView1.BackgroundColor       = Color.White;

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(21, 101, 192);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font      = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.ColumnHeadersHeight                     = 36;
            dataGridView1.ColumnHeadersHeightSizeMode             = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.EnableHeadersVisualStyles               = false;

            dataGridView1.DefaultCellStyle.BackColor          = Color.White;
            dataGridView1.DefaultCellStyle.ForeColor          = Color.FromArgb(30, 40, 60);
            dataGridView1.DefaultCellStyle.Font               = new Font("Segoe UI", 9.5f);
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(207, 226, 255);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.FromArgb(21, 101, 192);
            dataGridView1.RowTemplate.Height                  = 28;
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 249, 255);

            dataGridView1.CellFormatting += DgvMain_CellFormatting;
        }

        private void DgvMain_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || !dataGridView1.Columns.Contains("XepLoai")) return;
            if (e.ColumnIndex != dataGridView1.Columns["XepLoai"].Index) return;

            string val = e.Value?.ToString() ?? "";
            e.CellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            switch (val)
            {
                case "Xuất sắc":   e.CellStyle.ForeColor = Color.FromArgb(27, 94, 32);   break;
                case "Giỏi":       e.CellStyle.ForeColor = Color.FromArgb(51, 105, 30);  break;
                case "Khá":        e.CellStyle.ForeColor = Color.FromArgb(13, 71, 161);  break;
                case "Trung bình": e.CellStyle.ForeColor = Color.FromArgb(200, 100, 0);  break;
                case "Yếu":
                case "Kém":        e.CellStyle.ForeColor = Color.FromArgb(183, 28, 28);  break;
            }
        }

        // ════════════════════════════════════════════════════════════════════
        //  THỐNG KÊ CARDS  — FIX: phần gọi SetCardLabel bị thiếu trước đây
        // ════════════════════════════════════════════════════════════════════
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
                    case "Xuất sắc":   xuatSac++;   break;
                    case "Giỏi":       gioi++;       break;
                    case "Khá":        kha++;        break;
                    case "Trung bình": trungBinh++;  break;
                    case "Yếu":
                    case "Kém":        yeuKem++;     break;
                }
                if (row["GPA4"] != DBNull.Value)
                {
                    totalGPA += Convert.ToDouble(row["GPA4"]);
                    countGPA++;
                }
            }

            string gpaText = countGPA > 0
                ? (totalGPA / countGPA).ToString("F2")
                : "N/A";

            // Cập nhật labels trong các GroupBox card
            SetCardLabel(lblTong,    tongSV.ToString(),    Color.FromArgb(21, 101, 192));
            SetCardLabel(lblXuatsac, xuatSac.ToString(),   Color.FromArgb(27, 94, 32));
            SetCardLabel(lblGioi,    gioi.ToString(),      Color.FromArgb(51, 105, 30));
            SetCardLabel(lblKha,     kha.ToString(),       Color.FromArgb(13, 71, 161));
            SetCardLabel(lblTB,      trungBinh.ToString(), Color.FromArgb(200, 100, 0));
            SetCardLabel(lblYeu,     yeuKem.ToString(),    Color.FromArgb(183, 28, 28));
            SetCardLabel(lblDiemTB,  gpaText,              Color.FromArgb(106, 27, 154));
        }

        private void SetCardLabel(Label lbl, string text, Color color)
        {
            lbl.Text      = text;
            lbl.ForeColor = color;
            lbl.Font      = new Font("Segoe UI", 18f, FontStyle.Bold);
            lbl.AutoSize  = true;
        }


        // ════════════════════════════════════════════════════════════════════
        //  CHỌN DÒNG → LOAD CHI TIẾT
        // ════════════════════════════════════════════════════════════════════
        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;

            DataGridViewRow row = dataGridView1.SelectedRows[0];

            string maSV  = row.Cells["MaSV"]?.Value?.ToString()   ?? "";
            string hoTen = row.Cells["HoTen"]?.Value?.ToString()  ?? "";
            string lop   = row.Cells["TenLop"]?.Value?.ToString() ?? "";

            txtMa.Text    = maSV;
            txtTen.Text   = hoTen;
            txtLop.Text   = lop;
            txtNam.Text   = (cboNam.SelectedValue?.ToString()   == "ALL") ? "" : cboNam.Text;
            txthocky.Text = (cboHocky.SelectedValue?.ToString() == "ALL") ? "" : cboHocky.Text;

            if (!string.IsNullOrEmpty(maSV))
                LoadChiTietDiem(maSV);
        }

        // ════════════════════════════════════════════════════════════════════
        //  LOAD CHI TIẾT ĐIỂM TỪNG MÔN  (GUI → BLL → DAL)
        // ════════════════════════════════════════════════════════════════════
        private void LoadChiTietDiem(string maSV)
        {
            try
            {
                string maNamHoc = cboNam.SelectedValue?.ToString()   ?? "ALL";
                string maLoaiHK = cboHocky.SelectedValue?.ToString() ?? "ALL";
                if (string.IsNullOrEmpty(maNamHoc)) maNamHoc = "ALL";
                if (string.IsNullOrEmpty(maLoaiHK)) maLoaiHK = "ALL";

                // Gọi BLL → DAL
                DataTable dt = _bll.GetChiTietDiemSinhVien(maSV, maNamHoc, maLoaiHK);

                // Thêm cột tính toán
                if (!dt.Columns.Contains("DiemTongKet"))
                    dt.Columns.Add("DiemTongKet", typeof(double));
                if (!dt.Columns.Contains("DiemChu"))
                    dt.Columns.Add("DiemChu", typeof(string));
                if (!dt.Columns.Contains("DiemHe4"))
                    dt.Columns.Add("DiemHe4", typeof(double));

                foreach (DataRow row in dt.Rows)
                {
                    double cc  = row["DiemCC"]  != DBNull.Value ? Convert.ToDouble(row["DiemCC"])  : 0;
                    double kt1 = row["DiemKT1"] != DBNull.Value ? Convert.ToDouble(row["DiemKT1"]) : 0;
                    double kt2 = row["DiemKT2"] != DBNull.Value ? Convert.ToDouble(row["DiemKT2"]) : 0;
                    double ck  = row["DiemThi"] != DBNull.Value ? Convert.ToDouble(row["DiemThi"]) : 0;

                    bool hasGrade = row["DiemCC"]  != DBNull.Value || row["DiemKT1"] != DBNull.Value
                                 || row["DiemKT2"] != DBNull.Value || row["DiemThi"] != DBNull.Value;

                    if (hasGrade)
                    {
                        double tk   = KetQuaBLL.TinhDiemTongKet(cc, kt1, kt2, ck);
                        double gpa4 = KetQuaBLL.QuyDoiHe4(tk);
                        row["DiemTongKet"] = tk;
                        row["DiemChu"]     = KetQuaBLL.QuyDoiDiemChu(tk);
                        row["DiemHe4"]     = gpa4;
                    }
                    else
                    {
                        row["DiemTongKet"] = DBNull.Value;
                        row["DiemChu"]     = "—";
                        row["DiemHe4"]     = DBNull.Value;
                    }
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

            string[] colNames = { "MaLHP",    "TenMon",       "DiemCC",       "DiemKT1",
                                   "DiemKT2",  "DiemThi",      "DiemTongKet",  "DiemChu",  "DiemHe4" };
            string[] headers  = { "Mã LHP",   "Tên Môn Học",  "Chuyên Cần",   "KT1",
                                   "KT2",      "Cuối Kỳ",      "Tổng Kết (Hệ 10)", "Điểm Chữ", "Hệ 4" };

            for (int i = 0; i < colNames.Length; i++)
            {
                if (!dataGridView2.Columns.Contains(colNames[i])) continue;
                var col = dataGridView2.Columns[colNames[i]];
                col.Visible      = true;
                col.DisplayIndex = i;
                col.HeaderText   = headers[i];
            }

            foreach (string c in new[] { "DiemCC","DiemKT1","DiemKT2","DiemThi",
                                          "DiemTongKet","DiemChu","DiemHe4" })
            {
                if (dataGridView2.Columns.Contains(c))
                    dataGridView2.Columns[c].DefaultCellStyle.Alignment =
                        DataGridViewContentAlignment.MiddleCenter;
            }

            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }


        // ════════════════════════════════════════════════════════════════════
        //  STYLE GRID CHI TIẾT
        // ════════════════════════════════════════════════════════════════════
        private void StyleDetailGrid()
        {
            dataGridView2.AllowUserToAddRows    = false;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView2.ReadOnly              = true;
            dataGridView2.RowHeadersVisible     = false;
            dataGridView2.SelectionMode         = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.MultiSelect           = false;
            dataGridView2.BorderStyle           = BorderStyle.None;
            dataGridView2.CellBorderStyle       = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView2.GridColor             = Color.FromArgb(220, 230, 245);
            dataGridView2.BackgroundColor       = Color.White;

            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView2.ColumnHeadersDefaultCellStyle.Font      = new Font("Segoe UI", 9f, FontStyle.Bold);
            dataGridView2.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView2.ColumnHeadersHeight                     = 34;
            dataGridView2.ColumnHeadersHeightSizeMode             = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView2.EnableHeadersVisualStyles               = false;

            dataGridView2.DefaultCellStyle.BackColor          = Color.White;
            dataGridView2.DefaultCellStyle.ForeColor          = Color.FromArgb(30, 40, 60);
            dataGridView2.DefaultCellStyle.Font               = new Font("Segoe UI", 9f);
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.FromArgb(207, 226, 255);
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.FromArgb(21, 101, 192);
            dataGridView2.RowTemplate.Height                  = 26;
            dataGridView2.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 249, 255);

            dataGridView2.CellFormatting += DgvDetail_CellFormatting;
        }

        private void DgvDetail_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || !dataGridView2.Columns.Contains("DiemChu")) return;
            if (e.ColumnIndex != dataGridView2.Columns["DiemChu"].Index) return;

            string val = e.Value?.ToString() ?? "";
            e.CellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
            switch (val)
            {
                case "A+": case "A":
                    e.CellStyle.ForeColor = Color.FromArgb(27, 94, 32);  break;
                case "B+": case "B":
                    e.CellStyle.ForeColor = Color.FromArgb(13, 71, 161); break;
                case "C+": case "C":
                    e.CellStyle.ForeColor = Color.FromArgb(200, 100, 0); break;
                case "D+": case "D":
                    e.CellStyle.ForeColor = Color.FromArgb(183, 28, 28); break;
                case "F":
                    e.CellStyle.ForeColor = Color.White;
                    e.CellStyle.BackColor = Color.FromArgb(183, 28, 28);
                    break;
            }
        }

        // ════════════════════════════════════════════════════════════════════
        //  BUTTON EVENTS
        // ════════════════════════════════════════════════════════════════════
        private void BtnLoc_Click(object sender, EventArgs e)
        {
            LoadKetQua();
        }

        private void BtnLammoi_Click(object sender, EventArgs e)
        {
            _isLoadingCombo = true;
            try
            {
                if (cboNam.Items.Count   > 0) cboNam.SelectedIndex   = 0;
                if (cboHocky.Items.Count > 0) cboHocky.SelectedIndex = 0;
                if (cboLop.Items.Count   > 0) cboLop.SelectedIndex   = 0;
                txtMaTen.Text = "";
            }
            finally { _isLoadingCombo = false; }

            LoadKetQua(); // LoadKetQua đã gọi ResetChiTiet() bên trong
        }

        // Handler giữ lại từ Designer (cboNam.SelectedIndexChanged)
        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}
