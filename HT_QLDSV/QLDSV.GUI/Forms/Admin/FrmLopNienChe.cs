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

            ThemeHelper.ApplyTheme(this);

            txtTimKiem.Text = "Tìm kiếm theo mã lớp, tên lớp ...";

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

            DataGridViewLop.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }


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

                
                cboKhoa.SelectedIndexChanged += cboFilter_SelectedIndexChanged;
                cboGiangVien.SelectedIndexChanged += cboFilter_SelectedIndexChanged;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi nạp combobox bộ lọc: " + ex.Message);
            }
        }

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

        private void ResetFormState()
        {
            isAdding = false;
            isEditing = false;

            
            cboMaLopNC.Enabled = false; 
            cboTenlop.Enabled = false; 
            cboNienkhoa.Enabled = false; 
            cboKhoa2.Enabled = false; 
            cboCV.Enabled = false;

            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            btnHuy.Enabled = false;
            btnReset.Enabled = false;

            
            if (DataGridViewLop.SelectedRows.Count > 0)
            {
                DataGridViewLop_CellClick(null, null);
            }
        }

        private void DataGridViewLop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (isAdding)
            {
                DataGridViewLop.ClearSelection();
                ShowAddingBlockedMessage();
                return;
            }

            if (isEditing) return;

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

        private void btnThem_Click(object sender, EventArgs e)
        {
            isAdding = true;
            isEditing = false;

            ClearDetailInputs(selectFirstCombo: true);

            // Mở khóa nhập liệu
            cboMaLopNC.Enabled = true;
            cboTenlop.Enabled = true;
            cboNienkhoa.Enabled = true;
            cboKhoa2.Enabled = true;
            cboCV.Enabled = true;

           
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            btnReset.Enabled = true;

            DataGridViewLop.ClearSelection();

            cboMaLopNC.Focus();
        }

        private void ShowAddingBlockedMessage()
        {
            MessageBox.Show(
                "Đang ở trạng thái thêm mới.\r\nVui lòng nhấn Lưu hoặc Hủy trước khi chọn dòng trên danh sách.",
                "Không thể chọn danh sách",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (DataGridViewLop.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn lớp niên chế cần chỉnh sửa thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            isAdding = false;
            isEditing = true;

            
            cboMaLopNC.Enabled = false;
            cboTenlop.Enabled = true;
            cboNienkhoa.Enabled = true;
            cboKhoa2.Enabled = true;
            cboCV.Enabled = true;

            
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            btnReset.Enabled = false;

            cboTenlop.Focus();
        }

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

        private void btnHuy_Click(object sender, EventArgs e)
        {
            ResetFormState();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (!isAdding) return;

            ClearDetailInputs(selectFirstCombo: true);
            cboMaLopNC.Focus();
        }

        private void btnLammoi_Click(object sender, EventArgs e)
        {
            cboKhoa.SelectedIndexChanged -= cboFilter_SelectedIndexChanged;
            cboGiangVien.SelectedIndexChanged -= cboFilter_SelectedIndexChanged;

            txtTimKiem.Text = "Tìm kiếm theo mã lớp, tên lớp ...";
            

            cboKhoa.SelectedIndex = 0;
            cboGiangVien.SelectedIndex = 0;

            cboKhoa.SelectedIndexChanged += cboFilter_SelectedIndexChanged;
            cboGiangVien.SelectedIndexChanged += cboFilter_SelectedIndexChanged;

            LoadData();
            ClearDetailInputs();
            ResetFormState();
        }

        private void ApplyFilter()
        {
            if (dtLop == null) return;

            string filter = "1=1";

            if (cboKhoa.SelectedValue != null && cboKhoa.SelectedValue.ToString() != "ALL")
            {
                filter += $" AND MaKhoa = '{cboKhoa.SelectedValue}'";
            }

            if (cboGiangVien.SelectedValue != null && cboGiangVien.SelectedValue.ToString() != "ALL")
            {
                filter += $" AND MaGV = '{cboGiangVien.SelectedValue}'";
            }

            string kw = txtTimKiem.Text.Trim();
            if (!string.IsNullOrEmpty(kw) && kw != "Tìm kiếm theo mã lớp, tên lớp ...")
            {
                string escapedKw = kw.Replace("'", "''");
                filter += $" AND (MaLopNienChe LIKE '%{escapedKw}%' OR TenLop LIKE '%{escapedKw}%')";
            }

            dtLop.DefaultView.RowFilter = filter;

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

        private void ClearDetailInputs(bool selectFirstCombo = false)
        {
            cboMaLopNC.Text = "";
            cboTenlop.Text = "";
            cboNienkhoa.Text = "";

            if (selectFirstCombo)
            {
                if (cboKhoa2.Items.Count > 0) cboKhoa2.SelectedIndex = 0;
                if (cboCV.Items.Count > 0) cboCV.SelectedIndex = 0;
            }
            else
            {
                cboKhoa2.SelectedIndex = -1;
                cboCV.SelectedIndex = -1;
            }
        } 
    }
}
