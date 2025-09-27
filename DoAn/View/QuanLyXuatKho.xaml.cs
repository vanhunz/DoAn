using DoAn.Model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DoAn
{
    public partial class QuanLyXuatKho : Window
    {
        public QuanLyXuatKho()
        {
            InitializeComponent();
            LoadData();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var thongKe = LayThongKe();

            txbl_TongDon.Text = thongKe.TongDon.ToString("N0");
            txbl_GiaTriXuat.Text = thongKe.GiaTriXuat.ToString("C0", new System.Globalization.CultureInfo("vi-VN"));
        }

        public class ThongKeXuatKho
        {
            public int TongDon { get; set; }
            public decimal GiaTriXuat { get; set; }
        }

        private ThongKeXuatKho LayThongKe()
        {
            using (var db = new QLThucPhamEntities())
            {
                var tk = new ThongKeXuatKho();

                tk.TongDon = db.PhieuXuat.Count();
                var tongGiaTri = db.ChiTietXuat
                    .Sum(ct => (decimal?)(ct.SoLuong * ct.DonGia));

                tk.GiaTriXuat = tongGiaTri ?? 0;
                return tk;
            }
        }

        private void LoadData()
        {
            using (var db = new QLThucPhamEntities())
            {
                var data = (from ct in db.ChiTietXuat
                            join px in db.PhieuXuat on ct.MaPhieuXuat equals px.MaPhieuXuat
                            join hh in db.HangHoa on ct.MaHang equals hh.MaHang
                            select new
                            {
                                MaDon = px.MaPhieuXuat,
                                TenHang = hh.TenHang,
                                SoLuong = ct.SoLuong,
                                GiaTri = ct.SoLuong * ct.DonGia,
                                NgayXuat = px.NgayXuat
                            }).ToList();

                DG_HienThi.ItemsSource = data;
            }
        }

        private void Btn_Sua_Click(object sender, RoutedEventArgs e)
{
    var donchon = DG_HienThi.SelectedItem;
    if (donchon == null)
    {
        MessageBox.Show("Hãy chọn đơn cần sửa");
        return;
    }

    using (var db = new QLThucPhamEntities())
    {
        dynamic dx = donchon;
        int maDon = Convert.ToInt32(dx.MaDon);

        var phieu = db.PhieuXuat.FirstOrDefault(p => p.MaPhieuXuat == maDon);
        var chitiet = db.ChiTietXuat.FirstOrDefault(c => c.MaPhieuXuat == maDon);

        if (phieu != null && chitiet != null)
        {
            // Ngày xuất
            DateTime ngayXuat = DateTime.Parse(txt_NgayXuat.Text);
            phieu.NgayXuat = ngayXuat;

            // Số lượng
            chitiet.SoLuong = int.Parse(txt_SoLuong.Text);

            // Lấy đơn giá từ bảng Hàng hóa
            int maHang = chitiet.MaHang;
            var donGia = db.HangHoa
                           .Where(h => h.MaHang == maHang)
                           .Select(h => h.GiaXuat)
                           .FirstOrDefault();

            chitiet.DonGia = donGia ?? 0;

            db.SaveChanges();
            MessageBox.Show("Sửa đơn hàng thành công");
             LoadData();
             CapNhatThongKe();
        }
    }
}

        private void Btn_Them_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new QLThucPhamEntities())
            {
                // 1. Tạo phiếu xuất mới
                var phieu = new PhieuXuat
                {
                    NgayXuat = DateTime.Parse(txt_NgayXuat.Text),
                    MaND = 1, // hoặc lấy từ user đăng nhập
                    GhiChu = "Thêm mới từ form"
                };
                db.PhieuXuat.Add(phieu);
                db.SaveChanges();

                // 2. Lấy thông tin hàng hóa để tính đơn giá
                var hang = db.HangHoa.FirstOrDefault(h => h.TenHang == txt_TenHang.Text);
                if (hang == null)
                {
                    MessageBox.Show("Không tìm thấy mặt hàng này trong cơ sở dữ liệu");
                    return;
                }

                // 3. Tạo chi tiết xuất
                var chitiet = new ChiTietXuat
                {
                    MaPhieuXuat = phieu.MaPhieuXuat,
                    MaHang = hang.MaHang,
                    SoLuong = int.Parse(txt_SoLuong.Text),
                    DonGia = hang.GiaXuat ?? 0
                };
                db.ChiTietXuat.Add(chitiet);
                db.SaveChanges();
            }
            LoadData();
            CapNhatThongKe();
        }

        private void Btn_Xoa_Click(object sender, RoutedEventArgs e)
        {
            var donchon = DG_HienThi.SelectedItem;
            if (donchon == null)
            {
                MessageBox.Show("Hãy chọn đơn cần xoá");
                return;
            }

            using (var db = new QLThucPhamEntities())
            {
                dynamic dx = donchon;
                int maDon = Convert.ToInt32(dx.MaDon);

                var chitiet = db.ChiTietXuat.Where(c => c.MaPhieuXuat == maDon).ToList();
                db.ChiTietXuat.RemoveRange(chitiet);

                var phieu = db.PhieuXuat.FirstOrDefault(p => p.MaPhieuXuat == maDon);
                if (phieu != null)
                    db.PhieuXuat.Remove(phieu);

                db.SaveChanges();
                MessageBox.Show("Xoá đơn hàng thành công");
            }

            LoadData();
            CapNhatThongKe();
        }

        private void DG_HienThi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var donchon = DG_HienThi.SelectedItem;
            if (donchon == null) return;

            dynamic dx = donchon;

            txt_MaDon.Text = dx.MaDon.ToString();
            txt_TenHang.Text = dx.TenHang;
            txt_SoLuong.Text = dx.SoLuong.ToString();
            txt_NgayXuat.Text = dx.NgayXuat.ToString("dd/MM/yyyy");
        }
        private void CapNhatThongKe()
        {
            var thongKe = LayThongKe();
            txbl_TongDon.Text = thongKe.TongDon.ToString("N0");
            txbl_GiaTriXuat.Text = thongKe.GiaTriXuat.ToString("C0", new System.Globalization.CultureInfo("vi-VN"));
        }
    }
}
