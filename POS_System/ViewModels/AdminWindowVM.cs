using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using POS_System.Models;
using POS_System.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;

namespace POS_System.ViewModels
{
    public partial class AdminWindowVM : ObservableObject
    {

        public Action CloseAction { get; set; }
        public Action MinimizeAction { get; set; }

        [ObservableProperty]
        ObservableCollection<OrderItem> orderItems;

        [ObservableProperty]
        ObservableCollection<User> users;
        [ObservableProperty]
        public int userId;
        [ObservableProperty]
        public string username;
        [ObservableProperty]
        public string password;
        [ObservableProperty]
        public bool isAdmin;

        [ObservableProperty]
        ObservableCollection<Product> products;
        [ObservableProperty]
        public int id;
        [ObservableProperty]
        public string name;
        [ObservableProperty]
        public decimal price;
        [ObservableProperty]
        public string type;
        [ObservableProperty]
        public int quantity;


        private User selectedUser;
        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                Username = selectedUser?.Username;
                Password = selectedUser?.Password;
                IsAdmin = selectedUser?.IsAdmin ?? false;
            }
        }



        [ObservableProperty]
        ObservableCollection<Order> orders;

        public AdminWindowVM()
        {
            LoadUser();
            LoadProduct();
            LoadOrders();
            LoadOrderItem();
        }

        [RelayCommand]

        public void InsertPerson()
        {
            User user = new User()
            {
                Username = Username,
                Password = Password,
                IsAdmin = IsAdmin,
            };
            using (var db = new PosDbContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
            LoadUser();
        }
        [RelayCommand]
        public void RemovePerson(User user)
        {
            using (var db = new PosDbContext())
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
            LoadUser();
        }
        [RelayCommand]
        public void EditPerson(User user)
        {
            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password))
            {
                user.Username = username;
                user.Password = password;
                user.IsAdmin = isAdmin;

                using (var db = new PosDbContext())
                {
                    db.Users.Update(user);
                    db.SaveChanges();
                }
                LoadUser();
            }
        }


        public void LoadUser()
        {
            using (var db = new PosDbContext())
            {
                List<User> list = db.Users.ToList();
                Users = new ObservableCollection<User>(list);
            }
        }

        private Product selectedProduct;
        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                Name = selectedProduct?.Name;
                Type = selectedProduct?.Type;
                Price = selectedProduct?.Price ?? 0;
                Quantity = selectedProduct?.Quantity ?? 0;
            }
        }

        [RelayCommand]

        public void InsertProduct()
        {
            Product product = new Product()
            {
                Name = Name,
                Type = Type,
                Price = Price,
                Quantity = Quantity,
            };
            using (var db = new PosDbContext())
            {
                db.Products.Add(product);
                db.SaveChanges();
            }
            LoadProduct();
        }
        [RelayCommand]
        public void RemoveProduct(Product product)
        {
            using (var db = new PosDbContext())
            {
                db.Products.Remove(product);
                db.SaveChanges();
            }
            LoadProduct();
        }
        [RelayCommand]
        public void EditProduct(Product product)
        {
            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Type))
            {
                product.Name = Name;
                product.Type = Type;
                product.Price = Price;
                product.Quantity = Quantity;

                using (var db = new PosDbContext())
                {
                    db.Products.Update(product);
                    db.SaveChanges();
                }
                LoadProduct();
            }
        }

        [RelayCommand]
        public void LogOut()
        {
            var window = new LoginWindow();
            window.Show();
            CloseAction();
        }

        [RelayCommand]
        public void MinimizeWindow()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        [RelayCommand]

        public void CloseWindow()
        {
            CloseAction();
        }

        public void LoadProduct()
        {
            using (var db = new PosDbContext())
            {
                List<Product> list = db.Products.ToList();
                Products = new ObservableCollection<Product>(list);
            }
        }

        public void LoadOrders()
        {
            using (var db = new PosDbContext())
            {
                List<Order> list = db.Orders.ToList();
                Orders = new ObservableCollection<Order>(list);
            }
        }

        public void LoadOrderItem()
        {
            using (var db = new PosDbContext())
            {
                List<OrderItem> list = db.OrderItems.ToList();
                OrderItems = new ObservableCollection<OrderItem>(list);
            }
        }
    }
}
