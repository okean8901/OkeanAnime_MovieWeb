# TEST CASE - Okean Anime Movie

---

## MODULE 1: AUTHENTICATION (Xác Thực)

### TC_01_001: Đăng ký tài khoản hợp lệ
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_01_001 |
| Mô tả | Kiểm thử đăng ký tài khoản mới với dữ liệu hợp lệ |
| Điều kiện tiên quyết | Ứng dụng đang chạy, chưa đăng nhập |
| Bước thực hiện | 1. Vào trang Register<br>2. Nhập Email: test@example.com<br>3. Nhập Username: testuser<br>4. Nhập Password: Test@123456<br>5. Nhập Confirm Password: Test@123456<br>6. Click nút Register |
| Kết quả mong đợi | Tài khoản được tạo thành công, chuyển sang trang đăng nhập |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_01_002: Đăng ký với Email đã tồn tại
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_01_002 |
| Mô tả | Kiểm thử đăng ký với email đã được sử dụng |
| Điều kiện tiên quyết | Tài khoản admin@okeananime.com đã tồn tại |
| Bước thực hiện | 1. Vào trang Register<br>2. Nhập Email: admin@okeananime.com<br>3. Nhập Username: newuser<br>4. Nhập Password: Test@123456<br>5. Nhập Confirm Password: Test@123456<br>6. Click nút Register |
| Kết quả mong đợi | Hiển thị lỗi "Email đã được đăng ký" |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_01_003: Đăng ký với mật khẩu yếu
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_01_003 |
| Mô tả | Kiểm thử đăng ký với mật khẩu không đủ yêu cầu |
| Điều kiện tiên quyết | Ứng dụng đang chạy, chưa đăng nhập |
| Bước thực hiện | 1. Vào trang Register<br>2. Nhập Email: test@example.com<br>3. Nhập Username: testuser<br>4. Nhập Password: 123456<br>5. Nhập Confirm Password: 123456<br>6. Click nút Register |
| Kết quả mong đợi | Hiển thị lỗi "Mật khẩu phải chứa ký tự viết hoa, viết thường, số và ký tự đặc biệt" |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_01_004: Đăng nhập thành công
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_01_004 |
| Mô tả | Kiểm thử đăng nhập với tài khoản hợp lệ |
| Điều kiện tiên quyết | Tài khoản admin@okeananime.com đã tồn tại |
| Bước thực hiện | 1. Vào trang Login<br>2. Nhập Email/Username: admin@okeananime.com<br>3. Nhập Password: Admin123!<br>4. Click nút Login |
| Kết quả mong đợi | Đăng nhập thành công, chuyển sang trang Home, hiển thị tên người dùng |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_01_005: Đăng nhập với sai mật khẩu
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_01_005 |
| Mô tả | Kiểm thử đăng nhập với mật khẩu sai |
| Điều kiện tiên quyết | Tài khoản admin@okeananime.com đã tồn tại |
| Bước thực hiện | 1. Vào trang Login<br>2. Nhập Email: admin@okeananime.com<br>3. Nhập Password: WrongPassword<br>4. Click nút Login |
| Kết quả mong đợi | Hiển thị lỗi "Email hoặc mật khẩu không đúng" |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_01_006: Quên mật khẩu - Gửi email reset
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_01_006 |
| Mô tả | Kiểm thử gửi email reset mật khẩu |
| Điều kiện tiên quyết | Tài khoản admin@okeananime.com đã tồn tại, Email server đang chạy |
| Bước thực hiện | 1. Vào trang Forgot Password<br>2. Nhập Email: admin@okeananime.com<br>3. Click nút Send Reset Link |
| Kết quả mong đợi | Hiển thị thông báo "Email reset đã được gửi", Email được gửi đến admin@okeananime.com |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

## MODULE 2: ANIME MANAGEMENT (Quản Lý Anime)

