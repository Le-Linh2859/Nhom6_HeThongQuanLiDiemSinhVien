using QLDSV.DAL;
using QLDSV.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace QLDSV.GUI
{
    public partial class frmCanhBaoHocVu : Form
    {
        CanhBaoHocVuBLL bll = new CanhBaoHocVuBLL();
        public frmCanhBaoHocVu()
        {
            InitializeComponent();
        }

        private void frmCanhBaoHocVu_Load(object sender, EventArgs e)
        {
            Connection.KetNoi();
            dgvCanhBao.DataSource = bll.GetCanhBaoSinhVien();

            // Năm học
            cboNamHoc.DataSource = bll.GetNamHoc();
            cboNamHoc.ValueMember = "MaNamHoc";
            cboNamHoc.DisplayMember = "TenNamHoc";

            // Học kỳ
            cboHocKy.DataSource = bll.GetHocKy();
            cboHocKy.ValueMember = "MaLoaiHK";
            cboHocKy.DisplayMember = "TenLoaiHK";

            // Lớp
            cboLop.DataSource = bll.GetLop();
            cboLop.ValueMember = "MaLopNienChe";
            cboLop.DisplayMember = "MaLopNienChe";

            // Loại cảnh báo
            cboLoaiCanhBao.DataSource = bll.GetCanhBao();
            cboLoaiCanhBao.ValueMember = "MaCanhBao";
            cboLoaiCanhBao.DisplayMember = "LoaiCanhBao";
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
