# 📘 VANPHONG.MD — Tài liệu Văn phong Code & Comment
## Môn: Lập trình .NET | Học viện Ngân hàng | HK II 2025–2026

---

## 1. QUY TẮC ĐẶT TÊN (Naming Convention)

### 1.1 Class (Lớp)
- Dùng **PascalCase**: chữ cái đầu mỗi từ viết HOA.
- Tên lớp phải **rõ nghĩa**, phản ánh đúng chức năng.

```csharp
// ✅ Đúng
public class SachGiaoKhoa { }
public class PhongKhachSan { }
public class NhanVien { }

// ❌ Sai
public class sgk { }
public class pksd { }
```

### 1.2 Phương thức (Method)
- Dùng **PascalCase**.
- Tên phải mô tả rõ **hành động** mà phương thức thực hiện.

```csharp
// ✅ Đúng
public void LayThongTin() { }
public double TinhTongTien() { }
public bool KiemTraDuLieu() { }

// ❌ Sai (tên không dấu, viết thường, viết tắt khó hiểu)
public void laythongtin() { }
public double tt() { }
```

### 1.3 Thuộc tính / Biến thành viên (Field)
- Biến **private**: dùng **camelCase**.
- Biến **public**: dùng **PascalCase** hoặc camelCase.
- Tên biến phải có nghĩa rõ ràng.

```csharp
// ✅ Đúng
private string tenSach;
private double giaBan;
private int soLuong;
public bool TrangThai;

// ❌ Sai (viết tắt khó đọc)
string ts;
double gb;
int sl;
```

### 1.4 Biến cục bộ (Local Variable)
- Dùng **camelCase**.

```csharp
// ✅ Đúng
string tenSach = txtTenSach.Text;
double tongTien = giaBan * soLuong;

// ❌ Sai
string TenSach = txtTenSach.Text;
double TongTien = giaBan * soLuong;
```

### 1.5 Tên điều khiển trên Form (Controls)
| Loại control | Tiền tố | Ví dụ |
|---|---|---|
| TextBox | `txt` | `txtTenSach`, `txtGiaBan` |
| Button | `btn` | `btnTinhTien`, `btnHienThi`, `btnThoat` |
| Label | `lbl` | `lblTenSach`, `lblGiaBan` |
| ComboBox | `cbo` | `cboLoaiSach` |
| CheckBox | `chk` | `chkAnSang`, `chkDuaDon` |
| RadioButton | `rad` | `radMoi`, `radCu` |
| DataGridView | `dgv` | `dgvDanhSach` |
| ListBox | `lst` | `lstDichVu` |

---

## 2. QUY TẮC VIẾT COMMENT

### 2.0 Nguyên tắc chung

| Quy tắc | Giải thích |
|---|---|
| **Chỉ comment điểm chính** | Không comment thứ đọc code đã hiểu ngay |
| **Được viết tắt** | `vd`, `frm`, `ht` (hiển thị), `kt` (kiểm tra), `kn` (kết nối), `tt` (tổng tiền), `sl`, `gb`, `nv`, `mh`, `dl`, `csdl`... |
| **Không comment thừa** | `i++; // tăng i` → ❌ bỏ đi |
| **Không dùng ký tự `—`** | Viết `//` rồi thẳng vào nội dung, không thêm `—` phân cách |
| **Không comment inline** | Không viết `code(); // giải thích` bên cạnh lệnh. Comment phải đứng trên dòng riêng |
| **Comment trên method** | 1 dòng mô tả mục đích |
| **Comment trên nút bấm** | Tiêu đề IN HOA: `// NÚT TÍNH TIỀN` |
| **Comment logic phức tạp** | Giải thích công thức, điều kiện đặc biệt |

> **Ví dụ:**
> ```csharp
> // ❌ Sai — comment inline bên cạnh code
> DongKetNoi(); // Luôn chạy dù có lỗi
> TaiDuLieu();  // Refresh lại DataGridView
>
> // ✅ Đúng — comment trên dòng riêng (nếu cần)
> // Đóng kết nối
> DongKetNoi();
> ```

### 2.1 Comment mô tả phương thức
Viết **1 dòng** ngay trên method, mô tả **mục đích** — không mô tả từng dòng bên trong.

```csharp
// ✅ Đúng — 1 dòng mô tả mục đích, viết tắt được
// Tính tt = gb * sl
public virtual double TongTien()
{
    return giaBan * soLuong;
}

// ❌ Sai — comment thừa, đọc code đã hiểu
public virtual double TongTien()
{
    return giaBan * soLuong; // nhân giá bán với số lượng
}
```

### 2.2 Comment mô tả lớp kế thừa
```csharp
// Kế thừa Sach — thêm TrangThai (mới/cũ)
public class SachGiaoKhoa : Sach
{
    public bool TrangThai; // true = cũ
}
```

### 2.3 Comment nhóm thuộc tính
```csharp
// Thuộc tính
public string TenSach;
public string TacGia;
public double GiaBan;
public int SoLuong;
```

### 2.4 Comment mô tả constructor
```csharp
// Constructor không tham số
public Sach() { TenSach = ""; TacGia = ""; GiaBan = 0; SoLuong = 0; }

// Constructor có tham số
public Sach(string ten, string tg, double gb, int sl)
{
    this.TenSach = ten; this.TacGia = tg;
    this.GiaBan  = gb;  this.SoLuong = sl;
}
```

