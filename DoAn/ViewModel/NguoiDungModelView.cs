using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DoAn.ViewModel
{
    class NguoiDungModelView
    {
        Model.QLThucPhamEntities db = new Model.QLThucPhamEntities();
        public void ThemNguoiDung(Model.NguoiDung nd)
        {
            db.NguoiDung.Add(nd);
            db.SaveChanges();
        }
        public void XoaNguoiDung(Model.NguoiDung ndXoa)
        {
            Model.NguoiDung nd = db.NguoiDung.Find(ndXoa.MaND);
            if (nd != null)
            {
                db.NguoiDung.Remove(nd);
                db.SaveChanges();
            }
        }
        public void SuaNguoiDung(Model.NguoiDung ndCapNhat)
        {
            Model.NguoiDung nd = db.NguoiDung.Find(ndCapNhat.MaND);
            if (nd != null)
            {
                nd.MaND = ndCapNhat.MaND;
                nd.TenDangNhap = ndCapNhat.TenDangNhap;
                nd.MatKhau = ndCapNhat.MatKhau;
                nd.HoTen = ndCapNhat.HoTen;
                nd.MaVaiTro = ndCapNhat.MaVaiTro;
                nd.Email = ndCapNhat.Email;
                nd.SoDienThoai = ndCapNhat.SoDienThoai;
                nd.NgayTao = ndCapNhat.NgayTao;
                db.SaveChanges();
            }
        }
        public void LoadND(DataGrid dg)
        {
            dg.ItemsSource = db.NguoiDung.ToList();
        }
        public List<Model.NguoiDung> TimKiemNguoiDung(int maND)
        {
            return db.NguoiDung.Where(nd => nd.MaND == maND).ToList();
        }
    }
}
