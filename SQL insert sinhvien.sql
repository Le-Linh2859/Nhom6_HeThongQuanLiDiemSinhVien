USE DB_QLDiemSinhVien;
GO

Select * from VaiTro
INSERT INTO VaiTro (MaVaiTro, TenVaiTro)
VALUES 
(N'VT001', N'quản trị viên'),
(N'VT002', N'giảng viên'),
(N'VT003', N'sinh viên');

Select * from TaiKhoan
INSERT INTO dbo.TaiKhoan (MaTaiKhoan, TenDangNhap, MatKhau, TrangThai, MaVaiTro)
VALUES
(N'TK001', N'SV20260001', N'Sinhvien01@', 1, N'VT003'),
(N'TK002', N'SV20260002', N'Sinhvien02@', 1, N'VT003'),
(N'TK003', N'SV20260003', N'Sinhvien03@', 1, N'VT003'),
(N'TK004', N'SV20260004', N'Sinhvien04@', 1, N'VT003'),
(N'TK005', N'SV20260005', N'Sinhvien05@', 1, N'VT003'),
(N'TK006', N'Admin2025', N'Admin123@', 1, N'VT001'),
(N'TK007', N'GV20260001', N'Giangvien01@', 1, N'VT002'),
(N'TK008', N'GV20260002', N'Giangvien02@', 1, N'VT002'),
(N'TK009', N'GV20260003', N'Giangvien03@', 1, N'VT002'),
(N'TK010', N'GV20260004', N'Giangvien04@', 1, N'VT002'),
(N'TK011', N'GV20260005', N'Giangvien05@', 1, N'VT002');
INSERT INTO dbo.TaiKhoan (MaTaiKhoan, TenDangNhap, MatKhau, TrangThai, MaVaiTro)
VALUES
(N'TK012', N'GV20260006', N'Giangvien06@', 1, N'VT002'),
(N'TK013', N'GV20260007', N'Giangvien07@', 1, N'VT002'),
(N'TK014', N'GV20260008', N'Giangvien08@', 1, N'VT002');

select * from Khoa
INSERT INTO dbo.Khoa (MaKhoa, TenKhoa, NamThanhLap, MoTa)
VALUES
(N'BKN', N'Khoa Ngân hàng', 1961, N'Đào tạo chuyên sâu về nghiệp vụ ngân hàng, tín dụng và quản trị NH'),
(N'FIN', N'Khoa Tài chính', 1961, N'Đào tạo về tài chính doanh nghiệp, tài chính công'),
(N'ACC', N'Khoa Kế toán - Kiểm toán', 1961, N'Đào tạo kế toán, kiểm toán và hệ thống thông tin kế toán'),
(N'FBA', N'Khoa Quản trị kinh doanh', 2000, N'Đào tạo quản trị doanh nghiệp, marketing, chiến lược'),
(N'ITDE', N'Khoa Công nghệ thông tin và Kinh tế số', 2005, N'Đào tạo CNTT ứng dụng trong quản lý và kinh doanh'),
(N'ECO', N'Khoa Kinh tế', 2003, N'Đào tạo kinh tế học, kinh tế quốc tế'),
(N'LAW', N'Khoa Luật', 2010, N'Đào tạo pháp luật kinh tế, luật ngân hàng'),
(N'FFL', N'Khoa Ngoại ngữ', 2008, N'Đào tạo tiếng Anh và ngoại ngữ chuyên ngành tài chính – ngân hàng');

Select * from GiangVien
INSERT INTO dbo.GiangVien (MaGV, HoTen, GioiTinh, DiaChi, Email, SoDT, MaTaiKhoan, MaKhoa)
VALUES
(N'GV20260001', N'Nguyễn Văn Hùng', 1, N'Hà Nội', N'hung.nguyen@gv.edu.vn', N'0912341111', N'TK007', N'BKN'),
(N'GV20260002', N'Trần Thị Lan', 0, N'Hải Phòng', N'lan.tran@gv.edu.vn', N'0987652222', N'TK008', N'FIN'),
(N'GV20260003', N'Lê Quang Minh', 1, N'Nam Định', N'minh.le@gv.edu.vn', N'0934563333', N'TK009', N'FBA'),
(N'GV20260004', N'Phạm Thu Hương', 0, N'Hà Nam', N'huong.pham@gv.edu.vn', N'0978124444', N'TK010', N'ITDE'),
(N'GV20260005', N'Đỗ Văn Nam', 1, N'Bắc Ninh', N'nam.do@gv.edu.vn', N'0961235555', N'TK011', N'ECO');
INSERT INTO dbo.GiangVien (MaGV, HoTen, GioiTinh, DiaChi, Email, SoDT, MaTaiKhoan, MaKhoa)
VALUES
(N'GV20260006', N'Nguyễn Thị Hoa', 0, N'Hà Nội', N'hoa.nguyen@gv.edu.vn', '0983127456', N'TK012', N'ACC'),
(N'GV20260007', N'Phan Văn Đức', 1, N'Hà Nội', N'duc.phan@gv.edu.vn', '0915872364', N'TK013', N'LAW'),
(N'GV20260008', N'Trần Thị Mai', 0, N'Hà Nội', N'mai.tran@gv.edu.vn', '0962749185', N'TK014', N'FFL');

