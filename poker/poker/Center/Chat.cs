using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poker.Center
{
    public class Chat
    {
        private List<string[]> messages;

        public Chat()
        {
            this.messages = new List<string[]>();
        }

        public void AddMessage(string username, string msg)
        {
            messages.Add(new string[2] { username, msg });
        }

        public List<string[]> GetMessages()
        {
            return this.messages;
        }
    }
}