### 2.5 Comment trong sự kiện Form (Event Handler)
Tiêu đề IN HOA phía trên event. Bên trong chỉ comment **logic không hiển nhiên**.

```csharp
// NÚT TÍNH TIỀN
private void btnTinhTien_Click(object sender, EventArgs e)
{
    if (!KiemTraSo()) return;
    SachGiaoKhoa a = new SachGiaoKhoa(
        txtTenSach.Text, txtTacGia.Text,
        Convert.ToDouble(txtGiaBan.Text),
        Convert.ToInt32(txtSoLuong.Text),
        radCu.Checked);
    txtTongTien.Text = a.TongTien().ToString();
    btnHienThi.Enabled = true;
}

// NÚT HIỂN THỊ
private void btnHienThi_Click(object sender, EventArgs e)
{
    SachGiaoKhoa a = new SachGiaoKhoa(...);
    a.LayThongTin();
}

// NÚT THOÁT
private void btnThoat_Click(object sender, EventArgs e)
{
    if (MessageBox.Show("Bạn có chắc muốn thoát?", "Xác nhận",
        MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
        Application.Exit();
}
```

### 2.6 Comment hàm kiểm tra dữ liệu
```csharp
// Ẩn btn TinhTien nếu có ô rỗng
private void KiemTraDuLieu()
{
    bool hopLe = !string.IsNullOrWhiteSpace(txtTenSach.Text) &&
                 !string.IsNullOrWhiteSpace(txtTacGia.Text) &&
                 !string.IsNullOrWhiteSpace(txtGiaBan.Text) &&
                 !string.IsNullOrWhiteSpace(txtSoLuong.Text);
    btnTinhTien.Enabled = hopLe;
    txtTongTien.Text = "";
}

// KT định dạng số & không âm
private bool KiemTraSo()
{
    try
    {
        if (Convert.ToInt32(txtSoLuong.Text) < 0)
        {
            MessageBox.Show("Số lượng không được âm!", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtSoLuong.Focus(); return false;
        }
        if (Convert.ToDouble(txtGiaBan.Text) < 0)
        {
            MessageBox.Show("Giá bán không được âm!", "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtGiaBan.Focus(); return false;
        }
    }
    catch
    {
        MessageBox.Show("Giá bán & số lượng phải là số!", "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
    }
    return true;
}
```

### 2.7 Comment thao tác CSDL (ADO.NET)
```csharp
// Mở kết nối tới cơ sở dữ liệu
private void MoKetNoi()
{
    conn = new SqlConnection(connectionString);
    conn.Open();
}

// Đóng kết nối cơ sở dữ liệu
private void DongKetNoi()
{
    if (conn != null && conn.State == ConnectionState.Open)
        conn.Close();
}

// Đọc dữ liệu từ bảng và hiển thị lên DataGridView
private void TaiDuLieu()
{
    string sql = "SELECT * FROM HangHoa";
    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
    DataTable dt = new DataTable();
    da.Fill(dt);
    dgvDanhSach.DataSource = dt;
}

// Thêm bản ghi mới vào cơ sở dữ liệu
private void ThemMoi()
{
    string sql = "INSERT INTO HangHoa (MaHang, TenHang, Gia) VALUES (@ma, @ten, @gia)";
    SqlCommand cmd = new SqlCommand(sql, conn);
    cmd.Parameters.AddWithValue("@ma", txtMaHang.Text);
    cmd.Parameters.AddWithValue("@ten", txtTenHang.Text);
    cmd.Parameters.AddWithValue("@gia", Convert.ToDouble(txtGia.Text));
    cmd.ExecuteNonQuery();
}

// Cập nhật bản ghi hiện tại (không cho sửa Mã hàng)
private void CapNhat()
{
    string sql = "UPDATE HangHoa SET TenHang=@ten, Gia=@gia WHERE MaHang=@ma";
    SqlCommand cmd = new SqlCommand(sql, conn);
    cmd.Parameters.AddWithValue("@ten", txtTenHang.Text);
    cmd.Parameters.AddWithValue("@gia", Convert.ToDouble(txtGia.Text));
    cmd.Parameters.AddWithValue("@ma", txtMaHang.Text);
    cmd.ExecuteNonQuery();
}

// Xóa bản ghi hiện tại
private void XoaBanGhi()
{
    string sql = "DELETE FROM HangHoa WHERE MaHang=@ma";
    SqlCommand cmd = new SqlCommand(sql, conn);
    cmd.Parameters.AddWithValue("@ma", txtMaHang.Text);
    cmd.ExecuteNonQuery();
}
```

---

## 3. CẤU TRÚC TỔ CHỨC CODE

### 3.1 Thứ tự khai báo trong Class
```csharp
public class TenLop
{
    // 1. Khai báo thuộc tính (Fields)
    private string tenThuocTinh;

    // 2. Hàm khởi tạo không tham số
    public TenLop() { }

    // 3. Hàm khởi tạo có tham số
    public TenLop(string ten) { }

    // 4. Properties (nếu có)
    public string TenThuocTinh
    {
        get { return tenThuocTinh; }
        set { tenThuocTinh = value; }
    }

    // 5. Phương thức (Methods)
    public void PhuongThuc1() { }
    public double PhuongThuc2() { }
}
```

