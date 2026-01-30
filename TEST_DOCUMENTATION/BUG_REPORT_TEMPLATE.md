# BÁO CÁO LỖI (BUG REPORT)

Hình mẫu báo cáo lỗi cho dự án Okean Anime Movie

---

## Mẫu Bug Report

| Trường | Mô tả | Ví dụ |
|--------|-------|-------|
| Bug ID | ID duy nhất của bug | BUG_001 |
| Ngày báo cáo | Ngày phát hiện bug | 30/01/2026 |
| Người báo cáo | Tên tester | Tester 1 |
| Tiêu đề | Tiêu đề ngắn gọn | Video không phát được |
| Mô tả chi tiết | Mô tả chi tiết lỗi | Khi click Play, video không phát |
| Bước tái hiện lỗi | 1. Vào trang anime<br>2. Click Xem Tập<br>3. Bấm Play | 1. Đăng nhập<br>2. Vào SPY×FAMILY<br>3. Chọn Tập 1<br>4. Click Play |
| Dự kiến vs Thực tế | So sánh kết quả | Dự kiến: Video phát mượt<br>Thực tế: Video không phát, console lỗi |
| Mức độ (Severity) | Critical / High / Medium / Low | High |
| Trạng thái | Open / In Progress / Fixed / Retest / Closed | Open |
| Người xử lý | Tên developer | Dev 1 |
| Environment | Windows / Mac / Linux | Windows 11 |
| Browser | Chrome / Firefox / Edge / Safari | Chrome 130 |
| URL / Path | Đường dẫn page | /Anime/Details/1 |
| Screenshot / Video | Hình ảnh hoặc video lỗi | [Attachment] |
| Ghi chú | Thông tin thêm | Xảy ra trên tất cả các tập |

---

## Ví Dụ Báo Cáo Lỗi

### BUG_001: Video không phát được

| Trường | Giá trị |
|--------|--------|
| Bug ID | BUG_001 |
| Ngày báo cáo | 30/01/2026 |
| Người báo cáo | Tester 1 |
| Tiêu đề | Video không phát được - Player error |
| Mô tả chi tiết | Khi nhấp nút Play trên video player, video không phát được. Console hiển thị lỗi "Cannot read property 'play' of undefined" |
| Bước tái hiện | 1. Đăng nhập tài khoản user@test.com<br>2. Vào anime SPY×FAMILY<br>3. Click "Xem Tập"<br>4. Click vào Tập 1<br>5. Click nút Play |
| Dự kiến | Video phát, thấy nội dung anime |
| Thực tế | Video không phát, không có lỗi hiển thị nhưng không chạy |
| Severity | High |
| Trạng thái | Open |
| Người xử lý | Dev 1 |
| Environment | Windows 11 |
| Browser | Chrome 130 |
| URL | /Episodes/Details/1 |
| Screenshot | [Video player blank] |
| Ghi chú | Xảy ra ở tất cả các tập, 100% replicable |

---

### BUG_002: Bình luận không hiển thị

| Trường | Giá trị |
|--------|--------|
| Bug ID | BUG_002 |
| Ngày báo cáo | 30/01/2026 |
| Người báo cáo | Tester 2 |
| Tiêu đề | Bình luận mới không hiển thị ngay sau khi post |
| Mô tả chi tiết | Sau khi post bình luận, bình luận không hiển thị trong danh sách bình luận ngay lập tức |
| Bước tái hiện | 1. Đăng nhập<br>2. Vào chi tiết anime<br>3. Viết bình luận: "Test comment"<br>4. Click Post Comment |
| Dự kiến | Bình luận hiển thị ngay dưới ô input |
| Thực tế | Phải reload trang mới thấy bình luận vừa post |
| Severity | Medium |
| Trạng thái | In Progress |
| Người xử lý | Dev 2 |
| Environment | Windows 10 |
| Browser | Firefox 133 |
| URL | /Anime/Details/2 |
| Screenshot | [Comments section before/after] |
| Ghi chú | Xảy ra ở tất cả anime |

---

### BUG_003: Lọc anime không hoạt động

