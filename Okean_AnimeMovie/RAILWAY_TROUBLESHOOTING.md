# Railway Deployment Troubleshooting

## Lỗi thường gặp và cách khắc phục

### 1. Build Failures

#### Lỗi: "Build failed - Docker build error"
**Nguyên nhân:**
- Dockerfile syntax lỗi
- Thiếu dependencies
- Path không đúng

**Cách khắc phục:**
```bash
# Kiểm tra Dockerfile
docker build -t test-image .

# Kiểm tra cấu trúc project
ls -la
```

#### Lỗi: "Package restore failed"
**Nguyên nhân:**
- NuGet packages không tương thích
- Network issues

**Cách khắc phục:**
```bash
# Clear NuGet cache
dotnet nuget locals all --clear

# Restore packages
dotnet restore --force
```

### 2. Runtime Errors

#### Lỗi: "Application failed to start"
**Nguyên nhân:**
- Port configuration sai
- Environment variables thiếu

**Cách khắc phục:**
- Kiểm tra `PORT` environment variable
- Đảm bảo app lắng nghe trên `0.0.0.0:PORT`

#### Lỗi: "Database connection failed"
**Nguyên nhân:**
- Connection string sai
- Database chưa được tạo
- Network connectivity issues

**Cách khắc phục:**
```bash
# Kiểm tra environment variables
railway variables

# Test database connection
railway run dotnet ef database update
```

### 3. Migration Issues

#### Lỗi: "Migration failed"
**Nguyên nhân:**
- Database schema không tương thích
- Migration conflicts

**Cách khắc phục:**
```bash
# Reset database (CẨN THẬN - sẽ mất dữ liệu)
railway run dotnet ef database drop --force
railway run dotnet ef database update

# Hoặc tạo migration mới
railway run dotnet ef migrations add FixMigration
railway run dotnet ef database update
```

### 4. Environment Variables Issues

#### Lỗi: "Configuration error"
**Nguyên nhân:**
- Environment variables thiếu hoặc sai
- JWT secret không được set

**Cách khắc phục:**
```bash
# Kiểm tra variables
railway variables

# Set variables
railway variables set DB_HOST=your-host
railway variables set DB_PASSWORD=your-password
railway variables set JWT_SECRET=your-secret
```

### 5. Port Configuration Issues

#### Lỗi: "Port already in use"
**Nguyên nhân:**
- Port 8080 bị conflict
- Multiple services trên cùng port

**Cách khắc phục:**
```bash
# Set custom port
railway variables set PORT=3000

# Hoặc để Railway tự động assign
railway variables unset PORT
```

### 6. Memory/Resource Issues

#### Lỗi: "Out of memory"
**Nguyên nhân:**
- Application sử dụng quá nhiều RAM
- Free tier limitations

**Cách khắc phục:**
- Upgrade to paid plan
- Optimize application memory usage
- Reduce concurrent connections

### 7. SSL/HTTPS Issues

#### Lỗi: "SSL certificate error"
**Nguyên nhân:**
- Railway tự động cung cấp SSL
- Custom domain configuration

**Cách khắc phục:**
- Đảm bảo custom domain được cấu hình đúng
- Kiểm tra DNS settings
- Wait for SSL certificate propagation

### 8. Logs và Debugging

#### Xem logs:
```bash
# Railway CLI
railway logs

# Hoặc từ dashboard
# Deployments → View Logs
```

#### Debug locally:
```bash
# Test với Railway environment
railway run dotnet run

# Test Docker build
docker build -t test .
docker run -p 8080:8080 test
```

### 9. Common Fixes

#### Reset deployment:
```bash
# Redeploy
railway up

# Hoặc force rebuild
railway up --detach
```

#### Clear cache:
```bash
# Clear build cache
railway up --clear-cache
```

#### Check service status:
```bash
# Service status
railway status

# Service logs
railway logs --service your-service-name
```

### 10. Performance Issues

#### Slow startup:
- Optimize Dockerfile
- Reduce image size
- Use multi-stage builds

#### High memory usage:
- Monitor with Railway metrics
- Optimize database queries
- Implement caching

### 11. Database Issues

#### Connection timeout:
```bash
# Test connection
railway run mysql -h $DB_HOST -u $DB_USER -p$DB_PASSWORD -e "SELECT 1;"
```

#### Migration conflicts:
```bash
# Reset migrations
railway run dotnet ef migrations remove
railway run dotnet ef migrations add InitialCreate
railway run dotnet ef database update
```

### 12. Network Issues

#### External API calls:
- Use Railway's internal networking
- Configure CORS properly
- Handle timeouts

#### File uploads:
- Use Railway's storage service
- Configure proper file size limits
- Handle temporary files

## Best Practices

### 1. Environment Variables
- Never commit secrets to code
- Use Railway's secret management
- Validate all required variables

### 2. Database
- Use connection pooling
- Implement proper error handling
- Monitor query performance

### 3. Logging
- Use structured logging
- Log errors with context
- Monitor application health

### 4. Security
- Validate all inputs
- Use HTTPS in production
- Implement proper authentication

### 5. Performance
- Optimize Docker images
- Use caching strategies
- Monitor resource usage

## Getting Help

### Railway Support:
- Documentation: https://docs.railway.app/
- Discord: https://discord.gg/railway
- GitHub Issues: https://github.com/railwayapp/railway

### .NET Specific:
- ASP.NET Core Documentation
- Entity Framework Documentation
- Docker .NET Documentation

### Debug Commands:
```bash
# Full diagnostic
railway diagnose

# Service information
railway service

# Environment check
railway run env

# Health check
railway run curl http://localhost:$PORT/health
```
