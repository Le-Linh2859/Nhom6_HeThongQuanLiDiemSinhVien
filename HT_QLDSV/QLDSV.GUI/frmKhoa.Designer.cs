//namespace QLDSV.GUI
//{
//    partial class frmKhoa
//    {
//        private System.ComponentModel.IContainer components = null;

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//                components.Dispose();
//            base.Dispose(disposing);
//        }

//        #region Windows Form Designer generated code

//        private void InitializeComponent()
//        {
//            this.pnlSidebar = new Guna.UI2.WinForms.Guna2Panel();
//            this.label1 = new System.Windows.Forms.Label();
//            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
//            this.guna2Button7 = new Guna.UI2.WinForms.Guna2Button();
//            this.guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
//            this.pnlHeader = new System.Windows.Forms.Panel();
//            this.lblTitle = new System.Windows.Forms.Label();
//            this.pnlContent = new System.Windows.Forms.Panel();
//            this.pnlToolbar = new System.Windows.Forms.Panel();
//            this.btnThem = new System.Windows.Forms.Button();
//            this.btnSua = new System.Windows.Forms.Button();
//            this.btnXoa = new System.Windows.Forms.Button();
//            this.btnLamMoi = new System.Windows.Forms.Button();
//            this.txtTimKiem = new System.Windows.Forms.TextBox();
//            this.btnTim = new System.Windows.Forms.Button();
//            this.pnlMain = new System.Windows.Forms.Panel();
//            this.dataGridView = new System.Windows.Forms.DataGridView();
//            this.pnlDetail = new System.Windows.Forms.Panel();
//            this.btnCloseDetail = new System.Windows.Forms.Button();
//            this.lblDetailHeader = new System.Windows.Forms.Label();
//            this.lblDetailSep = new System.Windows.Forms.Label();
//            this.lblMaKhoaCaption = new System.Windows.Forms.Label();
//            this.lblDetailMaKhoa = new System.Windows.Forms.Label();
//            this.lblTenKhoaCaption = new System.Windows.Forms.Label();
//            this.lblDetailTenKhoa = new System.Windows.Forms.Label();
//            this.lblNamTLCaption = new System.Windows.Forms.Label();
//            this.lblDetailNamTL = new System.Windows.Forms.Label();
//            this.lblMoTaCaption = new System.Windows.Forms.Label();
//            this.lblDetailMoTa = new System.Windows.Forms.Label();
//            this.txtEditMaKhoa = new System.Windows.Forms.TextBox();
//            this.txtEditTenKhoa = new System.Windows.Forms.TextBox();
//            this.txtEditNamTL = new System.Windows.Forms.TextBox();
//            this.txtEditMoTa = new System.Windows.Forms.TextBox();
//            this.btnLuuDetail = new System.Windows.Forms.Button();
//            this.btnHuyDetail = new System.Windows.Forms.Button();
//            this.pnlFooter = new System.Windows.Forms.Panel();
//            this.lblTongBanGhi = new System.Windows.Forms.Label();
//            this.pnlSidebar.SuspendLayout();
//            this.pnlHeader.SuspendLayout();
//            this.pnlContent.SuspendLayout();
//            this.pnlToolbar.SuspendLayout();
//            this.pnlMain.SuspendLayout();
//            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
//            this.pnlDetail.SuspendLayout();
//            this.pnlFooter.SuspendLayout();
//            this.SuspendLayout();

//            // ── Sidebar ──────────────────────────────────────────────
//            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(21, 101, 192);
//            this.pnlSidebar.BorderRadius = 5;
//            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
//            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
//            this.pnlSidebar.Name = "pnlSidebar";
//            this.pnlSidebar.Size = new System.Drawing.Size(196, 700);
//            this.pnlSidebar.TabIndex = 0;
//            this.pnlSidebar.Controls.Add(this.label1);
//            this.pnlSidebar.Controls.Add(this.guna2HtmlLabel2);
//            this.pnlSidebar.Controls.Add(this.guna2HtmlLabel3);
//            this.pnlSidebar.Controls.Add(this.guna2Button7);

//            this.label1.AutoSize = true;
//            this.label1.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Bold);
//            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
//            this.label1.Location = new System.Drawing.Point(28, 17);
//            this.label1.Name = "label1";
//            this.label1.Text = "QUẢN LÝ ĐÀO TẠO";

