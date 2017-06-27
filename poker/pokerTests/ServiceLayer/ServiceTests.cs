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
    public class ServiceTests
    {
        [TestMethod()]
        public void RegisterTestNotCrush()
        {
            ProgramList.InitData();
            String ans = Service.GetLastInstance().Register(null, null, null);
        }

        [TestMethod()]
        public void LoginTestNotCrush()
        {
            ProgramList.InitData();
            String ans = Service.GetLastInstance().Login(null, null);
        }

        [TestMethod()]
        public void LoginWebTestNotCrush()
        {
            ProgramList.InitData();
            String ans = Service.GetLastInstance().LoginWeb(null, null);
            Assert.AreEqual(ans, "Error");
        }

        [TestMethod()]
        public void EditPlayerTestNotCrush()
        {
            ProgramList.InitData();
            String ans = Service.GetLastInstance().EditPlayer("yaki", "123", "21321", "2132");
            Assert.AreEqual(ans, "null");
        }

        [TestMethod()]
        public void SendMessageTestNotCrush()
        {
            ProgramList.InitData();
            String ans = Service.GetLastInstance().SendMessage("SAda", "12321", "1312", "213");
            Assert.AreEqual(ans, "null");
        }

        [TestMethod()]
        public void GetAllRoomsToPlayTestNotCrush()
        {
            ProgramList.InitData();
            String ans = Service.GetLastInstance().GetAllRoomsToPlay("asd", "123");
            Assert.AreEqual(ans, "null");
        }

        [TestMethod()]
        public void SitOnChairTestNotCrush()
        {
            ProgramList.InitData();
            String ans = Service.GetLastInstance().SitOnChair("123", "asd", "213", "1", "3");
            Assert.AreEqual(ans, "null");
        }

        [TestMethod()]
        public void AddPlayerToRoomTestNotCrush()
        {
            ProgramList.InitData();
            String ans = Service.GetLastInstance().AddPlayerToRoom("12", "qe", "1234a");
            Assert.AreEqual(ans, "null");
        }

        [TestMethod()]
        public void RemovePlayerFromRoomTestNotCrush()
        {
            ProgramList.InitData();
            String ans = Service.GetLastInstance().RemovePlayerFromRoom("213", "@31", "123");
            Assert.AreEqual(ans, "null");
        }

        [TestMethod()]
        public void StartGameTestNotCrush()
        {
            ProgramList.InitData();
            String ans = Service.GetLastInstance().StartGame("1231s");
            Assert.AreEqual(ans, "null");
        }

        [TestMethod()]
        public void UpdateGameTestNotCrush()
        {
            ProgramList.InitData();
            String ans = Service.GetLastInstance().UpdateGame("asdas");
            Assert.AreEqual(ans, "null");
        }

        [TestMethod()]
        public void AddChatMessageTestNotCrush()
        {
            ProgramList.InitData();
            String ans = Service.GetLastInstance().AddChatMessage("as", "asd", "123", "13", "asd");
            Assert.AreEqual(ans, "null");
        }

        [TestMethod()]
        public void AddFoldToGameTestNotCrush()
        {
            ProgramList.InitData();
            String ans = Service.GetLastInstance().AddFoldToGame("sad", "ASd");
            Assert.AreEqual(ans, "null");
        }

        [TestMethod()]
        public void AddCallToGameTestNotCrush()
        {
            ProgramList.InitData();
            String ans = Service.GetLastInstance().AddCallToGame("sad", "ASd");
            Assert.AreEqual(ans, "null");
        }

        [TestMethod()]
        public void AddCheckToGameTestNotCrush()
        {
            ProgramList.InitData();
            String ans = Service.GetLastInstance().AddCheckToGame("sad", "ASd");
            Assert.AreEqual(ans, "null");
        }

        [TestMethod()]
        public void AddRaiseToGameTestNotCrush()
        {
            ProgramList.InitData();
            String ans = Service.GetLastInstance().AddRaiseToGame("sad", "ASd");
            Assert.AreEqual(ans, "null");
        }

        [TestMethod()]
        public void UpdatePlayerInfoTestNotCrush()
        {
            ProgramList.InitData();
            String ans = Service.GetLastInstance().UpdatePlayerInfo("sadas", "sadsa", "asdsa");
            Assert.AreEqual(ans, "null");
        }

        [TestMethod()]
        public void CreateNewRoomTestNotCrush()
        {
            ProgramList.InitData();
            String ans = Service.GetLastInstance().CreateNewRoom("sd", "asd", "sd", "asd", "sad", "sda", "asd", "asd");
            Assert.AreEqual(ans, "null");
        }

        [TestMethod()]
        public void GetReplayTestNotCrush()
        {
            ProgramList.InitData();
            String ans = Service.GetLastInstance().GetReplay("dsfs");
            Assert.AreEqual(ans, "null");
        }
    }
}