-- ============================================================================
-- Migration V002: Thêm bảng theo dõi lịch sử migration
-- Ngày tạo: 2026-05-26
-- Mô tả: Tạo bảng DbMigrationHistory để tracking các migration đã chạy
-- ============================================================================

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'DbMigrationHistory')
BEGIN
    CREATE TABLE DbMigrationHistory (
        MigrationId NVARCHAR(100) PRIMARY KEY,
        AppliedOn DATETIME NOT NULL DEFAULT GETDATE(),
        Description NVARCHAR(500)
    );

    -- Ghi nhận migration V001 và V002 đã chạy
    INSERT INTO DbMigrationHistory (MigrationId, Description) 
    VALUES ('V001__InitialSchema', N'Tạo bảng VaiTro và TaiKhoan');
    
    INSERT INTO DbMigrationHistory (MigrationId, Description) 
    VALUES ('V002__AddMigrationTracking', N'Tạo bảng DbMigrationHistory');

    PRINT N'✅ Tạo bảng DbMigrationHistory thành công.';
END
ELSE
BEGIN
    PRINT N'⏭️ Bảng DbMigrationHistory đã tồn tại, bỏ qua.';
END
GO
