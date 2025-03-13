using FlowerShop.ViewModels;
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

namespace FlowerShop.Views.CodeBehind
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        LoginViewModel lvm = new LoginViewModel();
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = lvm;
        }

        // Masks the users password while typing it
        private void PB_password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel lvm)
            {
                lvm.Password = ((PasswordBox)sender).Password;
            }
        }
    }
}
