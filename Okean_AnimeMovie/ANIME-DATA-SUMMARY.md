# 📺 Tóm tắt Dữ liệu Anime trong Okean AnimeMovie

## 🎬 Danh sách Anime đã có

### 1. **Ragna Crimson** (24 tập)
- **Thể loại**: Action, Fantasy
- **Năm**: 2023
- **Trạng thái**: Hoàn thành
- **Mô tả**: Trong một thế giới mà loài rồng thống trị từ trên trời đến dưới biển sâu, những người muốn đấu tranh chống lại chúng buộc phải bứt phá được giới hạn vốn có của con người.

### 2. **Chuyển sinh thành đệ thất hoàng tử, tôi quyết định trau dồi ma thuật** (12 tập)
- **Thể loại**: Action, Fantasy
- **Năm**: 2024
- **Trạng thái**: Hoàn thành
- **Mô tả**: Câu chuyện chuyển sinh của đệ thất hoàng tử với những trận chiến phép thuật đầy phấn khích và ấn tượng.

### 3. **Câu chuyện về Senpai đáng ghét của tôi** (12 tập)
- **Thể loại**: Comedy, Drama, Romance, Slice of Life
- **Năm**: 2021
- **Trạng thái**: Hoàn thành
- **Mô tả**: Futaba Igarashi, một nhân viên văn phòng nhỏ nhắn và hay gây sự, liên tục phàn nàn về người đồng nghiệp to con và ồn ào của mình, Harumi Takeda.

### 4. **Hành trình của Elaina** (12 tập)
- **Thể loại**: Adventure, Fantasy, Slice of Life
- **Năm**: 2016
- **Trạng thái**: Hoàn thành
- **Mô tả**: Một ngày nọ, cô bé Elaina đọc được quyển sách 'Những cuộc phiêu lưu của Nike', và rồi ấp ủ ước mơ trở thành phù thủy, được đi chu du đây đó.

### 5. **SPY×FAMILY** (12 tập)
- **Thể loại**: Action, Comedy, Drama, Mystery, Romance, Sci-Fi
- **Năm**: 2022
- **Trạng thái**: Hoàn thành
- **Mô tả**: Điệp viên Twilight nhận nhiệm vụ thực hiện chiến dịch Strix nhằm bảo vệ hòa bình Đông Tây và thế giới.

### 6. **Cạo râu xong, tôi nhặt gái về nhà** (12 tập)
- **Thể loại**: Comedy, Drama, Romance, Slice of Life
- **Năm**: 2018
- **Trạng thái**: Hoàn thành
- **Mô tả**: Yoshida là một nhân viên văn phòng 26 tuổi, vừa bị crush suốt 5 năm trời từ chối. Trên đường mượn rượu giải sầu về, anh nhìn thấy một nữ sinh trung học đang ngồi bên xó đường.

### 7. **Chào mừng đến với lớp học đề cao thực lực** (12 tập)
- **Thể loại**: Comedy, Drama, Romance, Slice of Life
- **Năm**: 2017
- **Trạng thái**: Hoàn thành
- **Mô tả**: Trường THPT Đào tạo Nâng cao Tokyo là ngôi trường danh tiếng hàng đầu với các cơ sở hiện đại, nơi có 100% học sinh đậu đại học hoặc tìm được việc làm danh giá sau khi tốt nghiệp.

### 8. **Vì con gái, tôi có thể đánh bại cả ma vương** (12 tập)
- **Thể loại**: Action, Adventure, Fantasy, Drama, Mystery, Romance, Sci-Fi
- **Năm**: 2019
- **Trạng thái**: Hoàn thành
- **Mô tả**: Dale là một mạo hiểm giả đã đạt được những thành tựu dù còn rất trẻ. Trong một lần thực hiện nhiệm vụ, cậu đã gặp Latina – một cô nhóc thuộc Quỷ Nhân tộc – bị bỏ lại trong rừng.

## 📊 Thống kê Tổng quan

- **Tổng số anime**: 8 bộ
- **Tổng số tập phim**: 120 tập
- **Thể loại phổ biến**: 
  - Action (4 anime)
  - Fantasy (4 anime)
  - Comedy (5 anime)
  - Drama (6 anime)
  - Romance (5 anime)
  - Slice of Life (4 anime)

## 🎯 Cách Sử dụng

1. **Chạy ứng dụng**: `dotnet run`
2. **Database sẽ tự động khởi tạo** với tất cả dữ liệu trên
3. **Đăng nhập Admin**: `admin@okeananime.com` / `Admin123!`
4. **Quản lý anime**: Vào Admin > Anime để xem, sửa, xóa
5. **Xem anime**: Vào Anime > Index để xem danh sách

## 🔧 Cập nhật Dữ liệu

Để thêm anime mới:
1. Sửa file `Infrastructure/Data/DbInitializer.cs`
2. Thêm anime vào danh sách `animes`
3. Thêm thể loại vào `animeGenres`
4. Thêm tập phim vào `episodes`
5. Chạy lại ứng dụng hoặc xóa database để khởi tạo lại

## 📝 Lưu ý

- Tất cả anime đều có poster và trailer URL
- Mỗi tập phim có thời lượng 24 phút (1440 giây)
- Video URL hiện tại là placeholder, cần thay thế bằng URL thực tế
- Rating và ViewCount được thiết lập mặc định