### TC_02_001: Xem danh sách anime
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_02_001 |
| Mô tả | Kiểm thử xem danh sách tất cả anime |
| Điều kiện tiên quyết | Ứng dụng đang chạy, Database có 8 anime |
| Bước thực hiện | 1. Vào trang Home<br>2. Click menu "Anime"<br>3. Xem danh sách anime |
| Kết quả mong đợi | Hiển thị danh sách 8 anime với hình ảnh, tiêu đề, năm phát hành |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_02_002: Xem chi tiết anime
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_02_002 |
| Mô tả | Kiểm thử xem chi tiết một anime |
| Điều kiện tiên quyết | Danh sách anime đã được hiển thị |
| Bước thực hiến | 1. Click vào anime "Ragna Crimson"<br>2. Xem chi tiết |
| Kết quả mong đợi | Hiển thị: Tiêu đề, mô tả, năm phát hành, thể loại, poster, trailer, rating, số bình luận, nút Xem Tập |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_02_003: Tìm kiếm anime theo tên
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_02_003 |
| Mô tả | Kiểm thử tìm kiếm anime bằng từ khóa |
| Điều kiện tiên quyết | Danh sách anime đã được hiển thị |
| Bước thực hiến | 1. Nhập từ khóa "SPY" vào ô tìm kiếm<br>2. Click nút tìm kiếm hoặc Enter |
| Kết quả mong đợi | Hiển thị kết quả: anime "SPY×FAMILY" |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_02_004: Lọc anime theo thể loại
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_02_004 |
| Mô tả | Kiểm thử lọc anime theo thể loại Action |
| Điều kiện tiên quyết | Danh sách anime đã được hiển thị |
| Bước thực hiến | 1. Tìm bộ lọc thể loại<br>2. Chọn "Action"<br>3. Click Apply Filter |
| Kết quả mong đợi | Hiển thị 4 anime có thể loại Action (Ragna Crimson, SPY×FAMILY, v.v.) |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_02_005: Lọc anime theo năm phát hành
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_02_005 |
| Mô tả | Kiểm thử lọc anime theo năm 2022 |
| Điều kiện tiên quyết | Danh sách anime đã được hiển thị |
| Bước thực hiến | 1. Tìm bộ lọc năm<br>2. Chọn "2022"<br>3. Click Apply Filter |
| Kết quả mong đợi | Hiển thị anime được phát hành năm 2022 |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

## MODULE 3: VIDEO STREAMING (Xem Tập Phim)

### TC_03_001: Xem danh sách tập phim
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_03_001 |
| Mô tả | Kiểm thử xem danh sách tập phim của một anime |
| Điều kiện tiên quyết | Đã xem chi tiết anime "Ragna Crimson" |
| Bước thực hiến | 1. Nhấp nút "Xem Tập"<br>2. Xem danh sách tập phim |
| Kết quả mong đợi | Hiển thị danh sách 24 tập, mỗi tập có: số tập, tiêu đề, thời lượng |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_03_002: Phát video tập phim
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_03_002 |
| Mô tả | Kiểm thử phát video tập phim |
| Điều kiện tiên quyết | Danh sách tập phim đã được hiển thị |
| Bước thực hiến | 1. Click vào Tập 1<br>2. Video player tải<br>3. Click nút Play |
| Kết quả mong đợi | Video phát được, thanh progress hiển thị, âm thanh phát |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_03_003: Kiểm soát video (Pause, Seek, Volume)
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_03_003 |
| Mô tả | Kiểm thử các nút điều khiển video |
| Điều kiện tiên quyết | Video đang phát |
| Bước thực hiến | 1. Click Pause - video dừng<br>2. Click Play - video tiếp tục<br>3. Kéo thanh progress - seek position<br>4. Kéo slider Volume - thay đổi âm lượng |
| Kết quả mong đợi | Tất cả điều khiển hoạt động chính xác |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_03_004: Fullscreen mode
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_03_004 |
| Mô tả | Kiểm thử chế độ toàn màn hình |
| Điều kiện tiên quyết | Video đang phát |
| Bước thực hiến | 1. Click nút Fullscreen<br>2. Xem video toàn màn hình<br>3. Nhấn ESC để thoát fullscreen |
| Kết quả mong đợi | Video hiển thị toàn màn hình, thoát fullscreen bình thường |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

## MODULE 4: RATING & COMMENTS (Đánh Giá và Bình Luận)

