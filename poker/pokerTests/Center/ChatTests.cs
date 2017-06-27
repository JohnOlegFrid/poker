using Microsoft.VisualStudio.TestTools.UnitTesting;
using poker.Center;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.Center.Tests
{
    [TestClass()]
    public class ChatTests
    {
        [TestMethod()]
        public void AddMessageTest()
        {
            Chat chat = new Chat();
            chat.AddMessage(new Message("yakir", "hello", true));
            Assert.AreEqual(1, chat.GetMessages().Count);
        }

        [TestMethod()]
        public void GetMessagesTest()
        {
            Chat chat = new Chat();
            Message msg1 = new Message("yakir", "hello", true);
            Message msg2 = new Message("yakir", "hello", true);
            chat.AddMessage(msg1);
            chat.AddMessage(msg2);
            Assert.AreEqual(msg1, chat.GetMessages()[0]);
            Assert.AreEqual(msg2, chat.GetMessages()[1]);
        }
    }
}