using QLDSV.BLL;
using QLDSV.DAL;
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
    public partial class FrmLopNienChe : Form
    {
        private LopNienCheBLL bll = new LopNienCheBLL();
        private bool isAdding = false;
        private bool isEditing = false;
        private DataTable dtLop;

        public FrmLopNienChe()
        {
            InitializeComponent();

            this.Load += new System.EventHandler(this.FrmLopNienChe_Load);

            ThemeHelper.ApplyTheme(this);

            this.btnThem.Click += btnThem_Click;
            this.btnSua.Click += btnSua_Click;
            this.btnLuu.Click += btnLuu_Click;
            this.btnHuy.Click += btnHuy_Click;
            this.btnReset.Click += btnReset_Click;
            this.btnLammoi.Click += btnLammoi_Click;

            this.DataGridViewLop.CellClick += DataGridViewLop_CellClick;

            this.txtTimKiem.TextChanged += txtTimKiem_TextChanged;

            txtTimKiem.Text = "Tìm kiếm theo mã lớp, tên lớp ...";
            txtTimKiem.ForeColor = Color.Gray;

            txtTimKiem.Enter += (s, e) =>
            {
                if (txtTimKiem.Text == "Tìm kiếm theo mã lớp, tên lớp ...")
                {
                    txtTimKiem.Text = "";
                    txtTimKiem.ForeColor = Color.Black;
                }
            };

            txtTimKiem.Leave += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
                {
                    txtTimKiem.Text = "Tìm kiếm theo mã lớp, tên lớp ...";
                    txtTimKiem.ForeColor = Color.Gray;
                }
            };
        }

        private void FrmLopNienChe_Load(object sender, EventArgs e)
        {
            try
            {
                Connection.KetNoi();
                
                LoadComboboxFilters();
                LoadComboboxDetails();
                LoadData();
                ResetFormState();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo Form: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            DataGridViewLop.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            DataGridViewLop.ThemeStyle.HeaderStyle.ForeColor = Color.White;
        }

        // 1. Tải danh sách lớp niên chế lên GridView
        private void LoadData()
        {
            try
            {
                dtLop = bll.GetLopNienChe();
                DataGridViewLop.DataSource = dtLop;

                if (DataGridViewLop.Columns.Count >= 7)
                {
                    DataGridViewLop.Columns["MaLopNienChe"].HeaderText = "Mã lớp niên chế";
                    DataGridViewLop.Columns["TenLop"].HeaderText = "Tên lớp";
                    DataGridViewLop.Columns["NienKhoa"].HeaderText = "Niên khóa";
                    
                    // Ẩn các cột ID không cần hiển thị trực tiếp
                    DataGridViewLop.Columns["MaKhoa"].Visible = false;
                    DataGridViewLop.Columns["TenKhoa"].HeaderText = "Khoa";
                    DataGridViewLop.Columns["MaGV"].Visible = false;
                    DataGridViewLop.Columns["TenGV"].HeaderText = "Giảng viên cố vấn";
                }

                DataGridViewLop.AllowUserToAddRows = false;
                DataGridViewLop.EditMode = DataGridViewEditMode.EditProgrammatically;
                DataGridViewLop.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                // Đảm bảo tiêu đề cột hiển thị đầy đủ và đẹp mắt
                DataGridViewLop.ColumnHeadersVisible = true;
                DataGridViewLop.ColumnHeadersHeight = 35;

                // Tự động chọn dòng đầu tiên nếu có dữ liệu và hiển thị thông tin ở dưới
                if (DataGridViewLop.Rows.Count > 0 && !isAdding && !isEditing)
                {
                    DataGridViewLop.ClearSelection();
                    DataGridViewLop.CurrentCell = DataGridViewLop.Rows[0].Cells["MaLopNienChe"];
                    DataGridViewLop.Rows[0].Selected = true;
                    DataGridViewLop_CellClick(null, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 2. Nạp dữ liệu các bộ lọc Khoa và Giảng viên cố vấn ở trên
        private void LoadComboboxFilters()
        {
            try
            {
                // Lấy danh sách Khoa cho bộ lọc
                DataTable dtKhoa = bll.GetKhoa();
                DataTable dtKhoaFilter = dtKhoa.Copy();
                DataRow rowAllKhoa = dtKhoaFilter.NewRow();
                rowAllKhoa["MaKhoa"] = "ALL";
                rowAllKhoa["TenKhoa"] = "--- Tất cả ---";
                dtKhoaFilter.Rows.InsertAt(rowAllKhoa, 0);

                cboKhoa.DataSource = dtKhoaFilter;
                cboKhoa.ValueMember = "MaKhoa";
                cboKhoa.DisplayMember = "TenKhoa";
                cboKhoa.SelectedIndex = 0;

                // Lấy danh sách Giảng viên cho bộ lọc
                DataTable dtGV = bll.GetGiangVien();
                DataTable dtGVFilter = dtGV.Copy();
                DataRow rowAllGV = dtGVFilter.NewRow();
                rowAllGV["MaGV"] = "ALL";
                rowAllGV["HoTen"] = "--- Tất cả ---";
                dtGVFilter.Rows.InsertAt(rowAllGV, 0);

                cboGiangVien.DataSource = dtGVFilter;
                cboGiangVien.ValueMember = "MaGV";
                cboGiangVien.DisplayMember = "HoTen";
                cboGiangVien.SelectedIndex = 0;

                // Đăng ký sự kiện lọc sau khi nạp xong dữ liệu
                cboKhoa.SelectedIndexChanged += cboFilter_SelectedIndexChanged;
                cboGiangVien.SelectedIndexChanged += cboFilter_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi nạp combobox bộ lọc: " + ex.Message);
            }
        }

        // 3. Nạp dữ liệu các Combobox nhập liệu chi tiết
        private void LoadComboboxDetails()
        {
            try
            {
                // Khoa nhập liệu
                DataTable dtKhoa = bll.GetKhoa();
                cboKhoa2.DataSource = dtKhoa;
                cboKhoa2.ValueMember = "MaKhoa";
                cboKhoa2.DisplayMember = "TenKhoa";
                cboKhoa2.SelectedIndex = -1;

                // Giảng viên nhập liệu
                DataTable dtGV = bll.GetGiangVien();
                cboCV.DataSource = dtGV;
                cboCV.ValueMember = "MaGV";
                cboCV.DisplayMember = "HoTen";
                cboCV.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi nạp combobox chi tiết: " + ex.Message);
            }
        }

        // 4. Khôi phục trạng thái xem ban đầu của Form
        private void ResetFormState()
        {
            isAdding = false;
            isEditing = false;

            // Khóa các trường nhập liệu
            cboMaLopNC.Enabled = false; // Mã lớp
            cboTenlop.Enabled = false; // Tên lớp
            cboNienkhoa.Enabled = false; // Niên khóa
            cboKhoa2.Enabled = false; // Khoa
            cboCV.Enabled = false; // Cố vấn

            // Thiết lập trạng thái các nút
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnReset.Enabled = false;

            // Nạp lại chi tiết dòng đang chọn
            if (DataGridViewLop.SelectedRows.Count > 0)
            {
                DataGridViewLop_CellClick(null, null);
            }
        }

        // 5. Sự kiện khi click chọn một dòng trên bảng danh sách
        private void DataGridViewLop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (isAdding || isEditing) return; // Đang thêm/sửa thì không đổi chi tiết từ click grid

            if (DataGridViewLop.SelectedRows.Count > 0)
            {
                DataGridViewRow row = DataGridViewLop.SelectedRows[0];
                cboMaLopNC.Text = row.Cells["MaLopNienChe"].Value?.ToString().Trim() ?? "";
                cboTenlop.Text = row.Cells["TenLop"].Value?.ToString() ?? "";
                cboNienkhoa.Text = row.Cells["NienKhoa"].Value?.ToString() ?? "";
                
                string maKhoa = row.Cells["MaKhoa"].Value?.ToString().Trim() ?? "";
                string maGV = row.Cells["MaGV"].Value?.ToString().Trim() ?? "";

                if (!string.IsNullOrEmpty(maKhoa))
                    cboKhoa2.SelectedValue = maKhoa;
                else
                    cboKhoa2.SelectedIndex = -1;

                if (!string.IsNullOrEmpty(maGV))
                    cboCV.SelectedValue = maGV;
                else
                    cboCV.SelectedIndex = -1;
            }
        }

        // 6. Xử lý sự kiện nhấn nút Thêm
        private void btnThem_Click(object sender, EventArgs e)
        {
            isAdding = true;
            isEditing = false;

            // Xóa trắng dữ liệu nhập
            cboMaLopNC.Text = "";
            cboTenlop.Text = "";
            cboNienkhoa.Text = "";
            cboKhoa2.SelectedIndex = -1;
            cboCV.SelectedIndex = -1;

            // Mở khóa nhập liệu
            cboMaLopNC.Enabled = true;
            cboTenlop.Enabled = true;
            cboNienkhoa.Enabled = true;
            cboKhoa2.Enabled = true;
            cboCV.Enabled = true;

            // Trạng thái các nút điều hướng (btnThem_Click)
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            btnReset.Enabled = true;

            cboMaLopNC.Focus();
        }

        // 7. Xử lý sự kiện nhấn nút Sửa
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (DataGridViewLop.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn lớp niên chế cần chỉnh sửa thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            isAdding = false;
            isEditing = true;

            // Mở khóa nhập liệu ngoại trừ Mã lớp (Khóa chính)
            cboMaLopNC.Enabled = false;
            cboTenlop.Enabled = true;
            cboNienkhoa.Enabled = true;
            cboKhoa2.Enabled = true;
            cboCV.Enabled = true;

            // Trạng thái các nút điều hướng (btnSua_Click)
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            btnReset.Enabled = true;

            cboTenlop.Focus();
        }

        // 9. Xử lý sự kiện nhấn nút Lưu
        private void btnLuu_Click(object sender, EventArgs e)
        {
            string maLop = cboMaLopNC.Text.Trim();
            string tenLop = cboTenlop.Text.Trim();
            string nienKhoa = cboNienkhoa.Text.Trim();

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(maLop))
            {
                MessageBox.Show("Mã lớp niên chế không được để trống.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaLopNC.Focus();
                return;
            }
            if (string.IsNullOrEmpty(tenLop))
            {
                MessageBox.Show("Tên lớp niên chế không được để trống.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTenlop.Focus();
                return;
            }
            if (string.IsNullOrEmpty(nienKhoa))
            {
                MessageBox.Show("Niên khóa không được để trống.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboNienkhoa.Focus();
                return;
            }
            if (cboKhoa2.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Khoa quản lý của lớp.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhoa2.Focus();
                return;
            }
            if (cboCV.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Giảng viên làm cố vấn học tập.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCV.Focus();
                return;
            }

            string maKhoa = cboKhoa2.SelectedValue.ToString();
            string maGV = cboCV.SelectedValue.ToString();

            try
            {
                if (isAdding)
                {
                    // Kiểm tra trùng khóa chính
                    if (bll.CheckKeyExist(maLop))
                    {
                        MessageBox.Show($"Mã lớp niên chế '{maLop}' đã tồn tại trong hệ thống. Vui lòng sử dụng mã khác.", "Trùng khóa chính", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboMaLopNC.Focus();
                        return;
                    }

                    bll.InsertLop(maLop, tenLop, nienKhoa, maKhoa, maGV);
                    MessageBox.Show("Thêm mới lớp niên chế thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (isEditing)
                {
                    bll.UpdateLop(maLop, tenLop, nienKhoa, maKhoa, maGV);
                    MessageBox.Show("Cập nhật thông tin lớp niên chế thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadData();
                ResetFormState();

                // Chọn lại dòng vừa được Lưu
                foreach (DataGridViewRow row in DataGridViewLop.Rows)
                {
                    if (row.Cells["MaLopNienChe"].Value?.ToString().Trim() == maLop)
                    {
                        row.Selected = true;
                        DataGridViewLop_CellClick(null, null);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu thông tin: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 10. Xử lý sự kiện nhấn nút Hủy
        private void btnHuy_Click(object sender, EventArgs e)
        {
            ClearDetailInputs();
            ResetFormState();
        }

        // 11. Xử lý sự kiện nhấn nút Reset
        private void btnReset_Click(object sender, EventArgs e)
        {
            if (isAdding)
            {
                ClearDetailInputs();
            }
            else if (isEditing)
            {
                if (DataGridViewLop.SelectedRows.Count > 0)
                {
                    DataGridViewRow row = DataGridViewLop.SelectedRows[0];
                    cboTenlop.Text = row.Cells["TenLop"].Value?.ToString() ?? "";
                    cboNienkhoa.Text = row.Cells["NienKhoa"].Value?.ToString() ?? "";
                    
                    string maKhoa = row.Cells["MaKhoa"].Value?.ToString().Trim() ?? "";
                    string maGV = row.Cells["MaGV"].Value?.ToString().Trim() ?? "";

                    if (!string.IsNullOrEmpty(maKhoa))
                        cboKhoa2.SelectedValue = maKhoa;
                    else
                        cboKhoa2.SelectedIndex = -1;

                    if (!string.IsNullOrEmpty(maGV))
                        cboCV.SelectedValue = maGV;
                    else
                        cboCV.SelectedIndex = -1;
                }
            }
        }

        // 12. Xử lý sự kiện nhấn nút Làm mới dữ liệu bảng và xóa bộ lọc
        private void btnLammoi_Click(object sender, EventArgs e)
        {
            cboKhoa.SelectedIndexChanged -= cboFilter_SelectedIndexChanged;
            cboGiangVien.SelectedIndexChanged -= cboFilter_SelectedIndexChanged;

            txtTimKiem.Text = "Tìm kiếm theo mã lớp, tên lớp ...";
            txtTimKiem.ForeColor = Color.Gray;

            cboKhoa.SelectedIndex = 0;
            cboGiangVien.SelectedIndex = 0;

            cboKhoa.SelectedIndexChanged += cboFilter_SelectedIndexChanged;
            cboGiangVien.SelectedIndexChanged += cboFilter_SelectedIndexChanged;

            LoadData();
            ClearDetailInputs();
            ResetFormState();
        }

        // 13. Áp dụng bộ lọc và tìm kiếm kết hợp
        private void ApplyFilter()
        {
            if (dtLop == null) return;

            string filter = "1=1";

            // Lọc theo Khoa
            if (cboKhoa.SelectedValue != null && cboKhoa.SelectedValue.ToString() != "ALL")
            {
                filter += $" AND MaKhoa = '{cboKhoa.SelectedValue}'";
            }

            // Lọc theo Cố vấn học tập (Giảng viên)
            if (cboGiangVien.SelectedValue != null && cboGiangVien.SelectedValue.ToString() != "ALL")
            {
                filter += $" AND MaGV = '{cboGiangVien.SelectedValue}'";
            }

            // Tìm kiếm theo từ khóa (Mã hoặc Tên lớp)
            string kw = txtTimKiem.Text.Trim();
            if (!string.IsNullOrEmpty(kw) && kw != "Tìm kiếm theo mã lớp, tên lớp ...")
            {
                string escapedKw = kw.Replace("'", "''");
                filter += $" AND (MaLopNienChe LIKE '%{escapedKw}%' OR TenLop LIKE '%{escapedKw}%')";
            }

            dtLop.DefaultView.RowFilter = filter;

            // Nếu danh sách lọc trống, xóa trắng các trường thông tin chi tiết
            if (DataGridViewLop.Rows.Count == 0)
            {
                ClearDetailInputs();
            }
            else
            {
                DataGridViewLop.ClearSelection();
                DataGridViewLop.CurrentCell = DataGridViewLop.Rows[0].Cells["MaLopNienChe"];
                DataGridViewLop.Rows[0].Selected = true;
                DataGridViewLop_CellClick(null, null);
            }
        }

        private void cboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        // Dọn dẹp nội dung các trường nhập liệu
        private void ClearDetailInputs()
        {
            cboMaLopNC.Text = "";
            cboTenlop.Text = "";
            cboNienkhoa.Text = "";
            cboKhoa2.SelectedIndex = -1;
            cboCV.SelectedIndex = -1;
        }

        // 14. Thiết lập sự kiện điều hướng thanh Sidebar
        private void OpenForm(Form targetForm)
        {
            if (targetForm != null)
            {
                targetForm.FormClosed += (s, ev) =>
                {
                    this.Show();
                    LoadData(); // Nạp lại dữ liệu lớp khi quay lại form
                };
                targetForm.Show();
                this.Hide();
            }
        }
<<<<<<< HEAD

        private void WireSidebarEvents()
        {
            // Đăng ký điều hướng cho các Form có sẵn và đã được kích hoạt biên dịch
            btnGiangvien.Click += (s, e) => OpenForm(new frmGiangvien());
            btnCanhbao.Click += (s, e) => OpenForm(new frmCanhBaoHocVu());
            btnLopnc.Click += (s, e) => { /* Đang ở chính form này */ };
            btnSinhvien.Click += (s, e) => OpenForm(new frmQuanLiThongTinSinhVien());
            btnMon.Click += (s, e) => OpenForm(new frmMonhoc());
            btnLophp.Click += (s, e) => OpenForm(new frmLophocphan());
            btnKetqua.Click += (s, e) => OpenForm(new frmKetQuaHocTap());
            btnPhuckhao.Click += (s, e) =>
            {
                if (SessionHelper.MaVaiTro == "VT001")
                    OpenForm(new QLDSV.GUI.Forms.Admin.frmPhucKhao_Admin());
                else if (SessionHelper.MaVaiTro == "VT002")
                    OpenForm(new QLDSV.GUI.Forms.GiangVien.frmPhucKhao_GV());
                else if (SessionHelper.MaVaiTro == "VT003")
                    OpenForm(new QLDSV.GUI.Forms.SinhVien.frmPhucKhao_SV());
            };
            btnBaocao.Click += (s, e) => OpenForm(new frmBaoCaoThongKe());
            btnTongquan.Click += (s, e) => OpenForm(new frmTongQuan());

            // Hiển thị thông báo với các Form chưa kích hoạt hoặc đang phát triển
            btnDangky.Click += (s, e) => MessageBox.Show("Tính năng Đăng ký lớp đang được phát triển!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnDiem.Click += (s, e) => MessageBox.Show("Tính năng Nhập điểm đang được phát triển!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Làm nổi bật nút Lớp niên chế (Trực quan hóa thiết kế)
            btnLopnc.FillColor = Color.FromArgb(224, 224, 224);
            btnLopnc.ForeColor = Color.Black;
            btnLopnc.Font = new Font(btnLopnc.Font, FontStyle.Bold);
        }
=======
>>>>>>> db4428ec62895fa5581eeaa3f69767735ca37c59
    }
}
