using QLDSV.BLL;
using QLDSV.DAL;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace QLDSV.GUI
{
    public partial class frmCanhBaoHocVu : Form, IShellChildForm
    {
        private readonly CanhBaoHocVuBLL bll = new CanhBaoHocVuBLL();

        private DataTable dtCanhBao;

        public frmCanhBaoHocVu()
        {
            InitializeComponent();

            ThemeHelper.ApplyTheme(this);
        }
        public void OnEmbeddedInShell()
        {

        }
private void frmCanhBaoHocVu_Load(object sender, EventArgs e)
        {
            try
            {
                Connection.KetNoi();
                ChayPhatHienCanhBaoTuDong();

                LoadComboboxFilters();
                LoadData();
                WireEvents();
                SetupGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi khởi tạo form: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private void ChayPhatHienCanhBaoTuDong()
        {
            try
            {
                bll.PhatHienVaLuuCanhBao();
            }
            catch
            {
                // Chạy ngầm, không block form dù có lỗi
            }
        }

        // =====================================================
        // LOAD DATA
        // =====================================================

        private void LoadData()
        {
            try
            {
                dtCanhBao = bll.GetDanhSachCanhBao();

                DataGridViewCBHV.DataSource = dtCanhBao;

                if (DataGridViewCBHV.Columns.Count > 0)
                {
                    DataGridViewCBHV.Columns["MaCanhBao"].HeaderText = "Mã CB";

                    DataGridViewCBHV.Columns["MaSV"].HeaderText = "Mã SV";

                    DataGridViewCBHV.Columns["HoTen"].HeaderText = "Họ tên";

                    DataGridViewCBHV.Columns["MaLopNienChe"].HeaderText = "Lớp";

                    DataGridViewCBHV.Columns["TenLoaiHK"].HeaderText = "Học kỳ";

                    DataGridViewCBHV.Columns["TenNamHoc"].HeaderText = "Năm học";

                    DataGridViewCBHV.Columns["Noidung"].HeaderText = "Nội dung";

                    DataGridViewCBHV.Columns["LoaiCanhBao"].HeaderText = "Mức";

                    DataGridViewCBHV.Columns["ThoiDiem"].HeaderText = "Thời điểm";

                    DataGridViewCBHV.Columns["LanThu"].HeaderText = "Lần";
                    DataGridViewCBHV.Columns["TenKhoa"].HeaderText = "Khoa";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi tải dữ liệu: " + ex.Message);
            }
        }

        // =====================================================
        // LOAD FILTERS
        // =====================================================

        private void LoadComboboxFilters()
        {
            try
            {
                // Năm học
                DataTable dtNamHoc = bll.GetNamHoc();

                DataRow rowNH = dtNamHoc.NewRow();

                rowNH["MaNamHoc"] = "Tất cả";
                rowNH["TenNamHoc"] = "Tất cả";

                dtNamHoc.Rows.InsertAt(rowNH, 0);

                cboNamHoc.DataSource = dtNamHoc;

                cboNamHoc.DisplayMember = "TenNamHoc";

                cboNamHoc.ValueMember = "MaNamHoc";

                // Học kỳ
                DataTable dtHocKy = bll.GetHocKy();

                DataRow rowHK = dtHocKy.NewRow();

                rowHK["MaLoaiHK"] = "Tất cả";
                rowHK["TenLoaiHK"] = "Tất cả";

                dtHocKy.Rows.InsertAt(rowHK, 0);

                cboHocKy.DataSource = dtHocKy;

                cboHocKy.DisplayMember = "TenLoaiHK";

                cboHocKy.ValueMember = "MaLoaiHK";

                // Lớp
                DataTable dtLop = bll.GetLopNienChe();

                DataRow rowLop = dtLop.NewRow();

                rowLop["MaLopNienChe"] = "Tất cả"; 

                rowLop["TenLop"] = "Tất cả";

                dtLop.Rows.InsertAt(rowLop, 0);

                cboLop.DataSource = dtLop;

                cboLop.DisplayMember = "MaLopNienChe";

                cboLop.ValueMember = "MaLopNienChe";

                // Loại cảnh báo
                DataTable dtLoai = new DataTable();

                dtLoai.Columns.Add("Loai");

                dtLoai.Columns.Add("TenLoai");

                dtLoai.Rows.Add("Tất cả", "Tất cả");
                dtLoai.Rows.Add("2", "Mức 2 - Khối lượng 0 tín chỉ");
                dtLoai.Rows.Add("3", "Mức 3 - TBC dưới 1.5");

                cboLoaiCanhBao.DataSource = dtLoai;

                cboLoaiCanhBao.DisplayMember = "TenLoai";

                cboLoaiCanhBao.ValueMember = "Loai";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // =====================================================
        // FILTER
        // =====================================================

        private void ApplyFilter()
        {
            if (dtCanhBao == null)
                return;

            string filter = "1=1";

            if (cboNamHoc.SelectedValue != null &&
                cboNamHoc.SelectedValue.ToString() != "Tất cả")
            {
                filter +=
                    $" AND TenNamHoc = '{cboNamHoc.Text}'";
            }

            if (cboHocKy.SelectedValue != null &&
                cboHocKy.SelectedValue.ToString() != "Tất cả")
            {
                filter +=
                    $" AND TenLoaiHK = '{cboHocKy.Text}'";
            }

            if (cboLop.SelectedValue != null &&
                cboLop.SelectedValue.ToString() != "Tất cả")
            {
                filter +=
                    $" AND MaLopNienChe = '{cboLop.SelectedValue}'";
            }

            if (cboLoaiCanhBao.SelectedValue != null &&
                cboLoaiCanhBao.SelectedValue.ToString() != "Tất cả")
            {
                filter +=
                    $" AND LoaiCanhBao = {cboLoaiCanhBao.SelectedValue}";
            }

            string kw = txtTimKiem.Text.Trim();

            if (!string.IsNullOrEmpty(kw) &&
                kw != "")
            {
                kw = kw.Replace("'", "''");

                filter +=
                    $" AND (MaSV LIKE '%{kw}%' " +
                    $"OR HoTen LIKE '%{kw}%')";
            }

            dtCanhBao.DefaultView.RowFilter = filter;
        }

        // =====================================================
        // EVENTS
        // =====================================================

        private void WireEvents()
        {
            cboNamHoc.SelectedIndexChanged += cboFilter_SelectedIndexChanged;

            cboHocKy.SelectedIndexChanged += cboFilter_SelectedIndexChanged;

            cboLop.SelectedIndexChanged += cboFilter_SelectedIndexChanged;

            cboLoaiCanhBao.SelectedIndexChanged += cboFilter_SelectedIndexChanged;

            txtTimKiem.TextChanged += txtTimKiem_TextChanged;

            btnLammoi.Click += btnLammoi_Click;
        }

        private void cboFilter_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            ApplyFilter();
        }

        private void txtTimKiem_TextChanged(
            object sender,
            EventArgs e)
        {
            ApplyFilter();
        }

        // =====================================================
        // RESET
        // =====================================================

        private void btnLammoi_Click(
            object sender,
            EventArgs e)
        {
            txtTimKiem.Text =
                "";

            txtTimKiem.ForeColor = Color.Gray;

            cboNamHoc.SelectedIndex = 0;

            cboHocKy.SelectedIndex = 0;

            cboLop.SelectedIndex = 0;

            cboLoaiCanhBao.SelectedIndex = 0;

            LoadData();
        }

        // =====================================================
        // GRID
        // =====================================================

        private void SetupGrid()
        {
            DataGridViewCBHV.AllowUserToAddRows = false;

            DataGridViewCBHV.EditMode =
                DataGridViewEditMode.EditProgrammatically;

            DataGridViewCBHV.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

DataGridViewCBHV.AutoSizeColumnsMode =
    DataGridViewAutoSizeColumnsMode.DisplayedCells;
            DataGridViewCBHV.ScrollBars = ScrollBars.Both;



            DataGridViewCBHV.EnableHeadersVisualStyles = false;

            DataGridViewCBHV.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(100, 88, 255);

            DataGridViewCBHV.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;
        }

        // =====================================================
        // EXPORT EXCEL
        // =====================================================

        private void btnXuat_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                if (DataGridViewCBHV.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất.");
                    return;
                }

                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Excel Workbook|*.xlsx";
                save.FileName = "DanhSachCanhBaoHocVu.xlsx";

                if (save.ShowDialog() != DialogResult.OK)
                    return;

                // Lấy thông tin học kỳ / năm học từ combobox filter
                string tenHK = cboHocKy.Text == "Tất cả" ? "" : cboHocKy.Text;
                string tenNamHoc = cboNamHoc.Text == "Tất cả" ? "" : cboNamHoc.Text;
                string tieuDe = $"Danh sách sinh viên bị cảnh báo học vụ {tenHK}/{tenNamHoc}".Trim('/');

                using (var wb = new ClosedXML.Excel.XLWorkbook())
                {
                    var ws = wb.Worksheets.Add("Danh sách");

                    // ------------------------------------------------
                    // HEADER TRƯỜNG (dòng 1-2)
                    // ------------------------------------------------
                    ws.Cell("A1").Value = "TRƯỜNG ĐẠI HỌC ABC";
                    ws.Cell("A1").Style.Font.Bold = true;
                    ws.Cell("A1").Style.Font.FontSize = 11;

                    ws.Cell("F1").Value = "CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM";
                    ws.Cell("F1").Style.Font.Bold = true;
                    ws.Cell("F1").Style.Font.FontSize = 11;
                    ws.Cell("F1").Style.Alignment.Horizontal =
                        ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                    ws.Range("F1:H1").Merge();

                    ws.Cell("A2").Value = "KHOA / PHÒNG ĐÀO TẠO";
                    ws.Cell("A2").Style.Font.Bold = true;

                    ws.Cell("F2").Value = "Độc lập - Tự do - Hạnh phúc";
                    ws.Cell("F2").Style.Font.Italic = true;
                    ws.Cell("F2").Style.Alignment.Horizontal =
                        ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                    ws.Range("F2:H2").Merge();

                    // ------------------------------------------------
                    // TIÊU ĐỀ CHÍNH (dòng 4)
                    // ------------------------------------------------
                    ws.Cell("A4").Value = tieuDe;
                    ws.Cell("A4").Style.Font.Bold = true;
                    ws.Cell("A4").Style.Font.FontSize = 14;
                    ws.Cell("A4").Style.Alignment.Horizontal =
                        ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                    ws.Range("A4:H4").Merge();

                    // Ghi chú nhỏ (dòng 5)
                    ws.Cell("A5").Value =
                        "";
                    ws.Cell("A5").Style.Font.Italic = true;
                    ws.Cell("A5").Style.Alignment.Horizontal =
                        ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                    ws.Range("A5:H5").Merge();

                    // ------------------------------------------------
                    // HEADER BẢNG (dòng 7)
                    // ------------------------------------------------
                    int headerRow = 7;
                    string[] headers = {
                "STT", "Mã SV", "Họ tên",
                "Khoa", "Mã lớp",
                "Số kỳ bị CB", "Nội dung cảnh báo", "Ghi chú"
            };

                    for (int i = 0; i < headers.Length; i++)
                    {
                        var cell = ws.Cell(headerRow, i + 1);
                        cell.Value = headers[i];
                        cell.Style.Font.Bold = true;
                        cell.Style.Fill.BackgroundColor =
                            ClosedXML.Excel.XLColor.FromArgb(100, 88, 255);
                        cell.Style.Font.FontColor =
                            ClosedXML.Excel.XLColor.White;
                        cell.Style.Alignment.Horizontal =
                            ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                        cell.Style.Alignment.WrapText = true;
                        cell.Style.Border.OutsideBorder =
                            ClosedXML.Excel.XLBorderStyleValues.Thin;
                    }

                    // ------------------------------------------------
                    // DỮ LIỆU
                    // ------------------------------------------------
                    // Map tên cột grid → cột Excel
                    // Grid có: MaCanhBao, MaSV, HoTen, MaLopNienChe,
                    //          TenLoaiHK, TenNamHoc, Noidung, LoaiCanhBao,
                    //          ThoiDiem, LanThu
                    int dataRow = headerRow + 1;
                    int stt = 1;

                    foreach (DataGridViewRow row in DataGridViewCBHV.Rows)
                    {
                        string maSV = row.Cells["MaSV"].Value?.ToString() ?? "";
                        string hoTen = row.Cells["HoTen"].Value?.ToString() ?? "";
                        string maLop = row.Cells["MaLopNienChe"].Value?.ToString() ?? "";
                        string noiDung = row.Cells["Noidung"].Value?.ToString() ?? "";
                        string lanThu = row.Cells["LanThu"].Value?.ToString() ?? "";

                        // Khoa: chưa có trong grid → để trống,
                        // bạn có thể bổ sung cột TenKhoa vào query DAL sau
                        string tenKhoa = row.Cells["TenKhoa"].Value?.ToString() ?? "";

                        ws.Cell(dataRow, 1).Value = stt++;
                        ws.Cell(dataRow, 2).Value = maSV;
                        ws.Cell(dataRow, 3).Value = hoTen;
                        ws.Cell(dataRow, 4).Value = tenKhoa;
                        ws.Cell(dataRow, 5).Value = maLop;
                        ws.Cell(dataRow, 6).Value = lanThu;
                        ws.Cell(dataRow, 7).Value = noiDung;
                        ws.Cell(dataRow, 8).Value = "";  // Ghi chú

                        // Viền từng dòng
                        var rowRange = ws.Range(dataRow, 1, dataRow, 8);
                        rowRange.Style.Border.OutsideBorder =
                            ClosedXML.Excel.XLBorderStyleValues.Thin;
                        rowRange.Style.Border.InsideBorder =
                            ClosedXML.Excel.XLBorderStyleValues.Thin;

                        // Nền xen kẽ cho dễ đọc
                        if (stt % 2 == 0)
                            rowRange.Style.Fill.BackgroundColor =
                                ClosedXML.Excel.XLColor.FromArgb(242, 242, 242);

                        dataRow++;
                    }

                    // ------------------------------------------------
                    // DÒNG TỔNG KẾT
                    // ------------------------------------------------
                    int totalRow = dataRow + 1;
                    ws.Cell(totalRow, 1).Value =
                        $"Danh sách gồm {DataGridViewCBHV.Rows.Count} sinh viên";
                    ws.Cell(totalRow, 1).Style.Font.Bold = true;
                    ws.Cell(totalRow, 1).Style.Font.Italic = true;
                    ws.Range(totalRow, 1, totalRow, 4).Merge();

                    // Ngày ký
                    ws.Cell(totalRow, 6).Value =
                        $"Hà Nội, Ngày {DateTime.Now:dd} tháng {DateTime.Now:MM} năm {DateTime.Now:yyyy}";
                    ws.Cell(totalRow, 6).Style.Alignment.Horizontal =
                        ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                    ws.Range(totalRow, 6, totalRow, 8).Merge();

                    ws.Cell(totalRow + 1, 6).Value = "TL. HIỆU TRƯỞNG";
                    ws.Cell(totalRow + 1, 6).Style.Font.Bold = true;
                    ws.Cell(totalRow + 1, 6).Style.Alignment.Horizontal =
                        ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                    ws.Range(totalRow + 1, 6, totalRow + 1, 8).Merge();

                    ws.Cell(totalRow + 2, 6).Value = "TRƯỞNG PHÒNG ĐÀO TẠO";
                    ws.Cell(totalRow + 2, 6).Style.Font.Bold = true;
                    ws.Cell(totalRow + 2, 6).Style.Alignment.Horizontal =
                        ClosedXML.Excel.XLAlignmentHorizontalValues.Center;
                    ws.Range(totalRow + 2, 6, totalRow + 2, 8).Merge();

                    // ------------------------------------------------
                    // ĐỘ RỘNG CỘT
                    // ------------------------------------------------
                    ws.Column(1).Width = 6;   // STT
                    ws.Column(2).Width = 12;  // Mã SV
                    ws.Column(3).Width = 25;  // Họ tên
                    ws.Column(4).Width = 20;  // Khoa
                    ws.Column(5).Width = 12;  // Mã lớp
                    ws.Column(6).Width = 12;  // Số kỳ CB
                    ws.Column(7).Width = 35;  // Nội dung
                    ws.Column(8).Width = 15;  // Ghi chú

                    // In đậm viền bảng ngoài
                    ws.Range(headerRow, 1, dataRow - 1, 8)
                      .Style.Border.OutsideBorder =
                        ClosedXML.Excel.XLBorderStyleValues.Medium;

                    wb.SaveAs(save.FileName);
                }

                MessageBox.Show(
                    "Xuất Excel thành công!\n" + save.FileName,
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Mở file luôn sau khi xuất
                System.Diagnostics.Process.Start(save.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi xuất Excel: " + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
