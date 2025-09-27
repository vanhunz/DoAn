using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DoAn.ViewModel
{
    class QuanLyXuatKho
    {
        Model.QLThucPhamEntities db = new Model.QLThucPhamEntities();

        public void ThemPhieuXuat(Model.PhieuXuat phieu, Model.ChiTietXuat chitiet, string tenHang)
        {
            // Thêm phiếu
            db.PhieuXuat.Add(phieu);
            db.SaveChanges();

            // Lấy mã hàng theo tên hàng
            var maHang = db.HangHoa
                           .Where(h => h.TenHang == tenHang)
                           .Select(h => h.MaHang)
                           .FirstOrDefault();
            if (maHang == 0)
                throw new Exception("Tên hàng không tồn tại");

            chitiet.MaPhieuXuat = phieu.MaPhieuXuat;
            chitiet.MaHang = maHang;

            db.ChiTietXuat.Add(chitiet);
            db.SaveChanges();
        }

        public void SuaPhieuXuat(int maDon, DateTime ngayXuat, int soLuong, decimal giaTri)
        {
            var phieu = db.PhieuXuat.FirstOrDefault(p => p.MaPhieuXuat == maDon);
            var chitiet = db.ChiTietXuat.FirstOrDefault(c => c.MaPhieuXuat == maDon);

            if (phieu != null && chitiet != null)
            {
                phieu.NgayXuat = ngayXuat;
                chitiet.SoLuong = soLuong;
                chitiet.DonGia = giaTri / soLuong;

                db.SaveChanges();
            }
        }

        public void XoaPhieuXuat(int maDon)
        {
            var chitiet = db.ChiTietXuat.Where(c => c.MaPhieuXuat == maDon).ToList();
            db.ChiTietXuat.RemoveRange(chitiet);

            var phieu = db.PhieuXuat.FirstOrDefault(p => p.MaPhieuXuat == maDon);
            if (phieu != null)
                db.PhieuXuat.Remove(phieu);

            db.SaveChanges();
        }

        public void LoadPhieu(DataGrid dg)
        {
            dg.ItemsSource = (from p in db.PhieuXuat
                              join c in db.ChiTietXuat on p.MaPhieuXuat equals c.MaPhieuXuat
                              join h in db.HangHoa on c.MaHang equals h.MaHang
                              select new
                              {
                                  MaDon = p.MaPhieuXuat,
                                  h.TenHang,
                                  c.SoLuong,
                                  GiaTri = c.SoLuong * c.DonGia,
                                  p.NgayXuat
                              }).ToList();
        }
    }
}

