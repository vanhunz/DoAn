using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DoAn
{
    /// <summary>
    /// Interaction logic for QuanLyNhapKho.xaml
    /// </summary>
    public partial class QuanLyNhapKho : Window
    {
        ViewModel.NhapKhoViewModel nhapKhoVM = new ViewModel.NhapKhoViewModel();
        public QuanLyNhapKho()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nhapKhoVM.LoadThucPham(DG_NhapKho);
        }

        private void Btn_Luu_Click(object sender, RoutedEventArgs e)
        {
            Model.ChiTietNhap ctNhap = new Model.ChiTietNhap
            {
                MaPhieuNhap = int.Parse(txt_MaPhieuNhap.Text),
                MaHang = int.Parse(txt_MaHangNhap.Text),
                SoLuong = int.Parse(txt_SoLuongNhap.Text),
                DonGia = decimal.Parse(txt_DonGiaNhap.Text)
            };
            nhapKhoVM.Them(ctNhap);
            nhapKhoVM.LoadThucPham(DG_NhapKho);
            MessageBox.Show("Thêm thành công");

        }

        private void Btn_Sua_Click(object sender, RoutedEventArgs e)
        {
            Model.ChiTietNhap ctn = DG_NhapKho.SelectedItem as Model.ChiTietNhap;
            if (ctn == null)
            {
                MessageBox.Show("chọn mục cần sửa!");
                return;
            }
            Model.ChiTietNhap ct = new Model.ChiTietNhap
            {
                ID = ctn.ID,
                MaPhieuNhap = int.Parse(txt_MaPhieuNhap.Text),
                MaHang = int.Parse(txt_MaHangNhap.Text),
                SoLuong = int.Parse(txt_SoLuongNhap.Text),
                DonGia = decimal.Parse(txt_DonGiaNhap.Text)
            };
            nhapKhoVM.Sua(ct);
            nhapKhoVM.LoadThucPham(DG_NhapKho);
            MessageBox.Show("Sửa thành công!");
        }

        private void Btn_Xoa_Click(object sender, RoutedEventArgs e)
        {
            Model.ChiTietNhap ctn = DG_NhapKho.SelectedItem as Model.ChiTietNhap;
            if (ctn == null)
            {
                MessageBox.Show("Hãy chọn thực phẩm cần xoá");
                return;
            }
            nhapKhoVM.Xoa(ctn);
            nhapKhoVM.LoadThucPham(DG_NhapKho);
            MessageBox.Show("Xoá Thực phẩm thành công");
        }

        private void DG_NhapKho_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Model.ChiTietNhap ct = DG_NhapKho.SelectedItem as Model.ChiTietNhap;
            if (ct == null) return;

            txt_MaPhieuNhap.Text = ct.MaPhieuNhap.ToString();
            txt_MaHangNhap.Text = ct.MaHang.ToString();
            txt_SoLuongNhap.Text = ct.SoLuong.ToString();
            txt_DonGiaNhap.Text = ct.DonGia.ToString();
        }
    }
}