### 3.2 Thứ tự khai báo trong Form
```csharp
public partial class FrmTenForm : Form
{
    // 1. Biến dùng chung trong form
    private SqlConnection conn;
    private string connectionString = "...";

    // 2. Constructor
    public FrmTenForm() { InitializeComponent(); }

    // 3. Form Load
    private void FrmTenForm_Load(object sender, EventArgs e) { }

    // 4. Sự kiện DataGridView
    private void dgvDanhSach_CellClick(object sender, DataGridViewCellEventArgs e) { }

    // 5. Sự kiện TextChanged (kiểm tra dữ liệu)
    private void txt_TextChanged(object sender, EventArgs e) { }

    // 6. Sự kiện nút bấm (theo thứ tự: Thêm, Lưu, Sửa, Xóa, Huỷ, Thoát)
    private void btnThemMoi_Click(object sender, EventArgs e) { }
    private void btnLuu_Click(object sender, EventArgs e) { }
    private void btnSua_Click(object sender, EventArgs e) { }
    private void btnXoa_Click(object sender, EventArgs e) { }
    private void btnHuyBo_Click(object sender, EventArgs e) { }
    private void btnThoat_Click(object sender, EventArgs e) { }

    // 7. Phương thức hỗ trợ (private helpers)
    private void MoKetNoi() { }
    private void DongKetNoi() { }
    private void TaiDuLieu() { }
    private bool KiemTraDuLieu() { }
    private bool KiemTraSo() { }
}
```

---

## 4. MẪU COMMENT CHO BÀI THI / KIỂM TRA

### 4.1 Header file (đầu mỗi file .cs)
```csharp
// ============================================================
// Họ và tên : [Họ tên sinh viên]
// MSSV      : [Mã số sinh viên]
// Lớp       : [Tên lớp]
// Bài       : [Tên bài/Mã đề]
// Ngày      : [Ngày làm bài]
// ============================================================
```

### 4.2 Mô tả yêu cầu bài toán (đầu class)
```csharp
// Bài toán: Quản lý sách giáo khoa
// Thuộc tính: Tên sách, Tác giả, Giá bán, Số lượng, Trạng thái (mới/cũ)
// Phương thức: LayThongTin(), TongTien()
// Kế thừa từ: Sach (lớp cơ sở)
```

### 4.3 Giải thích logic phức tạp
```csharp
// Nếu sách cũ thì giảm 30% giá
// Công thức: TongTien = GiaBan * SoLuong * 0.7
public override double TongTien()
{
    double tong = base.TongTien();
    if (TrangThai) tong = tong * 0.7;
    return tong;
}
```

---

## 5. CÁC LỖI THƯỜNG GẶP VÀ CÁCH XỬ LÝ

### 5.1 Kiểm tra TextBox không rỗng
```csharp
// Kiểm tra tất cả Textbox không được bỏ trống
if (string.IsNullOrWhiteSpace(txtTenHang.Text))
{
    MessageBox.Show("Vui lòng nhập Tên hàng!", "Thông báo",
        MessageBoxButtons.OK, MessageBoxIcon.Warning);
    txtTenHang.Focus();
    return;
}
```

### 5.2 Kiểm tra trùng khóa khi thêm mới
```csharp
// Kiểm tra Mã hàng đã tồn tại chưa trước khi thêm
private bool KiemTraTrungKhoa(string maHang)
{
    string sql = "SELECT COUNT(*) FROM HangHoa WHERE MaHang = @ma";
    SqlCommand cmd = new SqlCommand(sql, conn);
    cmd.Parameters.AddWithValue("@ma", maHang);
    int count = (int)cmd.ExecuteScalar();
    return count > 0;
}
```

### 5.3 Xử lý ngoại lệ khi chuyển kiểu
```csharp
// Dùng try-catch khi Convert để tránh crash chương trình
try
{
    double gia = Convert.ToDouble(txtGiaBan.Text);
    int sl = Convert.ToInt32(txtSoLuong.Text);
}
catch (FormatException)
{
    MessageBox.Show("Giá bán và số lượng phải là kiểu số!", "Lỗi",
        MessageBoxButtons.OK, MessageBoxIcon.Error);
    return;
}
```

### 5.4 Đảm bảo đóng kết nối (dùng try-finally)
```csharp
// Đóng kết nối trong finally dù có lỗi hay không
try
{
    MoKetNoi();
    ThemMoi();
    TaiDuLieu();
    MessageBox.Show("Thêm thành công!", "Thông báo");
}
catch (Exception ex)
{
    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
        MessageBoxButtons.OK, MessageBoxIcon.Error);
}
finally
{
    DongKetNoi();
}
```

---

## 6. CHECKLIST TRƯỚC KHI NỘP BÀI

