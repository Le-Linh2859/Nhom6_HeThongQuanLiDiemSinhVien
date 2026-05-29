using System.Windows.Forms;

namespace QLDSV.GUI
{
    public partial class FrmNhapDiemSV : Form, IShellChildForm
    {
        public void OnEmbeddedInShell()
        {
            if (pnlSidebar != null) pnlSidebar.Visible = false;
            if (guna2ImageButton1 != null) guna2ImageButton1.Visible = false;
            if (guna2ImageButton2 != null) guna2ImageButton2.Visible = false;
            if (guna2ImageButton3 != null) guna2ImageButton3.Visible = false;
            if (guna2ImageButton4 != null) guna2ImageButton4.Visible = false;
            if (guna2ImageButton5 != null) guna2ImageButton5.Visible = false;
            if (guna2ImageButton6 != null) guna2ImageButton6.Visible = false;
            if (guna2CirclePictureBox1 != null) guna2CirclePictureBox1.Visible = false;
            if (guna2CirclePictureBox2 != null) guna2CirclePictureBox2.Visible = false;
            if (guna2HtmlLabel13 != null) guna2HtmlLabel13.Visible = false;
            if (guna2HtmlLabel14 != null) guna2HtmlLabel14.Visible = false;
            if (label3 != null) label3.Visible = false;
            if (label4 != null) label4.Visible = false;
            if (label5 != null) label5.Visible = false;
            if (label6 != null) label6.Visible = false;
        }

        public FrmNhapDiemSV()
        {
            InitializeComponent();

            ThemeHelper.ApplyTheme(this);
        }
    }
}
