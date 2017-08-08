using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoXUnitTest.Controllers;
using ToDoXUnitTest.Models;
using Xunit;
using Moq;
using ToDoXUnitTest.Models.Repositories;
using ToDoList.Controllers;

namespace ToDoList.Tests.ControllerTests
{
    public class ItemsControllerTest : IDisposable
    {
        Mock<IItemRepository> mock = new Mock<IItemRepository>();

        EFItemRepository db = new EFItemRepository(new TestDbContext());

        private void DbSetup()
        {
            mock.Setup(m => m.Items).Returns(new Item[]
            {
                new Item {ItemId = 1, Description = "Wash the dog", CategoryId = 1 },
                new Item {ItemId = 2, Description = "Do the dishes", CategoryId = 1 },
                new Item {ItemId = 3, Description = "Sweep the floor", CategoryId = 1 }
            }.AsQueryable());
        }
        [Fact]
        public void Mock_ViewResult_Index_Test()
        {
            //Arrange
            DbSetup();
            ItemsController controller = new ItemsController(mock.Object);

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsType<ViewResult>(result);
        }
            [Fact]
        public void Mock_ModelList_Index_Test()
        {
            //Arrange
            DbSetup();
            ViewResult indexView = new ItemsController(mock.Object).Index() as ViewResult;

            //Act
            var result = indexView.ViewData.Model;

            //Assert
            Assert.IsType<List<Item>>(result);
        }
        [Fact]
        public void Mock_MethodAddsItem_Test()
        {
            // Arrange
            DbSetup();
            ItemsController controller = new ItemsController(mock.Object);
            Item testItem = new Item();
            testItem.ItemId = 1;
            testItem.Description = "Wash the dog";
            testItem.CategoryId = 1;

            // Act

            ViewResult indexView = controller.Index() as ViewResult;
            var collection = indexView.ViewData.Model as IEnumerable<Item>;

            // Assert
            Assert.Contains<Item>(testItem, collection);
        }

        [Fact]
        public void DB_CreateNewEntry_Test()
        {
            // Arrange
            ItemsController controller = new ItemsController(db);
            Item testItem = new Item();
            testItem.Description = "TestDb Item";
            testItem.CategoryId = 1;

            // Act
            controller.Create(testItem);
            var collection = (controller.Index() as ViewResult).ViewData.Model as IEnumerable<Item>;

            // Assert
            Assert.Contains<Item>(testItem, collection);
        }

        public void Dispose()
        {

            db.ClearAll(db.Items);
        }
    }
}
