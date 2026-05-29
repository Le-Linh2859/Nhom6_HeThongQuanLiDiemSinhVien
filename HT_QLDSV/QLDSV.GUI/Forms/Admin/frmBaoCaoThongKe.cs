using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.WinForms;
using QLDSV.BLL;

namespace QLDSV.GUI
{
    public partial class frmBaoCaoThongKe : Form, IShellChildForm
    {
        // ─── Flag chống đệ quy sự kiện ComboBox ────────────────────────────────
        private bool _isLoadingCombo = false;

        // ─── Khai báo các controls động ───────────────────────────────────────
        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel4;
        private System.Windows.Forms.Label labelDat;
        private System.Windows.Forms.Label lblDatTruot;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnReset;

        // Lưu danh sách môn học hiển thị trên biểu đồ cột để map khi click
        private List<string> _chartSubjectNames = new List<string>();

        public frmBaoCaoThongKe()
        {
            InitializeComponent();
            ThemeHelper.ApplyTheme(this);
            this.Load += new System.EventHandler(this.frmBaoCaoThongKe_Load);
            this.Resize += (s, ev) => MakeLayoutResponsive();
        }

        //  SHELL EMBED PATTERN
        // ═══════════════════════════════════════════════════════════════════════
        public void OnEmbeddedInShell()
        {
            string[] controlNames = {
                "pnlSidebar", "pnlHeader", "guna2ImageButton1", "label3", "label4",
                "guna2ImageButton2", "guna2CirclePictureBox1", "guna2HtmlLabel13",
                "guna2HtmlLabel14", "guna2ImageButton3"
            };
            foreach (var name in controlNames)
            {
                var ct = this.Controls.Find(name, true);
                foreach (var c in ct) c.Visible = false;
            }

            int shiftX = 0;
            var sidebarControls = this.Controls.Find("pnlSidebar", true);
            if (sidebarControls.Length > 0) shiftX = sidebarControls[0].Width;

            if (shiftX > 0)
            {
                foreach (Control ctrl in this.Controls)
                {
                    bool isHidden = Array.Exists(controlNames, n => n == ctrl.Name);
                    if (!isHidden && ctrl.Left > 0)
                        ctrl.Left = Math.Max(0, ctrl.Left - shiftX);
                }
            }

            SetAnchorAllGrids(this.Controls);
            MakeLayoutResponsive();
        }

        private void SetAnchorAllGrids(Control.ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                if (c is DataGridView dgv)
                    dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                if (c.Controls.Count > 0)
                    SetAnchorAllGrids(c.Controls);
            }
        }

