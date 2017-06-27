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
    public class GameServiceTests
    {
        [TestMethod()]
        public void StartGameTestNotCrush()
        {
            ProgramList.InitData();
            String ans = Service.GetLastInstance().StartGame("1234");
            Assert.AreEqual("null", ans);
        }

    }
}