using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientPoker.Center
{
    public class Chat
    {
        private List<string[]> messages;

        public Chat(List<string[]> messages)
        {
            this.messages = messages;
        }

        public List<string[]> GetMessages()
        {
            return this.messages;
        }
    }
