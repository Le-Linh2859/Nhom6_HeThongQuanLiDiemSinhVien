# Design Document

## Overview

Tài liệu này mô tả thiết kế kỹ thuật cho tính năng **Phân quyền form Quản lý Phúc khảo** (`frmPhucKhao.cs`).

Thay đổi tập trung vào một file duy nhất: `frmPhucKhao.cs` trong namespace `QLDSV.GUI`. Không thêm class mới, không sửa DB schema (ngoại trừ bảng `LichSuPhucKhao` tùy chọn cho audit log). Toàn bộ logic phân quyền được đóng gói trong các method mới thêm vào class `frmPhucKhao`, đọc `SessionHelper.MaVaiTro` để quyết định hành vi giao diện và câu SQL tương ứng.

Luồng chính sau khi thay đổi:

```
frmPhucKhao_Load
  └─► BoPhanQuyen()           ← CẤU HÌNH giao diện theo role (REQ-1)
  └─► TaiDuLieuTheoQuyen()    ← THAY THẾ LoadData() (REQ-2)
        └─► CapNhatThongKeTheoQuyen()  ← tính từ DataTable đã load (REQ-6)

dataGridView_CellClick
  └─► CapNhatTrangThaiNut()   ← Enabled/Disabled btnDuyet, btnTuChoi (REQ-3)

btnDuyet/btnTuChoi_Click
  └─► KiemTraQuyenGhi()       ← guard quyền (REQ-7)
  └─► KiemTraTrangThaiHienTai() ← re-read DB (REQ-8)
  └─► GhiLichSu()             ← audit log (REQ-9)

btnThem_Click (SV)
  └─► NapComboLopHocPhanTheoQuyen()  ← lọc lớp đã đăng ký (REQ-4)
```

---

## Architecture

### Các thay đổi trong frmPhucKhao.cs

**Method mới thêm:**

| Method | Mục đích | REQ |
|---|---|---|
| `BoPhanQuyen()` | Ẩn/hiện nút theo role, lấy MaSV/MaGV | 1 |
| `TaiDuLieuTheoQuyen()` | Thay `LoadData()` — xây SQL WHERE theo role | 2 |
| `CapNhatThongKeTheoQuyen()` | Thay `UpdateStatistics()` — đếm từ DataTable | 6 |
| `NapComboLopHocPhanTheoQuyen()` | Lọc `cboLopHocPhan` chỉ lớp SV đã đăng ký | 4 |
| `KiemTraQuyenGhi(string thaoTac)` | Guard kiểm tra quyền trước mỗi thao tác ghi | 7 |
| `CapNhatTrangThaiNut(string trangThai, string maLHP)` | Enabled/Disabled btnDuyet, btnTuChoi khi chọn dòng | 3 |
| `KiemTraTrangThaiHienTai(string maPK)` | Re-read TrangThai từ DB trước khi UPDATE | 8 |
| `GhiLichSu(string maPK, string thaoTac)` | Ghi audit log vào LichSuPhucKhao nếu bảng tồn tại | 9 |

**Method sửa:**

| Method | Thay đổi |
|---|---|
| `frmPhucKhao_Load` | Gọi `BoPhanQuyen()` trước `TaiDuLieuTheoQuyen()` |
| `dataGridView_CellClick` | Gọi `CapNhatTrangThaiNut()` sau khi chọn dòng |
| `btnThem_Click` | Guard `KiemTraQuyenGhi("THEM")` + xử lý UX panel + `NapComboLopHocPhanTheoQuyen()` cho SV |
| `btnDuyet_Click` | Guard + `KiemTraTrangThaiHienTai()` + kiểm tra phạm vi GV + `GhiLichSu()` |
| `btnTuChoi_Click` | Guard + `KiemTraTrangThaiHienTai()` + kiểm tra phạm vi GV + `GhiLichSu()` |
| `btnXoa_Click` | Guard `KiemTraQuyenGhi("XOA")` + `GhiLichSu()` trước DELETE |
| `btnLuuDetail_Click` | Guard + xác nhận MaSV + kiểm tra trùng lặp |
| `LoadData()` / `UpdateStatistics()` | Thay bằng `TaiDuLieuTheoQuyen()` / `CapNhatThongKeTheoQuyen()` ở mọi nơi gọi |

**Biến mới thêm vào class:**

