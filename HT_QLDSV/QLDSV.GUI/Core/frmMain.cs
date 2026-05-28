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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nghiêm trọng khi khởi động trang chủ: " + ex.Message, "Lỗi nghiêm trọng",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void PhanQuyenSidebar()
        {
            string role = SessionHelper.MaVaiTro;

            // Admin (VT001): Hiển thị tất cả
            if (role == "VT001") return;

            // Giảng viên (VT002): Ẩn các mục Tổng quan, Giảng viên, Môn học, Lớp niên chế, Khoa
            if (role == "VT002")
            {
                if (btnTongquan != null) btnTongquan.Visible = false;
                if (btnGiangvien != null) btnGiangvien.Visible = false;
                if (btnMon != null) btnMon.Visible = false;
                if (btnLopnc != null) btnLopnc.Visible = false;
                if (btnDangky != null) btnDangky.Visible = false; // Ẩn Khoa
            }
            // Sinh viên (VT003): Chỉ hiện Kết quả học tập, Phúc khảo (Ẩn Đăng ký lớp đã bị xóa, Ẩn Khoa)
            else if (role == "VT003")
            {
                if (btnTongquan != null) btnTongquan.Visible = false;
                if (btnGiangvien != null) btnGiangvien.Visible = false;
                if (btnSinhvien != null) btnSinhvien.Visible = false;
                if (btnMon != null) btnMon.Visible = false;
                if (btnLopnc != null) btnLopnc.Visible = false;
                if (btnLophp != null) btnLophp.Visible = false;
                if (btnDangky != null) btnDangky.Visible = false; // Ẩn Khoa
                if (btnDiem != null) btnDiem.Visible = false;
                if (btnCanhbao != null) btnCanhbao.Visible = false;
                if (btnBaocao != null) btnBaocao.Visible = false;
            }
        }

        private void WireUpSidebarEvents()
        {
            if (btnTongquan != null) btnTongquan.Click += (s, e) => OpenChildForm(new frmTongQuan(), "Tổng Quan");
            if (btnGiangvien != null) btnGiangvien.Click += (s, e) => OpenChildForm(new frmGiangVien(), "Giảng Viên");
            if (btnSinhvien != null) btnSinhvien.Click += (s, e) => OpenChildForm(new frmQuanLiThongTinSinhVien(), "Sinh Viên");
            if (btnMon != null) btnMon.Click += (s, e) => OpenChildForm(new frmMonhoc(), "Môn Học");
            if (btnLopnc != null) btnLopnc.Click += (s, e) => OpenChildForm(new FrmLopNienChe(), "Lớp Niên Chế");
            if (btnLophp != null) btnLophp.Click += (s, e) => OpenChildForm(new frmLophocphan(), "Lớp Học Phần");
            if (btnDangky != null) btnDangky.Click += (s, e) => OpenChildForm(new frmKhoa(), "Khoa");
            if (btnDiem != null) btnDiem.Click += (s, e) =>
            {
                // Giảng viên dùng form nhập điểm riêng; Admin dùng form quản lý chung
                if (SessionHelper.MaVaiTro == "VT002")
                    OpenChildForm(new QLDSV.GUI.Forms.GiangVien.FrmNhapDiemSV(), "Nhập Điểm");
                else
                    OpenChildForm(new FrmNhapDiemSV(), "Nhập Điểm");
            };
            if (btnKetqua != null) btnKetqua.Click += (s, e) => {
                if (SessionHelper.MaVaiTro == "VT003")
                    OpenChildForm(new QLDSV.GUI.Forms.SinhVien.KetQuaHocTap(), "Nhập Điểm");
                else OpenChildForm(new frmKetQuaHocTap(), "Kết Quả Học Tập");
            }; 
            
            if (btnCanhbao != null) btnCanhbao.Click += (s, e) => OpenChildForm(new frmCanhBaoHocVu(), "Cảnh Báo Học Vụ");
            if (btnPhuckhao != null) btnPhuckhao.Click += (s, e) => OpenChildForm(new frmPhucKhao(), "Phúc Khảo");
            if (btnBaocao != null) btnBaocao.Click += (s, e) => OpenChildForm(new frmBaoCaoThongKe(), "Báo Cáo Thống Kê");

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

