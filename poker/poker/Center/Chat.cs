using System.Collections.ObjectModel;

namespace poker.Center
{
    public class Chat
    {
        private ObservableCollection<Message> messages;

        public Chat()
        {
            this.messages = new ObservableCollection<Message>();
        }

        public void AddMessage(string username, string msg, bool isActiveInGame)
        {
            messages.Add(new Message(username, msg, isActiveInGame));
        }

        public ObservableCollection<Message> GetMessages()
        {
            return this.messages;
        }
    }
}
