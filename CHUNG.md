# HỆ THỐNG QUẢN LÝ ĐIỂM SINH VIÊN - CHUẨN THIẾT KẾ UI/UX (DESIGN SYSTEM)

Tài liệu này quy định các tiêu chuẩn thiết kế đồng bộ cho toàn bộ giao diện (UI) và trải nghiệm người dùng (UX) trong dự án. Tất cả các Form con khi xây dựng hoặc cập nhật đều phải tuân thủ nghiêm ngặt các quy số màu sắc, kiểu chữ, cấu trúc layout và thuộc tính của các control dưới đây nhằm đảm bảo tính thẩm mỹ, hiện đại và cao cấp.

---

## I. MÀU SẮC CHỦ ĐẠO (COLOR PALETTE)

Để đảm bảo giao diện có chiều sâu, hài hòa và chuyên nghiệp, toàn bộ dự án áp dụng hệ màu phối hợp (Harmonious Palette) dựa trên tone Xanh Hoàng Gia và Tím hiện đại:

| Phân Loại | Mã Hex | Mã RGB (Visual Studio) | Minh Họa | Vai Trò & Ứng Dụng |
| :--- | :--- | :--- | :--- | :--- |
| **Primary (Xanh Chủ Đạo)** | `#1565C0` | `21, 101, 192` | 🟦 **Royal Blue** | Dùng cho thanh Sidebar trái (`pnlSidebar`), Header phía trên (`pnlHeader`), và các phần tử điều hướng chính. |
| **Secondary (Tím Grid)** | `#6458FF` | `100, 88, 255` | 🟪 **Indigo Purple** | Màu nền của tiêu đề bảng dữ liệu (`DataGridView Header`). Mang lại cảm giác trẻ trung, hiện đại. |
| **Background (Nền Form)** | `#F0F2F5` | `240, 242, 245` | ⬜ **Light Gray (Xám nhạt)** | Màu nền chính của toàn bộ các Form con (xám nhạt nhạt), giúp làm nổi bật các panel và bảng dữ liệu. |
| **Card / Grid / Panel Nền**| `#FFFFFF` | `255, 255, 255` | ⬜ **Pure White (Trắng)** | Nền của các khung nhập liệu chi tiết, GroupBox, và đặc biệt là toàn bộ bảng GridView. |
| **Filter Panel (Khung Lọc)**| `#B4C8DF` | `180, 200, 223` | 🩵 **Slate Blue-Gray** | Nền của Panel lọc tìm kiếm ở phía trên lưới dữ liệu. |
| **Selected Highlight** | `#AED8F2` | `174, 216, 242` | 🩵 **Soft Sky Blue** | Màu nền khi dòng dữ liệu trong Grid được chọn (`Selection Color`). |
| **Button Active (Slate Blue)**| `#4F5D75` | `79, 93, 117` | ⬛ **Dark Slate** | Màu của các nút chức năng hoạt động chính (Thêm, Sửa, Làm mới, Xem). |
| **Button Disabled** | `#BDBDBD` | `189, 189, 189` | ⬜ **Light Gray** | Màu của các nút ở trạng thái vô hiệu hóa (Lưu, Hủy, Reset khi chưa chọn hành động). |

---

## II. HỆ THỐNG KIỂU CHỮ (TYPOGRAPHY)

Sử dụng duy nhất phông chữ **Segoe UI** để đảm bảo khả năng hiển thị chữ tiếng Việt sắc nét, rõ ràng và đồng bộ trên mọi độ phân giải màn hình.

* **Tiêu đề Form lớn (Form Title - Main Header)**:
  * Font: `Segoe UI`
  * Size: `18pt` hoặc `20pt`
  * Style: `Bold` (In hoa toàn bộ, ví dụ: **QUẢN LÝ LỚP NIÊN CHẾ**)
  * Color: `#1F2937` (RGB `31, 41, 55` - Đen Xám)
