namespace QLDSV.GUI
{
    partial class frmTongQuan
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.cardsLayout = new System.Windows.Forms.TableLayoutPanel();
            
            // 4 Stats Panels (using standard Guna or system Panels)
            this.cardStudents = new Guna.UI2.WinForms.Guna2Panel();
            this.iconStudents = new System.Windows.Forms.Label();
            this.lblStudentsVal = new System.Windows.Forms.Label();
            this.lblStudentsLbl = new System.Windows.Forms.Label();

            this.cardLecturers = new Guna.UI2.WinForms.Guna2Panel();
            this.iconLecturers = new System.Windows.Forms.Label();
            this.lblLecturersVal = new System.Windows.Forms.Label();
            this.lblLecturersLbl = new System.Windows.Forms.Label();

            this.cardDepts = new Guna.UI2.WinForms.Guna2Panel();
            this.iconDepts = new System.Windows.Forms.Label();
            this.lblDeptsVal = new System.Windows.Forms.Label();
            this.lblDeptsLbl = new System.Windows.Forms.Label();

            this.cardReviews = new Guna.UI2.WinForms.Guna2Panel();
            this.iconReviews = new System.Windows.Forms.Label();
            this.lblReviewsVal = new System.Windows.Forms.Label();
            this.lblReviewsLbl = new System.Windows.Forms.Label();

            this.pnlHeader.SuspendLayout();
            this.cardsLayout.SuspendLayout();
            
            this.cardStudents.SuspendLayout();
            this.cardLecturers.SuspendLayout();
            this.cardDepts.SuspendLayout();
            this.cardReviews.SuspendLayout();
            
