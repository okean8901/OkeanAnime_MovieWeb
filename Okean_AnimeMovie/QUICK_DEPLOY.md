# Quick Deploy Guide - Railway

## 🚀 Deploy nhanh lên Railway

### Bước 1: Chuẩn bị
```bash
# Chạy script setup
.\setup_railway.ps1
```

### Bước 2: Push code
```bash
git add .
git commit -m "Setup for Railway deployment with .NET 6.0"
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

### Bước 7: Kiểm tra
- Vào tab "Deployments" để xem build logs
- Click vào domain được tạo để truy cập ứng dụng

## 🔧 Troubleshooting

### Lỗi build:
- Kiểm tra logs trong Railway Dashboard
- Đảm bảo tất cả environment variables được set
- Kiểm tra database connection

### Lỗi migration:
```bash
railway run dotnet ef database update
```

### Lỗi runtime:
- Kiểm tra application logs
- Đảm bảo PORT environment variable được set

## 📞 Support
- Xem file `RAILWAY_TROUBLESHOOTING.md` để biết thêm chi tiết
- Railway Discord: https://discord.gg/railway