### TC_04_001: Đánh giá anime (đã đăng nhập)
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_04_001 |
| Mô tả | Kiểm thử đánh giá anime với 5 sao |
| Điều kiện tiên quyết | Đã đăng nhập, xem chi tiết anime |
| Bước thực hiến | 1. Tìm phần Rating<br>2. Click vào ngôi sao thứ 5<br>3. Gửi đánh giá |
| Kết quả mong đợi | Đánh giá được lưu, hiển thị "Cảm ơn đã đánh giá", ngôi sao được tô màu |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_04_002: Đánh giá anime (chưa đăng nhập)
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_04_002 |
| Mô tả | Kiểm thử đánh giá khi chưa đăng nhập |
| Điều kiện tiên quyết | Chưa đăng nhập, xem chi tiết anime |
| Bước thực hiến | 1. Tìm phần Rating<br>2. Click vào ngôi sao |
| Kết quả mong đợi | Chuyển sang trang Login hoặc hiển thị popup "Vui lòng đăng nhập" |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_04_003: Viết bình luận
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_04_003 |
| Mô tả | Kiểm thử viết bình luận mới |
| Điều kiện tiên quyết | Đã đăng nhập, xem chi tiết anime |
| Bước thực hiến | 1. Tìm phần Comments<br>2. Nhập text: "Anime này hay quá!"<br>3. Click nút Post Comment |
| Kết quả mong đợi | Bình luận được post, hiển thị trong danh sách comments |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_04_004: Xóa bình luận của chính mình
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_04_004 |
| Mô tả | Kiểm thử xóa bình luận |
| Điều kiện tiên quyết | Đã post bình luận, đang xem chi tiết anime |
| Bước thực hiến | 1. Tìm bình luận của mình<br>2. Click nút Delete<br>3. Xác nhận xóa |
| Kết quả mong đợi | Bình luận bị xóa, không hiển thị trong danh sách |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_04_005: Edit bình luận
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_04_005 |
| Mô tả | Kiểm thử sửa bình luận |
| Điều kiện tiên quyết | Đã post bình luận, đang xem chi tiết anime |
| Bước thực hiến | 1. Tìm bình luận của mình<br>2. Click nút Edit<br>3. Sửa nội dung: "Anime này rất hay!"<br>4. Click Save |
| Kết quả mong đợi | Bình luận được cập nhật, hiển thị nội dung mới |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_04_006: Reply bình luận
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_04_006 |
| Mô tả | Kiểm thử trả lời bình luận |
| Điều kiện tiên quyết | Đã có bình luận từ người khác, đã đăng nhập |
| Bước thực hiến | 1. Click nút Reply dưới bình luận<br>2. Nhập text: "Mình cũng thích!"<br>3. Click Post Reply |
| Kết quả mong đợi | Reply được post, hiển thị dưới bình luận chính |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

## MODULE 5: FAVORITES & VIEW HISTORY (Yêu Thích & Lịch Sử Xem)

### TC_05_001: Thêm anime vào yêu thích
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_05_001 |
| Mô tả | Kiểm thử thêm anime vào danh sách yêu thích |
| Điều kiện tiên quyết | Đã đăng nhập, xem chi tiết anime |
| Bước thực hiến | 1. Tìm nút "Thêm vào Yêu thích"<br>2. Click nút |
| Kết quả mong đợi | Anime được thêm vào yêu thích, nút đổi màu/text |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_05_002: Xem danh sách yêu thích
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_05_002 |
| Mô tả | Kiểm thử xem danh sách anime yêu thích |
| Điều kiện tiên quyết | Đã đăng nhập, đã thêm anime vào yêu thích |
| Bước thực hiến | 1. Vào menu Profile<br>2. Click "Danh sách Yêu thích" |
| Kết quả mong đợi | Hiển thị danh sách anime yêu thích |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_05_003: Xóa khỏi yêu thích
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_05_003 |
| Mô tả | Kiểm thử xóa anime khỏi yêu thích |
| Điều kiện tiên quyết | Danh sách yêu thích đã được hiển thị |
| Bước thực hiến | 1. Click nút Xóa trên anime<br>2. Xác nhận xóa |
| Kết quả mong đợi | Anime bị xóa khỏi yêu thích, không hiển thị trong danh sách |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_05_004: Xem lịch sử xem
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_05_004 |
| Mô tả | Kiểm thử xem lịch sử xem anime |
| Điều kiện tiên quyết | Đã đăng nhập, đã xem ít nhất 1 anime |
| Bước thực hiến | 1. Vào menu Profile<br>2. Click "Lịch sử Xem" |
| Kết quả mong đợi | Hiển thị danh sách anime đã xem, thời gian xem |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_05_005: Xóa lịch sử xem
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_05_005 |
| Mô tả | Kiểm thử xóa lịch sử xem |
| Điều kiện tiên quyết | Lịch sử xem đã được hiển thị, có ít nhất 1 bản ghi |
| Bước thực hiến | 1. Click nút Xóa trên bản ghi<br>2. Xác nhận xóa |
| Kết quả mong đợi | Bản ghi bị xóa, không hiển thị trong danh sách |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