```csharp
// MaSV của sinh viên đang đăng nhập (dùng khi MaVaiTro = VT003)
private string maSVHienTai = "";

// MaGV của giảng viên đang đăng nhập (dùng khi MaVaiTro = VT002)
private string maGVHienTai = "";
```

---

## Components and Interfaces

### 1. BoPhanQuyen() — Phân quyền giao diện khi mở form

> Cấu hình Visible của các nút và lấy định danh người dùng từ DB.

**Vị trí gọi:** Trong `frmPhucKhao_Load`, trước `TaiDuLieuTheoQuyen()`.

```
BoPhanQuyen():
  role = SessionHelper.MaVaiTro

  IF role == null OR role == "" OR role NOT IN {VT001, VT002, VT003}:
    MessageBox.Show("Không xác định được vai trò người dùng.")
    this.Close()
    RETURN

  IF role == "VT001":  // Admin — toàn quyền
    btnThem.Visible = btnDuyet.Visible = btnTuChoi.Visible = true
    btnXoa.Visible  = btnLamMoi.Visible = true

  ELSE IF role == "VT002":  // Giảng viên
    btnThem.Visible = false;  btnXoa.Visible = false
    btnDuyet.Visible = btnTuChoi.Visible = btnLamMoi.Visible = true
    // Lấy MaGV từ DB
    maGVHienTai = FunctionQa.getfieldvalue(
        "SELECT MaGV FROM GiangVien WHERE MaTaiKhoan = '" + SessionHelper.MaTaiKhoan + "'")
    IF maGVHienTai == "":
      MessageBox.Show("Không tìm thấy thông tin giảng viên liên kết với tài khoản này.")

  ELSE IF role == "VT003":  // Sinh viên
    btnDuyet.Visible = btnTuChoi.Visible = btnXoa.Visible = false
    btnThem.Visible  = btnLamMoi.Visible = true
    // Lấy MaSV từ DB
    maSVHienTai = FunctionQa.getfieldvalue(
        "SELECT MaSV FROM SinhVien WHERE MaTaiKhoan = '" + SessionHelper.MaTaiKhoan + "'")
    IF maSVHienTai == "":
      MessageBox.Show("Không tìm thấy thông tin sinh viên liên kết với tài khoản này.")
```

**SQL tra cứu:**
```sql
-- Lấy MaGV (VT002)
SELECT MaGV FROM GiangVien WHERE MaTaiKhoan = 'TK_HIENTAI'

-- Lấy MaSV (VT003)
SELECT MaSV FROM SinhVien WHERE MaTaiKhoan = 'TK_HIENTAI'
```

---

### 2. TaiDuLieuTheoQuyen() — Thay thế LoadData()

> Tải dữ liệu vào dataGridView với SQL WHERE lọc theo role. Gọi `CapNhatThongKeTheoQuyen()` ở cuối.

**Thay thế hoàn toàn `LoadData()`.** Mọi nơi gọi `LoadData()` đổi thành `TaiDuLieuTheoQuyen()`.

```
TaiDuLieuTheoQuyen():
  role = SessionHelper.MaVaiTro
  scopeWhere = ""

  IF role == "VT001":
    scopeWhere = ""   // Admin: không lọc thêm

  ELSE IF role == "VT002":
    IF maGVHienTai == "":
      dataGridView.DataSource = null
      CapNhatThongKeTheoQuyen()
      RETURN
    scopeWhere = " AND pk.MaLHP IN (SELECT MaLHP FROM LopHocPhan WHERE MaGV = '" + maGVHienTai + "')"

  ELSE IF role == "VT003":
    IF maSVHienTai == "":
      dataGridView.DataSource = null
      CapNhatThongKeTheoQuyen()
      RETURN
    scopeWhere = " AND pk.MaSV = '" + maSVHienTai + "'"

  ELSE:
    dataGridView.DataSource = null
    CapNhatThongKeTheoQuyen()
    RETURN

  // Áp dụng bộ lọc tìm kiếm và trạng thái (giữ nguyên logic cũ)
  keyword = txtTimKiem.Text.Trim()
  IF keyword != "" AND keyword != placeholder:
    scopeWhere += " AND (sv.HoTen LIKE N'%keyword%' OR lhp.TenLopHocPhan LIKE N'%keyword%' OR pk.MaPhucKhao LIKE '%keyword%')"

  IF cboFilterTrangThai.SelectedIndex > 0:
    scopeWhere += " AND pk.TrangThai = N'filterStatus'"

  sql = BASE_SELECT + " WHERE 1=1" + scopeWhere + " ORDER BY pk.MaPhucKhao DESC"

  TRY:
    tblPhucKhao = FunctionQa.getdatatotable(sql)
    dataGridView.DataSource = tblPhucKhao
    // ... cấu hình cột, AllowUserToAddRows, EditMode như cũ ...
    lblTongBanGhi.Text = "Tổng: " + tblPhucKhao.Rows.Count + " bản ghi"
    CapNhatThongKeTheoQuyen()
  CATCH Exception ex:
    MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message)
```

