using DoAn.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DoAn.ViewModel
{
    internal class QLHangHoa
    {
        public List<HangHoa> GetAll()
        {
            using (var db = new QLThucPhamEntities())
            {
                return db.HangHoa.ToList();
            }
        }

        public bool ThemHangHoa(string ten, string donVi, decimal? giaNhap, decimal? giaXuat, int? soLuongTon)
        {
            try
            {
                using (var db = new QLThucPhamEntities())
                {
                    HangHoa hh = new HangHoa()
                    {
                        TenHang = ten,
                        DonVi = donVi,
                        GiaNhap = giaNhap,
                        GiaXuat = giaXuat,
                        SoLuongTon = soLuongTon
                    };

                    db.HangHoa.Add(hh);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm: " + ex.Message);
                return false;
            }
        }

        public bool SuaHangHoa(int maHang, string ten, string donVi, decimal? giaNhap, decimal? giaXuat, int? soLuongTon)
        {
            try
            {
                using (var db = new QLThucPhamEntities())
                {
                    var hh = db.HangHoa.FirstOrDefault(x => x.MaHang == maHang);
                    if (hh == null) return false;

                    hh.TenHang = ten;
                    hh.DonVi = donVi;
                    hh.GiaNhap = giaNhap;
                    hh.GiaXuat = giaXuat;
                    hh.SoLuongTon = soLuongTon;

                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi sửa: " + ex.Message);
                return false;
            }
        }

        public bool XoaHangHoa(int maHang)
        {
            try
            {
                using (var db = new QLThucPhamEntities())
                {
                    var hh = db.HangHoa.FirstOrDefault(x => x.MaHang == maHang);
                    if (hh == null) return false;

                    db.HangHoa.Remove(hh);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi xóa: " + ex.Message);
                return false;
            }
        }

        public List<HangHoa> SapXepTheoGiaNhap()
        {
            using (var db = new QLThucPhamEntities())
            {
                return db.HangHoa.OrderBy(h => h.GiaNhap).ToList();
            }
        }

        public List<HangHoa> SapXepTheoGiaXuat()
        {
            using (var db = new QLThucPhamEntities())
            {
                return db.HangHoa.OrderBy(h => h.GiaXuat).ToList();
            }
        }

        public List<HangHoa> SapXepTheoSoLuong()
        {
            using (var db = new QLThucPhamEntities())
            {
                return db.HangHoa.OrderByDescending(h => h.SoLuongTon).ToList();
            }
        }

        public List<HangHoa> SapXepTheoTen()
        {
            using (var db = new QLThucPhamEntities())
            {
                return db.HangHoa.OrderBy(h => h.TenHang).ToList();
            }
        }
        public int TongSoLuongTon()
        {
            using (var db = new QLThucPhamEntities())
            {
                return db.HangHoa.Sum(h => h.SoLuongTon ?? 0);
            }
        }

        public int DemSanPham()
        {
            using (var db = new QLThucPhamEntities())
            {
                return db.HangHoa.Count();
            }
        }


    }
}
