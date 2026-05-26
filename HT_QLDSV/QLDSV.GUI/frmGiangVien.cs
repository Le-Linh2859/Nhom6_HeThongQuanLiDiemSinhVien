using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDSV.GUI
{
    public partial class frmGiangVien : Form, IShellChildForm
    {
        public void OnEmbeddedInShell()
        {
            // Bước 1: Ẩn các control Sidebar & Header trùng lặp bằng tìm kiếm động an toàn
            string[] controlNames = { 
                "pnlSidebar", "pnlHeader", "guna2ImageButton1", "label3", "label4", 
                "guna2ImageButton2", "guna2CirclePictureBox1", "guna2HtmlLabel13", 
                "guna2HtmlLabel14", "guna2ImageButton3" 
            };
            foreach (var name in controlNames)
            {
                var ct = this.Controls.Find(name, true);
                foreach (var c in ct)
                {
                    c.Visible = false;
                }
            }

            // Bước 2: Tự động xác định khoảng dịch chuyển (shiftX) từ sidebar thực tế
            int shiftX = 0;
            var sidebarControls = this.Controls.Find("pnlSidebar", true);
            if (sidebarControls.Length > 0)
            {
                shiftX = sidebarControls[0].Width;
            }

            // Fallback an toàn: Nếu không tìm thấy sidebar, giữ nguyên giao diện hiện tại
            if (shiftX == 0) return;

            // Bước 3: Dịch chuyển các control cấp cao nhất (Top-Level Controls) sang trái
            foreach (Control ctrl in this.Controls)
            {
                bool isHiddenControl = false;
                foreach (var name in controlNames)
                {
                    if (ctrl.Name == name)
                    {
                        isHiddenControl = true;
                        break;
                    }
                }

                // Thực hiện dịch chuyển có Guard bảo vệ: Chỉ dịch các control có Left > 0 và không cho âm
                if (!isHiddenControl && ctrl.Left > 0)
                {
                    ctrl.Left = Math.Max(0, ctrl.Left - shiftX);
                }
            }

            // Bước 4: Đệ quy tìm kiếm mọi DataGridView bất kể tên gọi để cấu hình Anchor bốn chiều
            SetAnchorAllGrids(this.Controls);
        }

        private void SetAnchorAllGrids(Control.ControlCollection controls)
        {
            foreach (Control c in controls)
            {
                if (c is DataGridView dgv)
                {
                    dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                }
                if (c.Controls.Count > 0)
                {
                    SetAnchorAllGrids(c.Controls); // Đệ quy sâu vào các Panel/GroupBox con
                }
            }
        }

        public frmGiangVien()
        {
            InitializeComponent();
        }

        private void btnTongquan_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView_Click(object sender, EventArgs e)
        {

        }

        private void frmGiangVien_Load(object sender, EventArgs e)
        {
            //FunctionQa.ketnoi();
            txtMagv.Enabled = false;
            btnLuu.Enabled = false;
            btnBoqua.Enabled = false;
            FunctionQa.fillcombo("select MaGV, HoTen, GioiTinh, DiaChi, SoDT, Email, MaKhoa from GiangVien", cboKhoa, "MaKhoa", "TenKhoa");
            cboKhoa.SelectedIndex = -1;
            load_datagridhh();

        }

        DataTable tblhh;
        private void load_datagridhh()
        {
            string sql;
            sql = "SELECT MaGV, HoTen, GioiTinh, DiaChi, SoDT, Email, MaKhoa from GiangVien";
            tblhh = FunctionQa.getdatatotable(sql);
            dataGridView.DataSource = tblhh;
            dataGridView.Columns[0].HeaderText = "Mã Giảng Viên";
            dataGridView.Columns[1].HeaderText = "Tên giảng viên";
            dataGridView.Columns[2].HeaderText = "Giới tính";
            dataGridView.Columns[3].HeaderText = "Địa chỉ";
            dataGridView.Columns[4].HeaderText = "SĐT";
            dataGridView.Columns[5].HeaderText = "Email";
            dataGridView.Columns[6].HeaderText = "Mã Khoa";
            //dataGridView.Columns[6].HeaderText = "Ảnh";
            //dataGridView.Columns[7].HeaderText = "Ghi chú";
            dataGridView.AllowUserToAddRows = false;
            dataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void txtMagv_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTengv_TextChanged(object sender, EventArgs e)
        {

        }

        private void rdoNam_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdoNu_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSdt_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDiachi_TextChanged(object sender, EventArgs e)
        {

        }

        private void cboKhoa_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
