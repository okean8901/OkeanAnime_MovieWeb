# KỀ HOẠCH KIỂM THỬ (TEST PLAN)
## Okean Anime Movie - v1.0

---

## 1. MỤC TIÊU KIỂM THỬ

- Xác minh tất cả các chức năng chính hoạt động đúng theo yêu cầu
- Phát hiện lỗi (bug) trước khi phát hành sản phẩm
- Kiểm tra hiệu suất, độ ổn định và bảo mật ứng dụng
- Đảm bảo trải nghiệm người dùng tốt trên các trình duyệt khác nhau

---

## 2. PHẠM VI KIỂM THỬ

### Chức năng được kiểm thử:
1. Hệ thống xác thực (Đăng ký, Đăng nhập, Quên mật khẩu)
2. Quản lý profile người dùng
3. Danh sách anime và chi tiết anime
4. Tìm kiếm và lọc anime
5. Xem tập phim (Video streaming)
6. Đánh giá anime
7. Bình luận và phản hồi bình luận
8. Danh sách yêu thích
9. Lịch sử xem
10. Xu hướng anime (Trending)
11. Thống kê analytics
12. Quản trị nội dung (Admin)

### Chức năng không được kiểm thử (out of scope):
- Kiểm thử performance tải nặng (100,000+ người dùng)
- Kiểm thử trên thiết bị di động chi tiết (chỉ test responsive)
- Kiểm thử tích hợp thanh toán trực tuyến

---

## 3. LOẠI KIỂM THỬ

| Loại | Mô tả |
|------|-------|
| Functional Testing | Kiểm thử các chức năng chính của ứng dụng |
| Usability Testing | Kiểm thử trải nghiệm và tính dễ sử dụng |
| Compatibility Testing | Kiểm thử trên Chrome, Firefox, Edge, Safari |
| Security Testing | Kiểm thử input validation, SQL injection, XSS |
| Performance Testing | Kiểm thử tốc độ tải trang, phản hồi API |

---

## 4. MÔI TRƯỜNG KIỂM THỬ

### Máy chủ:
- OS: Windows Server 2019+
- .NET: 8.0 SDK
- Database: SQL Server 2019+
- Server: IIS 10.0+

### Máy khách:
- OS: Windows 10/11, MacOS, Linux
- Trình duyệt: Chrome, Firefox, Edge, Safari (phiên bản mới nhất)
- Độ phân giải: 1920x1080, 1366x768, 768x1024 (tablet)

### Data Test:
- Database: Okean_AnimeMovie_Test
- Dữ liệu mẫu: 8 anime, 5 người dùng test, 20 bình luận
- User admin: admin@okeananime.com / Admin123!
- User thường: user@test.com / User@123456

---

## 5. THỜI GIAN KIỂM THỬ

| Giai đoạn | Thời gian | Mô tả |
|----------|-----------|-------|
| Chuẩn bị test plan | 2 ngày | Viết test case, chuẩn bị environment |
| Smoke test | 1 ngày | Kiểm thử nhanh các chức năng chính |
| Functional test | 5 ngày | Kiểm thử chi tiết từng chức năng |
| Regression test | 2 ngày | Kiểm thử lại khi có fix |
| UAT | 2 ngày | Người dùng cuối kiểm thử |

**Tổng thời gian**: 12 ngày

---

## 6. CÔNG CỤ KIỂM THỬ

| Công cụ | Mục đích |
|---------|---------|
| **Postman** | Kiểm thử API |
| **Browser DevTools** | Kiểm thử Frontend, Console errors |
| **SQL Server Management Studio** | Kiểm thử Database |
| **Chrome DevTools Performance** | Kiểm thử Performance |
| **OWASP ZAP** | Kiểm thử Security |
| **Google Sheets** | Quản lý Test Case, Bug Report |

---

## 7. NHÂN SỰ THAM GIA

| Vai trò | Người | Trách nhiệm |
|---------|-------|------------|
| Test Lead | Lead QA | Giám sát kế hoạch test, báo cáo tiến độ |
| QA Engineer 1 | Tester 1 | Kiểm thử chức năng User |
| QA Engineer 2 | Tester 2 | Kiểm thử chức năng Admin |
| QA Engineer 3 | Tester 3 | Kiểm thử API & Performance |
| Developer | Dev Team | Fix bug phát hiện |

---

## 8. TIÊU CHÍ ĐẬU / RỚT

### Điều kiện PASS (Sản phẩm có thể release):
- Tất cả 100% Test Case PASS
- Không có bug Severity = HIGH hoặc CRITICAL
- Performance: Tải trang < 3 giây
- Không có lỗi security nghiêm trọng
- Coverage > 80%

### Điều kiện FAIL (Dừng release):
- > 10% Test Case FAIL
- Có bug CRITICAL không fix được
- Performance: Tải trang > 5 giây
- Lỗi security (SQL injection, XSS, CSRF)

---

## 9. QUẢN LÝ RỦI RO

| Rủi ro | Xác suất | Ảnh hưởng | Biện pháp |
|--------|----------|----------|-----------|
| Delay phát triển | Cao | Giảm thời gian test | Plan test song song với dev |
| Database crash | Thấp | Cao | Backup database định kỳ |
| Lỗi performance | Trung bình | Cao | Tối ưu query sớm |
| Lỗi bảo mật | Thấp | Rất cao | Security test từ sớm |

---

## 10. TIÊU CHÍ HOÀN THÀNH

- Tất cả test case được thực hiện
- Tất cả bug được ghi chép chi tiết
- Test report được lập
- Metrics được thu thập (Pass/Fail rate, Bug distribution)
- Approval từ Project Manager

---

## 11. PHƯƠNG PHÁP BÁNG CÁO

- **Daily Report**: Số lượng test case đã chạy, bug phát hiện
- **Weekly Report**: Tổng hợp tiến độ, rủi ro, recommendation
- **Final Report**: Test summary, metrics, approval decision

---

## 12. SIGN-OFF

| Vai trò | Tên | Chữ ký | Ngày |
|--------|-----|--------|------|
| Test Lead | | | |
| Project Manager | | | |
| Developer Lead | | | |
| Client/Business | | | |
