# Script khắc phục vấn đề kết nối MySQL Railway
# Chạy script này để sửa lỗi "trỏ đến localhost"

Write-Host "=== Khắc phục vấn đề MySQL Railway ===" -ForegroundColor Green
Write-Host ""

# Kiểm tra Railway CLI
Write-Host "Kiểm tra Railway CLI..." -ForegroundColor Yellow
try {
    $railwayVersion = railway --version
    Write-Host "✓ Railway CLI: $railwayVersion" -ForegroundColor Green
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

# Kiểm tra MySQL environment variables
Write-Host ""
Write-Host "Kiểm tra MySQL Environment Variables..." -ForegroundColor Yellow

$mysqlVars = @("MYSQLHOST", "MYSQLDATABASE", "MYSQLUSER", "MYSQLPASSWORD", "MYSQLPORT")
$allSet = $true

foreach ($var in $mysqlVars) {
    try {
        $value = railway variables get $var
        if ($value) {
            if ($var -eq "MYSQLPASSWORD") {
                Write-Host "✓ $var`: [HIDDEN]" -ForegroundColor Green
            } else {
                Write-Host "✓ $var`: $value" -ForegroundColor Green
            }
        } else {
            Write-Host "✗ $var`: Chưa set" -ForegroundColor Red
            $allSet = $false
        }
    } catch {
        Write-Host "✗ $var`: Lỗi khi lấy giá trị" -ForegroundColor Red
        $allSet = $false
    }
}

if (-not $allSet) {
    Write-Host ""
    Write-Host "⚠️  Một số MySQL variables chưa được set!" -ForegroundColor Yellow
    Write-Host "Hãy vào Railway Dashboard → MySQL Service → Variables" -ForegroundColor Cyan
    Write-Host "Và thêm các variables sau:" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "MYSQLHOST=mysql.railway.internal" -ForegroundColor White
    Write-Host "MYSQLDATABASE=railway" -ForegroundColor White
    Write-Host "MYSQLUSER=root" -ForegroundColor White
    Write-Host "MYSQLPASSWORD=your-actual-password" -ForegroundColor White
    Write-Host "MYSQLPORT=3306" -ForegroundColor White
    Write-Host ""
}

# Test database connection
Write-Host ""
Write-Host "Test Database Connection..." -ForegroundColor Yellow
try {
    Write-Host "Đang test kết nối MySQL..." -ForegroundColor Cyan
    
    # Test với EF Core
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

# Hướng dẫn khắc phục
Write-Host ""
Write-Host "=== Hướng dẫn khắc phục ===" -ForegroundColor Green
Write-Host ""

if (-not $allSet) {
    Write-Host "1. Vào Railway Dashboard" -ForegroundColor Yellow
    Write-Host "2. Chọn MySQL service" -ForegroundColor Yellow
    Write-Host "3. Vào tab Variables" -ForegroundColor Yellow
    Write-Host "4. Thêm các MySQL environment variables" -ForegroundColor Yellow
    Write-Host "5. Restart MySQL service" -ForegroundColor Yellow
    Write-Host "6. Deploy lại application" -ForegroundColor Yellow
} else {
    Write-Host "1. Restart MySQL service:" -ForegroundColor Yellow
    Write-Host "   railway service restart your-mysql-service-name" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "2. Deploy lại application:" -ForegroundColor Yellow
    Write-Host "   railway up" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "3. Chạy migration:" -ForegroundColor Yellow
    Write-Host "   railway run dotnet ef database update" -ForegroundColor Cyan
}

Write-Host ""
Write-Host "=== Kiểm tra logs ===" -ForegroundColor Green
Write-Host "Để xem logs chi tiết:" -ForegroundColor Yellow
Write-Host "railway logs --service your-mysql-service-name" -ForegroundColor Cyan
Write-Host "railway logs --service your-app-service-name" -ForegroundColor Cyan

Write-Host ""
Write-Host "=== Test hoàn tất! ===" -ForegroundColor Green
