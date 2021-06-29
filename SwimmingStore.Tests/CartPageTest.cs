﻿ using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Moq;
using SwimmingStore.Models;
using SwimmingStore.Models.Repository;
using SwimmingStore.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace SwimmingStore.Tests
{
    public class CartPageTest
    {
        [Fact]
        public void Can_Load_Cart()
        {
            Product p1 = new Product { ProductId = 1, Name = "P1" };
            Product p2 = new Product { ProductId = 2, Name = "P2" };

            Mock<IStoreRepository> mockRepo = new Mock<IStoreRepository>();

            mockRepo.Setup(m => m.Products).Returns((new Product[]
            {
                p1, p2
            }).AsQueryable<Product>());

            Cart testCart = new Cart();

            testCart.AddItem(p1, 2);
            testCart.AddItem(p2, 1);

            CartModel cartModel = new CartModel(mockRepo.Object, testCart);

            cartModel.OnGet("myUrl");

            Assert.Equal(2, cartModel.Cart.Lines.Count());
            Assert.Equal("myUrl", cartModel.ReturnUrl);
        }

        [Fact]
        public void Can_Update_Cart()
        {
            Mock<IStoreRepository> mockRepo = new Mock<IStoreRepository>();
            mockRepo.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductId = 1, Name="P1"}
            }).AsQueryable<Product>());

            Cart testCart = new Cart();

            CartModel cartModel = new CartModel(mockRepo.Object, testCart);

            cartModel.OnPost(1, "myUrl");

            Assert.Single(testCart.Lines);
            Assert.Equal("P1", testCart.Lines.First().Product.Name);
            Assert.Equal(1, testCart.Lines.First().Quantity);
        }
    }
}
