using Microsoft.AspNetCore.Mvc;
using Moq;
using SwimmingStore.Controllers;
using SwimmingStore.Models;
using SwimmingStore.Models.Repository;
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

            IEnumerable<Product> result = 
                (controller.Index() as ViewResult).ViewData.Model
                as IEnumerable<Product>;

            Product[] prodArray = result.ToArray();

            Assert.True(prodArray.Length == 2);
            Assert.Equal("P1", prodArray[0].Name);
            Assert.Equal("P2", prodArray[1].Name);
        }
    }
}
