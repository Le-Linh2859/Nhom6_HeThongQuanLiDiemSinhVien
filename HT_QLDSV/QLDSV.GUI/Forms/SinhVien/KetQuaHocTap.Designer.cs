using System.Drawing;
using System.Windows.Forms;

namespace QLDSV.GUI.Forms.SinhVien
{
    partial class KetQuaHocTap
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.lblNamHoc = new System.Windows.Forms.Label();
            this.cboNamHoc = new System.Windows.Forms.ComboBox();
            this.lblHocKy = new System.Windows.Forms.Label();
            this.cboHocKy = new System.Windows.Forms.ComboBox();
            this.lblMaSV = new System.Windows.Forms.Label();
            this.lblTenSV = new System.Windows.Forms.Label();
            this.lblMa = new System.Windows.Forms.Label();
            this.lblTen = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTC = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblTK10 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblTK4 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblPhanloai = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.DataGridViewKQDiem = new Guna.UI2.WinForms.Guna2DataGridView();
            this.pnlFilter.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewKQDiem)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlFilter
            // 
            this.pnlFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFilter.Controls.Add(this.lblTen);
            this.pnlFilter.Controls.Add(this.lblMa);
            this.pnlFilter.Controls.Add(this.lblTenSV);
            this.pnlFilter.Controls.Add(this.lblMaSV);
            this.pnlFilter.Controls.Add(this.lblNamHoc);
            this.pnlFilter.Controls.Add(this.cboNamHoc);
            this.pnlFilter.Controls.Add(this.lblHocKy);
            this.pnlFilter.Controls.Add(this.cboHocKy);
            this.pnlFilter.Location = new System.Drawing.Point(12, 12);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1200, 120);
            this.pnlFilter.TabIndex = 0;
            // 
            // lblNamHoc
            // 
            this.lblNamHoc.AutoSize = true;
            this.lblNamHoc.Location = new System.Drawing.Point(104, 82);
            this.lblNamHoc.Name = "lblNamHoc";
            this.lblNamHoc.Size = new System.Drawing.Size(50, 13);
            this.lblNamHoc.TabIndex = 0;
            this.lblNamHoc.Text = "Năm học";
            // 
            // cboNamHoc
            // 
            this.cboNamHoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNamHoc.Location = new System.Drawing.Point(183, 78);
            this.cboNamHoc.Name = "cboNamHoc";
            this.cboNamHoc.Size = new System.Drawing.Size(222, 21);
            this.cboNamHoc.TabIndex = 0;
            // 
            // lblHocKy
            // 
            this.lblHocKy.AutoSize = true;
            this.lblHocKy.Location = new System.Drawing.Point(581, 82);
            this.lblHocKy.Name = "lblHocKy";
            this.lblHocKy.Size = new System.Drawing.Size(41, 13);
            this.lblHocKy.TabIndex = 1;
            this.lblHocKy.Text = "Học kỳ";
            // 
            // cboHocKy
            // 
            this.cboHocKy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHocKy.Location = new System.Drawing.Point(677, 78);
            this.cboHocKy.Name = "cboHocKy";
            this.cboHocKy.Size = new System.Drawing.Size(205, 21);
            this.cboHocKy.TabIndex = 1;
            // 
            // lblMaSV
            // 
            this.lblMaSV.AutoSize = true;
            this.lblMaSV.Location = new System.Drawing.Point(104, 35);
            this.lblMaSV.Name = "lblMaSV";
            this.lblMaSV.Size = new System.Drawing.Size(70, 13);
            this.lblMaSV.TabIndex = 2;
            this.lblMaSV.Text = "Mã sinh viên:";
            // 
            // lblTenSV
            // 
            this.lblTenSV.AutoSize = true;
            this.lblTenSV.Location = new System.Drawing.Point(581, 35);
            this.lblTenSV.Name = "lblTenSV";
            this.lblTenSV.Size = new System.Drawing.Size(87, 13);
            this.lblTenSV.TabIndex = 3;
            this.lblTenSV.Text = "Họ tên sinh viên:";
            // 
            // lblMa
            // 
            this.lblMa.AutoSize = true;
            this.lblMa.Location = new System.Drawing.Point(180, 35);
            this.lblMa.Name = "lblMa";
            this.lblMa.Size = new System.Drawing.Size(46, 13);
            this.lblMa.TabIndex = 4;
            this.lblMa.Text = "lblMaSV";
            // 
            // lblTen
            // 
            this.lblTen.AutoSize = true;
            this.lblTen.Location = new System.Drawing.Point(674, 35);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(36, 13);
            this.lblTen.TabIndex = 5;
            this.lblTen.Text = "lblTen";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblPhanloai);
            this.groupBox1.Controls.Add(this.lblTK4);
            this.groupBox1.Controls.Add(this.lblTK10);
            this.groupBox1.Controls.Add(this.lblTC);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 497);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1200, 127);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin điểm tổng kết";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tổng số tín chỉ:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(293, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Điểm tổng kết (hệ 10):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(633, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "GPA (hệ 4):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(917, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Phân loại điểm TB:";
            // 
            // lblTC
            // 
            this.lblTC.BackColor = System.Drawing.Color.Transparent;
            this.lblTC.Location = new System.Drawing.Point(134, 55);
            this.lblTC.Name = "lblTC";
            this.lblTC.Size = new System.Drawing.Size(86, 15);
            this.lblTC.TabIndex = 4;
            this.lblTC.Text = "guna2HtmlLabel1";
            // 
            // lblTK10
            // 
            this.lblTK10.BackColor = System.Drawing.Color.Transparent;
            this.lblTK10.Location = new System.Drawing.Point(411, 57);
            this.lblTK10.Name = "lblTK10";
            this.lblTK10.Size = new System.Drawing.Size(86, 15);
            this.lblTK10.TabIndex = 5;
            this.lblTK10.Text = "guna2HtmlLabel1";
            // 
            // lblTK4
            // 
            this.lblTK4.BackColor = System.Drawing.Color.Transparent;
            this.lblTK4.Location = new System.Drawing.Point(701, 55);
            this.lblTK4.Name = "lblTK4";
            this.lblTK4.Size = new System.Drawing.Size(86, 15);
            this.lblTK4.TabIndex = 6;
            this.lblTK4.Text = "guna2HtmlLabel1";
            // 
            // lblPhanloai
            // 
            this.lblPhanloai.BackColor = System.Drawing.Color.Transparent;
            this.lblPhanloai.Location = new System.Drawing.Point(1020, 57);
            this.lblPhanloai.Name = "lblPhanloai";
            this.lblPhanloai.Size = new System.Drawing.Size(86, 15);
            this.lblPhanloai.TabIndex = 7;
            this.lblPhanloai.Text = "guna2HtmlLabel1";
            // 
            // DataGridViewKQDiem
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            this.DataGridViewKQDiem.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DataGridViewKQDiem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DataGridViewKQDiem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DataGridViewKQDiem.DefaultCellStyle = dataGridViewCellStyle3;
            this.DataGridViewKQDiem.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.DataGridViewKQDiem.Location = new System.Drawing.Point(13, 139);
            this.DataGridViewKQDiem.Name = "DataGridViewKQDiem";
            this.DataGridViewKQDiem.RowHeadersVisible = false;
            this.DataGridViewKQDiem.Size = new System.Drawing.Size(1199, 352);
            this.DataGridViewKQDiem.TabIndex = 22;
            this.DataGridViewKQDiem.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.DataGridViewKQDiem.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.DataGridViewKQDiem.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.DataGridViewKQDiem.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.DataGridViewKQDiem.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.DataGridViewKQDiem.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.DataGridViewKQDiem.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.DataGridViewKQDiem.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.DataGridViewKQDiem.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.DataGridViewKQDiem.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataGridViewKQDiem.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.DataGridViewKQDiem.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridViewKQDiem.ThemeStyle.HeaderStyle.Height = 22;
            this.DataGridViewKQDiem.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.DataGridViewKQDiem.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.DataGridViewKQDiem.AllowUserToAddRows = false;
            this.DataGridViewKQDiem.AllowUserToDeleteRows = false;
            this.DataGridViewKQDiem.ReadOnly = true;
            this.DataGridViewKQDiem.MultiSelect = false;
            this.DataGridViewKQDiem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DataGridViewKQDiem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.DataGridViewKQDiem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                MakeCol("colSTT",     "STT",           50),
                MakeCol("colMaMon",   "Mã môn",        90),
                MakeCol("colTenMon",  "Tên môn học",   220),
                MakeCol("colSoTC",    "Số TC",         55),
                MakeCol("colDiemCC",  "Chuyên cần",    85),
                MakeCol("colDiemKT1", "Kiểm tra 1",    85),
                MakeCol("colDiemKT2", "Kiểm tra 2",    85),
                MakeCol("colDiemThi", "Điểm thi",      80),
                MakeCol("colDiemTK",  "Điểm TK",       75),
                MakeCol("colXepLoai", "Xếp loại",      70),
            });
            // 
            // KetQuaHocTap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1224, 685);
            this.Controls.Add(this.DataGridViewKQDiem);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pnlFilter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "KetQuaHocTap";
            this.Text = "Kết Quả Học Tập";
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewKQDiem)).EndInit();
            this.ResumeLayout(false);

        }

        // Helper tạo cột nhanh
        private static DataGridViewTextBoxColumn MakeCol(string name, string header, int width)
        {
            return new DataGridViewTextBoxColumn
            {
                Name         = name,
                HeaderText   = header,
                Width        = width,
                MinimumWidth = 30,
                ReadOnly     = true,
                SortMode     = DataGridViewColumnSortMode.NotSortable
            };
        }

        // Helper tạo một hàng caption + value nằm ngang
        private static void SetupStatRow(
            Label caption, Label value,
            string captionText, string valueText,
            Font fontCaption, Font fontValue, Color valueColor,
            int x, int y)
        {
            caption.AutoSize  = true;
            caption.Font      = fontCaption;
            caption.ForeColor = Color.FromArgb(60, 60, 60);
            caption.Location  = new Point(x, y);
            caption.Text      = captionText;

            value.AutoSize  = true;
            value.Font      = fontValue;
            value.ForeColor = valueColor;
            value.Location  = new Point(x + 160, y);
            value.Text      = valueText;
        }

        #endregion

        private System.Windows.Forms.Panel              pnlFilter;
        private System.Windows.Forms.Label              lblNamHoc;
        private System.Windows.Forms.ComboBox           cboNamHoc;
        private System.Windows.Forms.Label              lblHocKy;
        private System.Windows.Forms.ComboBox           cboHocKy;
        private Label lblTenSV;
        private Label lblMaSV;
        private Label lblMa;
        private Label lblTen;
        private GroupBox groupBox1;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblPhanloai;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTK4;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTK10;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTC;
        private Guna.UI2.WinForms.Guna2DataGridView DataGridViewKQDiem;
    }
}
