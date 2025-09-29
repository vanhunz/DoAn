
# 🍀 Dự án Quản lý kho thực phẩm

## 🚀 Cài đặt và chạy

### 1. Clone project
```bash
git clone https://github.com/vanhunz/DoAn
````

Mở project bằng **Visual Studio 2022**.

---

### 2. Tạo database từ file SQL

1. Mở **Visual Studio**

2. Vào menu **View > SQL Server Object Explorer**

3. Kết nối với **(localdb)\MSSQLLocalDB**, đặt tên **QLThucPham**

<img width="208" height="58" alt="image" src="https://github.com/user-attachments/assets/3b240e95-6e97-4b66-97b1-031a09d2fb98" />


4. Chuột phải vào **QLThucPham** → chọn **New Query**

5. Mở file `QLThucPham.sql` (có trong thư mục project) → copy toàn bộ nội dung

6. Dán vào Query Editor và bấm **Execute (Ctrl + Shift + E)**

👉 Lúc này database **QLKho** sẽ được tạo cùng các bảng và dữ liệu mẫu.

---

### 3. Cập nhật chuỗi kết nối (nếu cần)

Trong file **`Web.config`**, chỉnh lại `connectionStrings` nếu LocalDB có tên khác.

---

### 4. Chạy project

Nhấn **Ctrl + F5** trong Visual Studio để chạy.

![image](https://github.com/user-attachments/assets/bf8f013c-91ce-4777-bd8e-a1edbd336ca1)

---

## 🛠 Chức năng chính

* Quản lý vai trò (Admin, Nhân viên, …)
* Quản lý người dùng (Tài khoản đăng nhập)
* Quản lý hàng hóa/thực phẩm (Thêm, sửa, xóa, số lượng tồn)
* Quản lý phiếu nhập và chi tiết nhập
* Quản lý phiếu xuất và chi tiết xuất

---

## 📧 Liên hệ

Nếu có vấn đề khi chạy project, vui lòng liên hệ:

📩 **Email:** [vovanhuanhjhj@gmail.com](mailto:vovanhuanhjhj@gmail.com)
💬 **Zalo:** 0397199215 *(Ưu tiên)*

