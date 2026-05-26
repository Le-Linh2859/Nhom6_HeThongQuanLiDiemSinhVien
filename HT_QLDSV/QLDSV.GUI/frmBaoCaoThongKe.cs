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

namespace QLDSV.GUI
{
    public partial class frmBaoCaoThongKe : Form, IShellChildForm
    {
        // ─── Flag chống đệ quy sự kiện ComboBox ────────────────────────────────
        private bool _isLoadingCombo = false;

        public frmBaoCaoThongKe()
        {
            InitializeComponent();
            ThemeHelper.ApplyTheme(this);
        }

        // ═══════════════════════════════════════════════════════════════════════
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
            if (shiftX == 0) return;

            foreach (Control ctrl in this.Controls)
            {
                bool isHidden = Array.Exists(controlNames, n => n == ctrl.Name);
                if (!isHidden && ctrl.Left > 0)
                    ctrl.Left = Math.Max(0, ctrl.Left - shiftX);
            }

            SetAnchorAllGrids(this.Controls);
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
                string sql = "SELECT '' AS MaLop, N'-- Tất cả Lớp --' AS TenLop " +
                             "UNION SELECT lnc.MaLop, lnc.TenLop FROM LopNienChe lnc" +
                             filter + " ORDER BY MaLop";
                FunctionQa.fillcombo(sql, guna2ComboBox3, "MaLop", "TenLop");
            }
            finally { _isLoadingCombo = false; }
        }

        private void LoadComboHocKy()
        {
            _isLoadingCombo = true;
            try
            {
                string sql = "SELECT '' AS MaHKNH, N'-- Tất cả Học kỳ --' AS TenHK " +
                             "UNION SELECT MaHKNH, TenHK FROM HocKyNamHoc ORDER BY MaHKNH";
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
                if (!string.IsNullOrEmpty(maLop))  conditions.Add($"sv.MaLop = '{maLop}'");
                if (!string.IsNullOrEmpty(maHK))   conditions.Add($"lhp.MaHKNH = '{maHK}'");
                if (!string.IsNullOrEmpty(maMon))  conditions.Add($"mh.MaMon = '{maMon}'");
                string where = conditions.Count > 0
                    ? "WHERE " + string.Join(" AND ", conditions)
                    : "";

                // ── Câu truy vấn lấy chi tiết điểm từng sinh viên ──────────────
                string sqlDetail = $@"
                    SELECT
                        sv.MaSV               AS [Mã SV],
                        sv.HoTen              AS [Họ Tên],
                        lnc.TenLop            AS [Lớp],
                        k.TenKhoa             AS [Khoa],
                        mh.TenMon             AS [Môn Học],
                        mh.SoTC               AS [TC],
                        hk.TenHK              AS [Học Kỳ],
                        MAX(CASE WHEN kq.MaLoaiDiem='CC'  THEN kq.Diem END) AS [CC],
                        MAX(CASE WHEN kq.MaLoaiDiem='KT1' THEN kq.Diem END) AS [KT1],
                        MAX(CASE WHEN kq.MaLoaiDiem='KT2' THEN kq.Diem END) AS [KT2],
                        MAX(CASE WHEN kq.MaLoaiDiem='CK'  THEN kq.Diem END) AS [CK]
                    FROM SinhVien sv
                    JOIN LopNienChe lnc ON sv.MaLop = lnc.MaLop
                    JOIN Khoa k ON lnc.MaKhoa = k.MaKhoa
                    JOIN DangKyLopHoc dk ON dk.MaSV = sv.MaSV
                    JOIN LopHocPhan lhp ON lhp.MaLHP = dk.MaLHP
                    JOIN MonHoc mh ON mh.MaMon = lhp.MaMon
                    JOIN HocKyNamHoc hk ON hk.MaHKNH = lhp.MaHKNH
                    LEFT JOIN KetQua kq ON kq.MaLHP = dk.MaLHP AND kq.MaSV = dk.MaSV
                    {where}
                    GROUP BY sv.MaSV, sv.HoTen, lnc.TenLop, k.TenKhoa,
                             mh.TenMon, mh.SoTC, hk.TenHK";

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
                        double tk = Math.Round((cc * 0.1) + (kt1 * 0.15) + (kt2 * 0.15) + (ck * 0.6), 2);
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

                // ── Thống kê thẻ tóm tắt ───────────────────────────────────────
                UpdateStatCards(dtDetail, where);

                // ── Biểu đồ ────────────────────────────────────────────────────
                UpdateCharts(dtDetail);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("LoadBaoCao Error: " + ex.Message);
            }
        }

        // ═══════════════════════════════════════════════════════════════════════
        //  THỐNG KÊ 3 THẺ
        // ═══════════════════════════════════════════════════════════════════════
        private void UpdateStatCards(DataTable dtDetail, string where)
        {
            try
            {
                // Tổng số sinh viên (distinct)
                var distinctSV = new HashSet<string>();
                foreach (DataRow r in dtDetail.Rows)
                    distinctSV.Add(r["Mã SV"].ToString());
                lblTongSV.Text = distinctSV.Count.ToString("N0");

                // GPA trung bình (chỉ tính những dòng có điểm tổng kết)
                double totalGPA = 0; int countGPA = 0;
                foreach (DataRow r in dtDetail.Rows)
                {
                    if (r["Tổng Kết"] != DBNull.Value)
                    {
                        totalGPA += Convert.ToDouble(r["Tổng Kết"]);
                        countGPA++;
                    }
                }
                lblGPA.Text = countGPA > 0
                    ? (totalGPA / countGPA).ToString("F2")
                    : "N/A";

                // Cảnh báo học vụ (đếm từ bảng CanhBao_SinhVien)
                string sqlCB = "SELECT COUNT(*) FROM CanhBao_SinhVien";
                // Áp dụng điều kiện join sinh viên nếu có lọc lớp
                string maLop = guna2ComboBox3.SelectedValue?.ToString() ?? "";
                if (!string.IsNullOrEmpty(maLop))
                    sqlCB = $"SELECT COUNT(*) FROM CanhBao_SinhVien cb " +
                            $"JOIN SinhVien sv ON cb.MaSV = sv.MaSV WHERE sv.MaLop = '{maLop}'";
                string cbCount = FunctionQa.getfieldvalue(sqlCB);
                lblcbhv.Text = string.IsNullOrEmpty(cbCount) ? "0" : cbCount;

                // Đổi màu cảnh báo khi > 0
                lblcbhv.ForeColor = (int.TryParse(cbCount, out int n) && n > 0)
                    ? Color.Firebrick
                    : Color.FromArgb(21, 101, 192);
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
                int countA = 0, countB = 0, countC = 0, countD = 0, countF = 0, countNA = 0;
                foreach (DataRow r in dtDetail.Rows)
                {
                    string xl = r["Xếp Loại"]?.ToString() ?? "";
                    if      (xl.StartsWith("A")) countA++;
                    else if (xl.StartsWith("B")) countB++;
                    else if (xl.StartsWith("C")) countC++;
                    else if (xl.StartsWith("D")) countD++;
                    else if (xl.StartsWith("F")) countF++;
                    else                          countNA++;
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
                // Tính fail rate theo từng môn
                var monStats = new Dictionary<string, (int total, int fail)>();
                foreach (DataRow r in dtDetail.Rows)
                {
                    string tenMon = r["Môn Học"].ToString();
                    string xl     = r["Xếp Loại"]?.ToString() ?? "";
                    if (r["Tổng Kết"] == DBNull.Value) continue; // bỏ qua chưa có điểm

                    if (!monStats.ContainsKey(tenMon)) monStats[tenMon] = (0, 0);
                    var st = monStats[tenMon];
                    st.total++;
                    if (xl.StartsWith("F")) st.fail++;
                    monStats[tenMon] = st;
                }

                // Sắp xếp theo tỷ lệ trượt, lấy Top 10
                var top10 = monStats
                    .Where(kv => kv.Value.total > 0)
                    .OrderByDescending(kv => (double)kv.Value.fail / kv.Value.total)
                    .Take(10)
                    .ToList();

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

                    cartesianChart1.AxisX = new AxesCollection
                    {
                        new Axis
                        {
                            Title  = "Môn học",
                            Labels = labels,
                            LabelsRotation = -30
                        }
                    };
                    cartesianChart1.AxisY = new AxesCollection
                    {
                        new Axis
                        {
                            Title    = "Tỷ lệ (%)",
                            MinValue = 0,
                            MaxValue = 100
                        }
                    };
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
