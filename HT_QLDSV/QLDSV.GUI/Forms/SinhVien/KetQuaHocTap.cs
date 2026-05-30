using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using ClosedXML.Excel;
using QLDSV.BLL;

namespace QLDSV.GUI.Forms.SinhVien
{
    public partial class KetQuaHocTap : Form
    {
        
        private readonly KetQuaBLL _bll = new KetQuaBLL();

        
        private string _maSV    = "";
        private bool   _loading = false;  // tránh trigger event khi đang nạp combo

        
        public KetQuaHocTap()
        {
            InitializeComponent();
            InitDataGridColumns();
        }

        private void InitDataGridColumns()
        {
            if (DataGridViewKQDiem.Columns.Count > 0)
                return;

            DataGridViewKQDiem.AutoGenerateColumns = false;
            DataGridViewKQDiem.Columns.AddRange(
                MakeCol("colSTT", "STT", 50, DataGridViewAutoSizeColumnMode.None, DataGridViewContentAlignment.MiddleCenter),
                MakeCol("colMaMon", "Mã môn", 100, DataGridViewAutoSizeColumnMode.None, DataGridViewContentAlignment.MiddleCenter),
                MakeCol("colTenMon", "Tên môn học", 220, DataGridViewAutoSizeColumnMode.Fill, DataGridViewContentAlignment.MiddleLeft),
                MakeCol("colSoTC", "Số TC", 70, DataGridViewAutoSizeColumnMode.None, DataGridViewContentAlignment.MiddleCenter),
                MakeCol("colDiemCC", "CC", 60, DataGridViewAutoSizeColumnMode.None, DataGridViewContentAlignment.MiddleCenter),
                MakeCol("colDiemKT1", "KT1", 60, DataGridViewAutoSizeColumnMode.None, DataGridViewContentAlignment.MiddleCenter),
                MakeCol("colDiemKT2", "KT2", 60, DataGridViewAutoSizeColumnMode.None, DataGridViewContentAlignment.MiddleCenter),
                MakeCol("colDiemThi", "Thi", 60, DataGridViewAutoSizeColumnMode.None, DataGridViewContentAlignment.MiddleCenter),
                MakeCol("colDiemTK", "Tổng kết", 80, DataGridViewAutoSizeColumnMode.None, DataGridViewContentAlignment.MiddleCenter));
        }

        private static DataGridViewTextBoxColumn MakeCol(
            string name, string header, int width,
            DataGridViewAutoSizeColumnMode autoSizeMode = DataGridViewAutoSizeColumnMode.None,
            DataGridViewContentAlignment alignment = DataGridViewContentAlignment.MiddleLeft)
        {
            var col = new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = header,
                Width = width,
                MinimumWidth = 30,
                ReadOnly = true,
                SortMode = DataGridViewColumnSortMode.NotSortable,
                AutoSizeMode = autoSizeMode
            };
            col.HeaderCell.Style.Alignment = alignment;
            col.DefaultCellStyle.Alignment = alignment;
            return col;
        }


        private void KetQuaHocTap_Load(object sender, EventArgs e)
        {
            try
            {
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


        private void LoadHocKy()
        {
            _loading = true;
            DataTable dt = _bll.GetLoaiHocKy();


            DataRow rowAll = dt.NewRow();
            rowAll["MaLoaiHK"]  = "ALL";
            rowAll["TenLoaiHK"] = "-- Tất cả --";
            dt.Rows.InsertAt(rowAll, 0);

            cboHocKy.DataSource    = dt;
            cboHocKy.ValueMember   = "MaLoaiHK";
            cboHocKy.DisplayMember = "TenLoaiHK";
            _loading = false;
        }


        private void LoadNamHocCuaSinhVien()
        {
            if (string.IsNullOrEmpty(_maSV)) return;

            _loading = true;
            DataTable dt = _bll.GetNamHocBySinhVien(_maSV);

            
            DataRow rowAll = dt.NewRow();
            rowAll["MaNamHoc"]  = "ALL";
            rowAll["TenNamHoc"] = "-- Tất cả --";
            dt.Rows.InsertAt(rowAll, 0);

            cboNamHoc.DataSource    = dt;
            cboNamHoc.ValueMember   = "MaNamHoc";
            cboNamHoc.DisplayMember = "TenNamHoc";
            _loading = false;

            LoadBangDiem();   
        }

        
        private void CboNamHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_loading) LoadBangDiem();
        }