INSERT INTO dbo.LopNienChe (MaLopNienChe, TenLop, NienKhoa, MaKhoa, MaGV)
VALUES
(N'K26NHA', N'Ngân hàng A khóa 2024', N'2024-2028', N'BKN', N'GV20260001'),
(N'K27NHA', N'Ngân hàng A khóa 2025', N'2025-2029', N'BKN', N'GV20260001'),

(N'K26TCA', N'Tài chính A khóa 2024', N'2024-2028', N'FIN', N'GV20260002'),
(N'K27TCA', N'Tài chính A khóa 2025', N'2025-2029', N'FIN', N'GV20260002'),

(N'K26KTA', N'Kế toán A khóa 2024', N'2024-2028', N'ACC', N'GV20260006'),
(N'K27KTA', N'Kế toán A khóa 2025', N'2025-2029', N'ACC', N'GV20260006'),

(N'K26QTA', N'Quản trị kinh doanh A 2024', N'2024-2028', N'FBA', N'GV20260003'),
(N'K27QTA', N'Quản trị kinh doanh A 2025', N'2025-2029', N'FBA', N'GV20260003'),

(N'K26HTTTA', N'Hệ thống thông tin A 2024', N'2024-2028', N'ITDE', N'GV20260004'),
(N'K27HTTTB', N'Hệ thống thông tin A 2025', N'2025-2029', N'ITDE', N'GV20260004'),

(N'K26KTE', N'Kinh tế A khóa 2024', N'2024-2028', N'ECO', N'GV20260005'),
(N'K27KTE', N'Kinh tế A khóa 2025', N'2025-2029', N'ECO', N'GV20260005'),

(N'K26LKA', N'Luật kinh tế A 2024', N'2024-2028', N'LAW', N'GV20260007'),
(N'K27LKA', N'Luật kinh tế A 2025', N'2025-2029', N'LAW', N'GV20260007'),

(N'K26NNA', N'Ngoại ngữ A khóa 2024', N'2024-2028', N'FFL', N'GV20260008'),
(N'K27NNA', N'Ngoại ngữ A khóa 2025', N'2025-2029', N'FFL', N'GV20260008');

select * from SinhVien 
INSERT INTO dbo.SinhVien 
(MaSV, HoTen, NgaySinh, GioiTinh, DiaChi, SoDT, Email, NienKhoa, MaTaiKhoan, MaLopNienChe)
VALUES
(N'SV20260001', N'Nguyễn Văn An', '2005-03-15', 1, N'Hà Nội', '0912345678', N'an.nguyen2601@gmail.com', N'2024-2028', N'TK001', N'K26NHA'),

(N'SV20260002', N'Trần Thị Bình', '2005-07-20', 0, N'Hải Phòng', '0987654321', N'binh.tran2602@gmail.com', N'2024-2028', N'TK002', N'K26TCA'),

(N'SV20260003', N'Lê Minh Hoàng', '2005-01-10', 1, N'Nam Định', '0934567890', N'hoang.le2603@gmail.com', N'2024-2028', N'TK003', N'K26KTA'),

(N'SV20260004', N'Phạm Thu Trang', '2007-11-05', 0, N'Hà Nam', '0978123456', N'trang.pham2701@gmail.com', N'2025-2029', N'TK004', N'K27QTA'),

(N'SV20260005', N'Đỗ Quang Huy', '2007-05-22', 1, N'Thái Bình', '0961237890', N'huy.do2702@gmail.com', N'2025-2029', N'TK005', N'K27HTTTB');

Select * from MonHoc
INSERT INTO dbo.MonHoc (MaMon, TenMon, SoTC, MoTa, MaKhoa)
VALUES
(N'BNK01', N'Tín dụng ngân hàng', 3, N'Quy trình cấp tín dụng và quản lý rủi ro tín dụng', N'BKN'),
(N'FIN01', N'Tài chính doanh nghiệp', 3, N'Quản lý tài chính trong doanh nghiệp', N'FIN'),
(N'ACC01', N'Nguyên lý kế toán', 3, N'Các nguyên lý cơ bản của kế toán', N'ACC'),
(N'ACC02', N'Quản trị học', 3, N'Kiến thức nền tảng về quản trị doanh nghiệp', N'ACC'),
(N'FBA01', N'Marketing căn bản', 2, N'Nguyên lý marketing và hành vi khách hàng', N'FBA'),
(N'ITDE01', N'Cơ sở dữ liệu', 3, N'Thiết kế và quản lý cơ sở dữ liệu', N'ITDE'),
(N'ECO01', N'Hệ thống thông tin quản lý', 3, N'Ứng dụng CNTT trong quản lý', N'ECO'),
(N'LAW01', N'Kinh tế vĩ mô', 3, N'Các yếu tố ảnh hưởng đến nền kinh tế', N'LAW'),
(N'LAW02', N'Luật ngân hàng', 2, N'Pháp luật liên quan đến hoạt động ngân hàng', N'LAW'),
(N'FFL01', N'Tiếng Anh chuyên ngành tài chính', 3, N'Tiếng Anh dùng trong lĩnh vực tài chính - ngân hàng', N'FFL');

