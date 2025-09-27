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

namespace DoAn.View
{
    /// <summary>
    /// Interaction logic for QuanLyNguoiDung.xaml
    /// </summary>
    public partial class QuanLyNguoiDung : Window
    {
        public QuanLyNguoiDung()
        {
            InitializeComponent();
        }
        ViewModel.NguoiDungModelView ndmd = new ViewModel.NguoiDungModelView();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ndmd.LoadND(dg_NguoiDung);
        }

        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {
            Model.NguoiDung nd = new Model.NguoiDung
            {
                TenDangNhap = txt_TenDangNhap.Text,
                MatKhau = pw_MK.Password,
                HoTen = txt_HoTen.Text,
                MaVaiTro = int.Parse(txt_MaVaiTro.Text),
                Email = txt_Email.Text,
                SoDienThoai = txt_SoDienThoai.Text,
                NgayTao = date_NgayTao.SelectedDate
            };
            ndmd.ThemNguoiDung(nd);
            ndmd.LoadND(dg_NguoiDung);
            MessageBox.Show("Thêm người dùng thành công");
        }

        private void Btn_Sua_Click(object sender, RoutedEventArgs e)
        {
            Model.NguoiDung ndchon = dg_NguoiDung.SelectedItem as Model.NguoiDung;
            if (ndchon == null)
            {
                MessageBox.Show("Hãy chọn người dùng cần sửa");
                return;
            }
            Model.NguoiDung nd = new Model.NguoiDung
            {
                MaND = ndchon.MaND,
                TenDangNhap = txt_TenDangNhap.Text,
                MatKhau = pw_MK.Password,
                HoTen = txt_HoTen.Text,
                MaVaiTro = int.Parse(txt_MaVaiTro.Text),
                Email = txt_Email.Text,
                SoDienThoai = txt_SoDienThoai.Text,
                NgayTao = date_NgayTao.SelectedDate
            };
            ndmd.SuaNguoiDung(nd);
            ndmd.LoadND(dg_NguoiDung);
            MessageBox.Show("Sửa người dùng thành công");
        }

        private void Btn_Xoa_Click(object sender, RoutedEventArgs e)
        {
            Model.NguoiDung ndchon = dg_NguoiDung.SelectedItem as Model.NguoiDung;
            if (ndchon == null)
            {
                MessageBox.Show("Hãy chọn người dùng cần xoá");
                return;
            }
            ndmd.XoaNguoiDung(ndchon);
            ndmd.LoadND(dg_NguoiDung);
            MessageBox.Show("Xoá người dùng thành công");
        }

        private void Dg_NguoiDung_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Model.NguoiDung nd = dg_NguoiDung.SelectedItem as Model.NguoiDung;
            if (nd == null) return;
            txt_MaNguoiDung.Text = nd.MaND.ToString();
            txt_TenDangNhap.Text = nd.TenDangNhap;
            pw_MK.Password = nd.MatKhau;
            txt_HoTen.Text = nd.HoTen;
            txt_MaVaiTro.Text = nd.MaVaiTro.ToString();
            txt_Email.Text = nd.Email;
            txt_SoDienThoai.Text = nd.SoDienThoai;
            date_NgayTao.SelectedDate = nd.NgayTao;
        }

        private void Btn_Add_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
