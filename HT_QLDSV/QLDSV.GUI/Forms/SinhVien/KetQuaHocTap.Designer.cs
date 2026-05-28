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
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlFilter = new Guna.UI2.WinForms.Guna2Panel();
            this.lblNamHoc = new System.Windows.Forms.Label();
            this.cboNamHoc = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblHocKy = new System.Windows.Forms.Label();
            this.cboHocKy = new Guna.UI2.WinForms.Guna2ComboBox();
            this.pnlChuY = new System.Windows.Forms.Panel();
            this.lblChuY = new System.Windows.Forms.Label();
            this.dgvKetQua = new Guna.UI2.WinForms.Guna2DataGridView();
            this.pnlSummary = new System.Windows.Forms.Panel();
            this.lblSummaryLeft = new System.Windows.Forms.Label();
            this.lblSummaryRight = new System.Windows.Forms.Label();
            this.pnlFilter.SuspendLayout();
            this.pnlChuY.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQua)).BeginInit();
            this.pnlSummary.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFilter
            // 
            this.pnlFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlFilter.BorderRadius = 5;
            this.pnlFilter.Controls.Add(this.lblNamHoc);
            this.pnlFilter.Controls.Add(this.cboNamHoc);
            this.pnlFilter.Controls.Add(this.lblHocKy);
            this.pnlFilter.Controls.Add(this.cboHocKy);
            this.pnlFilter.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(200)))), ((int)(((byte)(223)))));
            this.pnlFilter.Location = new System.Drawing.Point(12, 12);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1132, 52);
            this.pnlFilter.TabIndex = 0;
            // 
            // lblNamHoc
            // 
            this.lblNamHoc.AutoSize = true;
            this.lblNamHoc.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblNamHoc.ForeColor = System.Drawing.Color.Black;
            this.lblNamHoc.Location = new System.Drawing.Point(56, 15);
            this.lblNamHoc.Name = "lblNamHoc";
            this.lblNamHoc.Size = new System.Drawing.Size(69, 20);
            this.lblNamHoc.TabIndex = 0;
            this.lblNamHoc.Text = "Năm học";
            // 
            // cboNamHoc
            // 
            this.cboNamHoc.BackColor = System.Drawing.Color.Transparent;
            this.cboNamHoc.BorderRadius = 5;
            this.cboNamHoc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboNamHoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNamHoc.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboNamHoc.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboNamHoc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboNamHoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboNamHoc.ItemHeight = 28;
            this.cboNamHoc.Location = new System.Drawing.Point(152, 12);
            this.cboNamHoc.Name = "cboNamHoc";
            this.cboNamHoc.Size = new System.Drawing.Size(241, 34);
            this.cboNamHoc.TabIndex = 0;
            // 
            // lblHocKy
            // 
            this.lblHocKy.AutoSize = true;
            this.lblHocKy.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblHocKy.ForeColor = System.Drawing.Color.Black;
            this.lblHocKy.Location = new System.Drawing.Point(580, 15);
            this.lblHocKy.Name = "lblHocKy";
            this.lblHocKy.Size = new System.Drawing.Size(54, 20);
            this.lblHocKy.TabIndex = 1;
            this.lblHocKy.Text = "Học kỳ";
            // 
            // cboHocKy
            // 
            this.cboHocKy.BackColor = System.Drawing.Color.Transparent;
            this.cboHocKy.BorderRadius = 5;
            this.cboHocKy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboHocKy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHocKy.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboHocKy.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboHocKy.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboHocKy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboHocKy.ItemHeight = 28;
            this.cboHocKy.Location = new System.Drawing.Point(667, 12);
            this.cboHocKy.Name = "cboHocKy";
            this.cboHocKy.Size = new System.Drawing.Size(212, 34);
            this.cboHocKy.TabIndex = 1;
            // 
            // pnlChuY
            // 
            this.pnlChuY.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlChuY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(204)))));
            this.pnlChuY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlChuY.Controls.Add(this.lblChuY);
            this.pnlChuY.Location = new System.Drawing.Point(12, 72);
            this.pnlChuY.Name = "pnlChuY";
            this.pnlChuY.Size = new System.Drawing.Size(1132, 46);
            this.pnlChuY.TabIndex = 1;
            // 
            // lblChuY
            // 
            this.lblChuY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblChuY.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblChuY.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblChuY.Location = new System.Drawing.Point(0, 0);
            this.lblChuY.Name = "lblChuY";
            this.lblChuY.Padding = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.lblChuY.Size = new System.Drawing.Size(1130, 44);
            this.lblChuY.TabIndex = 0;
            this.lblChuY.Text = "Chú ý:\r\n- Những môn có dấu (*) sẽ không tính điểm trung bình mà chỉ là môn điều k" +
    "iện.";
            // 
            // dgvKetQua
            // 
            this.dgvKetQua.AllowUserToAddRows = false;
            this.dgvKetQua.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.dgvKetQua.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvKetQua.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvKetQua.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvKetQua.ColumnHeadersHeight = 44;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(216)))), ((int)(((byte)(242)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvKetQua.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvKetQua.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(240)))));
            this.dgvKetQua.Location = new System.Drawing.Point(12, 126);
            this.dgvKetQua.MultiSelect = false;
            this.dgvKetQua.Name = "dgvKetQua";
            this.dgvKetQua.ReadOnly = true;
            this.dgvKetQua.RowHeadersVisible = false;
            this.dgvKetQua.RowTemplate.Height = 26;
            this.dgvKetQua.Size = new System.Drawing.Size(1132, 300);
            this.dgvKetQua.TabIndex = 2;
            this.dgvKetQua.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(255)))));
            this.dgvKetQua.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dgvKetQua.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dgvKetQua.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dgvKetQua.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dgvKetQua.ThemeStyle.BackColor = System.Drawing.Color.White;
            this.dgvKetQua.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(240)))));
            this.dgvKetQua.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dgvKetQua.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dgvKetQua.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvKetQua.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dgvKetQua.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvKetQua.ThemeStyle.HeaderStyle.Height = 44;
            this.dgvKetQua.ThemeStyle.ReadOnly = true;
            this.dgvKetQua.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dgvKetQua.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvKetQua.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvKetQua.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvKetQua.ThemeStyle.RowsStyle.Height = 26;
            this.dgvKetQua.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(216)))), ((int)(((byte)(242)))));
            this.dgvKetQua.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.Black;
            // 
            // pnlSummary
            // 
            this.pnlSummary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.pnlSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSummary.Controls.Add(this.lblSummaryLeft);
            this.pnlSummary.Controls.Add(this.lblSummaryRight);
            this.pnlSummary.Location = new System.Drawing.Point(12, 434);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Size = new System.Drawing.Size(1132, 100);
            this.pnlSummary.TabIndex = 3;
            // 
            // lblSummaryLeft
            // 
            this.lblSummaryLeft.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSummaryLeft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblSummaryLeft.Location = new System.Drawing.Point(8, 6);
            this.lblSummaryLeft.Name = "lblSummaryLeft";
            this.lblSummaryLeft.Size = new System.Drawing.Size(560, 88);
            this.lblSummaryLeft.TabIndex = 0;
            this.lblSummaryLeft.Text = "- Điểm trung bình học kỳ 10/100: --\r\n- Điểm trung bình học kỳ 4: --\r\n- Điểm trung" +
    " bình tích lũy: --\r\n- Điểm trung bình tích lũy (hệ 4): --";
            // 
            // lblSummaryRight
            // 
            this.lblSummaryRight.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSummaryRight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.lblSummaryRight.Location = new System.Drawing.Point(580, 6);
            this.lblSummaryRight.Name = "lblSummaryRight";
            this.lblSummaryRight.Size = new System.Drawing.Size(610, 88);
            this.lblSummaryRight.TabIndex = 1;
            this.lblSummaryRight.Text = "- Số tín chỉ đăng ký: --\r\n- Số tín chỉ tích lũy: --\r\n- Phân loại ĐTB HK: --\r\n- Đi" +
    "ểm trung bình rèn luyện HK: --\r\n- Phân loại ĐTBRL HK: --";
            // 
            // KetQuaHocTap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(1156, 546);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.pnlChuY);
            this.Controls.Add(this.dgvKetQua);
            this.Controls.Add(this.pnlSummary);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "KetQuaHocTap";
            this.Text = "Kết Quả Học Tập";
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.pnlChuY.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQua)).EndInit();
            this.pnlSummary.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        // Helper: tạo DataGridViewTextBoxColumn nhanh
        private static System.Windows.Forms.DataGridViewTextBoxColumn MakeCol(
            string name, string header, int width, bool autoFill)
        {
            var col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            col.Name            = name;
            col.HeaderText      = header;
            col.Width           = width;
            col.MinimumWidth    = 30;
            col.ReadOnly        = true;
            col.SortMode        = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            col.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            if (autoFill)
            {
                col.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
                col.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            }
            return col;
        }

        #endregion

        // ── Field declarations ────────────────────────────────────────────────────
        private Guna.UI2.WinForms.Guna2Panel          pnlFilter;
        private System.Windows.Forms.Label            lblNamHoc;
        private Guna.UI2.WinForms.Guna2ComboBox       cboNamHoc;
        private System.Windows.Forms.Label            lblHocKy;
        private Guna.UI2.WinForms.Guna2ComboBox       cboHocKy;
        private System.Windows.Forms.Panel            pnlChuY;
        private System.Windows.Forms.Label            lblChuY;
        private Guna.UI2.WinForms.Guna2DataGridView   dgvKetQua;
        private System.Windows.Forms.Panel            pnlSummary;
        private System.Windows.Forms.Label            lblSummaryLeft;
        private System.Windows.Forms.Label            lblSummaryRight;
    }
}
