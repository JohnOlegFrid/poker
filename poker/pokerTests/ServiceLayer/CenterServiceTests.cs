using Microsoft.VisualStudio.TestTools.UnitTesting;
using poker.ServiceLayer;
using pokerTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.ServiceLayer.Tests
{
    [TestClass()]
    public class CenterServiceTests
    {
        [TestMethod()]
        public void GetAllRoomsToPlayTestError()
        {
            ProgramList.InitData();
            String ans = Service.GetLastInstance().GetAllRoomsToPlay("yakar", "121212");
            Assert.AreEqual("null", ans);
        }
    }
}