        private void CboHocKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_loading) LoadBangDiem();
        }

        
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

                string tkStr   = tk.HasValue ? tk.Value.ToString("F2") : "--";

                DataGridViewKQDiem.Rows.Add(
                    stt++,
                    row["MaMon"],
                    row["TenMon"],
                    row["SoTC"],
                    FormatDiem(cc),
                    FormatDiem(kt1),
                    FormatDiem(kt2),
                    FormatDiem(thi),
                    tkStr
                );
            }
        }

        
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

            double dtbHK10 = tcTichLuy > 0 ? sumWeighted / tcTichLuy : 0;
            double dtbHK4  = Quy4(dtbHK10);

            lblTC.Text       = tcTichLuy.ToString();
            lblTK10.Text     = tcTichLuy > 0 ? dtbHK10.ToString("F2") : "--";
            lblTK4.Text      = tcTichLuy > 0 ? dtbHK4.ToString("F2")  : "--";
            lblPhanloai.Text = tcTichLuy > 0 ? PhanLoaiGPA(dtbHK4)    : "--";
        }

        
        private static decimal? TinhDiemTongKet(decimal? cc, decimal? kt1, decimal? kt2, decimal? thi)
        {
            if (!cc.HasValue || !kt1.HasValue || !kt2.HasValue || !thi.HasValue)
                return null;
            return (decimal)KetQuaBLL.TinhDiemTongKet(
                (double)cc.Value, (double)kt1.Value, (double)kt2.Value, (double)thi.Value);
        }

        
        /// Xếp loại chữ và quy đổi hệ 4 từ điểm tổng kết hệ 10.
        /// Thang: 9.5-10→A+(4.0) | 8.5-9.5→A(4.0) | 8.0-8.5→B+(3.5) |
        ///        7.0-8.0→B(3.0) | 6.5-7.0→C+(2.5) | 5.5-6.5→C(2.0) |
        ///        5.0-5.5→D+(1.5) | 4.0-5.0→D(1.0) | <4.0→F(0)
       
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

        /// Phân loại học lực theo GPA hệ 4.
        /// 3.6-4.0→Xuất sắc | 3.2-3.6→Giỏi | 2.5-3.2→Khá |
        /// 2.0-2.5→Trung bình | 1.0-2.0→Yếu | <1.0→Kém
        private static string PhanLoaiGPA(double gpa4)
        {
            if (gpa4 >= 3.6) return "Xuất sắc";
            if (gpa4 >= 3.2) return "Giỏi";
            if (gpa4 >= 2.5) return "Khá";
            if (gpa4 >= 2.0) return "Trung bình";
            if (gpa4 >= 1.0) return "Yếu";
            return "Kém";
        }

        
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

        

        private sealed class TongKetHocTap
        {
            public int TongTinChi { get; set; }
            public double DiemHe10 { get; set; }
            public double DiemHe4 { get; set; }
            public string XepLoai { get; set; }
            public bool CoDuLieu { get; set; }
        }

        private void btnXuatBangDiem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(_maSV))
                {
                    MessageBox.Show("Chưa có thông tin sinh viên.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataTable thongTin = _bll.GetThongTinSinhVienDayDu(_maSV);
                if (thongTin.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy thông tin sinh viên.", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataTable bangDiem = LayBangDiemXuatToanBo();
                var tongKet = TinhTongKetTuBangDiem(bangDiem);
                if (!tongKet.CoDuLieu)
                {
                    MessageBox.Show("Chưa có môn nào đủ điểm tổng kết để xuất bảng điểm.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var dlg = new SaveFileDialog())
                {
                    dlg.Filter = "Excel Workbook|*.xlsx";
                    dlg.FileName = $"BangDiem_{_maSV}_{DateTime.Now:yyyyMMdd}.xlsx";
                    dlg.Title = "Lưu bảng điểm sinh viên";

                    if (dlg.ShowDialog() != DialogResult.OK) return;

                    XuatBangDiemExcel(dlg.FileName, thongTin.Rows[0], bangDiem, tongKet);

                    MessageBox.Show("Xuất bảng điểm thành công!\n" + dlg.FileName,
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Process.Start(dlg.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xuất bảng điểm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
        private DataTable LayBangDiemXuatToanBo()
        {
            DataTable dt = _bll.GetBangDiemSinhVien(_maSV, "ALL", "ALL");
            dt.Columns.Add("DiemTK", typeof(double));
            dt.Columns.Add("DiemChu", typeof(string));

            foreach (DataRow row in dt.Rows)
            {
                decimal? tk = TinhDiemTongKet(
                    ToDecimal(row["DiemCC"]),
                    ToDecimal(row["DiemKT1"]),
                    ToDecimal(row["DiemKT2"]),
                    ToDecimal(row["DiemThi"]));

                if (!tk.HasValue) continue;

                row["DiemTK"] = (double)tk.Value;
                row["DiemChu"] = KetQuaBLL.QuyDoiDiemChu((double)tk.Value);
            }

            return dt;
        }

        private static TongKetHocTap TinhTongKetTuBangDiem(DataTable dt)
        {
            var ketQua = new TongKetHocTap();
            if (dt == null || dt.Rows.Count == 0) return ketQua;

            int tongTc = 0;
            double sumWeighted = 0;

            foreach (DataRow row in dt.Rows)
            {
                if (row["DiemTK"] == DBNull.Value) continue;

                int soTc = Convert.ToInt32(row["SoTC"]);
                double diemTk = Convert.ToDouble(row["DiemTK"]);
                tongTc += soTc;
                sumWeighted += soTc * diemTk;
            }

            if (tongTc == 0) return ketQua;

            ketQua.CoDuLieu = true;
            ketQua.TongTinChi = tongTc;
            ketQua.DiemHe10 = Math.Round(sumWeighted / tongTc, 2);
            ketQua.DiemHe4 = KetQuaBLL.QuyDoiHe4(ketQua.DiemHe10);
            ketQua.XepLoai = KetQuaBLL.XepLoaiHocLuc(ketQua.DiemHe4);
            return ketQua;
        }

        private static void XuatBangDiemExcel(
            string filePath, DataRow thongTinSv, DataTable bangDiem, TongKetHocTap tongKet)
        {
            using (var wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Bảng điểm");

                ws.Cell("A1").Value = "BẢNG ĐIỂM SINH VIÊN (TOÀN KHÓA)";
                ws.Cell("A1").Style.Font.Bold = true;
                ws.Cell("A1").Style.Font.FontSize = 16;
                ws.Cell("A1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Range("A1:F1").Merge();

                ws.Cell("A2").Value = $"Ngày xuất: {DateTime.Now:dd/MM/yyyy HH:mm}";
                ws.Cell("A2").Style.Font.Italic = true;
                ws.Range("A2:F2").Merge();

                int row = 4;
                row = GhiDongThongTin(ws, row, "Mã sinh viên", thongTinSv["MaSV"]);
                row = GhiDongThongTin(ws, row, "Họ và tên", thongTinSv["HoTen"]);
                row = GhiDongThongTin(ws, row, "Giới tính",
                    Convert.ToBoolean(thongTinSv["GioiTinh"]) ? "Nam" : "Nữ");
                row = GhiDongThongTin(ws, row, "Ngày sinh",
                    thongTinSv["NgaySinh"] != DBNull.Value
                        ? Convert.ToDateTime(thongTinSv["NgaySinh"]).ToString("dd/MM/yyyy")
                        : "");
                row = GhiDongThongTin(ws, row, "Khoa", ChuoiAnToan(thongTinSv, "TenKhoa"));
                row = GhiDongThongTin(ws, row, "Lớp", ChuoiAnToan(thongTinSv, "TenLop"));
                row = GhiDongThongTin(ws, row, "Niên khóa", ChuoiAnToan(thongTinSv, "NienKhoa"));
                row = GhiDongThongTin(ws, row, "Địa chỉ", ChuoiAnToan(thongTinSv, "DiaChi"));
                row = GhiDongThongTin(ws, row, "Số điện thoại", ChuoiAnToan(thongTinSv, "SoDT"));
                row = GhiDongThongTin(ws, row, "Email", ChuoiAnToan(thongTinSv, "Email"));
                row = GhiDongThongTin(ws, row, "Tài khoản", ChuoiAnToan(thongTinSv, "TenDangNhap"));

                row += 1;
                int headerRow = row;
                string[] headers = { "STT", "Mã môn", "Tên môn học", "Số TC", "Điểm tổng kết", "Điểm chữ" };

                for (int i = 0; i < headers.Length; i++)
                {
                    var cell = ws.Cell(headerRow, i + 1);
                    cell.Value = headers[i];
                    cell.Style.Font.Bold = true;
                    cell.Style.Fill.BackgroundColor = XLColor.FromArgb(43, 54, 116);
                    cell.Style.Font.FontColor = XLColor.White;
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                }

                int dataRow = headerRow + 1;
                int stt = 1;

                foreach (DataRow dr in bangDiem.Rows)
                {
                    if (dr["DiemTK"] == DBNull.Value) continue;

                    ws.Cell(dataRow, 1).Value = stt++;
                    ws.Cell(dataRow, 2).Value = dr["MaMon"].ToString();
                    ws.Cell(dataRow, 3).Value = dr["TenMon"].ToString();
                    ws.Cell(dataRow, 4).Value = Convert.ToInt32(dr["SoTC"]);
                    ws.Cell(dataRow, 5).Value = Convert.ToDouble(dr["DiemTK"]);
                    ws.Cell(dataRow, 5).Style.NumberFormat.Format = "0.00";
                    ws.Cell(dataRow, 6).Value = dr["DiemChu"].ToString();

                    var rowRange = ws.Range(dataRow, 1, dataRow, 6);
                    rowRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    rowRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                    if (stt % 2 == 0)
                        rowRange.Style.Fill.BackgroundColor = XLColor.FromArgb(242, 242, 242);

                    dataRow++;
                }

                int summaryRow = dataRow + 1;
                ws.Cell(summaryRow, 1).Value = "Tổng kết";
                ws.Cell(summaryRow, 1).Style.Font.Bold = true;
                ws.Range(summaryRow, 1, summaryRow, 3).Merge();

                ws.Cell(summaryRow, 4).Value = "Số tín chỉ đạt được:";
                ws.Cell(summaryRow, 4).Style.Font.Bold = true;
                ws.Cell(summaryRow, 5).Value = tongKet.TongTinChi;

                ws.Cell(summaryRow + 1, 4).Value = "Điểm tổng kết (hệ 10):";
                ws.Cell(summaryRow + 1, 4).Style.Font.Bold = true;
                ws.Cell(summaryRow + 1, 5).Value = tongKet.DiemHe10;
                ws.Cell(summaryRow + 1, 5).Style.NumberFormat.Format = "0.00";

                ws.Cell(summaryRow + 2, 4).Value = "Điểm tổng kết (hệ 4):";
                ws.Cell(summaryRow + 2, 4).Style.Font.Bold = true;
                ws.Cell(summaryRow + 2, 5).Value = tongKet.DiemHe4;
                ws.Cell(summaryRow + 2, 5).Style.NumberFormat.Format = "0.00";

                ws.Cell(summaryRow + 3, 4).Value = "Xếp loại:";
                ws.Cell(summaryRow + 3, 4).Style.Font.Bold = true;
                ws.Cell(summaryRow + 3, 5).Value = tongKet.XepLoai;

                ws.Columns().AdjustToContents();
                ws.Column(3).Width = Math.Max(ws.Column(3).Width, 28);

                wb.SaveAs(filePath);
            }
        }

        private static int GhiDongThongTin(IXLWorksheet ws, int row, string nhan, object giaTri)
        {
            ws.Cell(row, 1).Value = nhan + ":";
            ws.Cell(row, 1).Style.Font.Bold = true;
            ws.Cell(row, 2).Value = giaTri?.ToString() ?? "";
            ws.Range(row, 2, row, 6).Merge();
            return row + 1;
        }

        private static string ChuoiAnToan(DataRow row, string cot)
        {
            return row.Table.Columns.Contains(cot) && row[cot] != DBNull.Value
                ? row[cot].ToString()
                : "";
        }
    }
}
