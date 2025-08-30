# 🔧 Khắc phục lỗi kết nối Database Railway

## ❌ Các lỗi thường gặp:

### 1. **Lỗi Connection String**
- **Vấn đề**: Connection string không đúng format hoặc thiếu thông tin
- **Giải pháp**: Sử dụng connection string từ Railway Dashboard

### 2. **Lỗi Environment Variables**
- **Vấn đề**: Environment variables không được set đúng
- **Giải pháp**: Cấu hình đúng environment variables

### 3. **Lỗi Database chưa được tạo**
- **Vấn đề**: Database service chưa được tạo trên Railway
- **Giải pháp**: Tạo MySQL database service

### 4. **Lỗi Network/SSL**
- **Vấn đề**: Kết nối mạng hoặc SSL issues
- **Giải pháp**: Sử dụng connection string nội bộ của Railway

## 🚀 Cách khắc phục:

### Bước 1: Kiểm tra Database Service trên Railway

1. **Đăng nhập Railway Dashboard**
2. **Vào project của bạn**
3. **Kiểm tra có MySQL service chưa:**
   - Nếu chưa có: Click "New" → "Database" → "MySQL"
   - Nếu đã có: Click vào MySQL service

### Bước 2: Lấy Connection String đúng

1. **Trong MySQL service, click "Connect"**
2. **Copy connection string có dạng:**
   ```
   Server=mysql.railway.internal;Port=3306;Database=railway;User=root;Password=your-password;CharSet=utf8mb4;
   ```

### Bước 3: Cấu hình Environment Variables

Trong Railway Dashboard → Variables, thêm:

```env
DB_HOST=mysql.railway.internal
DB_NAME=railway
DB_USER=root
DB_PASSWORD=your-actual-password
DB_PORT=3306
PORT=8080
JWT_SECRET=YourSuperSecretKeyHere123456789012345678901234567890
ASPNETCORE_ENVIRONMENT=Production
```

### Bước 4: Cập nhật appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": ""
  },
  "Jwt": {
    "Secret": "YourSuperSecretKeyHere123456789012345678901234567890",
    "Issuer": "OkeanAnimeMovie",
    "Audience": "OkeanAnimeMovieUsers",
    "ExpiryInDays": 7
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore.Database.Command": "Information"
    }
  },
  "AllowedHosts": "*"
}
```

### Bước 5: Kiểm tra Program.cs

Đảm bảo `Program.cs` có logic fallback:

```csharp
// Add Entity Framework
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    // Fallback for Railway environment variables
    var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
    var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "Okean_AnimeMovie";
    var dbUser = Environment.GetEnvironmentVariable("DB_USER") ?? "root";
    var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "123456";
    var dbPort = Environment.GetEnvironmentVariable("DB_PORT") ?? "3306";
    
    connectionString = $"Server={dbHost};Database={dbName};User={dbUser};Password={dbPassword};Port={dbPort};CharSet=utf8mb4;";
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, 
        ServerVersion.AutoDetect(connectionString)));
```

## 🔍 Debugging:

### 1. Kiểm tra Railway Logs
```bash
# Xem logs
railway logs

# Xem logs của service cụ thể
railway logs --service your-service-name
```

### 2. Test Database Connection
```bash
# Kết nối vào Railway container
railway shell

# Test connection
railway run dotnet ef database update
```

### 3. Kiểm tra Environment Variables
```bash
# Xem tất cả variables
railway variables

# Test connection string
railway run echo $DB_HOST
railway run echo $DB_PASSWORD
```

## 🚨 Các lỗi cụ thể và cách khắc phục:

### Lỗi: "Unable to connect to any of the specified MySQL hosts"
**Nguyên nhân**: DB_HOST sai hoặc database service chưa chạy
**Giải pháp**: 
- Kiểm tra DB_HOST = `mysql.railway.internal`
- Đảm bảo MySQL service đang chạy

### Lỗi: "Access denied for user"
**Nguyên nhân**: Username/password sai
**Giải pháp**: 
- Copy chính xác credentials từ Railway Dashboard
- Kiểm tra DB_USER và DB_PASSWORD

### Lỗi: "Unknown database"
**Nguyên nhân**: Database chưa được tạo
**Giải pháp**: 
- Railway tự động tạo database khi deploy
- Hoặc chạy migration: `railway run dotnet ef database update`

### Lỗi: "Connection timeout"
**Nguyên nhân**: Network issues
**Giải pháp**: 
- Sử dụng `mysql.railway.internal` thay vì external host
- Kiểm tra service dependencies

## 📋 Checklist khắc phục:

- [ ] MySQL service đã được tạo trên Railway
- [ ] Environment variables được set đúng
- [ ] Connection string sử dụng `mysql.railway.internal`
- [ ] Database credentials chính xác
- [ ] Migration đã chạy thành công
- [ ] Application logs không có lỗi database

## 🎯 Test Connection:

Sau khi cấu hình, test bằng cách:

```bash
# Deploy lại
railway up

# Kiểm tra logs
railway logs

# Test database
railway run dotnet ef database update
```

## 📞 Nếu vẫn không được:

1. **Kiểm tra Railway Status**: https://status.railway.app/
2. **Xem Documentation**: https://docs.railway.app/
3. **Railway Discord**: https://discord.gg/railway
4. **Tạo Support Ticket**: Trong Railway Dashboard

---

**💡 Tip**: Luôn sử dụng `mysql.railway.internal` thay vì external host để có kết nối ổn định nhất!
