using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using poker.PokerGame;
using poker.Data;
using poker.ServiceLayer;
using pokerTests;

namespace poker.Center.Tests
{
    [TestClass()]
    public class RoomTest
    {
        [TestMethod()]
        public void AddRoomTest()
        {
            ProgramList.InitData();
            Room room = new Room(100, new TexasGame(new GamePreferences(GamePreferences.GameTypePolicy.LIMIT,5,2,1,2,true,10)));
            Program.roomsData.AddRoom(room);
            Assert.IsNotNull(Program.roomsData.FindRoomById(100));
        }

        [TestMethod()]
        public void RemoveRoomTest()
        {
            ProgramList.InitData();
            Room room = new Room(100, new TexasGame(new GamePreferences(GamePreferences.GameTypePolicy.LIMIT, 5, 2, 1, 2, true, 10)));
            Program.roomsData.AddRoom(room);
            Program.roomsData.DeleteRoom(room);
            Assert.IsNull(Program.roomsData.FindRoomById(100));
        }


    }
}