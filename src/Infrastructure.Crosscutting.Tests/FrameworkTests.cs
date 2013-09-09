using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using Infrastructure.Crosscutting.Logging;
using Infrustructure.Crosscutting.Framework;

namespace Infrastructure.Crosscutting.Tests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class FrameworkTests
    {
        public FrameworkTests()
        {
            
        }

        private static IKernel _kernel;
        private TestContext testContextInstance;

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
            _kernel = new StandardKernel(new DependencyResolver());
        }
        
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void CanInstantiateDefaultLoggerInstance()
        {
            // Arange

            // Act
            LoggerFactory.CreateLog().Debug("Message to be saved in the default log file.");

            // Assert
        }
    }
}
