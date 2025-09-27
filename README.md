
Ứng dụng Quản lý Thực phẩm (ASP.NET Web Forms + SQL Server)

Dự án này là một **website quản lý kho thực phẩm** được xây dựng bằng **ASP.NET Web Forms** và **SQL Server LocalDB**.  
Database được khởi tạo từ file script `.sql` để dễ dàng chia sẻ và tái tạo.

---

##  Hướng dẫn cài đặt và chạy dự án

### 1 Clone project về máy
Mở terminal / git bash và chạy lệnh:
```bash
git clone https://github.com/vanhunz/DoAn
git clone 
````

Sau đó mở project bằng **Visual Studio 2022**.

---

### 2 Tạo database từ file SQL

1. Mở **Visual Studio**.
2. Vào menu **View > SQL Server Object Explorer**.
3. Kết nối với **(localdb)\MSSQLLocalDB**.
4. Chuột phải vào **QLThucPham** → chọn **New Query**.
5. Mở file `QLKho.sql` (có trong thư mục project) → copy toàn bộ nội dung.
6. Dán vào query editor và bấm **Execute (Ctrl + Shift + E)**.

👉 Lúc này database **QLKho** sẽ được tạo cùng các bảng và dữ liệu mẫu.

---

### 3 Cập nhật chuỗi kết nối (nếu cần)

Trong file `Web.config`, chỉnh lại `connectionStrings` nếu LocalDB khác tên:

```xml
<connectionStrings>
  <add name="QLThucPhamEntities" connectionString="metadata=res://*/Model.ThucPhamModel.csdl|res://*/Model.ThucPhamModel.ssdl|res://*/Model.ThucPhamModel.msl;provider=System.Data.SqlClient;
 provider connection string=&quot;Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\CS464\DoAn\QLThucPham.mdf;
Integrated Security=True&quot;
" providerName="System.Data.EntityClient" />
</connectionStrings>
```

---

### 4 Chạy project

* Nhấn **Ctrl + F5** trong Visual Studio.
* Truy cập website qua trình duyệt (mặc định là `https://localhost:xxxx`).

---

## 🛠 Chức năng chính

* Quản lý vai trò (admin, nhân viên, ...).
* Quản lý người dùng (tài khoản đăng nhập).
* Quản lý hàng hóa/thực phẩm (thêm, sửa, xóa, số lượng tồn).
* Quản lý phiếu nhập và chi tiết nhập
* Quản lý phiếu xuất và chi tiết xuất

---

## 📧 Liên hệ

Nếu có vấn đề khi chạy project, vui lòng liên hệ: vovanhuanhjhj@gmail.com hoặc za.lo 0397199215( Ưu tiên )
