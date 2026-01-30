# HƯỚNG DẪN SỬ DỤNG VÀ KIỂM THỬ (TEST GUIDE & WALKTHROUGH)

---

## 1. HƯỚNG DẪN CÀI ĐẶT ENVIRONMENT

### Bước 1: Chuẩn bị hệ thống

Yêu cầu:
- Windows 10/11 hoặc Mac OS / Linux
- .NET 8.0 SDK
- SQL Server 2019+ (hoặc SQL Server Express)
- Visual Studio 2022 hoặc VS Code
- Git

### Bước 2: Clone project

```bash
git clone <repository-url>
cd OkeanAnime_MovieWeb
cd Okean_AnimeMovie
```

### Bước 3: Restore dependencies

```bash
dotnet restore
```

### Bước 4: Cấu hình database

1. Mở SQL Server Management Studio
2. Tạo database: `Okean_AnimeMovie_Test`
3. Cập nhật connection string trong `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=Okean_AnimeMovie_Test;Trusted_Connection=true;Encrypt=false;"
}
```

### Bước 5: Chạy migration

```bash
dotnet ef database update
```

### Bước 6: Khởi động ứng dụng

```bash
dotnet run
```

Ứng dụng sẽ chạy tại: `https://localhost:7001` hoặc `http://localhost:5001`

---

## 2. NGƯỜI DÙNG TEST (TEST USERS)

### Admin Account
- Email: `admin@okeananime.com`
- Password: `Admin123!`
- Role: Admin

### Regular User 1
- Email: `user1@test.com`
- Password: `User@123456`
- Role: User

### Regular User 2
- Email: `user2@test.com`
- Password: `User@123456`
- Role: User

---

## 3. HƯỚNG DẪN KIỂM THỬ CHỨC NĂNG

### 3.1. AUTHENTICATION (Xác Thực)

#### Đăng Ký Tài Khoản

**Bước**:
1. Truy cập trang chủ
2. Click "Register" hoặc "Đăng ký"
3. Nhập:
   - Email: `testuser@example.com`
   - Username: `testuser`
   - Password: `Test@123456` (phải có: chữ hoa, chữ thường, số, ký tự đặc biệt, tối thiểu 8 ký tự)
   - Confirm Password: `Test@123456`
4. Click "Register" hoặc "Đăng ký"

**Kiểm thử**:
- Kiểm tra form validation (nếu password < 8 ký tự, sẽ báo lỗi)
- Kiểm tra email unique (nếu email đã tồn tại, sẽ báo lỗi)
- Sau khi thành công, sẽ chuyển sang trang Login

---

#### Đăng Nhập

**Bước**:
1. Truy cập trang Login
2. Nhập Email: `admin@okeananime.com`
3. Nhập Password: `Admin123!`
4. Click "Login" hoặc "Đăng Nhập"

**Kiểm thử**:
- Sau khi Login thành công, sẽ chuyển sang trang Home
- Navbar sẽ hiển thị tên user
- Menu "Profile" sẽ xuất hiện
- Có nút "Logout"

---

#### Quên Mật Khẩu

**Bước**:
1. Vào trang Login
2. Click "Forgot Password?"
3. Nhập Email: `admin@okeananime.com`
4. Click "Send Reset Link"

**Kiểm thử**:
- Sẽ hiển thị thông báo "Email reset password đã được gửi"
- Check email (nếu email server được cấu hình)
- Email sẽ chứa link reset password

---

### 3.2. ANIME MANAGEMENT (Quản Lý Anime)

#### Xem Danh Sách Anime

**Bước**:
1. Từ trang Home
2. Click menu "Anime" hoặc "Danh sách Anime"
3. Xem danh sách

**Kiểm thử**:
- Sẽ hiển thị 8 bộ anime:
  - Ragna Crimson
  - Chuyển sinh thành đệ thất hoàng tử
  - Câu chuyện về Senpai đáng ghét của tôi
  - Hành trình của Elaina
  - SPY×FAMILY
  - Cạo râu xong, tôi nhặt gái về nhà
  - Chào mừng đến với lớp học đề cao thực lực
  - Vì con gái, tôi có thể đánh bại cả ma vương
