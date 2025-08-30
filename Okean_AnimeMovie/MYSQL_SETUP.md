# Hướng dẫn cài đặt MySQL cho Okean Anime Movie

## 1. Cài đặt MySQL Server

### Windows:
1. Tải MySQL Server từ: https://dev.mysql.com/downloads/mysql/
2. Chọn "MySQL Installer for Windows"
3. Chọn "Server only" hoặc "Full" installation
4. Trong quá trình cài đặt, đặt mật khẩu root là: `123456`

### Hoặc sử dụng XAMPP:
1. Tải XAMPP từ: https://www.apachefriends.org/
2. Cài đặt và khởi động MySQL service
3. Mật khẩu root mặc định là rỗng

## 2. Cài đặt MySQL Workbench (Tùy chọn)

1. Tải MySQL Workbench từ: https://dev.mysql.com/downloads/workbench/
2. Cài đặt và kết nối với MySQL Server

## 3. Tạo Database

### Cách 1: Sử dụng MySQL Workbench
1. Mở MySQL Workbench
2. Kết nối với MySQL Server (localhost:3306, user: root, password: 123456)
3. Chạy script trong file `create_database.sql`

### Cách 2: Sử dụng MySQL Command Line
```bash
mysql -u root -p
# Nhập mật khẩu: 123456
# Sau đó chạy nội dung file create_database.sql
```

### Cách 3: Sử dụng phpMyAdmin (nếu dùng XAMPP)
1. Mở http://localhost/phpmyadmin
2. Tạo database mới tên: `Okean_AnimeMovie`
3. Chọn collation: `utf8mb4_unicode_ci`

## 4. Cấu hình Connection String

Connection string đã được cấu hình trong `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=Okean_AnimeMovie;User=root;Password=123456;Port=3306;"
}
```

## 5. Chạy Migrations

```bash
# Tạo migration (đã thực hiện)
dotnet ef migrations add InitialCreate

# Cập nhật database
dotnet ef database update
```

## 6. Khởi chạy ứng dụng

```bash
dotnet run
```

## Lưu ý quan trọng:

1. **Mật khẩu**: Đảm bảo mật khẩu MySQL root là `123456` hoặc cập nhật connection string
2. **Port**: MySQL mặc định chạy trên port 3306
3. **Character Set**: Database sử dụng utf8mb4 để hỗ trợ emoji và ký tự đặc biệt
4. **Backup**: Nếu có dữ liệu cũ từ SQL Server, hãy export và import vào MySQL

## Troubleshooting:

### Lỗi kết nối:
- Kiểm tra MySQL service đã chạy chưa
- Kiểm tra port 3306 có bị block không
- Kiểm tra mật khẩu root

### Lỗi migration:
- Xóa tất cả migrations cũ: `dotnet ef migrations remove`
- Tạo migration mới: `dotnet ef migrations add InitialCreate`
- Cập nhật database: `dotnet ef database update`
