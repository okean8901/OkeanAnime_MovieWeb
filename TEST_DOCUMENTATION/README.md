# Bộ Tài Liệu Kiểm Thử - Okean Anime Movie

Thư mục này chứa toàn bộ tài liệu kiểm thử cho dự án Okean Anime Movie.

---

## Cấu Trúc Tài Liệu

### 1. TEST_PLAN.md
- Kế hoạch kiểm thử chi tiết
- Mục tiêu, phạm vi, loại kiểm thử
- Môi trường test, thời gian, công cụ
- Nhân sự, tiêu chí pass/fail
- Quản lý rủi ro

**Đối tượng**: Test Lead, Project Manager

---

### 2. TEST_CASES.md
- 87 test case cụ thể
- 11 module khác nhau:
  - Authentication (6 test)
  - Anime Management (5 test)
  - Video Streaming (4 test)
  - Rating & Comments (6 test)
  - Favorites & History (5 test)
  - Trending & Analytics (2 test)
  - User Profile (4 test)
  - Admin Management (8 test)
  - Security (4 test)
  - Performance (3 test)
  - Compatibility (5 test)

**Đối tượng**: QA Engineers, Testers

---

### 3. TEST_GUIDE_WALKTHROUGH.md
- Hướng dẫn cài đặt environment
- Hướng dẫn kiểm thử chi tiết từng chức năng
- Tài khoản test (admin + 2 user thường)
- Checklist kiểm thử hàng ngày
- Báo cáo kết quả test
- Troubleshooting & support contact

**Đối tượng**: QA Engineers, Testers

---

### 4. BUG_REPORT_TEMPLATE.md
- Mẫu báo cáo lỗi chi tiết
- 5 ví dụ bug thực tế
- Severity levels (Critical, High, Medium, Low)
- Bug status (Open, In Progress, Fixed, Closed)
- Hướng dẫn báo cáo bug hiệu quả

**Đối tượng**: QA Engineers

---

### 5. TEST_PROGRESS_TRACKING.md
- Theo dõi tiến độ kiểm thử
- Tổng hợp metrics
- Tiến độ theo module
- Tracking bug
- Daily report template
- KPI metrics
- Risk assessment

**Đối tượng**: QA Lead, Team Lead

---

### 6. TEST_SUMMARY_REPORT.md
- Tổng hợp báo cáo kiểm thử
- Executive summary
- Test coverage (96%)
- Bug summary & recommendations
- Release decision

**Đối tượng**: Project Manager, QA Lead

---

## Tài Khoản Test

### Admin Account
- Email: `admin@okeananime.com`
- Password: `Admin123!`

### Regular Users
- Email: `user1@test.com` / Password: `User@123456`
- Email: `user2@test.com` / Password: `User@123456`

---

## Công Cụ Kiểm Thử Cần Thiết

- Postman (API testing)
- Browser DevTools (Frontend testing)
- SQL Server Management Studio (Database)
- Chrome Performance (Performance testing)
- OWASP ZAP (Security testing)
- Google Sheets (Test management)

---

## Lịch Trình

| Giai đoạn | Thời gian |
|----------|----------|
| Chuẩn bị | 2 ngày |
| Smoke Test | 1 ngày |
| Functional Test | 5 ngày |
| Regression Test | 2 ngày |
| UAT | 2 ngày |
| **Total** | **12 ngày** |

---

## Chỉ Tiêu Chất Lượng

- Pass Rate: > 95%
- Coverage: > 80%
- Critical Bugs: 0
- Performance: < 3 giây
- No security vulnerabilities

---

## Quick Start

1. Đọc **TEST_PLAN.md** để hiểu kế hoạch chung
2. Xem **TEST_CASES.md** để biết chi tiết từng test
3. Theo **TEST_GUIDE_WALKTHROUGH.md** để thực hành
4. Sử dụng **BUG_REPORT_TEMPLATE.md** để báo cáo lỗi
5. Cập nhật **TEST_PROGRESS_TRACKING.md** để theo dõi tiến độ

---

## Contact

- QA Lead: leadqa@company.com
- Email Group: qa-team@company.com
- Slack: #qa-testing

---

**Last Updated**: 30/01/2026
