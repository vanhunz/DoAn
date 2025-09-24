using DoAn.Model;
using DoAn.ViewModel;
using System;
using System.Linq;
using System.Windows;

namespace DoAn
{
    /// <summary>
    /// Interaction logic for QuanLyThongTinHangHoa.xaml
    /// </summary>
    public partial class QuanLyThongTinHangHoa : Window
    {
        public QuanLyThongTinHangHoa()
        {
            InitializeComponent();
            LoadHangHoa();
        }

        private void LoadHangHoa()
        {
            try
            {
                using (var db = new QLThucPhamEntities())
                {
                    var data = db.HangHoa.ToList();
                    lvHangHoa.ItemsSource = data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message,
                                "Thông báo",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }
    }
}
