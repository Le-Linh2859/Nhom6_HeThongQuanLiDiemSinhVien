using System.Windows.Forms;

namespace QLDSV.GUI
{
    partial class frmGiangvien
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            this.lblTenlop = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblMalop = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.cboDiaChi = new Guna.UI2.WinForms.Guna2TextBox();
            this.cboMaGV = new Guna.UI2.WinForms.Guna2TextBox();
            this.cboTenGV = new Guna.UI2.WinForms.Guna2TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboMatKhau1 = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblNhapLaiMK = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblMatKhau = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.mskSoDienThoai = new System.Windows.Forms.MaskedTextBox();
            this.cboEmail = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2HtmlLabel5 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.cboMatKhau = new Guna.UI2.WinForms.Guna2TextBox();
            this.rdoNu = new System.Windows.Forms.RadioButton();
            this.rdoNam = new System.Windows.Forms.RadioButton();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnReset = new Guna.UI2.WinForms.Guna2Button();
            this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
            this.lblNienkhoa = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblKhoa = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnSua = new Guna.UI2.WinForms.Guna2Button();
            this.lblCovan = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btnThem = new Guna.UI2.WinForms.Guna2Button();
            this.cboKhoa2 = new Guna.UI2.WinForms.Guna2ComboBox();
            this.txtTimKiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2HtmlLabel15 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.DataGridViewGV = new Guna.UI2.WinForms.Guna2DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pnlThongKeGV = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSumGV = new System.Windows.Forms.Label();
            this.guna2HtmlLabel4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.cboKhoa = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnLammoi = new Guna.UI2.WinForms.Guna2Button();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.guna2HtmlLabel3 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.groupBox1.SuspendLayout();
            this.guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewGV)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.pnlThongKeGV.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnHuy
            // 
            this.btnHuy.BorderRadius = 2;
            this.btnHuy.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnHuy.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnHuy.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnHuy.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnHuy.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnHuy.Location = new System.Drawing.Point(917, 285);
            this.btnHuy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(104, 47);
            this.btnHuy.TabIndex = 73;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.TextOffset = new System.Drawing.Point(1, 0);
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // lblTenlop
            // 
            this.lblTenlop.BackColor = System.Drawing.Color.Transparent;
            this.lblTenlop.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenlop.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTenlop.Location = new System.Drawing.Point(22, 99);
            this.lblTenlop.Margin = new System.Windows.Forms.Padding(1);
            this.lblTenlop.Name = "lblTenlop";
            this.lblTenlop.Size = new System.Drawing.Size(107, 23);
            this.lblTenlop.TabIndex = 62;
            this.lblTenlop.Text = "Tên giảng viên: ";
            // 
            // lblMalop
            // 
            this.lblMalop.BackColor = System.Drawing.Color.Transparent;
            this.lblMalop.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMalop.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblMalop.Location = new System.Drawing.Point(22, 49);
            this.lblMalop.Margin = new System.Windows.Forms.Padding(1);
            this.lblMalop.Name = "lblMalop";
            this.lblMalop.Size = new System.Drawing.Size(104, 23);
            this.lblMalop.TabIndex = 61;
            this.lblMalop.Text = "Mã giảng viên: ";
            // 
            // cboDiaChi
            // 
            this.cboDiaChi.BorderRadius = 2;
            this.cboDiaChi.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.cboDiaChi.DefaultText = "";
            this.cboDiaChi.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.cboDiaChi.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.cboDiaChi.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.cboDiaChi.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.cboDiaChi.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboDiaChi.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDiaChi.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboDiaChi.IconRightOffset = new System.Drawing.Point(5, 0);
            this.cboDiaChi.IconRightSize = new System.Drawing.Size(13, 13);
            this.cboDiaChi.Location = new System.Drawing.Point(957, 24);
            this.cboDiaChi.Margin = new System.Windows.Forms.Padding(4);
            this.cboDiaChi.Name = "cboDiaChi";
            this.cboDiaChi.PlaceholderText = "";
            this.cboDiaChi.SelectedText = "";
            this.cboDiaChi.Size = new System.Drawing.Size(557, 36);
            this.cboDiaChi.TabIndex = 77;
            // 
            // cboMaGV
            // 
            this.cboMaGV.BorderRadius = 2;
            this.cboMaGV.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.cboMaGV.DefaultText = "";
            this.cboMaGV.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.cboMaGV.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.cboMaGV.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.cboMaGV.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.cboMaGV.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboMaGV.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMaGV.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboMaGV.IconRightOffset = new System.Drawing.Point(5, 0);
            this.cboMaGV.IconRightSize = new System.Drawing.Size(13, 13);
            this.cboMaGV.Location = new System.Drawing.Point(167, 36);
            this.cboMaGV.Margin = new System.Windows.Forms.Padding(4);
            this.cboMaGV.Name = "cboMaGV";
            this.cboMaGV.PlaceholderText = "";
            this.cboMaGV.SelectedText = "";
            this.cboMaGV.Size = new System.Drawing.Size(454, 36);
            this.cboMaGV.TabIndex = 76;
            // 
            // cboTenGV
            // 
            this.cboTenGV.BorderRadius = 2;
            this.cboTenGV.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.cboTenGV.DefaultText = "";
            this.cboTenGV.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.cboTenGV.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.cboTenGV.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.cboTenGV.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.cboTenGV.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboTenGV.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTenGV.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboTenGV.IconRightOffset = new System.Drawing.Point(5, 0);
            this.cboTenGV.IconRightSize = new System.Drawing.Size(13, 13);
            this.cboTenGV.Location = new System.Drawing.Point(167, 87);
            this.cboTenGV.Margin = new System.Windows.Forms.Padding(4);
            this.cboTenGV.Name = "cboTenGV";
            this.cboTenGV.PlaceholderText = "";
            this.cboTenGV.SelectedText = "";
            this.cboTenGV.Size = new System.Drawing.Size(454, 36);
            this.cboTenGV.TabIndex = 75;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.cboMatKhau1);
            this.groupBox1.Controls.Add(this.lblNhapLaiMK);
            this.groupBox1.Controls.Add(this.lblMatKhau);
            this.groupBox1.Controls.Add(this.mskSoDienThoai);
            this.groupBox1.Controls.Add(this.cboEmail);
            this.groupBox1.Controls.Add(this.guna2HtmlLabel5);
            this.groupBox1.Controls.Add(this.cboMatKhau);
            this.groupBox1.Controls.Add(this.rdoNu);
            this.groupBox1.Controls.Add(this.rdoNam);
            this.groupBox1.Controls.Add(this.guna2HtmlLabel1);
            this.groupBox1.Controls.Add(this.cboDiaChi);
            this.groupBox1.Controls.Add(this.cboMaGV);
            this.groupBox1.Controls.Add(this.cboTenGV);
            this.groupBox1.Controls.Add(this.btnReset);
            this.groupBox1.Controls.Add(this.lblMalop);
            this.groupBox1.Controls.Add(this.btnHuy);
            this.groupBox1.Controls.Add(this.lblTenlop);
            this.groupBox1.Controls.Add(this.btnLuu);
            this.groupBox1.Controls.Add(this.lblNienkhoa);
            this.groupBox1.Controls.Add(this.lblKhoa);
            this.groupBox1.Controls.Add(this.btnSua);
            this.groupBox1.Controls.Add(this.lblCovan);
            this.groupBox1.Controls.Add(this.btnThem);
            this.groupBox1.Controls.Add(this.cboKhoa2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 13F, System.Drawing.FontStyle.Bold);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(0, 397);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(1772, 346);
            this.groupBox1.TabIndex = 63;
            this.groupBox1.TabStop = false;
            // 
            // cboMatKhau1
            // 
            this.cboMatKhau1.BorderRadius = 2;
            this.cboMatKhau1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.cboMatKhau1.DefaultText = "";
            this.cboMatKhau1.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.cboMatKhau1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.cboMatKhau1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.cboMatKhau1.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.cboMatKhau1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboMatKhau1.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMatKhau1.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboMatKhau1.IconRightOffset = new System.Drawing.Point(5, 0);
            this.cboMatKhau1.IconRightSize = new System.Drawing.Size(13, 13);
            this.cboMatKhau1.Location = new System.Drawing.Point(1357, 193);
            this.cboMatKhau1.Margin = new System.Windows.Forms.Padding(4);
            this.cboMatKhau1.Name = "cboMatKhau1";
            this.cboMatKhau1.PlaceholderText = "";
            this.cboMatKhau1.SelectedText = "";
            this.cboMatKhau1.Size = new System.Drawing.Size(159, 36);
            this.cboMatKhau1.TabIndex = 86;
            // 
            // lblNhapLaiMK
            // 
            this.lblNhapLaiMK.BackColor = System.Drawing.Color.Transparent;
            this.lblNhapLaiMK.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNhapLaiMK.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblNhapLaiMK.Location = new System.Drawing.Point(1225, 198);
            this.lblNhapLaiMK.Margin = new System.Windows.Forms.Padding(1);
            this.lblNhapLaiMK.Name = "lblNhapLaiMK";
            this.lblNhapLaiMK.Size = new System.Drawing.Size(127, 22);
            this.lblNhapLaiMK.TabIndex = 85;
            this.lblNhapLaiMK.Text = "Nhập lại mật khẩu:";
            // 
            // lblMatKhau
            // 
            this.lblMatKhau.BackColor = System.Drawing.Color.Transparent;
            this.lblMatKhau.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatKhau.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblMatKhau.Location = new System.Drawing.Point(803, 207);
            this.lblMatKhau.Margin = new System.Windows.Forms.Padding(1);
            this.lblMatKhau.Name = "lblMatKhau";
            this.lblMatKhau.Size = new System.Drawing.Size(67, 22);
            this.lblMatKhau.TabIndex = 83;
            this.lblMatKhau.Text = "Mật khẩu:";
            // 
            // mskSoDienThoai
            // 
            this.mskSoDienThoai.Location = new System.Drawing.Point(957, 87);
            this.mskSoDienThoai.Mask = "(999) 000-0000";
            this.mskSoDienThoai.Name = "mskSoDienThoai";
            this.mskSoDienThoai.Size = new System.Drawing.Size(557, 36);
            this.mskSoDienThoai.TabIndex = 87;
            // 
            // cboEmail
            // 
            this.cboEmail.BorderRadius = 2;
            this.cboEmail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.cboEmail.DefaultText = "";
            this.cboEmail.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.cboEmail.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.cboEmail.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.cboEmail.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.cboEmail.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboEmail.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboEmail.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboEmail.IconRightOffset = new System.Drawing.Point(5, 0);
            this.cboEmail.IconRightSize = new System.Drawing.Size(13, 13);
            this.cboEmail.Location = new System.Drawing.Point(167, 198);
            this.cboEmail.Margin = new System.Windows.Forms.Padding(4);
            this.cboEmail.Name = "cboEmail";
            this.cboEmail.PlaceholderText = "";
            this.cboEmail.SelectedText = "";
            this.cboEmail.Size = new System.Drawing.Size(454, 36);
            this.cboEmail.TabIndex = 82;
            // 
            // guna2HtmlLabel5
            // 
            this.guna2HtmlLabel5.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.guna2HtmlLabel5.Location = new System.Drawing.Point(22, 210);
            this.guna2HtmlLabel5.Margin = new System.Windows.Forms.Padding(1);
            this.guna2HtmlLabel5.Name = "guna2HtmlLabel5";
            this.guna2HtmlLabel5.Size = new System.Drawing.Size(44, 23);
            this.guna2HtmlLabel5.TabIndex = 81;
            this.guna2HtmlLabel5.Text = "Email:";
            // 
            // cboMatKhau
            // 
            this.cboMatKhau.BorderRadius = 2;
            this.cboMatKhau.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.cboMatKhau.DefaultText = "";
            this.cboMatKhau.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.cboMatKhau.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.cboMatKhau.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.cboMatKhau.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.cboMatKhau.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboMatKhau.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMatKhau.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboMatKhau.IconRightOffset = new System.Drawing.Point(5, 0);
            this.cboMatKhau.IconRightSize = new System.Drawing.Size(13, 13);
            this.cboMatKhau.Location = new System.Drawing.Point(960, 193);
            this.cboMatKhau.Margin = new System.Windows.Forms.Padding(4);
            this.cboMatKhau.Name = "cboMatKhau";
            this.cboMatKhau.PlaceholderText = "";
            this.cboMatKhau.SelectedText = "";
            this.cboMatKhau.Size = new System.Drawing.Size(238, 36);
            this.cboMatKhau.TabIndex = 84;
            // 
            // rdoNu
            // 
            this.rdoNu.AutoSize = true;
            this.rdoNu.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoNu.Location = new System.Drawing.Point(260, 148);
            this.rdoNu.Name = "rdoNu";
            this.rdoNu.Size = new System.Drawing.Size(53, 24);
            this.rdoNu.TabIndex = 80;
            this.rdoNu.TabStop = true;
            this.rdoNu.Text = "Nữ";
            this.rdoNu.UseVisualStyleBackColor = true;
            // 
            // rdoNam
            // 
            this.rdoNam.AutoSize = true;
            this.rdoNam.Font = new System.Drawing.Font("Times New Roman", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoNam.Location = new System.Drawing.Point(167, 148);
            this.rdoNam.Name = "rdoNam";
            this.rdoNam.Size = new System.Drawing.Size(65, 24);
            this.rdoNam.TabIndex = 79;
            this.rdoNam.TabStop = true;
            this.rdoNam.Text = "Nam";
            this.rdoNam.UseVisualStyleBackColor = true;
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(814, 140);
            this.guna2HtmlLabel1.Margin = new System.Windows.Forms.Padding(1);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(38, 23);
            this.guna2HtmlLabel1.TabIndex = 78;
            this.guna2HtmlLabel1.Text = "Khoa";
            // 
            // btnReset
            // 
            this.btnReset.BorderRadius = 2;
            this.btnReset.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnReset.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnReset.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnReset.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnReset.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.btnReset.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(6)))));
            this.btnReset.Location = new System.Drawing.Point(1132, 285);
            this.btnReset.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(112, 47);
            this.btnReset.TabIndex = 74;
            this.btnReset.Text = "Reset";
            this.btnReset.TextOffset = new System.Drawing.Point(1, 0);
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.BorderRadius = 2;
            this.btnLuu.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLuu.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLuu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLuu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLuu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.btnLuu.Location = new System.Drawing.Point(717, 285);
            this.btnLuu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(109, 47);
            this.btnLuu.TabIndex = 72;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.TextOffset = new System.Drawing.Point(1, 0);
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // lblNienkhoa
            // 
            this.lblNienkhoa.BackColor = System.Drawing.Color.Transparent;
            this.lblNienkhoa.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNienkhoa.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblNienkhoa.Location = new System.Drawing.Point(22, 152);
            this.lblNienkhoa.Margin = new System.Windows.Forms.Padding(1);
            this.lblNienkhoa.Name = "lblNienkhoa";
            this.lblNienkhoa.Size = new System.Drawing.Size(66, 23);
            this.lblNienkhoa.TabIndex = 63;
            this.lblNienkhoa.Text = "Giới tính:";
            // 
            // lblKhoa
            // 
            this.lblKhoa.BackColor = System.Drawing.Color.Transparent;
            this.lblKhoa.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKhoa.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblKhoa.Location = new System.Drawing.Point(814, 37);
            this.lblKhoa.Margin = new System.Windows.Forms.Padding(1);
            this.lblKhoa.Name = "lblKhoa";
            this.lblKhoa.Size = new System.Drawing.Size(53, 23);
            this.lblKhoa.TabIndex = 64;
            this.lblKhoa.Text = "Địa chỉ:";
            // 
            // btnSua
            // 
            this.btnSua.BorderRadius = 2;
            this.btnSua.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSua.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSua.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSua.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSua.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnSua.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnSua.Location = new System.Drawing.Point(533, 285);
            this.btnSua.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(97, 47);
            this.btnSua.TabIndex = 70;
            this.btnSua.Text = "Sửa";
            this.btnSua.TextOffset = new System.Drawing.Point(1, 0);
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // lblCovan
            // 
            this.lblCovan.BackColor = System.Drawing.Color.Transparent;
            this.lblCovan.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCovan.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblCovan.Location = new System.Drawing.Point(814, 87);
            this.lblCovan.Margin = new System.Windows.Forms.Padding(1);
            this.lblCovan.Name = "lblCovan";
            this.lblCovan.Size = new System.Drawing.Size(94, 23);
            this.lblCovan.TabIndex = 66;
            this.lblCovan.Text = "Số điện thoại";
            // 
            // btnThem
            // 
            this.btnThem.BorderRadius = 2;
            this.btnThem.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnThem.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnThem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnThem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnThem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnThem.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.btnThem.ImageSize = new System.Drawing.Size(15, 15);
            this.btnThem.Location = new System.Drawing.Point(346, 285);
            this.btnThem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(117, 47);
            this.btnThem.TabIndex = 69;
            this.btnThem.Text = "Thêm";
            this.btnThem.TextOffset = new System.Drawing.Point(1, 0);
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // cboKhoa2
            // 
            this.cboKhoa2.BackColor = System.Drawing.Color.Transparent;
            this.cboKhoa2.BorderRadius = 2;
            this.cboKhoa2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboKhoa2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhoa2.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboKhoa2.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboKhoa2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboKhoa2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboKhoa2.ItemHeight = 25;
            this.cboKhoa2.Location = new System.Drawing.Point(960, 138);
            this.cboKhoa2.Margin = new System.Windows.Forms.Padding(0);
            this.cboKhoa2.Name = "cboKhoa2";
            this.cboKhoa2.Size = new System.Drawing.Size(554, 31);
            this.cboKhoa2.TabIndex = 65;
            this.cboKhoa2.SelectedIndexChanged += new System.EventHandler(this.cboKhoa2_SelectedIndexChanged);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.BorderRadius = 5;
            this.txtTimKiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimKiem.DefaultText = "Tìm kiếm theo mã, tên giảng viên ...";
            this.txtTimKiem.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTimKiem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTimKiem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimKiem.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimKiem.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimKiem.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiem.IconRightOffset = new System.Drawing.Point(5, 0);
            this.txtTimKiem.IconRightSize = new System.Drawing.Size(13, 13);
            this.txtTimKiem.Location = new System.Drawing.Point(26, 30);
            this.txtTimKiem.Margin = new System.Windows.Forms.Padding(4);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "";
            this.txtTimKiem.SelectedText = "";
            this.txtTimKiem.Size = new System.Drawing.Size(350, 39);
            this.txtTimKiem.TabIndex = 1;
            // 
            // guna2HtmlLabel15
            // 
            this.guna2HtmlLabel15.BackColor = System.Drawing.SystemColors.ControlDark;
            this.guna2HtmlLabel15.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.guna2HtmlLabel15.Location = new System.Drawing.Point(213, 97);
            this.guna2HtmlLabel15.Margin = new System.Windows.Forms.Padding(1);
            this.guna2HtmlLabel15.Name = "guna2HtmlLabel15";
            this.guna2HtmlLabel15.Size = new System.Drawing.Size(3, 2);
            this.guna2HtmlLabel15.TabIndex = 61;
            this.guna2HtmlLabel15.Text = null;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel1.BorderColor = System.Drawing.Color.LightGray;
            this.guna2Panel1.BorderRadius = 12;
            this.guna2Panel1.BorderThickness = 1;
            this.guna2Panel1.Controls.Add(this.DataGridViewGV);
            this.guna2Panel1.Controls.Add(this.groupBox2);
            this.guna2Panel1.CustomBorderColor = System.Drawing.Color.White;
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.FillColor = System.Drawing.Color.White;
            this.guna2Panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Margin = new System.Windows.Forms.Padding(1);
            this.guna2Panel1.MinimumSize = new System.Drawing.Size(1100, 394);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1772, 743);
            this.guna2Panel1.TabIndex = 62;
            // 
            // DataGridViewGV
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.DataGridViewGV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.DataGridViewGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridViewGV.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridViewGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DataGridViewGV.ColumnHeadersHeight = 4;
            this.DataGridViewGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(216)))), ((int)(((byte)(230)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridViewGV.DefaultCellStyle = dataGridViewCellStyle3;
            this.DataGridViewGV.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.DataGridViewGV.Location = new System.Drawing.Point(23, 96);
            this.DataGridViewGV.Margin = new System.Windows.Forms.Padding(1);
            this.DataGridViewGV.Name = "DataGridViewGV";
            this.DataGridViewGV.RowHeadersVisible = false;
            this.DataGridViewGV.RowHeadersWidth = 62;
            this.DataGridViewGV.RowTemplate.Height = 28;
            this.DataGridViewGV.Size = new System.Drawing.Size(1730, 623);
            this.DataGridViewGV.TabIndex = 32;
            this.DataGridViewGV.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.DataGridViewGV.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.DataGridViewGV.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.DataGridViewGV.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.DataGridViewGV.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.DataGridViewGV.ThemeStyle.BackColor = System.Drawing.Color.WhiteSmoke;
            this.DataGridViewGV.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.DataGridViewGV.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.DataGridViewGV.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.DataGridViewGV.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataGridViewGV.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.DataGridViewGV.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.DataGridViewGV.ThemeStyle.HeaderStyle.Height = 4;
            this.DataGridViewGV.ThemeStyle.ReadOnly = false;
            this.DataGridViewGV.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.DataGridViewGV.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.DataGridViewGV.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataGridViewGV.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.DataGridViewGV.ThemeStyle.RowsStyle.Height = 28;
            this.DataGridViewGV.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(216)))), ((int)(((byte)(230)))));
            this.DataGridViewGV.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.DataGridViewGV.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewGV_CellClick);
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.pnlThongKeGV);
            this.groupBox2.Controls.Add(this.guna2HtmlLabel4);
            this.groupBox2.Controls.Add(this.txtTimKiem);
            this.groupBox2.Controls.Add(this.cboKhoa);
            this.groupBox2.Controls.Add(this.btnLammoi);
            this.groupBox2.Location = new System.Drawing.Point(22, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1526, 102);
            this.groupBox2.TabIndex = 35;
            this.groupBox2.TabStop = false;
            // 
            // pnlThongKeGV
            // 
            this.pnlThongKeGV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlThongKeGV.BackColor = System.Drawing.Color.White;
            this.pnlThongKeGV.Controls.Add(this.lblTitle);
            this.pnlThongKeGV.Controls.Add(this.lblSumGV);
            this.pnlThongKeGV.Location = new System.Drawing.Point(1077, 13);
            this.pnlThongKeGV.Name = "pnlThongKeGV";
            this.pnlThongKeGV.Size = new System.Drawing.Size(180, 68);
            this.pnlThongKeGV.TabIndex = 34;
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semilight", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Silver;
            this.lblTitle.Location = new System.Drawing.Point(16, 2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(120, 20);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Tổng số GV";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSumGV
            // 
            this.lblSumGV.BackColor = System.Drawing.Color.Transparent;
            this.lblSumGV.Font = new System.Drawing.Font("Segoe UI Semibold", 22F, System.Drawing.FontStyle.Bold);
            this.lblSumGV.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.lblSumGV.Location = new System.Drawing.Point(3, 22);
            this.lblSumGV.Name = "lblSumGV";
            this.lblSumGV.Size = new System.Drawing.Size(173, 40);
            this.lblSumGV.TabIndex = 1;
            this.lblSumGV.Text = "0";
            this.lblSumGV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // guna2HtmlLabel4
            // 
            this.guna2HtmlLabel4.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel4.ForeColor = System.Drawing.Color.White;
            this.guna2HtmlLabel4.Location = new System.Drawing.Point(436, 19);
            this.guna2HtmlLabel4.Margin = new System.Windows.Forms.Padding(1);
            this.guna2HtmlLabel4.Name = "guna2HtmlLabel4";
            this.guna2HtmlLabel4.Size = new System.Drawing.Size(105, 22);
            this.guna2HtmlLabel4.TabIndex = 27;
            this.guna2HtmlLabel4.Text = "Lọc theo khoa:";
            // 
            // cboKhoa
            // 
            this.cboKhoa.BackColor = System.Drawing.Color.Transparent;
            this.cboKhoa.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboKhoa.DropDownHeight = 50;
            this.cboKhoa.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhoa.DropDownWidth = 350;
            this.cboKhoa.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboKhoa.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboKhoa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboKhoa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboKhoa.IntegralHeight = false;
            this.cboKhoa.ItemHeight = 19;
            this.cboKhoa.Location = new System.Drawing.Point(436, 44);
            this.cboKhoa.Margin = new System.Windows.Forms.Padding(1);
            this.cboKhoa.MaxDropDownItems = 10;
            this.cboKhoa.Name = "cboKhoa";
            this.cboKhoa.Size = new System.Drawing.Size(475, 25);
            this.cboKhoa.TabIndex = 30;
            // 
            // btnLammoi
            // 
            this.btnLammoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLammoi.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnLammoi.BorderRadius = 8;
            this.btnLammoi.BorderThickness = 1;
            this.btnLammoi.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLammoi.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLammoi.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLammoi.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLammoi.FillColor = System.Drawing.Color.RoyalBlue;
            this.btnLammoi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLammoi.ForeColor = System.Drawing.Color.White;
            this.btnLammoi.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(148)))), ((int)(((byte)(136)))));
            this.btnLammoi.ImageSize = new System.Drawing.Size(18, 18);
            this.btnLammoi.Location = new System.Drawing.Point(935, 34);
            this.btnLammoi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLammoi.Name = "btnLammoi";
            this.btnLammoi.Size = new System.Drawing.Size(120, 36);
            this.btnLammoi.TabIndex = 31;
            this.btnLammoi.Text = "Làm mới";
            this.btnLammoi.TextOffset = new System.Drawing.Point(1, 0);
            this.btnLammoi.Click += new System.EventHandler(this.btnLammoi_Click);
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.AutoSize = false;
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(9, 33);
            this.guna2HtmlLabel2.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(175, 17);
            this.guna2HtmlLabel2.TabIndex = 14;
            this.guna2HtmlLabel2.Text = "---------------------------------------------------------------";
            // 
            // guna2HtmlLabel3
            // 
            this.guna2HtmlLabel3.AutoSize = false;
            this.guna2HtmlLabel3.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel3.Location = new System.Drawing.Point(13, 702);
            this.guna2HtmlLabel3.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.guna2HtmlLabel3.Name = "guna2HtmlLabel3";
            this.guna2HtmlLabel3.Size = new System.Drawing.Size(175, 17);
            this.guna2HtmlLabel3.TabIndex = 19;
            this.guna2HtmlLabel3.Text = "---------------------------------------------------------------";
            // 
            // frmGiangvien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1772, 743);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.guna2HtmlLabel15);
            this.Controls.Add(this.guna2Panel1);
            this.MinimumSize = new System.Drawing.Size(1300, 650);
            this.Name = "frmGiangvien";
            this.Text = "Quản lý giảng viên";
            this.Load += new System.EventHandler(this.frmGiangvien_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.guna2Panel1.ResumeLayout(false);
            this.guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewGV)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.pnlThongKeGV.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btnHuy;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTenlop;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblMalop;
        private Guna.UI2.WinForms.Guna2TextBox cboDiaChi;
        private Guna.UI2.WinForms.Guna2TextBox cboMaGV;
        private Guna.UI2.WinForms.Guna2TextBox cboTenGV;
        private System.Windows.Forms.GroupBox groupBox1;
        private Guna.UI2.WinForms.Guna2Button btnReset;
        private Guna.UI2.WinForms.Guna2Button btnLuu;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblNienkhoa;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblKhoa;
        private Guna.UI2.WinForms.Guna2Button btnSua;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCovan;
        private Guna.UI2.WinForms.Guna2Button btnThem;
        private Guna.UI2.WinForms.Guna2ComboBox cboKhoa2;
        private Guna.UI2.WinForms.Guna2TextBox txtTimKiem;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel15;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Guna.UI2.WinForms.Guna2DataGridView DataGridViewGV;
        private Guna.UI2.WinForms.Guna2Button btnLammoi;
        private Guna.UI2.WinForms.Guna2ComboBox cboKhoa;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel4;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private System.Windows.Forms.RadioButton rdoNu;
        private System.Windows.Forms.RadioButton rdoNam;
        private Guna.UI2.WinForms.Guna2TextBox cboEmail;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel5;
        private System.Windows.Forms.MaskedTextBox mskSoDienThoai;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblMatKhau;
        private Guna.UI2.WinForms.Guna2TextBox cboMatKhau;
        private Guna.UI2.WinForms.Guna2TextBox cboMatKhau1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblNhapLaiMK;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel3;
        private Panel pnlThongKeGV;
        private Label lblSumGV;
        private Label lblTitle;
        private GroupBox groupBox2;
    }
}