## MODULE 6: TRENDING & ANALYTICS (Xu Hướng & Thống Kê)

### TC_06_001: Xem anime Trending
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_06_001 |
| Mô tả | Kiểm thử xem anime đang xu hướng |
| Điều kiện tiên quyết | Ứng dụng đang chạy, có dữ liệu view/comment |
| Bước thực hiến | 1. Vào menu "Trending"<br>2. Xem danh sách |
| Kết quả mong đợi | Hiển thị Top anime theo view/comment trong tuần |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_06_002: Xem thống kê anime
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_06_002 |
| Mô tả | Kiểm thử xem thống kê chi tiết anime |
| Điều kiện tiên quyết | Xem chi tiết anime |
| Bước thực hiến | 1. Scroll xuống phần Analytics<br>2. Xem dữ liệu |
| Kết quả mong đợi | Hiển thị: Lượt xem, lượt comment, rating trung bình |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

## MODULE 7: USER PROFILE (Hồ Sơ Người Dùng)

### TC_07_001: Xem thông tin profile
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_07_001 |
| Mô tả | Kiểm thử xem thông tin profile cá nhân |
| Điều kiện tiên quyết | Đã đăng nhập |
| Bước thực hiến | 1. Click menu Profile<br>2. Xem thông tin |
| Kết quả mong đợi | Hiển thị: Email, Username, Ngày tạo tài khoản, avatar |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_07_002: Chỉnh sửa thông tin profile
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_07_002 |
| Mô tả | Kiểm thử chỉnh sửa thông tin profile |
| Điều kiện tiên quyết | Đang xem trang profile |
| Bước thực hiến | 1. Click nút Edit<br>2. Sửa Username: "newusername"<br>3. Click Save |
| Kết quả mong đợi | Thông tin được cập nhật, hiển thị thông báo "Cập nhật thành công" |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_07_003: Đổi mật khẩu
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_07_003 |
| Mô tả | Kiểm thử đổi mật khẩu |
| Điều kiện tiên quyết | Đã đăng nhập |
| Bước thực hiến | 1. Vào menu Profile<br>2. Click "Đổi Mật khẩu"<br>3. Nhập mật khẩu cũ: Admin123!<br>4. Nhập mật khẩu mới: NewPass@123456<br>5. Xác nhận mật khẩu mới<br>6. Click Save |
| Kết quả mong đợi | Mật khẩu được thay đổi, hiển thị thông báo thành công |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_07_004: Đăng xuất
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_07_004 |
| Mô tả | Kiểm thử đăng xuất khỏi tài khoản |
| Điều kiện tiên quyết | Đã đăng nhập |
| Bước thực hiến | 1. Click menu Profile<br>2. Click "Đăng Xuất" |
| Kết quả mong đợi | Đăng xuất thành công, chuyển sang trang Home, không hiển thị menu Profile |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

## MODULE 8: ADMIN MANAGEMENT (Quản Trị Viên)