- [ ] Tên lớp, phương thức dùng **PascalCase** có dấu tiếng Việt
- [ ] Biến private dùng **camelCase**
- [ ] Tên điều khiển (controls) đúng tiền tố (`txt`, `btn`, `lbl`, `cbo`, `dgrid`...)
- [ ] Mỗi phương thức có **comment mô tả** phía trên
- [ ] Các nút bấm có **comment tiêu đề** (`// NÚT THÊM MỚI`, `// NÚT LƯU`...)
- [ ] Hàm kiểm tra dữ liệu có giải thích logic
- [ ] Xử lý **ngoại lệ** (try-catch) khi Convert kiểu
- [ ] Kiểm tra **trùng khóa** khi Thêm mới
- [ ] Không cho sửa **Mã** khi cập nhật (txtMahang.Enabled = false)
- [ ] Kết nối CSDL luôn được **đóng** sau khi dùng (finally)
- [ ] Confirm trước khi **Thoát / Xóa** bằng MessageBox
- [ ] Nút **Lưu / Bỏ qua** chỉ Enabled khi đang ở chế độ Thêm/Sửa
- [ ] Sau khi Lưu thành công phải **reset form** và **load lại DataGridView**
- [ ] **DataGridView** set `AllowUserToAddRows = false` và `EditMode = EditProgrammatically`
- [ ] **ComboBox** sau khi fillcombo phải đặt `SelectedIndex = -1`
- [ ] **class Functions** (nếu dùng) phải có comment mô tả từng hàm tiện ích

---

## 7. MẪU CLASS FUNCTIONS DÙNG CHUNG (Từ bài thực tế Ontap2904)

> Đây là mẫu class `Functions` tĩnh thường dùng trong đề thi thực hành.
> Tất cả các phương thức đều là `public static` để gọi trực tiếp từ Form.

```csharp
// Class tiện ích: kn DB, đọc DataTable, fill ComboBox, kt trùng khóa, thực thi SQL
internal class Functions
{
    public static SqlConnection conn;
    public static string connstring;

    // Mở kn tới SQL Server, gọi 1 lần trong Form_Load
    public static void ketnoi()
    {
        connstring = "Data Source=TÊN_SERVER;Initial Catalog=TÊN_DB;Integrated Security=True;Encrypt=False";
        conn = new SqlConnection();
        conn.ConnectionString = connstring;
        conn.Open();
    }

    // SELECT trả về DataTable
    public static DataTable getdatatotable(string sql)
    {
        SqlDataAdapter mydata = new SqlDataAdapter(sql, Functions.conn);
        DataTable table = new DataTable();
        mydata.Fill(table);
        return table;
    }

    // Nạp dl vào ComboBox (ma=ValueMember, ten=DisplayMember)
    public static void fillcombo(string sql, ComboBox cbo, string ma, string ten)
    {
        SqlDataAdapter mydata = new SqlDataAdapter(sql, Functions.conn);
        DataTable table = new DataTable();
        mydata.Fill(table);
        cbo.DataSource = table;
        cbo.ValueMember = ma;
        cbo.DisplayMember = ten;
    }

    // Lấy 1 giá trị string từ câu truy vấn
    public static string getfieldvalue(string sql)
    {
        string ketqua = "";
        SqlCommand cmd = new SqlCommand(sql, Functions.conn);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            ketqua = reader.GetValue(0).ToString();
        }
        reader.Close();
        return ketqua;
    }

    // KT trùng khóa, true = đã tồn tại
    public static bool checkkey(string sql)
    {
        SqlDataAdapter mydata = new SqlDataAdapter(sql, Functions.conn);
        DataTable table = new DataTable();
        mydata.Fill(table);
        return table.Rows.Count > 0;
    }

    // Thực thi INSERT / UPDATE / DELETE
    public static void runsql(string sql)
    {
        SqlCommand cmd = new SqlCommand();
        cmd.Connection = Functions.conn;
        cmd.CommandText = sql;
        try
        {
            cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi thực thi SQL: " + ex.Message, "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            cmd.Dispose();
        }
    }
}
```

---

## 8. MẪU FORM QUẢN LÝ CRUD ĐẦY ĐỦ (Từ bài thực tế Ontap2904)

### 8.1 Luồng điều khiển trạng thái nút

> **Nguyên tắc:** Các nút chỉ Enabled khi đúng chế độ — tránh thao tác nhầm.

| Trạng thái | btnThem | btnSua | btnXoa | btnLuu | btnBoqua | txtMa |
|---|---|---|---|---|---|---|
| Mặc định (xem) | ✅ | ✅ | ✅ | ❌ | ❌ | ❌ |
| Đang Thêm mới | ❌ | ❌ | ❌ | ✅ | ✅ | ✅ |
| Sau khi Lưu | ✅ | ✅ | ✅ | ❌ | ❌ | ❌ |

```csharp
// NÚT THÊM
private void btnThem_Click(object sender, EventArgs e)
{
    btnThem.Enabled = false;
    btnSua.Enabled  = false;
    btnXoa.Enabled  = false;
    btnLuu.Enabled  = true;
    btnBoqua.Enabled = true;
    ResetForm();
    txtMahang.Enabled = true;
    txtMahang.Focus();
}

// NÚT BỎ QUA
private void btnBoqua_Click(object sender, EventArgs e)
{
    btnBoqua.Enabled = false;
    btnLuu.Enabled   = false;
    btnThem.Enabled  = true;
    btnSua.Enabled   = true;
    btnXoa.Enabled   = true;
    txtMahang.Enabled = false;
    ResetForm();
}
```

### 8.2 Mẫu nút Lưu có kiểm tra đầy đủ

