using System.Windows.Forms;

namespace QLDSV.GUI
{
    partial class frmCanhBaoHocVu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCanhBaoHocVu));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnLammoi = new Guna.UI2.WinForms.Guna2Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cboLoaiCanhBao = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cboHocKy = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboNamHoc = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cboLop = new System.Windows.Forms.ComboBox();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DataGridViewCBHV = new System.Windows.Forms.DataGridView();
            this.btnXuat = new System.Windows.Forms.Button();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewCBHV)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.groupBox3.Controls.Add(this.btnLammoi);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.cboLoaiCanhBao);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.cboHocKy);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.cboNamHoc);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.cboLop);
            this.groupBox3.Controls.Add(this.txtTimKiem);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1203, 102);
            this.groupBox3.TabIndex = 57;
            this.groupBox3.TabStop = false;
            // 
            // btnLammoi
            // 
            this.btnLammoi.BorderRadius = 2;
            this.btnLammoi.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnLammoi.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnLammoi.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnLammoi.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnLammoi.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(123)))), ((int)(((byte)(180)))));
            this.btnLammoi.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLammoi.ForeColor = System.Drawing.Color.White;
            this.btnLammoi.Image = ((System.Drawing.Image)(resources.GetObject("btnLammoi.Image")));
            this.btnLammoi.ImageSize = new System.Drawing.Size(18, 18);
            this.btnLammoi.Location = new System.Drawing.Point(398, 13);
            this.btnLammoi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLammoi.Name = "btnLammoi";
            this.btnLammoi.Size = new System.Drawing.Size(101, 30);
            this.btnLammoi.TabIndex = 63;
            this.btnLammoi.Text = "Làm mới";
            this.btnLammoi.TextOffset = new System.Drawing.Point(1, 0);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(822, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(108, 20);
            this.label5.TabIndex = 62;
            this.label5.Text = "Loại cảnh báo:";
            // 
            // cboLoaiCanhBao
            // 
            this.cboLoaiCanhBao.FormattingEnabled = true;
            this.cboLoaiCanhBao.Location = new System.Drawing.Point(936, 58);
            this.cboLoaiCanhBao.Name = "cboLoaiCanhBao";
            this.cboLoaiCanhBao.Size = new System.Drawing.Size(227, 28);
            this.cboLoaiCanhBao.TabIndex = 61;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(534, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 20);
            this.label6.TabIndex = 60;
            this.label6.Text = "Học kỳ:";
            // 
            // cboHocKy
            // 
            this.cboHocKy.FormattingEnabled = true;
            this.cboHocKy.Location = new System.Drawing.Point(596, 59);
            this.cboHocKy.Name = "cboHocKy";
            this.cboHocKy.Size = new System.Drawing.Size(175, 28);
            this.cboHocKy.TabIndex = 59;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(15, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 20);
            this.label7.TabIndex = 58;
            this.label7.Text = "Năm học:";
            // 
            // cboNamHoc
            // 
            this.cboNamHoc.FormattingEnabled = true;
            this.cboNamHoc.Location = new System.Drawing.Point(97, 58);
            this.cboNamHoc.Name = "cboNamHoc";
            this.cboNamHoc.Size = new System.Drawing.Size(206, 28);
            this.cboNamHoc.TabIndex = 57;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(892, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(38, 20);
            this.label10.TabIndex = 42;
            this.label10.Text = "Lớp:";
            // 
            // cboLop
            // 
            this.cboLop.FormattingEnabled = true;
            this.cboLop.Location = new System.Drawing.Point(936, 18);
            this.cboLop.Name = "cboLop";
            this.cboLop.Size = new System.Drawing.Size(227, 28);
            this.cboLop.TabIndex = 20;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(96, 15);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(296, 27);
            this.txtTimKiem.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 253);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 20);
            this.label8.TabIndex = 17;
            this.label8.Text = "Email:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(75, 20);
            this.label9.TabIndex = 3;
            this.label9.Text = "Tìm kiếm:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.DataGridViewCBHV);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(6, 120);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1210, 368);
            this.groupBox1.TabIndex = 59;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DANH SÁCH BÁO CÁO HỌC VỤ";
            // 
            // DataGridViewCBHV
            // 
            this.DataGridViewCBHV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataGridViewCBHV.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.DataGridViewCBHV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridViewCBHV.Location = new System.Drawing.Point(0, 33);
            this.DataGridViewCBHV.Name = "DataGridViewCBHV";
            this.DataGridViewCBHV.RowHeadersWidth = 51;
            this.DataGridViewCBHV.RowTemplate.Height = 24;
            this.DataGridViewCBHV.Size = new System.Drawing.Size(1210, 395);
            this.DataGridViewCBHV.TabIndex = 61;
            // 
            // btnXuat
            // 
            this.btnXuat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXuat.BackColor = System.Drawing.Color.Green;
            this.btnXuat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXuat.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXuat.ForeColor = System.Drawing.Color.White;
            this.btnXuat.Location = new System.Drawing.Point(1103, 495);
            this.btnXuat.Margin = new System.Windows.Forms.Padding(4);
            this.btnXuat.Name = "btnXuat";
            this.btnXuat.Size = new System.Drawing.Size(112, 41);
            this.btnXuat.TabIndex = 62;
            this.btnXuat.Text = "Xuất Excel";
            this.btnXuat.UseVisualStyleBackColor = false;
            this.btnXuat.Click += new System.EventHandler(this.btnXuat_Click);
            // 
            // frmCanhBaoHocVu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1228, 576);
            this.Controls.Add(this.btnXuat);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmCanhBaoHocVu";
            this.Text = "frmCanhBaoHocVu";
            this.Load += new System.EventHandler(this.frmCanhBaoHocVu_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewCBHV)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboLoaiCanhBao;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboHocKy;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboNamHoc;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cboLop;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView DataGridViewCBHV;
        private Guna.UI2.WinForms.Guna2Button btnLammoi;
        private System.Windows.Forms.Button btnXuat;
    }
}