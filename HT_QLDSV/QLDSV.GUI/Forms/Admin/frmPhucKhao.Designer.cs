namespace QLDSV.GUI
{
    partial class frmPhucKhao
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlSidebar = new Guna.UI2.WinForms.Guna2Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2Button7 = new Guna.UI2.WinForms.Guna2Button();
            
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            
            this.pnlContent = new System.Windows.Forms.Panel();
            
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.btnThem = new Guna.UI2.WinForms.Guna2Button();
            this.btnDuyet = new Guna.UI2.WinForms.Guna2Button();
            this.btnTuChoi = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoa = new Guna.UI2.WinForms.Guna2Button();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.lblFilterTrangThai = new System.Windows.Forms.Label();
            this.cboFilterTrangThai = new System.Windows.Forms.ComboBox();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            
            this.pnlStats = new System.Windows.Forms.Panel();
            this.lblThongKeCaption = new System.Windows.Forms.Label();
            this.pnlStatTong = new System.Windows.Forms.Panel();
            this.lblStatTongVal = new System.Windows.Forms.Label();
            this.lblStatTongTitle = new System.Windows.Forms.Label();
            
            this.pnlStatChoDuyet = new System.Windows.Forms.Panel();
            this.lblStatChoDuyetVal = new System.Windows.Forms.Label();
            this.lblStatChoDuyetTitle = new System.Windows.Forms.Label();
            
            this.pnlStatDaDuyet = new System.Windows.Forms.Panel();
            this.lblStatDaDuyetVal = new System.Windows.Forms.Label();
            this.lblStatDaDuyetTitle = new System.Windows.Forms.Label();
            
            this.pnlStatTuChoi = new System.Windows.Forms.Panel();
            this.lblStatTuChoiVal = new System.Windows.Forms.Label();
            this.lblStatTuChoiTitle = new System.Windows.Forms.Label();

            this.pnlMain = new System.Windows.Forms.Panel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.pnlDetail = new System.Windows.Forms.Panel();
            
            this.btnCloseDetail = new System.Windows.Forms.Button();
            this.lblDetailHeader = new System.Windows.Forms.Label();
            this.lblDetailSep = new System.Windows.Forms.Label();
            
            this.lblSinhVienCaption = new System.Windows.Forms.Label();
            this.cboSinhVien = new System.Windows.Forms.ComboBox();
            
            this.lblLopHocPhanCaption = new System.Windows.Forms.Label();
            this.cboLopHocPhan = new System.Windows.Forms.ComboBox();
            
            this.lblNgayYCCaption = new System.Windows.Forms.Label();
            this.dtpNgayYeuCau = new System.Windows.Forms.DateTimePicker();
            
            this.lblTrangThaiCaptionDetail = new System.Windows.Forms.Label();
            this.txtTrangThai = new System.Windows.Forms.TextBox();
            
            this.lblLyDoCaption = new System.Windows.Forms.Label();
            this.txtLyDo = new System.Windows.Forms.TextBox();
            
            this.btnLuuDetail = new Guna.UI2.WinForms.Guna2Button();
            this.btnHuyDetail = new Guna.UI2.WinForms.Guna2Button();
            this.lblDetailStatus = new System.Windows.Forms.Label();
            
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.lblTongBanGhi = new System.Windows.Forms.Label();
            
            this.pnlSidebar.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlToolbar.SuspendLayout();
            this.pnlStats.SuspendLayout();
            this.pnlStatTong.SuspendLayout();
            this.pnlStatChoDuyet.SuspendLayout();
            this.pnlStatDaDuyet.SuspendLayout();
            this.pnlStatTuChoi.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.pnlDetail.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();

            // ── Sidebar ──────────────────────────────────────────────
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(21, 101, 192);
            this.pnlSidebar.BorderRadius = 5;
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(196, 700);
            this.pnlSidebar.TabIndex = 0;
            this.pnlSidebar.Controls.Add(this.label1);
            this.pnlSidebar.Controls.Add(this.guna2HtmlLabel2);
            this.pnlSidebar.Controls.Add(this.guna2HtmlLabel3);
            this.pnlSidebar.Controls.Add(this.guna2Button7);

            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(28, 17);
            this.label1.Name = "label1";
            this.label1.Text = "QUẢN LÝ ĐÀO TẠO";

            this.guna2HtmlLabel2.AutoSize = false;
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(9, 34);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(175, 17);
            this.guna2HtmlLabel2.Text = "---------------------------------------------------------------";

            this.guna2HtmlLabel3.AutoSize = false;
            this.guna2HtmlLabel3.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel3.Location = new System.Drawing.Point(11, 647);
            this.guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            this.guna2HtmlLabel3.Size = new System.Drawing.Size(175, 17);
            this.guna2HtmlLabel3.Text = "---------------------------------------------------------------";

            this.guna2Button7.FillColor = System.Drawing.Color.Transparent;
            this.guna2Button7.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2Button7.ForeColor = System.Drawing.Color.White;
            this.guna2Button7.Location = new System.Drawing.Point(7, 659);
            this.guna2Button7.Name = "guna2Button7";
            this.guna2Button7.Size = new System.Drawing.Size(160, 36);
            this.guna2Button7.Text = "Đăng xuất";
            this.guna2Button7.TextOffset = new System.Drawing.Point(5, 0);

            // ── Header ───────────────────────────────────────────────
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(21, 101, 192);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 50;
            this.pnlHeader.Controls.Add(this.lblTitle);

            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(16, 12);
            this.lblTitle.Text = "📋  QUẢN LÝ PHÚC KHẢO";

            // ── Content ──────────────────────────────────────────────
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.BackColor = System.Drawing.Color.White;
            this.pnlContent.Controls.Add(this.pnlMain);
            this.pnlContent.Controls.Add(this.pnlStats);
            this.pnlContent.Controls.Add(this.pnlToolbar);
            this.pnlContent.Controls.Add(this.pnlFooter);

            // ── Toolbar ──────────────────────────────────────────────
            this.pnlToolbar.BackColor = System.Drawing.Color.White;
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Height = 55;
            this.pnlToolbar.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.pnlToolbar.Controls.Add(this.btnThem);
            this.pnlToolbar.Controls.Add(this.btnDuyet);
            this.pnlToolbar.Controls.Add(this.btnTuChoi);
            this.pnlToolbar.Controls.Add(this.btnXoa);
            this.pnlToolbar.Controls.Add(this.btnLamMoi);
            this.pnlToolbar.Controls.Add(this.lblFilterTrangThai);
            this.pnlToolbar.Controls.Add(this.cboFilterTrangThai);
            this.pnlToolbar.Controls.Add(this.txtTimKiem);

            StyleButton(this.btnThem, "+ Thêm mới", System.Drawing.Color.FromArgb(21, 101, 192), 10, 12, 110);
            StyleButton(this.btnDuyet, "☑ Duyệt", System.Drawing.Color.FromArgb(46, 125, 50), 130, 12, 100);
            StyleButton(this.btnTuChoi, "✕ Từ chối", System.Drawing.Color.FromArgb(198, 40, 40), 240, 12, 100);
            StyleButton(this.btnXoa, "🗑 Xóa", System.Drawing.Color.FromArgb(128, 0, 0), 350, 12, 90);
            StyleButton(this.btnLamMoi, "↺ Làm mới", System.Drawing.Color.FromArgb(56, 142, 60), 450, 12, 110);

            this.lblFilterTrangThai.Text = "Trạng thái:";
            this.lblFilterTrangThai.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.lblFilterTrangThai.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.lblFilterTrangThai.Location = new System.Drawing.Point(510, 17);
            this.lblFilterTrangThai.AutoSize = true;

            this.cboFilterTrangThai.Location = new System.Drawing.Point(585, 13);
            this.cboFilterTrangThai.Size = new System.Drawing.Size(120, 26);
            this.cboFilterTrangThai.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboFilterTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.txtTimKiem.Location = new System.Drawing.Point(715, 13);
            this.txtTimKiem.Size = new System.Drawing.Size(175, 26);
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtTimKiem.ForeColor = System.Drawing.Color.Gray;

            // ── Stats Panel ──────────────────────────────────────────
            this.pnlStats.BackColor = System.Drawing.Color.White;
            this.pnlStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlStats.Height = 55;
            this.pnlStats.Controls.Add(this.lblThongKeCaption);
            this.pnlStats.Controls.Add(this.pnlStatTong);
            this.pnlStats.Controls.Add(this.pnlStatChoDuyet);
            this.pnlStats.Controls.Add(this.pnlStatDaDuyet);
            this.pnlStats.Controls.Add(this.pnlStatTuChoi);

            this.lblThongKeCaption.Text = "Thống kê:";
            this.lblThongKeCaption.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblThongKeCaption.ForeColor = System.Drawing.Color.Gray;
            this.lblThongKeCaption.Location = new System.Drawing.Point(16, 20);
            this.lblThongKeCaption.AutoSize = true;

            StyleStatCard(this.pnlStatTong, this.lblStatTongTitle, this.lblStatTongVal, "Tổng", "0", 90);
            StyleStatCard(this.pnlStatChoDuyet, this.lblStatChoDuyetTitle, this.lblStatChoDuyetVal, "Chờ duyệt", "0", 215);
            StyleStatCard(this.pnlStatDaDuyet, this.lblStatDaDuyetTitle, this.lblStatDaDuyetVal, "Đã duyệt", "0", 350);
            StyleStatCard(this.pnlStatTuChoi, this.lblStatTuChoiTitle, this.lblStatTuChoiVal, "Từ chối", "0", 475);

            // ── Main area (grid + detail) ─────────────────────────────
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.pnlDetail);
            this.pnlMain.Controls.Add(this.dataGridView);

            // ── DataGridView ─────────────────────────────────────────
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(21, 101, 192);
            this.dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dataGridView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dataGridView.ColumnHeadersHeight = 36;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dataGridView.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(187, 222, 251);
            this.dataGridView.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.GridColor = System.Drawing.Color.FromArgb(224, 224, 224);
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.TabIndex = 0;

            // ── Detail Panel (right sidebar) ──────────────────────────
            this.pnlDetail.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlDetail.Width = 0;
            this.pnlDetail.BackColor = System.Drawing.Color.FromArgb(245, 248, 255);
            this.pnlDetail.Padding = new System.Windows.Forms.Padding(16);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Controls.Add(this.btnCloseDetail);
            this.pnlDetail.Controls.Add(this.lblDetailHeader);
            this.pnlDetail.Controls.Add(this.lblDetailSep);
            this.pnlDetail.Controls.Add(this.lblSinhVienCaption);
            this.pnlDetail.Controls.Add(this.cboSinhVien);
            this.pnlDetail.Controls.Add(this.lblLopHocPhanCaption);
            this.pnlDetail.Controls.Add(this.cboLopHocPhan);
            this.pnlDetail.Controls.Add(this.lblNgayYCCaption);
            this.pnlDetail.Controls.Add(this.dtpNgayYeuCau);
            this.pnlDetail.Controls.Add(this.lblTrangThaiCaptionDetail);
            this.pnlDetail.Controls.Add(this.txtTrangThai);
            this.pnlDetail.Controls.Add(this.lblLyDoCaption);
            this.pnlDetail.Controls.Add(this.txtLyDo);
            this.pnlDetail.Controls.Add(this.btnLuuDetail);
            this.pnlDetail.Controls.Add(this.btnHuyDetail);
            this.pnlDetail.Controls.Add(this.lblDetailStatus);

            // Close button
            this.btnCloseDetail.Text = "✕";
            this.btnCloseDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseDetail.FlatAppearance.BorderSize = 0;
            this.btnCloseDetail.BackColor = System.Drawing.Color.Transparent;
            this.btnCloseDetail.ForeColor = System.Drawing.Color.Gray;
            this.btnCloseDetail.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCloseDetail.Size = new System.Drawing.Size(30, 30);
            this.btnCloseDetail.Location = new System.Drawing.Point(260, 10);
            this.btnCloseDetail.Cursor = System.Windows.Forms.Cursors.Hand;

            // Header label
            this.lblDetailHeader.Text = "📄  CHI TIẾT YÊU CẦU";
            this.lblDetailHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblDetailHeader.ForeColor = System.Drawing.Color.FromArgb(21, 101, 192);
            this.lblDetailHeader.Location = new System.Drawing.Point(16, 14);
            this.lblDetailHeader.AutoSize = true;

            // Separator
            this.lblDetailSep.Text = "";
            this.lblDetailSep.BackColor = System.Drawing.Color.FromArgb(21, 101, 192);
            this.lblDetailSep.Location = new System.Drawing.Point(16, 44);
            this.lblDetailSep.Size = new System.Drawing.Size(268, 2);

            // Detail Fields & Dropdowns
            this.lblSinhVienCaption.Text = "Sinh Viên *";
            this.lblSinhVienCaption.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblSinhVienCaption.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.lblSinhVienCaption.Location = new System.Drawing.Point(16, 60);
            this.lblSinhVienCaption.AutoSize = true;

            this.cboSinhVien.Location = new System.Drawing.Point(16, 78);
            this.cboSinhVien.Size = new System.Drawing.Size(268, 26);
            this.cboSinhVien.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboSinhVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblLopHocPhanCaption.Text = "Lớp Học Phần *";
            this.lblLopHocPhanCaption.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblLopHocPhanCaption.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.lblLopHocPhanCaption.Location = new System.Drawing.Point(16, 115);
            this.lblLopHocPhanCaption.AutoSize = true;

            this.cboLopHocPhan.Location = new System.Drawing.Point(16, 133);
            this.cboLopHocPhan.Size = new System.Drawing.Size(268, 26);
            this.cboLopHocPhan.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboLopHocPhan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblNgayYCCaption.Text = "Ngày Yêu Cầu";
            this.lblNgayYCCaption.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblNgayYCCaption.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.lblNgayYCCaption.Location = new System.Drawing.Point(16, 170);
            this.lblNgayYCCaption.AutoSize = true;

            this.dtpNgayYeuCau.Location = new System.Drawing.Point(16, 188);
            this.dtpNgayYeuCau.Size = new System.Drawing.Size(268, 26);
            this.dtpNgayYeuCau.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtpNgayYeuCau.Format = System.Windows.Forms.DateTimePickerFormat.Short;

            this.lblTrangThaiCaptionDetail.Text = "Trạng Thái";
            this.lblTrangThaiCaptionDetail.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblTrangThaiCaptionDetail.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.lblTrangThaiCaptionDetail.Location = new System.Drawing.Point(16, 225);
            this.lblTrangThaiCaptionDetail.AutoSize = true;

            this.txtTrangThai.Location = new System.Drawing.Point(16, 243);
            this.txtTrangThai.Size = new System.Drawing.Size(268, 26);
            this.txtTrangThai.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtTrangThai.ReadOnly = true;
            this.txtTrangThai.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);

            this.lblLyDoCaption.Text = "Lý Do Phúc Khảo *";
            this.lblLyDoCaption.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblLyDoCaption.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.lblLyDoCaption.Location = new System.Drawing.Point(16, 280);
            this.lblLyDoCaption.AutoSize = true;

            this.txtLyDo.Location = new System.Drawing.Point(16, 298);
            this.txtLyDo.Size = new System.Drawing.Size(268, 80);
            this.txtLyDo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtLyDo.Multiline = true;
            this.txtLyDo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLyDo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Save button
            this.btnLuuDetail.Text = "💾  Lưu";
            this.btnLuuDetail.FillColor = System.Drawing.Color.FromArgb(21, 101, 192);
            this.btnLuuDetail.ForeColor = System.Drawing.Color.White;
            this.btnLuuDetail.BorderRadius = 5;
            this.btnLuuDetail.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnLuuDetail.Size = new System.Drawing.Size(128, 34);
            this.btnLuuDetail.Location = new System.Drawing.Point(16, 395);
            this.btnLuuDetail.Cursor = System.Windows.Forms.Cursors.Hand;

            // Cancel button
            this.btnHuyDetail.Text = "✕  Hủy";
            this.btnHuyDetail.FillColor = System.Drawing.Color.FromArgb(180, 180, 180);
            this.btnHuyDetail.ForeColor = System.Drawing.Color.White;
            this.btnHuyDetail.BorderRadius = 5;
            this.btnHuyDetail.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnHuyDetail.Size = new System.Drawing.Size(122, 34);
            this.btnHuyDetail.Location = new System.Drawing.Point(162, 395);
            this.btnHuyDetail.Cursor = System.Windows.Forms.Cursors.Hand;

            // Detail status status label
            this.lblDetailStatus.Font = new System.Drawing.Font("Segoe UI Light", 8.5F, System.Drawing.FontStyle.Italic);
            this.lblDetailStatus.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.lblDetailStatus.Location = new System.Drawing.Point(16, 440);
            this.lblDetailStatus.Size = new System.Drawing.Size(268, 20);
            this.lblDetailStatus.Text = "🔑 Đang xem: N/A";

            // ── Footer ───────────────────────────────────────────────
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Height = 30;
            this.pnlFooter.Controls.Add(this.lblTongBanGhi);

            this.lblTongBanGhi.AutoSize = true;
            this.lblTongBanGhi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTongBanGhi.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
            this.lblTongBanGhi.Location = new System.Drawing.Point(10, 7);
            this.lblTongBanGhi.Text = "Tổng: 0 bản ghi";

            // ── Form ─────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlSidebar);
            this.Name = "frmPhucKhao";
            this.Text = "Quản Lý Yêu Cầu Phúc Khảo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.pnlSidebar.ResumeLayout(false);
            this.pnlSidebar.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlStats.ResumeLayout(false);
            this.pnlStats.PerformLayout();
            this.pnlStatTong.ResumeLayout(false);
            this.pnlStatChoDuyet.ResumeLayout(false);
            this.pnlStatDaDuyet.ResumeLayout(false);
            this.pnlStatTuChoi.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.pnlDetail.ResumeLayout(false);
            this.pnlDetail.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        // Helper: style toolbar buttons
        private void StyleButton(Guna.UI2.WinForms.Guna2Button btn, string text, System.Drawing.Color color, int x, int y, int width)
        {
            btn.Text = text;
            btn.FillColor = color;
            btn.ForeColor = System.Drawing.Color.White;
            btn.BorderRadius = 5;
            btn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btn.Size = new System.Drawing.Size(width, 30);
            btn.Location = new System.Drawing.Point(x, y);
            btn.Cursor = System.Windows.Forms.Cursors.Hand;
        }

        // Helper: style stat card
        private void StyleStatCard(System.Windows.Forms.Panel pnl, System.Windows.Forms.Label title, System.Windows.Forms.Label value, string titleText, string defaultVal, int x)
        {
            pnl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pnl.BackColor = System.Drawing.Color.White;
            pnl.Size = new System.Drawing.Size(115, 42);
            pnl.Location = new System.Drawing.Point(x, 6);
            pnl.Controls.Add(title);
            pnl.Controls.Add(value);

            title.Text = titleText;
            title.Font = new System.Drawing.Font("Segoe UI", 8F);
            title.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
            title.Location = new System.Drawing.Point(4, 2);
            title.Size = new System.Drawing.Size(105, 15);
            title.TextAlign = System.Drawing.ContentAlignment.TopCenter;

            value.Text = defaultVal;
            value.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            value.ForeColor = System.Drawing.Color.FromArgb(10, 10, 10);
            value.Location = new System.Drawing.Point(4, 18);
            value.Size = new System.Drawing.Size(105, 20);
            value.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
        }

        // Controls
        private Guna.UI2.WinForms.Guna2Panel pnlSidebar;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Guna.UI2.WinForms.Guna2Button guna2Button7;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlToolbar;
        private Guna.UI2.WinForms.Guna2Button btnThem;
        private Guna.UI2.WinForms.Guna2Button btnDuyet;
        private Guna.UI2.WinForms.Guna2Button btnTuChoi;
        private Guna.UI2.WinForms.Guna2Button btnXoa;
        private Guna.UI2.WinForms.Guna2Button btnLamMoi;
        private System.Windows.Forms.Label lblFilterTrangThai;
        private System.Windows.Forms.ComboBox cboFilterTrangThai;
        private System.Windows.Forms.TextBox txtTimKiem;
        
        private System.Windows.Forms.Panel pnlStats;
        private System.Windows.Forms.Label lblThongKeCaption;
        private System.Windows.Forms.Panel pnlStatTong;
        private System.Windows.Forms.Label lblStatTongVal;
        private System.Windows.Forms.Label lblStatTongTitle;
        private System.Windows.Forms.Panel pnlStatChoDuyet;
        private System.Windows.Forms.Label lblStatChoDuyetVal;
        private System.Windows.Forms.Label lblStatChoDuyetTitle;
        private System.Windows.Forms.Panel pnlStatDaDuyet;
        private System.Windows.Forms.Label lblStatDaDuyetVal;
        private System.Windows.Forms.Label lblStatDaDuyetTitle;
        private System.Windows.Forms.Panel pnlStatTuChoi;
        private System.Windows.Forms.Label lblStatTuChoiVal;
        private System.Windows.Forms.Label lblStatTuChoiTitle;

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Panel pnlDetail;
        private System.Windows.Forms.Button btnCloseDetail;
        private System.Windows.Forms.Label lblDetailHeader;
        private System.Windows.Forms.Label lblDetailSep;
        private System.Windows.Forms.Label lblSinhVienCaption;
        private System.Windows.Forms.ComboBox cboSinhVien;
        private System.Windows.Forms.Label lblLopHocPhanCaption;
        private System.Windows.Forms.ComboBox cboLopHocPhan;
        private System.Windows.Forms.Label lblNgayYCCaption;
        private System.Windows.Forms.DateTimePicker dtpNgayYeuCau;
        private System.Windows.Forms.Label lblTrangThaiCaptionDetail;
        private System.Windows.Forms.TextBox txtTrangThai;
        private System.Windows.Forms.Label lblLyDoCaption;
        private System.Windows.Forms.TextBox txtLyDo;
        private Guna.UI2.WinForms.Guna2Button btnLuuDetail;
        private Guna.UI2.WinForms.Guna2Button btnHuyDetail;
        private System.Windows.Forms.Label lblDetailStatus;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblTongBanGhi;
    }
}