```csharp
// NÚT LƯU
private void btnLuu_Click(object sender, EventArgs e)
{
    // KT rỗng
    if (txtMahang.Text == "")
    {
        MessageBox.Show("Bạn phải nhập mã hàng!", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        txtMahang.Focus(); return;
    }
    if (txtTenhang.Text == "")
    {
        MessageBox.Show("Bạn phải nhập tên hàng!", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        txtTenhang.Focus(); return;
    }
    if (cboMamau.Text == "")
    {
        MessageBox.Show("Bạn phải chọn màu!", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
    }

    // KT trùng khóa
    string sqlCheck = "SELECT Mahang FROM tblHanghoa WHERE Mahang=N'" + txtMahang.Text + "'";
    if (Functions.checkkey(sqlCheck))
    {
        MessageBox.Show("Mã hàng đã tồn tại!", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        txtMahang.Text = ""; txtMahang.Focus(); return;
    }

    // INSERT
    string sql = "INSERT INTO tblHanghoa(Mahang,Tenhang,Mamau,Soluong,Thoigianbaohanh) " +
                 "VALUES(N'" + txtMahang.Text + "',N'" + txtTenhang.Text + "'," +
                 "N'" + cboMamau.SelectedValue + "'," + txtSoluong.Text + "," + txtTGBH.Text + ")";
    Functions.runsql(sql);
    LoadDataGridView();
    ResetForm();
    btnXoa.Enabled = true; btnSua.Enabled = true; btnThem.Enabled = true;
    btnLuu.Enabled = false; btnBoqua.Enabled = false; txtMahang.Enabled = false;
    MessageBox.Show("Thêm mới thành công!", "Thông báo",
        MessageBoxButtons.OK, MessageBoxIcon.Information);
}
```

### 8.3 Mẫu load DataGridView có đặt Header và chặn sửa trực tiếp

```csharp
// Load dl lên dgridHH
private void LoadDataGridView()
{
    string sql = "SELECT * FROM tblHanghoa";
    tblhh = Functions.getdatatotable(sql);
    dgridHH.DataSource = tblhh;
    dgridHH.Columns[0].HeaderText = "Mã hàng";
    dgridHH.Columns[1].HeaderText = "Tên hàng";
    dgridHH.Columns[2].HeaderText = "Mã màu";
    dgridHH.Columns[3].HeaderText = "Số lượng";
    dgridHH.Columns[4].HeaderText = "Thời gian bảo hành";
    dgridHH.AllowUserToAddRows = false;
    dgridHH.EditMode = DataGridViewEditMode.EditProgrammatically;
}
```

### 8.4 Mẫu click vào DataGridView để hiển thị lên form

```csharp
// Click dgrid -> đổ dl lên form
private void dgridHH_Click(object sender, EventArgs e)
{
    if (btnThem.Enabled == false)
    {
        MessageBox.Show("Đang ở chế độ thêm mới!",
            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return;
    }
    if (tblhh.Rows.Count == 0) return;
    txtMahang.Text  = dgridHH.CurrentRow.Cells["Mahang"].Value.ToString();
    txtTenhang.Text = dgridHH.CurrentRow.Cells["Tenhang"].Value.ToString();
    txtSoluong.Text = dgridHH.CurrentRow.Cells["Soluong"].Value.ToString();
    txtTGBH.Text    = dgridHH.CurrentRow.Cells["Thoigianbaohanh"].Value.ToString();
    string maMau = dgridHH.CurrentRow.Cells["Mamau"].Value.ToString();
    cboMamau.Text = Functions.getfieldvalue(
        "SELECT Tenmau FROM tblMausac WHERE Mamau=N'" + maMau + "'");
}
```

### 8.5 Mẫu Form_Load khởi tạo đúng thứ tự

```csharp
// Form_Load: kn -> cài nút -> fill combo -> load grid
private void FrmHanghoa_Load(object sender, EventArgs e)
{
    Functions.ketnoi();
    txtMahang.Enabled = false;
    btnLuu.Enabled    = false;
    btnBoqua.Enabled  = false;
    Functions.fillcombo("SELECT Mamau, Tenmau FROM tblMausac",
        cboMamau, "Mamau", "Tenmau");
    cboMamau.SelectedIndex = -1;
    LoadDataGridView();
}
```

---

## 9. LỖI THƯỜNG GẶP TRONG ĐỀ THI & CÁCH PHÒNG TRÁNH

### 9.1 Quên `reader.Close()` sau khi dùng SqlDataReader
```csharp
// ❌ SAI
SqlDataReader reader = cmd.ExecuteReader();
while (reader.Read()) { ... }

// ✅ ĐÚNG
SqlDataReader reader = cmd.ExecuteReader();
while (reader.Read()) { ... }
reader.Close();
```

### 9.2 Dùng string ghép SQL thay vì Parameters (SQL Injection)
```csharp
// ❌ KHÔNG AN TOÀN (nhưng vẫn chấp nhận trong đề thi thực hành)
string sql = "SELECT * FROM tblHanghoa WHERE Mahang='" + txtMahang.Text + "'";

// ✅ AN TOÀN HƠN (dùng trong dự án thực tế)
string sql = "SELECT * FROM tblHanghoa WHERE Mahang=@ma";
SqlCommand cmd = new SqlCommand(sql, conn);
cmd.Parameters.AddWithValue("@ma", txtMahang.Text);
```

