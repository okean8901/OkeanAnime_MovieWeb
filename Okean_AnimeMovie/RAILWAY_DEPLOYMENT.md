# Hướng dẫn Deploy Okean Anime Movie lên Railway

## 1. Chuẩn bị

### 1.1. Tài khoản Railway
- Đăng ký tài khoản tại: https://railway.app/
- Kết nối với GitHub để deploy từ repository

### 1.2. Repository GitHub
- Đảm bảo code đã được push lên GitHub
- Repository phải có các file sau:
  - `Dockerfile`
  - `railway.json`
  - `.dockerignore`

## 2. Tạo Project trên Railway

### 2.1. Tạo Project mới
1. Đăng nhập vào Railway Dashboard
2. Click "New Project"
3. Chọn "Deploy from GitHub repo"
4. Chọn repository của bạn

### 2.2. Cấu hình Database
1. Trong project, click "New"
2. Chọn "Database" → "MySQL"
3. Railway sẽ tự động tạo MySQL database
4. Ghi nhớ thông tin kết nối database

## 3. Cấu hình Environment Variables

### 3.1. Database Variables
Trong Railway Dashboard, vào tab "Variables" và thêm:

```env
DB_HOST=your-mysql-host.railway.app
DB_NAME=railway
DB_USER=root
DB_PASSWORD=your-mysql-password
DB_PORT=3306
```

### 3.2. JWT Variables (Tùy chọn)
```env
JWT_SECRET=YourSuperSecretKeyHere123456789012345678901234567890
JWT_ISSUER=OkeanAnimeMovie
JWT_AUDIENCE=OkeanAnimeMovieUsers
JWT_EXPIRY_IN_DAYS=7
```

### 3.3. ASP.NET Core Variables
```env
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://0.0.0.0:8080
```

## 4. Deploy Application

### 4.1. Tự động Deploy
- Railway sẽ tự động deploy khi bạn push code lên GitHub
- Hoặc có thể trigger manual deploy từ Railway Dashboard

### 4.2. Kiểm tra Build Logs
- Vào tab "Deployments" để xem build logs
- Đảm bảo build thành công

## 5. Cấu hình Domain (Tùy chọn)

### 5.1. Custom Domain
1. Vào tab "Settings"
2. Click "Generate Domain" hoặc thêm custom domain
3. Cấu hình DNS nếu cần

### 5.2. HTTPS
- Railway tự động cung cấp SSL certificate
- Không cần cấu hình thêm

## 6. Database Migration

### 6.1. Chạy Migration
Sau khi deploy thành công, chạy migration:

```bash
# Kết nối vào Railway container
railway shell

# Chạy migration
dotnet ef database update
```

### 6.2. Hoặc sử dụng Railway CLI
```bash
# Cài đặt Railway CLI
npm install -g @railway/cli

# Login
railway login

# Chạy migration
railway run dotnet ef database update
```

## 7. Monitoring và Logs

### 7.1. Application Logs
- Vào tab "Deployments" → chọn deployment → "View Logs"
- Hoặc tab "Logs" để xem real-time logs

### 7.2. Database Monitoring
- Vào MySQL service → "Connect" để xem database
- Có thể sử dụng MySQL Workbench với connection string từ Railway

## 8. Troubleshooting

### 8.1. Build Failures
- Kiểm tra Dockerfile syntax
- Đảm bảo tất cả dependencies được include
- Xem build logs để debug

### 8.2. Runtime Errors
- Kiểm tra application logs
- Đảm bảo environment variables được set đúng
- Kiểm tra database connection

### 8.3. Database Connection Issues
- Kiểm tra database credentials
- Đảm bảo database service đang chạy
- Kiểm tra network connectivity

## 9. Scaling và Performance

### 9.1. Auto-scaling
- Railway tự động scale dựa trên traffic
- Có thể cấu hình manual scaling trong settings

### 9.2. Resource Limits
- Free tier có giới hạn về CPU và RAM
- Upgrade plan để có nhiều resources hơn

## 10. Backup và Recovery

### 10.1. Database Backup
- Railway tự động backup MySQL database
- Có thể download backup từ dashboard

### 10.2. Code Backup
- Code được backup trên GitHub
- Có thể rollback về version cũ từ Railway

## 11. Cost Optimization

### 11.1. Free Tier
- 500 hours/month cho free tier
- 1GB RAM, 0.5 vCPU
- 1GB storage

### 11.2. Paid Plans
- $5/month cho Developer plan
- $20/month cho Pro plan
- Custom pricing cho enterprise

## 12. Security Best Practices

### 12.1. Environment Variables
- Không commit sensitive data vào code
- Sử dụng Railway secrets cho sensitive data
- Rotate secrets regularly

### 12.2. Database Security
- Sử dụng strong passwords
- Enable SSL cho database connections
- Regular security updates

## 13. Continuous Deployment

### 13.1. GitHub Integration
- Railway tự động deploy khi push to main branch
- Có thể cấu hình branch-specific deployments

### 13.2. Preview Deployments
- Tạo preview deployments cho pull requests
- Test changes trước khi merge

## 14. Support và Documentation

### 14.1. Railway Documentation
- https://docs.railway.app/
- Community Discord: https://discord.gg/railway

### 14.2. .NET on Railway
- https://docs.railway.app/deploy/deployments/dockerfile
- https://docs.railway.app/deploy/deployments/dockerfile#dotnet
