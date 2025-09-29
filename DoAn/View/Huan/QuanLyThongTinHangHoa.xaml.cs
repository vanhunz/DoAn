using DoAn.Model;
using DoAn.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using ClosedXML.Excel;
using Microsoft.Win32;


namespace DoAn
{
    public partial class QuanLyThongTinHangHoa : Window
    {
        private int nguongTonKho = 10;
        private List<HangHoa> allData;
        private int currentPage = 1;
        private int itemsPerPage = 18;

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
                                   newHangHoa.GiaNhap,
                                   newHangHoa.GiaXuat,
                                   newHangHoa.SoLuongTon,
                                   newHangHoa.NSX,
                                   newHangHoa.HSD);

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
                ql.SuaHangHoa(selected.MaHang, selected.TenHang,
                               selected.GiaNhap, selected.GiaXuat,
                               selected.SoLuongTon, selected.NSX, selected.HSD);

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
                                 $"Giá nhập: {selected.GiaNhap}\n" +
                                 $"Giá xuất: {selected.GiaXuat}\n" +
                                 $"Số lượng tồn: {selected.SoLuongTon}\n" +
                                 $"NSX: {selected.NSX?.ToShortDateString()}\n" +
                                 $"HSD: {selected.HSD?.ToShortDateString()}";

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
        // button bên Trái
        private void BtnCanhBaoTonKho_Click(object sender, RoutedEventArgs e)
        {
            var ds = lvHangHoa.Items.OfType<Model.HangHoa>().ToList();
            var canhBao = ds.Where(h => h.SoLuongTon.HasValue && h.SoLuongTon.Value < nguongTonKho).ToList();

            if (canhBao.Any())
            {
                string msg = "⚠️ Các sản phẩm sắp hết hàng:\n" +
                             string.Join("\n", canhBao.Select(h => $"{h.TenHang} (Còn {h.SoLuongTon})"));
                MessageBox.Show(msg, "Cảnh báo tồn kho", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                MessageBox.Show("✅ Tất cả sản phẩm đủ số lượng.", "Cảnh báo tồn kho");
            }
        }
        private void BtnCanhBaoHSD_Click(object sender, RoutedEventArgs e)
        {
            var ds = lvHangHoa.Items.OfType<Model.HangHoa>().ToList();
            var today = DateTime.Now;

            var sapHetHan = ds.Where(h => h.HSD.HasValue && (h.HSD.Value - today).TotalDays <= 30 && h.HSD.Value > today).ToList();
            var hetHan = ds.Where(h => h.HSD.HasValue && h.HSD.Value <= today).ToList();

            StringBuilder msg = new StringBuilder();
            if (hetHan.Any())
            {
                msg.AppendLine("❌ Sản phẩm đã hết hạn:");
                msg.AppendLine(string.Join("\n", hetHan.Select(h => $"{h.TenHang} (HSD: {h.HSD:dd/MM/yyyy})")));
            }
            if (sapHetHan.Any())
            {
                msg.AppendLine("\n⚠️ Sản phẩm sắp hết hạn:");
                msg.AppendLine(string.Join("\n", sapHetHan.Select(h => $"{h.TenHang} (HSD: {h.HSD:dd/MM/yyyy})")));
            }

            if (msg.Length > 0)
                MessageBox.Show(msg.ToString(), "Cảnh báo HSD", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
                MessageBox.Show("✅ Không có sản phẩm nào hết hạn hoặc sắp hết hạn.", "Cảnh báo HSD");
        }
        private void BtnBaoCao_Click(object sender, RoutedEventArgs e)
        {
            var ds = lvHangHoa.Items.OfType<Model.HangHoa>().ToList();
            var today = DateTime.Now;

            int spSapHet = ds.Count(h => h.SoLuongTon.HasValue && h.SoLuongTon.Value < nguongTonKho);
            int spSapHetHan = ds.Count(h => h.HSD.HasValue && (h.HSD.Value - today).TotalDays <= 30);
            decimal tongGiaTri = ds.Sum(h => (h.SoLuongTon ?? 0) * (h.GiaNhap ?? 0));

            string msg = $"📊 Báo cáo nhanh:\n" +
                         $"- Sản phẩm sắp hết hàng: {spSapHet}\n" +
                         $"- Sản phẩm sắp hết hạn: {spSapHetHan}\n" +
                         $"- Giá trị tồn kho: {tongGiaTri:N0} VND";

            MessageBox.Show(msg, "Báo cáo nhanh", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void BtnXuat_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new Model.QLThucPhamEntities())
            {
                var ds = db.HangHoa.ToList();

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV file (*.csv)|*.csv";
                saveFileDialog.FileName = "HangHoaExport.csv";

                if (saveFileDialog.ShowDialog() == true)
                {
                    StringBuilder csv = new StringBuilder();
                    // Header dùng dấu ";" để Excel tự tách cột
                    csv.AppendLine("MaHang;TenHang;GiaNhap;GiaXuat;SoLuongTon;NSX;HSD");

                    foreach (var h in ds)
                    {
                        csv.AppendLine($"{h.MaHang};{h.TenHang};{h.GiaNhap};{h.GiaXuat};{h.SoLuongTon};{h.NSX:dd/MM/yyyy};{h.HSD:dd/MM/yyyy}");
                    }

                    File.WriteAllText(saveFileDialog.FileName, csv.ToString(), Encoding.UTF8);
                    MessageBox.Show($"✅ Đã xuất {ds.Count} sản phẩm ra file:\n{saveFileDialog.FileName}", "Xuất dữ liệu");
                }
            }
        }
        private void BtnNhap_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "CSV Files (*.csv)|*.csv|Excel Files (*.xlsx)|*.xlsx";

            if (dialog.ShowDialog() == true)
            {
                // ================================
                // 1. XỬ LÝ CSV
                // ================================
                if (dialog.FileName.EndsWith(".csv"))
                {
                    var lines = File.ReadAllLines(dialog.FileName, Encoding.UTF8);
                    using (var db = new Model.QLThucPhamEntities())
                    {
                        foreach (var line in lines.Skip(1)) // bỏ dòng tiêu đề
                        {
                            if (string.IsNullOrWhiteSpace(line)) continue;

                            var parts = line.Split(',');
                            if (parts.Length >= 7)
                            {
                                DateTime nsx, hsd;

                                // xử lý ngày tháng an toàn
                                DateTime.TryParseExact(parts[5], "dd/MM/yyyy", null,
                                    System.Globalization.DateTimeStyles.None, out nsx);
                                if (nsx == DateTime.MinValue)
                                    DateTime.TryParse(parts[5], out nsx);

                                DateTime.TryParseExact(parts[6], "dd/MM/yyyy", null,
                                    System.Globalization.DateTimeStyles.None, out hsd);
                                if (hsd == DateTime.MinValue)
                                    DateTime.TryParse(parts[6], out hsd);

                                var hh = new Model.HangHoa
                                {
                                    MaHang = int.Parse(parts[0]),
                                    TenHang = parts[1],
                                    GiaNhap = decimal.Parse(parts[2]),
                                    GiaXuat = decimal.Parse(parts[3]),
                                    SoLuongTon = int.Parse(parts[4]),
                                    NSX = nsx,
                                    HSD = hsd
                                };

                                // tránh trùng mã hàng
                                if (!db.HangHoa.Any(x => x.MaHang == hh.MaHang))
                                {
                                    db.HangHoa.Add(hh);
                                }
                            }
                        }

                        db.SaveChanges();
                        lvHangHoa.ItemsSource = db.HangHoa.ToList();
                    }
                    MessageBox.Show("✅ Đã nhập dữ liệu CSV và lưu vào database!", "Nhập dữ liệu");
                }

                // ================================
                // 2. XỬ LÝ EXCEL (XLSX)
                // ================================
                else if (dialog.FileName.EndsWith(".xlsx"))
                {
                    using (var workbook = new XLWorkbook(dialog.FileName))
                    {
                        var ws = workbook.Worksheet(1); // sheet đầu tiên
                        using (var db = new Model.QLThucPhamEntities())
                        {
                            foreach (var row in ws.RowsUsed().Skip(1)) // bỏ header
                            {
                                int maHang, soLuong;
                                decimal giaNhap, giaXuat;
                                DateTime nsx, hsd;

                                int.TryParse(row.Cell(1).GetValue<string>(), out maHang);
                                decimal.TryParse(row.Cell(3).GetValue<string>(), out giaNhap);
                                decimal.TryParse(row.Cell(4).GetValue<string>(), out giaXuat);
                                int.TryParse(row.Cell(5).GetValue<string>(), out soLuong);

                                // đọc ngày tháng an toàn
                                string nsxStr = row.Cell(6).GetValue<string>();
                                string hsdStr = row.Cell(7).GetValue<string>();

                                if (!DateTime.TryParseExact(nsxStr, "dd/MM/yyyy", null,
                                    System.Globalization.DateTimeStyles.None, out nsx))
                                {
                                    DateTime.TryParse(nsxStr, out nsx);
                                }

                                if (!DateTime.TryParseExact(hsdStr, "dd/MM/yyyy", null,
                                    System.Globalization.DateTimeStyles.None, out hsd))
                                {
                                    DateTime.TryParse(hsdStr, out hsd);
                                }

                                var hh = new Model.HangHoa
                                {
                                    MaHang = maHang,
                                    TenHang = row.Cell(2).GetValue<string>(),
                                    GiaNhap = giaNhap,
                                    GiaXuat = giaXuat,
                                    SoLuongTon = soLuong,
                                    NSX = nsx,
                                    HSD = hsd
                                };

                                if (!db.HangHoa.Any(x => x.MaHang == hh.MaHang))
                                {
                                    db.HangHoa.Add(hh);
                                }
                            }

                            db.SaveChanges();
                            lvHangHoa.ItemsSource = db.HangHoa.ToList();
                        }
                    }
                    MessageBox.Show("✅ Đã nhập dữ liệu Excel vào database! HÃY ẤN BUTTON QUAY LẠI ĐỂ CẬP NHẬT DATAGRID", "Nhập dữ liệu");
                }
            }
        }
    }
}
