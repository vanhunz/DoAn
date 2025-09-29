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
    GiaNhap DECIMAL(18,2),
    GiaXuat DECIMAL(18,2),
    SoLuongTon INT DEFAULT 0,
    NSX DATE,
    HSD DATE
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
INSERT INTO VaiTro (TenVaiTro) VALUES
(N'Quản trị'),
(N'Nhân viên kho'),
(N'Kế toán'),
(N'Nhân viên bán hàng'),
(N'Khách hàng');
INSERT INTO NguoiDung (TenDangNhap, MatKhau, HoTen, Email, SoDienThoai, MaVaiTro) VALUES
(N'admin',  N'123456', N'Nguyễn Văn A', N'admin@shop.com',  N'0901000001', 1),
(N'kho1',   N'123456', N'Lê Thị B',     N'kho1@shop.com',   N'0901000002', 2),
(N'ketoan1',N'123456', N'Trần Văn C',  N'ketoan@shop.com', N'0901000003', 3),
(N'ban1',   N'123456', N'Phạm Thị D',  N'ban1@shop.com',   N'0901000004', 4),
(N'user1',  N'123456', N'Đỗ Văn E',    N'user1@shop.com',  N'0901000005', 5);
INSERT INTO HangHoa (TenHang, GiaNhap, GiaXuat, SoLuongTon, NSX, HSD) VALUES
(N'Gạo Jasmine 5kg',       60000,  80000, 100,'2025-01-05','2026-01-05'),
(N'Gạo ST25 5kg',          70000,  95000, 80, '2025-01-08','2026-01-08'),
(N'Đường trắng 1kg',       15000,  22000, 120,'2025-02-01','2027-02-01'),
(N'Muối hạt 500g',          5000,   8000, 90, '2025-02-03','2027-02-03'),
(N'Nước mắm Phú Quốc 500ml',35000, 48000, 60,'2025-01-15','2026-07-15'),
(N'Nước tương 500ml',      20000,  30000, 70,'2025-01-20','2026-01-20'),
(N'Dầu ăn 1L',             40000,  55000, 85,'2025-01-25','2026-01-25'),
(N'Bột ngọt 500g',         25000,  35000, 95,'2025-02-05','2027-02-05'),
(N'Bánh tráng 500g',       20000,  30000, 50,'2025-03-01','2026-03-01'),
(N'Bánh phở tươi 1kg',     25000,  35000, 40,'2025-03-02','2025-04-02'),

(N'Rau muống 1kg',         15000,  22000, 30,'2025-09-20','2025-09-27'),
(N'Rau cải ngọt 1kg',      16000,  23000, 25,'2025-09-21','2025-09-28'),
(N'Rau xà lách 1kg',       18000,  26000, 28,'2025-09-22','2025-09-29'),
(N'Cà chua 1kg',           20000,  28000, 40,'2025-09-22','2025-09-29'),
(N'Hành lá 1kg',           22000,  30000, 35,'2025-09-23','2025-09-30'),

(N'Táo Fuji 1kg',          40000,  55000, 60,'2025-09-18','2025-10-18'),
(N'Cam sành 1kg',          35000,  48000, 70,'2025-09-19','2025-10-19'),
(N'Chuối 1kg',             25000,  35000, 65,'2025-09-20','2025-09-30'),
(N'Nho đỏ 1kg',            80000, 100000, 25,'2025-09-21','2025-10-05'),
(N'Xoài cát 1kg',          50000,  70000, 30,'2025-09-22','2025-10-02'),

(N'Thịt heo ba rọi 1kg',   95000, 120000, 50,'2025-09-26','2025-10-02'),
(N'Thịt bò thăn 1kg',     180000, 210000, 40,'2025-09-26','2025-10-01'),
(N'Ức gà 1kg',             85000, 105000, 55,'2025-09-26','2025-10-03'),
(N'Cá basa phi lê 1kg',    70000,  90000, 45,'2025-09-25','2025-10-02'),
(N'Tôm sú 1kg',          180000, 210000, 35,'2025-09-25','2025-09-30'),

(N'Sữa tươi 1L',           25000,  35000, 80,'2025-09-20','2025-10-10'),
(N'Sữa chua 4 hộp',        30000,  45000, 75,'2025-09-21','2025-10-05'),
(N'Phô mai 200g',          45000,  60000, 30,'2025-09-22','2026-01-22'),
(N'Bơ lạt 200g',           50000,  70000, 25,'2025-09-22','2026-01-22'),
(N'Kem que 5 que',         20000,  30000, 40,'2025-09-23','2025-12-23'),

(N'Mì gói 30 gói',         80000, 100000, 90,'2025-02-10','2026-02-10'),
(N'Bún khô 500g',          15000,  22000, 80,'2025-03-01','2026-03-01'),
(N'Phở khô 500g',          18000,  26000, 70,'2025-03-05','2026-03-05'),
(N'Cháo ăn liền',          12000,  18000,100,'2025-04-01','2026-04-01'),
(N'Ngũ cốc 500g',          50000,  70000, 60,'2025-04-10','2026-04-10'),

(N'Nước khoáng 500ml',      3000,   5000,150,'2025-05-01','2027-05-01'),
(N'Nước ngọt lon',         10000,  15000,200,'2025-05-01','2026-05-01'),
(N'Cà phê hòa tan 20 gói', 70000,  95000, 90,'2025-05-02','2026-05-02'),
(N'Trà xanh 500ml',         8000,  12000,120,'2025-05-03','2026-05-03'),
(N'Bia lon 330ml',         12000,  17000,180,'2025-05-05','2026-05-05');

INSERT INTO PhieuNhap (MaND, GhiChu) VALUES
(1, N'Nhập kho thực phẩm khô đầu tháng'),
(2, N'Nhập trái cây tươi'),
(1, N'Nhập thịt hải sản'),
(3, N'Nhập sữa và sản phẩm từ sữa'),
(2, N'Nhập đồ uống các loại');

INSERT INTO ChiTietNhap (MaPhieuNhap, MaHang, SoLuong, DonGia) VALUES
(1, 1, 20, 60000),
(1, 3, 30, 15000),
(2,16, 15, 40000),
(3,21, 10, 95000),
(4,26, 25, 25000);

INSERT INTO PhieuXuat (MaND, GhiChu) VALUES
(4, N'Xuất cho cửa hàng bán lẻ A'),
(4, N'Xuất đơn online'),
(3, N'Xuất cho bếp ăn tập thể'),
(4, N'Xuất chương trình khuyến mãi'),
(4, N'Xuất hàng sỉ cho đại lý');

INSERT INTO ChiTietXuat (MaPhieuXuat, MaHang, SoLuong, DonGia) VALUES
(1, 1, 10, 80000),
(2,16,  5, 55000),
(3,21,  8,120000),
(4,26, 12, 35000),
(5,31, 20,100000);
