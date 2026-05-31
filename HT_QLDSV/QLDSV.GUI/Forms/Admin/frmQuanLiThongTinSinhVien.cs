using QLDSV.DAL;
using ClosedXML.Excel;
using QLDSV.BLL;
using QLDSV.GUI.Forms.Admin;
using System;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Collections.Generic;

namespace QLDSV.GUI
{
    public partial class frmQuanLiThongTinSinhVien : Form
    {
        // =====================================================================
        //  CẤU HÌNH & TRẠNG THÁI
        // =====================================================================
        private readonly SinhVienBLL bll = new SinhVienBLL();

        // Lưu toàn bộ dữ liệu từ database để phục vụ phân trang và lọc nội bộ
        private DataTable dtFull = new DataTable();

        // Trạng thái form: "" (chế độ xem) | "THEM" | "SUA"
        private string trangThai = "";

        // Phân trang
        private int trangHienTai = 1;
        private const int SO_DONG_MOI_TRANG = 20;
        private int tongSoTrang = 1;

        // =====================================================================
        //  KHỞI TẠO
        // =====================================================================
        public frmQuanLiThongTinSinhVien()
        {
            InitializeComponent();
            // Đảm bảo sự kiện Load Form được liên kết chính xác
            this.Load += new EventHandler(this.frmQuanLiThongTinSinhVien_Load);
        }

        private void frmQuanLiThongTinSinhVien_Load(object sender, EventArgs e)
        {
            CauHinhDgv();
            LoadCboKhoa();
            LoadCboNienKhoa();
            LoadCboTimKhoa();
            LoadDanhSach();
            SetFormState(false);
            XoaForm();

            // Gán sự kiện cho các control
            cboTimKhoa.SelectedIndexChanged += cboTimKhoa_SelectedIndexChanged;
            cboTimLop.SelectedIndexChanged += cboTimLop_SelectedIndexChanged;
            cboTimNienKhoa.SelectedIndexChanged += cboTimNienKhoa_SelectedIndexChanged;
            cboKhoa.SelectedIndexChanged += cboKhoa_SelectedIndexChanged;
            btnXuatExcel.Click += btnXuatExcel_Click;
            guna2Button3.Click += guna2Button3_Click;
            btnSua.Click += btnSua_Click;
            btnLuu.Click += btnLuu_Click;

            // Gán sự kiện cho các nút điều hướng sidebar
            WireSidebarEvents();
        }

        // =====================================================================
        //  CẤU HÌNH DATAGRIDVIEW
        // =====================================================================
        private void CauHinhDgv()
        {
            dgvSinhVien.ReadOnly = true;
            dgvSinhVien.AllowUserToAddRows = false;
            dgvSinhVien.AllowUserToDeleteRows = false;
            dgvSinhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSinhVien.MultiSelect = false;
            dgvSinhVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSinhVien.RowHeadersVisible = false;
            dgvSinhVien.BackgroundColor = System.Drawing.Color.White;
            dgvSinhVien.BorderStyle = BorderStyle.None;
            dgvSinhVien.ColumnHeadersDefaultCellStyle.BackColor =
                System.Drawing.Color.FromArgb(21, 101, 192);
            dgvSinhVien.ColumnHeadersDefaultCellStyle.ForeColor =
                System.Drawing.Color.White;
            dgvSinhVien.ColumnHeadersDefaultCellStyle.Font =
                new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            dgvSinhVien.EnableHeadersVisualStyles = false;
            dgvSinhVien.AlternatingRowsDefaultCellStyle.BackColor =
                System.Drawing.Color.FromArgb(240, 245, 255);
            dgvSinhVien.CellClick += DgvSinhVien_CellClick;
            dgvSinhVien.DoubleClick += DgvSinhVien_DoubleClick;
        }

        // =====================================================================
        //  LOAD DATA COMBOBOXES (Standard WinForms Data Binding Order)
        // =====================================================================
        private void LoadCboKhoa()
        {
            try
            {
                var dt = bll.GetKhoa();

                var rowEmpty = dt.NewRow();
                rowEmpty["MaKhoa"] = "";
                rowEmpty["TenKhoa"] = "-- Chọn khoa --";
                dt.Rows.InsertAt(rowEmpty, 0);

                cboKhoa.DataSource = dt;
                cboKhoa.DisplayMember = "TenKhoa";
                cboKhoa.ValueMember = "MaKhoa";
                cboKhoa.SelectedIndex = 0;
            }
            catch (Exception ex) { ShowError("Lỗi tải danh sách khoa", ex); }
        }