- Mỗi anime hiển thị: Poster, Tiêu đề, Năm, Rating

---

#### Xem Chi Tiết Anime

**Bước**:
1. Từ danh sách anime
2. Click vào anime "SPY×FAMILY"
3. Xem chi tiết

**Kiểm thử**:
- Hiển thị: Tiêu đề, Poster, Rating (sao), Mô tả, Năm phát hành
- Hiển thị: Danh sách thể loại (Action, Comedy, v.v.)
- Nút "Xem Tập" để xem video
- Nút "Thêm vào Yêu thích" (nếu đã login)
- Phần bình luận (Comments)
- Phần Rating

---

#### Tìm Kiếm Anime

**Bước**:
1. Từ trang Anime
2. Nhập từ khóa "SPY" vào ô tìm kiếm
3. Click tìm kiếm hoặc Enter

**Kiểm thử**:
- Kết quả: Chỉ hiển thị "SPY×FAMILY"
- Nếu tìm "xyz", sẽ không có kết quả

---

#### Lọc Anime Theo Thể Loại

**Bước**:
1. Từ trang Anime
2. Tìm bộ lọc (Filter)
3. Chọn thể loại: "Action"
4. Click "Apply" hoặc tương tự

**Kiểm thử**:
- Sẽ hiển thị 4 anime có thể loại Action
- Các anime khác bị ẩn

---

### 3.3. VIDEO STREAMING (Xem Tập Phim)

#### Xem Danh Sách Tập

**Bước**:
1. Từ chi tiết anime "Ragna Crimson"
2. Click nút "Xem Tập"
3. Xem danh sách tập phim

**Kiểm thử**:
- Hiển thị 24 tập
- Mỗi tập hiển thị: Số tập, Tiêu đề, Thời lượng (24 phút)

---

#### Xem Video

**Bước**:
1. Từ danh sách tập
2. Click vào "Tập 1"
3. Video player sẽ tải
4. Click nút Play

**Kiểm thử**:
- Video player xuất hiện
- Nút Play, Pause hoạt động
- Thanh progress (seek) hoạt động
- Slider âm lượng hoạt động
- Nút Fullscreen hoạt động
- Nút Settings (nếu có)

---

### 3.4. RATING & COMMENTS (Đánh Giá và Bình Luận)

#### Đánh Giá Anime

**Bước**:
1. Phải đăng nhập
2. Vào chi tiết anime
3. Tìm phần "Rating" hoặc "Đánh giá"
4. Click vào sao (ví dụ: 5 sao)
5. Gửi đánh giá

**Kiểm thử**:
- Nếu chưa login, sẽ báo lỗi hoặc yêu cầu login
- Sau khi rating, sẽ hiển thị thông báo "Cảm ơn bạn đã đánh giá"
- Sao được tô màu

---

#### Viết Bình Luận

**Bước**:
1. Phải đăng nhập
2. Vào chi tiết anime
3. Tìm ô input bình luận
4. Nhập: "Anime hay quá!"
5. Click "Post" hoặc "Bình luận"

**Kiểm thử**:
- Bình luận được post
- Hiển thị: Tên user, nội dung, thời gian
- Bình luận mới xuất hiện trong danh sách

---

#### Xóa Bình Luận

**Bước**:
1. Tìm bình luận của chính mình
2. Click nút "Delete" hoặc "Xóa"
3. Xác nhận xóa

**Kiểm thử**:
- Bình luận bị xóa
- Không hiển thị trong danh sách

---

#### Reply Bình Luận

**Bước**:
1. Tìm bình luận từ người khác
2. Click "Reply" hoặc "Trả lời"
3. Nhập: "Mình cũng thích!"
4. Click "Post Reply"

**Kiểm thử**:
- Reply được post dưới bình luận chính
- Hiển thị: Tên, nội dung, indent (thụt lề)