        private void MakeLayoutResponsive()
        {
            try
            {
                // 1. Thẻ lọc (guna2Panel1 và tableLayoutPanel1)
                guna2Panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                guna2Panel1.Width = this.ClientSize.Width - (guna2Panel1.Left * 2);
                tableLayoutPanel1.Dock = DockStyle.Fill;
                guna2Panel1.Padding = new Padding(10, 5, 10, 5);

                guna2ComboBox1.Dock = DockStyle.Fill;
                guna2ComboBox2.Dock = DockStyle.Fill;
                guna2ComboBox3.Dock = DockStyle.Fill;
                guna2ComboBox4.Dock = DockStyle.Fill;

                // 2. tableLayoutPanel2 (Stat Cards)
                tableLayoutPanel2.Left = guna2Panel1.Left;
                tableLayoutPanel2.Width = guna2Panel1.Width;
                tableLayoutPanel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

                // 3. tableLayoutPanel3 (Charts)
                tableLayoutPanel3.Left = guna2Panel1.Left;
                tableLayoutPanel3.Width = guna2Panel1.Width;
                tableLayoutPanel3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

                tableLayoutPanel3.RowStyles.Clear();
                tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

                cartesianChart1.Dock = DockStyle.Fill;
                pieChart1.Dock = DockStyle.Fill;

                // 4. groupBox1 (Chi tiết điểm)
                groupBox1.Left = guna2Panel1.Left;
                groupBox1.Width = guna2Panel1.Width;
                groupBox1.Height = this.ClientSize.Height - groupBox1.Top - 15;
                groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

                // Đảm bảo Grid co giãn bên trong GroupBox
                guna2DataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                guna2DataGridView1.Width = groupBox1.Width - (guna2DataGridView1.Left * 2);
                guna2DataGridView1.Height = groupBox1.Height - guna2DataGridView1.Top - 15;

                // Đảm bảo các nút chức năng neo về góc trên bên phải
                button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                if (btnPrint != null) btnPrint.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                if (btnReset != null) btnReset.Anchor = AnchorStyles.Top | AnchorStyles.Right;

                // Tính toán lại vị trí các nút động dựa trên kích thước thực tế của GroupBox
                button1.Location = new Point(groupBox1.Width - button1.Width - 15, 16);
                if (btnPrint != null) btnPrint.Location = new Point(button1.Left - btnPrint.Width - 6, 16);
                if (btnReset != null) btnReset.Location = new Point((btnPrint?.Left ?? button1.Left) - (btnReset?.Width ?? 100) - 6, 16);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("MakeLayoutResponsive Error: " + ex.Message);
            }
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  FORM LOAD
        // ═══════════════════════════════════════════════════════════════════════
        private void frmBaoCaoThongKe_Load(object sender, EventArgs e)
        {
            try
            {
                FunctionQa.ketnoi();

                // Gắn event handler cho nút xuất Excel
                button1.Click += BtnXuatExcel_Click;

                // Tải các ComboBox lọc (theo thứ tự: Khoa → Lớp niên chế → Học kỳ → Môn học)
                LoadComboKhoa();
                LoadComboHocKy();
                LoadComboMonHoc();
                LoadComboLopNienChe();

                // Khởi tạo các tính năng tương tác nâng cao & UI động
                InitializeInteractiveFeatures();

                // Thiết lập Responsive cho Layout
                MakeLayoutResponsive();

                // Gắn sự kiện cascading
                guna2ComboBox2.SelectedIndexChanged += CboKhoa_SelectedIndexChanged;   // cbo Khoa
                guna2ComboBox3.SelectedIndexChanged += CboLop_SelectedIndexChanged;    // cbo Lớp niên chế
                guna2ComboBox1.SelectedIndexChanged += CboHocKy_SelectedIndexChanged;  // cbo Học kỳ
                guna2ComboBox4.SelectedIndexChanged += CboMon_SelectedIndexChanged;    // cbo Môn học

                // Nạp dữ liệu tổng quan lần đầu
                LoadBaoCao();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo form Báo cáo: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  INITIALIZE INTERACTIVE FEATURES
        // ═══════════════════════════════════════════════════════════════════════
        private void InitializeInteractiveFeatures()
        {
            // 1. Thêm 4th stat card (Tỷ lệ Đạt / Trượt) vào tableLayoutPanel2
            var tblDat = new TableLayoutPanel();
            tblDat.ColumnCount = 2;
            tblDat.RowCount = 3;
            tblDat.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 75F));
            tblDat.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tblDat.RowStyles.Add(new RowStyle(SizeType.Percent, 33.333F));
            tblDat.RowStyles.Add(new RowStyle(SizeType.Percent, 43.333F));
            tblDat.RowStyles.Add(new RowStyle(SizeType.Percent, 23.333F));
            tblDat.Size = new Size(250, 60);
            tblDat.Location = new Point(3, 3);
            tblDat.Dock = DockStyle.Fill;

            labelDat = new Label();
            labelDat.Text = "TỶ LỆ ĐẠT / TRƯỢT";
            labelDat.Font = new Font("Segoe UI", 8.25f, FontStyle.Bold);
            labelDat.ForeColor = Color.FromArgb(0, 0, 64);
            labelDat.AutoSize = true;
            labelDat.Padding = new Padding(0, 3, 0, 0);

            lblDatTruot = new Label();
            lblDatTruot.Text = "N/A";
            lblDatTruot.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
            lblDatTruot.ForeColor = Color.FromArgb(0, 0, 64);
            lblDatTruot.AutoSize = true;

            tblDat.Controls.Add(labelDat, 0, 0);
            tblDat.Controls.Add(lblDatTruot, 0, 1);

            guna2ShadowPanel4 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            guna2ShadowPanel4.FillColor = Color.White;
            guna2ShadowPanel4.Radius = 5;
            guna2ShadowPanel4.ShadowColor = Color.MediumPurple;
            guna2ShadowPanel4.ShadowStyle = Guna.UI2.WinForms.Guna2ShadowPanel.ShadowMode.Dropped;
            guna2ShadowPanel4.Dock = DockStyle.Fill;
            guna2ShadowPanel4.Controls.Add(tblDat);

            // Cấu hình lại layout và dock cho tất cả các card thống kê để co giãn tự động đẹp mắt
            guna2ShadowPanel1.Dock = DockStyle.Fill;
            guna2ShadowPanel2.Dock = DockStyle.Fill;
            guna2ShadowPanel3.Dock = DockStyle.Fill;

            tableLayoutPanel2.ColumnCount = 4;
            tableLayoutPanel2.ColumnStyles.Clear();
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));

            tableLayoutPanel2.Controls.Add(guna2ShadowPanel4, 3, 0);

