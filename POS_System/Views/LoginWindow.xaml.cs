using POS_System.Models;
using POS_System.ViewModels;
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

namespace POS_System.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            LoginWindowVM vM = new LoginWindowVM(new PosDbContext());
            DataContext = vM;
            vM.closeWindow = () => Close();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Update the Password property of the view model when the password is changed
            if (DataContext is LoginWindowVM vm)
            {
                vm.Password = ((PasswordBox)sender).Password;
            }
        }
    }
}
