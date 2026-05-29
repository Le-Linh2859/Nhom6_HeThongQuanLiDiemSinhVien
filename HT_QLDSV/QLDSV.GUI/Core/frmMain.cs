using QLDSV.GUI.Forms.Admin;
using QLDSV.GUI.Forms.SinhVien;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QLDSV.GUI
{
    public partial class frmMain : Form
    {
        private Form activeForm = null;
        private Panel pnlContent;

        private bool isLoggingOut = false;

        public frmMain()
        {
            InitializeComponent();

            ThemeHelper.ApplyTheme(this);
            
            // Đăng ký sự kiện đóng ứng dụng để giải phóng kết nối SQL và tiến trình ngầm
            this.FormClosing += (s, e) =>
            {
                try
                {
                    FunctionQa.conn?.Close();
                }
                catch { }
            };

            this.FormClosed += (s, e) =>
            {
                if (!isLoggingOut)
                {
                    Application.Exit();
                }
            };
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Kiểm tra quyền đăng nhập trước tiên để đảm bảo bảo mật điều hướng
            if (string.IsNullOrEmpty(SessionHelper.MaTaiKhoan))
            {
                MessageBox.Show("Bạn chưa đăng nhập! Hệ thống sẽ tự động chuyển hướng về trang Đăng nhập.",
                                "Cảnh báo bảo mật", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                isLoggingOut = true;
                frmDangNhap loginForm = new frmDangNhap();
                loginForm.Show();
                this.Close();
                return;
            }

            try
            {
                // Khởi tạo kết nối cơ sở dữ liệu toàn cục cho các Form con sử dụng FunctionQa
                FunctionQa.ketnoi();
                
                // Kiểm tra xem kết nối đã mở thành công hay chưa
                if (FunctionQa.conn == null || FunctionQa.conn.State != ConnectionState.Open)
                {
                    MessageBox.Show("Không thể kết nối cơ sở dữ liệu. Vui lòng kiểm tra lại chuỗi kết nối.",
                                    "Lỗi khởi động", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    return;
                }

                // Đồng bộ Connection.conn (DAL layer) với FunctionQa.conn (GUI layer)
                // để các form dùng DAL layer không cần mở kết nối riêng
                QLDSV.DAL.Connection.conn       = FunctionQa.conn;
                QLDSV.DAL.Connection.connstring = FunctionQa.connstring;

                // 1. Khởi tạo pnlContent động bên phải Sidebar (x=147) và dưới Header (y=40)
                pnlContent = new Panel();
                pnlContent.Location = new Point(147, 40);
                pnlContent.Size = new Size(this.ClientSize.Width - 147, this.ClientSize.Height - 40);
                pnlContent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                pnlContent.BackColor = Color.White;
                this.Controls.Add(pnlContent);

                // 2. Hiển thị thông tin đăng nhập thực tế lên Top Header của Shell
                if (guna2HtmlLabel13 != null)
                {
                    guna2HtmlLabel13.Text = SessionHelper.TenDangNhap ?? "User";
                }
                
                if (guna2HtmlLabel14 != null)
                {
                    string roleTitle = "Khách";
                    if (SessionHelper.MaVaiTro == "VT001") roleTitle = "Quản trị viên";
                    else if (SessionHelper.MaVaiTro == "VT002") roleTitle = "Giảng viên";
                    else if (SessionHelper.MaVaiTro == "VT003") roleTitle = "Sinh viên";
                    guna2HtmlLabel14.Text = roleTitle;
                }

                // 3. Phân quyền hiển thị Sidebar
                PhanQuyenSidebar();

                // 4. Đăng ký sự kiện Click cho tất cả các nút Sidebar
                WireUpSidebarEvents();

                // 5. Mở form mặc định theo role sau khi đăng nhập
                if (SessionHelper.MaVaiTro == "VT002")
                {
                    SetActiveButton(btnGiangvien);
                    OpenChildForm(new QLDSV.GUI.Forms.GiangVien.frmThongTinCaNhan_GV(), "Thông Tin Cá Nhân");
                }
                else if (SessionHelper.MaVaiTro == "VT003")
                {
                    SetActiveButton(btnSinhvien);
                    OpenChildForm(new QLDSV.GUI.Forms.SinhVien.frmThongTinCaNhan_SV(), "Thông Tin Cá Nhân");
                }
                // VT001 (Admin): không mở form con mặc định, hiển thị frmMain trống

                // 6. Nhấp vào ảnh đại diện hoặc Tên tài khoản để mở trang Thông tin tài khoản
                if (guna2CirclePictureBox1 != null)
                {
                    guna2CirclePictureBox1.Cursor = Cursors.Hand;
                    guna2CirclePictureBox1.Click += (s, ev) =>
                    {
                        OpenChildForm(new QLDSV.GUI.Forms.frmThongTinTaiKhoan(), "Thông Tin Tài Khoản");
                    };
                }
                if (guna2HtmlLabel13 != null)
                {
                    guna2HtmlLabel13.Cursor = Cursors.Hand;
                    guna2HtmlLabel13.Click += (s, ev) =>
                    {
                        OpenChildForm(new QLDSV.GUI.Forms.frmThongTinTaiKhoan(), "Thông Tin Tài Khoản");
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nghiêm trọng khi khởi động trang chủ: " + ex.Message, "Lỗi nghiêm trọng",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void SetMenuRowVisibility(int rowIndex, Control button, Control pictureBox, bool visible)
        {
            if (button != null) button.Visible = visible;
            if (pictureBox != null) pictureBox.Visible = visible;
            
            if (tableLayoutPanel1 != null && rowIndex >= 0 && rowIndex < tableLayoutPanel1.RowStyles.Count)
            {
                if (visible)
                {
                    tableLayoutPanel1.RowStyles[rowIndex] = new RowStyle(SizeType.Percent, 8.333335F);
                }
                else
                {
                    tableLayoutPanel1.RowStyles[rowIndex] = new RowStyle(SizeType.Absolute, 0F);
                }
            }
        }

        private void PhanQuyenSidebar()
        {
            string role = SessionHelper.MaVaiTro;

            // Admin (VT001): Hiển thị tất cả, khôi phục hàng về mặc định
            if (role == "VT001")
            {
                SetMenuRowVisibility(0, btnTongquan, guna2PictureBox1, true);
                SetMenuRowVisibility(1, btnGiangvien, guna2PictureBox2, true);
                SetMenuRowVisibility(2, btnSinhvien, guna2PictureBox3, true);
                SetMenuRowVisibility(3, btnMon, guna2PictureBox4, true);
                SetMenuRowVisibility(4, btnLopnc, guna2PictureBox5, true);
                SetMenuRowVisibility(5, btnLophp, guna2PictureBox6, true);
                SetMenuRowVisibility(6, btnDangky, guna2PictureBox7, true);
                
                // Mở chức năng Tài khoản cho Admin
                if (btnDiem != null) btnDiem.Text = "Tài khoản";
                SetMenuRowVisibility(7, btnDiem, guna2PictureBox8, true);

                SetMenuRowVisibility(8, btnKetqua, guna2PictureBox9, true);
                SetMenuRowVisibility(9, btnCanhbao, guna2PictureBox10, true);
                SetMenuRowVisibility(10, btnPhuckhao, guna2PictureBox11, true);
                SetMenuRowVisibility(11, btnBaocao, guna2PictureBox12, true);
                return;
            }

            // Giảng viên (VT002): Môn học, Giảng viên, Lớp học phần, Sinh viên, Nhập điểm, Tra cứu điểm, Phúc khảo
            if (role == "VT002")
            {
                SetMenuRowVisibility(0, btnTongquan, guna2PictureBox1, false);
                SetMenuRowVisibility(1, btnGiangvien, guna2PictureBox2, true);
                SetMenuRowVisibility(2, btnSinhvien, guna2PictureBox3, true);
                SetMenuRowVisibility(3, btnMon, guna2PictureBox4, true);
                SetMenuRowVisibility(4, btnLopnc, guna2PictureBox5, false);
                SetMenuRowVisibility(5, btnLophp, guna2PictureBox6, true);
                SetMenuRowVisibility(6, btnDangky, guna2PictureBox7, false);
                
                // Đặt lại text Nhập điểm cho Giảng viên
                if (btnDiem != null) btnDiem.Text = "Nhập điểm";
                SetMenuRowVisibility(7, btnDiem, guna2PictureBox8, true);

                SetMenuRowVisibility(8, btnKetqua, guna2PictureBox9, false);
                SetMenuRowVisibility(9, btnCanhbao, guna2PictureBox10, false);
                SetMenuRowVisibility(10, btnPhuckhao, guna2PictureBox11, true);
                SetMenuRowVisibility(11, btnBaocao, guna2PictureBox12, true); // Tra cứu điểm / Báo cáo
            }
            // Sinh viên (VT003): Môn học, Giảng viên, Lớp học phần, Kết quả học tập, Phúc khảo, Cảnh báo học vụ, Tra cứu điểm
            else if (role == "VT003")
            {
                SetMenuRowVisibility(0, btnTongquan, guna2PictureBox1, false);
                SetMenuRowVisibility(1, btnGiangvien, guna2PictureBox2, true);
                SetMenuRowVisibility(2, btnSinhvien, guna2PictureBox3, true);
                SetMenuRowVisibility(3, btnMon, guna2PictureBox4, true);
                SetMenuRowVisibility(4, btnLopnc, guna2PictureBox5, false);
                SetMenuRowVisibility(5, btnLophp, guna2PictureBox6, true);
                SetMenuRowVisibility(6, btnDangky, guna2PictureBox7, false);
                SetMenuRowVisibility(7, btnDiem, guna2PictureBox8, false);
                SetMenuRowVisibility(8, btnKetqua, guna2PictureBox9, true);
                SetMenuRowVisibility(9, btnCanhbao, guna2PictureBox10, true);
                SetMenuRowVisibility(10, btnPhuckhao, guna2PictureBox11, true);
                SetMenuRowVisibility(11, btnBaocao, guna2PictureBox12, true); // Tra cứu điểm / Báo cáo
            }
        }

        private void WireUpSidebarEvents()
        {
            //if (btnTongquan != null) btnTongquan.Click += (s, e) => OpenChildForm(new frmTongQuan(), "Tổng Quan");
            //if (btnGiangvien != null) btnGiangvien.Click += (s, e) => OpenChildForm(new frmGiangvien(), "Giảng Viên");
            //if (btnSinhvien != null) btnSinhvien.Click += (s, e) => OpenChildForm(new frmQuanLiThongTinSinhVien(), "Sinh Viên");

            if (btnTongquan != null) btnTongquan.Click += (s, e) => { SetActiveButton(btnTongquan); OpenChildForm(new frmTongQuan(), "Tổng Quan"); };
            if (btnGiangvien != null) btnGiangvien.Click += (s, e) =>
            {
                SetActiveButton(btnGiangvien);
                if (SessionHelper.MaVaiTro == "VT002")
                    OpenChildForm(new QLDSV.GUI.Forms.GiangVien.frmThongTinCaNhan_GV(), "Thông Tin Cá Nhân");
                else
                    OpenChildForm(new frmGiangvien(), "Giảng Viên");
            };
            if (btnSinhvien != null) btnSinhvien.Click += (s, e) =>
            {
                SetActiveButton(btnSinhvien);
                if (SessionHelper.MaVaiTro == "VT003")
                    OpenChildForm(new QLDSV.GUI.Forms.SinhVien.frmThongTinCaNhan_SV(), "Thông Tin Cá Nhân");
                else
                    OpenChildForm(new frmQuanLiThongTinSinhVien(), "Sinh Viên");
            };

            if (btnMon != null) btnMon.Click += (s, e) =>
            {
                SetActiveButton(btnMon);
                if (SessionHelper.MaVaiTro == "VT002")
                    OpenChildForm(new QLDSV.GUI.Forms.GiangVien.frmMonhoc_GV(), "Môn Học");
                else if (SessionHelper.MaVaiTro == "VT003")
                    OpenChildForm(new QLDSV.GUI.frmMonhoc_SV(), "Môn Học");
                else
                    OpenChildForm(new frmMonhoc(), "Môn Học");
            };

            if (btnCanhbao != null)
            {
                btnCanhbao.Click += (s, e) =>
                {
                    if (SessionHelper.MaVaiTro == "VT003")
                        OpenChildForm(
                            new frmCanhBaoHocVu_SV(),
                            "Cảnh Báo Học Vụ");
                    else
                        OpenChildForm(
                            new frmCanhBaoHocVu(),
                            "Cảnh Báo Học Vụ");
                };
            }
            //if (btnLopnc != null) btnLopnc.Click += (s, e) => OpenChildForm(new FrmLopNienChe(), "Lớp Niên Chế");
            if (btnLopnc != null) btnLopnc.Click += (s, e) => { SetActiveButton(btnLopnc); OpenChildForm(new FrmLopNienChe(), "Lớp Niên Chế"); };
            if (btnLophp != null) btnLophp.Click += (s, e) =>
            {
                SetActiveButton(btnLophp);
                if (SessionHelper.MaVaiTro == "VT002")
                    OpenChildForm(new QLDSV.GUI.Forms.GiangVien.frmLophocphan_GV(), "Lớp Học Phần");
                else if (SessionHelper.MaVaiTro == "VT003")
                    OpenChildForm(new QLDSV.GUI.Forms.SinhVien.frmLophocphan_SV(), "Lớp Học Phần");
                else
                    OpenChildForm(new frmLophocphan(), "Lớp Học Phần");
            };
            if (btnDangky != null) btnDangky.Click += (s, e) => { SetActiveButton(btnDangky); OpenChildForm(new frmKhoa(), "Khoa"); };
            if (btnDiem != null) btnDiem.Click += (s, e) =>
            {
                SetActiveButton(btnDiem);
                // Điều hướng động theo vai trò
                if (SessionHelper.MaVaiTro == "VT001")
                    OpenChildForm(new QLDSV.GUI.frmQuanLiTaiKhoan(), "Quản Lý Tài Khoản");
                else if (SessionHelper.MaVaiTro == "VT002")
                    OpenChildForm(new QLDSV.GUI.Forms.GiangVien.FrmNhapDiemSV(), "Nhập Điểm");
            };
            if (btnKetqua != null) btnKetqua.Click += (s, e) =>
            {
                SetActiveButton(btnKetqua);
                if (SessionHelper.MaVaiTro == "VT003")
                    OpenChildForm(new QLDSV.GUI.Forms.SinhVien.KetQuaHocTap(), "Kết Quả Học Tập");
                else OpenChildForm(new QLDSV.GUI.Forms.Admin.frmTheoDoiDiem(), "Theo Dõi Điểm");
            };


            if (btnBaocao != null) btnBaocao.Click += (s, e) =>
            {
                SetActiveButton(btnBaocao);
                if (SessionHelper.MaVaiTro == "VT002" || SessionHelper.MaVaiTro == "VT003")
                    OpenChildForm(new QLDSV.GUI.Forms.GiangVien.FrmTraCuuDiem(), "Tra Cứu Điểm");
                else
                    OpenChildForm(new frmBaoCaoThongKe(), "Báo Cáo Thống Kê");
            };


            if (guna2Button7 != null)
            {
                guna2Button7.Click += (s, e) =>
                {
                    var confirm = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm == DialogResult.Yes)
                    {
                        Logout();
                    }
                };
            }
        }

        /// <summary>
        /// Đặt nút được chọn thành trạng thái active (nền xám, chữ đen, in đậm, bo tròn).
        /// Các nút còn lại trở về trạng thái mặc định (nền trong suốt, chữ trắng, không in đậm).
        /// </summary>
        private void SetActiveButton(Guna.UI2.WinForms.Guna2Button activeBtn)
        {
            var allButtons = new[]
            {
                btnTongquan, btnGiangvien, btnSinhvien, btnMon,
                btnLopnc, btnLophp, btnDangky, btnDiem,
                btnKetqua, btnCanhbao, btnPhuckhao, btnBaocao
            };

            foreach (var btn in allButtons)
            {
                if (btn == null) continue;
                if (btn == activeBtn)
                {
                    btn.FillColor = System.Drawing.Color.FromArgb(224, 224, 224);
                    btn.ForeColor = System.Drawing.Color.Black;
                    btn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
                    btn.BorderRadius = 5;
                }
                else
                {
                    btn.FillColor = System.Drawing.Color.Transparent;
                    btn.ForeColor = System.Drawing.Color.White;
                    btn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular);
                    btn.BorderRadius = 0;
                }
            }
        }

        public void OpenChildForm(Form childForm, string sectionTitle)
        {
            try
            {
                // Bring panel to front to show the child form
                pnlContent.BringToFront();

                // 1. Giải phóng hoàn toàn Form cũ một cách an toàn (tránh rò rỉ RAM)
                if (activeForm != null)
                {
                    activeForm.Dispose();
                    activeForm = null;
                }

                // 2. Dọn sạch các control khỏi panel sau khi đã giải phóng
                pnlContent.Controls.Clear();

                activeForm = childForm;

                // 3. Cập nhật Tiêu đề Chức năng trên Top Header của Shell
                if (label4 != null)
                {
                    label4.Text = sectionTitle;
                }

                // 4. Thiết lập thuộc tính nhúng
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;

                // 5. Sử dụng Interface ẩn Sidebar & Header nội bộ của Form con một cách chuẩn xác OOP
                if (childForm is IShellChildForm child)
                {
                    child.OnEmbeddedInShell();
                }

                // [GLOBAL UX SAFETY NET] Tự động ẩn các control Header và Sidebar trùng lặp của Form con
                string[] duplicateControls = { 
                    "pnlSidebar", "pnlHeader", "guna2ImageButton1", 
                    "guna2ImageButton2", "guna2CirclePictureBox1", "guna2HtmlLabel13", 
                    "guna2HtmlLabel14", "guna2ImageButton3" 
                };
                foreach (var name in duplicateControls)
                {
                    var found = childForm.Controls.Find(name, true);
                    foreach (var c in found)
                    {
                        c.Visible = false;
                    }
                }

                // 6. Đưa Form con vào Panel và hiển thị
                pnlContent.Controls.Add(childForm);
                pnlContent.Tag = childForm;
                childForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị màn hình chức năng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Logout()
        {
            isLoggingOut = true;

            // 1. Giải phóng Form con đang chạy ngầm trong Panel
            if (activeForm != null)
            {
                activeForm.Dispose();
                activeForm = null;
            }

            // 2. Reset session người dùng
            SessionHelper.Clear();

            // 3. Khởi tạo và mở Form đăng nhập
            var loginForm = new frmDangNhap();
            loginForm.Show();

            // 4. Đóng thẳng Shell frmMain
            this.Close();
        }
    }
}