**SQL mẫu cho Giảng viên (VT002):**
```sql
SELECT pk.MaPhucKhao, sv.HoTen, lhp.TenLopHocPhan,
       pk.NgayYeuCau, pk.TrangThai, pk.MaSV, pk.MaLHP, pk.LyDo
FROM PhucKhao pk
JOIN SinhVien sv ON pk.MaSV = sv.MaSV
JOIN LopHocPhan lhp ON pk.MaLHP = lhp.MaLHP
WHERE 1=1
  AND pk.MaLHP IN (SELECT MaLHP FROM LopHocPhan WHERE MaGV = 'GV001')
ORDER BY pk.MaPhucKhao DESC
```

**SQL mẫu cho Sinh viên (VT003):**
```sql
SELECT pk.MaPhucKhao, sv.HoTen, lhp.TenLopHocPhan,
       pk.NgayYeuCau, pk.TrangThai, pk.MaSV, pk.MaLHP, pk.LyDo
FROM PhucKhao pk
JOIN SinhVien sv ON pk.MaSV = sv.MaSV
JOIN LopHocPhan lhp ON pk.MaLHP = lhp.MaLHP
WHERE 1=1
  AND pk.MaSV = 'SV001'
ORDER BY pk.MaPhucKhao DESC
```

---

### 3. CapNhatThongKeTheoQuyen() — Thay thế UpdateStatistics()

> Tính thống kê trực tiếp từ `tblPhucKhao` (DataTable đã load), không thêm query SQL riêng.

**Không thực thi thêm câu SQL.** Đếm từ DataTable đảm bảo số liệu khớp 100% với `dataGridView`.

```
CapNhatThongKeTheoQuyen():
  role = SessionHelper.MaVaiTro

  IF role NOT IN {VT001, VT002, VT003} OR tblPhucKhao == null:
    lblStatTongVal.Text = lblStatChoDuyetVal.Text = "0"
    lblStatDaDuyetVal.Text = lblStatTuChoiVal.Text = "0"
    RETURN

  // Đếm từ DataTable đã load
  choDuyet = tblPhucKhao.Select("TrangThai = 'Chờ duyệt'").Length
  daDuyet  = tblPhucKhao.Select("TrangThai = 'Đã duyệt'").Length
  tuChoi   = tblPhucKhao.Select("TrangThai = 'Từ chối'").Length
  tong     = choDuyet + daDuyet + tuChoi

  lblStatTongVal.Text     = tong.ToString()
  lblStatChoDuyetVal.Text = choDuyet.ToString()
  lblStatDaDuyetVal.Text  = daDuyet.ToString()
  lblStatTuChoiVal.Text   = tuChoi.ToString()
```

> **Lưu ý:** Vì thống kê đếm từ DataTable đã lọc theo role, số liệu tự động phản ánh đúng phạm vi của từng role mà không cần thêm điều kiện WHERE.

---

### 4. CapNhatTrangThaiNut(string trangThai, string maLHP) — Enabled/Disabled btnDuyet, btnTuChoi

> Cập nhật trạng thái Enabled của btnDuyet và btnTuChoi khi người dùng chọn một dòng trong dataGridView.

**Vị trí gọi:** Trong `dataGridView_CellClick`, sau khi đọc giá trị dòng được chọn.

