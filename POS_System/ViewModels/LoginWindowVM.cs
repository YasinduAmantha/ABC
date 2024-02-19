using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using POS_System.Models;
using POS_System.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace POS_System.ViewModels
{
    public partial class LoginWindowVM : ObservableObject
    {
        [ObservableProperty]
        public string username;

        [ObservableProperty]
        public string password;

        public Action closeWindow { get; set; }

        public readonly PosDbContext dbContext;

        public LoginWindowVM(PosDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public LoginWindowVM()
        {
        }

        [RelayCommand]
        public void MinimizeWindow()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        [RelayCommand]
        public void CloseWindow()
        {
            closeWindow();
        }

        [RelayCommand]
        private void Login()
        {
            var user = dbContext.Users.FirstOrDefault(u => u.Username == Username && u.Password == Password);

            if (user != null)
            {
                Window window;

                if (user.IsAdmin)
                {
                    window = new AdminWindow();
                }
                else
                {
                    window = new NormalUserWindow();
                }
                window.Show();
                Application.Current.MainWindow.Close();
                closeWindow();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
