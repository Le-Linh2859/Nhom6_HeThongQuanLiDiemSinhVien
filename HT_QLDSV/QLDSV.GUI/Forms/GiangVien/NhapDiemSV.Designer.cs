namespace QLDSV.GUI.Forms.GiangVien
{
    partial class FrmNhapDiemSV
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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlFilter = new System.Windows.Forms.Panel();
            this.lblHKNH = new System.Windows.Forms.Label();
            this.cboHocKy = new System.Windows.Forms.ComboBox();
            this.lblLHP = new System.Windows.Forms.Label();
            this.cboLopHocPhan = new System.Windows.Forms.ComboBox();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.pnlInput = new System.Windows.Forms.Panel();
            this.lblInputTitle = new System.Windows.Forms.Label();
            this.lblMaSV = new System.Windows.Forms.Label();
            this.cboSinhVien = new System.Windows.Forms.ComboBox();
            this.lblTenSV = new System.Windows.Forms.Label();
            this.txtTenSV = new System.Windows.Forms.TextBox();
            this.lblCC = new System.Windows.Forms.Label();
            this.txtDiemCC = new System.Windows.Forms.TextBox();
            this.lblKT1 = new System.Windows.Forms.Label();
            this.txtDiemKT1 = new System.Windows.Forms.TextBox();
            this.lblKT2 = new System.Windows.Forms.Label();
            this.txtDiemKT2 = new System.Windows.Forms.TextBox();
            this.lblThi = new System.Windows.Forms.Label();
            this.txtDiemThi = new System.Windows.Forms.TextBox();
            this.lblNote = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.lblGridTitle = new System.Windows.Forms.Label();
            this.lblSoSV = new System.Windows.Forms.Label();
            this.dgvDiem = new System.Windows.Forms.DataGridView();
            this.pnlTop.SuspendLayout();
            this.pnlFilter.SuspendLayout();
            this.pnlInput.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiem)).BeginInit();
            this.SuspendLayout();

            // ── pnlTop ──────────────────────────────────────────────────────────
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(64, 58, 178);
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Height = 52;
            this.pnlTop.Name = "pnlTop";
            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(16, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Text = "✏️  Nhập Điểm Sinh Viên";

            // ── pnlFilter ───────────────────────────────────────────────────────
            this.pnlFilter.BackColor = System.Drawing.Color.FromArgb(245, 245, 250);
            this.pnlFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFilter.Controls.Add(this.lblHKNH);
            this.pnlFilter.Controls.Add(this.cboHocKy);
            this.pnlFilter.Controls.Add(this.lblLHP);
            this.pnlFilter.Controls.Add(this.cboLopHocPhan);
            this.pnlFilter.Controls.Add(this.lblTimKiem);
            this.pnlFilter.Controls.Add(this.txtTimKiem);
            this.pnlFilter.Controls.Add(this.btnTimKiem);
            this.pnlFilter.Controls.Add(this.btnLamMoi);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Height = 60;
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            // lblHKNH
            this.lblHKNH.AutoSize = true;
            this.lblHKNH.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblHKNH.Location = new System.Drawing.Point(10, 20);
            this.lblHKNH.Name = "lblHKNH";
            this.lblHKNH.Text = "Học kỳ - Năm học:";
            // cboHocKy
            this.cboHocKy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHocKy.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboHocKy.Location = new System.Drawing.Point(130, 16);
            this.cboHocKy.Name = "cboHocKy";
            this.cboHocKy.Size = new System.Drawing.Size(200, 23);
            // lblLHP
            this.lblLHP.AutoSize = true;
            this.lblLHP.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLHP.Location = new System.Drawing.Point(345, 20);
            this.lblLHP.Name = "lblLHP";
            this.lblLHP.Text = "Lớp học phần:";
            // cboLopHocPhan
            this.cboLopHocPhan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLopHocPhan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboLopHocPhan.Location = new System.Drawing.Point(445, 16);
            this.cboLopHocPhan.Name = "cboLopHocPhan";
            this.cboLopHocPhan.Size = new System.Drawing.Size(240, 23);

            // lblTimKiem
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTimKiem.Location = new System.Drawing.Point(700, 20);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Text = "Tìm SV:";
            // txtTimKiem
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTimKiem.Location = new System.Drawing.Point(755, 16);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(160, 23);
            // btnTimKiem
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(64, 58, 178);
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.FlatAppearance.BorderSize = 0;
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(922, 15);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(70, 26);
            this.btnTimKiem.Text = "🔍 Tìm";
            // btnLamMoi
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.FlatAppearance.BorderSize = 0;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(998, 15);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(80, 26);
            this.btnLamMoi.Text = "🔄 Làm mới";

            // ── pnlInput ────────────────────────────────────────────────────────
            this.pnlInput.BackColor = System.Drawing.Color.White;
            this.pnlInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlInput.Controls.Add(this.lblInputTitle);
            this.pnlInput.Controls.Add(this.lblMaSV);
            this.pnlInput.Controls.Add(this.cboSinhVien);
            this.pnlInput.Controls.Add(this.lblTenSV);
            this.pnlInput.Controls.Add(this.txtTenSV);
            this.pnlInput.Controls.Add(this.lblCC);
            this.pnlInput.Controls.Add(this.txtDiemCC);
            this.pnlInput.Controls.Add(this.lblKT1);
            this.pnlInput.Controls.Add(this.txtDiemKT1);
            this.pnlInput.Controls.Add(this.lblKT2);
            this.pnlInput.Controls.Add(this.txtDiemKT2);
            this.pnlInput.Controls.Add(this.lblThi);
            this.pnlInput.Controls.Add(this.txtDiemThi);
            this.pnlInput.Controls.Add(this.lblNote);
            this.pnlInput.Controls.Add(this.pnlButtons);
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInput.Height = 130;
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);

            // lblInputTitle
            this.lblInputTitle.AutoSize = true;
            this.lblInputTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblInputTitle.ForeColor = System.Drawing.Color.FromArgb(64, 58, 178);
            this.lblInputTitle.Location = new System.Drawing.Point(10, 6);
            this.lblInputTitle.Name = "lblInputTitle";
            this.lblInputTitle.Text = "📝  Nhập điểm";
            // lblMaSV
            this.lblMaSV.AutoSize = true;
            this.lblMaSV.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMaSV.Location = new System.Drawing.Point(10, 38);
            this.lblMaSV.Name = "lblMaSV";
            this.lblMaSV.Text = "Sinh viên:";
            // cboSinhVien
            this.cboSinhVien.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSinhVien.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboSinhVien.Location = new System.Drawing.Point(80, 34);
            this.cboSinhVien.Name = "cboSinhVien";
            this.cboSinhVien.Size = new System.Drawing.Size(130, 23);
            // lblTenSV
            this.lblTenSV.AutoSize = true;
            this.lblTenSV.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTenSV.Location = new System.Drawing.Point(218, 38);
            this.lblTenSV.Name = "lblTenSV";
            this.lblTenSV.Text = "Họ tên:";
            // txtTenSV
            this.txtTenSV.BackColor = System.Drawing.Color.FromArgb(240, 240, 240);
            this.txtTenSV.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTenSV.Location = new System.Drawing.Point(268, 34);
            this.txtTenSV.Name = "txtTenSV";
            this.txtTenSV.ReadOnly = true;
            this.txtTenSV.Size = new System.Drawing.Size(170, 23);
            // lblCC
            this.lblCC.AutoSize = true;
            this.lblCC.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCC.Location = new System.Drawing.Point(10, 72);
            this.lblCC.Name = "lblCC";
            this.lblCC.Text = "Chuyên cần (0-10):";
            // txtDiemCC
            this.txtDiemCC.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDiemCC.Location = new System.Drawing.Point(140, 68);
            this.txtDiemCC.Name = "txtDiemCC";
            this.txtDiemCC.Size = new System.Drawing.Size(70, 23);
            // lblKT1
            this.lblKT1.AutoSize = true;
            this.lblKT1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblKT1.Location = new System.Drawing.Point(225, 72);
            this.lblKT1.Name = "lblKT1";
            this.lblKT1.Text = "Kiểm tra 1 (0-10):";
            // txtDiemKT1
            this.txtDiemKT1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDiemKT1.Location = new System.Drawing.Point(355, 68);
            this.txtDiemKT1.Name = "txtDiemKT1";
            this.txtDiemKT1.Size = new System.Drawing.Size(70, 23);
            // lblKT2
            this.lblKT2.AutoSize = true;
            this.lblKT2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblKT2.Location = new System.Drawing.Point(440, 72);
            this.lblKT2.Name = "lblKT2";
            this.lblKT2.Text = "Kiểm tra 2 (0-10):";
            // txtDiemKT2
            this.txtDiemKT2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDiemKT2.Location = new System.Drawing.Point(570, 68);
            this.txtDiemKT2.Name = "txtDiemKT2";
            this.txtDiemKT2.Size = new System.Drawing.Size(70, 23);

            // lblThi
            this.lblThi.AutoSize = true;
            this.lblThi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblThi.ForeColor = System.Drawing.Color.FromArgb(180, 80, 0);
            this.lblThi.Location = new System.Drawing.Point(655, 72);
            this.lblThi.Name = "lblThi";
            this.lblThi.Text = "Điểm thi (0-10):";
            // txtDiemThi
            this.txtDiemThi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDiemThi.Location = new System.Drawing.Point(775, 68);
            this.txtDiemThi.Name = "txtDiemThi";
            this.txtDiemThi.Size = new System.Drawing.Size(70, 23);
            // lblNote
            this.lblNote.AutoSize = true;
            this.lblNote.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblNote.ForeColor = System.Drawing.Color.FromArgb(180, 80, 0);
            this.lblNote.Location = new System.Drawing.Point(655, 94);
            this.lblNote.Name = "lblNote";
            this.lblNote.Text = "* Điểm thi có thể nhập sau";

            // ── pnlButtons ──────────────────────────────────────────────────────
            this.pnlButtons.Controls.Add(this.btnThem);
            this.pnlButtons.Controls.Add(this.btnSua);
            this.pnlButtons.Controls.Add(this.btnLuu);
            this.pnlButtons.Controls.Add(this.btnHuy);
            this.pnlButtons.Controls.Add(this.btnReset);
            this.pnlButtons.Location = new System.Drawing.Point(860, 28);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(220, 90);
            // btnThem
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.FlatAppearance.BorderSize = 0;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(0, 0);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(100, 30);
            this.btnThem.Text = "➕ Thêm";
            // btnSua
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(255, 153, 0);
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.FlatAppearance.BorderSize = 0;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(110, 0);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(100, 30);
            this.btnSua.Text = "✏️ Sửa";
            // btnLuu
            this.btnLuu.BackColor = System.Drawing.Color.FromArgb(64, 58, 178);
            this.btnLuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuu.FlatAppearance.BorderSize = 0;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(0, 38);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(100, 30);
            this.btnLuu.Text = "💾 Lưu";
            // btnHuy
            this.btnHuy.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            this.btnHuy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuy.FlatAppearance.BorderSize = 0;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(110, 38);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(100, 30);
            this.btnHuy.Text = "✖ Hủy";
            // btnReset
            this.btnReset.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReset.FlatAppearance.BorderSize = 0;
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnReset.ForeColor = System.Drawing.Color.White;
            this.btnReset.Location = new System.Drawing.Point(55, 72);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(100, 30);
            this.btnReset.Text = "🔄 Reset";

            // ── pnlGrid ─────────────────────────────────────────────────────────
            this.pnlGrid.Controls.Add(this.dgvDiem);
            this.pnlGrid.Controls.Add(this.lblGridTitle);
            this.pnlGrid.Controls.Add(this.lblSoSV);
            this.pnlGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
            // lblGridTitle
            this.lblGridTitle.AutoSize = true;
            this.lblGridTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblGridTitle.ForeColor = System.Drawing.Color.FromArgb(64, 58, 178);
            this.lblGridTitle.Location = new System.Drawing.Point(8, 6);
            this.lblGridTitle.Name = "lblGridTitle";
            this.lblGridTitle.Text = "📋  Danh sách điểm sinh viên";
            // lblSoSV
            this.lblSoSV.AutoSize = true;
            this.lblSoSV.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSoSV.ForeColor = System.Drawing.Color.Gray;
            this.lblSoSV.Location = new System.Drawing.Point(8, 28);
            this.lblSoSV.Name = "lblSoSV";
            this.lblSoSV.Text = "Tổng: 0 sinh viên";
            // dgvDiem
            this.dgvDiem.AllowUserToAddRows = false;
            this.dgvDiem.AllowUserToDeleteRows = false;
            this.dgvDiem.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
                | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            this.dgvDiem.BackgroundColor = System.Drawing.Color.White;
            this.dgvDiem.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDiem.ColumnHeadersHeight = 36;
            this.dgvDiem.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvDiem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dgvDiem.Location = new System.Drawing.Point(8, 48);
            this.dgvDiem.Name = "dgvDiem";
            this.dgvDiem.ReadOnly = true;
            this.dgvDiem.RowHeadersWidth = 30;
            this.dgvDiem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDiem.Size = new System.Drawing.Size(1064, 300);

            // ── Form ────────────────────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1100, 650);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlInput);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.pnlTop);
            this.MinimumSize = new System.Drawing.Size(1000, 580);
            this.Name = "FrmNhapDiemSV";
            this.Text = "Nhập Điểm Sinh Viên";
            this.Load += new System.EventHandler(this.FrmNhapDiemSV_Load);

            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.pnlInput.ResumeLayout(false);
            this.pnlInput.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.pnlGrid.ResumeLayout(false);
            this.pnlGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDiem)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlFilter;
        private System.Windows.Forms.Label lblHKNH;
        private System.Windows.Forms.ComboBox cboHocKy;
        private System.Windows.Forms.Label lblLHP;
        private System.Windows.Forms.ComboBox cboLopHocPhan;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Panel pnlInput;
        private System.Windows.Forms.Label lblInputTitle;
        private System.Windows.Forms.Label lblMaSV;
        private System.Windows.Forms.ComboBox cboSinhVien;
        private System.Windows.Forms.Label lblTenSV;
        private System.Windows.Forms.TextBox txtTenSV;
        private System.Windows.Forms.Label lblCC;
        private System.Windows.Forms.TextBox txtDiemCC;
        private System.Windows.Forms.Label lblKT1;
        private System.Windows.Forms.TextBox txtDiemKT1;
        private System.Windows.Forms.Label lblKT2;
        private System.Windows.Forms.TextBox txtDiemKT2;
        private System.Windows.Forms.Label lblThi;
        private System.Windows.Forms.TextBox txtDiemThi;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Panel pnlGrid;
        private System.Windows.Forms.Label lblGridTitle;
        private System.Windows.Forms.Label lblSoSV;
        private System.Windows.Forms.DataGridView dgvDiem;
    }
}
