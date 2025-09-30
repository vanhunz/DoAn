using System.Windows;
using System.Windows.Controls;
using DoAn.ViewModel;

namespace DoAn.View
{
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void LoginPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is AuthViewModel vm)
                vm.LoginPass = ((PasswordBox)sender).Password;
        }

        private void RegisterPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is AuthViewModel vm)
                vm.RegPass = ((PasswordBox)sender).Password;
        }

        private void NewPassBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is AuthViewModel vm)
                vm.NewPass = ((PasswordBox)sender).Password;
        }

        private void ConfirmPassBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is AuthViewModel vm)
                vm.ConfirmNewPass = ((PasswordBox)sender).Password;
        }
    }
}