```
CapNhatTrangThaiNut(trangThai, maLHP):
  role = SessionHelper.MaVaiTro

  // Mặc định: disable cả hai
  btnDuyet.Enabled  = false
  btnTuChoi.Enabled = false

  // Chỉ Admin và GV mới có nút này (SV không thấy)
  IF role != "VT001" AND role != "VT002": RETURN

  // Bản ghi phải ở trạng thái "Chờ duyệt"
  IF trangThai != "Chờ duyệt": RETURN

  IF role == "VT001":
    // Admin: enable với mọi bản ghi Chờ duyệt
    btnDuyet.Enabled  = true
    btnTuChoi.Enabled = true

  ELSE IF role == "VT002":
    // GV: chỉ enable nếu MaLHP thuộc lớp phụ trách
    sqlCheck = "SELECT COUNT(*) FROM LopHocPhan WHERE MaLHP = '" + maLHP + "' AND MaGV = '" + maGVHienTai + "'"
    IF FunctionQa.getfieldvalue(sqlCheck) != "0":
      btnDuyet.Enabled  = true
      btnTuChoi.Enabled = true
    // Nếu không thuộc lớp phụ trách: giữ nguyên disabled
```

---

### 5. NapComboLopHocPhanTheoQuyen() — Lọc cboLopHocPhan cho Sinh viên

> Nạp danh sách lớp học phần vào cboLopHocPhan, chỉ hiển thị lớp SV đã đăng ký.

**Vị trí gọi:** Trong `btnThem_Click` khi `MaVaiTro == "VT003"`.

```
NapComboLopHocPhanTheoQuyen():
  IF maSVHienTai == "":
    cboLopHocPhan.DataSource = null
    btnLuuDetail.Enabled = false
    MessageBox.Show("Không xác định được sinh viên. Không thể thêm yêu cầu.")
    RETURN

  sqlLHP = "SELECT lhp.MaLHP, lhp.MaLHP + ' - ' + lhp.TenLopHocPhan AS HienThi
            FROM LopHocPhan lhp
            JOIN DangKyHocPhan dkhp ON lhp.MaLHP = dkhp.MaLHP
            WHERE dkhp.MaSV = '" + maSVHienTai + "'"

  FunctionQa.fillcombo(sqlLHP, cboLopHocPhan, "MaLHP", "HienThi")
  cboLopHocPhan.SelectedIndex = -1

  IF cboLopHocPhan.Items.Count == 0:
    btnLuuDetail.Enabled = false
    MessageBox.Show("Bạn chưa đăng ký lớp học phần nào. Không thể nộp yêu cầu phúc khảo.")
  ELSE:
    btnLuuDetail.Enabled = true
```

**SQL lọc lớp học phần đã đăng ký:**
```sql
SELECT lhp.MaLHP,
       lhp.MaLHP + ' - ' + lhp.TenLopHocPhan AS HienThi
FROM LopHocPhan lhp
JOIN DangKyHocPhan dkhp ON lhp.MaLHP = dkhp.MaLHP
WHERE dkhp.MaSV = 'SV001'
```

---

### 6. KiemTraQuyenGhi(string thaoTac) — Guard trước mỗi thao tác ghi

> Đọc lại `SessionHelper.MaVaiTro` tại thời điểm thực thi và kiểm tra quyền. Trả về `bool`.

**Tham số `thaoTac`:** `"THEM"`, `"DUYET"`, `"TUCHOI"`, `"XOA"`.

```
KiemTraQuyenGhi(thaoTac) → bool:
  role = SessionHelper.MaVaiTro   // đọc lại tại thời điểm thực thi

  IF role == null OR role == "":
    MessageBox.Show("Phiên đăng nhập không hợp lệ. Vui lòng đăng nhập lại.")
    RETURN false

  SWITCH thaoTac:
    "THEM":
      IF role IN {"VT001", "VT003"}: RETURN true
      MessageBox.Show("Bạn không có quyền thêm yêu cầu phúc khảo.")
      RETURN false

    "DUYET", "TUCHOI":
      IF role IN {"VT001", "VT002"}: RETURN true
      MessageBox.Show("Bạn không có quyền thực hiện thao tác này.")
      RETURN false

    "XOA":
      IF role == "VT001": RETURN true
      MessageBox.Show("Chỉ Admin mới có quyền xóa yêu cầu phúc khảo.")
      RETURN false

    DEFAULT: RETURN false
```

**Cách dùng trong handler:**
```csharp
// KIỂM TRA QUYỀN XÓA
private void btnXoa_Click(object sender, EventArgs e)
{
    if (!KiemTraQuyenGhi("XOA")) return;
    // ... phần còn lại ...
}
```

---

### 7. KiemTraTrangThaiHienTai(string maPK) — Re-read DB trước khi UPDATE (REQ-8)

> Truy vấn lại TrangThai từ DB ngay trước khi thực thi UPDATE để phát hiện concurrent modification.