            this.SuspendLayout();

            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(800, 80);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(465, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "HỆ THỐNG QUẢN LÝ ĐIỂM SINH VIÊN";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(120)))));
            this.lblSubtitle.Location = new System.Drawing.Point(22, 47);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(360, 19);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Trang tổng quan thông tin và số liệu thống kê hệ thống";

            // 
            // cardsLayout
            // 
            this.cardsLayout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cardsLayout.ColumnCount = 4;
            this.cardsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.cardsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.cardsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.cardsLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            
            this.cardsLayout.Controls.Add(this.cardStudents, 0, 0);
            this.cardsLayout.Controls.Add(this.cardLecturers, 1, 0);
            this.cardsLayout.Controls.Add(this.cardDepts, 2, 0);
            this.cardsLayout.Controls.Add(this.cardReviews, 3, 0);
            
            this.cardsLayout.Location = new System.Drawing.Point(20, 100);
            this.cardsLayout.Name = "cardsLayout";
            this.cardsLayout.RowCount = 1;
            this.cardsLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.cardsLayout.Size = new System.Drawing.Size(760, 130);
            this.cardsLayout.TabIndex = 1;

            // ==========================================
            // CARD 1: STUDENTS
            // ==========================================
            this.cardStudents.BackColor = System.Drawing.Color.Transparent;
            this.cardStudents.BorderRadius = 10;
            this.cardStudents.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(244)))), ((int)(((byte)(253)))));
            this.cardStudents.Controls.Add(this.iconStudents);
            this.cardStudents.Controls.Add(this.lblStudentsVal);
            this.cardStudents.Controls.Add(this.lblStudentsLbl);
            this.cardStudents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardStudents.Margin = new System.Windows.Forms.Padding(6);
            this.cardStudents.Name = "cardStudents";
            // 
            // iconStudents
            // 
            this.iconStudents.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconStudents.AutoSize = true;
            this.iconStudents.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconStudents.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.iconStudents.Location = new System.Drawing.Point(120, 15);
            this.iconStudents.Name = "iconStudents";
            this.iconStudents.Size = new System.Drawing.Size(53, 45);
            this.iconStudents.Text = "🎓";
            // 
            // lblStudentsVal
            // 
            this.lblStudentsVal.AutoSize = true;
            this.lblStudentsVal.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStudentsVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(21)))), ((int)(((byte)(101)))), ((int)(((byte)(192)))));
            this.lblStudentsVal.Location = new System.Drawing.Point(12, 10);
            this.lblStudentsVal.Name = "lblStudentsVal";
            this.lblStudentsVal.Size = new System.Drawing.Size(40, 47);
            this.lblStudentsVal.Text = "0";
            // 
            // lblStudentsLbl
            // 
            this.lblStudentsLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStudentsLbl.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStudentsLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(120)))));
            this.lblStudentsLbl.Location = new System.Drawing.Point(14, 85);
            this.lblStudentsLbl.Name = "lblStudentsLbl";
            this.lblStudentsLbl.Size = new System.Drawing.Size(150, 20);
            this.lblStudentsLbl.Text = "Tổng số sinh viên";

            // ==========================================
            // CARD 2: LECTURERS
            // ==========================================
            this.cardLecturers.BackColor = System.Drawing.Color.Transparent;
            this.cardLecturers.BorderRadius = 10;
            this.cardLecturers.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(251)))), ((int)(((byte)(238)))));
            this.cardLecturers.Controls.Add(this.iconLecturers);
            this.cardLecturers.Controls.Add(this.lblLecturersVal);
            this.cardLecturers.Controls.Add(this.lblLecturersLbl);
            this.cardLecturers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardLecturers.Margin = new System.Windows.Forms.Padding(6);
            this.cardLecturers.Name = "cardLecturers";
            // 
            // iconLecturers
            // 
            this.iconLecturers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconLecturers.AutoSize = true;
            this.iconLecturers.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconLecturers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.iconLecturers.Location = new System.Drawing.Point(120, 15);
            this.iconLecturers.Name = "iconLecturers";
            this.iconLecturers.Size = new System.Drawing.Size(53, 45);
            this.iconLecturers.Text = "👨‍🏫";
            // 
            // lblLecturersVal
            // 
            this.lblLecturersVal.AutoSize = true;
            this.lblLecturersVal.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLecturersVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.lblLecturersVal.Location = new System.Drawing.Point(12, 10);
            this.lblLecturersVal.Name = "lblLecturersVal";
            this.lblLecturersVal.Size = new System.Drawing.Size(40, 47);
            this.lblLecturersVal.Text = "0";
            // 
            // lblLecturersLbl
            // 
            this.lblLecturersLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLecturersLbl.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLecturersLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(120)))));
            this.lblLecturersLbl.Location = new System.Drawing.Point(14, 85);
            this.lblLecturersLbl.Name = "lblLecturersLbl";
            this.lblLecturersLbl.Size = new System.Drawing.Size(150, 20);
            this.lblLecturersLbl.Text = "Tổng số giảng viên";

            // ==========================================
            // CARD 3: DEPTS
            // ==========================================
            this.cardDepts.BackColor = System.Drawing.Color.Transparent;
            this.cardDepts.BorderRadius = 10;
            this.cardDepts.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(232)))), ((int)(((byte)(255)))));
            this.cardDepts.Controls.Add(this.iconDepts);
            this.cardDepts.Controls.Add(this.lblDeptsVal);
            this.cardDepts.Controls.Add(this.lblDeptsLbl);
            this.cardDepts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardDepts.Margin = new System.Windows.Forms.Padding(6);
            this.cardDepts.Name = "cardDepts";
            // 
            // iconDepts
            // 
            this.iconDepts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconDepts.AutoSize = true;
            this.iconDepts.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconDepts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(33)))), ((int)(((byte)(168)))));
            this.iconDepts.Location = new System.Drawing.Point(120, 15);
            this.iconDepts.Name = "iconDepts";
            this.iconDepts.Size = new System.Drawing.Size(53, 45);
            this.iconDepts.Text = "🏛";
            // 
            // lblDeptsVal
            // 
            this.lblDeptsVal.AutoSize = true;
            this.lblDeptsVal.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeptsVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(33)))), ((int)(((byte)(168)))));
            this.lblDeptsVal.Location = new System.Drawing.Point(12, 10);
            this.lblDeptsVal.Name = "lblDeptsVal";
            this.lblDeptsVal.Size = new System.Drawing.Size(40, 47);
            this.lblDeptsVal.Text = "0";
            // 
            // lblDeptsLbl
            // 
            this.lblDeptsLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDeptsLbl.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeptsLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(120)))));
            this.lblDeptsLbl.Location = new System.Drawing.Point(14, 85);
            this.lblDeptsLbl.Name = "lblDeptsLbl";
            this.lblDeptsLbl.Size = new System.Drawing.Size(150, 20);
            this.lblDeptsLbl.Text = "Tổng số khoa";

            // ==========================================
            // CARD 4: REVIEWS
            // ==========================================
            this.cardReviews.BackColor = System.Drawing.Color.Transparent;
            this.cardReviews.BorderRadius = 10;
            this.cardReviews.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(247)))), ((int)(((byte)(237)))));
            this.cardReviews.Controls.Add(this.iconReviews);
            this.cardReviews.Controls.Add(this.lblReviewsVal);
            this.cardReviews.Controls.Add(this.lblReviewsLbl);
            this.cardReviews.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardReviews.Margin = new System.Windows.Forms.Padding(6);
            this.cardReviews.Name = "cardReviews";
            // 
            // iconReviews
            // 
            this.iconReviews.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.iconReviews.AutoSize = true;
            this.iconReviews.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iconReviews.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(65)))), ((int)(((byte)(12)))));
            this.iconReviews.Location = new System.Drawing.Point(120, 15);
            this.iconReviews.Name = "iconReviews";
            this.iconReviews.Size = new System.Drawing.Size(53, 45);
            this.iconReviews.Text = "🔄";
            // 
            // lblReviewsVal
            // 
            this.lblReviewsVal.AutoSize = true;
            this.lblReviewsVal.Font = new System.Drawing.Font("Segoe UI", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReviewsVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(65)))), ((int)(((byte)(12)))));
            this.lblReviewsVal.Location = new System.Drawing.Point(12, 10);
            this.lblReviewsVal.Name = "lblReviewsVal";
            this.lblReviewsVal.Size = new System.Drawing.Size(40, 47);
            this.lblReviewsVal.Text = "0";
            // 
            // lblReviewsLbl
            // 
            this.lblReviewsLbl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblReviewsLbl.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReviewsLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(120)))));
            this.lblReviewsLbl.Location = new System.Drawing.Point(14, 85);
            this.lblReviewsLbl.Name = "lblReviewsLbl";
            this.lblReviewsLbl.Size = new System.Drawing.Size(150, 20);
            this.lblReviewsLbl.Text = "Phúc khảo chờ duyệt";

            // 
            // frmTongQuan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245))))); // Màu xám nhạt nhạt chuẩn CHUNG.md
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cardsLayout);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmTongQuan";
            this.Text = "Tổng Quan";
            this.Load += new System.EventHandler(this.frmTongQuan_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.cardsLayout.ResumeLayout(false);
            
            this.cardStudents.ResumeLayout(false);
            this.cardStudents.PerformLayout();
            this.cardLecturers.ResumeLayout(false);
            this.cardLecturers.PerformLayout();
            this.cardDepts.ResumeLayout(false);
            this.cardDepts.PerformLayout();
            this.cardReviews.ResumeLayout(false);
            this.cardReviews.PerformLayout();
            
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.TableLayoutPanel cardsLayout;
        
        private Guna.UI2.WinForms.Guna2Panel cardStudents;
        private System.Windows.Forms.Label iconStudents;
        public System.Windows.Forms.Label lblStudentsVal;
        private System.Windows.Forms.Label lblStudentsLbl;

        private Guna.UI2.WinForms.Guna2Panel cardLecturers;
        private System.Windows.Forms.Label iconLecturers;
        public System.Windows.Forms.Label lblLecturersVal;
        private System.Windows.Forms.Label lblLecturersLbl;

        private Guna.UI2.WinForms.Guna2Panel cardDepts;
        private System.Windows.Forms.Label iconDepts;
        public System.Windows.Forms.Label lblDeptsVal;
        private System.Windows.Forms.Label lblDeptsLbl;

        private Guna.UI2.WinForms.Guna2Panel cardReviews;
        private System.Windows.Forms.Label iconReviews;
        public System.Windows.Forms.Label lblReviewsVal;
        private System.Windows.Forms.Label lblReviewsLbl;
    }
}
