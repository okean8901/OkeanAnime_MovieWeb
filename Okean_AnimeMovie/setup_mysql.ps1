# Script tự động cài đặt MySQL cho Okean Anime Movie
# Chạy script này với quyền Administrator

Write-Host "=== Okean Anime Movie - MySQL Setup ===" -ForegroundColor Green
Write-Host ""

# Kiểm tra MySQL đã được cài đặt chưa
Write-Host "Kiểm tra MySQL..." -ForegroundColor Yellow
try {
    $mysqlVersion = mysql --version 2>$null
    if ($mysqlVersion) {
        Write-Host "MySQL đã được cài đặt: $mysqlVersion" -ForegroundColor Green
    } else {
        Write-Host "MySQL chưa được cài đặt. Vui lòng cài đặt MySQL Server trước." -ForegroundColor Red
        Write-Host "Tải MySQL từ: https://dev.mysql.com/downloads/mysql/" -ForegroundColor Cyan
        exit 1
    }
} catch {
    Write-Host "MySQL chưa được cài đặt hoặc không có trong PATH." -ForegroundColor Red
    Write-Host "Vui lòng cài đặt MySQL Server và thêm vào PATH." -ForegroundColor Cyan
    exit 1
}

# Kiểm tra kết nối MySQL
Write-Host ""
Write-Host "Kiểm tra kết nối MySQL..." -ForegroundColor Yellow
try {
    $testConnection = mysql -u root -p123456 -e "SELECT 1;" 2>$null
    if ($LASTEXITCODE -eq 0) {
        Write-Host "Kết nối MySQL thành công!" -ForegroundColor Green
    } else {
        Write-Host "Không thể kết nối MySQL với mật khẩu 123456" -ForegroundColor Red
        Write-Host "Vui lòng kiểm tra mật khẩu MySQL root" -ForegroundColor Cyan
        exit 1
    }
} catch {
    Write-Host "Lỗi kết nối MySQL: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

# Tạo database
Write-Host ""
Write-Host "Tạo database Okean_AnimeMovie..." -ForegroundColor Yellow
try {
    $createDb = mysql -u root -p123456 -e "CREATE DATABASE IF NOT EXISTS \`Okean_AnimeMovie\` CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;"
    if ($LASTEXITCODE -eq 0) {
        Write-Host "Database Okean_AnimeMovie đã được tạo thành công!" -ForegroundColor Green
    } else {
        Write-Host "Lỗi tạo database" -ForegroundColor Red
        exit 1
    }
} catch {
    Write-Host "Lỗi tạo database: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

# Kiểm tra database đã tồn tại
Write-Host ""
Write-Host "Kiểm tra database..." -ForegroundColor Yellow
try {
    $checkDb = mysql -u root -p123456 -e "USE \`Okean_AnimeMovie\`; SHOW TABLES;"
    if ($LASTEXITCODE -eq 0) {
        Write-Host "Database Okean_AnimeMovie đã sẵn sàng!" -ForegroundColor Green
    } else {
        Write-Host "Lỗi kiểm tra database" -ForegroundColor Red
        exit 1
    }
} catch {
    Write-Host "Lỗi kiểm tra database: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

# Restore NuGet packages
Write-Host ""
Write-Host "Restore NuGet packages..." -ForegroundColor Yellow
try {
    dotnet restore
    if ($LASTEXITCODE -eq 0) {
        Write-Host "NuGet packages đã được restore thành công!" -ForegroundColor Green
    } else {
        Write-Host "Lỗi restore NuGet packages" -ForegroundColor Red
        exit 1
    }
} catch {
    Write-Host "Lỗi restore packages: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

# Build project
Write-Host ""
Write-Host "Build project..." -ForegroundColor Yellow
try {
    dotnet build
    if ($LASTEXITCODE -eq 0) {
        Write-Host "Project đã được build thành công!" -ForegroundColor Green
    } else {
        Write-Host "Lỗi build project" -ForegroundColor Red
        exit 1
    }
} catch {
    Write-Host "Lỗi build project: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

# Cập nhật database
Write-Host ""
Write-Host "Cập nhật database với migrations..." -ForegroundColor Yellow
try {
    dotnet ef database update
    if ($LASTEXITCODE -eq 0) {
        Write-Host "Database đã được cập nhật thành công!" -ForegroundColor Green
    } else {
        Write-Host "Lỗi cập nhật database" -ForegroundColor Red
        exit 1
    }
} catch {
    Write-Host "Lỗi cập nhật database: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

Write-Host ""
Write-Host "=== Cài đặt hoàn tất! ===" -ForegroundColor Green
Write-Host "Bạn có thể chạy ứng dụng bằng lệnh: dotnet run" -ForegroundColor Cyan
Write-Host "Hoặc mở MySQL Workbench để quản lý database" -ForegroundColor Cyan
Write-Host ""
Write-Host "Thông tin kết nối:" -ForegroundColor Yellow
Write-Host "Server: localhost" -ForegroundColor White
Write-Host "Port: 3306" -ForegroundColor White
Write-Host "Database: Okean_AnimeMovie" -ForegroundColor White
Write-Host "User: root" -ForegroundColor White
Write-Host "Password: 123456" -ForegroundColor White
