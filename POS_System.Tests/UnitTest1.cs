using FluentAssertions;
using POS_System.Models;
using POS_System.ViewModels;
using System.Windows;

namespace POS_System.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void CalcSalesTax_Should_ReturnCorrectValue_When_GivenDecimalNumber()
        {
            // Arrange
            var viewModel = new NormalUserVM();
            decimal num = 100;

            // Act
            var result = viewModel.CalcSalesTax(num);

            // Assert
            result.Should().Be(112); // Replace with the expected result based on your calculation
        }

        [Fact]
        public void UpdateTotalAmount_Should_ReturnCorrectValue_When_CartProductsHaveValidValues()
        {
            // Arrange
            var viewModel = new NormalUserVM();
            viewModel.CartProducts.Add(new Product { Price = 10, Quantity = 2 });
            viewModel.CartProducts.Add(new Product { Price = 5, Quantity = 3 });

            // Act
            decimal result = viewModel.UpdateTotalAmount();

            // Assert
            result.Should().Be((decimal)35.0); // Replace with the expected result based on your calculation
        }

        [Fact]
        public void TotalAmount_Should_ReturnCorrectValue_When_UpdateTotalAmountCalled()
        {
            // Arrange
            var viewModel = new NormalUserVM();
            viewModel.CartProducts.Add(new Product { Price = 10, Quantity = 2 });
            viewModel.CartProducts.Add(new Product { Price = 5, Quantity = 3 });

            // Act
            viewModel.TotalAmount = viewModel.UpdateTotalAmount();
            decimal result = viewModel.TotalAmount;

            // Assert
            result.Should().Be((decimal)35.0); // Replace with the expected result based on your calculation
        }
    } 
}