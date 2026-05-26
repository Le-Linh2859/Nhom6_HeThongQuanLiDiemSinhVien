# Database Migration – Hướng dẫn sử dụng

## Tổng quan

Thư mục `Database/Migrations` chứa các script SQL được đánh số phiên bản theo format:

```
V{số thứ tự}__{mô tả}.sql
```

Các script được thiết kế **idempotent** (chạy lại nhiều lần không bị lỗi) nhờ kiểm tra `IF NOT EXISTS`.

## Danh sách Migration

| Version | File | Mô tả |
|---------|------|-------|
| V001 | `V001__InitialSchema.sql` | Tạo bảng VaiTro, TaiKhoan + seed 3 vai trò |
| V002 | `V002__AddMigrationTracking.sql` | Tạo bảng DbMigrationHistory để theo dõi lịch sử |

## Cách chạy

### Cách 1: Dùng script `migrate.bat`

```cmd
cd Database
migrate.bat ADMIN-PC\QUYNHANH
```

### Cách 2: Chạy thủ công từng file trong SSMS

1. Mở SQL Server Management Studio (SSMS)
2. Kết nối đến server `ADMIN-PC\QUYNHANH`
3. Chọn database `DB_QLDiemSinhVien`
4. Mở từng file `.sql` theo thứ tự V001 → V002 → ...
5. Chạy (F5)

## Quy tắc thêm migration mới

1. Tạo file mới: `V{số tiếp theo}__{mô tả}.sql`
2. Luôn dùng `IF NOT EXISTS` để đảm bảo idempotent
3. Thêm `INSERT INTO DbMigrationHistory` ở cuối script
4. Cập nhật `migrate.bat` để include file mới
5. Cập nhật bảng danh sách migration trong file README này

## Lưu ý

- Các script sử dụng **Windows Authentication** (`-E`), không cần nhập user/password
- Không bao giờ sửa migration đã chạy trên production – tạo migration mới để thay đổi
