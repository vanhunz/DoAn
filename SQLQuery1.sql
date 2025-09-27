CREATE TABLE VaiTro (
    MaVaiTro INT PRIMARY KEY IDENTITY(1,1),
    TenVaiTro NVARCHAR(50) NOT NULL
);
CREATE TABLE NguoiDung (
    MaND INT PRIMARY KEY IDENTITY(1,1),
    TenDangNhap NVARCHAR(50) UNIQUE NOT NULL,
    MatKhau NVARCHAR(100) NOT NULL,
    HoTen NVARCHAR(100),
    Email NVARCHAR(100),
    SoDienThoai NVARCHAR(20),
    NgayTao DATETIME DEFAULT GETDATE(),
    MaVaiTro INT NOT NULL,
    FOREIGN KEY (MaVaiTro) REFERENCES VaiTro(MaVaiTro)
);
CREATE TABLE HangHoa (
    MaHang INT PRIMARY KEY IDENTITY(1,1),
    TenHang NVARCHAR(100) NOT NULL,
    DonVi NVARCHAR(50),
    GiaNhap DECIMAL(18,2),
    GiaXuat DECIMAL(18,2),
    SoLuongTon INT DEFAULT 0
);
CREATE TABLE PhieuNhap (
    MaPhieuNhap INT PRIMARY KEY IDENTITY(1,1),
    NgayNhap DATETIME DEFAULT GETDATE(),
    MaND INT NOT NULL,
    GhiChu NVARCHAR(200),
    FOREIGN KEY (MaND) REFERENCES NguoiDung(MaND)
);
CREATE TABLE ChiTietNhap (
    ID INT PRIMARY KEY IDENTITY(1,1),
    MaPhieuNhap INT NOT NULL,
    MaHang INT NOT NULL,
    SoLuong INT NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (MaPhieuNhap) REFERENCES PhieuNhap(MaPhieuNhap),
    FOREIGN KEY (MaHang) REFERENCES HangHoa(MaHang)
);

-- Bảng Phiếu xuất
CREATE TABLE PhieuXuat (
    MaPhieuXuat INT PRIMARY KEY IDENTITY(1,1),
    NgayXuat DATETIME DEFAULT GETDATE(),
    MaND INT NOT NULL,
    GhiChu NVARCHAR(200),
    FOREIGN KEY (MaND) REFERENCES NguoiDung(MaND)
);
CREATE TABLE ChiTietXuat (
    ID INT PRIMARY KEY IDENTITY(1,1),
    MaPhieuXuat INT NOT NULL,
    MaHang INT NOT NULL,
    SoLuong INT NOT NULL,
    DonGia DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (MaPhieuXuat) REFERENCES PhieuXuat(MaPhieuXuat),
    FOREIGN KEY (MaHang) REFERENCES HangHoa(MaHang)
);
1. VAI TRÒ
INSERT INTO VaiTro (TenVaiTro) VALUES
(N'Quản trị viên'),
(N'Nhân viên kho');

2. NGƯỜI DÙNG
INSERT INTO NguoiDung (TenDangNhap, MatKhau, HoTen, Email, SoDienThoai, MaVaiTro) VALUES
(N'admin1', N'123456', N'Nguyễn Văn A', N'admin1@example.com', N'0901234567', 1),
(N'admin2', N'123456', N'Lê Thị B', N'admin2@example.com', N'0907654321', 1),
(N'kho01', N'123456', N'Phạm Văn C', N'kho01@example.com', N'0911223344', 2),
(N'kho02', N'123456', N'Hoàng Thị D', N'kho02@example.com', N'0911334455', 2),
(N'kho03', N'123456', N'Ngô Văn E', N'kho03@example.com', N'0911445566', 2);

3. Hàng hóa
INSERT INTO HangHoa (TenHang, DonVi, GiaNhap, GiaXuat, SoLuongTon) VALUES
(N'Bút bi Thiên Long', N'Cây', 2000, 3000, 500),
(N'Vở 96 trang', N'Cuốn', 5000, 7000, 1000),
(N'Tập giấy A4', N'Ram', 40000, 55000, 200),
(N'Mực in HP 12A', N'Hộp', 90000, 120000, 80),
(N'Kẹp giấy', N'Hộp', 5000, 8000, 300),
(N'Bìa nhựa', N'Cái', 7000, 10000, 150),
(N'Bảng trắng', N'Cái', 120000, 150000, 30),
(N'USB 16GB', N'Cái', 100000, 130000, 50);

4. Phiếu nhập
INSERT INTO PhieuNhap (MaND, GhiChu) VALUES
(3, N'Nhập hàng tháng 1'),
(4, N'Nhập hàng tháng 2'),
(5, N'Nhập thêm thiết bị'),
(3, N'Bổ sung tồn kho');

5. Chi tiết nhập
INSERT INTO ChiTietNhap (MaPhieuNhap, MaHang, SoLuong, DonGia) VALUES
(1, 1, 200, 2000),
(1, 2, 500, 5000),
(2, 3, 100, 40000),
(2, 4, 40, 90000),
(3, 8, 20, 100000),
(3, 7, 10, 120000),
(4, 5, 100, 5000),
(4, 6, 50, 7000);

6. Phiếu xuất
INSERT INTO PhieuXuat (MaND, GhiChu) VALUES
(1, N'Xuất vật tư cho phòng kế toán'),
(2, N'Xuất cho phòng nhân sự'),
(1, N'Xuất thiết bị đào tạo');

7. Chi tiết xuất
INSERT INTO ChiTietXuat (MaPhieuXuat, MaHang, SoLuong, DonGia) VALUES
(1, 1, 50, 3000),
(1, 2, 100, 7000),
(1, 5, 50, 8000),

(2, 3, 20, 55000),
(2, 6, 20, 10000),

(3, 7, 5, 150000),
(3, 8, 10, 130000);
