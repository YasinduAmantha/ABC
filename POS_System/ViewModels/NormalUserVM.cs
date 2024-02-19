using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using POS_System.Models;
using POS_System.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace POS_System.ViewModels
{
    public partial class NormalUserVM : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<Product> products;
        [ObservableProperty]
        ObservableCollection<Product> cartProducts;
        [ObservableProperty]
        public Product selectedProduct;
        [ObservableProperty]
        public DateTime date;
        [ObservableProperty]
        public List<Product> orderItems;
        [ObservableProperty]
        public decimal totalAmount;

        public Action closeWindow { get; set; }


    public NormalUserVM()
        {
            LoadProducts();
            CartProducts = new ObservableCollection<Product>();
        }

        [RelayCommand]
        public void AddToCart()
        {
            if (SelectedProduct != null)
            {
                var existingProduct = CartProducts.FirstOrDefault(p => p.Id == SelectedProduct.Id);
                if (existingProduct != null)
                {
                    existingProduct.Quantity++;
                }
                else
                {
                    var newProduct = new Product
                    {
                        Id = SelectedProduct.Id,
                        Name = SelectedProduct.Name,
                        Price = SelectedProduct.Price,
                        Type = SelectedProduct.Type,
                        Quantity = 1
                    };
                    CartProducts.Add(newProduct);
                }
            }
        }

        [RelayCommand]
        public void RemoveFromCart(Product product)
        {
            if (product != null)
            {
                if (product.Quantity > 1)
                {
                    product.Quantity--;
                }
                else
                {
                    CartProducts.Remove(product);
                }
            }
        }

        [RelayCommand]
        public void Pay()
        {
            var total = 0;
            if (CartProducts.Count > 0)
            {
                using (var db = new PosDbContext())
                {
                    // Create a new order
                    var order = new Order
                    {
                        Date = DateTime.Now,
                        TotalAmount = UpdateTotalAmount(),
                    };
                    db.Orders.Add(order);


                    // Add order items
                    foreach (var cartProduct in CartProducts)
                    {
                        var orderItem = new OrderItem
                        {
                            Order = order,
                            ProductId = cartProduct.Id,
                            Quantity = cartProduct.Quantity,
                            Price = cartProduct.Price
                        };
                        db.OrderItems.Add(orderItem);

                        // Update product quantity in the database
                        var product = db.Products.Find(cartProduct.Id);
                        product.Quantity -= cartProduct.Quantity;
                    }
                    TotalAmount = order.TotalAmount;

                    // Save changes to the database
                    db.SaveChanges();

                    var TotalAmountWithTax = CalcSalesTax(TotalAmount);
                    // Reset cart
                    MessageBox.Show($"Total Products : {CartProducts.Count} \nTotal Amount (without Tax) : {TotalAmount} \nTotal Amount (with Tax) : {TotalAmountWithTax} \nOrder completed Successfully !! ");
                    CartProducts.Clear();
                    TotalAmount = 0;
                }
            }
            else
            {
                MessageBox.Show("Cart is empty.");
            }
        }




        [RelayCommand]
        public void IncreaseQuantity(Product product)
        {
            if (product != null)
            {
                product.Quantity++;
            }
        }

        [RelayCommand]
        public void DecreaseQuantity(Product product)
        {
            if (product != null && product.Quantity > 1)
            {
                product.Quantity--;
            }
        }

        [RelayCommand]
        public void LogOut()
        {
            var window = new LoginWindow();
            window.Show();
            closeWindow();
        }

        [RelayCommand]
        public void CloseWindow()
        {
            closeWindow();
        }

        public void LoadProducts()
        {
            using (var db = new PosDbContext())
            {
                List<Product> list = db.Products.ToList();
                Products = new ObservableCollection<Product>(list);
            }
        }

        public decimal CalcSalesTax(decimal num)
        {
            return Math.Round((num) * (1.12m), 1);
        }

        public decimal UpdateTotalAmount()
        {
            return CartProducts.Sum(p => p.Price * p.Quantity);
        }

    }
}