**Trả về:** `string` — TrangThai hiện tại trong DB, hoặc `""` nếu không tìm thấy.

```
KiemTraTrangThaiHienTai(maPK) → string:
  sql = "SELECT TrangThai FROM PhucKhao WHERE MaPhucKhao = '" + maPK + "'"
  RETURN FunctionQa.getfieldvalue(sql)
```

**Cách dùng trong btnDuyet_Click:**
```
btnDuyet_Click:
  IF NOT KiemTraQuyenGhi("DUYET"): RETURN
  ...
  // Re-read trước khi UPDATE
  trangThaiHienTai = KiemTraTrangThaiHienTai(maPK)
  IF trangThaiHienTai != trangThaiTrenGrid:
    MessageBox.Show("Yêu cầu này đã được cập nhật bởi người khác. Danh sách sẽ được làm mới.")
    TaiDuLieuTheoQuyen()
    RETURN
  IF trangThaiHienTai != "Chờ duyệt":
    MessageBox.Show("Yêu cầu này đã được xử lý trước đó.")
    TaiDuLieuTheoQuyen()
    RETURN
  // Thực thi UPDATE
  ...
```

---

### 8. GhiLichSu(string maPK, string thaoTac) — Audit Log (REQ-9)

> Ghi bản ghi vào bảng `LichSuPhucKhao` nếu bảng tồn tại. Không ném exception nếu bảng không có.

**Tham số `thaoTac`:** `"Duyệt"`, `"Từ chối"`, `"Xóa"`.

```
GhiLichSu(maPK, thaoTac):
  TRY:
    sql = "INSERT INTO LichSuPhucKhao (MaPhucKhao, MaTaiKhoan, ThaoTac, ThoiGian)
           VALUES ('" + maPK + "', '" + SessionHelper.MaTaiKhoan + "',
                   N'" + thaoTac + "', GETDATE())"
    FunctionQa.runsql(sql)
  CATCH Exception:
    // Bỏ qua lỗi — bảng LichSuPhucKhao có thể không tồn tại
    // Không hiển thị MessageBox, không rollback thao tác chính
    System.Diagnostics.Debug.WriteLine("GhiLichSu bỏ qua: " + ex.Message)
```

**SQL ghi log:**
```sql
INSERT INTO LichSuPhucKhao (MaPhucKhao, MaTaiKhoan, ThaoTac, ThoiGian)
VALUES ('PK001', 'TK001', N'Duyệt', GETDATE())
```

> **Lưu ý:** `GhiLichSu` được gọi **sau** khi thao tác chính thành công (duyệt/từ chối), hoặc **trước** khi DELETE (xóa) để đảm bảo log được ghi trước khi bản ghi bị xóa.

---

### 9. Sửa các handler nút bấm

#### 9.1 frmPhucKhao_Load

```csharp
private void frmPhucKhao_Load(object sender, EventArgs e)
{
    try
    {
        FunctionQa.ketnoi();
        cboFilterTrangThai.Items.AddRange(new[] { "--- Tất cả ---", "Chờ duyệt", "Đã duyệt", "Từ chối" });
        cboFilterTrangThai.SelectedIndex = 0;

        BoPhanQuyen();          // ← MỚI: phân quyền giao diện
        LoadComboBoxes();       // nạp combo mặc định
        TaiDuLieuTheoQuyen();   // ← THAY LoadData() + UpdateStatistics()
    }
    catch (Exception ex)
    {
        MessageBox.Show("Lỗi khởi tạo form: " + ex.Message, "Lỗi",
            MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    pnlDetail.Width = 0;
    pnlDetail.Visible = false;
}
```

#### 9.2 dataGridView_CellClick

Thêm lời gọi `CapNhatTrangThaiNut()` sau khi đọc giá trị dòng:

```
dataGridView_CellClick:
  ...đọc maPK, trangThai, maSV, maLHP, lyDo như cũ...
  CapNhatTrangThaiNut(trangThai, maLHP)   // ← MỚI
  ...ShowEditMode / ShowViewMode như cũ...
```

#### 9.3 btnThem_Click

Thêm guard quyền và xử lý UX khi panel đang mở:

```
btnThem_Click:
  // KIỂM TRA QUYỀN THÊM MỚI
  IF NOT KiemTraQuyenGhi("THEM"): RETURN

  // Xử lý UX: panel đang mở
  IF detailPanelVisible AND isAddingNew:
    RETURN   // đang thêm mới rồi, không làm gì
  IF detailPanelVisible AND NOT isAddingNew:
    CloseSidebar()   // đóng panel xem, mở lại ở chế độ thêm mới

  isAddingNew = true
  dataGridView.ClearSelection()
  nextMaPK = GenerateNewMaPhucKhao()
  currentMaPK = nextMaPK

  IF SessionHelper.MaVaiTro == "VT003":
    // Cố định sinh viên = chính mình
    cboSinhVien.SelectedValue = maSVHienTai
    cboSinhVien.Enabled = false
    NapComboLopHocPhanTheoQuyen()   // ← lọc lớp đã đăng ký
  ELSE:
    cboSinhVien.Enabled = true

  ShowEditMode(nextMaPK)
```

#### 9.4 btnDuyet_Click

```
btnDuyet_Click:
  // KIỂM TRA QUYỀN DUYỆT
  IF NOT KiemTraQuyenGhi("DUYET"): RETURN

  IF dataGridView.SelectedRows.Count == 0:
    MessageBox.Show("Vui lòng chọn yêu cầu cần duyệt.")
    RETURN

  row = dataGridView.SelectedRows[0]
  maPK      = row.Cells[0].Value
  trangThaiTrenGrid = row.Cells[4].Value
  maLHP     = row.Cells[6].Value

  // Kiểm tra phạm vi GV
  IF SessionHelper.MaVaiTro == "VT002":
    sqlCheck = "SELECT COUNT(*) FROM LopHocPhan WHERE MaLHP = '" + maLHP + "' AND MaGV = '" + maGVHienTai + "'"
    IF FunctionQa.getfieldvalue(sqlCheck) == "0":
      MessageBox.Show("Bạn không có quyền duyệt yêu cầu này.")
      RETURN

  // Re-read DB (concurrent access check)
  trangThaiHienTai = KiemTraTrangThaiHienTai(maPK)
  IF trangThaiHienTai != trangThaiTrenGrid:
    MessageBox.Show("Yêu cầu này đã được cập nhật bởi người khác. Danh sách sẽ được làm mới.")
    TaiDuLieuTheoQuyen()
    RETURN
  IF trangThaiHienTai != "Chờ duyệt":
    MessageBox.Show("Yêu cầu này đã được xử lý trước đó.")
    TaiDuLieuTheoQuyen()
    RETURN

  confirm = MessageBox.Show("Duyệt yêu cầu '" + maPK + "'?", YesNo)
  IF confirm == Yes:
    FunctionQa.runsql("UPDATE PhucKhao SET TrangThai = N'Đã duyệt' WHERE MaPhucKhao = '" + maPK + "'")
    GhiLichSu(maPK, "Duyệt")   // ← audit log
    CloseSidebar()
    TaiDuLieuTheoQuyen()
```

#### 9.5 btnTuChoi_Click

Tương tự `btnDuyet_Click`, thay `"Đã duyệt"` → `"Từ chối"` và `GhiLichSu(maPK, "Từ chối")`.

#### 9.6 btnXoa_Click

```
btnXoa_Click:
  // KIỂM TRA QUYỀN XÓA
  IF NOT KiemTraQuyenGhi("XOA"): RETURN

  IF dataGridView.SelectedRows.Count == 0:
    MessageBox.Show("Vui lòng chọn yêu cầu cần xóa.")
    RETURN

  maPK = dataGridView.SelectedRows[0].Cells[0].Value
  confirm = MessageBox.Show("Xóa yêu cầu '" + maPK + "'?", YesNo)
  IF confirm == Yes:
    GhiLichSu(maPK, "Xóa")   // ← ghi log TRƯỚC khi xóa
    FunctionQa.RunSqlDel("DELETE FROM PhucKhao WHERE MaPhucKhao = '" + maPK + "'")
    CloseSidebar()
    TaiDuLieuTheoQuyen()
```

#### 9.7 btnLuuDetail_Click