        private void LoadCboLopNienChe(string maKhoa = "")
        {
            try
            {
                var dt = bll.GetLopNienChe(maKhoa);

                if (dt == null || dt.Columns.Count == 0 || dt.Rows.Count == 0)
                {
                    dt = new DataTable();
                    dt.Columns.Add("MaLop", typeof(string));
                    dt.Columns.Add("TenLop", typeof(string));
                }

                var r = dt.NewRow();
                r["MaLop"] = ""; r["TenLop"] = "-- Chọn lớp --";
                dt.Rows.InsertAt(r, 0);

                cboLopNienChe.DataSource = dt;
                cboLopNienChe.DisplayMember = "TenLop";
                cboLopNienChe.ValueMember = "MaLop";
                cboLopNienChe.SelectedIndex = 0;
            }
            catch { }
        }

        private void LoadCboLopNienCheTheoKhoa(string maKhoa)
        {
            try
            {
                var dt = bll.LoadLopNienCheTheoKhoa(maKhoa);

                if (dt == null || dt.Columns.Count == 0)
                {
                    dt = new DataTable();
                    dt.Columns.Add("MaLop", typeof(string));
                    dt.Columns.Add("TenLop", typeof(string));
                }

                var r = dt.NewRow();
                r["MaLop"] = ""; r["TenLop"] = "-- Tất cả lớp --";
                dt.Rows.InsertAt(r, 0);

                cboTimLop.DataSource = dt;
                cboTimLop.DisplayMember = "TenLop";
                cboTimLop.ValueMember = "MaLop";
            }
            catch
            {
                var dtLop = new DataTable();
                dtLop.Columns.Add("MaLop", typeof(string));
                dtLop.Columns.Add("TenLop", typeof(string));
                dtLop.Rows.Add("", "-- Tất cả lớp --");
                cboTimLop.DataSource = dtLop;
                cboTimLop.DisplayMember = "TenLop";
                cboTimLop.ValueMember = "MaLop";
            }
        }

        private void LoadCboNienKhoa()
        {
            guna2ComboBox5.Items.Clear();
            guna2ComboBox5.Items.Add("-- Chọn niên khóa --");
            int y = DateTime.Now.Year;
            for (int i = -3; i <= 3; i++)
                guna2ComboBox5.Items.Add($"{y + i}-{y + i + 1}");
            guna2ComboBox5.SelectedIndex = 0;
        }

        private void LoadCboTimKhoa()
        {
            try
            {
                var dt = bll.GetKhoa();

                var r = dt.NewRow();
                r["MaKhoa"] = ""; r["TenKhoa"] = "-- Tất cả khoa --";
                dt.Rows.InsertAt(r, 0);

                cboTimKhoa.DataSource = dt;
                cboTimKhoa.DisplayMember = "TenKhoa";
                cboTimKhoa.ValueMember = "MaKhoa";
                cboTimKhoa.SelectedIndex = 0;
            }
            catch { }

            // cboTimLop - load lớp niên chế rỗng ban đầu
            var dtLop = new DataTable();
            dtLop.Columns.Add("MaLop", typeof(string));
            dtLop.Columns.Add("TenLop", typeof(string));
            dtLop.Rows.Add("", "-- Tất cả lớp --");
            cboTimLop.DataSource = dtLop;
            cboTimLop.DisplayMember = "TenLop";
            cboTimLop.ValueMember = "MaLop";

            // guna2ComboBox3
            guna2ComboBox3.Items.Clear();
            guna2ComboBox3.Items.Add("-- Tất cả niên khóa --");
            int y = DateTime.Now.Year;
            for (int i = -3; i <= 3; i++)
                guna2ComboBox3.Items.Add($"{y + i}-{y + i + 1}");
            guna2ComboBox3.SelectedIndex = 0;
        }

        // =====================================================================
        //  TRUY VẤN & LỌC HỌC VIÊN
        // =====================================================================
        private void LoadDanhSach()
        {
            try
            {
                dtFull = bll.GetSinhVien();
                LocVaHienThi();
            }
            catch (Exception ex) { ShowError("Lỗi tải danh sách sinh viên", ex); }
        }

