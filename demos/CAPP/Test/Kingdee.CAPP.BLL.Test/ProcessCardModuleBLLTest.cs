using Kingdee.CAPP.BLL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Kingdee.CAPP.Model;

namespace Kingdee.CAPP.BLL.Test
{  
    /// <summary>
    ///This is a test class for ProcessCardBLLTest and is intended
    ///to contain all ProcessCardBLLTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ProcessCardModuleBLLTest
    {


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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion
        [TestMethod()]
        public void GetProcessCardTest()
        {
            Guid id = new Guid("6E41805B-7A69-4A93-A16F-C1BB62C447FB"); // TODO: Initialize to an appropriate value            
            ProcessCardModuleBLL cardbll = new ProcessCardModuleBLL();
            ProcessCardModule module = cardbll.GetProcessCardModule(id);

            Assert.IsNotNull(module);
        }

        [TestMethod()]
        public void GetCardModuleTest()
        {
            //Guid id = new Guid("6E41805B-7A69-4A93-A16F-C1BB62C447FB"); // TODO: Initialize to an appropriate value            
            //CardModuleXML module = null;
            //ProcessCardModuleBLL cardbll = new ProcessCardModuleBLL();
            //module = cardbll.GetCardModule(id);            
            //Assert.IsNotNull(module);
        }
    }
}
