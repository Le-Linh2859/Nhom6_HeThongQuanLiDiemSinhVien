using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLDSV.BLL;
using QLDSV.DAL;

namespace QLDSV.GUI
{
    public partial class frmGiangVien : Form
    {
        GiangVienBLL bll = new GiangVienBLL();
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
            Connection.KetNoi();

            dgvGiangVien.DataSource = bll.GetGiangVien();
            cboKhoa.DataSource = bll.GetKhoa();
            cboLocKhoa.DataSource = bll.GetKhoa();

            cboKhoa.ValueMember = "MaKhoa";
            cboKhoa.DisplayMember = "TenKhoa";
            cboLocKhoa.ValueMember = "MaKhoa";
            cboLocKhoa.DisplayMember = "TenKhoa";

            cboKhoa.SelectedIndex = -1;
            cboLocKhoa.SelectedIndex = -1;

            dgvGiangVien.Columns["MaGV"].HeaderText = "Mã giảng viên";
            dgvGiangVien.Columns["HoTen"].HeaderText = "Họ và tên";
            dgvGiangVien.Columns["GioiTinh"].HeaderText = "Giới tính";
            dgvGiangVien.Columns["DiaChi"].HeaderText = "Địa chỉ";
            dgvGiangVien.Columns["SoDT"].HeaderText = "Số điện thoại";
            dgvGiangVien.Columns["Email"].HeaderText = "Email";
            dgvGiangVien.Columns["TenKhoa"].HeaderText = "Tên khoa";
        }

        //DataTable tblhh;
        private void load_datagridhh()
        {
            //string sql;
            //sql = "SELECT MaGV, HoTen, GioiTinh, DiaChi, SoDT, Email, MaKhoa from GiangVien";
            //tblhh = FunctionQa.getdatatotable(sql);
            //dataGridView.DataSource = tblhh;
            //dataGridView.Columns[0].HeaderText = "Mã Giảng Viên";
            //dataGridView.Columns[1].HeaderText = "Tên giảng viên";
            //dataGridView.Columns[2].HeaderText = "Giới tính";
            //dataGridView.Columns[3].HeaderText = "Địa chỉ";
            //dataGridView.Columns[4].HeaderText = "SĐT";
            //dataGridView.Columns[5].HeaderText = "Email";
            //dataGridView.Columns[6].HeaderText = "Mã Khoa";
            ////dataGridView.Columns[6].HeaderText = "Ảnh";
            ////dataGridView.Columns[7].HeaderText = "Ghi chú";
            //dataGridView.AllowUserToAddRows = false;
            //dataGridView.EditMode = DataGridViewEditMode.EditProgrammatically;
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

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
