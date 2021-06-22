﻿using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using SwimmingStore.Components;
using SwimmingStore.Models;
using SwimmingStore.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SwimmingStore.Tests
{
    public class NavigationMenuViewComponentTest
    {
        [Fact]
        public void Can_Select_Categories()
        {
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();

            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductId = 1, Name = "P1", Category = "Apples"},
                new Product {ProductId = 1, Name = "P2", Category = "Apples"},
                new Product {ProductId = 1, Name = "P3", Category = "Plums"},
                new Product {ProductId = 1, Name = "P4", Category = "Oranges"},
            }).AsQueryable<Product>());

            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);

            string[] results = ((IEnumerable<string>)(target.Invoke()
                as ViewViewComponentResult).ViewData.Model).ToArray();

            Assert.True(Enumerable.SequenceEqual(new string[] { "Apples",
            "Oranges", "Plums"}, results));
        }
    }
}
