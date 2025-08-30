# Script setup Railway cho Okean Anime Movie
# Chạy script này để chuẩn bị deploy lên Railway

Write-Host "=== Setup Railway Deployment ===" -ForegroundColor Green
Write-Host ""

# Kiểm tra Git status
Write-Host "Kiểm tra Git status..." -ForegroundColor Yellow
try {
    $gitStatus = git status --porcelain
    if ($gitStatus) {
        Write-Host "Có thay đổi chưa commit:" -ForegroundColor Yellow
        Write-Host $gitStatus -ForegroundColor Cyan
        Write-Host ""
        $commit = Read-Host "Bạn có muốn commit các thay đổi này? (y/n)"
        if ($commit -eq "y" -or $commit -eq "Y") {
            $commitMessage = Read-Host "Nhập commit message"
            git add .
            git commit -m $commitMessage
            Write-Host "Đã commit thành công!" -ForegroundColor Green
        }
    } else {
        Write-Host "Không có thay đổi nào cần commit." -ForegroundColor Green
    }
} catch {
    Write-Host "Lỗi kiểm tra Git: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

# Xóa migrations cũ
Write-Host ""
Write-Host "Xóa migrations cũ..." -ForegroundColor Yellow
try {
    dotnet ef migrations remove --force
    Write-Host "Đã xóa migrations cũ!" -ForegroundColor Green
} catch {
    Write-Host "Không có migrations để xóa hoặc lỗi: $($_.Exception.Message)" -ForegroundColor Yellow
}

# Tạo migration mới
Write-Host ""
Write-Host "Tạo migration mới..." -ForegroundColor Yellow
try {
    dotnet ef migrations add InitialCreate
    Write-Host "Đã tạo migration mới!" -ForegroundColor Green
} catch {
    Write-Host "Lỗi tạo migration: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

# Kiểm tra các file cần thiết
Write-Host ""
Write-Host "Kiểm tra các file cần thiết..." -ForegroundColor Yellow

$requiredFiles = @(
    "Dockerfile",
    "railway.json",
    ".dockerignore",
    "appsettings.Production.json"
)

foreach ($file in $requiredFiles) {
    if (Test-Path $file) {
        Write-Host "✓ $file" -ForegroundColor Green
    } else {
        Write-Host "✗ $file (thiếu)" -ForegroundColor Red
    }
}

# Build project
Write-Host ""
Write-Host "Build project..." -ForegroundColor Yellow
try {
    dotnet build --configuration Release
    if ($LASTEXITCODE -eq 0) {
        Write-Host "Build thành công!" -ForegroundColor Green
    } else {
        Write-Host "Build thất bại!" -ForegroundColor Red
        exit 1
    }
} catch {
    Write-Host "Lỗi build: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

# Test Docker build
Write-Host ""
Write-Host "Test Docker build..." -ForegroundColor Yellow
try {
    docker build -t okean-anime-movie-test .
    if ($LASTEXITCODE -eq 0) {
        Write-Host "Docker build thành công!" -ForegroundColor Green
        # Clean up test image
        docker rmi okean-anime-movie-test
    } else {
        Write-Host "Docker build thất bại!" -ForegroundColor Red
        exit 1
    }
} catch {
    Write-Host "Lỗi Docker build: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

Write-Host ""
Write-Host "=== Setup hoàn tất! ===" -ForegroundColor Green
Write-Host ""
Write-Host "Bước tiếp theo:" -ForegroundColor Yellow
Write-Host "1. Push code lên GitHub:" -ForegroundColor Cyan
Write-Host "   git push origin main" -ForegroundColor White
Write-Host ""
Write-Host "2. Tạo project trên Railway:" -ForegroundColor Cyan
Write-Host "   - Đăng nhập: https://railway.app/" -ForegroundColor White
Write-Host "   - New Project → Deploy from GitHub repo" -ForegroundColor White
Write-Host "   - Chọn repository của bạn" -ForegroundColor White
Write-Host ""
Write-Host "3. Thêm MySQL Database:" -ForegroundColor Cyan
Write-Host "   - New → Database → MySQL" -ForegroundColor White
Write-Host ""
Write-Host "4. Cấu hình Environment Variables:" -ForegroundColor Cyan
Write-Host "   DB_HOST=your-mysql-host.railway.app" -ForegroundColor White
Write-Host "   DB_NAME=railway" -ForegroundColor White
Write-Host "   DB_USER=root" -ForegroundColor White
Write-Host "   DB_PASSWORD=your-mysql-password" -ForegroundColor White
Write-Host "   DB_PORT=3306" -ForegroundColor White
Write-Host "   JWT_SECRET=YourSuperSecretKeyHere123456789012345678901234567890" -ForegroundColor White
Write-Host ""
Write-Host "5. Chạy Migration:" -ForegroundColor Cyan
Write-Host "   railway run dotnet ef database update" -ForegroundColor White
Write-Host ""
Write-Host "Xem file RAILWAY_DEPLOYMENT.md để biết thêm chi tiết!" -ForegroundColor Green
