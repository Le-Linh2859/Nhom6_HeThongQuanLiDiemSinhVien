namespace QLDSV.GUI.Forms.GiangVien
{
    partial class frmThongTinCaNhan_GV
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlScroll = new System.Windows.Forms.Panel();
            this.pnlCardId = new System.Windows.Forms.Panel();
            this.lblSecId = new System.Windows.Forms.Label();
            this.lblBadgeId = new System.Windows.Forms.Label();
            this.lblLineId = new System.Windows.Forms.Panel();
            this.lblMagv = new System.Windows.Forms.Label();
            this.txtMagv = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTenkhoa = new System.Windows.Forms.Label();
            this.txtTenkhoa = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblMatk = new System.Windows.Forms.Label();
            this.txtMatk = new Guna.UI2.WinForms.Guna2TextBox();
            this.pnlCardContact = new System.Windows.Forms.Panel();
            this.rdoNu = new System.Windows.Forms.RadioButton();
            this.lblSecContact = new System.Windows.Forms.Label();
            this.rdoNam = new System.Windows.Forms.RadioButton();
            this.lblBadgeContact = new System.Windows.Forms.Label();
            this.lblLineContact = new System.Windows.Forms.Panel();
            this.lblSdt = new System.Windows.Forms.Label();
            this.txtSdt = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblGioitinh = new System.Windows.Forms.Label();
            this.txtEmail = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTengv = new System.Windows.Forms.Label();
            this.lblDiachi = new System.Windows.Forms.Label();
            this.txtTengv = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtDiachi = new Guna.UI2.WinForms.Guna2TextBox();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            this.btnCapnhat = new Guna.UI2.WinForms.Guna2Button();
            this.pnlScroll.SuspendLayout();
            this.pnlCardId.SuspendLayout();
            this.pnlCardContact.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlScroll
            //
            this.pnlScroll.AutoScroll = true;
            this.pnlScroll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.pnlScroll.Controls.Add(this.pnlCardId);
            this.pnlScroll.Controls.Add(this.pnlCardContact);
            this.pnlScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlScroll.Location = new System.Drawing.Point(0, 0);
            this.pnlScroll.Name = "pnlScroll";
            this.pnlScroll.Padding = new System.Windows.Forms.Padding(32, 24, 32, 24);
            this.pnlScroll.Size = new System.Drawing.Size(1100, 721);
            this.pnlScroll.TabIndex = 0;
            //
            // pnlCardId
            //
            this.pnlCardId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCardId.BackColor = System.Drawing.Color.White;
            this.pnlCardId.Controls.Add(this.lblSecId);
            this.pnlCardId.Controls.Add(this.lblBadgeId);
            this.pnlCardId.Controls.Add(this.lblLineId);
            this.pnlCardId.Controls.Add(this.lblMagv);
            this.pnlCardId.Controls.Add(this.txtMagv);
            this.pnlCardId.Controls.Add(this.lblTenkhoa);
            this.pnlCardId.Controls.Add(this.txtTenkhoa);
            this.pnlCardId.Controls.Add(this.lblMatk);
            this.pnlCardId.Controls.Add(this.txtMatk);
            this.pnlCardId.Location = new System.Drawing.Point(22, 12);
            this.pnlCardId.Name = "pnlCardId";
            this.pnlCardId.Size = new System.Drawing.Size(1931, 326);
            this.pnlCardId.TabIndex = 1;
            //
            // lblSecId
            //
            this.lblSecId.AutoSize = true;
            this.lblSecId.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblSecId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.lblSecId.Location = new System.Drawing.Point(24, 16);
            this.lblSecId.Name = "lblSecId";
            this.lblSecId.Size = new System.Drawing.Size(167, 23);
            this.lblSecId.TabIndex = 0;
            this.lblSecId.Text = "Thông tin định danh";
            //
            // lblBadgeId
            //
            this.lblBadgeId.AutoSize = true;
            this.lblBadgeId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(242)))), ((int)(((byte)(255)))));
            this.lblBadgeId.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblBadgeId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            this.lblBadgeId.Location = new System.Drawing.Point(229, 16);
            this.lblBadgeId.Name = "lblBadgeId";
            this.lblBadgeId.Padding = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.lblBadgeId.Size = new System.Drawing.Size(71, 25);
            this.lblBadgeId.TabIndex = 1;
            this.lblBadgeId.Text = "Chỉ đọc";
            //
            // lblLineId
            //
            this.lblLineId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.lblLineId.Location = new System.Drawing.Point(24, 46);
            this.lblLineId.Name = "lblLineId";
            this.lblLineId.Size = new System.Drawing.Size(952, 1);
            this.lblLineId.TabIndex = 2;
            //
            // lblMagv
            //
            this.lblMagv.AutoSize = true;
            this.lblMagv.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblMagv.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(130)))));
            this.lblMagv.Location = new System.Drawing.Point(24, 60);
            this.lblMagv.Name = "lblMagv";
            this.lblMagv.Size = new System.Drawing.Size(103, 20);
            this.lblMagv.TabIndex = 3;
            this.lblMagv.Text = "Mã giảng viên";
            //
            // txtMagv
            //
            this.txtMagv.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this.txtMagv.BorderRadius = 8;
            this.txtMagv.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMagv.DefaultText = "";
            this.txtMagv.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.txtMagv.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMagv.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(120)))));
            this.txtMagv.Location = new System.Drawing.Point(24, 80);
            this.txtMagv.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtMagv.Name = "txtMagv";
            this.txtMagv.PlaceholderText = "";
            this.txtMagv.ReadOnly = true;
            this.txtMagv.SelectedText = "";
            this.txtMagv.Size = new System.Drawing.Size(300, 36);
            this.txtMagv.TabIndex = 0;
            //
            // lblTenkhoa
            //
            this.lblTenkhoa.AutoSize = true;
            this.lblTenkhoa.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTenkhoa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(130)))));
            this.lblTenkhoa.Location = new System.Drawing.Point(27, 205);
            this.lblTenkhoa.Name = "lblTenkhoa";
            this.lblTenkhoa.Size = new System.Drawing.Size(43, 20);
            this.lblTenkhoa.TabIndex = 6;
            this.lblTenkhoa.Text = "Khoa";
            //
            // txtTenkhoa
            //
            this.txtTenkhoa.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this.txtTenkhoa.BorderRadius = 8;
            this.txtTenkhoa.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenkhoa.DefaultText = "";
            this.txtTenkhoa.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.txtTenkhoa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTenkhoa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(120)))));
            this.txtTenkhoa.Location = new System.Drawing.Point(24, 230);
            this.txtTenkhoa.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtTenkhoa.Name = "txtTenkhoa";
            this.txtTenkhoa.PlaceholderText = "";
            this.txtTenkhoa.ReadOnly = true;
            this.txtTenkhoa.SelectedText = "";
            this.txtTenkhoa.Size = new System.Drawing.Size(300, 36);
            this.txtTenkhoa.TabIndex = 3;
            //
            // lblMatk
            //
            this.lblMatk.AutoSize = true;
            this.lblMatk.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblMatk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(130)))));
            this.lblMatk.Location = new System.Drawing.Point(24, 130);
            this.lblMatk.Name = "lblMatk";
            this.lblMatk.Size = new System.Drawing.Size(95, 20);
            this.lblMatk.TabIndex = 7;
            this.lblMatk.Text = "Mã tài khoản";
            //
            // txtMatk
            //
            this.txtMatk.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this.txtMatk.BorderRadius = 8;
            this.txtMatk.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMatk.DefaultText = "";
            this.txtMatk.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.txtMatk.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMatk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(120)))));
            this.txtMatk.Location = new System.Drawing.Point(24, 150);
            this.txtMatk.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtMatk.Name = "txtMatk";
            this.txtMatk.PlaceholderText = "";
            this.txtMatk.ReadOnly = true;
            this.txtMatk.SelectedText = "";
            this.txtMatk.Size = new System.Drawing.Size(300, 36);
            this.txtMatk.TabIndex = 4;
            //
            // pnlCardContact
            //
            this.pnlCardContact.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCardContact.BackColor = System.Drawing.Color.White;
            this.pnlCardContact.Controls.Add(this.rdoNu);
            this.pnlCardContact.Controls.Add(this.lblSecContact);
            this.pnlCardContact.Controls.Add(this.rdoNam);
            this.pnlCardContact.Controls.Add(this.lblBadgeContact);
            this.pnlCardContact.Controls.Add(this.lblLineContact);
            this.pnlCardContact.Controls.Add(this.lblSdt);
            this.pnlCardContact.Controls.Add(this.txtSdt);
            this.pnlCardContact.Controls.Add(this.lblEmail);
            this.pnlCardContact.Controls.Add(this.lblGioitinh);
            this.pnlCardContact.Controls.Add(this.txtEmail);
            this.pnlCardContact.Controls.Add(this.lblTengv);
            this.pnlCardContact.Controls.Add(this.lblDiachi);
            this.pnlCardContact.Controls.Add(this.txtTengv);
            this.pnlCardContact.Controls.Add(this.txtDiachi);
            this.pnlCardContact.Location = new System.Drawing.Point(22, 344);
            this.pnlCardContact.Name = "pnlCardContact";
            this.pnlCardContact.Size = new System.Drawing.Size(1611, 371);
            this.pnlCardContact.TabIndex = 2;
            //
            // rdoNu
            //
            this.rdoNu.AutoSize = true;
            this.rdoNu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rdoNu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(50)))));
            this.rdoNu.Location = new System.Drawing.Point(537, 86);
            this.rdoNu.Name = "rdoNu";
            this.rdoNu.Size = new System.Drawing.Size(54, 27);
            this.rdoNu.TabIndex = 9;
            this.rdoNu.TabStop = true;
            this.rdoNu.Text = "Nữ";
            this.rdoNu.UseVisualStyleBackColor = true;
            //
            // lblSecContact
            //
            this.lblSecContact.AutoSize = true;
            this.lblSecContact.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblSecContact.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.lblSecContact.Location = new System.Drawing.Point(24, 16);
            this.lblSecContact.Name = "lblSecContact";
            this.lblSecContact.Size = new System.Drawing.Size(142, 23);
            this.lblSecContact.TabIndex = 0;
            this.lblSecContact.Text = "Thông tin liên lạc";
            //
            // rdoNam
            //
            this.rdoNam.AutoSize = true;
            this.rdoNam.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rdoNam.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(50)))));
            this.rdoNam.Location = new System.Drawing.Point(394, 86);
            this.rdoNam.Name = "rdoNam";
            this.rdoNam.Size = new System.Drawing.Size(68, 27);
            this.rdoNam.TabIndex = 8;
            this.rdoNam.TabStop = true;
            this.rdoNam.Text = "Nam";
            this.rdoNam.UseVisualStyleBackColor = true;
            //
            // lblBadgeContact
            //
            this.lblBadgeContact.AutoSize = true;
            this.lblBadgeContact.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(250)))), ((int)(((byte)(229)))));
            this.lblBadgeContact.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblBadgeContact.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(122)))), ((int)(((byte)(85)))));
            this.lblBadgeContact.Location = new System.Drawing.Point(229, 14);
            this.lblBadgeContact.Name = "lblBadgeContact";
            this.lblBadgeContact.Padding = new System.Windows.Forms.Padding(8, 3, 8, 3);
            this.lblBadgeContact.Size = new System.Drawing.Size(128, 25);
            this.lblBadgeContact.TabIndex = 1;
            this.lblBadgeContact.Text = "Có thể chỉnh sửa";
            //
            // lblLineContact
            //
            this.lblLineContact.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(248)))));
            this.lblLineContact.Location = new System.Drawing.Point(24, 46);
            this.lblLineContact.Name = "lblLineContact";
            this.lblLineContact.Size = new System.Drawing.Size(952, 1);
            this.lblLineContact.TabIndex = 2;
            //
            // lblSdt
            //
            this.lblSdt.AutoSize = true;
            this.lblSdt.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblSdt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(130)))));
            this.lblSdt.Location = new System.Drawing.Point(15, 155);
            this.lblSdt.Name = "lblSdt";
            this.lblSdt.Size = new System.Drawing.Size(107, 20);
            this.lblSdt.TabIndex = 3;
            this.lblSdt.Text = "Số điện thoại *";
            //
            // txtSdt
            //
            this.txtSdt.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(215)))));
            this.txtSdt.BorderRadius = 8;
            this.txtSdt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSdt.DefaultText = "";
            this.txtSdt.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            this.txtSdt.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSdt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(50)))));
            this.txtSdt.Location = new System.Drawing.Point(15, 175);
            this.txtSdt.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtSdt.Name = "txtSdt";
            this.txtSdt.PlaceholderText = "";
            this.txtSdt.SelectedText = "";
            this.txtSdt.Size = new System.Drawing.Size(300, 36);
            this.txtSdt.TabIndex = 5;
            //
            // lblEmail
            //
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(130)))));
            this.lblEmail.Location = new System.Drawing.Point(351, 155);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(56, 20);
            this.lblEmail.TabIndex = 6;
            this.lblEmail.Text = "Email *";
            //
            // lblGioitinh
            //
            this.lblGioitinh.AutoSize = true;
            this.lblGioitinh.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblGioitinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(130)))));
            this.lblGioitinh.Location = new System.Drawing.Point(390, 60);
            this.lblGioitinh.Name = "lblGioitinh";
            this.lblGioitinh.Size = new System.Drawing.Size(65, 20);
            this.lblGioitinh.TabIndex = 5;
            this.lblGioitinh.Text = "Giới tính";
            //
            // txtEmail
            //
            this.txtEmail.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(215)))));
            this.txtEmail.BorderRadius = 8;
            this.txtEmail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtEmail.DefaultText = "";
            this.txtEmail.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(50)))));
            this.txtEmail.Location = new System.Drawing.Point(351, 175);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PlaceholderText = "";
            this.txtEmail.SelectedText = "";
            this.txtEmail.Size = new System.Drawing.Size(300, 36);
            this.txtEmail.TabIndex = 6;
            //
            // lblTengv
            //
            this.lblTengv.AutoSize = true;
            this.lblTengv.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblTengv.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(130)))));
            this.lblTengv.Location = new System.Drawing.Point(20, 61);
            this.lblTengv.Name = "lblTengv";
            this.lblTengv.Size = new System.Drawing.Size(73, 20);
            this.lblTengv.TabIndex = 4;
            this.lblTengv.Text = "Họ và tên";
            //
            // lblDiachi
            //
            this.lblDiachi.AutoSize = true;
            this.lblDiachi.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblDiachi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(130)))));
            this.lblDiachi.Location = new System.Drawing.Point(15, 229);
            this.lblDiachi.Name = "lblDiachi";
            this.lblDiachi.Size = new System.Drawing.Size(65, 20);
            this.lblDiachi.TabIndex = 7;
            this.lblDiachi.Text = "Địa chỉ *";
            //
            // txtTengv
            //
            this.txtTengv.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this.txtTengv.BorderRadius = 8;
            this.txtTengv.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTengv.DefaultText = "";
            this.txtTengv.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.txtTengv.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTengv.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(120)))));
            this.txtTengv.Location = new System.Drawing.Point(15, 86);
            this.txtTengv.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtTengv.Name = "txtTengv";
            this.txtTengv.PlaceholderText = "";
            this.txtTengv.SelectedText = "";
            this.txtTengv.Size = new System.Drawing.Size(300, 36);
            this.txtTengv.TabIndex = 1;
            //
            // txtDiachi
            //
            this.txtDiachi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDiachi.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(215)))));
            this.txtDiachi.BorderRadius = 8;
            this.txtDiachi.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDiachi.DefaultText = "";
            this.txtDiachi.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(102)))), ((int)(((byte)(241)))));
            this.txtDiachi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDiachi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(50)))));
            this.txtDiachi.Location = new System.Drawing.Point(15, 254);
            this.txtDiachi.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtDiachi.Name = "txtDiachi";
            this.txtDiachi.PlaceholderText = "";
            this.txtDiachi.SelectedText = "";
            this.txtDiachi.Size = new System.Drawing.Size(692, 36);
            this.txtDiachi.TabIndex = 7;
            //
            // pnlFooter
            //
            this.pnlFooter.BackColor = System.Drawing.Color.White;
            this.pnlFooter.Controls.Add(this.btnHuy);
            this.pnlFooter.Controls.Add(this.btnCapnhat);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 721);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(1100, 64);
            this.pnlFooter.TabIndex = 1;
            //
            // btnHuy
            //
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHuy.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(210)))), ((int)(((byte)(220)))));
            this.btnHuy.BorderRadius = 8;
            this.btnHuy.BorderThickness = 1;
            this.btnHuy.FillColor = System.Drawing.Color.White;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnHuy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this.btnHuy.Location = new System.Drawing.Point(730, 12);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(110, 40);
            this.btnHuy.TabIndex = 8;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            //
            // btnCapnhat
            //
            this.btnCapnhat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCapnhat.BorderRadius = 8;
            this.btnCapnhat.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(70)))), ((int)(((byte)(229)))));
            this.btnCapnhat.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnCapnhat.ForeColor = System.Drawing.Color.White;
            this.btnCapnhat.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(56)))), ((int)(((byte)(202)))));
            this.btnCapnhat.Location = new System.Drawing.Point(856, 12);
            this.btnCapnhat.Name = "btnCapnhat";
            this.btnCapnhat.Size = new System.Drawing.Size(180, 45);
            this.btnCapnhat.TabIndex = 9;
            this.btnCapnhat.Text = "Cập nhật";
            this.btnCapnhat.Click += new System.EventHandler(this.btnCapnhat_Click);
            //
            // frmThongTinCaNhan_GV
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1100, 785);
            this.Controls.Add(this.pnlScroll);
            this.Controls.Add(this.pnlFooter);
            this.MinimumSize = new System.Drawing.Size(900, 650);
            this.Name = "frmThongTinCaNhan_GV";
            this.Text = "Thông tin cá nhân";
            this.Load += new System.EventHandler(this.frmThongTinCaNhan_GV_Load);
            this.pnlScroll.ResumeLayout(false);
            this.pnlCardId.ResumeLayout(false);
            this.pnlCardId.PerformLayout();
            this.pnlCardContact.ResumeLayout(false);
            this.pnlCardContact.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        // ===========================
        // Khai báo controls
        // ===========================
        private System.Windows.Forms.Panel pnlScroll;

        private System.Windows.Forms.Panel pnlCardId;
        private System.Windows.Forms.Label lblSecId;
        private System.Windows.Forms.Label lblBadgeId;
        private System.Windows.Forms.Panel lblLineId;
        private System.Windows.Forms.Label lblMagv;
        private Guna.UI2.WinForms.Guna2TextBox txtMagv;
        private System.Windows.Forms.Label lblTengv;
        private Guna.UI2.WinForms.Guna2TextBox txtTengv;
        private System.Windows.Forms.Label lblGioitinh;
        private System.Windows.Forms.Label lblTenkhoa;
        private Guna.UI2.WinForms.Guna2TextBox txtTenkhoa;
        private System.Windows.Forms.Label lblMatk;
        private Guna.UI2.WinForms.Guna2TextBox txtMatk;

        private System.Windows.Forms.Panel pnlCardContact;
        private System.Windows.Forms.Label lblSecContact;
        private System.Windows.Forms.Label lblBadgeContact;
        private System.Windows.Forms.Panel lblLineContact;
        private System.Windows.Forms.Label lblSdt;
        private Guna.UI2.WinForms.Guna2TextBox txtSdt;
        private System.Windows.Forms.Label lblEmail;
        private Guna.UI2.WinForms.Guna2TextBox txtEmail;
        private System.Windows.Forms.Label lblDiachi;
        private Guna.UI2.WinForms.Guna2TextBox txtDiachi;

        private System.Windows.Forms.Panel pnlFooter;
        private Guna.UI2.WinForms.Guna2Button btnHuy;
        private Guna.UI2.WinForms.Guna2Button btnCapnhat;
        private System.Windows.Forms.RadioButton rdoNu;
        private System.Windows.Forms.RadioButton rdoNam;
    }
}