//            this.guna2HtmlLabel2.AutoSize = false;
//            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
//            this.guna2HtmlLabel2.Location = new System.Drawing.Point(9, 34);
//            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
//            this.guna2HtmlLabel2.Size = new System.Drawing.Size(175, 17);
//            this.guna2HtmlLabel2.Text = "---------------------------------------------------------------";

//            this.guna2HtmlLabel3.AutoSize = false;
//            this.guna2HtmlLabel3.BackColor = System.Drawing.Color.Transparent;
//            this.guna2HtmlLabel3.Location = new System.Drawing.Point(11, 647);
//            this.guna2HtmlLabel3.Name = "guna2HtmlLabel3";
//            this.guna2HtmlLabel3.Size = new System.Drawing.Size(175, 17);
//            this.guna2HtmlLabel3.Text = "---------------------------------------------------------------";

//            this.guna2Button7.FillColor = System.Drawing.Color.Transparent;
//            this.guna2Button7.Font = new System.Drawing.Font("Segoe UI", 9F);
//            this.guna2Button7.ForeColor = System.Drawing.Color.White;
//            this.guna2Button7.Location = new System.Drawing.Point(7, 659);
//            this.guna2Button7.Name = "guna2Button7";
//            this.guna2Button7.Size = new System.Drawing.Size(160, 36);
//            this.guna2Button7.Text = "Đăng xuất";
//            this.guna2Button7.TextOffset = new System.Drawing.Point(5, 0);

//            // ── Header ───────────────────────────────────────────────
//            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(21, 101, 192);
//            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
//            this.pnlHeader.Height = 50;
//            this.pnlHeader.Controls.Add(this.lblTitle);

//            this.lblTitle.AutoSize = true;
//            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
//            this.lblTitle.ForeColor = System.Drawing.Color.White;
//            this.lblTitle.Location = new System.Drawing.Point(16, 12);
//            this.lblTitle.Text = "🏫  QUẢN LÝ KHOA";

//            // ── Content ──────────────────────────────────────────────
//            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.pnlContent.BackColor = System.Drawing.Color.White;
//            this.pnlContent.Controls.Add(this.pnlMain);
//            this.pnlContent.Controls.Add(this.pnlToolbar);
//            this.pnlContent.Controls.Add(this.pnlFooter);

//            // ── Toolbar ──────────────────────────────────────────────
//            this.pnlToolbar.BackColor = System.Drawing.Color.White;
//            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
//            this.pnlToolbar.Height = 55;
//            this.pnlToolbar.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
//            this.pnlToolbar.Controls.Add(this.btnThem);
//            this.pnlToolbar.Controls.Add(this.btnSua);
//            this.pnlToolbar.Controls.Add(this.btnXoa);
//            this.pnlToolbar.Controls.Add(this.btnLamMoi);
//            this.pnlToolbar.Controls.Add(this.txtTimKiem);
//            this.pnlToolbar.Controls.Add(this.btnTim);

//            StyleButton(this.btnThem, "+ Thêm", System.Drawing.Color.FromArgb(21, 101, 192), 10, 12);
//            StyleButton(this.btnSua, "✎ Sửa", System.Drawing.Color.FromArgb(33, 150, 243), 110, 12);
//            StyleButton(this.btnXoa, "🗑 Xóa", System.Drawing.Color.FromArgb(211, 47, 47), 210, 12);
//            StyleButton(this.btnLamMoi, "↺ Làm mới", System.Drawing.Color.FromArgb(56, 142, 60), 310, 12);

//            this.txtTimKiem.Location = new System.Drawing.Point(430, 13);
//            this.txtTimKiem.Size = new System.Drawing.Size(200, 26);
//            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F);
//            this.txtTimKiem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTimKiem_KeyDown);

//            StyleButton(this.btnTim, "Tìm", System.Drawing.Color.FromArgb(21, 101, 192), 638, 12);
//            this.btnTim.Width = 70;

//            // ── Main area (grid + detail) ─────────────────────────────
//            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.pnlMain.BackColor = System.Drawing.Color.White;
//            this.pnlMain.Controls.Add(this.pnlDetail);
//            this.pnlMain.Controls.Add(this.dataGridView);

