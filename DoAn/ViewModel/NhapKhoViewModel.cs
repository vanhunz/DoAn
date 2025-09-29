using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.Windows.Controls;

namespace DoAn.ViewModel
{
    class NhapKhoViewModel
    {
        private Model.QLThucPhamEntities db = new Model.QLThucPhamEntities();

        public void LoadThucPham(DataGrid dg)
        {
            dg.ItemsSource = db.ChiTietNhap.ToList();
        }

        public void Them(Model.ChiTietNhap ct)
        {
            db.ChiTietNhap.Add(ct);
            db.SaveChanges();
        }

        public void Sua(Model.ChiTietNhap suaCT)
        {
            Model.ChiTietNhap ctn = db.ChiTietNhap.Find(suaCT.ID);
            if (ctn != null)
            {
                ctn.ID = suaCT.ID;
                ctn.MaPhieuNhap = suaCT.MaPhieuNhap;
                ctn.MaHang = suaCT.MaHang;
                ctn.SoLuong = suaCT.SoLuong;
                ctn.DonGia = suaCT.DonGia;
                db.SaveChanges();
            }
        }

        public void Xoa(Model.ChiTietNhap Xoact)
        {
            Model.ChiTietNhap ctn = db.ChiTietNhap.Find(Xoact.ID);
            if (ctn != null)
            {
                db.ChiTietNhap.Remove(ctn);
                db.SaveChanges();
            }
        }
    }
}
