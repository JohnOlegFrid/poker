using System.Collections.ObjectModel;

namespace PokerClient.Center
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

        public void AddMessage(Message msg)
        {
            messages.Add(msg);
        }

        public ObservableCollection<Message> GetMessages()
        {
            return this.messages;
        }
    }
}