//            // ── DataGridView ─────────────────────────────────────────
//            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.dataGridView.BackgroundColor = System.Drawing.Color.White;
//            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
//            this.dataGridView.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(21, 101, 192);
//            this.dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
//            this.dataGridView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
//            this.dataGridView.ColumnHeadersHeight = 36;
//            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
//            this.dataGridView.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
//            this.dataGridView.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(187, 222, 251);
//            this.dataGridView.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
//            this.dataGridView.RowHeadersVisible = false;
//            this.dataGridView.GridColor = System.Drawing.Color.FromArgb(224, 224, 224);
//            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
//            this.dataGridView.Name = "dataGridView";
//            this.dataGridView.TabIndex = 0;
//            this.dataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_CellClick);

//            // ── Detail Panel (right sidebar) ──────────────────────────
//            this.pnlDetail.Dock = System.Windows.Forms.DockStyle.Right;
//            this.pnlDetail.Width = 0;
//            this.pnlDetail.BackColor = System.Drawing.Color.FromArgb(245, 248, 255);
//            this.pnlDetail.Padding = new System.Windows.Forms.Padding(16);
//            this.pnlDetail.Name = "pnlDetail";
//            this.pnlDetail.Controls.Add(this.btnCloseDetail);
//            this.pnlDetail.Controls.Add(this.lblDetailHeader);
//            this.pnlDetail.Controls.Add(this.lblDetailSep);
//            this.pnlDetail.Controls.Add(this.lblMaKhoaCaption);
//            this.pnlDetail.Controls.Add(this.lblDetailMaKhoa);
//            this.pnlDetail.Controls.Add(this.lblTenKhoaCaption);
//            this.pnlDetail.Controls.Add(this.lblDetailTenKhoa);
//            this.pnlDetail.Controls.Add(this.lblNamTLCaption);
//            this.pnlDetail.Controls.Add(this.lblDetailNamTL);
//            this.pnlDetail.Controls.Add(this.lblMoTaCaption);
//            this.pnlDetail.Controls.Add(this.lblDetailMoTa);
//            this.pnlDetail.Controls.Add(this.txtEditMaKhoa);
//            this.pnlDetail.Controls.Add(this.txtEditTenKhoa);
//            this.pnlDetail.Controls.Add(this.txtEditNamTL);
//            this.pnlDetail.Controls.Add(this.txtEditMoTa);
//            this.pnlDetail.Controls.Add(this.btnLuuDetail);
//            this.pnlDetail.Controls.Add(this.btnHuyDetail);

//            // Close button
//            this.btnCloseDetail.Text = "✕";
//            this.btnCloseDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//            this.btnCloseDetail.FlatAppearance.BorderSize = 0;
//            this.btnCloseDetail.BackColor = System.Drawing.Color.Transparent;
//            this.btnCloseDetail.ForeColor = System.Drawing.Color.Gray;
//            this.btnCloseDetail.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
//            this.btnCloseDetail.Size = new System.Drawing.Size(30, 30);
//            this.btnCloseDetail.Location = new System.Drawing.Point(238, 10);
//            this.btnCloseDetail.Cursor = System.Windows.Forms.Cursors.Hand;
//            this.btnCloseDetail.Click += new System.EventHandler(this.btnCloseDetail_Click);

//            // Header label
//            this.lblDetailHeader.Text = "Chi tiết Khoa";
//            this.lblDetailHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
//            this.lblDetailHeader.ForeColor = System.Drawing.Color.FromArgb(21, 101, 192);
//            this.lblDetailHeader.Location = new System.Drawing.Point(16, 14);
//            this.lblDetailHeader.AutoSize = true;

//            // Separator
//            this.lblDetailSep.Text = "";
//            this.lblDetailSep.BackColor = System.Drawing.Color.FromArgb(21, 101, 192);
//            this.lblDetailSep.Location = new System.Drawing.Point(16, 44);
//            this.lblDetailSep.Size = new System.Drawing.Size(248, 2);

