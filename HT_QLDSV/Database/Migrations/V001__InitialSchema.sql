-- ============================================================================
-- Migration V001: Initial Schema – Bảng VaiTro và TaiKhoan
-- Ngày tạo: 2026-05-26
-- Mô tả: Tài liệu hoá và tạo schema ban đầu cho module đăng nhập
-- ============================================================================

-- Bảng VaiTro: Lưu các vai trò trong hệ thống
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'VaiTro')
BEGIN
    CREATE TABLE VaiTro (
        MaVaiTro NVARCHAR(10) PRIMARY KEY,
        TenVaiTro NVARCHAR(50) NOT NULL
    );

    -- Seed data: 3 vai trò mặc định
    INSERT INTO VaiTro (MaVaiTro, TenVaiTro) VALUES ('VT001', N'Admin');
    INSERT INTO VaiTro (MaVaiTro, TenVaiTro) VALUES ('VT002', N'Giảng viên');
    INSERT INTO VaiTro (MaVaiTro, TenVaiTro) VALUES ('VT003', N'Sinh viên');

    PRINT N'✅ Tạo bảng VaiTro thành công.';
END
ELSE
BEGIN
    PRINT N'⏭️ Bảng VaiTro đã tồn tại, bỏ qua.';
END
GO

-- Bảng TaiKhoan: Lưu thông tin tài khoản đăng nhập
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'TaiKhoan')
BEGIN
    CREATE TABLE TaiKhoan (
        MaTaiKhoan NVARCHAR(20) PRIMARY KEY,
        TenDangNhap NVARCHAR(50) NOT NULL UNIQUE,
        MatKhau NVARCHAR(255) NOT NULL,
        MaVaiTro NVARCHAR(10) NOT NULL,
        CONSTRAINT FK_TaiKhoan_VaiTro FOREIGN KEY (MaVaiTro) REFERENCES VaiTro(MaVaiTro)
    );

    PRINT N'✅ Tạo bảng TaiKhoan thành công.';
END
ELSE
BEGIN
    PRINT N'⏭️ Bảng TaiKhoan đã tồn tại, bỏ qua.';
END
GO
