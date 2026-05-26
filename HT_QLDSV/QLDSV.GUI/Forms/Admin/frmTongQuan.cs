using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QLDSV.GUI
{
    public partial class frmTongQuan : Form, IShellChildForm
    {
        public void OnEmbeddedInShell()
        {
            // Form này được xây dựng độc lập làm màn hình trung tâm của Shell,
            // không chứa Sidebar hay Header nội bộ trùng lặp nên không cần dịch chuyển.
        }

        public frmTongQuan()
        {
            InitializeComponent();
            ThemeHelper.ApplyTheme(this);
        }

        private void frmTongQuan_Load(object sender, EventArgs e)
        {
            LoadStatistics();
        }

        public void LoadStatistics()
        {
            try
            {
                // Lấy thống kê số liệu thực tế từ database thông qua helper FunctionQa
                string studentsCount = "0";
                string lecturersCount = "0";
                string departmentsCount = "0";
                string reviewsCount = "0";

                try
                {
                    studentsCount = FunctionQa.getfieldvalue("SELECT COUNT(*) FROM SinhVien") ?? "0";
                }
                catch { }

                try
                {
                    lecturersCount = FunctionQa.getfieldvalue("SELECT COUNT(*) FROM GiangVien") ?? "0";
                }
                catch { }

                try
                {
                    departmentsCount = FunctionQa.getfieldvalue("SELECT COUNT(*) FROM Khoa") ?? "0";
                }
                catch { }

                try
                {
                    reviewsCount = FunctionQa.getfieldvalue("SELECT COUNT(*) FROM PhucKhao WHERE TrangThai = N'Chờ duyệt'") ?? "0";
                }
                catch { }

                lblStudentsVal.Text = studentsCount;
                lblLecturersVal.Text = lecturersCount;
                lblDeptsVal.Text = departmentsCount;
                lblReviewsVal.Text = reviewsCount;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi thống kê: " + ex.Message);
            }
        }
    }
}
