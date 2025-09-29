using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;
using System.ComponentModel;   // <- thiếu cái này nên báo lỗi


namespace DoAn.ViewModel
{
    public class TheoDoiTonKhoViewModel : INotifyPropertyChanged
    {
        private Model.QLThucPhamEntities db = new Model.QLThucPhamEntities();

        public ObservableCollection<HangHoaRow> DanhSachSanPham { get; set; }

        private int _tongSanPham;
        public int TongSanPham
        {
            get => _tongSanPham;
            set { _tongSanPham = value; OnPropertyChanged(nameof(TongSanPham)); }
        }

        private int _sapHetHang;
        public int SapHetHang
        {
            get => _sapHetHang;
            set { _sapHetHang = value; OnPropertyChanged(nameof(SapHetHang)); }
        }

        private int _hetHang;
        public int HetHang
        {
            get => _hetHang;
            set { _hetHang = value; OnPropertyChanged(nameof(HetHang)); }
        }

        public TheoDoiTonKhoViewModel()
        {
            LoadData();
        }

        public void LoadData()
        {
            var list = db.HangHoa.ToList().Select(h => new HangHoaRow
            {
                MaHang = h.MaHang,
                TenHang = h.TenHang,
                GiaNhap = h.GiaNhap ?? 0,   // DECIMAL có thể nullable
                GiaXuat = h.GiaXuat ?? 0,   // DECIMAL có thể nullable
                SoLuong = h.SoLuongTon ?? 0,
                NSX = h.NSX,
                HSD = h.HSD,
                TrangThai = (h.SoLuongTon ?? 0) == 0 ? "Hết hàng"
                           : (h.SoLuongTon ?? 0) <= 5 ? "Sắp hết"
                           : "Còn hàng"
            });

            DanhSachSanPham = new ObservableCollection<HangHoaRow>(list);

            TongSanPham = DanhSachSanPham.Count;
            SapHetHang = DanhSachSanPham.Count(x => x.TrangThai == "Sắp hết");
            HetHang = DanhSachSanPham.Count(x => x.TrangThai == "Hết hàng");
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    public class HangHoaRow
    {
        public int MaHang { get; set; }
        public string TenHang { get; set; }
        public int SoLuong { get; set; }
        public string DonVi { get; set; }
        public string TrangThai { get; set; }
        public decimal GiaNhap { get; internal set; }
        public decimal GiaXuat { get; internal set; }
        public DateTime? NSX { get; internal set; }
        public DateTime? HSD { get; internal set; }
    }
}