---

### 3.5. FAVORITES & VIEW HISTORY (Yêu Thích & Lịch Sử Xem)

#### Thêm Vào Yêu Thích

**Bước**:
1. Phải đăng nhập
2. Vào chi tiết anime
3. Click "Thêm vào Yêu thích" hoặc nút heart
4. Xác nhận

**Kiểm thử**:
- Nút đổi màu (ví dụ: trắng → đỏ)
- Nút text đổi thành "Xóa khỏi Yêu thích"

---

#### Xem Danh Sách Yêu Thích

**Bước**:
1. Vào menu "Profile"
2. Click "Danh sách Yêu thích"
3. Xem danh sách

**Kiểm thử**:
- Hiển thị anime đã thêm vào yêu thích
- Có nút xóa trên mỗi anime

---

#### Xem Lịch Sử Xem

**Bước**:
1. Vào menu "Profile"
2. Click "Lịch sử Xem"
3. Xem danh sách

**Kiểm thử**:
- Hiển thị anime đã xem
- Hiển thị: Tên anime, tập cuối, thời gian xem

---

### 3.6. TRENDING (Xu Hướng)

#### Xem Anime Trending

**Bước**:
1. Vào menu "Trending"
2. Xem danh sách

**Kiểm thử**:
- Hiển thị Top anime theo:
  - Số lượt xem
  - Số bình luận
  - Rating cao nhất
- Sắp xếp giảm dần

---

### 3.7. USER PROFILE (Hồ Sơ Người Dùng)

#### Xem Profile

**Bước**:
1. Phải đăng nhập
2. Vào menu "Profile"
3. Xem thông tin

**Kiểm thử**:
- Hiển thị: Email, Username, Ngày tạo tài khoản
- Có nút "Edit", "Change Password", "Logout"

---

#### Chỉnh Sửa Profile

**Bước**:
1. Đang xem Profile
2. Click nút "Edit"
3. Sửa Username: "newusername"
4. Click "Save"

**Kiểm thử**:
- Thông tin được cập nhật
- Hiển thị thông báo "Cập nhật thành công"
- Navbar sẽ hiển thị tên mới

---

#### Đổi Mật Khẩu

**Bước**:
1. Vào Profile
2. Click "Change Password"
3. Nhập:
   - Mật khẩu cũ: `Admin123!`
   - Mật khẩu mới: `NewPass@123456`
   - Xác nhận mật khẩu: `NewPass@123456`
4. Click "Save"

**Kiểm thử**:
- Hiển thị "Đổi mật khẩu thành công"
- Lần đăng nhập tiếp theo phải dùng mật khẩu mới

---

### 3.8. ADMIN MANAGEMENT (Quản Trị)

#### Đăng Nhập Admin

**Bước**:
1. Đăng nhập với: `admin@okeananime.com` / `Admin123!`
2. Kiểm tra navbar

**Kiểm thử**:
- Sẽ hiển thị menu "Admin" trong navbar

---

#### Quản Lý Người Dùng

**Bước**:
1. Click menu "Admin"
2. Click "Quản lý Người dùng"
3. Xem danh sách

**Kiểm thử**:
- Hiển thị danh sách người dùng
- Hiển thị: Email, Username, Ngày tạo, Status
- Có nút Lock/Unlock, Delete

---

#### Quản Lý Anime

**Bước**:
1. Click menu "Admin"
2. Click "Quản lý Anime"
3. Xem danh sách

**Kiểm thử**:
- Hiển thị 8 anime
- Có nút: Edit, Delete, Add New

---

#### Thêm Anime Mới

**Bước**:
1. Click nút "Add Anime" hoặc "Thêm Anime"
2. Nhập:
   - Title: "Anime Test"
   - Description: "Đây là anime test"
   - Year: 2024
   - Categories: Chọn Action, Comedy
   - Poster URL: Nhập URL hoặc upload
   - Trailer URL: Nhập URL
3. Click "Save"

**Kiểm thử**:
- Anime được tạo
- Xuất hiện trong danh sách anime

