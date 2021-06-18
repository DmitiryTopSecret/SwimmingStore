using Microsoft.AspNetCore.Mvc;
using Moq;
using SwimmingStore.Controllers;
using SwimmingStore.Models;
using SwimmingStore.Models.Repository;
using SwimmingStore.Models.VIewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SwimmingStore.Tests
{
    public class HomControllerTest
    {
        [Fact]
        public void Can_Use_Repository()
        {
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();

            mock.Setup(m => m.Products).Returns((new Product[] 
            { 
                new Product {ProductId = 1, Name = "P1"},
                new Product {ProductId = 2, Name = "P2"}
            }).AsQueryable<Product>());

            HomeController controller = new HomeController(mock.Object);

            ProductsListViewModel result = controller.Index().ViewData.Model as ProductsListViewModel;

            Product[] prodArray = result.Products.ToArray();

            Assert.True(prodArray.Length == 2);
            Assert.Equal("P1", prodArray[0].Name);
            Assert.Equal("P2", prodArray[1].Name);
        }

        [Fact]
        public void Can_Paginate()
        {
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();

            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductId = 1, Name = "P1"},
                new Product {ProductId = 2, Name = "P2"},
                new Product {ProductId = 3, Name = "P3"},
                new Product {ProductId = 4, Name = "P4"},
                new Product {ProductId = 5, Name = "P5"},
            }).AsQueryable<Product>());

            HomeController controller = new HomeController(mock.Object);

            controller.PageSize = 3;

            ProductsListViewModel result = controller.Index(2).ViewData.Model as ProductsListViewModel;

            Product[] prodArray = result.Products.ToArray();

            Assert.True(prodArray.Length == 2);
            Assert.Equal("P4", prodArray[0].Name);
            Assert.Equal("P5", prodArray[1].Name);
        }

        [Fact]
        public void Can_Send_Pagination_VIew_Model()
        {
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();

            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductId = 1, Name = "P1"},
                new Product {ProductId = 2, Name = "P2"},
                new Product {ProductId = 3, Name = "P3"},
                new Product {ProductId = 4, Name = "P4"},
                new Product {ProductId = 5, Name = "P5"},
            }).AsQueryable<Product>());

            HomeController controller = new HomeController(mock.Object) { PageSize = 3 };

            ProductsListViewModel result = controller.Index(2).ViewData.Model as ProductsListViewModel;

            PagingInfo pageInfo = result.PagingInfo;

            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItem);
            Assert.Equal(2, pageInfo.TotalPages);
        }
    }
}
