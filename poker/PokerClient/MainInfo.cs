using PokerClient.Center;
using PokerClient.Communication;
using PokerClient.Players;
using PokerClient.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows;

namespace PokerClient
{
    public class MainInfo
    {
        private Client client = null;
        private Player player = null;
        private MainWindow mainWindow = null;
        private Login login = null;
        private List<Room> roomsToPlay = null;
        ObservableCollection<Room> roomsToPlayObsever = null;

        private static MainInfo instance;

        private MainInfo() {
            this.roomsToPlayObsever = new ObservableCollection<Room>();
        }

        public bool ConnectToServer()
        {
            if (client != null)
                return true;
            String ip = Client.GetLocalIPAddress();
            int port = 5555;
            try
            {
                client = new Client(ip, port);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public void SendMessage(Command command)
        {
            client.SendMessage(command);
        }

        public static MainInfo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainInfo();
                }
                return instance;
            }
        }

        public Player Player { get { return player; } set { player = value; } }

        public MainWindow MainWindow { get { return mainWindow; } set { mainWindow = value; } }


        public Login Login { get => login; set => login = value; }
        public List<Room> RoomsToPlay { get { return roomsToPlay; }
            set {
                roomsToPlay = value;
                Application.Current.Dispatcher.Invoke(() => { roomsToPlayObsever.Clear(); });
                    foreach (Room room in roomsToPlay)
                    Application.Current.Dispatcher.Invoke(() => { roomsToPlayObsever.Add(room); });                  
            } }

        public ObservableCollection<Room> RoomsToPlayObsever { get { return roomsToPlayObsever; } }
    }
}