| Trường | Giá trị |
|--------|--------|
| Bug ID | BUG_003 |
| Ngày báo cáo | 29/01/2026 |
| Người báo cáo | Tester 3 |
| Tiêu đề | Bộ lọc thể loại không lọc chính xác |
| Mô tả chi tiết | Khi chọn thể loại "Comedy" để lọc, vẫn hiển thị anime khác không phải Comedy |
| Bước tái hiện | 1. Vào trang Anime<br>2. Chọn bộ lọc Genre = "Comedy"<br>3. Click Apply Filter |
| Dự kiến | Chỉ hiển thị anime có thể loại Comedy |
| Thực tế | Vẫn hiển thị 8 anime (tất cả) |
| Severity | High |
| Trạng thái | Fixed |
| Người xử lý | Dev 3 |
| Environment | Windows 11 |
| Browser | Edge 130 |
| URL | /Anime |
| Screenshot | [Filter results] |
| Ghi chú | Lỗi ở tất cả bộ lọc |

---

### BUG_004: Nút Delete bình luận tương tác sai

| Trường | Giá trị |
|--------|--------|
| Bug ID | BUG_004 |
| Ngày báo cáo | 28/01/2026 |
| Người báo cáo | Tester 1 |
| Tiêu đề | Click Delete bình luận xóa sai comment |
| Mô tả chi tiết | Khi có nhiều bình luận và bấm Delete trên một comment, nó lại xóa comment khác |
| Bước tái hiện | 1. Vào anime có 5+ bình luận<br>2. Scroll xuống<br>3. Click Delete trên comment thứ 3 |
| Dự kiến | Xóa comment thứ 3 |
| Thực tế | Xóa comment thứ 5 |
| Severity | High |
| Trạng thái | Fixed |
| Người xử lý | Dev 2 |
| Environment | Windows 10 |
| Browser | Chrome 130 |
| URL | /Anime/Details/1 |
| Screenshot | [Comments list before/after] |
| Ghi chú | Do ID binding sai trong JavaScript |

---

### BUG_005: Performance - Trang tải chậm

| Trường | Giá trị |
|--------|--------|
| Bug ID | BUG_005 |
| Ngày báo cáo | 27/01/2026 |
| Người báo cáo | Tester 3 |
| Tiêu đề | Danh sách anime tải quá chậm (>5 giây) |
| Mô tả chi tiết | Trang danh sách anime tải lâu hơn 5 giây |
| Bước tái hiện | 1. Vào trang Anime |
| Dự kiến | Tải < 3 giây |
| Thực tế | Tải 5-7 giây, DevTools Network tab hiển thị query database mất ~4s |
| Severity | Medium |
| Trạng thái | In Progress |
| Người xử lý | Dev 1 |
| Environment | Windows 11 |
| Browser | Chrome 130 |
| URL | /Anime |
| Screenshot | [DevTools Network tab] |
| Ghi chú | Cần optimize query, có thể thêm caching |

---

## Bug Severity Levels

| Mức Độ | Mô Tả | Ví Dụ |
|--------|-------|-------|
| CRITICAL | Tính năng chính không hoạt động, ứng dụng crash | Không thể đăng nhập, Database error |
| HIGH | Tính năng chính bị hỏng, ảnh hưởng lớn | Video không phát, lọc không hoạt động |
| MEDIUM | Tính năng phụ bị lỗi, ít ảnh hưởng | Bình luận không hiển thị ngay, UI lỗi nhỏ |
| LOW | Lỗi nhỏ, không ảnh hưởng nhiều | Lỗi chính tả, UI không căn chỉnh |

---

## Bug Status

| Trạng thái | Mô tả |
|-----------|-------|
| Open | Bug vừa được báo cáo, chưa xử lý |
| In Progress | Developer đang xử lý |
| Fixed | Developer xác nhận sửa xong, chờ verify |
| Retest | Tester kiểm thử lại sau khi fix |
| Closed | Bug được xác nhận sửa đúng |
| Rejected | Bug không được accept (không lỗi) |
| Duplicate | Bug trùng lặp với bug khác |

---

## Hướng Dẫn Báo Cáo Bug Hiệu Quả

1. **Tiêu đề rõ ràng**: Mô tả lỗi trong 1 dòng
2. **Mô tả chi tiết**: Giải thích rõ lỗi là gì
3. **Bước tái hiện**: Liệt kê chi tiết từng bước để tái hiện
4. **Dự kiến vs Thực tế**: So sánh kết quả mong đợi với kết quả thực tế
5. **Environment**: Ghi rõ OS, Browser, phiên bản
6. **Screenshot/Video**: Đính kèm hình ảnh hoặc video lỗi
7. **Console error**: Nếu có lỗi JavaScript, copy console message
8. **Replicable**: Ghi rõ có thể tái hiện 100% hay không
9. **Frequency**: Lỗi xảy ra bao giờ (mỗi lần / ngẫu nhiên)
10. **Workaround**: Có cách khác để thực hiện chức năng không

---

