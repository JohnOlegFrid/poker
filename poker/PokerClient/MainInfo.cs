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
        private EditWindow editWindow = null;
        private Login login = null;
        private List<Room> roomsToPlay = null;
        ObservableCollection<Room> roomsToPlayObsever = null;
        private static readonly object syncRoot = new object();
        public static string key = "fvggtzYH675PiXpjK5fGuGhadAa5Sjb1G4hUQobzlls=";
        public static string iv = "2EPvpwkqNxcc4qmKlPv80cpNWuVu6ypjwhGGE5dceMI=";

        private static MainInfo instance;

        private MainInfo() {
            this.roomsToPlayObsever = new ObservableCollection<Room>();
        }

        public bool ConnectToServer(String ip)
        {
            if (client != null)
                return true;
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

        public static void CleanInfo()
        {
            instance.roomsToPlayObsever = new ObservableCollection<Room>();
            instance.roomsToPlay = null;
            instance.player = null;
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
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new MainInfo();
                        }
                    }
                }
                return instance;
            }
        }

        public Player Player { get { return player; } set { player = value; } }

        public MainWindow MainWindow { get { return mainWindow; } set { mainWindow = value; } }


        public Login Login { get { return login; } set { login = value; } }
        public List<Room> RoomsToPlay {
            get { return roomsToPlay; }
            set {
                UpdateRooms(value);
                Application.Current.Dispatcher.Invoke(() => { roomsToPlayObsever.Clear(); });
                    foreach (Room room in roomsToPlay)
                        Application.Current.Dispatcher.Invoke(() => { roomsToPlayObsever.Add(room); });                  
            } }

        public ObservableCollection<Room> RoomsToPlayObsever { get { return roomsToPlayObsever; } }

        public EditWindow EditWindow { get => editWindow; set => editWindow = value; }



        public Room FindRoomById(int roomId)
        {
            return roomsToPlay.Find(r => r.Id == roomId);
        }

        private void UpdateRooms(List<Room> rooms)
        {
            if (roomsToPlay == null) roomsToPlay = rooms;
            Room findRoom;
            foreach(Room room in rooms)
            {
                try
                {
                    findRoom = FindRoomById(room.Id);
                }
                catch {
                    roomsToPlay.Add(room);
                }
            }
        }

        public void Logout()
        {
           if(RoomsToPlay != null)
               RoomsToPlay.ForEach(r =>
                {
                    if (r.RoomWindow != null)
                        Application.Current.Dispatcher.Invoke(() => { r.RoomWindow.Close(); });
                });
            if (MainInfo.Instance.client != null)
                MainInfo.Instance.client.CloseConnection();
            MainInfo.Instance.client = null;
            Application.Current.Dispatcher.Invoke(() => { MainWindow.mainContentControl.Content = new Login(); });
            
        }

        public String getPlayerUsername()
        {
            return player.Username;
        }

        public String getPlayerEmail()
        {
            return player.Email;
        }
    }
}
