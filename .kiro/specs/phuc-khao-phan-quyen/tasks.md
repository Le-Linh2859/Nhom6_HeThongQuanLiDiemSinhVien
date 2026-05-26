# Implementation Tasks

## Task List

- [x] 1. Thêm biến và method helper cơ bản vào frmPhucKhao
  - Thêm 2 biến private `maSVHienTai` và `maGVHienTai` vào class `frmPhucKhao`
  - Implement method `KiemTraQuyenGhi(string thaoTac)` trả về `bool`
  - Implement method `KiemTraTrangThaiHienTai(string maPK)` trả về `string`
  - Implement method `GhiLichSu(string maPK, string thaoTac)` với try-catch graceful
  - **File:** `QLDSV.GUI/frmPhucKhao.cs`
  - **Requirements:** REQ-7, REQ-8, REQ-9

- [x] 2. Implement BoPhanQuyen() và cập nhật frmPhucKhao_Load
  - Implement method `BoPhanQuyen()`: ẩn/hiện nút theo role, lấy `maSVHienTai`/`maGVHienTai` từ DB
  - Xử lý trường hợp `MaVaiTro` null/rỗng/không hợp lệ → hiển thị lỗi và đóng form
  - Cập nhật `frmPhucKhao_Load`: gọi `BoPhanQuyen()` trước khi tải dữ liệu
  - **File:** `QLDSV.GUI/frmPhucKhao.cs`
  - **Requirements:** REQ-1, REQ-2.5, REQ-2.6

- [x] 3. Implement TaiDuLieuTheoQuyen() thay thế LoadData()
  - Implement method `TaiDuLieuTheoQuyen()`: xây SQL WHERE theo role (VT001/VT002/VT003)
  - Gọi `CapNhatThongKeTheoQuyen()` ở cuối method
  - Thay tất cả lời gọi `LoadData()` trong form bằng `TaiDuLieuTheoQuyen()`
  - **File:** `QLDSV.GUI/frmPhucKhao.cs`
  - **Requirements:** REQ-2.1, REQ-2.2, REQ-2.3, REQ-2.4, REQ-2.7, REQ-2.8

- [x] 4. Implement CapNhatThongKeTheoQuyen() thay thế UpdateStatistics()
  - Implement method `CapNhatThongKeTheoQuyen()`: đếm từ `tblPhucKhao` DataTable (không thêm SQL)
  - Thay tất cả lời gọi `UpdateStatistics()` bằng `CapNhatThongKeTheoQuyen()`
  - **File:** `QLDSV.GUI/frmPhucKhao.cs`
  - **Requirements:** REQ-6.1, REQ-6.2, REQ-6.3, REQ-6.4, REQ-6.5

- [ ] 5. Implement CapNhatTrangThaiNut() và cập nhật dataGridView_CellClick
  - Implement method `CapNhatTrangThaiNut(string trangThai, string maLHP)`
  - Logic: Admin enable khi "Chờ duyệt"; GV enable khi "Chờ duyệt" VÀ MaLHP thuộc lớp phụ trách; còn lại disable
  - Gọi `CapNhatTrangThaiNut()` trong `dataGridView_CellClick` sau khi đọc giá trị dòng
  - **File:** `QLDSV.GUI/frmPhucKhao.cs`
  - **Requirements:** REQ-3.6, REQ-3.7, REQ-3.8

- [~] 6. Implement NapComboLopHocPhanTheoQuyen() và cập nhật btnThem_Click
  - Implement method `NapComboLopHocPhanTheoQuyen()`: lọc `cboLopHocPhan` chỉ lớp SV đã đăng ký qua `DangKyHocPhan`
  - Xử lý trường hợp danh sách rỗng: disable `btnLuuDetail`, hiển thị thông báo
  - Cập nhật `btnThem_Click`: thêm guard `KiemTraQuyenGhi("THEM")`, xử lý UX panel đang mở, cố định `cboSinhVien` cho SV
  - **File:** `QLDSV.GUI/frmPhucKhao.cs`
  - **Requirements:** REQ-4.1, REQ-4.2, REQ-4.5, REQ-4.7

- [~] 7. Cập nhật btnDuyet_Click và btnTuChoi_Click
  - Thêm guard `KiemTraQuyenGhi("DUYET"/"TUCHOI")` ở đầu mỗi handler
  - Thêm kiểm tra phạm vi GV: xác nhận MaLHP thuộc lớp phụ trách trước khi UPDATE
  - Thêm `KiemTraTrangThaiHienTai()` để phát hiện concurrent modification
  - Thêm `GhiLichSu()` sau khi UPDATE thành công
  - Thay `LoadData()` bằng `TaiDuLieuTheoQuyen()`
  - **File:** `QLDSV.GUI/frmPhucKhao.cs`
  - **Requirements:** REQ-3.1, REQ-3.2, REQ-3.3, REQ-3.4, REQ-3.5, REQ-7.1, REQ-8.1, REQ-8.2, REQ-8.3, REQ-9.1, REQ-9.2

- [~] 8. Cập nhật btnXoa_Click và btnLuuDetail_Click
  - `btnXoa_Click`: thêm guard `KiemTraQuyenGhi("XOA")`, gọi `GhiLichSu()` trước DELETE, thay `LoadData()` bằng `TaiDuLieuTheoQuyen()`
  - `btnLuuDetail_Click`: thêm guard `KiemTraQuyenGhi("THEM")`, xác nhận MaSV cho SV, kiểm tra trùng lặp yêu cầu trước INSERT
  - **File:** `QLDSV.GUI/frmPhucKhao.cs`
  - **Requirements:** REQ-4.3, REQ-4.4, REQ-4.6, REQ-5.3, REQ-5.4, REQ-5.5, REQ-7.2, REQ-7.3, REQ-9.3
