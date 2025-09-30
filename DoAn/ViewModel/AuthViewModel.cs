using DoAn.Model;          // EF generated model/context
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DoAn.ViewModel
{
    public class AuthViewModel : BaseViewModel
    {
        private readonly QLThucPhamEntities _db = new QLThucPhamEntities();

        private string _currentPage = "Login";
        public string CurrentPage
        {
            get => _currentPage;
            set { _currentPage = value; OnPropertyChanged(); }
        }

        // --- Login ---
        public string LoginUser { get; set; }
        public string LoginPass { get; set; }
        public string ForgotUser { get; set; }
        public string ForgotContact { get; set; } 
        public string NewPass { get; set; }
        public string ConfirmNewPass { get; set; }
        private bool _showResetFields;
        public bool ShowResetFields
        {
            get => _showResetFields;
            set { _showResetFields = value; OnPropertyChanged(); }
        }

        // --- Register ---
        public string RegUser { get; set; }
        public string RegPass { get; set; }
        public string RegFullName { get; set; }
        public string RegEmail { get; set; }
        public string RegPhone { get; set; }

        // Commands
        public ICommand LoginCommand { get; }
        public ICommand ShowForgotCommand { get; }
        public ICommand CheckForgotCommand { get; }
        public ICommand ResetPasswordCommand { get; }
        public ICommand ShowRegisterCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand ShowLoginCommand { get; }

        public AuthViewModel()
        {
            LoginCommand = new RelayCommand(_ => DoLogin());
            ShowForgotCommand = new RelayCommand(_ => { CurrentPage = "Forgot"; ShowResetFields = false; });
            CheckForgotCommand = new RelayCommand(_ => DoCheckForgot());
            ResetPasswordCommand = new RelayCommand(_ => DoResetPassword());
            ShowRegisterCommand = new RelayCommand(_ => { CurrentPage = "Register"; ShowResetFields = false; });
            RegisterCommand = new RelayCommand(_ => DoRegister());
            ShowLoginCommand = new RelayCommand(_ => { CurrentPage = "Login"; ShowResetFields = false; });
        }

        private void DoLogin()
        {
            try
            {
                var user = _db.NguoiDung
                              .FirstOrDefault(u => u.TenDangNhap == LoginUser && u.MatKhau == LoginPass);

                if (user != null)
                {
                    // Mở Menu.xaml
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        var menu = new DoAn.View.Menu();
                        menu.Show();

                        foreach (Window w in Application.Current.Windows)
                        {
                            if (w is DoAn.View.AuthWindow)
                            {
                                w.Close();
                                break;
                            }
                        }
                    });
                }
                else
                {
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!",
                                    "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đăng nhập: " + ex.Message);
            }
        }


        private void DoCheckForgot()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ForgotUser) || string.IsNullOrWhiteSpace(ForgotContact))
                {
                    MessageBox.Show("Nhập tên đăng nhập và email hoặc số điện thoại để xác thực.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var user = _db.NguoiDung
                              .FirstOrDefault(u => u.TenDangNhap == ForgotUser &&
                                      (u.Email == ForgotContact || u.SoDienThoai == ForgotContact));
                if (user != null)
                {
                    ShowResetFields = true;
                    MessageBox.Show("Xác thực thành công. Hãy nhập mật khẩu mới.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Thông tin không khớp!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xác thực: " + ex.Message);
            }
        }

        private void DoResetPassword()
        {
            try
            {
                if (!ShowResetFields)
                {
                    MessageBox.Show("Bạn cần xác thực trước khi đổi mật khẩu.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(NewPass) || string.IsNullOrWhiteSpace(ConfirmNewPass))
                {
                    MessageBox.Show("Nhập mật khẩu mới và xác nhận.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (NewPass != ConfirmNewPass)
                {
                    MessageBox.Show("Mật khẩu nhập lại không trùng khớp.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var user = _db.NguoiDung.FirstOrDefault(u => u.TenDangNhap == ForgotUser);
                if (user == null)
                {
                    MessageBox.Show("Người dùng không tồn tại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                user.MatKhau = NewPass;
                _db.SaveChanges();
                MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                // quay lại login
                CurrentPage = "Login";
                ShowResetFields = false;
                NewPass = ConfirmNewPass = string.Empty;
                ForgotUser = ForgotContact = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đổi mật khẩu: " + ex.Message);
            }
        }

        private void DoRegister()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(RegUser) || string.IsNullOrWhiteSpace(RegPass))
                {
                    MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (_db.NguoiDung.Any(u => u.TenDangNhap == RegUser))
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var nd = new NguoiDung
                {
                    TenDangNhap = RegUser,
                    MatKhau = RegPass,
                    HoTen = RegFullName,
                    Email = RegEmail,
                    SoDienThoai = RegPhone,
                    NgayTao = DateTime.Now,
                    MaVaiTro = 1 // theo yêu cầu bạn muốn mặc định là 1
                };

                _db.NguoiDung.Add(nd);
                _db.SaveChanges();

                MessageBox.Show("Đăng ký thành công! Hãy đăng nhập.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                // reset form và chuyển về Login
                RegUser = RegPass = RegFullName = RegEmail = RegPhone = string.Empty;
                CurrentPage = "Login";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đăng ký: " + ex.Message);
            }
        }
    }
}
