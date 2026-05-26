# Requirements Document

## Introduction

Tính năng **Phân quyền hiển thị form Quản lý Phúc khảo theo Role** nhằm mục đích kiểm soát quyền truy cập và hiển thị dữ liệu trên form `frmPhucKhao` dựa theo vai trò của người dùng đang đăng nhập. Hiện tại form chỉ phục vụ Admin; sau khi triển khai tính năng này, mỗi vai trò (Admin, Giảng viên, Sinh viên) sẽ thấy đúng dữ liệu và đúng tập chức năng tương ứng với quyền hạn của mình.

Hệ thống sử dụng WinForms .NET (C#), thông tin phiên đăng nhập được lưu trong `SessionHelper` với thuộc tính `MaVaiTro` (VT001 = Admin, VT002 = Giảng viên, VT003 = Sinh viên).

---

## Glossary

- **Form_PhucKhao**: Form WinForms `frmPhucKhao` — giao diện quản lý yêu cầu phúc khảo.
- **SessionHelper**: Class tĩnh lưu thông tin phiên đăng nhập, gồm `MaTaiKhoan`, `TenDangNhap`, `MaVaiTro`.
- **MaVaiTro**: Mã vai trò của tài khoản đang đăng nhập. Giá trị hợp lệ: `VT001`, `VT002`, `VT003`.
- **Admin**: Người dùng có `MaVaiTro = VT001`. Có toàn quyền trên form.
- **Giảng_Viên**: Người dùng có `MaVaiTro = VT002`. Chỉ thao tác trên dữ liệu thuộc lớp học phần mình phụ trách.
- **Sinh_Viên**: Người dùng có `MaVaiTro = VT003`. Chỉ xem và nộp yêu cầu phúc khảo của chính mình.
- **FunctionQa**: Class tiện ích truy vấn cơ sở dữ liệu (tương đương `Functions`), cung cấp các phương thức `ketnoi()`, `getdatatotable()`, `fillcombo()`, `runsql()`, `RunSqlDel()`, `getfieldvalue()`.
- **BoPhanQuyen**: Phương thức khởi tạo phân quyền được gọi trong `frmPhucKhao_Load`, chịu trách nhiệm ẩn/hiện nút và lọc dữ liệu theo `MaVaiTro`.
- **TaiDuLieuTheoQuyen**: Phương thức tải dữ liệu vào `dataGridView` với câu truy vấn SQL được lọc theo vai trò.
- **YeuCauPhucKhao**: Một bản ghi trong bảng `PhucKhao`, gồm `MaPhucKhao`, `MaSV`, `MaLHP`, `NgayYeuCau`, `TrangThai`, `LyDo`.
- **LopHocPhan_PhuTrach**: Tập hợp các lớp học phần mà Giảng_Viên đang đăng nhập phụ trách, xác định qua `LopHocPhan.MaGV = GiangVien.MaGV` và `GiangVien.MaTaiKhoan = SessionHelper.MaTaiKhoan`.
- **MaSV_CuaSinhVien**: Mã sinh viên tương ứng với tài khoản đang đăng nhập, xác định qua `SinhVien.MaTaiKhoan = SessionHelper.MaTaiKhoan`.
- **HocKyHienTai**: Học kỳ hiện tại, xác định qua bảng `HocKy` với điều kiện `NgayBatDau <= GETDATE() AND NgayKetThuc >= GETDATE()`; nếu bảng `HocKy` không tồn tại, hệ thống không lọc theo học kỳ.
- **LichSuPhucKhao**: Bảng ghi lịch sử thao tác duyệt/từ chối/xóa yêu cầu phúc khảo, gồm các trường `MaPhucKhao`, `MaTaiKhoan`, `ThaoTac`, `ThoiGian`; bảng này là tùy chọn — hệ thống hoạt động bình thường nếu bảng không tồn tại.

---

## Requirements

### Requirement 1: Khởi tạo phân quyền khi mở form

**User Story:** Là một người dùng hệ thống, tôi muốn form Quản lý Phúc khảo tự động điều chỉnh giao diện và dữ liệu ngay khi mở, để tôi chỉ thấy và thao tác được những gì phù hợp với vai trò của mình.

#### Acceptance Criteria

1. WHEN `frmPhucKhao_Load` được kích hoạt, THE `Form_PhucKhao` SHALL đọc giá trị `SessionHelper.MaVaiTro`, gọi `BoPhanQuyen` để hoàn tất cấu hình giao diện, và chỉ sau đó mới tải dữ liệu lên `dataGridView`.
2. IF `SessionHelper.MaVaiTro` bằng `VT001`, THEN THE `Form_PhucKhao` SHALL đặt `Visible = true` cho tất cả các nút `btnThem`, `btnDuyet`, `btnTuChoi`, `btnXoa`, `btnLamMoi`.
3. IF `SessionHelper.MaVaiTro` bằng `VT002`, THEN THE `Form_PhucKhao` SHALL đặt `Visible = false` cho `btnThem` và `btnXoa`; đặt `Visible = true` cho `btnDuyet`, `btnTuChoi`, `btnLamMoi`.
4. IF `SessionHelper.MaVaiTro` bằng `VT003`, THEN THE `Form_PhucKhao` SHALL đặt `Visible = false` cho `btnDuyet`, `btnTuChoi`, `btnXoa`; đặt `Visible = true` cho `btnThem` và `btnLamMoi`.
5. IF `SessionHelper.MaVaiTro` là `null`, rỗng, hoặc không thuộc tập `{VT001, VT002, VT003}`, THEN THE `Form_PhucKhao` SHALL hiển thị thông báo lỗi "Không xác định được vai trò người dùng.", không tải dữ liệu lên `dataGridView`, và đóng form.

---

### Requirement 2: Lọc dữ liệu hiển thị theo vai trò

**User Story:** Là một người dùng hệ thống, tôi muốn danh sách yêu cầu phúc khảo chỉ hiển thị những bản ghi liên quan đến tôi, để tôi không thấy dữ liệu của người khác.

#### Acceptance Criteria

1. WHEN `Form_PhucKhao` được tải với `SessionHelper.MaVaiTro` bằng `VT001`, THE `Form_PhucKhao` SHALL tải toàn bộ bản ghi từ bảng `PhucKhao` vào `dataGridView` mà không áp dụng bộ lọc theo người dùng.
2. WHEN `Form_PhucKhao` được tải với `SessionHelper.MaVaiTro` bằng `VT002`, THE `Form_PhucKhao` SHALL chỉ tải các `YeuCauPhucKhao` có `MaLHP` thuộc `LopHocPhan_PhuTrach` của `Giảng_Viên` đang đăng nhập vào `dataGridView`.
3. WHEN `Form_PhucKhao` được tải với `SessionHelper.MaVaiTro` bằng `VT003`, THE `Form_PhucKhao` SHALL chỉ tải các `YeuCauPhucKhao` có `MaSV` bằng `MaSV_CuaSinhVien` của tài khoản đang đăng nhập vào `dataGridView`.
4. WHEN `TaiDuLieuTheoQuyen` được gọi thành công, THE `Form_PhucKhao` SHALL xây dựng câu truy vấn SQL có mệnh đề `WHERE` phù hợp với `MaVaiTro` và thực thi qua `FunctionQa`.
5. IF `Giảng_Viên` đang đăng nhập không có bản ghi tương ứng trong bảng `GiangVien` theo `MaTaiKhoan`, THEN THE `Form_PhucKhao` SHALL hiển thị `dataGridView` rỗng và thông báo cho người dùng biết không tìm thấy thông tin giảng viên liên kết với tài khoản này.
6. IF `Sinh_Viên` đang đăng nhập không có bản ghi tương ứng trong bảng `SinhVien` theo `MaTaiKhoan`, THEN THE `Form_PhucKhao` SHALL hiển thị `dataGridView` rỗng và thông báo cho người dùng biết không tìm thấy thông tin sinh viên liên kết với tài khoản này.
7. IF quá trình xây dựng hoặc thực thi câu truy vấn SQL trong `TaiDuLieuTheoQuyen` thất bại, THEN THE `Form_PhucKhao` SHALL hiển thị thông báo lỗi mô tả nguyên nhân và không hiển thị dữ liệu lên `dataGridView`.
8. IF `SessionHelper.MaVaiTro` là `null` hoặc không thuộc tập `{VT001, VT002, VT003}` khi `TaiDuLieuTheoQuyen` được gọi, THEN THE `Form_PhucKhao` SHALL không thực thi truy vấn và hiển thị `dataGridView` rỗng.

---

### Requirement 3: Quyền duyệt và từ chối yêu cầu phúc khảo

**User Story:** Là một Giảng viên hoặc Admin, tôi muốn có thể duyệt hoặc từ chối các yêu cầu phúc khảo, nhưng chỉ trong phạm vi quyền hạn của mình.

#### Acceptance Criteria

1. WHEN `Admin` nhấn `btnDuyet` trên một `YeuCauPhucKhao` có `TrangThai = 'Chờ duyệt'`, THE `Form_PhucKhao` SHALL cập nhật `TrangThai` của bản ghi đó thành `'Đã duyệt'` và tải lại `dataGridView`.
2. WHEN `Admin` nhấn `btnTuChoi` trên một `YeuCauPhucKhao` có `TrangThai = 'Chờ duyệt'`, THE `Form_PhucKhao` SHALL cập nhật `TrangThai` của bản ghi đó thành `'Từ chối'` và tải lại `dataGridView`.
3. WHEN `Giảng_Viên` nhấn `btnDuyet` trên một `YeuCauPhucKhao` có `MaLHP` thuộc `LopHocPhan_PhuTrach` và `TrangThai = 'Chờ duyệt'`, THE `Form_PhucKhao` SHALL cập nhật `TrangThai` thành `'Đã duyệt'` và tải lại `dataGridView`.
4. WHEN `Giảng_Viên` nhấn `btnTuChoi` trên một `YeuCauPhucKhao` có `MaLHP` thuộc `LopHocPhan_PhuTrach` và `TrangThai = 'Chờ duyệt'`, THE `Form_PhucKhao` SHALL cập nhật `TrangThai` thành `'Từ chối'` và tải lại `dataGridView`.
5. IF `Giảng_Viên` cố gắng duyệt hoặc từ chối một `YeuCauPhucKhao` không thuộc `LopHocPhan_PhuTrach` của mình, THEN THE `Form_PhucKhao` SHALL hủy thao tác, giữ nguyên `TrangThai` hiện tại của bản ghi, và thông báo cho người dùng biết họ không có quyền thực hiện thao tác này.
6. WHEN một `YeuCauPhucKhao` có `TrangThai = 'Chờ duyệt'` được chọn, THE `Form_PhucKhao` SHALL đặt `btnDuyet.Enabled = true` và `btnTuChoi.Enabled = true` cho cả `Admin` lẫn `Giảng_Viên` (nếu bản ghi thuộc `LopHocPhan_PhuTrach`).
7. WHEN một `YeuCauPhucKhao` có `TrangThai` khác `'Chờ duyệt'` (bao gồm `'Đã duyệt'` và `'Từ chối'`) được chọn, THE `Form_PhucKhao` SHALL đặt `btnDuyet.Enabled = false` và `btnTuChoi.Enabled = false` cho cả `Admin` lẫn `Giảng_Viên`; IF người dùng nhấn `btnDuyet` hoặc `btnTuChoi` khi nút đang ở trạng thái `Enabled = false`, THE `Form_PhucKhao` SHALL không thực thi thao tác.
8. WHEN `Giảng_Viên` chọn một `YeuCauPhucKhao` có `MaLHP` không thuộc `LopHocPhan_PhuTrach` của mình, THE `Form_PhucKhao` SHALL đặt `btnDuyet.Enabled = false` và `btnTuChoi.Enabled = false` bất kể `TrangThai` của bản ghi đó là gì.

---

### Requirement 4: Quyền thêm mới yêu cầu phúc khảo (Sinh viên)

**User Story:** Là một Sinh viên, tôi muốn nộp yêu cầu phúc khảo cho lớp học phần mình đã đăng ký, để đề nghị xem xét lại điểm số.

#### Acceptance Criteria

1. WHEN `Sinh_Viên` nhấn `btnThem`, THE `Form_PhucKhao` SHALL mở panel chi tiết ở chế độ thêm mới với `cboSinhVien` đã được cố định sẵn giá trị `MaSV_CuaSinhVien`, đặt `cboSinhVien.Enabled = false` để không cho phép thay đổi.
2. WHILE `Sinh_Viên` đang thêm mới, THE `Form_PhucKhao` SHALL chỉ hiển thị trong `cboLopHocPhan` các lớp học phần mà `Sinh_Viên` đó đã đăng ký, xác định qua bảng `DangKyHocPhan` với `MaSV = MaSV_CuaSinhVien`; nếu hệ thống có bảng `HocKy`, chỉ lấy các lớp thuộc `HocKyHienTai`, ngược lại lấy toàn bộ lớp đã đăng ký; IF danh sách lớp học phần rỗng, THEN THE `Form_PhucKhao` SHALL đặt `btnLuuDetail.Enabled = false` và thông báo cho sinh viên biết chưa đăng ký lớp học phần nào.
3. WHEN `Sinh_Viên` nhấn `btnLuuDetail` và lưu thành công, THE `Form_PhucKhao` SHALL đóng panel chi tiết và làm mới danh sách `dataGridView` để hiển thị yêu cầu vừa nộp.
4. IF `MaSV` trong bản ghi cần lưu khác `MaSV_CuaSinhVien`, THEN THE `Form_PhucKhao` SHALL hủy thao tác lưu và hiển thị thông báo lỗi cho người dùng.
5. WHEN `Sinh_Viên` nhấn `btnThem`, THE `Form_PhucKhao` SHALL tự động đặt `TrangThai` của yêu cầu mới là `'Chờ duyệt'`, đặt `txtTrangThai.ReadOnly = true` để không cho phép `Sinh_Viên` thay đổi giá trị này.
6. IF `Sinh_Viên` cố gắng nộp yêu cầu phúc khảo cho một lớp học phần mà mình đã có yêu cầu đang ở trạng thái `'Chờ duyệt'` hoặc `'Đã duyệt'`, THEN THE `Form_PhucKhao` SHALL hủy thao tác lưu và thông báo cho sinh viên biết đã tồn tại yêu cầu phúc khảo cho lớp học phần này.
7. WHEN `Sinh_Viên` nhấn `btnThem` trong khi `pnlDetail` đang mở ở chế độ thêm mới, THE `Form_PhucKhao` SHALL không mở thêm panel mới mà giữ nguyên trạng thái hiện tại; WHEN `Sinh_Viên` nhấn `btnThem` trong khi `pnlDetail` đang mở ở chế độ xem, THE `Form_PhucKhao` SHALL đóng panel xem hiện tại và mở lại ở chế độ thêm mới.

---

### Requirement 5: Quyền xóa yêu cầu phúc khảo (chỉ Admin)

**User Story:** Là một Admin, tôi muốn có thể xóa các yêu cầu phúc khảo không hợp lệ hoặc trùng lặp, để giữ dữ liệu hệ thống sạch sẽ.

#### Acceptance Criteria

1. IF `SessionHelper.MaVaiTro` bằng `VT001`, THEN THE `Form_PhucKhao` SHALL đặt `btnXoa.Visible = true` và `btnXoa.Enabled = true`.
2. IF `SessionHelper.MaVaiTro` bằng `VT002` hoặc `VT003`, THEN THE `Form_PhucKhao` SHALL đặt `btnXoa.Visible = false` và vô hiệu hóa chức năng xóa ở tầng xử lý để ngăn thực thi dù nút có hiển thị qua bất kỳ cách nào.
3. WHEN `Admin` nhấn `btnXoa` mà không có bản ghi nào được chọn trong `dataGridView`, THE `Form_PhucKhao` SHALL hiển thị thông báo yêu cầu chọn bản ghi và không thực thi xóa.
4. WHEN `Admin` nhấn `btnXoa` với một bản ghi đang được chọn và xác nhận trong hộp thoại, THE `Form_PhucKhao` SHALL thực thi xóa bản ghi qua `FunctionQa.RunSqlDel` và tải lại `dataGridView`; WHEN `Admin` hủy trong hộp thoại xác nhận, THE `Form_PhucKhao` SHALL không thay đổi dữ liệu và giữ nguyên trạng thái chọn.
5. IF `FunctionQa.RunSqlDel` thất bại khi xóa, THEN THE `Form_PhucKhao` SHALL hiển thị thông báo lỗi mô tả nguyên nhân, giữ nguyên dữ liệu `dataGridView` và trạng thái chọn hiện tại.

---

### Requirement 6: Hiển thị thống kê theo phạm vi quyền

**User Story:** Là một người dùng hệ thống, tôi muốn các con số thống kê trên form (Tổng, Chờ duyệt, Đã duyệt, Từ chối) phản ánh đúng phạm vi dữ liệu tôi được phép xem, để tôi có cái nhìn chính xác về tình trạng phúc khảo.

#### Acceptance Criteria

1. IF `SessionHelper.MaVaiTro` bằng `VT001`, THEN THE `Form_PhucKhao` SHALL tính `lblStatTongVal`, `lblStatChoDuyetVal`, `lblStatDaDuyetVal`, `lblStatTuChoiVal` trên toàn bộ bảng `PhucKhao`, trong đó `lblStatTongVal` bằng tổng của ba giá trị còn lại.
2. IF `SessionHelper.MaVaiTro` bằng `VT002`, THEN THE `Form_PhucKhao` SHALL tính thống kê chỉ trên các `YeuCauPhucKhao` thuộc `LopHocPhan_PhuTrach` của `Giảng_Viên` đang đăng nhập, trong đó `lblStatTongVal` bằng tổng của `lblStatChoDuyetVal`, `lblStatDaDuyetVal`, `lblStatTuChoiVal`.
3. IF `SessionHelper.MaVaiTro` bằng `VT003`, THEN THE `Form_PhucKhao` SHALL tính thống kê chỉ trên các `YeuCauPhucKhao` có `MaSV` bằng `MaSV_CuaSinhVien`, trong đó `lblStatTongVal` bằng tổng của `lblStatChoDuyetVal`, `lblStatDaDuyetVal`, `lblStatTuChoiVal`.
4. WHEN `TaiDuLieuTheoQuyen` hoàn thành, THE `Form_PhucKhao` SHALL gọi `CapNhatThongKeTheoQuyen` để cập nhật các nhãn thống kê bằng cách đếm trực tiếp từ `tblPhucKhao` (DataTable đã tải lên `dataGridView`) theo cột `TrangThai`, không thực thi thêm câu truy vấn SQL riêng, đảm bảo số liệu thống kê khớp 100% với dữ liệu đang hiển thị.
5. IF `SessionHelper.MaVaiTro` không thuộc tập `{VT001, VT002, VT003}` khi `CapNhatThongKeTheoQuyen` được gọi, THEN THE `Form_PhucKhao` SHALL đặt tất cả nhãn thống kê về `"0"` và không thực thi truy vấn.

---

### Requirement 7: Bảo vệ thao tác bằng kiểm tra quyền phía server

**User Story:** Là quản trị viên hệ thống, tôi muốn mọi thao tác ghi dữ liệu (thêm, duyệt, từ chối, xóa) đều được kiểm tra quyền ngay trước khi thực thi SQL, để ngăn chặn trường hợp người dùng vượt quyền do lỗi giao diện.

#### Acceptance Criteria

1. WHEN bất kỳ thao tác ghi nào (`INSERT`, `UPDATE`, `DELETE`) được kích hoạt, THE `Form_PhucKhao` SHALL đọc lại `SessionHelper.MaVaiTro` ngay trước khi gọi `FunctionQa` để xác nhận quyền còn hiệu lực.
2. IF `SessionHelper.MaVaiTro` tại thời điểm thực thi không có quyền thực hiện thao tác tương ứng, THEN THE `Form_PhucKhao` SHALL hủy thao tác, không gọi `FunctionQa`, và thông báo cho người dùng biết họ không có quyền thực hiện thao tác này.
3. THE `Form_PhucKhao` SHALL thực hiện kiểm tra quyền trong thân phương thức xử lý sự kiện (không chỉ dựa vào trạng thái `Visible` hoặc `Enabled` của nút bấm) trước mỗi lần gọi `FunctionQa.runsql`, `FunctionQa.RunSqlDel`.
4. IF `SessionHelper.MaVaiTro` là `null` hoặc rỗng tại thời điểm thực thi thao tác ghi, THEN THE `Form_PhucKhao` SHALL hủy thao tác và thông báo lỗi phiên đăng nhập không hợp lệ.

---

### Requirement 8: Xử lý truy cập đồng thời (Concurrent Access)

**User Story:** Là một người dùng hệ thống, tôi muốn được thông báo khi một yêu cầu phúc khảo đã được xử lý bởi người khác trước khi tôi thực hiện thao tác, để tránh thao tác trên dữ liệu đã lỗi thời.

#### Acceptance Criteria

1. WHEN `Admin` hoặc `Giảng_Viên` nhấn `btnDuyet` hoặc `btnTuChoi`, THE `Form_PhucKhao` SHALL truy vấn lại `TrangThai` hiện tại của `YeuCauPhucKhao` từ cơ sở dữ liệu ngay trước khi thực thi `UPDATE`.
2. IF `TrangThai` đọc lại từ DB khác với `TrangThai` đang hiển thị trên `dataGridView` (tức là bản ghi đã bị thay đổi bởi người dùng khác), THEN THE `Form_PhucKhao` SHALL hủy thao tác, thông báo cho người dùng biết yêu cầu này đã được cập nhật bởi người khác, và tải lại `dataGridView` để hiển thị trạng thái mới nhất.
3. IF `TrangThai` đọc lại từ DB không còn là `'Chờ duyệt'`, THEN THE `Form_PhucKhao` SHALL không thực thi `UPDATE` và thông báo cho người dùng biết yêu cầu đã được xử lý trước đó.

---

### Requirement 9: Ghi nhận lịch sử thao tác (Audit Log)

**User Story:** Là quản trị viên hệ thống, tôi muốn mọi thao tác duyệt, từ chối và xóa yêu cầu phúc khảo đều được ghi lại, để có thể truy vết khi có tranh chấp về điểm số.

#### Acceptance Criteria

1. WHEN `Admin` hoặc `Giảng_Viên` thực thi thành công thao tác duyệt (`TrangThai` → `'Đã duyệt'`), THE `Form_PhucKhao` SHALL ghi một bản ghi vào bảng `LichSuPhucKhao` (nếu tồn tại) với các trường: `MaPhucKhao`, `MaTaiKhoan` (người thực hiện), `ThaoTac = 'Duyệt'`, `ThoiGian = GETDATE()`.
2. WHEN `Admin` hoặc `Giảng_Viên` thực thi thành công thao tác từ chối (`TrangThai` → `'Từ chối'`), THE `Form_PhucKhao` SHALL ghi bản ghi vào `LichSuPhucKhao` với `ThaoTac = 'Từ chối'` và các trường tương tự.
3. WHEN `Admin` thực thi thành công thao tác xóa, THE `Form_PhucKhao` SHALL ghi bản ghi vào `LichSuPhucKhao` với `ThaoTac = 'Xóa'` trước khi thực thi `DELETE`.
4. IF bảng `LichSuPhucKhao` không tồn tại trong cơ sở dữ liệu, THEN THE `Form_PhucKhao` SHALL bỏ qua bước ghi log mà không làm gián đoạn thao tác chính; việc thiếu bảng log không được gây lỗi hoặc rollback thao tác.