        private void LocVaHienThi()
        {
            if (dtFull == null) return;

            string tuKhoa = txtTimKiem.Text.Trim().ToLower();
            string maKhoa = cboTimKhoa.SelectedValue?.ToString() ?? "";
            string maLop = cboTimLop.SelectedValue?.ToString() ?? "";
            string nienKhoa = cboTimNienKhoa.SelectedIndex > 0
                              ? cboTimNienKhoa.SelectedItem?.ToString() ?? "" : "";

            var dtFilter = dtFull.Clone();
            foreach (DataRow row in dtFull.Rows)
            {
                bool ok = true;
                if (!string.IsNullOrEmpty(tuKhoa))
                {
                    ok &= row["MaSinhVien"].ToString().ToLower().Contains(tuKhoa)
                       || row["HoVaTen"].ToString().ToLower().Contains(tuKhoa);
                }
                if (!string.IsNullOrEmpty(maKhoa))
                    ok &= row["MaKhoa"].ToString() == maKhoa;
                if (!string.IsNullOrEmpty(maLop))
                    ok &= row["MaLopNienChe"].ToString() == maLop;
                if (!string.IsNullOrEmpty(nienKhoa))
                    ok &= row["NienKhoa"].ToString() == nienKhoa;
                
                if (ok) dtFilter.ImportRow(row);
            }

            tongSoTrang = Math.Max(1, (int)Math.Ceiling((double)dtFilter.Rows.Count / SO_DONG_MOI_TRANG));
            if (trangHienTai > tongSoTrang) trangHienTai = tongSoTrang;

            // Chia trang
            int start = (trangHienTai - 1) * SO_DONG_MOI_TRANG;
            var dtPage = dtFilter.Clone();
            for (int i = start; i < Math.Min(start + SO_DONG_MOI_TRANG, dtFilter.Rows.Count); i++)
                dtPage.ImportRow(dtFilter.Rows[i]);

            dataGridView1.DataSource = dtPage;
            DatTenCotDgv();

            guna2HtmlLabel7.Text = $"{trangHienTai}";
            guna2Button9.Enabled = trangHienTai > 1;
            guna2Button11.Enabled = trangHienTai < tongSoTrang;
        }

        private void DatTenCotDgv()
        {
            var map = new Dictionary<string, string>
            {
                {"MaSinhVien","Mã SV"},{"HoVaTen","Họ và tên"},{"NgaySinh","Ngày sinh"},
                {"GioiTinh","Giới tính"},{"DiaChi","Địa chỉ"},{"Email","Email"},
                {"SoDienThoai","SĐT"},{"TenKhoa","Khoa"},{"LopNienChe","Lớp niên chế"},
                {"NienKhoa","Niên khóa"}
            };
            string[] hidden = { "MaKhoa", "MaLopNienChe" };

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (Array.IndexOf(hidden, col.Name) >= 0)
                    col.Visible = false;
                else if (map.ContainsKey(col.Name))
                    col.HeaderText = map[col.Name];
            }
        }