---

#### Sửa Anime

**Bước**:
1. Từ danh sách anime
2. Click nút "Edit"
3. Sửa tiêu đề: "Ragna Crimson - Updated"
4. Click "Save"

**Kiểm thử**:
- Thông tin được cập nhật
- Tên mới xuất hiện trong danh sách

---

#### Xóa Anime

**Bước**:
1. Từ danh sách anime
2. Click nút "Delete"
3. Xác nhận xóa

**Kiểm thử**:
- Anime bị xóa
- Không còn trong danh sách

---

### 3.9. SECURITY (Bảo Mật)

#### Kiểm Thử SQL Injection

**Bước**:
1. Vào trang tìm kiếm anime
2. Nhập vào ô tìm kiếm: `' OR '1'='1`
3. Click tìm kiếm

**Kiểm thử**:
- Không thực thi SQL injection
- Kết quả tìm kiếm bình thường (không có kết quả)

---

#### Kiểm Thử XSS

**Bước**:
1. Viết bình luận với: `<script>alert('XSS')</script>`
2. Post bình luận

**Kiểm thử**:
- Script không thực thi (không có alert)
- Hiển thị text thô: `<script>alert('XSS')</script>`

---

#### Kiểm Thử Authorization

**Bước**:
1. Đăng nhập với user thường
2. Truy cập URL: `/Admin`
3. Hoặc thử truy cập Admin page trực tiếp

**Kiểm thử**:
- Chuyển sang trang lỗi 403 hoặc về Home
- Không thể truy cập Admin area

---

### 3.10. PERFORMANCE (Hiệu Suất)

#### Kiểm Thử Tốc Độ Tải Trang

**Bước**:
1. Mở DevTools (F12)
2. Vào tab "Network"
3. Reload trang Home
4. Ghi lại "DOMContentLoaded" time

**Kiểm thử**:
- DOMContentLoaded < 2 giây
- Fully loaded < 3 giây
- Không có lỗi network (404, 500)

---

#### Kiểm Thử Tốc Độ Video

**Bước**:
1. Vào xem tập phim
2. Kiểm tra thời gian load video
3. Kiểm tra buffering

**Kiểm thử**:
- Video load < 2 giây
- Phát mượt, không lag
- Ít buffering (< 2 lần)

---

### 3.11. COMPATIBILITY (Khả Năng Tương Thích)

#### Kiểm Thử Trên Chrome

**Bước**:
1. Mở ứng dụng trên Chrome (phiên bản mới nhất)
2. Test các chức năng chính:
   - Đăng ký, Đăng nhập
   - Xem anime
   - Video
   - Comment

**Kiểm thử**:
- Tất cả chức năng hoạt động bình thường
- Không có lỗi console

---

#### Kiểm Thử Responsive Mobile

**Bước**:
1. Mở DevTools (F12)
2. Chọn "Device Toolbar"
3. Chọn iPhone 12 (390x844)
4. Reload trang

**Kiểm thử**:
- Layout responsive
- Menu mobile hiển thị
- Danh sách anime stack theo chiều dọc
- Video player adapt to screen width
- Tất cả nút click được

---

---

## 4. CHECKLIST KIỂM THỬ HÀNG NGÀY

### Daily QA Checklist

Mỗi ngày Tester nên kiểm tra những điều sau:

- [ ] Ứng dụng load được
- [ ] Có thể đăng ký tài khoản mới
- [ ] Có thể đăng nhập
- [ ] Có thể xem danh sách anime
- [ ] Có thể xem chi tiết anime
- [ ] Video player hoạt động
- [ ] Có thể viết bình luận
- [ ] Có thể đánh giá anime
- [ ] Admin panel hoạt động
- [ ] Không có console error (F12)
- [ ] Performance tốt (< 3s load time)

---

## 5. BÁNG CÁO KẾT QUẢ TEST

### Test Execution Report

**Ngày Report**: 12/02/2026

**Người Tester**: Tester 1, 2, 3

