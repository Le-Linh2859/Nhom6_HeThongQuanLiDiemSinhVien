using System.Drawing;
using System.Windows.Forms;

namespace QLDSV.GUI.Forms.GiangVien
{
    partial class FrmNhapDiemSV
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
            this.pnlFilter = new Guna.UI2.WinForms.Guna2Panel();
            this.cboLopHocPhan = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblFilterLHP = new System.Windows.Forms.Label();
            this.cboHocKy = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblFilterHK = new System.Windows.Forms.Label();
            this.cboNamHoc = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblFilterNH = new System.Windows.Forms.Label();
            this.pnlMiddle = new System.Windows.Forms.TableLayoutPanel();
            this.grpStudents = new System.Windows.Forms.GroupBox();
            this.lstSinhVien = new System.Windows.Forms.ListBox();
            this.grpDetails = new System.Windows.Forms.GroupBox();
            this.btnReset = new Guna.UI2.WinForms.Guna2Button();
            this.Lưu = new Guna.UI2.WinForms.Guna2Button();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            this.btnSua = new Guna.UI2.WinForms.Guna2Button();
            this.btnNhap = new Guna.UI2.WinForms.Guna2Button();
            this.txtDiemCK = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblDiemCK = new System.Windows.Forms.Label();
            this.txtDiemKT2 = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblDiemKT2 = new System.Windows.Forms.Label();
            this.txtDiemKT1 = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblDiemKT1 = new System.Windows.Forms.Label();
            this.txtDiemCC = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblDiemCC = new System.Windows.Forms.Label();
            this.txtHoTen = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.txtMaSV = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblMaSV = new System.Windows.Forms.Label();
            this.dgvDiem = new Guna.UI2.WinForms.Guna2DataGridView();
            this.lblTongSV = new System.Windows.Forms.Label();
            this.pnlFilter.SuspendLayout();
            this.pnlMiddle.SuspendLayout();
            this.grpStudents.SuspendLayout();
            this.grpDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiem)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlFilter
            // 
            this.pnlFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFilter.BorderRadius = 5;
            this.pnlFilter.Controls.Add(this.cboLopHocPhan);
            this.pnlFilter.Controls.Add(this.lblFilterLHP);
            this.pnlFilter.Controls.Add(this.cboHocKy);
            this.pnlFilter.Controls.Add(this.lblFilterHK);
            this.pnlFilter.Controls.Add(this.cboNamHoc);
            this.pnlFilter.Controls.Add(this.lblFilterNH);
            this.pnlFilter.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(200)))), ((int)(((byte)(223)))));
            this.pnlFilter.Location = new System.Drawing.Point(12, 12);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(941, 66);
            this.pnlFilter.TabIndex = 1;
            // 
            // cboLopHocPhan
            // 
            this.cboLopHocPhan.BackColor = System.Drawing.Color.Transparent;
            this.cboLopHocPhan.BorderRadius = 5;
            this.cboLopHocPhan.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLopHocPhan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLopHocPhan.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboLopHocPhan.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboLopHocPhan.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboLopHocPhan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboLopHocPhan.ItemHeight = 30;
            this.cboLopHocPhan.Location = new System.Drawing.Point(595, 14);
            this.cboLopHocPhan.Name = "cboLopHocPhan";
            this.cboLopHocPhan.Size = new System.Drawing.Size(325, 36);
            this.cboLopHocPhan.TabIndex = 5;
            // 
            // lblFilterLHP
            // 
            this.lblFilterLHP.AutoSize = true;
            this.lblFilterLHP.BackColor = System.Drawing.Color.Transparent;
            this.lblFilterLHP.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterLHP.ForeColor = System.Drawing.Color.Black;
            this.lblFilterLHP.Location = new System.Drawing.Point(482, 22);
            this.lblFilterLHP.Name = "lblFilterLHP";
            this.lblFilterLHP.Size = new System.Drawing.Size(108, 21);
            this.lblFilterLHP.TabIndex = 4;
            this.lblFilterLHP.Text = "Lớp học phần:";
            // 
            // cboHocKy
            // 
            this.cboHocKy.BackColor = System.Drawing.Color.Transparent;
            this.cboHocKy.BorderRadius = 5;
            this.cboHocKy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboHocKy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHocKy.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboHocKy.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboHocKy.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboHocKy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboHocKy.ItemHeight = 30;
            this.cboHocKy.Location = new System.Drawing.Point(325, 14);
            this.cboHocKy.Name = "cboHocKy";
            this.cboHocKy.Size = new System.Drawing.Size(140, 36);
            this.cboHocKy.TabIndex = 3;
            // 
            // lblFilterHK
            // 
            this.lblFilterHK.AutoSize = true;
            this.lblFilterHK.BackColor = System.Drawing.Color.Transparent;
            this.lblFilterHK.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterHK.ForeColor = System.Drawing.Color.Black;
            this.lblFilterHK.Location = new System.Drawing.Point(260, 22);
            this.lblFilterHK.Name = "lblFilterHK";
            this.lblFilterHK.Size = new System.Drawing.Size(60, 21);
            this.lblFilterHK.TabIndex = 2;
            this.lblFilterHK.Text = "Học kỳ:";
            // 
            // cboNamHoc
            // 
            this.cboNamHoc.BackColor = System.Drawing.Color.Transparent;
            this.cboNamHoc.BorderRadius = 5;
            this.cboNamHoc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboNamHoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNamHoc.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboNamHoc.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboNamHoc.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboNamHoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboNamHoc.ItemHeight = 30;
            this.cboNamHoc.Location = new System.Drawing.Point(88, 14);
            this.cboNamHoc.Name = "cboNamHoc";
            this.cboNamHoc.Size = new System.Drawing.Size(155, 36);
            this.cboNamHoc.TabIndex = 1;
            // 
            // lblFilterNH
            // 
            this.lblFilterNH.AutoSize = true;
            this.lblFilterNH.BackColor = System.Drawing.Color.Transparent;
            this.lblFilterNH.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterNH.ForeColor = System.Drawing.Color.Black;
            this.lblFilterNH.Location = new System.Drawing.Point(12, 22);
            this.lblFilterNH.Name = "lblFilterNH";
            this.lblFilterNH.Size = new System.Drawing.Size(76, 21);
            this.lblFilterNH.TabIndex = 0;
            this.lblFilterNH.Text = "Năm học:";
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMiddle.ColumnCount = 2;
            this.pnlMiddle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32F));
            this.pnlMiddle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68F));
            this.pnlMiddle.Controls.Add(this.grpStudents, 0, 0);
            this.pnlMiddle.Controls.Add(this.grpDetails, 1, 0);
            this.pnlMiddle.Location = new System.Drawing.Point(12, 84);
            this.pnlMiddle.Name = "pnlMiddle";
            this.pnlMiddle.RowCount = 1;
            this.pnlMiddle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlMiddle.Size = new System.Drawing.Size(941, 229);
            this.pnlMiddle.TabIndex = 2;
            // 
            // grpStudents
            // 
            this.grpStudents.Controls.Add(this.lstSinhVien);
            this.grpStudents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpStudents.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpStudents.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.grpStudents.Location = new System.Drawing.Point(3, 3);
            this.grpStudents.Name = "grpStudents";
            this.grpStudents.Size = new System.Drawing.Size(295, 223);
            this.grpStudents.TabIndex = 0;
            this.grpStudents.TabStop = false;
            this.grpStudents.Text = "Danh sách sinh viên";
            // 
            // lstSinhVien
            // 
            this.lstSinhVien.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstSinhVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstSinhVien.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstSinhVien.FormattingEnabled = true;
            this.lstSinhVien.ItemHeight = 20;
            this.lstSinhVien.Location = new System.Drawing.Point(3, 25);
            this.lstSinhVien.Name = "lstSinhVien";
            this.lstSinhVien.Size = new System.Drawing.Size(289, 195);
            this.lstSinhVien.TabIndex = 0;
            // 
            // grpDetails
            // 
            this.grpDetails.BackColor = System.Drawing.Color.White;
            this.grpDetails.Controls.Add(this.btnReset);
            this.grpDetails.Controls.Add(this.Lưu);
            this.grpDetails.Controls.Add(this.btnHuy);
            this.grpDetails.Controls.Add(this.btnSua);
            this.grpDetails.Controls.Add(this.btnNhap);
            this.grpDetails.Controls.Add(this.txtDiemCK);
            this.grpDetails.Controls.Add(this.lblDiemCK);
            this.grpDetails.Controls.Add(this.txtDiemKT2);
            this.grpDetails.Controls.Add(this.lblDiemKT2);
            this.grpDetails.Controls.Add(this.txtDiemKT1);
            this.grpDetails.Controls.Add(this.lblDiemKT1);
            this.grpDetails.Controls.Add(this.txtDiemCC);
            this.grpDetails.Controls.Add(this.lblDiemCC);
            this.grpDetails.Controls.Add(this.txtHoTen);
            this.grpDetails.Controls.Add(this.lblHoTen);
            this.grpDetails.Controls.Add(this.txtMaSV);
            this.grpDetails.Controls.Add(this.lblMaSV);
            this.grpDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDetails.Font = new System.Drawing.Font("Segoe UI", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDetails.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.grpDetails.Location = new System.Drawing.Point(304, 3);
            this.grpDetails.Name = "grpDetails";
            this.grpDetails.Size = new System.Drawing.Size(634, 223);
            this.grpDetails.TabIndex = 1;
            this.grpDetails.TabStop = false;
            this.grpDetails.Text = "Thông tin nhập điểm";
            // 
            // btnReset
            // 
            this.btnReset.BorderRadius = 5;
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnReset.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnReset.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnReset.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnReset.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(93)))), ((int)(((byte)(117)))));
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(78)))), ((int)(((byte)(104)))));
            this.btnReset.Location = new System.Drawing.Point(521, 172);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(95, 35);
            this.btnReset.TabIndex = 17;
            this.btnReset.Text = "Reset";
            // 
            // Lưu
            // 
            this.Lưu.BorderRadius = 5;
            this.Lưu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Lưu.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Lưu.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Lưu.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Lưu.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Lưu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(93)))), ((int)(((byte)(117)))));
            this.Lưu.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.Lưu.ForeColor = System.Drawing.Color.White;
            this.Lưu.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(78)))), ((int)(((byte)(104)))));
            this.Lưu.Location = new System.Drawing.Point(403, 172);
            this.Lưu.Name = "Lưu";
            this.Lưu.Size = new System.Drawing.Size(95, 35);
            this.Lưu.TabIndex = 16;
            this.Lưu.Text = "Lưu";
            // 
            // btnHuy
            // 
            this.btnHuy.BorderRadius = 5;
            this.btnHuy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHuy.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnHuy.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnHuy.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnHuy.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnHuy.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(93)))), ((int)(((byte)(117)))));
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(78)))), ((int)(((byte)(104)))));
            this.btnHuy.Location = new System.Drawing.Point(280, 172);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(95, 35);
            this.btnHuy.TabIndex = 15;
            this.btnHuy.Text = "Hủy";
            // 
            // btnSua
            // 
            this.btnSua.BorderRadius = 5;
            this.btnSua.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSua.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSua.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSua.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSua.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSua.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(93)))), ((int)(((byte)(117)))));
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(78)))), ((int)(((byte)(104)))));
            this.btnSua.Location = new System.Drawing.Point(146, 172);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(95, 35);
            this.btnSua.TabIndex = 14;
            this.btnSua.Text = "Sửa";
            this.btnSua.Click += new System.EventHandler(this.BtnSua_Click);
            // 
            // btnNhap
            // 
            this.btnNhap.BorderRadius = 5;
            this.btnNhap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNhap.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnNhap.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnNhap.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnNhap.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnNhap.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(93)))), ((int)(((byte)(117)))));
            this.btnNhap.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnNhap.ForeColor = System.Drawing.Color.White;
            this.btnNhap.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(78)))), ((int)(((byte)(104)))));
            this.btnNhap.Location = new System.Drawing.Point(21, 172);
            this.btnNhap.Name = "btnNhap";
            this.btnNhap.Size = new System.Drawing.Size(95, 35);
            this.btnNhap.TabIndex = 12;
            this.btnNhap.Text = "Nhập";
            // 
            // txtDiemCK
            // 
            this.txtDiemCK.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(200)))), ((int)(((byte)(223)))));
            this.txtDiemCK.BorderRadius = 5;
            this.txtDiemCK.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDiemCK.DefaultText = "";
            this.txtDiemCK.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtDiemCK.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtDiemCK.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDiemCK.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDiemCK.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDiemCK.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtDiemCK.ForeColor = System.Drawing.Color.Black;
            this.txtDiemCK.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDiemCK.Location = new System.Drawing.Point(475, 116);
            this.txtDiemCK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDiemCK.Name = "txtDiemCK";
            this.txtDiemCK.PlaceholderText = "";
            this.txtDiemCK.SelectedText = "";
            this.txtDiemCK.Size = new System.Drawing.Size(130, 30);
            this.txtDiemCK.TabIndex = 11;
            // 
            // lblDiemCK
            // 
            this.lblDiemCK.AutoSize = true;
            this.lblDiemCK.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiemCK.ForeColor = System.Drawing.Color.Black;
            this.lblDiemCK.Location = new System.Drawing.Point(365, 121);
            this.lblDiemCK.Name = "lblDiemCK";
            this.lblDiemCK.Size = new System.Drawing.Size(103, 21);
            this.lblDiemCK.TabIndex = 10;
            this.lblDiemCK.Text = "Điểm cuối kỳ:";
            // 
            // txtDiemKT2
            // 
            this.txtDiemKT2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(200)))), ((int)(((byte)(223)))));
            this.txtDiemKT2.BorderRadius = 5;
            this.txtDiemKT2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDiemKT2.DefaultText = "";
            this.txtDiemKT2.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtDiemKT2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtDiemKT2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDiemKT2.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDiemKT2.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDiemKT2.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtDiemKT2.ForeColor = System.Drawing.Color.Black;
            this.txtDiemKT2.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDiemKT2.Location = new System.Drawing.Point(475, 74);
            this.txtDiemKT2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDiemKT2.Name = "txtDiemKT2";
            this.txtDiemKT2.PlaceholderText = "";
            this.txtDiemKT2.SelectedText = "";
            this.txtDiemKT2.Size = new System.Drawing.Size(130, 30);
            this.txtDiemKT2.TabIndex = 9;
            // 
            // lblDiemKT2
            // 
            this.lblDiemKT2.AutoSize = true;
            this.lblDiemKT2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiemKT2.ForeColor = System.Drawing.Color.Black;
            this.lblDiemKT2.Location = new System.Drawing.Point(365, 79);
            this.lblDiemKT2.Name = "lblDiemKT2";
            this.lblDiemKT2.Size = new System.Drawing.Size(84, 21);
            this.lblDiemKT2.TabIndex = 8;
            this.lblDiemKT2.Text = "Kiểm tra 2:";
            // 
            // txtDiemKT1
            // 
            this.txtDiemKT1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(200)))), ((int)(((byte)(223)))));
            this.txtDiemKT1.BorderRadius = 5;
            this.txtDiemKT1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDiemKT1.DefaultText = "";
            this.txtDiemKT1.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtDiemKT1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtDiemKT1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDiemKT1.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDiemKT1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDiemKT1.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtDiemKT1.ForeColor = System.Drawing.Color.Black;
            this.txtDiemKT1.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDiemKT1.Location = new System.Drawing.Point(475, 32);
            this.txtDiemKT1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDiemKT1.Name = "txtDiemKT1";
            this.txtDiemKT1.PlaceholderText = "";
            this.txtDiemKT1.SelectedText = "";
            this.txtDiemKT1.Size = new System.Drawing.Size(130, 30);
            this.txtDiemKT1.TabIndex = 7;
            // 
            // lblDiemKT1
            // 
            this.lblDiemKT1.AutoSize = true;
            this.lblDiemKT1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiemKT1.ForeColor = System.Drawing.Color.Black;
            this.lblDiemKT1.Location = new System.Drawing.Point(365, 37);
            this.lblDiemKT1.Name = "lblDiemKT1";
            this.lblDiemKT1.Size = new System.Drawing.Size(84, 21);
            this.lblDiemKT1.TabIndex = 6;
            this.lblDiemKT1.Text = "Kiểm tra 1:";
            // 
            // txtDiemCC
            // 
            this.txtDiemCC.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(200)))), ((int)(((byte)(223)))));
            this.txtDiemCC.BorderRadius = 5;
            this.txtDiemCC.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDiemCC.DefaultText = "";
            this.txtDiemCC.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtDiemCC.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtDiemCC.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDiemCC.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtDiemCC.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDiemCC.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtDiemCC.ForeColor = System.Drawing.Color.Black;
            this.txtDiemCC.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtDiemCC.Location = new System.Drawing.Point(125, 116);
            this.txtDiemCC.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtDiemCC.Name = "txtDiemCC";
            this.txtDiemCC.PlaceholderText = "";
            this.txtDiemCC.SelectedText = "";
            this.txtDiemCC.Size = new System.Drawing.Size(200, 30);
            this.txtDiemCC.TabIndex = 5;
            // 
            // lblDiemCC
            // 
            this.lblDiemCC.AutoSize = true;
            this.lblDiemCC.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiemCC.ForeColor = System.Drawing.Color.Black;
            this.lblDiemCC.Location = new System.Drawing.Point(15, 121);
            this.lblDiemCC.Name = "lblDiemCC";
            this.lblDiemCC.Size = new System.Drawing.Size(94, 21);
            this.lblDiemCC.TabIndex = 4;
            this.lblDiemCC.Text = "Chuyên cần:";
            // 
            // txtHoTen
            // 
            this.txtHoTen.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(200)))), ((int)(((byte)(223)))));
            this.txtHoTen.BorderRadius = 5;
            this.txtHoTen.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtHoTen.DefaultText = "";
            this.txtHoTen.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtHoTen.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.txtHoTen.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.txtHoTen.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtHoTen.Enabled = false;
            this.txtHoTen.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtHoTen.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtHoTen.ForeColor = System.Drawing.Color.Black;
            this.txtHoTen.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtHoTen.Location = new System.Drawing.Point(125, 74);
            this.txtHoTen.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.PlaceholderText = "";
            this.txtHoTen.ReadOnly = true;
            this.txtHoTen.SelectedText = "";
            this.txtHoTen.Size = new System.Drawing.Size(200, 30);
            this.txtHoTen.TabIndex = 3;
            // 
            // lblHoTen
            // 
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHoTen.ForeColor = System.Drawing.Color.Black;
            this.lblHoTen.Location = new System.Drawing.Point(15, 79);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(102, 21);
            this.lblHoTen.TabIndex = 2;
            this.lblHoTen.Text = "Tên sinh viên:";
            // 
            // txtMaSV
            // 
            this.txtMaSV.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(200)))), ((int)(((byte)(223)))));
            this.txtMaSV.BorderRadius = 5;
            this.txtMaSV.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaSV.DefaultText = "";
            this.txtMaSV.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtMaSV.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.txtMaSV.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.txtMaSV.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtMaSV.Enabled = false;
            this.txtMaSV.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMaSV.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtMaSV.ForeColor = System.Drawing.Color.Black;
            this.txtMaSV.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtMaSV.Location = new System.Drawing.Point(125, 32);
            this.txtMaSV.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMaSV.Name = "txtMaSV";
            this.txtMaSV.PlaceholderText = "";
            this.txtMaSV.ReadOnly = true;
            this.txtMaSV.SelectedText = "";
            this.txtMaSV.Size = new System.Drawing.Size(200, 30);
            this.txtMaSV.TabIndex = 1;
            // 
            // lblMaSV
            // 
            this.lblMaSV.AutoSize = true;
            this.lblMaSV.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaSV.ForeColor = System.Drawing.Color.Black;
            this.lblMaSV.Location = new System.Drawing.Point(15, 37);
            this.lblMaSV.Name = "lblMaSV";
            this.lblMaSV.Size = new System.Drawing.Size(101, 21);
            this.lblMaSV.TabIndex = 0;
            this.lblMaSV.Text = "Mã sinh viên:";
            // 
            // dgvDiem
            // 
            this.dgvDiem.AllowUserToAddRows = false;
            this.dgvDiem.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.dgvDiem.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDiem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDiem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDiem.ColumnHeadersHeight = 40;
            this.dgvDiem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(216)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDiem.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDiem.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDiem.Location = new System.Drawing.Point(12, 319);
            this.dgvDiem.MultiSelect = false;
            this.dgvDiem.Name = "dgvDiem";
            this.dgvDiem.ReadOnly = true;
            this.dgvDiem.RowHeadersVisible = false;
            this.dgvDiem.RowTemplate.Height = 28;
            this.dgvDiem.Size = new System.Drawing.Size(941, 297);
            this.dgvDiem.TabIndex = 3;
            this.dgvDiem.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvDiem.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvDiem.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvDiem.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvDiem.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvDiem.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvDiem.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dgvDiem.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvDiem.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvDiem.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDiem.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvDiem.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            this.dgvDiem.ThemeStyle.HeaderStyle.Height = 40;
            this.dgvDiem.ThemeStyle.ReadOnly = true;
            this.dgvDiem.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvDiem.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvDiem.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvDiem.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvDiem.ThemeStyle.RowsStyle.Height = 28;
            this.dgvDiem.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(216)))), ((int)(((byte)(242)))));
            this.dgvDiem.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            // 
            // lblTongSV
            // 
            this.lblTongSV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTongSV.AutoSize = true;
            this.lblTongSV.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongSV.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblTongSV.Location = new System.Drawing.Point(12, 630);
            this.lblTongSV.Name = "lblTongSV";
            this.lblTongSV.Size = new System.Drawing.Size(226, 20);
            this.lblTongSV.TabIndex = 4;
            this.lblTongSV.Text = "Tổng số sinh viên nhập điểm: 0";
            // 
            // FrmNhapDiemSV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(965, 660);
            this.Controls.Add(this.lblTongSV);
            this.Controls.Add(this.dgvDiem);
            this.Controls.Add(this.pnlMiddle);
            this.Controls.Add(this.pnlFilter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmNhapDiemSV";
            this.Text = "Nhập Điểm Sinh Viên";
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.pnlMiddle.ResumeLayout(false);
            this.grpStudents.ResumeLayout(false);
            this.grpDetails.ResumeLayout(false);
            this.grpDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Panel pnlFilter;
        private Guna.UI2.WinForms.Guna2ComboBox cboLopHocPhan;
        private System.Windows.Forms.Label lblFilterLHP;
        private Guna.UI2.WinForms.Guna2ComboBox cboHocKy;
        private System.Windows.Forms.Label lblFilterHK;
        private Guna.UI2.WinForms.Guna2ComboBox cboNamHoc;
        private System.Windows.Forms.Label lblFilterNH;
        private System.Windows.Forms.TableLayoutPanel pnlMiddle;
        private System.Windows.Forms.GroupBox grpStudents;
        private System.Windows.Forms.ListBox lstSinhVien;
        private System.Windows.Forms.GroupBox grpDetails;
        private Guna.UI2.WinForms.Guna2TextBox txtMaSV;
        private System.Windows.Forms.Label lblMaSV;
        private Guna.UI2.WinForms.Guna2TextBox txtHoTen;
        private System.Windows.Forms.Label lblHoTen;
        private Guna.UI2.WinForms.Guna2TextBox txtDiemCK;
        private System.Windows.Forms.Label lblDiemCK;
        private Guna.UI2.WinForms.Guna2TextBox txtDiemKT2;
        private System.Windows.Forms.Label lblDiemKT2;
        private Guna.UI2.WinForms.Guna2TextBox txtDiemKT1;
        private System.Windows.Forms.Label lblDiemKT1;
        private Guna.UI2.WinForms.Guna2TextBox txtDiemCC;
        private System.Windows.Forms.Label lblDiemCC;
        private Guna.UI2.WinForms.Guna2Button btnNhap;
        private Guna.UI2.WinForms.Guna2DataGridView dgvDiem;
        private System.Windows.Forms.Label lblTongSV;
        private Guna.UI2.WinForms.Guna2Button Lưu;
        private Guna.UI2.WinForms.Guna2Button btnHuy;
        private Guna.UI2.WinForms.Guna2Button btnSua;
        private Guna.UI2.WinForms.Guna2Button btnReset;
    }
}
