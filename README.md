# 🎬 Okean Anime Movie

**Okean Anime Movie** là một ứng dụng web xem anime trực tuyến được xây dựng bằng ASP.NET Core 8.0 và SQL Server. Ứng dụng cung cấp trải nghiệm xem anime chất lượng cao với nhiều tính năng hiện đại.

## ✨ Tính năng chính

### 🎯 **Quản lý Anime**
- Xem danh sách anime với thông tin chi tiết
- Tìm kiếm và lọc anime theo thể loại, năm phát hành
- Xem thông tin chi tiết: poster, trailer, mô tả, đánh giá
- Quản lý tập phim với video streaming

### 👥 **Hệ thống người dùng**
- Đăng ký và đăng nhập tài khoản
- Quản lý profile cá nhân
- Phân quyền người dùng (Admin/User)
- Lịch sử xem anime

### ❤️ **Tương tác người dùng**
- Đánh giá anime (1-10 sao)
- Bình luận và thảo luận
- Thêm anime vào danh sách yêu thích
- Theo dõi số lượt xem

### 📊 **Thống kê và xu hướng**
- Anime xem nhiều nhất
- Anime đánh giá cao nhất
- Anime bình luận nhiều nhất
- Anime mới thêm gần đây
- Xu hướng trong tuần

### 🎭 **Quản lý nội dung**
- Quản lý thể loại anime
- Upload và quản lý video tập phim
- Hệ thống comment phân cấp
- Quản lý người dùng (Admin)

## 🛠️ Công nghệ sử dụng

### **Backend**
- **ASP.NET Core 8.0** - Framework web
- **Entity Framework Core 8.0** - ORM
- **SQL Server** - Database
- **ASP.NET Core Identity** - Authentication & Authorization
- **JWT Bearer** - Token authentication
- **AutoMapper** - Object mapping
- **FluentValidation** - Data validation

### **Frontend**
- **Razor Pages** - Server-side rendering
- **Bootstrap 5** - UI framework
- **jQuery** - JavaScript library
- **AJAX** - Asynchronous requests

### **Development Tools**
- **Visual Studio 2022** - IDE
- **SQL Server Management Studio** - Database management
- **Git** - Version control

## 📁 Cấu trúc dự án

```
Okean_AnimeMovie/
├── Controllers/           # MVC Controllers
├── Core/                 # Domain layer
│   ├── DTOs/            # Data Transfer Objects
│   ├── Entities/        # Domain entities
│   ├── Interfaces/      # Repository interfaces
│   └── Services/        # Business services
├── Infrastructure/      # Data access layer
│   ├── Data/           # DbContext & configurations
│   ├── Repositories/   # Repository implementations
│   └── Services/       # Infrastructure services
├── Views/              # Razor views
├── ViewComponents/     # Reusable view components
├── wwwroot/           # Static files (CSS, JS, images)
└── Migrations/        # Database migrations
```

## 🚀 Cài đặt và chạy dự án

### **Yêu cầu hệ thống**
- .NET 8.0 SDK
- SQL Server 2019+ hoặc SQL Server Express
- Visual Studio 2022 (khuyến nghị)

### **Bước 1: Clone dự án**
```bash
git clone <repository-url>
cd Okean_AnimeMovie
```

### **Bước 2: Cấu hình database**
1. Mở **SQL Server Management Studio**
2. Tạo database mới: `Okean_AnimeMovie`
3. Cập nhật connection string trong `appsettings.json` nếu cần

### **Bước 3: Cài đặt dependencies**
```bash
dotnet restore
```

### **Bước 4: Tạo database schema**
```bash
cd Okean_AnimeMovie
dotnet ef database update
```

### **Bước 5: Chạy ứng dụng**
```bash
dotnet run --environment Development
```

Ứng dụng sẽ chạy tại:
- **HTTP**: http://localhost:5000
- **HTTPS**: https://localhost:5001

## 🔧 Cấu hình

### **Connection String**
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=Okean_AnimeMovie;Trusted_Connection=true;TrustServerCertificate=true;MultipleActiveResultSets=true"
  }
}
```

### **JWT Configuration**
```json
{
  "Jwt": {
    "Secret": "YourSuperSecretKeyHere123456789012345678901234567890",
    "Issuer": "OkeanAnimeMovie",
    "Audience": "OkeanAnimeMovieUsers",
    "ExpiryInMinutes": 60
  }
}
```

## 📊 Database Schema

### **Các bảng chính**
- **Animes** - Thông tin anime
- **Episodes** - Tập phim
- **Genres** - Thể loại
- **Users** - Người dùng
- **Comments** - Bình luận
- **Ratings** - Đánh giá
- **Favorites** - Yêu thích
- **ViewHistories** - Lịch sử xem

## 👨‍💻 Tài khoản mặc định

### **Admin Account**
- **Email**: admin@okean.com
- **Password**: Admin123!
- **Role**: Administrator

### **User Account**
- **Email**: user@okean.com
- **Password**: User123!
- **Role**: User

## 🔒 Bảo mật

- **JWT Authentication** cho API endpoints
- **ASP.NET Core Identity** cho user management
- **Role-based Authorization** cho phân quyền
- **HTTPS** cho production
- **SQL Injection Protection** với Entity Framework

## 📈 Tính năng nâng cao

### **Performance**
- **Lazy Loading** cho navigation properties
- **Pagination** cho danh sách dài
- **Caching** cho dữ liệu tĩnh
- **Optimized Queries** với Entity Framework

### **User Experience**
- **Responsive Design** cho mobile
- **Real-time Updates** với AJAX
- **Progressive Loading** cho video
- **Search & Filter** nâng cao

## 🤝 Đóng góp

1. Fork dự án
2. Tạo feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to branch (`git push origin feature/AmazingFeature`)
5. Tạo Pull Request

## 📝 License

Dự án này được phát hành dưới giấy phép MIT. Xem file `LICENSE` để biết thêm chi tiết.


## 🙏 Lời cảm ơn

Cảm ơn tất cả những người đã đóng góp vào dự án này!

---

**Okean Anime Movie** - Nơi anime trở nên sống động! 🎬✨
