using DoAn.Model;
using DoAn.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Windows;


namespace DoAn
{
    /// <summary>
    /// Interaction logic for QuanLyThongTinHangHoa.xaml
    /// </summary>
    public partial class QuanLyThongTinHangHoa : Window
    {
        private List<Model.HangHoa> allData;
        private int currentPage = 1;
        private int itemsPerPage = 4;

        public QuanLyThongTinHangHoa()
        {
            InitializeComponent();
            string projectFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\");
            string sourceImages = Path.Combine(projectFolder, "Images");

            CopyFolderToOutput(sourceImages);
            LoadHangHoa();
        }

        private void LoadHangHoa()
        {
            try
            {
                using (var db = new QLThucPhamEntities())
                {
                    allData = db.HangHoa.ToList();
                    LoadPage(currentPage);
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

        private void LoadPage(int page)
        {
            if (allData == null || allData.Count == 0) return;

            int totalItems = allData.Count;
            int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

            if (page < 1) page = 1;
            if (page > totalPages) page = totalPages;

            currentPage = page;

            var pageData = allData.Skip((currentPage - 1) * itemsPerPage)
                                  .Take(itemsPerPage)
                                  .ToList();

            lvHangHoa.ItemsSource = pageData;
            txtPage.Text = $"Trang {currentPage}/{totalPages}";
            btnPrev.IsEnabled = currentPage > 1;
            btnNext.IsEnabled = currentPage < totalPages;
        }

        private void BtnPrev_Click(object sender, RoutedEventArgs e)
        {
            LoadPage(currentPage - 1);
        }
        private void CopyFolderToOutput(string sourceFolder)
        {
            // Thư mục xuất ra: bin/Debug/Images
            string targetFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetFileName(sourceFolder));

            if (!Directory.Exists(sourceFolder))
            {
                MessageBox.Show($"Không tìm thấy folder ảnh gốc: {sourceFolder}");
                return;
            }

            // Nếu thư mục target chưa tồn tại thì tạo
            if (!Directory.Exists(targetFolder))
                Directory.CreateDirectory(targetFolder);

            // Copy tất cả file trong folder
            foreach (string file in Directory.GetFiles(sourceFolder, "*.*", SearchOption.AllDirectories))
            {
                string relativePath = file.Substring(sourceFolder.Length + 1); // đường dẫn relative
                string destPath = Path.Combine(targetFolder, relativePath);

                string destDir = Path.GetDirectoryName(destPath);
                if (!Directory.Exists(destDir))
                    Directory.CreateDirectory(destDir);

                File.Copy(file, destPath, true); // true = overwrite
            }
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            LoadPage(currentPage + 1);
        }
    }

}