### 9.3 Không set `SelectedIndex = -1` sau khi fillcombo
```csharp
// ❌ SAI — ComboBox tự chọn dòng đầu tiên, dễ nhầm dữ liệu
Functions.fillcombo("SELECT ...", cboMamau, "Mamau", "Tenmau");

// ✅ ĐÚNG
Functions.fillcombo("SELECT ...", cboMamau, "Mamau", "Tenmau");
cboMamau.SelectedIndex = -1;
```

### 9.4 Không kiểm tra chế độ khi click DataGridView
```csharp
// ❌ SAI — click grid khi đang thêm mới sẽ ghi đè dữ liệu đang nhập
private void dgridHH_Click(object sender, EventArgs e)
{
    txtMahang.Text = dgridHH.CurrentRow.Cells["Mahang"].Value.ToString();
    // ...
}

// ✅ ĐÚNG — chặn click khi đang ở chế độ Thêm
private void dgridHH_Click(object sender, EventArgs e)
{
    if (btnThem.Enabled == false) // btnThem bị disable = đang thêm mới
    {
        MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
        return;
    }
    // ... đổ dữ liệu bình thường
}
```

### 9.5 Không reset form sau khi Lưu / Bỏ qua
```csharp
// Hàm reset toàn bộ ô nhập về trạng thái trống
private void ResetForm()
{
    txtMahang.Text  = "";
    txtTenhang.Text = "";
    txtSoluong.Text = "";
    txtTGBH.Text    = "";
    cboMamau.SelectedIndex = -1; // Bỏ chọn ComboBox
}
```

---

## 6. CHECKLIST TRƯỚC KHI NỘP BÀI (CẬP NHẬT)

- [ ] Tên lớp, phương thức dùng **PascalCase** có dấu tiếng Việt
- [ ] Biến private dùng **camelCase**
- [ ] Tên điều khiển đúng tiền tố (`txt`, `btn`, `lbl`, `cbo`, `dgrid`...)
- [ ] Mỗi phương thức có **comment mô tả** phía trên
- [ ] Các nút bấm có **comment tiêu đề** (`// NÚT THÊM`, `// NÚT LƯU`...)
- [ ] **Form_Load**: kết nối → cài nút → fill combo → load grid (đúng thứ tự)
- [ ] **btnThem_Click**: disable Thêm/Sửa/Xóa, enable Lưu/Bỏqua, txtMa.Enabled=true
- [ ] **btnBoqua_Click**: trả lại trạng thái mặc định + ResetForm()
- [ ] **btnLuu_Click**: kiểm tra rỗng → kiểm tra trùng khóa → INSERT → LoadGrid → ResetForm
- [ ] **dgridHH_Click**: chặn khi `btnThem.Enabled == false`
- [ ] Sau loadGrid: `AllowUserToAddRows=false`, `EditMode=EditProgrammatically`
- [ ] Sau fillcombo: `cboMamau.SelectedIndex = -1`
- [ ] `reader.Close()` sau mọi `SqlDataReader`
- [ ] Xử lý **ngoại lệ** (try-catch) trong `runsql()`
- [ ] Confirm `MessageBox.YesNo` trước khi **Thoát / Xóa**

---

## 10. PATTERN NÂNG CAO TỪ DỰ ÁN QLBH_2026

> Project QLBH_2026 là ví dụ điển hình về ứng dụng **đa Form, đa namespace**.
> Bao gồm: FrmMain (menu chính), FrmChatlieu, FrmHanghoa, FrmNV + class Functions riêng.

### 10.1 Tổ chức namespace phân cấp (Class / Forms tách thư mục)

```
QLBH_2026/
├── Class/
│   └── Functions.cs       → namespace QLBH_2026.Class
└── Forms/
    ├── FrmMain.cs          → namespace QLBH_2026
    ├── FrmChatlieu.cs      → namespace QLBH_2026.Forms
    ├── FrmHanghoa.cs       → namespace QLBH_2026.Forms
    └── FrmNV.cs            → namespace QLBH_2026.Forms
```

```csharp
// Cách gọi Functions từ Form khác namespace
using QLBH_2026.Class;   // thêm using để gọi ngắn: Functions.Ketnoi()
// hoặc gọi đầy đủ không cần using:
Class.Functions.GetDataToTable(sql);
```

### 10.2 Kết nối tập trung tại FrmMain — ngắt kết nối khi thoát

```csharp
// FrmMain_Load — Kết nối 1 lần duy nhất khi khởi động app
private void FrmMain_Load(object sender, EventArgs e)
{
    Class.Functions.Ketnoi(); // Mở kết nối toàn cục dùng chung cho mọi Form con
}

// Nút Thoát — đóng kết nối rồi mới tắt app
private void thoatToolStripMenuItem_Click(object sender, EventArgs e)
{
    Class.Functions.ngatketnoi(); // Giải phóng connection trước khi exit
    Application.Exit();
}
```

```csharp
// Hàm ngắt kết nối — kiểm tra trạng thái trước khi đóng
public static void ngatketnoi()
{
    if (conn.State == ConnectionState.Open)
    {
        conn.Close();
        conn.Dispose(); // Giải phóng tài nguyên
        conn = null;    // Đặt về null để tránh dùng lại
    }
}
```

### 10.3 Tách `RunSqldel` riêng cho thao tác XÓA (bắt lỗi khóa ngoại)

> Khi xóa bản ghi có **khóa ngoại** (FK), SQL Server sẽ ném lỗi.
> Dùng `RunSqldel` riêng để hiển thị thông báo thân thiện hơn.