* **Tiêu đề phụ / Mô tả (Subtitle)**:
  * Font: `Segoe UI`
  * Size: `10pt` hoặc `11pt`
  * Style: `Regular` (Chữ thường, ví dụ: *Danh sách và thông tin lớp niên chế*)
  * Color: `#4B5563` (RGB `75, 85, 99` - Xám vừa)
* **Tiêu đề Khung thông tin (GroupBox / Panel Header)**:
  * Font: `Segoe UI`
  * Size: `12pt`
  * Style: `Bold & Italic` (Đậm nghiêng, ví dụ: ***Thông tin lớp niên chế***)
  * Color: `#1F2937` (RGB `31, 41, 55`)
* **Nhãn trường thông tin (Field Labels)**:
  * Font: `Segoe UI`
  * Size: `12pt`
  * Style: `Regular` hoặc `Semibold` (Ví dụ: `Mã lớp niên chế:`, `Tên lớp:`)
  * Color: `#000000` (Đen thuần)
* **Chữ hiển thị trong ô nhập / Combobox / Lưới**:
  * Font: `Segoe UI`
  * Size: `11pt` hoặc `12pt`
  * Style: `Regular`

---

## III. CHI TIẾT THUỘC TÍNH CÁC COMPONENT (GUNA UI & STANDARD CONTROLS)

Khi thiết kế hoặc kéo thả các control trên giao diện, cần cài đặt chính xác các thuộc tính sau trong bảng **Properties**:

### 1. Nút chức năng (Guna2Button / Button)
* **Bo tròn góc (Border Radius / BorderRadius)**: `5` (tương đương 5px). *Tuyệt đối không dùng nút vuông góc hoặc bo tròn quá đà dạng Oval.*
* **Kích thước chuẩn (Size)**:
  * Nút nhỏ / Lọc / Làm mới: `Width = 120`, `Height = 35`
  * Nút chức năng chính (Thêm, Sửa, Lưu, Hủy): `Width = 140`, `Height = 40`
* **Màu sắc nút hoạt động (Enabled Buttons)**:
  * `FillColor` (Màu nền): `79, 93, 117` (Slate Blue)
  * `ForeColor` (Màu chữ): `255, 255, 255` (Trắng)
  * `HoverState.FillColor` (Màu khi di chuột): `62, 78, 104` (Xanh Slate đậm hơn)
* **Màu sắc nút vô hiệu hóa (Disabled Buttons)**:
  * `DisabledState.FillColor`: `189, 189, 189` (Xám)
  * `DisabledState.ForeColor`: `100, 100, 100` (Xám đậm)
* **Icon kèm theo**:
  * Đặt căn lề trái, kích thước `20x20` pixel.
  * Sử dụng các icon phẳng đơn sắc (Flat Icon) màu đen hoặc trắng tùy theo trạng thái nút.

### 2. Ô nhập liệu (Guna2TextBox / TextBox)
* **Bo tròn góc (BorderRadius)**: `5`
* **Màu đường viền (BorderColor)**: `180, 200, 223` (Trùng với màu khung lọc)
* **Màu nền hoạt động (FillColor)**: `255, 255, 255` (Trắng)
* **Màu nền khi Read-Only / Disabled (FillColor)**: `230, 230, 230` (Xám nhạt)
* **Độ dày đường viền (BorderThickness)**: `1`

### 3. Hộp chọn (Guna2ComboBox / ComboBox)
* **Bo tròn góc (BorderRadius)**: `5`
* **Màu nền hoạt động (FillColor)**: `255, 255, 255`
* **Màu nền khi Read-Only / Disabled (FillColor)**: `230, 230, 230`
* **Kiểu hiển thị (DropDownStyle)**: `DropDownList`

### 4. Lưới hiển thị dữ liệu (Guna2DataGridView / DataGridView)
* **Định dạng Header bảng (`ColumnHeadersDefaultCellStyle`)**:
  * `BackColor`: `100, 88, 255` (Tím Indigo `#6458FF`)
  * `ForeColor`: `255, 255, 255` (Trắng)
  * `Font`: `Segoe UI`, `11pt` hoặc `12pt`, `Bold`
  * `Height`: `40`
