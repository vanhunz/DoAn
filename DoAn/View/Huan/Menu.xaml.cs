using System.Windows;
using DoAn.View;      
using DoAn.View.Huan;
namespace DoAn.View.Huan
{
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void MainWindow_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
        }

        private void QuanLyNguoiDung_Click(object sender, RoutedEventArgs e)
        {
            new QuanLyNguoiDung().Show();
        }

        private void QuanLyNhapKho_Click(object sender, RoutedEventArgs e)
        {
            new QuanLyNhapKho().Show();
        }

        private void QuanLyXuatKho_Click(object sender, RoutedEventArgs e)
        {
            new QuanLyXuatKho().Show();
        }

        private void QuanLyThongTinHangHoa_Click(object sender, RoutedEventArgs e)
        {
            new QuanLyThongTinHangHoa().Show();
        }

        private void TheoDoiTonKho_Click(object sender, RoutedEventArgs e)
        {
            new TheoDoiTonKho().Show();
        }

        private void AuthWindow_Click(object sender, RoutedEventArgs e)
        {
            new AuthWindow().Show();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
