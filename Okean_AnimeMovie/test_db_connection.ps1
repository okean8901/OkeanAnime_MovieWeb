# Script test database connection cho Railway
# Chạy script này để kiểm tra kết nối database

Write-Host "=== Test Database Connection ===" -ForegroundColor Green
Write-Host ""

# Kiểm tra Railway CLI
Write-Host "Kiểm tra Railway CLI..." -ForegroundColor Yellow
try {
    $railwayVersion = railway --version
    Write-Host "✓ Railway CLI đã cài đặt: $railwayVersion" -ForegroundColor Green
} catch {
    Write-Host "✗ Railway CLI chưa cài đặt" -ForegroundColor Red
    Write-Host "Cài đặt: npm install -g @railway/cli" -ForegroundColor Cyan
    exit 1
}

# Kiểm tra login
Write-Host ""
Write-Host "Kiểm tra Railway login..." -ForegroundColor Yellow
try {
    $loginStatus = railway whoami
    Write-Host "✓ Đã đăng nhập: $loginStatus" -ForegroundColor Green
} catch {
    Write-Host "✗ Chưa đăng nhập Railway" -ForegroundColor Red
    Write-Host "Đăng nhập: railway login" -ForegroundColor Cyan
    exit 1
}

# Kiểm tra environment variables
Write-Host ""
Write-Host "Kiểm tra Environment Variables..." -ForegroundColor Yellow
try {
    $dbHost = railway variables get DB_HOST
    $dbName = railway variables get DB_NAME
    $dbUser = railway variables get DB_USER
    $dbPort = railway variables get DB_PORT
    
    Write-Host "✓ DB_HOST: $dbHost" -ForegroundColor Green
    Write-Host "✓ DB_NAME: $dbName" -ForegroundColor Green
    Write-Host "✓ DB_USER: $dbUser" -ForegroundColor Green
    Write-Host "✓ DB_PORT: $dbPort" -ForegroundColor Green
    
    # Kiểm tra password (không hiển thị)
    $dbPassword = railway variables get DB_PASSWORD
    if ($dbPassword) {
        Write-Host "✓ DB_PASSWORD: [HIDDEN]" -ForegroundColor Green
    } else {
        Write-Host "✗ DB_PASSWORD: Chưa set" -ForegroundColor Red
    }
} catch {
    Write-Host "✗ Lỗi khi lấy environment variables" -ForegroundColor Red
    Write-Host "Kiểm tra: railway variables" -ForegroundColor Cyan
}

# Test database connection
Write-Host ""
Write-Host "Test Database Connection..." -ForegroundColor Yellow
try {
    $testResult = railway run dotnet ef database update --dry-run
    if ($LASTEXITCODE -eq 0) {
        Write-Host "✓ Database connection thành công!" -ForegroundColor Green
    } else {
        Write-Host "✗ Database connection thất bại!" -ForegroundColor Red
        Write-Host "Kiểm tra logs: railway logs" -ForegroundColor Cyan
    }
} catch {
    Write-Host "✗ Lỗi khi test database connection" -ForegroundColor Red
    Write-Host "Lỗi: $($_.Exception.Message)" -ForegroundColor Red
}

# Kiểm tra service status
Write-Host ""
Write-Host "Kiểm tra Service Status..." -ForegroundColor Yellow
try {
    $services = railway service list
    Write-Host "Services:" -ForegroundColor Cyan
    Write-Host $services -ForegroundColor White
} catch {
    Write-Host "✗ Lỗi khi lấy service status" -ForegroundColor Red
}

Write-Host ""
Write-Host "=== Test hoàn tất! ===" -ForegroundColor Green
Write-Host ""
Write-Host "Nếu có lỗi, hãy kiểm tra:" -ForegroundColor Yellow
Write-Host "1. Railway Dashboard → Variables" -ForegroundColor Cyan
Write-Host "2. MySQL service đã được tạo chưa" -ForegroundColor Cyan
Write-Host "3. Connection string từ MySQL service" -ForegroundColor Cyan
Write-Host "4. Logs: railway logs" -ForegroundColor Cyan