//            // View-mode Fields (Labels)
//            SetDetailField(this.lblMaKhoaCaption, this.lblDetailMaKhoa, "Mã Khoa", 60);
//            SetDetailField(this.lblTenKhoaCaption, this.lblDetailTenKhoa, "Tên Khoa", 110);
//            SetDetailField(this.lblNamTLCaption, this.lblDetailNamTL, "Năm thành lập", 160);
//            SetDetailField(this.lblMoTaCaption, this.lblDetailMoTa, "Mô tả", 210);
//            this.lblDetailMoTa.MaximumSize = new System.Drawing.Size(240, 0);
//            this.lblDetailMoTa.AutoSize = true;

//            // Edit-mode TextBoxes (hidden by default)
//            StyleEditBox(this.txtEditMaKhoa, 78);
//            StyleEditBox(this.txtEditTenKhoa, 128);
//            StyleEditBox(this.txtEditNamTL, 178);
//            this.txtEditMoTa.Location = new System.Drawing.Point(16, 228);
//            this.txtEditMoTa.Size = new System.Drawing.Size(248, 70);
//            this.txtEditMoTa.Font = new System.Drawing.Font("Segoe UI", 9.5F);
//            this.txtEditMoTa.Multiline = true;
//            this.txtEditMoTa.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
//            this.txtEditMoTa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
//            this.txtEditMoTa.Visible = false;

//            // Save button
//            this.btnLuuDetail.Text = "💾  Lưu";
//            this.btnLuuDetail.BackColor = System.Drawing.Color.FromArgb(21, 101, 192);
//            this.btnLuuDetail.ForeColor = System.Drawing.Color.White;
//            this.btnLuuDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//            this.btnLuuDetail.FlatAppearance.BorderSize = 0;
//            this.btnLuuDetail.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
//            this.btnLuuDetail.Size = new System.Drawing.Size(118, 34);
//            this.btnLuuDetail.Location = new System.Drawing.Point(16, 315);
//            this.btnLuuDetail.Cursor = System.Windows.Forms.Cursors.Hand;
//            this.btnLuuDetail.Visible = false;
//            this.btnLuuDetail.Click += new System.EventHandler(this.btnLuuDetail_Click);

//            // Cancel button
//            this.btnHuyDetail.Text = "✕  Hủy";
//            this.btnHuyDetail.BackColor = System.Drawing.Color.FromArgb(180, 180, 180);
//            this.btnHuyDetail.ForeColor = System.Drawing.Color.White;
//            this.btnHuyDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//            this.btnHuyDetail.FlatAppearance.BorderSize = 0;
//            this.btnHuyDetail.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
//            this.btnHuyDetail.Size = new System.Drawing.Size(112, 34);
//            this.btnHuyDetail.Location = new System.Drawing.Point(146, 315);
//            this.btnHuyDetail.Cursor = System.Windows.Forms.Cursors.Hand;
//            this.btnHuyDetail.Visible = false;
//            this.btnHuyDetail.Click += new System.EventHandler(this.btnHuyDetail_Click);

//            // ── Footer ───────────────────────────────────────────────
//            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
//            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
//            this.pnlFooter.Height = 30;
//            this.pnlFooter.Controls.Add(this.lblTongBanGhi);

//            this.lblTongBanGhi.AutoSize = true;
//            this.lblTongBanGhi.Font = new System.Drawing.Font("Segoe UI", 9F);
//            this.lblTongBanGhi.ForeColor = System.Drawing.Color.FromArgb(80, 80, 80);
//            this.lblTongBanGhi.Location = new System.Drawing.Point(10, 7);
//            this.lblTongBanGhi.Text = "Tổng: 0 bản ghi";

//            // ── Form ─────────────────────────────────────────────────
//            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.ClientSize = new System.Drawing.Size(1100, 700);
//            this.Controls.Add(this.pnlContent);
//            this.Controls.Add(this.pnlHeader);
//            this.Controls.Add(this.pnlSidebar);
//            this.Name = "frmKhoa";
//            this.Text = "Quản Lý Khoa";
//            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
//            this.Load += new System.EventHandler(this.frmKhoa_Load);

