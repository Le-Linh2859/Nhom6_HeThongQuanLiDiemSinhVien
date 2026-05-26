namespace QLDSV.GUI
{
    partial class frmMonhoc
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
            this.guna2Button7 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTim = new System.Windows.Forms.Button();
            this.lblFilterKhoa = new System.Windows.Forms.Label();
            this.cboFilterKhoa = new System.Windows.Forms.ComboBox();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.pnlDetail = new System.Windows.Forms.Panel();
            this.btnCloseDetail = new System.Windows.Forms.Button();
            this.lblDetailHeader = new System.Windows.Forms.Label();
            this.lblDetailSep = new System.Windows.Forms.Label();
            
            this.lblMaMonCaption = new System.Windows.Forms.Label();
            this.lblDetailMaMon = new System.Windows.Forms.Label();
            this.txtEditMaMon = new System.Windows.Forms.TextBox();

            this.lblTenMonCaption = new System.Windows.Forms.Label();
            this.lblDetailTenMon = new System.Windows.Forms.Label();
            this.txtEditTenMon = new System.Windows.Forms.TextBox();

            this.lblSoTCCaption = new System.Windows.Forms.Label();
            this.lblDetailSoTC = new System.Windows.Forms.Label();
            this.txtEditSoTC = new System.Windows.Forms.TextBox();

            this.lblKhoaCaption = new System.Windows.Forms.Label();
            this.lblDetailKhoa = new System.Windows.Forms.Label();
            this.cboEditKhoa = new System.Windows.Forms.ComboBox();

            this.lblMoTaCaption = new System.Windows.Forms.Label();
            this.lblDetailMoTa = new System.Windows.Forms.Label();
            this.txtEditMoTa = new System.Windows.Forms.TextBox();

            this.lblTrangThaiCaption = new System.Windows.Forms.Label();
            this.lblDetailTrangThai = new System.Windows.Forms.Label();
            this.chkEditActive = new System.Windows.Forms.CheckBox();

            this.btnLuuDetail = new System.Windows.Forms.Button();
            this.btnHuyDetail = new System.Windows.Forms.Button();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.lblTongBanGhi = new System.Windows.Forms.Label();
            
            this.pnlSidebar.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlToolbar.SuspendLayout();
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
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
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
            this.lblTitle.Text = "📚  QUẢN LÝ MÔN HỌC";

            // ── Content ──────────────────────────────────────────────
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.BackColor = System.Drawing.Color.White;
            this.pnlContent.Controls.Add(this.pnlMain);
            this.pnlContent.Controls.Add(this.pnlToolbar);
            this.pnlContent.Controls.Add(this.pnlFooter);

            // ── Toolbar ──────────────────────────────────────────────
            this.pnlToolbar.BackColor = System.Drawing.Color.White;
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Height = 55;
            this.pnlToolbar.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.pnlToolbar.Controls.Add(this.btnThem);
            this.pnlToolbar.Controls.Add(this.btnSua);
            this.pnlToolbar.Controls.Add(this.btnXoa);
            this.pnlToolbar.Controls.Add(this.btnLamMoi);
            this.pnlToolbar.Controls.Add(this.txtTimKiem);
            this.pnlToolbar.Controls.Add(this.btnTim);
            this.pnlToolbar.Controls.Add(this.lblFilterKhoa);
            this.pnlToolbar.Controls.Add(this.cboFilterKhoa);

            StyleButton(this.btnThem, "+ Thêm", System.Drawing.Color.FromArgb(21, 101, 192), 10, 12);
            StyleButton(this.btnSua, "✎ Sửa", System.Drawing.Color.FromArgb(33, 150, 243), 110, 12);
            StyleButton(this.btnXoa, "🗑 Xóa", System.Drawing.Color.FromArgb(211, 47, 47), 210, 12);
            StyleButton(this.btnLamMoi, "↺ Làm mới", System.Drawing.Color.FromArgb(56, 142, 60), 310, 12);

            this.txtTimKiem.Location = new System.Drawing.Point(420, 13);
            this.txtTimKiem.Size = new System.Drawing.Size(150, 26);
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTimKiem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTimKiem_KeyDown);

            StyleButton(this.btnTim, "Tìm", System.Drawing.Color.FromArgb(21, 101, 192), 575, 12);
            this.btnTim.Width = 60;

            this.lblFilterKhoa.AutoSize = true;
            this.lblFilterKhoa.Location = new System.Drawing.Point(645, 17);
            this.lblFilterKhoa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFilterKhoa.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.lblFilterKhoa.Text = "Bộ lọc Khoa:";

            this.cboFilterKhoa.Location = new System.Drawing.Point(730, 13);
            this.cboFilterKhoa.Size = new System.Drawing.Size(160, 26);
            this.cboFilterKhoa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboFilterKhoa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

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
            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);

            // ── Detail Panel (right sidebar) ──────────────────────────
            this.pnlDetail.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlDetail.Width = 0;
            this.pnlDetail.BackColor = System.Drawing.Color.FromArgb(245, 248, 255);
            this.pnlDetail.Padding = new System.Windows.Forms.Padding(16);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Controls.Add(this.btnCloseDetail);
            this.pnlDetail.Controls.Add(this.lblDetailHeader);
            this.pnlDetail.Controls.Add(this.lblDetailSep);
            
            this.pnlDetail.Controls.Add(this.lblMaMonCaption);
            this.pnlDetail.Controls.Add(this.lblDetailMaMon);
            this.pnlDetail.Controls.Add(this.txtEditMaMon);

            this.pnlDetail.Controls.Add(this.lblTenMonCaption);
            this.pnlDetail.Controls.Add(this.lblDetailTenMon);
            this.pnlDetail.Controls.Add(this.txtEditTenMon);

            this.pnlDetail.Controls.Add(this.lblSoTCCaption);
            this.pnlDetail.Controls.Add(this.lblDetailSoTC);
            this.pnlDetail.Controls.Add(this.txtEditSoTC);

            this.pnlDetail.Controls.Add(this.lblKhoaCaption);
            this.pnlDetail.Controls.Add(this.lblDetailKhoa);
            this.pnlDetail.Controls.Add(this.cboEditKhoa);

            this.pnlDetail.Controls.Add(this.lblMoTaCaption);
            this.pnlDetail.Controls.Add(this.lblDetailMoTa);
            this.pnlDetail.Controls.Add(this.txtEditMoTa);

            this.pnlDetail.Controls.Add(this.lblTrangThaiCaption);
            this.pnlDetail.Controls.Add(this.lblDetailTrangThai);
            this.pnlDetail.Controls.Add(this.chkEditActive);

            this.pnlDetail.Controls.Add(this.btnLuuDetail);
            this.pnlDetail.Controls.Add(this.btnHuyDetail);

            // Close button
            this.btnCloseDetail.Text = "✕";
            this.btnCloseDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseDetail.FlatAppearance.BorderSize = 0;
            this.btnCloseDetail.BackColor = System.Drawing.Color.Transparent;
            this.btnCloseDetail.ForeColor = System.Drawing.Color.Gray;
            this.btnCloseDetail.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCloseDetail.Size = new System.Drawing.Size(30, 30);
            this.btnCloseDetail.Location = new System.Drawing.Point(248, 10);
            this.btnCloseDetail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCloseDetail.Click += new System.EventHandler(this.btnCloseDetail_Click);

            // Header label
            this.lblDetailHeader.Text = "Chi tiết Môn Học";
            this.lblDetailHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblDetailHeader.ForeColor = System.Drawing.Color.FromArgb(21, 101, 192);
            this.lblDetailHeader.Location = new System.Drawing.Point(16, 14);
            this.lblDetailHeader.AutoSize = true;

            // Separator
            this.lblDetailSep.Text = "";
            this.lblDetailSep.BackColor = System.Drawing.Color.FromArgb(21, 101, 192);
            this.lblDetailSep.Location = new System.Drawing.Point(16, 44);
            this.lblDetailSep.Size = new System.Drawing.Size(248, 2);

            // Field 1: Mã môn học
            SetDetailField(this.lblMaMonCaption, this.lblDetailMaMon, "Mã môn học *", 55);
            StyleEditBox(this.txtEditMaMon, 73);

            // Field 2: Tên môn học
            SetDetailField(this.lblTenMonCaption, this.lblDetailTenMon, "Tên môn học *", 110);
            StyleEditBox(this.txtEditTenMon, 128);

            // Field 3: Số tín chỉ
            SetDetailField(this.lblSoTCCaption, this.lblDetailSoTC, "Số tín chỉ *", 165);
            StyleEditBox(this.txtEditSoTC, 183);

            // Field 4: Khoa
            SetDetailField(this.lblKhoaCaption, this.lblDetailKhoa, "Khoa quản lý *", 220);
            this.cboEditKhoa.Location = new System.Drawing.Point(16, 238);
            this.cboEditKhoa.Size = new System.Drawing.Size(248, 26);
            this.cboEditKhoa.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboEditKhoa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboEditKhoa.Visible = false;

            // Field 5: Mô tả
            SetDetailField(this.lblMoTaCaption, this.lblDetailMoTa, "Mô tả", 275);
            this.lblDetailMoTa.MaximumSize = new System.Drawing.Size(240, 0);
            this.lblDetailMoTa.AutoSize = true;
            this.txtEditMoTa.Location = new System.Drawing.Point(16, 293);
            this.txtEditMoTa.Size = new System.Drawing.Size(248, 55);
            this.txtEditMoTa.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtEditMoTa.Multiline = true;
            this.txtEditMoTa.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEditMoTa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEditMoTa.Visible = false;

            // Field 6: Trạng thái (Đang hoạt động)
            SetDetailField(this.lblTrangThaiCaption, this.lblDetailTrangThai, "Trạng thái", 355);
            this.chkEditActive.Text = "Đang hoạt động";
            this.chkEditActive.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.chkEditActive.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.chkEditActive.Location = new System.Drawing.Point(16, 373);
            this.chkEditActive.Size = new System.Drawing.Size(248, 24);
            this.chkEditActive.Visible = false;

            // Save button
            this.btnLuuDetail.Text = "💾  Lưu";
            this.btnLuuDetail.BackColor = System.Drawing.Color.FromArgb(21, 101, 192);
            this.btnLuuDetail.ForeColor = System.Drawing.Color.White;
            this.btnLuuDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuuDetail.FlatAppearance.BorderSize = 0;
            this.btnLuuDetail.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnLuuDetail.Size = new System.Drawing.Size(118, 34);
            this.btnLuuDetail.Location = new System.Drawing.Point(16, 415);
            this.btnLuuDetail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLuuDetail.Visible = false;
            this.btnLuuDetail.Click += new System.EventHandler(this.btnLuuDetail_Click);

            // Cancel button
            this.btnHuyDetail.Text = "✕  Hủy";
            this.btnHuyDetail.BackColor = System.Drawing.Color.FromArgb(180, 180, 180);
            this.btnHuyDetail.ForeColor = System.Drawing.Color.White;
            this.btnHuyDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuyDetail.FlatAppearance.BorderSize = 0;
            this.btnHuyDetail.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnHuyDetail.Size = new System.Drawing.Size(112, 34);
            this.btnHuyDetail.Location = new System.Drawing.Point(146, 415);
            this.btnHuyDetail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHuyDetail.Visible = false;
            this.btnHuyDetail.Click += new System.EventHandler(this.btnHuyDetail_Click);

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
            this.Name = "frmMonhoc";
            this.Text = "Quản Lý Môn Học";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmMonhoc_Load);

            this.pnlSidebar.ResumeLayout(false);
            this.pnlSidebar.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.pnlDetail.ResumeLayout(false);
            this.pnlDetail.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            this.ResumeLayout(false);
        }

        // Helper: style toolbar buttons
        private void StyleButton(System.Windows.Forms.Button btn, string text, System.Drawing.Color color, int x, int y)
        {
            btn.Text = text;
            btn.BackColor = color;
            btn.ForeColor = System.Drawing.Color.White;
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btn.Size = new System.Drawing.Size(90, 30);
            btn.Location = new System.Drawing.Point(x, y);
            btn.Cursor = System.Windows.Forms.Cursors.Hand;
        }

        // Helper: caption + value label pair
        private void SetDetailField(System.Windows.Forms.Label caption, System.Windows.Forms.Label value, string captionText, int y)
        {
            caption.Text = captionText;
            caption.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            caption.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            caption.Location = new System.Drawing.Point(16, y);
            caption.AutoSize = true;

            value.Text = "";
            value.Font = new System.Drawing.Font("Segoe UI", 10F);
            value.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            value.Location = new System.Drawing.Point(16, y + 18);
            value.AutoSize = true;
        }

        // Helper: style edit textbox
        private void StyleEditBox(System.Windows.Forms.TextBox txt, int y)
        {
            txt.Location = new System.Drawing.Point(16, y);
            txt.Size = new System.Drawing.Size(248, 26);
            txt.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            txt.Visible = false;
        }

        #endregion

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
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.Label lblFilterKhoa;
        private System.Windows.Forms.ComboBox cboFilterKhoa;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Panel pnlDetail;
        private System.Windows.Forms.Button btnCloseDetail;
        private System.Windows.Forms.Label lblDetailHeader;
        private System.Windows.Forms.Label lblDetailSep;
        
        private System.Windows.Forms.Label lblMaMonCaption;
        private System.Windows.Forms.Label lblDetailMaMon;
        private System.Windows.Forms.TextBox txtEditMaMon;

        private System.Windows.Forms.Label lblTenMonCaption;
        private System.Windows.Forms.Label lblDetailTenMon;
        private System.Windows.Forms.TextBox txtEditTenMon;

        private System.Windows.Forms.Label lblSoTCCaption;
        private System.Windows.Forms.Label lblDetailSoTC;
        private System.Windows.Forms.TextBox txtEditSoTC;

        private System.Windows.Forms.Label lblKhoaCaption;
        private System.Windows.Forms.Label lblDetailKhoa;
        private System.Windows.Forms.ComboBox cboEditKhoa;

        private System.Windows.Forms.Label lblMoTaCaption;
        private System.Windows.Forms.Label lblDetailMoTa;
        private System.Windows.Forms.TextBox txtEditMoTa;

        private System.Windows.Forms.Label lblTrangThaiCaption;
        private System.Windows.Forms.Label lblDetailTrangThai;
        private System.Windows.Forms.CheckBox chkEditActive;

        private System.Windows.Forms.Button btnLuuDetail;
        private System.Windows.Forms.Button btnHuyDetail;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblTongBanGhi;
    }
}