            // 2. Thêm nút "In Báo cáo" cạnh nút "Xuất Excel"
            btnPrint = new Button();
            btnPrint.Text = "In Báo cáo";
            btnPrint.BackColor = Color.FromArgb(21, 101, 192); // Deep Blue chuyên nghiệp
            btnPrint.FlatStyle = FlatStyle.Flat;
            btnPrint.ForeColor = Color.White;
            btnPrint.Font = new Font("Segoe UI", 9.75f, FontStyle.Bold);
            btnPrint.Size = new Size(113, 27);
            btnPrint.Location = new Point(635, 16);
            btnPrint.UseVisualStyleBackColor = false;
            btnPrint.Click += BtnPrint_Click;
            groupBox1.Controls.Add(btnPrint);

            // 3. Thêm nút "Xem tất cả" (Reset bộ lọc biểu đồ & combobox)
            btnReset = new Button();
            btnReset.Text = "Xem tất cả";
            btnReset.BackColor = Color.FromArgb(100, 88, 255); // Màu tím nhạt
            btnReset.FlatStyle = FlatStyle.Flat;
            btnReset.ForeColor = Color.White;
            btnReset.Font = new Font("Segoe UI", 9.75f, FontStyle.Bold);
            btnReset.Size = new Size(100, 27);
            btnReset.Location = new Point(529, 16);
            btnReset.UseVisualStyleBackColor = false;
            btnReset.Click += BtnReset_Click;
            groupBox1.Controls.Add(btnReset);

            // 4. Đăng ký sự kiện click biểu đồ để tương tác lọc dữ liệu
            pieChart1.DataClick += PieChart1_DataClick;
            cartesianChart1.DataClick += CartesianChart1_DataClick;

