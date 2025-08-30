# Okean Anime Movie

Ứng dụng web xem anime được xây dựng bằng ASP.NET Core 8.0 và MySQL.

## 🚀 Tính năng

- **Xem Anime**: Xem các bộ anime với chất lượng cao
- **Quản lý Favorites**: Lưu và quản lý anime yêu thích
- **Hệ thống Comment**: Bình luận và thảo luận về anime
- **View History**: Theo dõi lịch sử xem anime
- **Trending**: Xem các anime đang hot
- **Admin Panel**: Quản lý người dùng và nội dung
- **Authentication**: Đăng ký, đăng nhập với JWT

## 🛠️ Công nghệ sử dụng

- **Backend**: ASP.NET Core 8.0
- **Database**: MySQL 8.0
- **ORM**: Entity Framework Core
- **Authentication**: ASP.NET Core Identity + JWT
- **Frontend**: Razor Pages, Bootstrap, jQuery
- **Deployment**: Railway, Docker

## 📋 Yêu cầu hệ thống

- .NET 8.0 SDK
- MySQL 8.0+
- Git

## 🚀 Cài đặt và chạy

### 1. Clone repository
```bash
git clone <repository-url>
cd Okean_AnimeMovie
```

### 2. Cài đặt MySQL
- Tải MySQL Server từ: https://dev.mysql.com/downloads/mysql/
- Đặt mật khẩu root là: `123456`
- Hoặc sử dụng XAMPP

### 3. Tạo database
```bash
# Sử dụng script PowerShell
.\setup_mysql.ps1

# Hoặc chạy script SQL trong MySQL Workbench
# Xem file: create_database.sql
```

### 4. Cấu hình connection string
Cập nhật `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=Okean_AnimeMovie;User=root;Password=123456;Port=3306;"
  }
}
```

### 5. Chạy migrations
```bash
dotnet ef database update
```

### 6. Chạy ứng dụng
```bash
dotnet run
```

Truy cập: https://localhost:7000

## 🚀 Deploy lên Railway

### 1. Chuẩn bị deploy
```bash
# Chạy script kiểm tra
.\prepare_railway_deploy.ps1
```

### 2. Push code lên GitHub
```bash
git add .
git commit -m "Prepare for Railway deployment"
git push origin main
```

### 3. Tạo project trên Railway
1. Đăng nhập: https://railway.app/
2. New Project → Deploy from GitHub repo
3. Chọn repository của bạn

### 4. Thêm MySQL Database
1. New → Database → MySQL
2. Railway sẽ tự động tạo database

### 5. Cấu hình Environment Variables
Trong Railway Dashboard, thêm:
```env
DB_HOST=your-mysql-host.railway.app
DB_NAME=railway
DB_USER=root
DB_PASSWORD=your-mysql-password
DB_PORT=3306
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://0.0.0.0:8080
```

### 6. Chạy migration
```bash
railway run dotnet ef database update
```

Xem chi tiết trong file: `RAILWAY_DEPLOYMENT.md`

## 📁 Cấu trúc dự án

```
Okean_AnimeMovie/
├── Controllers/          # API Controllers
├── Core/                # Domain entities và interfaces
│   ├── DTOs/           # Data Transfer Objects
│   ├── Entities/       # Domain entities
│   ├── Interfaces/     # Repository interfaces
│   └── Services/       # Service interfaces
├── Infrastructure/     # Data access và external services
│   ├── Data/          # DbContext và migrations
│   ├── Repositories/  # Repository implementations
│   └── Services/      # Service implementations
├── Views/             # Razor views
├── wwwroot/          # Static files (CSS, JS, images)
├── Migrations/       # Entity Framework migrations
└── ViewComponents/   # Custom view components
```

## 🔧 Cấu hình

### Database
- **Provider**: MySQL
- **Connection**: Pomelo.EntityFrameworkCore.MySql
- **Migrations**: Entity Framework Core

### Authentication
- **Provider**: ASP.NET Core Identity
- **JWT**: JSON Web Tokens
- **Password Policy**: 8+ chars, uppercase, lowercase, digit, special char

### Security
- **HTTPS**: Enabled in development
- **CORS**: Configured for web clients
- **JWT Secret**: Configurable via environment variables

## 📚 API Endpoints

### Authentication
- `POST /Account/Register` - Đăng ký
- `POST /Account/Login` - Đăng nhập
- `POST /Account/Logout` - Đăng xuất

### Anime
- `GET /Anime` - Danh sách anime
- `GET /Anime/{id}` - Chi tiết anime
- `GET /Anime/Watch/{id}` - Xem anime
- `POST /Anime/Create` - Tạo anime (Admin)
- `PUT /Anime/Edit/{id}` - Sửa anime (Admin)

### Favorites
- `GET /Favorites` - Danh sách yêu thích
- `POST /Favorites/Add/{animeId}` - Thêm vào yêu thích
- `DELETE /Favorites/Remove/{animeId}` - Xóa khỏi yêu thích

### Comments
- `GET /Comment/GetComments/{animeId}` - Lấy comments
- `POST /Comment/Add` - Thêm comment
- `DELETE /Comment/Delete/{id}` - Xóa comment

## 🧪 Testing

```bash
# Build project
dotnet build

# Run tests (nếu có)
dotnet test

# Check code quality
dotnet format
```

## 📝 Migration

```bash
# Tạo migration mới
dotnet ef migrations add MigrationName

# Cập nhật database
dotnet ef database update

# Xóa migration cuối
dotnet ef migrations remove
```

## 🔍 Troubleshooting

### Lỗi kết nối database
- Kiểm tra MySQL service đang chạy
- Kiểm tra connection string
- Kiểm tra mật khẩu root

### Lỗi build
- Kiểm tra .NET 8.0 SDK đã cài đặt
- Restore NuGet packages: `dotnet restore`
- Clean và rebuild: `dotnet clean && dotnet build`

### Lỗi migration
- Xóa migrations cũ: `dotnet ef migrations remove`
- Tạo migration mới: `dotnet ef migrations add InitialCreate`
- Cập nhật database: `dotnet ef database update`

## 📄 License

MIT License - xem file LICENSE để biết thêm chi tiết.

## 🤝 Contributing

1. Fork repository
2. Tạo feature branch: `git checkout -b feature/AmazingFeature`
3. Commit changes: `git commit -m 'Add some AmazingFeature'`
4. Push to branch: `git push origin feature/AmazingFeature`
5. Tạo Pull Request

## 📞 Support

- **Email**: support@okeananime.com
- **Issues**: https://github.com/your-repo/issues
- **Documentation**: Xem các file .md trong project

## 🔄 Changelog

### v1.0.0
- Initial release
- Basic anime viewing functionality
- User authentication
- Favorites system
- Comments system
- Admin panel

---

**Okean Anime Movie** - Nơi tốt nhất để xem anime! 🎬✨