### TC_08_001: Đăng nhập Admin
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_08_001 |
| Mô tả | Kiểm thử đăng nhập với quyền Admin |
| Điều kiện tiên quyết | Tài khoản admin@okeananime.com có role Admin |
| Bước thực hiến | 1. Đăng nhập với admin@okeananime.com / Admin123!<br>2. Kiểm tra menu Admin |
| Kết quả mong đợi | Hiển thị menu Admin trong navbar |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_08_002: Xem danh sách người dùng
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_08_002 |
| Mô tả | Kiểm thử xem danh sách tất cả người dùng |
| Điều kiện tiên quyết | Đã đăng nhập với quyền Admin |
| Bước thực hiến | 1. Click menu Admin<br>2. Click "Quản lý Người dùng"<br>3. Xem danh sách |
| Kết quả mong đợi | Hiển thị danh sách người dùng với: Email, Username, Ngày tạo, Status |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_08_003: Khóa/Mở khóa người dùng
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_08_003 |
| Mô tả | Kiểm thử khóa tài khoản người dùng |
| Điều kiện tiên quyết | Danh sách người dùng đã được hiển thị |
| Bước thực hiến | 1. Tìm người dùng<br>2. Click nút Lock<br>3. Xác nhận khóa |
| Kết quả mong đợi | Tài khoản bị khóa, không thể đăng nhập |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_08_004: Xem danh sách anime (Admin)
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_08_004 |
| Mô tả | Kiểm thử xem danh sách anime quản lý |
| Điều kiện tiên quyết | Đã đăng nhập với quyền Admin |
| Bước thực hiến | 1. Click menu Admin<br>2. Click "Quản lý Anime"<br>3. Xem danh sách |
| Kết quả mong đợi | Hiển thị danh sách 8 anime với nút Edit, Delete, Add |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_08_005: Thêm anime mới
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_08_005 |
| Mô tả | Kiểm thử thêm anime mới |
| Điều kiện tiên quyết | Đang xem danh sách anime (Admin) |
| Bước thực hiến | 1. Click nút "Thêm Anime"<br>2. Nhập tiêu đề: "Test Anime"<br>3. Nhập mô tả: "Anime test"<br>4. Chọn năm: 2024<br>5. Chọn thể loại<br>6. Upload poster<br>7. Nhập Trailer URL<br>8. Click Save |
| Kết quả mong đợi | Anime được tạo, xuất hiện trong danh sách |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_08_006: Sửa thông tin anime
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_08_006 |
| Mô tả | Kiểm thử chỉnh sửa thông tin anime |
| Điều kiện tiên quyết | Danh sách anime đã được hiển thị |
| Bước thực hiến | 1. Click nút Edit trên anime<br>2. Sửa tiêu đề<br>3. Click Save |
| Kết quả mong đợi | Thông tin anime được cập nhật |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_08_007: Xóa anime
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_08_007 |
| Mô tả | Kiểm thử xóa anime |
| Điều kiên tiên quyết | Danh sách anime đã được hiển thị |
| Bước thực hiến | 1. Click nút Delete<br>2. Xác nhận xóa |
| Kết quả mong đợi | Anime bị xóa khỏi danh sách |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_08_008: Quản lý bình luận
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_08_008 |
| Mô tả | Kiểm thử xem và xóa bình luận |
| Điều kiện tiên quyết | Đã đăng nhập với quyền Admin |
| Bước thực hiến | 1. Click menu Admin<br>2. Click "Quản lý Bình Luận"<br>3. Xem danh sách bình luận<br>4. Click Delete để xóa bình luận |
| Kết quả mong đợi | Danh sách bình luận được hiển thị, có thể xóa bình luận |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

## MODULE 9: SECURITY (Bảo Mật)

### TC_09_001: SQL Injection - Tìm kiếm
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_09_001 |
| Mô tả | Kiểm thử SQL injection trong tìm kiếm |
| Điều kiện tiên quyết | Ứng dụng đang chạy |
| Bước thực hiến | 1. Nhập vào ô tìm kiếm: ' OR '1'='1<br>2. Click tìm kiếm |
| Kết quả mong đợi | Không thực thi SQL injection, hiển thị kết quả tìm kiếm bình thường hoặc lỗi |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_09_002: XSS - Bình luận
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_09_002 |
| Mô tả | Kiểm thử XSS trong bình luận |
| Điều kiên tiên quyết | Đã đăng nhập |
| Bước thực hiến | 1. Viết bình luận với nội dung: <script>alert('XSS')</script><br>2. Post bình luận |
| Kết quả mong đợi | Script bị escape, không thực thi, hiển thị dưới dạng text |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_09_003: Truy cập trang Admin không được phép
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_09_003 |
| Mô tả | Kiểm thử truy cập Admin khi không có quyền |
| Điều kiện tiên quyết | Đã đăng nhập với user thường |
| Bước thực hiến | 1. Truy cập URL: /Admin<br>2. Thử vào trang quản lý |
| Kết quả mong đợi | Chuyển sang trang lỗi 403 Forbidden hoặc về Home |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_09_004: CSRF Protection - Form submission
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_09_004 |
| Mô tả | Kiểm thử CSRF token trong form |
| Điều kiện tiên quyết | Ứng dụng đang chạy |
| Bước thực hiến | 1. Mở Developer Tools<br>2. Kiểm tra form có CSRF token<br>3. Gửi form |
| Kết quả mong đợi | Form có __RequestVerificationToken, gửi form bình thường |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