            // 5. Cấu hình Tooltip cho biểu đồ
            pieChart1.DataTooltip = new DefaultTooltip();
            cartesianChart1.DataTooltip = new DefaultTooltip();
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  INTERACTIVE CHART CLICK EVENTS
        // ═══════════════════════════════════════════════════════════════════════
        private void PieChart1_DataClick(object sender, ChartPoint chartPoint)
        {
            try
            {
                string gradeTitle = chartPoint.SeriesView.Title; // Ví dụ: "Giỏi (A)", "Khá (B)", ...
                DataTable dt = guna2DataGridView1.DataSource as DataTable;
                if (dt == null) return;

                string gradeLetter = "";
                if (gradeTitle.Contains("(A)")) gradeLetter = "A";
                else if (gradeTitle.Contains("(B)")) gradeLetter = "B";
                else if (gradeTitle.Contains("(C)")) gradeLetter = "C";
                else if (gradeTitle.Contains("(D)")) gradeLetter = "D";
                else if (gradeTitle.Contains("(F)")) gradeLetter = "F";

                if (!string.IsNullOrEmpty(gradeLetter))
                {
                    dt.DefaultView.RowFilter = $"[Xếp Loại] LIKE '{gradeLetter}%'";
                }
                else
                {
                    dt.DefaultView.RowFilter = "";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("PieChart DataClick Error: " + ex.Message);
            }
        }

        private void CartesianChart1_DataClick(object sender, ChartPoint chartPoint)
        {
            try
            {
                int index = (int)chartPoint.X;
                if (index >= 0 && index < _chartSubjectNames.Count)
                {
                    string subjectName = _chartSubjectNames[index];
                    DataTable dt = guna2DataGridView1.DataSource as DataTable;
                    if (dt == null) return;

                    dt.DefaultView.RowFilter = $"[Môn Học] = '{subjectName.Replace("'", "''")}'";
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("CartesianChart DataClick Error: " + ex.Message);
            }
        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = guna2DataGridView1.DataSource as DataTable;
                if (dt != null)
                {
                    dt.DefaultView.RowFilter = "";
                }

                _isLoadingCombo = true;
                try
                {
                    guna2ComboBox2.SelectedIndex = 0; // Reset Khoa
                    guna2ComboBox3.SelectedIndex = 0; // Reset Lớp
                    guna2ComboBox1.SelectedIndex = 0; // Reset Học kỳ
                    guna2ComboBox4.SelectedIndex = 0; // Reset Môn học
                }
                finally
                {
                    _isLoadingCombo = false;
                }

                LoadBaoCao();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Reset Error: " + ex.Message);
            }
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  PRINT REPORT SYSTEM (PrintPreviewDialog)
        // ═══════════════════════════════════════════════════════════════════════
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                System.Drawing.Printing.PrintDocument printDoc = new System.Drawing.Printing.PrintDocument();
                printDoc.DocumentName = "Báo cáo Thống kê Kết quả Học tập";
                printDoc.PrintPage += PrintDoc_PrintPage;

                PrintPreviewDialog previewDlg = new PrintPreviewDialog();
                previewDlg.Document = printDoc;
                previewDlg.Width = 1024;
                previewDlg.Height = 768;
                previewDlg.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi chạy in: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            DataTable dt = guna2DataGridView1.DataSource as DataTable;
            if (dt == null) return;

            Graphics g = e.Graphics;
            Font fontTitle = new Font("Segoe UI", 16f, FontStyle.Bold);
            Font fontHeader = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            Font fontBody = new Font("Segoe UI", 9f, FontStyle.Regular);
            Brush brush = Brushes.Black;
            Pen pen = new Pen(Color.Gray, 1);

            int y = 50;

            // Header Tiêu đề
            g.DrawString("BÁO CÁO THỐNG KÊ KẾT QUẢ HỌC TẬP", fontTitle, brush, new PointF(100, y));
            y += 40;

            // Ngày lập và bộ lọc hiện tại
            g.DrawString($"Ngày lập báo cáo: {DateTime.Now:dd/MM/yyyy HH:mm}", fontBody, brush, new PointF(100, y));
            y += 20;

            string filterText = "Bộ lọc: ";
            if (guna2ComboBox2.SelectedIndex > 0) filterText += $"Khoa: {guna2ComboBox2.Text}  ";
            if (guna2ComboBox3.SelectedIndex > 0) filterText += $"Lớp: {guna2ComboBox3.Text}  ";
            if (guna2ComboBox1.SelectedIndex > 0) filterText += $"Học kỳ: {guna2ComboBox1.Text}  ";
            if (guna2ComboBox4.SelectedIndex > 0) filterText += $"Môn: {guna2ComboBox4.Text}  ";
            if (filterText == "Bộ lọc: ") filterText += "Tất cả dữ liệu";

            g.DrawString(filterText, fontBody, brush, new PointF(100, y));
            y += 35;

            // Vẽ bảng Grid dữ liệu
            int xStart = 40;
            int[] colWidths = { 80, 130, 70, 140, 45, 45, 45, 45, 65, 80 }; // Tổng: 755px
            string[] colsToPrint = { "Mã SV", "Họ Tên", "Lớp", "Môn Học", "CC", "KT1", "KT2", "CK", "Tổng Kết", "Xếp Loại" };

            // In Header Cột
            int currentX = xStart;
            for (int i = 0; i < colsToPrint.Length; i++)
            {
                g.DrawRectangle(pen, currentX, y, colWidths[i], 25);
                g.DrawString(colsToPrint[i], fontHeader, brush, new PointF(currentX + 4, y + 4));
                currentX += colWidths[i];
            }
            y += 25;

            // In dữ liệu từng dòng
            DataView dv = dt.DefaultView;
            for (int rowIdx = 0; rowIdx < dv.Count; rowIdx++)
            {
                if (y > e.MarginBounds.Bottom - 30) // Xử lý tràn trang tự động
                {
                    e.HasMorePages = true;
                    return;
                }

                DataRowView dr = dv[rowIdx];
                currentX = xStart;
                for (int colIdx = 0; colIdx < colsToPrint.Length; colIdx++)
                {
                    string colName = colsToPrint[colIdx];
                    string val = "";
                    if (dr[colName] != DBNull.Value)
                    {
                        if (colName == "Tổng Kết")
                            val = Convert.ToDouble(dr[colName]).ToString("F2");
                        else
                            val = dr[colName].ToString();
                    }

                    g.DrawRectangle(pen, currentX, y, colWidths[colIdx], 22);

                    // Cắt chữ nếu dài hơn độ rộng của cột
                    string drawVal = val;
                    if (g.MeasureString(drawVal, fontBody).Width > colWidths[colIdx] - 8)
                    {
                        while (drawVal.Length > 0 && g.MeasureString(drawVal + "...", fontBody).Width > colWidths[colIdx] - 8)
                            drawVal = drawVal.Substring(0, drawVal.Length - 1);
                        drawVal += "...";
                    }

                    g.DrawString(drawVal, fontBody, brush, new PointF(currentX + 4, y + 3));
                    currentX += colWidths[colIdx];
                }
                y += 22;
            }

            e.HasMorePages = false;
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  LOAD COMBOBOXES
        // ═══════════════════════════════════════════════════════════════════════
        private void LoadComboKhoa()
        {
            _isLoadingCombo = true;
            try
            {
                string sql = "SELECT '' AS MaKhoa, N'-- Tất cả Khoa --' AS TenKhoa " +
                             "UNION SELECT MaKhoa, TenKhoa FROM Khoa ORDER BY MaKhoa";
                FunctionQa.fillcombo(sql, guna2ComboBox2, "MaKhoa", "TenKhoa");
            }
            finally { _isLoadingCombo = false; }
        }

        private void LoadComboLopNienChe(string maKhoa = "")
        {
            _isLoadingCombo = true;
            try
            {
                string filter = string.IsNullOrEmpty(maKhoa)
                    ? ""
                    : $" WHERE lnc.MaKhoa = '{maKhoa}'";
                string sql = "SELECT '' AS MaLopNienChe, N'-- Tất cả Lớp --' AS TenLop " +
                             "UNION SELECT lnc.MaLopNienChe, lnc.TenLop FROM LopNienChe lnc" +
                             filter + " ORDER BY MaLopNienChe";
                FunctionQa.fillcombo(sql, guna2ComboBox3, "MaLopNienChe", "TenLop");
            }
            finally { _isLoadingCombo = false; }
        }

        private void LoadComboHocKy()
        {
            _isLoadingCombo = true;
            try
            {
                string sql = "SELECT '' AS MaHKNH, N'-- Tất cả Học kỳ --' AS TenHK " +
                             "UNION SELECT hknh.MaHKNH, (lhk.TenLoaiHK + N' - ' + nh.TenNamHoc) AS TenHK " +
                             "FROM HocKy_NamHoc hknh " +
                             "INNER JOIN LoaiHocKy lhk ON hknh.MaLoaiHK = lhk.MaLoaiHK " +
                             "INNER JOIN NamHoc nh ON hknh.MaNamHoc = nh.MaNamHoc " +
                             "ORDER BY MaHKNH";
                FunctionQa.fillcombo(sql, guna2ComboBox1, "MaHKNH", "TenHK");
            }
            finally { _isLoadingCombo = false; }
        }

        private void LoadComboMonHoc(string maKhoa = "")
        {
            _isLoadingCombo = true;
            try
            {
                string filter = string.IsNullOrEmpty(maKhoa)
                    ? ""
                    : $" WHERE mh.MaKhoa = '{maKhoa}'";
                string sql = "SELECT '' AS MaMon, N'-- Tất cả Môn --' AS TenMon " +
                             "UNION SELECT mh.MaMon, mh.TenMon FROM MonHoc mh" +
                             filter + " ORDER BY MaMon";
                FunctionQa.fillcombo(sql, guna2ComboBox4, "MaMon", "TenMon");
            }
            finally { _isLoadingCombo = false; }
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  CASCADING EVENTS
        // ═══════════════════════════════════════════════════════════════════════
        private void CboKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoadingCombo) return;
            string maKhoa = guna2ComboBox2.SelectedValue?.ToString() ?? "";
            LoadComboLopNienChe(maKhoa);
            LoadComboMonHoc(maKhoa);
            LoadBaoCao();
        }

        private void CboLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoadingCombo) return;
            LoadBaoCao();
        }

        private void CboHocKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoadingCombo) return;
            LoadBaoCao();
        }

        private void CboMon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoadingCombo) return;
            LoadBaoCao();
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  LOAD BÁO CÁO CHÍNH
        // ═══════════════════════════════════════════════════════════════════════
        private void LoadBaoCao()
        {
            try
            {
                string maKhoa = guna2ComboBox2.SelectedValue?.ToString() ?? "";
                string maLop  = guna2ComboBox3.SelectedValue?.ToString() ?? "";
                string maHK   = guna2ComboBox1.SelectedValue?.ToString() ?? "";
                string maMon  = guna2ComboBox4.SelectedValue?.ToString() ?? "";

                // ── Xây dựng điều kiện WHERE động ──────────────────────────────
                var conditions = new List<string>();
                if (!string.IsNullOrEmpty(maKhoa)) conditions.Add($"k.MaKhoa = '{maKhoa}'");
                if (!string.IsNullOrEmpty(maLop))  conditions.Add($"sv.MaLopNienChe = '{maLop}'");
                if (!string.IsNullOrEmpty(maHK))   conditions.Add($"lhp.MaHKNH = '{maHK}'");
                if (!string.IsNullOrEmpty(maMon))  conditions.Add($"mh.MaMon = '{maMon}'");
                string where = conditions.Count > 0
                    ? "WHERE " + string.Join(" AND ", conditions)
                    : "";

                // ── Câu truy vấn lấy chi tiết điểm từng sinh viên (đã fix toàn bộ lỗi SQL) ──
                string sqlDetail = $@"
                    SELECT
                        sv.MaSV               AS [Mã SV],
                        sv.HoTen              AS [Họ Tên],
                        lnc.TenLop            AS [Lớp],
                        k.TenKhoa             AS [Khoa],
                        mh.TenMon             AS [Môn Học],
                        mh.SoTC               AS [TC],
                        (lhk.TenLoaiHK + N' - ' + nh.TenNamHoc) AS [Học Kỳ],
                        MAX(CASE WHEN kq.MaLoaiDiem='CC'  THEN kq.Diem END) AS [CC],
                        MAX(CASE WHEN kq.MaLoaiDiem='KT1' THEN kq.Diem END) AS [KT1],
                        MAX(CASE WHEN kq.MaLoaiDiem='KT2' THEN kq.Diem END) AS [KT2],
                        MAX(CASE WHEN kq.MaLoaiDiem='CK'  THEN kq.Diem END) AS [CK]
                    FROM SinhVien sv
                    JOIN LopNienChe lnc ON sv.MaLopNienChe = lnc.MaLopNienChe
                    JOIN Khoa k ON lnc.MaKhoa = k.MaKhoa
                    JOIN DangKyLopHoc dk ON dk.MaSV = sv.MaSV
                    JOIN LopHocPhan lhp ON lhp.MaLHP = dk.MaLHP
                    JOIN MonHoc mh ON mh.MaMon = lhp.MaMon
                    JOIN HocKy_NamHoc hknh ON hknh.MaHKNH = lhp.MaHKNH
                    JOIN LoaiHocKy lhk ON hknh.MaLoaiHK = lhk.MaLoaiHK
                    JOIN NamHoc nh ON hknh.MaNamHoc = nh.MaNamHoc
                    LEFT JOIN KetQua kq ON kq.MaLHP = dk.MaLHP AND kq.MaSV = dk.MaSV
                    {where}
                    GROUP BY sv.MaSV, sv.HoTen, lnc.TenLop, k.TenKhoa,
                             mh.TenMon, mh.SoTC, lhk.TenLoaiHK, nh.TenNamHoc";

                DataTable dtDetail = FunctionQa.getdatatotable(sqlDetail);

                // ── Tính cột điểm tổng kết & điểm chữ ─────────────────────────
                dtDetail.Columns.Add("Tổng Kết", typeof(double));
                dtDetail.Columns.Add("Xếp Loại", typeof(string));

                foreach (DataRow row in dtDetail.Rows)
                {
                    double cc  = row["CC"]  != DBNull.Value ? Convert.ToDouble(row["CC"])  : 0;
                    double kt1 = row["KT1"] != DBNull.Value ? Convert.ToDouble(row["KT1"]) : 0;
                    double kt2 = row["KT2"] != DBNull.Value ? Convert.ToDouble(row["KT2"]) : 0;
                    double ck  = row["CK"]  != DBNull.Value ? Convert.ToDouble(row["CK"])  : 0;

                    bool hasGrade = row["CC"]  != DBNull.Value || row["KT1"] != DBNull.Value ||
                                   row["KT2"] != DBNull.Value || row["CK"]  != DBNull.Value;

                    if (hasGrade)
                    {
                        double tk = KetQuaBLL.TinhDiemTongKet(cc, kt1, kt2, ck);
                        row["Tổng Kết"] = tk;
                        if      (tk >= 8.5) row["Xếp Loại"] = "A  (Giỏi)";
                        else if (tk >= 7.0) row["Xếp Loại"] = "B  (Khá)";
                        else if (tk >= 5.5) row["Xếp Loại"] = "C  (TB)";
                        else if (tk >= 4.0) row["Xếp Loại"] = "D  (Yếu)";
                        else                 row["Xếp Loại"] = "F  (Trượt)";
                    }
                    else
                    {
                        row["Tổng Kết"] = DBNull.Value;
                        row["Xếp Loại"] = "Chưa có điểm";
                    }
                }

                guna2DataGridView1.DataSource = dtDetail;
                ApplyGridStyle();

                // ── Thống kê các thẻ tóm tắt (bao gồm Đạt / Trượt) ─────────────────
                UpdateStatCards(dtDetail, where);

                // ── Vẽ/Cập nhật Biểu đồ LiveCharts ────────────────────────────────
                UpdateCharts(dtDetail);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("LoadBaoCao Error: " + ex.Message);
            }
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  THỐNG KÊ 4 THẺ
        // ═══════════════════════════════════════════════════════════════════════
        private void UpdateStatCards(DataTable dtDetail, string where)
        {
            try
            {
                // 1. Tổng số sinh viên (distinct)
                var distinctSV = new HashSet<string>();
                foreach (DataRow r in dtDetail.Rows)
                    distinctSV.Add(r["Mã SV"].ToString());
                lblTongSV.Text = distinctSV.Count.ToString("N0");

                // 2. GPA trung bình (chỉ tính những dòng đã có điểm tổng kết)
                double totalGPA = 0; int countGPA = 0;
                int countA = 0, countB = 0, countC = 0, countD = 0, countF = 0;
                foreach (DataRow r in dtDetail.Rows)
                {
                    if (r["Tổng Kết"] != DBNull.Value)
                    {
                        double tk = Convert.ToDouble(r["Tổng Kết"]);
                        totalGPA += tk;
                        countGPA++;

                        string xl = r["Xếp Loại"]?.ToString() ?? "";
                        if      (xl.StartsWith("A")) countA++;
                        else if (xl.StartsWith("B")) countB++;
                        else if (xl.StartsWith("C")) countC++;
                        else if (xl.StartsWith("D")) countD++;
                        else if (xl.StartsWith("F")) countF++;
                    }
                }
                lblGPA.Text = countGPA > 0
                    ? (totalGPA / countGPA).ToString("F2")
                    : "N/A";

                // 3. Cảnh báo học vụ (đếm từ bảng CanhBao_SinhVien có lọc)
                string sqlCB = "SELECT COUNT(*) FROM CanhBao_SinhVien";
                string maLop = guna2ComboBox3.SelectedValue?.ToString() ?? "";
                if (!string.IsNullOrEmpty(maLop))
                    sqlCB = $"SELECT COUNT(*) FROM CanhBao_SinhVien cb " +
                             $"JOIN SinhVien sv ON cb.MaSV = sv.MaSV WHERE sv.MaLopNienChe = '{maLop}'";
                string cbCount = FunctionQa.getfieldvalue(sqlCB);
                lblcbhv.Text = string.IsNullOrEmpty(cbCount) ? "0" : cbCount;

                lblcbhv.ForeColor = (int.TryParse(cbCount, out int n) && n > 0)
                    ? Color.Firebrick
                    : Color.FromArgb(21, 101, 192);

                // 4. Thống kê tỷ lệ Đạt / Trượt
                int passCount = countA + countB + countC + countD;
                int failCount = countF;
                int totalGraded = passCount + failCount;

                if (totalGraded > 0)
                {
                    double passRate = Math.Round((double)passCount / totalGraded * 100, 1);
                    double failRate = Math.Round((double)failCount / totalGraded * 100, 1);
                    lblDatTruot.Text = $"{passRate}% / {failRate}%";

                    if (passRate >= 75)
                        lblDatTruot.ForeColor = Color.Green;
                    else if (passRate >= 50)
                        lblDatTruot.ForeColor = Color.DarkOrange;
                    else
                        lblDatTruot.ForeColor = Color.Firebrick;
                }
                else
                {
                    lblDatTruot.Text = "N/A";
                    lblDatTruot.ForeColor = Color.FromArgb(0, 0, 64);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("UpdateStatCards Error: " + ex.Message);
            }
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  BIỂU ĐỒ LIVECHARTS
        // ═══════════════════════════════════════════════════════════════════════
        private void UpdateCharts(DataTable dtDetail)
        {
            try
            {
                // ── PIE CHART: Phân bố xếp loại học lực ──────────────────────
                int countA = 0, countB = 0, countC = 0, countD = 0, countF = 0;
                foreach (DataRow r in dtDetail.Rows)
                {
                    string xl = r["Xếp Loại"]?.ToString() ?? "";
                    if      (xl.StartsWith("A")) countA++;
                    else if (xl.StartsWith("B")) countB++;
                    else if (xl.StartsWith("C")) countC++;
                    else if (xl.StartsWith("D")) countD++;
                    else if (xl.StartsWith("F")) countF++;
                }

                pieChart1.Series = new SeriesCollection
                {
                    new PieSeries { Title = "Giỏi (A)",   Values = new ChartValues<double> { countA }, Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(21, 101, 192))  },
                    new PieSeries { Title = "Khá (B)",    Values = new ChartValues<double> { countB }, Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(100, 88, 255))  },
                    new PieSeries { Title = "TB (C)",     Values = new ChartValues<double> { countC }, Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(25, 185, 154))  },
                    new PieSeries { Title = "Yếu (D)",    Values = new ChartValues<double> { countD }, Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 152, 0))   },
                    new PieSeries { Title = "Trượt (F)",  Values = new ChartValues<double> { countF }, Fill = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(211, 47, 47))   },
                };
                pieChart1.LegendLocation = LegendLocation.Right;

                // ── CARTESIAN CHART: Tỷ lệ trượt theo môn học (Top 10) ────────
                var monStats = new Dictionary<string, (int total, int fail)>();
                foreach (DataRow r in dtDetail.Rows)
                {
                    string tenMon = r["Môn Học"].ToString();
                    string xl     = r["Xếp Loại"]?.ToString() ?? "";
                    if (r["Tổng Kết"] == DBNull.Value) continue;

                    if (!monStats.ContainsKey(tenMon)) monStats[tenMon] = (0, 0);
                    var st = monStats[tenMon];
                    st.total++;
                    if (xl.StartsWith("F")) st.fail++;
                    monStats[tenMon] = st;
                }

                var top10 = monStats
                    .Where(kv => kv.Value.total > 0)
                    .OrderByDescending(kv => (double)kv.Value.fail / kv.Value.total)
                    .Take(10)
                    .ToList();

                // Lưu danh sách môn học gốc để map click
                _chartSubjectNames.Clear();
                foreach (var item in top10)
                {
                    _chartSubjectNames.Add(item.Key);
                }

                if (top10.Count > 0)
                {
                    var labels  = top10.Select(kv => kv.Key.Length > 12 ? kv.Key.Substring(0, 12) + "…" : kv.Key).ToArray();
                    var values  = top10.Select(kv => Math.Round((double)kv.Value.fail / kv.Value.total * 100, 1)).ToArray();

                    cartesianChart1.Series = new SeriesCollection
                    {
                        new ColumnSeries
                        {
                            Title  = "Tỷ lệ trượt (%)",
                            Values = new ChartValues<double>(values),
                            Fill   = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(211, 47, 47)),
                            DataLabels = true,
                            LabelPoint = p => p.Y.ToString("F1") + "%"
                        }
                    };

                    cartesianChart1.AxisX.Clear();
                    cartesianChart1.AxisX.Add(new Axis
                    {
                        Title  = "Môn học",
                        Labels = labels,
                        LabelsRotation = -30
                    });

                    cartesianChart1.AxisY.Clear();
                    cartesianChart1.AxisY.Add(new Axis
                    {
                        Title    = "Tỷ lệ (%)",
                        MinValue = 0,
                        MaxValue = 100
                    });
                }
                else
                {
                    cartesianChart1.Series = new SeriesCollection
                    {
                        new ColumnSeries
                        {
                            Title  = "Tỷ lệ trượt (%)",
                            Values = new ChartValues<double> { 0 },
                            Fill   = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(200, 200, 200))
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("UpdateCharts Error: " + ex.Message);
            }
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  STYLE GRID
        // ═══════════════════════════════════════════════════════════════════════
        private void ApplyGridStyle()
        {
            try
            {
                guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                guna2DataGridView1.AllowUserToAddRows  = false;
                guna2DataGridView1.SelectionMode       = DataGridViewSelectionMode.FullRowSelect;
                guna2DataGridView1.ReadOnly            = true;
                guna2DataGridView1.RowHeadersVisible   = false;

                // Tô màu cột Xếp Loại theo giá trị
                guna2DataGridView1.CellFormatting += (s, e) =>
                {
                    if (guna2DataGridView1.Columns.Count == 0) return;
                    var col = guna2DataGridView1.Columns["Xếp Loại"];
                    if (col == null || e.ColumnIndex != col.Index || e.RowIndex < 0) return;

                    string val = e.Value?.ToString() ?? "";
                    if      (val.StartsWith("A")) e.CellStyle.ForeColor = Color.FromArgb(21, 101, 192);
                    else if (val.StartsWith("B")) e.CellStyle.ForeColor = Color.FromArgb(100, 88, 255);
                    else if (val.StartsWith("C")) e.CellStyle.ForeColor = Color.FromArgb(25, 185, 154);
                    else if (val.StartsWith("D")) e.CellStyle.ForeColor = Color.FromArgb(255, 152, 0);
                    else if (val.StartsWith("F")) e.CellStyle.ForeColor = Color.Firebrick;
                    e.CellStyle.Font = new Font("Segoe UI", 9.75f, FontStyle.Bold);
                };
            }
            catch { }
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  XUẤT EXCEL (UTF-8 BOM CSV)
        // ═══════════════════════════════════════════════════════════════════════
        private void BtnXuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = guna2DataGridView1.DataSource as DataTable;
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var dlg = new SaveFileDialog())
                {
                    dlg.Title  = "Lưu báo cáo Excel";
                    dlg.Filter = "CSV UTF-8 (*.csv)|*.csv|Tất cả (*.*)|*.*";
                    dlg.FileName = $"BaoCaoThongKe_{DateTime.Now:yyyyMMdd_HHmm}.csv";

                    if (dlg.ShowDialog() != DialogResult.OK) return;

                    // UTF-8 BOM để Excel nhận diện đúng tiếng Việt
                    using (var sw = new StreamWriter(dlg.FileName, false, new UTF8Encoding(true)))
                    {
                        // Header row
                        var headers = new List<string>();
                        foreach (DataColumn col in dt.Columns)
                            headers.Add($"\"{col.ColumnName}\"");
                        sw.WriteLine(string.Join(",", headers));

                        // Data rows
                        foreach (DataRow row in dt.Rows)
                        {
                            var cells = new List<string>();
                            foreach (var item in row.ItemArray)
                            {
                                string val = item == DBNull.Value ? "" : item.ToString();
                                cells.Add($"\"{val.Replace("\"", "\"\"")}\"");
                            }
                            sw.WriteLine(string.Join(",", cells));
                        }
                    }

                    MessageBox.Show($"Đã xuất thành công!\n{dlg.FileName}", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Mở file sau khi xuất
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName        = dlg.FileName,
                        UseShellExecute = true
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất Excel: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
