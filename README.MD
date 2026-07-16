# Student Management API (.NET Core Web API)

Dự án Web API quản lý thông tin sinh viên được xây dựng bằng ASP.NET Core. Dự án đã được tái cấu trúc (refactor) và chuẩn hóa theo các mô hình thiết kế chuẩn (Standard Design Patterns), tách biệt rõ ràng giữa các lớp dữ liệu và tối ưu hóa trải nghiệm người dùng cuối (Client).

##  Các Tính Năng Đã Hoàn Thành

- **Phân tách DTO (Data Transfer Objects):** Tách biệt hoàn toàn các Model truyền nhận dữ liệu (`CreateStudentDTO`, `UpdateStudentDTO`, `StudentResponseDTO`) để tăng tính bảo mật và tối ưu hiệu năng.
- **Chuẩn hóa phản hồi (Standardized ApiResponse):** Toàn bộ API trả về cấu trúc thống nhất giúp Client dễ dàng xử lý lỗi và dữ liệu (`ApiResponse<T>`).
- **Phân biệt luồng truyền tải dữ liệu:** Áp dụng chuẩn RESTful, phân định rõ ràng cách truyền dữ liệu qua **Route Parameter**, **Query String**, và **Request Body**.
- **Bộ lọc tìm kiếm đa năng (Search System):** Hỗ trợ tìm kiếm sinh viên linh hoạt theo nhiều tiêu chí (`Name`, `Age`, `Address`, `ClassName`).
- **Hệ thống phân trang (Pagination):** Hỗ trợ phân chia trang dữ liệu linh hoạt (`PageNumber`, `PageSize`) giảm tải cho hệ thống khi lượng dữ liệu lớn.
- **Dependency Injection (DI):** Đăng ký dịch vụ lỏng lẻo qua Interface (`IStudentService` - `StudentService`) trong `Program.cs`.

---

## Cấu Trúc Thư Mục Dự Án

```text
StudentAPI/
│
├── Controller/
│   └── StudentController.cs       # Tiếp nhận request, điều phối dữ liệu (Route/Query/Body)
│
├── DTO/
│   └── StudentDTO.cs              # Định nghĩa Create/Update/Response DTOs
│
├── Model/
│   ├── Request/
│   │   └── PaginationRequest.cs   # Chứa thông tin số trang (PageNumber, PageSize)
│   └── Responses/
│       ├── ApiResponse.cs         # Cấu trúc phản hồi API chuẩn chỉnh toàn hệ thống
│       └── PageResponse.cs        # Trả về kết quả phân trang kèm Metadata
│
├── Service/
│   ├── IStudentService.cs         # Khai báo các nghiệp vụ quản lý sinh viên
│   └── StudentService.cs          # Triển khai chi tiết logic xử lý dữ liệu
│
├── Program.cs                     
└── README.md  


## Công Nghệ Sử Dụng
Framework: .NET 10.0 (hoặc .NET Core tương đương)
API Documentation: Swagger UI / OpenAPI
Quản lý mã nguồn: Git (Conventional Commits)

