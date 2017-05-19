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
