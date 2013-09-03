using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Domain.MainBoundedContext.Aggregates.InventoryAgg;
using Domain.Seedwork.Specification;
using System.Linq.Expressions;

namespace Infrastructure.Data.MainBoundedContext.Tests
{
    [TestClass]
    public class InventoryRepositoryTests
    {
        private static IKernel _kernel;
        private IInventoryRepository _repository;
        private TestContext _testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return _testContextInstance;
            }
            set
            {
                _testContextInstance = value;
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
            _kernel = new StandardKernel(new DependencyResolver());
        }

        // Use ClassCleanup to run code after all tests in a class have run
         [ClassCleanup()]
         public static void MyClassCleanup() { }
        
        // Use TestInitialize to run code before running each test 
        [TestInitialize()]
        public void MyTestInitialize()
        {
            _repository = _kernel.Get<IInventoryRepository>();
        }

        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
        }
        #endregion

        [TestMethod]
        public void InventoryRepositoryReturnMaterializedSetOfEntities()
        {
            // Arange

            // Act
            var list = _repository.GetAll();

            // Accert
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void InventoryRepositoryReturnMaterializedEntityById()
        {
            // Arrange
            var id = Guid.Parse("77f23bee-294a-49a0-b461-a3b732571c7d");

            // Act
            var entity = _repository.Get(id);

            // Accert
            Assert.IsNotNull(entity);
            Assert.AreEqual(id, entity.Id);
        }

        [TestMethod]
        public void InventoryRepositoryReturnMaterializedSetOfEntitiesWithAllObjectsAttached()
        {
            // Arange
            var typeId = Guid.Parse("ac80e617-1018-451a-916c-0f20c34b626d");

            // Act
            var list = _repository.GetAll();

            // Accert
            Assert.IsNotNull(list.First().Type);
            Assert.AreEqual(typeId, list.First().Type.Id);
        }

        [TestMethod]
        public void InventoryRepositoryReturnMaterializedEntityByIdWithAllObjectsAttachedTo()
        {
            // Arrange
            var id = Guid.Parse("77f23bee-294a-49a0-b461-a3b732571c7d");
            var typeId = Guid.Parse("ac80e617-1018-451a-916c-0f20c34b626d");

            // Act
            var entity = _repository.Get(id);

            // Accert
            Assert.IsNotNull(entity.Type);
            Assert.AreEqual(typeId, entity.Type.Id);
        }

        [TestMethod]
        public void InventoryRepositoryReturnSetOfMatchedEntities()
        {
            // Arrange
            string model = "RS III";
            ISpecification<Inventory> spec = InventorySpecification.InventoryByModel(model);

            // Act
            var list = _repository.AllMatching(spec);

            // Accert
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void InventoryRepositoryReturnSetOfFilteredEntities()
        {
            // Arrange
            string model = "RS III";
            Expression<Func<Inventory, bool>> filter = i => i.Model.ToLower() == model.ToLower();

            // Act
            var list = _repository.GetFiltered(filter);

            // Accert
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void InventoryRepositoryReturnSetOfPagedAndSortedEntities()
        {
            // Arrange
            int pageIndex = 0;
            int pageCount = 3;
            int total;
            bool ascending = false;
            Expression<Func<Inventory, string>> orderByExpression = i => i.Model;

            // Act
            var page = _repository.GetPaged<string>(pageIndex, pageCount,  out total, orderByExpression, ascending, null, false);

            // Accert
            Assert.IsTrue(page != null && page.Any());
            Assert.IsTrue(page.Count() < pageCount);
            Assert.IsFalse(total > 0);
        }

        [TestMethod]
        public void InventoryRepositoryReturnPagedInventoryList()
        {
            // Arrange
            int pageIndex = 0;
            int pageCount = 3;
            int total;
            string orderByExpression = "Id";
            string keywords = "III";

            // Act
            var page = _repository.GetInventoryList(pageIndex, pageCount, out total, orderByExpression, true, keywords);

            // Accert
            Assert.IsTrue(page != null);
            Assert.IsTrue(page.Count < pageCount);
            Assert.IsFalse(total == 0);
        }
    }
}
