namespace QLDSV.GUI
{
    partial class frmDangNhap
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
            this.pnlLeft = new Guna.UI2.WinForms.Guna2Panel();
            this.lblIcon = new System.Windows.Forms.Label();
            this.lblTitleLine1 = new System.Windows.Forms.Label();
            this.lblTitleLine2 = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblLoginTitle = new System.Windows.Forms.Label();
            this.lblLoginDesc = new System.Windows.Forms.Label();
            this.lblTenDN = new System.Windows.Forms.Label();
            this.lblMK = new System.Windows.Forms.Label();
            this.txtTaikhoan = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtMatkhau = new Guna.UI2.WinForms.Guna2TextBox();
            this.chkGhiNho = new Guna.UI2.WinForms.Guna2CheckBox();
            this.btnDangnhap = new Guna.UI2.WinForms.Guna2Button();
            this.btnThoat = new Guna.UI2.WinForms.Guna2Button();
            this.lblFooter = new System.Windows.Forms.Label();
            this.pnlLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(206)))));
            this.pnlLeft.Controls.Add(this.lblIcon);
            this.pnlLeft.Controls.Add(this.lblTitleLine1);
            this.pnlLeft.Controls.Add(this.lblTitleLine2);
            this.pnlLeft.Controls.Add(this.lblSubtitle);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(350, 530);
            this.pnlLeft.TabIndex = 0;
            // 
            // lblIcon
            // 
            this.lblIcon.Font = new System.Drawing.Font("Segoe UI", 48F);
            this.lblIcon.ForeColor = System.Drawing.Color.White;
            this.lblIcon.Location = new System.Drawing.Point(0, 100);
            this.lblIcon.Name = "lblIcon";
            this.lblIcon.Size = new System.Drawing.Size(350, 90);
            this.lblIcon.TabIndex = 0;
            this.lblIcon.Text = "\uD83C\uDF93";
            this.lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleLine1
            // 
            this.lblTitleLine1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitleLine1.ForeColor = System.Drawing.Color.White;
            this.lblTitleLine1.Location = new System.Drawing.Point(0, 200);
            this.lblTitleLine1.Name = "lblTitleLine1";
            this.lblTitleLine1.Size = new System.Drawing.Size(350, 40);
            this.lblTitleLine1.TabIndex = 1;
            this.lblTitleLine1.Text = "H\u1EC6 TH\u1ED0NG";
            this.lblTitleLine1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTitleLine2
            // 
            this.lblTitleLine2.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitleLine2.ForeColor = System.Drawing.Color.White;
            this.lblTitleLine2.Location = new System.Drawing.Point(0, 240);
            this.lblTitleLine2.Name = "lblTitleLine2";
            this.lblTitleLine2.Size = new System.Drawing.Size(350, 50);
            this.lblTitleLine2.TabIndex = 2;
            this.lblTitleLine2.Text = "QU\u1EA2N L\u00CD \u0110I\u1EC2M";
            this.lblTitleLine2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Italic);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(220)))), ((int)(((byte)(240)))));
            this.lblSubtitle.Location = new System.Drawing.Point(0, 300);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(350, 30);
            this.lblSubtitle.TabIndex = 3;
            this.lblSubtitle.Text = "Student Management System";
            this.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLoginTitle
            // 
            this.lblLoginTitle.AutoSize = true;
            this.lblLoginTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline);
            this.lblLoginTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.lblLoginTitle.Location = new System.Drawing.Point(430, 45);
            this.lblLoginTitle.Name = "lblLoginTitle";
            this.lblLoginTitle.Size = new System.Drawing.Size(200, 46);
            this.lblLoginTitle.TabIndex = 1;
            this.lblLoginTitle.Text = "\u0110\u0102NG NH\u1EACP";
            // 
            // lblLoginDesc
            // 
            this.lblLoginDesc.AutoSize = true;
            this.lblLoginDesc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLoginDesc.ForeColor = System.Drawing.Color.Gray;
            this.lblLoginDesc.Location = new System.Drawing.Point(433, 95);
            this.lblLoginDesc.Name = "lblLoginDesc";
            this.lblLoginDesc.Size = new System.Drawing.Size(280, 20);
            this.lblLoginDesc.TabIndex = 2;
            this.lblLoginDesc.Text = "Vui l\u00F2ng nh\u1EADp th\u00F4ng tin t\u00E0i kho\u1EA3n c\u1EE7a b\u1EA1n";
            // 
            // lblTenDN
            // 
            this.lblTenDN.AutoSize = true;
            this.lblTenDN.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTenDN.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblTenDN.Location = new System.Drawing.Point(433, 135);
            this.lblTenDN.Name = "lblTenDN";
            this.lblTenDN.Size = new System.Drawing.Size(120, 23);
            this.lblTenDN.TabIndex = 3;
            this.lblTenDN.Text = "T\u00EAn \u0111\u0103ng nh\u1EADp";
            // 
            // txtTaikhoan
            // 
            this.txtTaikhoan.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.txtTaikhoan.BorderRadius = 8;
            this.txtTaikhoan.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTaikhoan.DefaultText = "";
            this.txtTaikhoan.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTaikhoan.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTaikhoan.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTaikhoan.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTaikhoan.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(206)))));
            this.txtTaikhoan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTaikhoan.ForeColor = System.Drawing.Color.Black;
            this.txtTaikhoan.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(206)))));
            this.txtTaikhoan.Location = new System.Drawing.Point(433, 162);
            this.txtTaikhoan.Margin = new System.Windows.Forms.Padding(4);
            this.txtTaikhoan.Name = "txtTaikhoan";
            this.txtTaikhoan.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.txtTaikhoan.PlaceholderText = "Nh\u1EADp t\u00EAn \u0111\u0103ng nh\u1EADp...";
            this.txtTaikhoan.SelectedText = "";
            this.txtTaikhoan.Size = new System.Drawing.Size(390, 42);
            this.txtTaikhoan.TabIndex = 4;
            // 
            // lblMK
            // 
            this.lblMK.AutoSize = true;
            this.lblMK.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMK.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.lblMK.Location = new System.Drawing.Point(433, 218);
            this.lblMK.Name = "lblMK";
            this.lblMK.Size = new System.Drawing.Size(85, 23);
            this.lblMK.TabIndex = 5;
            this.lblMK.Text = "M\u1EADt kh\u1EA9u";
            // 
            // txtMatkhau
            // 
            this.txtMatkhau.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.txtMatkhau.BorderRadius = 8;
            this.txtMatkhau.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMatkhau.DefaultText = "";
            this.txtMatkhau.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMatkhau.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtMatkhau.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMatkhau.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMatkhau.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(206)))));
            this.txtMatkhau.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMatkhau.ForeColor = System.Drawing.Color.Black;
            this.txtMatkhau.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(206)))));
            this.txtMatkhau.Location = new System.Drawing.Point(433, 245);
            this.txtMatkhau.Margin = new System.Windows.Forms.Padding(4);
            this.txtMatkhau.Name = "txtMatkhau";
            this.txtMatkhau.PasswordChar = '\u25CF';
            this.txtMatkhau.PlaceholderForeColor = System.Drawing.Color.Silver;
            this.txtMatkhau.PlaceholderText = "Nh\u1EADp m\u1EADt kh\u1EA9u...";
            this.txtMatkhau.SelectedText = "";
            this.txtMatkhau.Size = new System.Drawing.Size(390, 42);
            this.txtMatkhau.TabIndex = 6;
            this.txtMatkhau.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMatkhau_KeyDown);
            // 
            // chkGhiNho
            // 
            this.chkGhiNho.AutoSize = true;
            this.chkGhiNho.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(206)))));
            this.chkGhiNho.CheckedState.BorderRadius = 3;
            this.chkGhiNho.CheckedState.BorderThickness = 0;
            this.chkGhiNho.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(206)))));
            this.chkGhiNho.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkGhiNho.ForeColor = System.Drawing.Color.Gray;
            this.chkGhiNho.Location = new System.Drawing.Point(433, 300);
            this.chkGhiNho.Name = "chkGhiNho";
            this.chkGhiNho.Size = new System.Drawing.Size(160, 23);
            this.chkGhiNho.TabIndex = 7;
            this.chkGhiNho.Text = "Ghi nh\u1EDB \u0111\u0103ng nh\u1EADp";
            this.chkGhiNho.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.chkGhiNho.UncheckedState.BorderRadius = 3;
            this.chkGhiNho.UncheckedState.BorderThickness = 1;
            this.chkGhiNho.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            // 
            // btnDangnhap
            // 
            this.btnDangnhap.BorderRadius = 8;
            this.btnDangnhap.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnDangnhap.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnDangnhap.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnDangnhap.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnDangnhap.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(206)))));
            this.btnDangnhap.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnDangnhap.ForeColor = System.Drawing.Color.White;
            this.btnDangnhap.Location = new System.Drawing.Point(433, 345);
            this.btnDangnhap.Name = "btnDangnhap";
            this.btnDangnhap.Size = new System.Drawing.Size(390, 45);
            this.btnDangnhap.TabIndex = 8;
            this.btnDangnhap.Text = "\u0110\u0102NG NH\u1EACP";
            this.btnDangnhap.Click += new System.EventHandler(this.btnDangnhap_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnThoat.BorderRadius = 8;
            this.btnThoat.BorderThickness = 1;
            this.btnThoat.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThoat.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThoat.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThoat.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThoat.FillColor = System.Drawing.Color.White;
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnThoat.ForeColor = System.Drawing.Color.Gray;
            this.btnThoat.Location = new System.Drawing.Point(433, 400);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(390, 40);
            this.btnThoat.TabIndex = 9;
            this.btnThoat.Text = "Tho\u00E1t";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // lblFooter
            // 
            this.lblFooter.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblFooter.ForeColor = System.Drawing.Color.Silver;
            this.lblFooter.Location = new System.Drawing.Point(350, 490);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(550, 20);
            this.lblFooter.TabIndex = 10;
            this.lblFooter.Text = "v1.0.0 \u00A9 2026 QuanLyDiem";
            this.lblFooter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmDangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(900, 530);
            this.Controls.Add(this.lblFooter);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnDangnhap);
            this.Controls.Add(this.chkGhiNho);
            this.Controls.Add(this.txtMatkhau);
            this.Controls.Add(this.lblMK);
            this.Controls.Add(this.txtTaikhoan);
            this.Controls.Add(this.lblTenDN);
            this.Controls.Add(this.lblLoginDesc);
            this.Controls.Add(this.lblLoginTitle);
            this.Controls.Add(this.pnlLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmDangNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "H\u1EC6 TH\u1ED0NG QU\u1EA2N L\u00CD \u0110I\u1EC2M - \u0110\u0103ng nh\u1EADp";
            this.pnlLeft.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlLeft;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Label lblTitleLine1;
        private System.Windows.Forms.Label lblTitleLine2;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblLoginTitle;
        private System.Windows.Forms.Label lblLoginDesc;
        private System.Windows.Forms.Label lblTenDN;
        private System.Windows.Forms.Label lblMK;
        private Guna.UI2.WinForms.Guna2TextBox txtTaikhoan;
        private Guna.UI2.WinForms.Guna2TextBox txtMatkhau;
        private Guna.UI2.WinForms.Guna2CheckBox chkGhiNho;
        private Guna.UI2.WinForms.Guna2Button btnDangnhap;
        private Guna.UI2.WinForms.Guna2Button btnThoat;
        private System.Windows.Forms.Label lblFooter;
    }
}