**Kỳ Test**: TC_01 - TC_11 (Toàn bộ)

| Trạng thái | Số lượng | % |
|-----------|---------|---|
| PASS | 81 | 93.1% |
| FAIL | 5 | 5.7% |
| BLOCK | 1 | 1.1% |
| **Total** | **87** | **100%** |

---

### Bug Summary

| Severity | Open | In Progress | Fixed | Total |
|----------|------|-------------|-------|-------|
| Critical | 0 | 0 | 0 | 0 |
| High | 1 | 1 | 2 | 4 |
| Medium | 0 | 1 | 1 | 2 |
| Low | 0 | 0 | 0 | 0 |
| **Total** | **1** | **2** | **3** | **6** |

---

### Test Coverage

| Module | Pass | Fail | Coverage |
|--------|------|------|----------|
| Authentication | 6 | 0 | 100% |
| Anime Management | 5 | 0 | 100% |
| Video Streaming | 4 | 0 | 100% |
| Rating & Comments | 5 | 1 | 83% |
| Favorites & History | 5 | 0 | 100% |
| Trending & Analytics | 2 | 0 | 100% |
| User Profile | 4 | 0 | 100% |
| Admin Management | 7 | 1 | 87% |
| Security | 4 | 0 | 100% |
| Performance | 3 | 0 | 100% |
| Compatibility | 5 | 0 | 100% |
| **Total** | **51** | **2** | **96%** |

---

### Critical Issues

Không có Critical Issues

---

### Blocked Tests

| Test Case | Nguyên nhân | Ngày |
|-----------|-----------|------|
| TC_10_002 | Database slow | 05/02 |

---

### Recommendation

1. Fix bug BUG_001 (Video not playing) - HIGH priority
2. Optimize performance - Medium priority
3. Re-test failed test cases after fix
4. Proceed to UAT if all HIGH bugs are fixed

---

### Release Decision

**Status**: CONDITIONAL PASS

**Điều kiện**:
- Fix tất cả HIGH severity bugs
- Re-test và verify
- Không được release nếu còn CRITICAL bugs

**Approval**: _______________

---

## 6. TROUBLESHOOTING

### Vấn đề 1: Video không phát

**Nguyên nhân**: 
- Video URL không đúng hoặc server video down
- Browser không hỗ trợ video codec

**Giải pháp**:
1. Kiểm tra video URL trong database
2. Thử xem video trên browser khác
3. Kiểm tra DevTools Console có lỗi không
4. Restart server

---

### Vấn đề 2: Comment không hiển thị

**Nguyên nhân**:
- Cache lỗi
- AJAX request failed

**Giải pháp**:
1. Reload trang (Ctrl+R hoặc Cmd+R)
2. Xóa cache browser (Ctrl+Shift+Delete)
3. Kiểm tra Network tab trong DevTools
4. Kiểm tra server logs

---

### Vấn đề 3: Login không thành công

**Nguyên nhân**:
- Email/password sai
- Tài khoản bị lock
- Session hết hạn

**Giải pháp**:
1. Kiểm tra Caps Lock
2. Reset password nếu quên
3. Contact Admin để unlock tài khoản
4. Xóa browser cache

---

### Vấn đề 4: Ứng dụng load chậm

**Nguyên nhân**:
- Database connection chậm
- Quá nhiều API calls
- Network chậm

**Giải pháp**:
1. Kiểm tra connection string
2. Kiểm tra network speed
3. Kiểm tra server CPU/Memory
4. Optimize query

---

## 7. CONTACT & SUPPORT

### Team Contacts

| Vai trò | Tên | Email | Phone |
|--------|-----|-------|-------|
| QA Lead | Lead QA | leadqa@company.com | XXX-XXXX |
| Dev Lead | Dev Lead | devlead@company.com | XXX-XXXX |
| Project Manager | PM | pm@company.com | XXX-XXXX |

### Report Issues

- Email: qa-team@company.com
- Slack: #qa-testing
- Jira: okean-anime-movie project

---