//            this.pnlSidebar.ResumeLayout(false);
//            this.pnlSidebar.PerformLayout();
//            this.pnlHeader.ResumeLayout(false);
//            this.pnlHeader.PerformLayout();
//            this.pnlContent.ResumeLayout(false);
//            this.pnlToolbar.ResumeLayout(false);
//            this.pnlToolbar.PerformLayout();
//            this.pnlMain.ResumeLayout(false);
//            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
//            this.pnlDetail.ResumeLayout(false);
//            this.pnlDetail.PerformLayout();
//            this.pnlFooter.ResumeLayout(false);
//            this.pnlFooter.PerformLayout();
//            this.ResumeLayout(false);
//        }

//        // Helper: style toolbar buttons
//        private void StyleButton(System.Windows.Forms.Button btn, string text, System.Drawing.Color color, int x, int y)
//        {
//            btn.Text = text;
//            btn.BackColor = color;
//            btn.ForeColor = System.Drawing.Color.White;
//            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//            btn.FlatAppearance.BorderSize = 0;
//            btn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
//            btn.Size = new System.Drawing.Size(90, 30);
//            btn.Location = new System.Drawing.Point(x, y);
//            btn.Cursor = System.Windows.Forms.Cursors.Hand;
//        }

//        // Helper: caption + value label pair
//        private void SetDetailField(System.Windows.Forms.Label caption, System.Windows.Forms.Label value, string captionText, int y)
//        {
//            caption.Text = captionText;
//            caption.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
//            caption.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
//            caption.Location = new System.Drawing.Point(16, y);
//            caption.AutoSize = true;

//            value.Text = "";
//            value.Font = new System.Drawing.Font("Segoe UI", 10F);
//            value.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
//            value.Location = new System.Drawing.Point(16, y + 18);
//            value.AutoSize = true;
//        }

//        // Helper: style edit textbox
//        private void StyleEditBox(System.Windows.Forms.TextBox txt, int y)
//        {
//            txt.Location = new System.Drawing.Point(16, y);
//            txt.Size = new System.Drawing.Size(248, 26);
//            txt.Font = new System.Drawing.Font("Segoe UI", 9.5F);
//            txt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
//            txt.Visible = false;
//        }

//        #endregion

//        // Controls
//        private Guna.UI2.WinForms.Guna2Panel pnlSidebar;
//        private System.Windows.Forms.Label label1;
//        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
//        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
//        private Guna.UI2.WinForms.Guna2Button guna2Button7;
//        private System.Windows.Forms.Panel pnlHeader;
//        private System.Windows.Forms.Label lblTitle;
//        private System.Windows.Forms.Panel pnlContent;
//        private System.Windows.Forms.Panel pnlToolbar;
//        private System.Windows.Forms.Button btnThem;
//        private System.Windows.Forms.Button btnSua;
//        private System.Windows.Forms.Button btnXoa;
//        private System.Windows.Forms.Button btnLamMoi;
//        private System.Windows.Forms.TextBox txtTimKiem;
//        private System.Windows.Forms.Button btnTim;
//        private System.Windows.Forms.Panel pnlMain;
//        private System.Windows.Forms.DataGridView dataGridView;
//        private System.Windows.Forms.Panel pnlDetail;
//        private System.Windows.Forms.Button btnCloseDetail;
//        private System.Windows.Forms.Label lblDetailHeader;
//        private System.Windows.Forms.Label lblDetailSep;
//        private System.Windows.Forms.Label lblMaKhoaCaption;
//        private System.Windows.Forms.Label lblDetailMaKhoa;
//        private System.Windows.Forms.Label lblTenKhoaCaption;
//        private System.Windows.Forms.Label lblDetailTenKhoa;
//        private System.Windows.Forms.Label lblNamTLCaption;
//        private System.Windows.Forms.Label lblDetailNamTL;
//        private System.Windows.Forms.Label lblMoTaCaption;
//        private System.Windows.Forms.Label lblDetailMoTa;
//        private System.Windows.Forms.TextBox txtEditMaKhoa;
//        private System.Windows.Forms.TextBox txtEditTenKhoa;
//        private System.Windows.Forms.TextBox txtEditNamTL;
//        private System.Windows.Forms.TextBox txtEditMoTa;
//        private System.Windows.Forms.Button btnLuuDetail;
//        private System.Windows.Forms.Button btnHuyDetail;
//        private System.Windows.Forms.Panel pnlFooter;
//        private System.Windows.Forms.Label lblTongBanGhi;
//    }
//}
