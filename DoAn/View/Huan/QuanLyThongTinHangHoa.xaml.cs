using DoAn.Model;
using DoAn.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DoAn
{
    public partial class QuanLyThongTinHangHoa : Window
    {
        private List<HangHoa> allData;
        private int currentPage = 1;
        private int itemsPerPage = 15;

        private QLHangHoa ql = new QLHangHoa();
        private bool isAdding = false;
        private HangHoa newHangHoa;

        public QuanLyThongTinHangHoa()
        {
            InitializeComponent();
            LoadHangHoa();
        }

        private void LoadHangHoa()
        {
            allData = ql.GetAll();
            LoadPagedData();
        }

        private void LoadPagedData()
        {
            if (allData == null) return;

            int totalItems = allData.Count;
            int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

            var pageData = allData
                .Skip((currentPage - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToList();

            lvHangHoa.ItemsSource = pageData;
            txtPage.Text = $"Trang {currentPage}/{totalPages}";
        }

        private void lvHangHoa_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            // Xử lý lưu khi edit trực tiếp
        }

        private void BtnThem_Click(object sender, RoutedEventArgs e)
        {
            if (!isAdding)
            {
                isAdding = true;
                newHangHoa = new HangHoa();
                var currentData = (lvHangHoa.ItemsSource as List<HangHoa>) ?? new List<HangHoa>();
                currentData.Insert(0, newHangHoa);
                lvHangHoa.ItemsSource = null;
                lvHangHoa.ItemsSource = currentData;

                lvHangHoa.SelectedItem = newHangHoa;
                lvHangHoa.ScrollIntoView(newHangHoa);
                lvHangHoa.CurrentCell = new DataGridCellInfo(newHangHoa, lvHangHoa.Columns[1]);
                lvHangHoa.BeginEdit();

                MessageBox.Show("Nhập dữ liệu vào dòng trống, sau đó ấn Thêm lần nữa để lưu.");
            }
            else
            {
                try
                {
                    if (newHangHoa == null)
                    {
                        MessageBox.Show("Không có dữ liệu để thêm.");
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(newHangHoa.TenHang))
                    {
                        MessageBox.Show("Tên hàng không được để trống.");
                        return;
                    }

                    ql.ThemHangHoa(newHangHoa.TenHang,
                                   newHangHoa.DonVi,
                                   newHangHoa.GiaNhap,
                                   newHangHoa.GiaXuat,
                                   newHangHoa.SoLuongTon);

                    MessageBox.Show("Thêm thành công!");
                    isAdding = false;
                    newHangHoa = null;
                    LoadHangHoa();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm: " + ex.Message);
                }
            }
        }

        private void BtnSua_Click(object sender, RoutedEventArgs e)
        {
            var selected = lvHangHoa.SelectedItem as HangHoa;
            if (selected == null)
            {
                MessageBox.Show("Vui lòng chọn 1 dòng để sửa.");
                return;
            }

            try
            {
                ql.SuaHangHoa(selected.MaHang, selected.TenHang, selected.DonVi,
                               selected.GiaNhap, selected.GiaXuat, selected.SoLuongTon);
                MessageBox.Show("Sửa thành công!");
                LoadHangHoa();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa: " + ex.Message);
            }
        }

        private void BtnXoa_Click(object sender, RoutedEventArgs e)
        {
            var selected = lvHangHoa.SelectedItem as HangHoa;
            if (selected == null)
            {
                MessageBox.Show("Vui lòng chọn 1 dòng để xóa!");
                return;
            }

            if (MessageBox.Show($"Bạn có chắc chắn muốn xóa [{selected.TenHang}] ?",
                                "Xác nhận",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (ql.XoaHangHoa(selected.MaHang))
                {
                    MessageBox.Show("Xóa thành công!");
                    LoadHangHoa();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại!");
                }
            }
        }

        private void BtnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadPagedData();
            }
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)allData.Count / itemsPerPage);
            if (currentPage < totalPages)
            {
                currentPage++;
                LoadPagedData();
            }
        }

        private void BtnSapXepGiaNhap_Click(object sender, RoutedEventArgs e)
        {
            allData = ql.SapXepTheoGiaNhap();
            currentPage = 1;
            LoadPagedData();
        }

        private void BtnSapXepGiaXuat_Click(object sender, RoutedEventArgs e)
        {
            allData = ql.SapXepTheoGiaXuat();
            currentPage = 1;
            LoadPagedData();
        }

        private void BtnSapXepSoLuong_Click(object sender, RoutedEventArgs e)
        {
            allData = ql.SapXepTheoSoLuong();
            currentPage = 1;
            LoadPagedData();
        }

        private void BtnSapXepTenHang_Click(object sender, RoutedEventArgs e)
        {
            allData = ql.SapXepTheoTen();
            currentPage = 1;
            LoadPagedData();
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            string keyword = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadHangHoa();
                return;
            }

            allData = ql.GetAll()
                        .Where(h => h.TenHang.Contains(keyword))
                        .ToList();
            currentPage = 1;
            LoadPagedData();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchProducts();
        }

        private void SearchProducts()
        {
            string keyword = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadHangHoa();
                return;
            }

            allData = ql.GetAll()
                        .Where(h => h.TenHang.Contains(keyword))
                        .ToList();
            currentPage = 1;
            LoadPagedData();
        }

        private void lvHangHoa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = lvHangHoa.SelectedItem as HangHoa;
            if (selected != null)
            {
                lblDetail.Text = $"Mã: {selected.MaHang}\n" +
                                 $"Tên: {selected.TenHang}\n" +
                                 $"Đơn vị: {selected.DonVi}\n" +
                                 $"Giá nhập: {selected.GiaNhap}\n" +
                                 $"Giá xuất: {selected.GiaXuat}\n" +
                                 $"Số lượng tồn: {selected.SoLuongTon}";

                productDetailPanel.Visibility = Visibility.Visible;
            }
        }
        //Thông kê
        private void btnThongKe_Click(object sender, RoutedEventArgs e)
        {
            QLHangHoa ql = new QLHangHoa();
            int tongSL = ql.TongSoLuongTon();
            int soSP = ql.DemSanPham();

            txtThongKe.Text = $"Tổng số sản phẩm: {soSP}\n" +
                              $"Tổng số lượng tồn: {tongSL}";
        }

        private void BtnCloseDetail_Click(object sender, RoutedEventArgs e)
        {
            productDetailPanel.Visibility = Visibility.Collapsed;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = "";
            productDetailPanel.Visibility = Visibility.Collapsed;
            LoadHangHoa();
        }
    }
}
