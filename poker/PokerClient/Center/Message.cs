namespace PokerClient.Center
{
    public class Message
    {
        private string username;
        private string msg;
        private bool isPlayerActiveInGame;

        public Message(string username, string msg, bool isPlayerActiveInGame)
        {
            this.username = username;
            this.msg = msg;
            this.isPlayerActiveInGame = isPlayerActiveInGame;
        }

        public string Username { get { return username; } set { username = value; } }
        public string Msg { get { return msg; } set { msg = value; } }
        public bool IsPlayerActiveInGame { get { return isPlayerActiveInGame; } set { isPlayerActiveInGame = value; } }

        public override string ToString()
        {
            return "MSG: " + msg + " From: " + username + " IsActive: " + isPlayerActiveInGame;
        }
    }
}