## MODULE 10: PERFORMANCE (Hiệu Suất)

### TC_10_001: Tốc độ tải trang Home
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_10_001 |
| Mô tả | Kiểm thử tốc độ tải trang chủ |
| Điều kiện tiên quyết | Ứng dụng đang chạy, connection ổn định |
| Bước thực hiến | 1. Mở DevTools Performance<br>2. Reload trang Home<br>3. Ghi lại thời gian tải |
| Kết quả mong đợi | Trang tải < 3 giây |
| Kết quả thực tế | Thời gian: ___ giây |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_10_002: Tốc độ tải danh sách anime
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_10_002 |
| Mô tả | Kiểm thử tốc độ tải danh sách anime |
| Điều kiện tiên quyết | Ứng dụng đang chạy |
| Bước thực hiến | 1. Vào trang Anime<br>2. Ghi lại thời gian tải |
| Kết quả mong đợi | Trang tải < 2 giây, danh sách 8 anime hiển thị đầy đủ |
| Kết quả thực tế | Thời gian: ___ giây |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_10_003: Tốc độ video streaming
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_10_003 |
| Mô tả | Kiểm thử tốc độ phát video |
| Điều kiện tiên quyết | Xem tập phim |
| Bước thực hiến | 1. Bật video<br>2. Kiểm tra thời gian loading video<br>3. Kiểm tra buffering |
| Kết quả mong đợi | Video load < 2 giây, phát mượt, ít buffering |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

## MODULE 11: COMPATIBILITY (Khả Năng Tương Thích)

### TC_11_001: Chrome browser
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_11_001 |
| Mô tả | Kiểm thử trên Chrome (phiên bản mới nhất) |
| Điều kiện tiên quyết | Chrome đã cài đặt |
| Bước thực hiến | 1. Mở ứng dụng trên Chrome<br>2. Test các chức năng chính |
| Kết quả mong đợi | Tất cả chức năng hoạt động bình thường |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_11_002: Firefox browser
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_11_002 |
| Mô tả | Kiểm thử trên Firefox (phiên bản mới nhất) |
| Điều kiện tiên quyết | Firefox đã cài đặt |
| Bước thực hiến | 1. Mở ứng dụng trên Firefox<br>2. Test các chức năng chính |
| Kết quả mong đợi | Tất cả chức năng hoạt động bình thường |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_11_003: Responsive - Desktop 1920x1080
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_11_003 |
| Mô tả | Kiểm thử responsive trên desktop |
| Điều kiên tiên quyết | Ứng dụng đang chạy |
| Bước thực hiến | 1. Mở DevTools<br>2. Set viewport 1920x1080<br>3. Xem layout |
| Kết quả mong đợi | Layout hiển thị đầy đủ, không bị tắt các phần tử |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_11_004: Responsive - Tablet 768x1024
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_11_004 |
| Mô tả | Kiểm thử responsive trên tablet |
| Điều kiên tiên quyết | Ứng dụng đang chạy |
| Bước thực hiến | 1. Mở DevTools<br>2. Set viewport 768x1024<br>3. Xem layout |
| Kết quả mong đợi | Layout responsive, không tắt phần tử, có thể scroll |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

### TC_11_005: Responsive - Mobile 375x667
| Thuộc tính | Giá trị |
|-----------|--------|
| Test Case ID | TC_11_005 |
| Mô tả | Kiểm thử responsive trên mobile |
| Điều kiên tiên quyết | Ứng dụng đang chạy |
| Bước thực hiến | 1. Mở DevTools<br>2. Set viewport 375x667<br>3. Xem layout |
| Kết quả mong đợi | Layout responsive, menu mobile, tất cả chức năng vẫn hoạt động |
| Kết quả thực tế | |
| Status | PASS / FAIL |
| Ghi chú | |

---