        // =====================================================================
        //  SỰ KIỆN TƯƠNG TÁC BỘ LỌC
        // =====================================================================
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            trangHienTai = 1;
            LocVaHienThi();
        }

        private void cboTimKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Load lớp niên chế tương ứng vào cboTimLop
            string maKhoa = cboTimKhoa.SelectedValue?.ToString() ?? "";
            LoadCboLopNienCheTheoKhoa(maKhoa);
            trangHienTai = 1;
            LocVaHienThi();
        }

        private void cboTimLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            trangHienTai = 1;
            LocVaHienThi();
        }

        private void cboTimNienKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            trangHienTai = 1;
            LocVaHienThi();
        }

        private void cboKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(trangThai))
            {
                string mk = cboKhoa.SelectedValue?.ToString() ?? "";
                if (mk == "System.Data.DataRowView") mk = "";
                LoadCboLopNienChe(mk);
            }
        }

        // =====================================================================
        //  CHỌN DÒNG TRÊN GRIDVIEW
        // =====================================================================
        private void DgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            HienThiDong(dataGridView1.Rows[e.RowIndex]);
        }

        private void DgvSinhVien_DoubleClick(object sender, EventArgs e)
        {
            if (dgvSinhVien.CurrentRow == null) return;
            HienThiDong(dgvSinhVien.CurrentRow);
            // Mở luôn chế độ sửa khi double-click
            trangThai = "SUA";
            SetFormState(true);
            guna2TextBox4.ReadOnly = true;
            guna2TextBox3.Focus();
        }

        private void HienThiDong(DataGridViewRow row)
        {
            txtMaSV.Text = row.Cells["MaSinhVien"]?.Value?.ToString() ?? "";
            txtHoTen.Text = row.Cells["HoVaTen"]?.Value?.ToString() ?? "";
            txtNgaySinh.Text = row.Cells["NgaySinh"]?.Value?.ToString() ?? "";
            txtDiaChi.Text = row.Cells["DiaChi"]?.Value?.ToString() ?? "";
            txtEmail.Text = row.Cells["Email"]?.Value?.ToString() ?? "";
            txtSoDT.Text = row.Cells["SoDienThoai"]?.Value?.ToString() ?? "";

            string gt = row.Cells["GioiTinh"]?.Value?.ToString() ?? "";
            radioButton2.Checked = gt != "Nữ"; // radioButton2 = Nam
            rdoNu.Checked = gt == "Nữ";

            // Combo khoa và lớp niên chế
            string maKhoa = row.Cells["MaKhoa"]?.Value?.ToString() ?? "";
            SetComboValue(cboKhoa, maKhoa);
            LoadCboLopNienChe(maKhoa);

            // Đợi combobox load xong rồi mới set giá trị
            string maLopNC = row.Cells["MaLopNienChe"]?.Value?.ToString() ?? "";
            if (cboLopNienChe.DataSource != null)
                SetComboValue(cboLopNienChe, maLopNC);

            string nienKhoa = row.Cells["NienKhoa"]?.Value?.ToString() ?? "";
            if (cboNienKhoa.Items.Contains(nienKhoa))
                cboNienKhoa.SelectedItem = nienKhoa;
            else
                guna2ComboBox5.SelectedIndex = 0;
        }

        private void SetComboValue(ComboBox cbo, string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                {
                    cbo.SelectedIndex = 0;
                    return;
                }
                
                string target = value.Trim().ToLower();
                
                // Thử gán SelectedValue trực tiếp trước
                cbo.SelectedValue = value;
                if (cbo.SelectedValue != null && cbo.SelectedValue.ToString().Trim().ToLower() == target)
                    return;

                cbo.SelectedValue = target;
                if (cbo.SelectedValue != null && cbo.SelectedValue.ToString().Trim().ToLower() == target)
                    return;

                // Nếu gán SelectedValue trực tiếp bị null/thất bại, tiến hành duyệt qua nguồn dữ liệu (DataTable/Items)
                if (cbo.DataSource is DataTable dt)
                {
                    string valMember = cbo.ValueMember;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string val = dt.Rows[i][valMember]?.ToString()?.Trim()?.ToLower() ?? "";
                        if (val == target)
                        {
                            cbo.SelectedIndex = i;
                            return;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < cbo.Items.Count; i++)
                    {
                        string val = "";
                        if (cbo.Items[i] is DataRowView drv)
                        {
                            val = drv[cbo.ValueMember]?.ToString()?.Trim()?.ToLower() ?? "";
                        }
                        else
                        {
                            val = cbo.Items[i]?.ToString()?.Trim()?.ToLower() ?? "";
                        }
                        if (val == target)
                        {
                            cbo.SelectedIndex = i;
                            return;
                        }
                    }
                }

                // Dự phòng cuối cùng
                cbo.SelectedIndex = 0;
            }
            catch { cbo.SelectedIndex = 0; }
        }

        // =====================================================================
        //  NGHIỆP VỤ: THÊM / SỬA / XÓA / LƯU / HỦY
        // =====================================================================
        private void btnThem_Click(object sender, EventArgs e)
        {
            XoaForm();
            trangThai = "THEM";
            guna2TextBox4.Text = SinhMaSV();
            guna2TextBox4.ReadOnly = false;
            SetFormState(true);
            guna2TextBox3.Focus();
        }

        private string SinhMaSV()
        {
            try
            {
                return bll.TaoMaSVMoi();
            }
            catch { return "SV" + DateTime.Now.ToString("MMddHHmm"); }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox4.Text))
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần sửa!", "Chưa chọn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            trangThai = "SUA";
            SetFormState(true);
            guna2TextBox4.ReadOnly = true;
            guna2TextBox3.Focus();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!Validate_()) return;
            try
            {
                DateTime? ngaySinh = null;
                if (DateTime.TryParseExact(txtNgaySinh.Text.Trim(), "dd/MM/yyyy",
                    null, System.Globalization.DateTimeStyles.None, out DateTime ns))
                    ngaySinh = ns;

                bool gioiTinh = radioButton2.Checked;

                if (trangThai == "THEM")
                {
                    var result = bll.ThemSinhVien(
                        guna2TextBox4.Text.Trim(),
                        guna2TextBox3.Text.Trim(),
                        ngaySinh,
                        gioiTinh,
                        guna2TextBox6.Text.Trim(),
                        guna2TextBox9.Text.Trim(),
                        guna2TextBox8.Text.Trim(),
                        guna2ComboBox6.SelectedValue?.ToString() ?? "",
                        guna2ComboBox5.SelectedIndex > 0 ? guna2ComboBox5.SelectedItem?.ToString() ?? "" : ""
                    );
                    if (!result.success)
                        throw new Exception(result.message);
                }
                else if (trangThai == "SUA")
                {
                    var result = bll.SuaSinhVien(
                        guna2TextBox4.Text.Trim(),
                        guna2TextBox3.Text.Trim(),
                        ngaySinh,
                        gioiTinh,
                        guna2TextBox6.Text.Trim(),
                        guna2TextBox9.Text.Trim(),
                        guna2TextBox8.Text.Trim(),
                        guna2ComboBox6.SelectedValue?.ToString() ?? "",
                        guna2ComboBox5.SelectedIndex > 0 ? guna2ComboBox5.SelectedItem?.ToString() ?? "" : ""
                    );
                    if (!result.success)
                        throw new Exception(result.message);
                }

                MessageBox.Show("Lưu dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSach();
                XoaForm();
                SetFormState(false);
                trangThai = "";
            }
            catch (Exception ex) { ShowError("Lỗi lưu dữ liệu", ex); }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(guna2TextBox4.Text))
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa!", "Chưa chọn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show(
                $"Xóa sinh viên \"{txtHoTen.Text}\" ({txtMaSV.Text})?\nThao tác không thể hoàn tác!",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                var result = bll.XoaSinhVien(guna2TextBox4.Text);
                if (!result.success)
                {
                    MessageBox.Show(result.message, "Ràng buộc dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MessageBox.Show("Xóa dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDanhSach();
                XoaForm();
                SetFormState(false);
                trangThai = "";
            }
            catch (Exception ex) { ShowError("Lỗi xóa dữ liệu", ex); }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            XoaForm();
            SetFormState(false);
            trangThai = "";
        }

        // =====================================================================
        //  XUẤT / NHẬP EXCEL
        // =====================================================================
        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dtFull == null || dtFull.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var sfd = new SaveFileDialog
            {
                Filter = "Excel 2007+ (*.xlsx)|*.xlsx",
                FileName = $"SinhVien_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
            })
            {
                if (sfd.ShowDialog() != DialogResult.OK) return;
                try
                {
                    using (var wb = new XLWorkbook())
                    {
                        var ws = wb.Worksheets.Add("Danh sách sinh viên");

                        ws.Cell(1, 1).Value = "DANH SÁCH SINH VIÊN";
                        ws.Range(1, 1, 1, 11).Merge().Style.Font.SetBold(true).Font.SetFontSize(14).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        ws.Cell(2, 1).Value = $"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm}";
                        ws.Range(2, 1, 2, 11).Merge();

                        string[] hdrs = { "STT","Mã SV","Họ và tên","Ngày sinh","Giới tính","Địa chỉ","Email","SĐT","Khoa","Lớp niên chế","Niên khóa" };
                        for (int c = 0; c < hdrs.Length; c++)
                        {
                            var cell = ws.Cell(4, c + 1);
                            cell.Value = hdrs[c];
                            cell.Style.Font.Bold = true;
                            cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#1565C0");
                            cell.Style.Font.FontColor = XLColor.White;
                            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        }

                        int stt = 1;
                        foreach (DataRow dr in dtFull.Rows)
                        {
                            int row = stt + 4;
                            ws.Cell(row, 1).Value = stt;
                            ws.Cell(row, 2).Value = dr["MaSinhVien"].ToString();
                            ws.Cell(row, 3).Value = dr["HoVaTen"].ToString();
                            ws.Cell(row, 4).Value = dr["NgaySinh"].ToString();
                            ws.Cell(row, 5).Value = dr["GioiTinh"].ToString();
                            ws.Cell(row, 6).Value = dr["DiaChi"].ToString();
                            ws.Cell(row, 7).Value = dr["Email"].ToString();
                            ws.Cell(row, 8).Value = dr["SoDienThoai"].ToString();
                            ws.Cell(row, 9).Value = dr["TenKhoa"].ToString();
                            ws.Cell(row, 10).Value = dr["LopNienChe"].ToString();
                            ws.Cell(row, 11).Value = dr["NienKhoa"].ToString();

                            if (stt % 2 == 0)
                                ws.Range(row, 1, row, 11).Style.Fill.BackgroundColor = XLColor.FromHtml("#E3F2FD");

                            for (int c = 1; c <= 11; c++)
                                ws.Cell(row, c).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            stt++;
                        }

                        ws.Columns().AdjustToContents();
                        wb.SaveAs(sfd.FileName);
                    }

                    if (MessageBox.Show("Xuất file Excel thành công!\nBạn có muốn mở file ngay không?", "Thành công",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        System.Diagnostics.Process.Start(sfd.FileName);
                }
                catch (Exception ex) { ShowError("Lỗi xuất Excel", ex); }
            }
        }

        private void btnNhapExcel_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog
            {
                Filter = "Excel Files (*.xlsx;*.xls)|*.xlsx;*.xls",
                Title = "Chọn file Excel để nhập dữ liệu"
            })
            {
                if (ofd.ShowDialog() != DialogResult.OK) return;
                try
                {
                    using (var wb = new XLWorkbook(ofd.FileName))
                    {
                        var ws = wb.Worksheets.First();
                        int lastRow = ws.LastRowUsed().RowNumber();
                        int them = 0, capNhat = 0, loi = 0;

                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.Connection = Connection.conn;
                            for (int r = 5; r <= lastRow; r++)
                            {
                                try
                                {
                                    string maSV = ws.Cell(r, 2).GetString().Trim();
                                    if (string.IsNullOrEmpty(maSV)) continue;

                                    string hoTen = ws.Cell(r, 3).GetString().Trim();
                                    string ngaySinhStr = ws.Cell(r, 4).GetString().Trim();
                                    string gt = ws.Cell(r, 5).GetString().Trim();
                                    string diaChi = ws.Cell(r, 6).GetString().Trim();
                                    string email = ws.Cell(r, 7).GetString().Trim();
                                    string sdt = ws.Cell(r, 8).GetString().Trim();
                                    string maLopNC = ws.Cell(r, 10).GetString().Trim();
                                    string nienKhoa = ws.Cell(r, 11).GetString().Trim();

                                    object ngaySinhVal = DBNull.Value;
                                    if (DateTime.TryParse(ngaySinhStr, out DateTime ns))
                                        ngaySinhVal = ns;

                                    var chk = new SqlCommand("SELECT COUNT(*) FROM SinhVien WHERE MaSV=@ms", cmd.Connection);
                                    chk.Parameters.AddWithValue("@ms", maSV);
                                    bool exists = (int)chk.ExecuteScalar() > 0;

                                    SqlCommand upsert;
                                    if (exists)
                                    {
                                        upsert = new SqlCommand(@"
                                            UPDATE SinhVien SET HoTen=@ht,NgaySinh=@ns,
                                            GioiTinh=@gt,DiaChi=@dc,Email=@em,SoDT=@sdt,
                                            MaLopNienChe=@mlnc,NienKhoa=@nk
                                            WHERE MaSV=@ms", cmd.Connection);
                                        capNhat++;
                                    }
                                    else
                                    {
                                        upsert = new SqlCommand(@"
                                            INSERT INTO SinhVien (MaSV,HoTen,NgaySinh,GioiTinh,DiaChi,Email,SoDT,MaLopNienChe,NienKhoa,MaTaiKhoan)
                                            VALUES(@ms,@ht,@ns,@gt,@dc,@em,@sdt,@mlnc,@nk,NULL)", cmd.Connection);
                                        them++;
                                    }
                                    upsert.Parameters.AddWithValue("@ms", maSV);
                                    upsert.Parameters.AddWithValue("@ht", hoTen);
                                    upsert.Parameters.AddWithValue("@ns", ngaySinhVal);
                                    upsert.Parameters.AddWithValue("@gt", gt);
                                    upsert.Parameters.AddWithValue("@dc", diaChi);
                                    upsert.Parameters.AddWithValue("@em", email);
                                    upsert.Parameters.AddWithValue("@sdt", sdt);
                                    upsert.Parameters.AddWithValue("@mlnc", maLopNC);
                                    upsert.Parameters.AddWithValue("@nk", nienKhoa);
                                    upsert.ExecuteNonQuery();
                                }
                                catch { loi++; }
                            }
                        }

                        MessageBox.Show($"Nhập Excel hoàn tất!\n✅ Thêm mới: {them}\n🔄 Cập nhật: {capNhat}\n❌ Lỗi: {loi}",
                            "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDanhSach();
                    }
                }
                catch (Exception ex) { ShowError("Lỗi nhập Excel", ex); }
            }
        }

        // =====================================================================
        //  PHÂN TRANG
        // =====================================================================
        private void btnTrangTruoc_Click(object sender, EventArgs e)
        {
            if (trangHienTai > 1) { trangHienTai--; LocVaHienThi(); }
        }

        private void btnTrangSau_Click(object sender, EventArgs e)
        {
            if (trangHienTai < tongSoTrang) { trangHienTai++; LocVaHienThi(); }
        }

        // =====================================================================
        //  HELPERS & VALIDATE
        // =====================================================================
        private void cboKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(trangThai)) // chỉ load khi đang nhập
            {
                string mk = cboKhoa.SelectedValue?.ToString() ?? "";
                if (mk == "System.Data.DataRowView") mk = "";
                LoadCboLopNienChe(mk);
            }
        }

        // =====================================================================
        //  KIỂM TRA DỮ LIỆU
        // =====================================================================
        private bool Validate_()
        {
            if (string.IsNullOrWhiteSpace(txtMaSV.Text))
            { Warn("Vui lòng nhập Mã sinh viên!", txtMaSV); return false; }

            if (string.IsNullOrWhiteSpace(guna2TextBox3.Text))
            { Warn("Vui lòng nhập Họ và tên!", guna2TextBox3); return false; }

            if (!string.IsNullOrEmpty(guna2TextBox5.Text))
            {
                if (!DateTime.TryParseExact(txtNgaySinh.Text.Trim(), "dd/MM/yyyy",
                    null, System.Globalization.DateTimeStyles.None, out _))
                { Warn("Ngày sinh không đúng định dạng dd/MM/yyyy!", txtNgaySinh); return false; }
            }

            if (!string.IsNullOrEmpty(guna2TextBox9.Text))
            {
                try { new System.Net.Mail.MailAddress(guna2TextBox9.Text); }
                catch { Warn("Email không đúng định dạng!", guna2TextBox9); return false; }
            }

            if (!string.IsNullOrEmpty(txtSoDT.Text) &&
                !System.Text.RegularExpressions.Regex.IsMatch(txtSoDT.Text.Trim(), @"^[0-9]{10,11}$"))
            { Warn("Số điện thoại phải gồm 10-11 chữ số!", txtSoDT); return false; }

            return true;
        }

        private void Warn(string msg, Control focus = null)
        {
            MessageBox.Show(msg, "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            focus?.Focus();
        }

        // =====================================================================
        //  HELPERS
        // =====================================================================
        private void XoaForm()
        {
            txtMaSV.Text = "";
            txtHoTen.Text = "";
            txtNgaySinh.Text = "";
            txtDiaChi.Text = "";
            txtEmail.Text = "";
            txtSoDT.Text = "";
            radioButton2.Checked = true; // Nam
            rdoNu.Checked = false;
            txtMaSV.ReadOnly = false;

            // Reset combo
            if (cboKhoa.Items.Count > 0) cboKhoa.SelectedIndex = 0;
            LoadCboLopNienChe();
            if (cboNienKhoa.Items.Count > 0) cboNienKhoa.SelectedIndex = 0;
        }

        private void SetFormState(bool editing)
        {
            // Các nút chức năng
            guna2Button1.Enabled = !editing; // Thêm sinh viên
            btnSua.Enabled = !editing;
            btnXoa.Enabled = !editing;
            btnLuu.Enabled = editing;
            btnHuy.Enabled = editing;

            // Controls nhập liệu
            Control[] inputs =
            {
                txtHoTen, txtNgaySinh, txtDiaChi, txtEmail, txtSoDT,
                radioButton2, rdoNu, cboKhoa, cboLopNienChe, cboNienKhoa
            };
            foreach (var c in inputs) c.Enabled = editing;
            txtMaSV.Enabled = editing; // maSV bị lock riêng khi SUA
        }

        private void ShowError(string msg, Exception ex)
        {
            MessageBox.Show($"{msg}:\n{ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape && trangThai != "")
            { btnHuy_Click(null, null); return true; }

            if (keyData == (Keys.Control | Keys.S) && trangThai != "")
            { btnLuu_Click(null, null); return true; }

            if (keyData == (Keys.Control | Keys.N))
            { btnThem_Click(null, null); return true; }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        // =====================================================================
        //  ĐIỀU HƯỚNG SIDEBAR
        // =====================================================================
        private void OpenForm(Form targetForm)
        {
            this.Hide();
            targetForm.ShowDialog();
            this.Close();
        }

        private void WireSidebarEvents()
        {
            btnSinhvien.Click += (s, e) => { };

            guna2Button7.Click += (s, e) =>
            {
                this.Hide();
                new frmDangNhap().ShowDialog();
                this.Close();
            };

            btnTongquan.Click += (s, e) => OpenForm(new frmTongQuan());
            btnMon.Click += (s, e) => OpenForm(new frmMonhoc());
            btnLopnc.Click += (s, e) => OpenForm(new FrmLopNienChe());
            btnLophp.Click += (s, e) => OpenForm(new frmLophocphan());
            btnDangky.Click += (s, e) => MessageBox.Show("Tính năng Đăng ký lớp đang được phát triển!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnDiem.Click += (s, e) => MessageBox.Show("Tính năng Nhập điểm đang được phát triển!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnKetqua.Click += (s, e) => OpenForm(new frmTheoDoiDiem());
            btnCanhbao.Click += (s, e) => OpenForm(new frmCanhBaoHocVu());
            btnPhuckhao.Click += (s, e) =>
            {
                if (SessionHelper.MaVaiTro == "VT001")
                    OpenForm(new frmPhucKhao_Admin());
                else if (SessionHelper.MaVaiTro == "VT002")
                    OpenForm(new QLDSV.GUI.Forms.GiangVien.frmPhucKhao_GV());
                else if (SessionHelper.MaVaiTro == "VT003")
                    OpenForm(new QLDSV.GUI.Forms.SinhVien.frmPhucKhao_SV());
            };
            btnBaocao.Click += (s, e) => OpenForm(new frmBaoCaoThongKe());
            btnGiangvien.Click += (s, e) => OpenForm(new frmGiangvien());
        }

        // =====================================================================
        //  EMPTY EVENT HANDLERS REQUIRED BY DESIGNER
        // =====================================================================
        private void label3_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void guna2ImageButton1_Click(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e) { }
        private void guna2Button11_Click(object sender, EventArgs e) { }
        private void guna2HtmlLabel7_Click(object sender, EventArgs e) { }
        private void guna2Button9_Click(object sender, EventArgs e) { }
        private void guna2Button1_Click(object sender, EventArgs e) { }
        private void guna2ComboBox4_SelectedIndexChanged(object sender, EventArgs e) { }
        private void guna2TextBox4_TextChanged(object sender, EventArgs e) { }
        private void guna2ComboBox6_SelectedIndexChanged(object sender, EventArgs e) { }
        private void guna2Button6_Click(object sender, EventArgs e) { }
        private void pnlHeader_Paint(object sender, PaintEventArgs e) { }
        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e) { }
        private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e) { }
        private void guna2GroupBox2_Click(object sender, EventArgs e) { }
        private void guna2PictureBox3_Click(object sender, EventArgs e) { }

        private void txtMaSV_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