```
btnLuuDetail_Click:
  // KIỂM TRA QUYỀN THÊM/SỬA
  IF NOT KiemTraQuyenGhi("THEM"): RETURN

  // Validate input (giữ nguyên logic cũ)
  IF cboSinhVien.SelectedValue == null: ...
  IF cboLopHocPhan.SelectedValue == null: ...
  IF txtLyDo.Text.Trim() == "": ...

  maSV  = cboSinhVien.SelectedValue.ToString()
  maLHP = cboLopHocPhan.SelectedValue.ToString()

  // Xác nhận MaSV không bị giả mạo (chỉ áp dụng cho SV)
  IF SessionHelper.MaVaiTro == "VT003" AND maSV != maSVHienTai:
    MessageBox.Show("Lỗi: Không thể nộp yêu cầu thay cho sinh viên khác.")
    RETURN

  IF isAddingNew:
    // Kiểm tra trùng lặp
    sqlTrung = "SELECT COUNT(*) FROM PhucKhao
                WHERE MaSV = '" + maSV + "' AND MaLHP = '" + maLHP + "'
                  AND TrangThai IN (N'Chờ duyệt', N'Đã duyệt')"
    IF FunctionQa.getfieldvalue(sqlTrung) != "0":
      MessageBox.Show("Đã tồn tại yêu cầu phúc khảo cho lớp học phần này.")
      RETURN

    maPK = GenerateNewMaPhucKhao()
    FunctionQa.runsql("INSERT INTO PhucKhao (MaPhucKhao, MaSV, MaLHP, NgayYeuCau, TrangThai, LyDo) VALUES (...)")
    MessageBox.Show("Thêm yêu cầu phúc khảo mới thành công!")
    TaiDuLieuTheoQuyen()
    ShowViewMode(maPK, maSV, maLHP, ngayYC, "Chờ duyệt", lyDo)
  ELSE:
    FunctionQa.runsql("UPDATE PhucKhao SET NgayYeuCau = ..., LyDo = ... WHERE MaPhucKhao = '" + currentMaPK + "'")
    MessageBox.Show("Cập nhật yêu cầu phúc khảo thành công!")
    TaiDuLieuTheoQuyen()
    ShowViewMode(currentMaPK, maSV, maLHP, ngayYC, trangThai, lyDo)
```

---

## Data Models

### Bảng LichSuPhucKhao (tùy chọn — REQ-9)

Nếu muốn bật tính năng audit log, tạo bảng sau trong DB:

```sql
CREATE TABLE LichSuPhucKhao (
    MaLog        INT IDENTITY(1,1) PRIMARY KEY,
    MaPhucKhao   VARCHAR(20)   NOT NULL,
    MaTaiKhoan   VARCHAR(50)   NOT NULL,
    ThaoTac      NVARCHAR(20)  NOT NULL,  -- 'Duyệt', 'Từ chối', 'Xóa'
    ThoiGian     DATETIME      NOT NULL DEFAULT GETDATE()
)
```

Nếu bảng không tồn tại, `GhiLichSu()` bắt exception và bỏ qua — hệ thống vẫn hoạt động bình thường.

---

## Error Handling

| Tình huống | Xử lý |
|---|---|
| `MaVaiTro` null/rỗng khi mở form | Hiển thị lỗi, đóng form |
| `MaVaiTro` không hợp lệ | Hiển thị lỗi, đóng form |
| GV không có bản ghi trong `GiangVien` | Cảnh báo, `dataGridView` rỗng, không đóng form |
| SV không có bản ghi trong `SinhVien` | Cảnh báo, `dataGridView` rỗng, không đóng form |
| SV chưa đăng ký lớp nào | Thông báo, `btnLuuDetail.Enabled = false` |
| SV nộp trùng yêu cầu | Hủy lưu, thông báo trùng lặp |
| GV duyệt lớp không phụ trách | Hủy thao tác, thông báo không có quyền |
| Bản ghi đã bị thay đổi bởi người khác (concurrent) | Hủy thao tác, thông báo, tải lại `dataGridView` |
| Bản ghi không còn ở trạng thái "Chờ duyệt" | Hủy thao tác, thông báo, tải lại `dataGridView` |
| `MaVaiTro` thay đổi giữa chừng (session hết hạn) | `KiemTraQuyenGhi` từ chối, thông báo lỗi phiên |
| `FunctionQa.runsql` ném exception | `try-catch`, hiển thị `MessageBox` mô tả lỗi |
| `GhiLichSu` thất bại (bảng không tồn tại) | Bỏ qua, ghi `Debug.WriteLine`, không ảnh hưởng thao tác chính |

---

## Testing Considerations

### Kịch bản test theo role

