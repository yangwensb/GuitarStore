using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Application.MainBoundedContext.DTO;

namespace Application.MainBoundedContext.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class InventoryAppServiceTests
    {
        public InventoryAppServiceTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;
        private static IKernel _kernel;
        private IInventoryAppService _service;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext) 
        {
            _kernel = new StandardKernel(new Application.MainBoundedContext.DependencyResolver(),
                                         new Infrastructure.Data.MainBoundedContext.DependencyResolver());
        }
        
        // Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup() { }
        
        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize() 
        {
            _service = _kernel.Get<IInventoryAppService>();
        }
        
        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup() { }
        
        #endregion

        [TestMethod]
        public void InventoryAppServiceReturnPagedInventoryListDTO()
        {
            // Arrange
            int pageIndex = 0;
            int pageCount = 3;
            int total;
            string keywords = "III";

            // Act
            var page = _service.GetInventoryList(pageIndex, pageCount, out total, InventoryListDto.Fields.Model, true, keywords);

            // Accert
            Assert.IsTrue(page != null);
            Assert.IsTrue(page.Count() < pageCount);
            Assert.IsFalse(total == 0);
            Assert.IsInstanceOfType(page.ElementAt(0), typeof(InventoryListDto));
        }
    }
}
