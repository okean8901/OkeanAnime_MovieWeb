# Script để sửa các giá trị Rating trong DbInitializer.cs
$filePath = "Okean_AnimeMovie\Infrastructure\Data\DbInitializer.cs"

# Đọc nội dung file
$content = Get-Content $filePath -Raw

# Thay thế tất cả các giá trị Rating = 8.0 thành Rating = 8.0M
$newContent = $content -replace 'Rating = 8\.0,', 'Rating = 8.0M,'

# Ghi lại file
Set-Content $filePath $newContent -Encoding UTF8

Write-Host "Đã sửa tất cả các giá trị Rating trong DbInitializer.cs"