**Admin (VT001):**
- Mở form → thấy đủ 5 nút; `dataGridView` hiển thị toàn bộ bản ghi
- Chọn bản ghi "Chờ duyệt" → `btnDuyet/btnTuChoi.Enabled = true`
- Chọn bản ghi "Đã duyệt" → `btnDuyet/btnTuChoi.Enabled = false`
- Duyệt/Từ chối/Xóa → thành công; thống kê cập nhật đúng

**Giảng viên (VT002):**
- Mở form → không thấy `btnThem`, `btnXoa`
- `dataGridView` chỉ hiển thị phúc khảo thuộc lớp GV phụ trách
- Chọn bản ghi thuộc lớp mình + "Chờ duyệt" → `btnDuyet/btnTuChoi.Enabled = true`
- Chọn bản ghi KHÔNG thuộc lớp mình → `btnDuyet/btnTuChoi.Enabled = false`
- Duyệt bản ghi thuộc lớp mình → thành công
- Bypass UI và gọi handler với bản ghi không thuộc lớp → `KiemTraQuyenGhi` + kiểm tra DB chặn lại

**Sinh viên (VT003):**
- Mở form → không thấy `btnDuyet`, `btnTuChoi`, `btnXoa`
- `dataGridView` chỉ hiển thị phúc khảo của chính SV đó
- Nhấn `btnThem` → `cboSinhVien` cố định, `cboLopHocPhan` chỉ liệt kê lớp đã đăng ký
- Nhấn `btnThem` khi panel đang mở ở chế độ thêm → không làm gì
- Nhấn `btnThem` khi panel đang mở ở chế độ xem → đóng panel xem, mở thêm mới
- Nộp trùng lớp đã có yêu cầu "Chờ duyệt" → bị chặn

### Kịch bản test edge case

- GV không có bản ghi trong `GiangVien` → form mở, `dataGridView` rỗng, cảnh báo
- SV không có bản ghi trong `SinhVien` → form mở, `dataGridView` rỗng, cảnh báo
- SV chưa đăng ký lớp nào → nhấn `btnThem`, `cboLopHocPhan` rỗng, `btnLuuDetail` disabled
- `MaVaiTro = null` → form hiển thị lỗi và đóng
- `MaVaiTro = "VT999"` → form hiển thị lỗi và đóng
- Hai người dùng cùng duyệt một yêu cầu → người thứ hai nhận thông báo "đã được xử lý"
- Bảng `LichSuPhucKhao` không tồn tại → thao tác duyệt/xóa vẫn thành công, không có lỗi

---

## Correctness Properties

### Property 1: Bất biến phân quyền ghi

Với mọi thao tác ghi (INSERT/UPDATE/DELETE), `KiemTraQuyenGhi` luôn được gọi trước `FunctionQa.runsql` / `FunctionQa.RunSqlDel`. Không có đường code nào bỏ qua bước kiểm tra này.

**Validates: REQ-7.1, 7.2, 7.3**

### Property 2: Bất biến MaSV khi Sinh viên thêm mới

Khi `MaVaiTro = VT003`, `MaSV` trong câu INSERT luôn bằng `maSVHienTai`. Nếu hai giá trị khác nhau, thao tác lưu bị hủy trước khi gọi `FunctionQa`.

**Validates: REQ-4.1, 4.4**

### Property 3: Bất biến phạm vi duyệt của Giảng viên

Khi `MaVaiTro = VT002`, mọi thao tác duyệt/từ chối đều được xác nhận qua DB trước khi thực thi UPDATE. Kết quả = 0 thì hủy thao tác.

**Validates: REQ-3.3, 3.4, 3.5, 3.8**

### Property 4: Nhất quán thống kê

`lblStatTongVal` luôn bằng tổng của ba giá trị còn lại, được tính từ cùng một DataTable với `dataGridView`. Không có query SQL riêng cho thống kê.

**Validates: REQ-6.1, 6.2, 6.3, 6.4**

### Property 5: Bất biến concurrent access

Trước mỗi UPDATE duyệt/từ chối, `TrangThai` được đọc lại từ DB. Nếu khác với giá trị trên grid, UPDATE bị hủy và grid được làm mới.

**Validates: REQ-8.1, 8.2, 8.3**

### Property 6: Graceful degradation của audit log

`GhiLichSu` không bao giờ ném exception ra ngoài. Mọi lỗi trong `GhiLichSu` đều được bắt và bỏ qua, không ảnh hưởng đến thao tác chính.

**Validates: REQ-9.4**