select * from LoaiHocKy
INSERT INTO dbo.LoaiHocKy (MaLoaiHK, TenLoaiHK, ThangBD, ThangKT)
VALUES
(N'Hocky_1', N'Học kỳ 1', 8, 12),
(N'Hocky_2', N'Học kỳ 2', 1, 5),
(N'Hocky_3', N'Học hè', 6, 7);

Select * from NamHoc
INSERT INTO dbo.NamHoc (MaNamHoc, TenNamHoc)
VALUES
(N'NH_22_23', N'Năm học 2022-2023'),
(N'NH_23_24', N'Năm học 2023-2024'),
(N'NH_24_25', N'Năm học 2024-2025'),
(N'NH_25_26', N'Năm học 2025-2026'),
(N'NH_26_27', N'Năm học 2026-2027');

Select * from LoaiDiem
INSERT INTO dbo.LoaiDiem (MaLoaiDiem, TenLoaiDiem, TyLePhanTram)
VALUES
(N'CC', N'Điểm chuyên cần', 10),
(N'KT1', N'Điểm kiểm tra 1', 15),
(N'KT2', N'Điểm kiểm tra 2', 15),
(N'CK', N'Điểm cuối kì', 60);

EXEC sp_rename 'dbo.HocKy', 'HocKy_NamHoc';
select * from HocKy_NamHoc
INSERT INTO dbo.HocKy_NamHoc (MaHKNH, MaLoaiHK, MaNamHoc)
VALUES
(N'HK001', N'Hocky_1', N'NH_22_23'),
(N'HK002', N'Hocky_2', N'NH_22_23'),
(N'HK003', N'Hocky_3', N'NH_22_23'),

(N'HK004', N'Hocky_1', N'NH_23_24'),
(N'HK005', N'Hocky_2', N'NH_23_24'),
(N'HK006', N'Hocky_3', N'NH_23_24'),

(N'HK007', N'Hocky_1', N'NH_24_25'),
(N'HK008', N'Hocky_2', N'NH_24_25'),
(N'HK009', N'Hocky_3', N'NH_24_25'),

(N'HK010', N'Hocky_1', N'NH_24_25'),
(N'HK011', N'Hocky_2', N'NH_24_25'),
(N'HK012', N'Hocky_3', N'NH_24_25'),

(N'HK013', N'Hocky_1', N'NH_25_26'),
(N'HK014', N'Hocky_2', N'NH_25_26'),
(N'HK015', N'Hocky_3', N'NH_25_26'),

(N'HK016', N'Hocky_1', N'NH_26_27'),
(N'HK017', N'Hocky_2', N'NH_26_27'),
(N'HK018', N'Hocky_3', N'NH_26_27');

select * from LopHocPhan
INSERT INTO dbo.LopHocPhan 
(MaLHP, TenLopHocPhan, CaHoc, Thu, PhongHoc, ThoiGianBD, ThoiGianKT, SoLuongToiDa, TrangThai, MaMon, MaGV, MaHKNH)
VALUES
(N'26BNK0101', N'Tín dụng ngân hàng', 1, N'Thứ 2', N'P.101', '2026-08-15', '2026-12-15', 50, N'DangMo', N'BNK01', N'GV20260001', N'HK007'),
(N'26FIN00101', N'Tài chính doanh nghiệp', 2, N'Thứ 3', N'P.102', '2026-08-15', '2026-12-15', 70, N'DangMo', N'FIN01', N'GV20260002', N'HK007'),

(N'26ACC0101', N'Nguyên lý kế toán', 3, N'Thứ 4', N'P.103', '2026-08-15', '2026-12-15', 80, N'DangMo', N'ACC01', N'GV20260006', N'HK007'),

(N'26ACC0201', N'Quản trị học', 4, N'Thứ 5', N'P.104', '2026-08-15', '2026-12-15', 50, N'DangMo', N'ACC02', N'GV20260006', N'HK007'),

(N'26FBA0101', N'Marketing căn bản', 1, N'Thứ 6', N'P.201', '2026-08-15', '2026-12-15', 50, N'DangMo', N'FBA01', N'GV20260003', N'HK007');