* **Định dạng dòng dữ liệu (`DefaultCellStyle`)**:
  * `BackColor`: `255, 255, 255` (Trắng)
  * `ForeColor`: `0, 0, 0` (Đen)
  * `SelectionBackColor`: `174, 216, 242` (Sky Blue nhạt `#AED8F2`)
  * `SelectionForeColor`: `0, 0, 0` (Đen)
  * `Font`: `Segoe UI`, `11pt`
* **Các thuộc tính tối ưu hóa**:
  * `BackgroundColor` (Màu nền của Grid): `255, 255, 255` (Trắng tinh - bắt buộc để tạo khoảng trắng bao quanh sạch sẽ)
  * `AllowUserToAddRows`: `False`
  * `AutoSizeColumnsMode`: `Fill` (Tự động giãn đều cột)
  * `SelectionMode`: `FullRowSelect` (Chọn cả dòng khi click)
  * `GridColor`: `231, 229, 255` (Đường lưới mảnh, nhạt)

---

## IV. BỐ CỤC FORM TIÊU CHUẨN (STANDARD LAYOUT STRUCTURE)

Mỗi Form chức năng phải tuân thủ layout chia làm **4 khu vực cố định** từ trên xuống dưới:

```
+--------------------------------------------------------------------------+
|  1. TIÊU ĐỀ FORM (Header Title & Subtitle) - Bên cạnh có icon đại diện   |
+--------------------------------------------------------------------------+
|  2. KHUNG LỌC TÌM KIẾM (Filter Panel) - Nền màu Slate Blue-Gray #B4C8DF  |
|     [Ô tìm kiếm nhanh]    [Combobox Lọc 1]    [Combobox Lọc 2]  [Làm mới]|
+--------------------------------------------------------------------------+
|  3. BẢNG DỮ LIỆU CHÍNH (DataGridView) - Header Tím #6458FF               |
|     ======================= Dòng dữ liệu 1 =======================       |
|     ======================= Dòng dữ liệu 2 =======================       |
+--------------------------------------------------------------------------+
|  4. KHUNG CHI TIẾT & NÚT bấm (Detail Area & Action Buttons)              |
|     - Khung nhập thông tin chi tiết có nền trắng tinh (#FFFFFF)          |
|     - Các ô nhập xếp đều 2 bên cân đối.                                  |
|     - Hàng nút dưới cùng: [Thêm] [Sửa] [Lưu] [Hủy] [Reset]               |
+--------------------------------------------------------------------------+
```

---

## V. QUY TẮC PHẢN HỒI GIAO DIỆN (RESPONSIVE RULES & SHELL PATTERN)

Khi các Form con được nhúng trực tiếp vào trong giao diện chính (`frmMain` - Shell Form):
1. **Ẩn các Panel trùng lặp**: Các Form con khi chạy độc lập có `pnlSidebar` và `pnlHeader` riêng. Khi nhúng vào `frmMain`, Form con phải thực thi giao diện `IShellChildForm` để ẩn các Panel này đi và dịch chuyển các control còn lại sang trái (`Shift-Left`) nhằm tận dụng tối đa diện tích hiển thị.
2. **Neo góc các Control (Anchor)**:
   * **Filter Panel**: Đặt `Anchor = Top, Left, Right` để tự động kéo dài khi phóng to màn hình.
   * **DataGridView**: Đặt `Anchor = Top, Bottom, Left, Right` để co giãn cả chiều ngang lẫn chiều dọc.
   * **Detail Area**: Đặt `Anchor = Bottom, Left, Right` để luôn cố định ở phía dưới cùng và giãn rộng theo chiều ngang.
   * **Action Buttons Bar**: Đặt `Anchor = Bottom` hoặc neo căn giữa để nhóm nút luôn nằm chính giữa hoặc góc dưới bên phải cân đối.