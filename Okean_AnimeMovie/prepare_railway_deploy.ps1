# Script chuẩn bị deploy Okean Anime Movie lên Railway
# Chạy script này trước khi push code lên GitHub

Write-Host "=== Chuẩn bị Deploy Railway ===" -ForegroundColor Green
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

# Kiểm tra các file cần thiết cho Railway
Write-Host ""
Write-Host "Kiểm tra các file cần thiết..." -ForegroundColor Yellow

$requiredFiles = @(
    "Dockerfile",
    "railway.json",
    ".dockerignore"
)

foreach ($file in $requiredFiles) {
    if (Test-Path $file) {
        Write-Host "✓ $file" -ForegroundColor Green
    } else {
        Write-Host "✗ $file (thiếu)" -ForegroundColor Red
    }
}

# Kiểm tra build project
Write-Host ""
Write-Host "Build project để kiểm tra..." -ForegroundColor Yellow
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

# Kiểm tra Docker build
Write-Host ""
Write-Host "Kiểm tra Docker build..." -ForegroundColor Yellow
try {
    docker build -t okean-anime-movie .
    if ($LASTEXITCODE -eq 0) {
        Write-Host "Docker build thành công!" -ForegroundColor Green
    } else {
        Write-Host "Docker build thất bại!" -ForegroundColor Red
        exit 1
    }
} catch {
    Write-Host "Lỗi Docker build: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

# Hiển thị thông tin deploy
Write-Host ""
Write-Host "=== Thông tin Deploy Railway ===" -ForegroundColor Green
Write-Host ""
Write-Host "1. Push code lên GitHub:" -ForegroundColor Yellow
Write-Host "   git push origin main" -ForegroundColor Cyan
Write-Host ""
Write-Host "2. Tạo project trên Railway:" -ForegroundColor Yellow
Write-Host "   - Đăng nhập: https://railway.app/" -ForegroundColor Cyan
Write-Host "   - New Project → Deploy from GitHub repo" -ForegroundColor Cyan
Write-Host ""
Write-Host "3. Thêm MySQL Database:" -ForegroundColor Yellow
Write-Host "   - New → Database → MySQL" -ForegroundColor Cyan
Write-Host ""
Write-Host "4. Cấu hình Environment Variables:" -ForegroundColor Yellow
Write-Host "   DB_HOST=your-mysql-host.railway.app" -ForegroundColor Cyan
Write-Host "   DB_NAME=railway" -ForegroundColor Cyan
Write-Host "   DB_USER=root" -ForegroundColor Cyan
Write-Host "   DB_PASSWORD=your-mysql-password" -ForegroundColor Cyan
Write-Host "   DB_PORT=3306" -ForegroundColor Cyan
Write-Host ""
Write-Host "5. Chạy Migration:" -ForegroundColor Yellow
Write-Host "   railway run dotnet ef database update" -ForegroundColor Cyan
Write-Host ""
Write-Host "Xem file RAILWAY_DEPLOYMENT.md để biết thêm chi tiết!" -ForegroundColor Green
