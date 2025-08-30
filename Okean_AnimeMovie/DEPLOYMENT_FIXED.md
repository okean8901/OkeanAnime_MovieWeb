# 🚀 Railway Deployment - Đã Sửa Lỗi

## ✅ Các lỗi đã được sửa:

### 1. **Lỗi .NET Version Mismatch**
- **Vấn đề**: Railway sử dụng .NET 6.0 nhưng project target .NET 8.0
- **Giải pháp**: Downgrade project xuống .NET 6.0
- **Thay đổi**: 
  - `TargetFramework`: `net8.0` → `net6.0`
  - Package versions: Cập nhật tất cả packages cho .NET 6.0
  - Dockerfile: Sử dụng `mcr.microsoft.com/dotnet/aspnet:6.0`

### 2. **Lỗi JWT Secret Null**
- **Vấn đề**: JWT secret có thể null trong production
- **Giải pháp**: Thêm fallback values và environment variable support
- **Thay đổi**: 
  - `Program.cs`: Thêm fallback cho JWT secret
  - `appsettings.Production.json`: Cấu hình mặc định

### 3. **Lỗi Database Initialization Crash**
- **Vấn đề**: DbInitializer crash khi không thể kết nối database
- **Giải pháp**: Thêm error handling và connection test
- **Thay đổi**:
  - `DbInitializer.cs`: Thêm try-catch và connection test
  - `Program.cs`: Wrap DbInitializer trong try-catch

### 4. **Lỗi Entity Properties**
- **Vấn đề**: Sử dụng properties không tồn tại trong entity
- **Giải pháp**: Sử dụng đúng properties của entity
- **Thay đổi**:
  - `ImageUrl` → `Poster`
  - `Views` → `ViewCount`
  - Loại bỏ các properties không tồn tại

## 🚀 Hướng dẫn Deploy:

### Bước 1: Chuẩn bị
```bash
# Chạy script setup
.\setup_railway.ps1
```

### Bước 2: Push code
```bash
git add .
git commit -m "Fix Railway deployment issues - .NET 6.0 compatibility"
git push origin main
```

### Bước 3: Tạo project Railway
1. Đăng nhập: https://railway.app/
2. Click "New Project"
3. Chọn "Deploy from GitHub repo"
4. Chọn repository của bạn

### Bước 4: Thêm MySQL Database
1. Trong project, click "New"
2. Chọn "Database" → "MySQL"
3. Railway sẽ tự động tạo database

### Bước 5: Cấu hình Environment Variables
Trong Railway Dashboard → Variables, thêm:

```env
DB_HOST=your-mysql-host.railway.app
DB_NAME=railway
DB_USER=root
DB_PASSWORD=your-mysql-password
DB_PORT=3306
PORT=8080
JWT_SECRET=YourSuperSecretKeyHere123456789012345678901234567890
ASPNETCORE_ENVIRONMENT=Production
```

### Bước 6: Chạy Migration
```bash
# Cài đặt Railway CLI
npm install -g @railway/cli

# Login
railway login

# Chạy migration
railway run dotnet ef database update
```

## 🔧 Troubleshooting:

### Nếu vẫn gặp lỗi build:
1. Kiểm tra logs trong Railway Dashboard
2. Đảm bảo tất cả environment variables được set
3. Kiểm tra database connection

### Nếu gặp lỗi runtime:
1. Kiểm tra application logs
2. Đảm bảo PORT environment variable được set
3. Kiểm tra database connection

### Nếu gặp lỗi migration:
```bash
railway run dotnet ef database update
```

## 📋 Checklist trước khi deploy:

- [ ] Project build thành công với .NET 6.0
- [ ] Dockerfile sử dụng .NET 6.0 images
- [ ] Tất cả environment variables được cấu hình
- [ ] Database connection string đúng
- [ ] JWT secret được set
- [ ] Migration chạy thành công

## 🎯 Kết quả mong đợi:

Sau khi deploy thành công:
- ✅ Application chạy trên Railway
- ✅ Database được tạo và seed data
- ✅ Admin user được tạo (admin@okeananime.com / Admin123!)
- ✅ Sample anime và episodes được thêm
- ✅ Genres được tạo

## 📞 Support:

Nếu vẫn gặp vấn đề:
1. Xem file `RAILWAY_TROUBLESHOOTING.md`
2. Kiểm tra Railway logs
3. Railway Discord: https://discord.gg/railway

---

**🎉 Chúc bạn deploy thành công!**
