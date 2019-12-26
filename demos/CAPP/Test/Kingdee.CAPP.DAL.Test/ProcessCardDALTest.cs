using Kingdee.CAPP.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Kingdee.CAPP.Model;
using System.Data;

namespace Kingdee.CAPP.DAL.Test
{
    
    
    /// <summary>
    ///This is a test class for ProcessCardDALTest and is intended
    ///to contain all ProcessCardDALTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ProcessCardDALTest
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


        /// <summary>
        ///A test for AddProcessCard
        ///</summary>
        [TestMethod()]
        public void AddProcessCardTest()
        {
            //ProcessCardModule cardmodule = new ProcessCardModule();
            //cardmodule.Id = Guid.NewGuid();
            ////cardmodule.CardModule = P;
            //cardmodule.CreateBy = "zxf";
            //cardmodule.CreateTime = DateTime.Now;
            //cardmodule.DetailMapValues = "产品1，产品2，产品3，产品4，产品5";
            //cardmodule.TitleMapValues = "产品1，产品2，产品3，产品4，产品5";
            //cardmodule.FixedMapValues = "产品1，产品2，产品3，产品4，产品5";
            //cardmodule.CreateTime = DateTime.Now;
            //cardmodule.IsCheckout = false;
            //cardmodule.IsDelete = 0;
            //cardmodule.Name = "card11";
            

            //CardModuleXML card = new CardModuleXML(); // TODO: Initialize to an appropriate value

            //#region Cells
            //Cell cell1 = new Cell();
            //cell1.Id = 1;
            //cell1.Name = "cell1";
            //cell1.Width = 200;
            //cell1.CellType = "固定提示框";
            //cell1.Align = "居中";
            //cell1.Vertical = "middle";
            //cell1.BackGround = "black";
            //cell1.BorderColor = "red";
            //cell1.IsReadOnly = false;
            //cell1.Vertical = "middle";
            //cell1.BorderWidth = 2;
            //cell1.FontSize = "18";
            //cell1.FontStyle = "Bold";
            //cell1.FontFamily = "宋体";
            //cell1.ForeColor = "Green";
            //cell1.RowSpan = 3;
            //cell1.ColSpan = 4;
            //cell1.Content = "产品型号";


            //Cell cell2 = new Cell();
            //cell2.Id = 2;
            //cell2.Name = "cell1";
            //cell2.Width = 200;
            //cell2.CellType = "固定提示框";
            //cell2.Align = "居中";
            //cell2.Vertical = "middle";
            //cell2.BackGround = "black";
            //cell2.BorderColor = "red";
            //cell2.IsReadOnly = false;
            //cell2.Vertical = "middle";
            //cell2.BorderWidth = 2;
            //cell2.FontSize = "18";
            //cell2.FontStyle = "Bold";
            //cell2.FontFamily = "宋体";
            //cell2.ForeColor = "Green";
            //cell2.RowSpan = 3;
            //cell2.ColSpan = 4;
            //cell2.Content = "产品型号";

            //Cell cell3 = new Cell();
            //cell3.Id = 3;
            //cell3.Name = "cell1";
            //cell3.Width = 200;
            //cell3.CellType = "固定提示框";
            //cell3.Align = "居中";
            //cell3.Vertical = "middle";
            //cell3.BackGround = "black";
            //cell3.BorderColor = "red";
            //cell3.IsReadOnly = false;
            //cell3.Vertical = "middle";
            //cell3.BorderWidth = 2;
            //cell3.FontSize = "18";
            //cell3.FontStyle = "Bold";
            //cell3.FontFamily = "宋体";
            //cell3.ForeColor = "Green";
            //cell3.RowSpan = 3;
            //cell3.ColSpan = 4;
            //cell3.Content = "产品型号";


            //Cell cell4 = new Cell();
            //cell4.Id = 4;
            //cell4.Name = "cell1";
            //cell4.Width = 200;
            //cell4.CellType = "固定提示框";
            //cell4.Align = "居中";
            //cell4.Vertical = "middle";
            //cell4.BackGround = "black";
            //cell4.BorderColor = "red";
            //cell4.IsReadOnly = false;
            //cell4.Vertical = "middle";
            //cell4.BorderWidth = 2;
            //cell4.FontSize = "18";
            //cell4.FontStyle = "Bold";
            //cell4.FontFamily = "宋体";
            //cell4.ForeColor = "Green";
            //cell4.RowSpan = 3;
            //cell4.ColSpan = 4;
            //cell4.Content = "产品型号";

            //Cell cell5 = new Cell();
            //cell5.Id = 5;
            //cell5.Name = "cell1";
            //cell5.Width = 200;
            //cell5.CellType = "固定提示框";
            //cell5.Align = "居中";
            //cell5.Vertical = "middle";
            //cell5.BackGround = "black";
            //cell5.BorderColor = "red";
            //cell5.IsReadOnly = false;
            //cell5.Vertical = "middle";
            //cell5.BorderWidth = 2;
            //cell5.FontSize = "18";
            //cell5.FontStyle = "Bold";
            //cell5.FontFamily = "宋体";
            //cell5.ForeColor = "Green";
            //cell5.RowSpan = 3;
            //cell5.ColSpan = 4;
            //cell5.Content = "产品型号";
            //#endregion

            //#region Rows
            //Row row1 = new Row();
            //row1.Id = 1;
            //row1.Height = 30;
            //row1.BorderWidth = 2;
            //row1.BorderColor = "black";
            //row1.BackGround = "white";
            //row1.Cells = new Cell[] { cell1, cell2, cell3, cell4, cell5 };

            //Row row2 = new Row();
            //row2.Id = 2;
            //row2.Height = 30;
            //row2.BorderWidth = 2;
            //row2.BorderColor = "black";
            //row2.BackGround = "white";
            //row2.Cells = new Cell[] { cell1, cell2, cell3, cell4, cell5 };

            //Row row3 = new Row();
            //row3.Id = 3;
            //row3.Height = 30;
            //row3.BorderWidth = 2;
            //row3.BorderColor = "black";
            //row3.BackGround = "white";
            //row3.Cells = new Cell[] { cell1, cell2, cell3, cell4, cell5 };

            //Row row4 = new Row();
            //row4.Id = 4;
            //row4.Height = 30;
            //row4.BorderWidth = 2;
            //row4.BorderColor = "black";
            //row4.BackGround = "white";
            //row4.Cells = new Cell[] { cell1, cell2, cell3, cell4, cell5 };

            //Row row5 = new Row();
            //row5.Id = 5;
            //row5.Height = 30;
            //row5.BorderWidth = 2;
            //row5.BorderColor = "black";
            //row5.BackGround = "white";
            //row5.Cells = new Cell[] { cell1, cell2, cell3, cell4, cell5 };

            //Row row6 = new Row();
            //row6.Id = 6;
            //row6.Height = 30;
            //row6.BorderWidth = 2;
            //row6.BorderColor = "black";
            //row6.BackGround = "white";
            //row6.Cells = new Cell[] { cell1, cell2, cell3, cell4, cell5 };

            //Row row7 = new Row();
            //row7.Id = 7;
            //row7.Height = 30;
            //row7.BorderWidth = 2;
            //row7.BorderColor = "black";
            //row7.BackGround = "white";
            //row7.Cells = new Cell[] { cell1, cell2, cell3, cell4, cell5 };

            //Row row8 = new Row();
            //row8.Id = 8;
            //row8.Height = 30;
            //row8.BorderWidth = 2;
            //row8.BorderColor = "black";
            //row8.BackGround = "white";
            //row8.Cells = new Cell[] { cell1, cell2, cell3, cell4, cell5 };

            //Row row9 = new Row();
            //row9.Id = 9;
            //row9.Height = 30;
            //row9.BorderWidth = 2;
            //row9.BorderColor = "black";
            //row9.BackGround = "white";
            //row9.Cells = new Cell[] { cell1, cell2, cell3, cell4, cell5 };

            //Row row10 = new Row();
            //row10.Id = 10;
            //row10.Height = 30;
            //row10.BorderWidth = 2;
            //row10.BorderColor = "black";
            //row10.BackGround = "white";
            //row10.Cells = new Cell[] { cell1, cell2, cell3, cell4, cell5 };
            //#endregion

            //card.Id = 1;
            //card.Name = "螺丝钉卡片模板";
            //card.Height = 200;
            //card.BackGround = "white";
            //card.BorderColor = "red";
            //card.BorderWidth = 20;
            //card.CardRange = "A4";
            //card.CardDirection = "横向";
            //card.MarginLeft = "5";
            //card.MarginTop = "5";
            //card.MarginRight = "5";
            //card.MarginBottom = "5";
            //card.PrintScale = 120;
            //card.PrintAboveOffset = 5;
            //card.PrintUnderOffset = 10;
            //card.Rows = new Row[] { row1, row2, row3, row4, row5, row6, row7, row8, row9, row10 };


            ////int expected = 1; // TODO: Initialize to an appropriate value
            //cardmodule.CardModule = card;
            //object actual;
            //actual = ProcessCardModuleDAL.AddProcessCard(cardmodule);
            //Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for GetProcessCardDataset
        ///</summary>
        [TestMethod()]
        public void GetProcessCardDatasetTest()
        {
            Guid id = new Guid("1D1511D6-9465-439F-8EDF-F373E6F45292"); // TODO: Initialize to an appropriate value            
            DataSet actual;
            actual = ProcessCardModuleDAL.GetProcessCardDataset(id);
            Assert.IsTrue(actual.Tables[0].Rows.Count > 0);            
        }
    }
}