```csharp
// Thực thi DELETE — bắt lỗi khóa ngoại riêng biệt
public static void RunSqldel(string sql)
{
    SqlCommand cmd = new SqlCommand();
    cmd.Connection = Class.Functions.conn;
    cmd.CommandText = sql;
    try
    {
        cmd.ExecuteNonQuery();
    }
    catch (Exception)
    {
        // Không hiển thị lỗi kỹ thuật — thông báo thân thiện cho người dùng
        MessageBox.Show("Dữ liệu đang được sử dụng, không thể xóa!",
            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    cmd.Dispose();
    cmd = null;
}

// Cách gọi trong btnXoa_Click
private void btnXoa_Click(object sender, EventArgs e)
{
    // Kiểm tra có bản ghi được chọn chưa
    if (tblcl.Rows.Count == 0)
    {
        MessageBox.Show("Không có dữ liệu!", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
    }
    if (txtMachatlieu.Text == "")
    {
        MessageBox.Show("Chưa chọn bản ghi nào!", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
    }

    // Xác nhận trước khi xóa
    if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo",
        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
    {
        string sql = "DELETE FROM tblChatlieu WHERE Machatlieu=N'" + txtMachatlieu.Text.Trim() + "'";
        Class.Functions.RunSqldel(sql); // Dùng RunSqldel (không phải RunSql)
    }
    load_dgridchatlieu();
    resetvalue();
    btnBoqua.Enabled = false;
}
```

### 10.4 Nút SỬA — UPDATE không sửa mã khóa chính

```csharp
// NÚT SỬA — cập nhật tất cả trường trừ mã khóa chính
private void btnSua_Click(object sender, EventArgs e)
{
    // Kiểm tra có dữ liệu và đã chọn bản ghi chưa
    if (tblcl.Rows.Count == 0)
    {
        MessageBox.Show("Không có dữ liệu!", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
    }
    if (txtMachatlieu.Text == "")
    {
        MessageBox.Show("Chưa chọn bản ghi nào!", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
    }
    if (txtTenchatlieu.Text == "")
    {
        MessageBox.Show("Chưa nhập tên!", "Thông báo",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
        txtTenchatlieu.Focus();
        return;
    }

    // UPDATE — WHERE theo mã khóa chính (không update mã)
    string sql = "UPDATE tblChatlieu SET " +
                 "Tenchatlieu=N'" + txtTenchatlieu.Text.Trim() + "'" +
                 " WHERE Machatlieu=N'" + txtMachatlieu.Text.Trim() + "'";
    Class.Functions.RunSql(sql);
    load_dgridchatlieu();
    resetvalue();
    btnBoqua.Enabled = false;
}
```

### 10.5 Validate MaskedTextBox (Điện thoại, Ngày sinh)

```csharp
// MaskedTextBox rỗng có giá trị mặc định là chuỗi mask — phải so sánh đúng cách
// Mask điện thoại "(999) 000-0000" → rỗng = "(   )    -"
if (mskDienthoai.Text == "(   )    -")
{
    MessageBox.Show("Phải nhập điện thoại!", "Thông báo",
        MessageBoxButtons.OK, MessageBoxIcon.Error);
    mskDienthoai.Focus();
    return;
}

// Mask ngày sinh "00/00/0000" → rỗng = "  /  /"
if (mskNgaysinh.Text == "  /  /")
{
    MessageBox.Show("Phải nhập ngày sinh!", "Thông báo",
        MessageBoxButtons.OK, MessageBoxIcon.Error);
    mskNgaysinh.Focus();
    return;
}
```

### 10.6 Hàm IsDate và ConvertDateTime (kiểm tra và đổi định dạng ngày)

```csharp
// Kiểm tra ngày nhập vào hợp lệ (định dạng dd/MM/yyyy)
public static bool IsDate(string d)
{
    string[] parts = d.Split('/');
    if ((Convert.ToInt32(parts[0]) >= 1) && (Convert.ToInt32(parts[0]) <= 31) &&
        (Convert.ToInt32(parts[1]) >= 1) && (Convert.ToInt32(parts[1]) <= 12) &&
        (Convert.ToInt32(parts[2]) >= 1900))
        return true;
    else
        return false;
}

// Chuyển dd/MM/yyyy → MM/dd/yyyy để SQL Server nhận đúng
public static string ConvertDateTime(string d)
{
    string[] parts = d.Split('/');
    return String.Format("{0}/{1}/{2}", parts[1], parts[0], parts[2]);
}

// Cách dùng khi INSERT ngày sinh
if (Class.Functions.IsDate(mskNgaysinh.Text) == false)
{
    MessageBox.Show("Lỗi định dạng ngày sinh!", "Thông báo",
        MessageBoxButtons.OK, MessageBoxIcon.Error);
    mskNgaysinh.Text = "";
    return;
}
string sqlNgay = Class.Functions.ConvertDateTime(mskNgaysinh.Text); // dùng trong câu INSERT
```

### 10.7 CheckBox Giới tính — đọc/ghi đúng cách

```csharp
// Khi LOAD từ DataGridView lên form — gán giá trị cho CheckBox
if (dgridNhanvien.CurrentRow.Cells["Gioitinh"].Value.ToString() == "Nam")
    chkGioitinh.Checked = true;
else
    chkGioitinh.Checked = false;

// Khi INSERT — đọc giá trị từ CheckBox
string gt;
if (chkGioitinh.Checked == true)
    gt = "Nam";
else
    gt = "Nữ";

// Dùng gt trong câu INSERT
string sql = "INSERT INTO tblNhanvien(..., Gioitinh) VALUES(..., N'" + gt + "')";
```

### 10.8 PictureBox + OpenFileDialog — chọn và hiển thị ảnh

```csharp
// Trong Form_Load — thiết lập chế độ hiển thị ảnh vừa khung
picAnh.SizeMode = PictureBoxSizeMode.StretchImage;

// Trong ResetValues — xóa ảnh khi reset form
picAnh.Image = null;
txtAnh.Text = "";

// NÚT CHỌN ẢNH — mở OpenFileDialog
private void btnOpen_Click(object sender, EventArgs e)
{
    OpenFileDialog dlgOpen = new OpenFileDialog();
    dlgOpen.Filter = "Bitmap(*.bmp)|*.bmp|GIF(*.gif)|*.gif|All files(*.*)|*.*";
    dlgOpen.InitialDirectory = "D:\\";
    dlgOpen.FilterIndex = 2; // Mặc định chọn GIF
    dlgOpen.Title = "Chọn hình ảnh minh họa";

    if (dlgOpen.ShowDialog() == DialogResult.OK)
    {
        picAnh.Image = Image.FromFile(dlgOpen.FileName); // Hiển thị ảnh
        txtAnh.Text = dlgOpen.FileName;                   // Lưu đường dẫn
    }
}

// Khi click DataGridView — load lại ảnh từ đường dẫn đã lưu trong DB
txtAnh.Text = Functions.GetFieldValues(
    "SELECT Anh FROM tblHang WHERE Mahang=N'" + txtMahang.Text + "'");
picAnh.Image = Image.FromFile(txtAnh.Text); // Đọc file ảnh từ đường dẫn
```

### 10.9 FrmMain — mở Form con từ Menu

```csharp
// FrmMain đóng vai trò form gốc, mở các form con qua menu
private void MnuChatlieu_Click(object sender, EventArgs e)
{
    Forms.FrmChatlieu a = new Forms.FrmChatlieu();
    a.Show(); // Dùng Show() thay vì ShowDialog() để không block form chính
}

private void mnuNhanvien_Click(object sender, EventArgs e)
{
    Forms.FrmNV a = new Forms.FrmNV();
    a.Show();
}

private void mnuHanghoa_Click(object sender, EventArgs e)
{
    Forms.FrmHanghoa a = new Forms.FrmHanghoa();
    a.Show();
}
```

> **Lưu ý:** Vì kết nối (`Functions.conn`) đã mở ở `FrmMain_Load`, các Form con
> **không cần gọi `Ketnoi()` lại** — dùng chung biến `static conn`.

### 10.10 Bảng so sánh: `ResetValues` nâng cao (có TextBox số và PictureBox)

```csharp
// ResetValues đầy đủ cho form có nhiều loại control
private void ResetValues()
{
    txtMahang.Text     = "";
    txtTenhang.Text    = "";
    txtSoluong.Text    = "0";       // Số → đặt về "0" thay vì ""
    txtDongianhap.Text = "0";
    txtDongiaban.Text  = "0";
    txtGhichu.Text     = "";
    txtAnh.Text        = "";

    cboMachatlieu.Text = "";        // ComboBox → xóa text hiển thị

    picAnh.Image = null;            // PictureBox → xóa ảnh

    // Khóa các ô số — chỉ mở khi ở chế độ nhập liệu
    txtSoluong.Enabled    = false;
    txtDongianhap.Enabled = false;
    txtDongiaban.Enabled  = false;
}
```

---

## 11. CHECKLIST NÂNG CAO (Bổ sung từ QLBH_2026)

- [ ] **Kết nối** mở 1 lần ở `FrmMain_Load`, **không** gọi `Ketnoi()` ở từng Form con
- [ ] **Ngắt kết nối** (`ngatketnoi()`) trước khi `Application.Exit()`
- [ ] Dùng `RunSqldel` thay `RunSql` cho câu **DELETE** (bắt lỗi FK riêng)
- [ ] Nút **SỬA** kiểm tra: có dữ liệu → đã chọn dòng → trường không rỗng → rồi mới UPDATE
- [ ] **MaskedTextBox** rỗng so sánh với chuỗi mask (không phải `""`)
- [ ] Validate **ngày** bằng `IsDate()` và chuyển định dạng bằng `ConvertDateTime()`
- [ ] **CheckBox giới tính**: load từ DB dùng `==` so sánh chuỗi; ghi vào DB dùng biến `gt`
- [ ] **PictureBox**: set `SizeMode = StretchImage` trong Load, `Image = null` khi Reset
- [ ] **OpenFileDialog**: luôn kiểm tra `ShowDialog() == DialogResult.OK` trước khi dùng
- [ ] **FrmMain** mở form con bằng `.Show()` (không dùng `.ShowDialog()`)
- [ ] **ResetValues**: TextBox số reset về `"0"`, không phải `""`

---

*Tài liệu được tổng hợp dựa trên codebase VANPHONG + Ontap2904 + QLBH_2026 — Học viện Ngân hàng 2025-2026*
