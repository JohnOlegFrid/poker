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
        private ObservableCollection<Room> roomsToPlayObsever = null;
        private MainPanel mainPanel = null;
        private static readonly object syncRoot = new object();
        public static string key = "fvggtzYH675PiXpjK5fGuGhadAa5Sjb1G4hUQobzlls=";
        public static string iv = "2EPvpwkqNxcc4qmKlPv80cpNWuVu6ypjwhGGE5dceMI=";

        private static MainInfo instance;

        private MainInfo() {
            this.RoomsToPlayObsever = new ObservableCollection<Room>();
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
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static void CleanInfo()
        {
            instance.RoomsToPlayObsever = new ObservableCollection<Room>();
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
                Application.Current.Dispatcher.Invoke(() => {
                    RoomsToPlayObsever.Clear();
                    foreach (Room room in roomsToPlay)
                        RoomsToPlayObsever.Add(room);
                    if (MainInfo.Instance.MainPanel != null)
                    {
                        MainInfo.Instance.MainPanel.filterByTypeAndSpect(RoomsToPlayObsever);
                    }
                
                });
                
                
            } }

        //public ObservableCollection<Room> RoomsToPlayObsever { get { return roomsToPlayObsever; } }

        public EditWindow EditWindow { get => editWindow; set => editWindow = value; }
        public ObservableCollection<Room> RoomsToPlayObsever { get => roomsToPlayObsever; set => roomsToPlayObsever = value; }
        public MainPanel MainPanel { get => mainPanel; set => mainPanel = value; }

        public Room FindRoomById(int roomId)
        {
            return roomsToPlay.Find(r => r.Id == roomId);
        }

        private void UpdateRooms(List<Room> rooms)
        {
            if (roomsToPlay == null) roomsToPlay = rooms;
            Room findRoom = null;
            foreach(Room room in rooms)
            {
                findRoom = FindRoomById(room.Id);
                    if (findRoom == null)
                        roomsToPlay.Add(room);
                    else
                        findRoom = null;  
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

        public void displayRoomsByMaxNumOfPlayers (int maxNumOfPlayers)
        {
            RoomsToPlayObsever.Clear();
            foreach (Room room in roomsToPlay)
                if (room.Game.GamePreferences.MaxPlayers==maxNumOfPlayers)
                    RoomsToPlayObsever.Add(room);


        }

        public void displayRoomsByMinNumOfPlayers(int minNumOfPlayers)
        {
            RoomsToPlayObsever.Clear();
            foreach (Room room in roomsToPlay)
                if (room.Game.GamePreferences.MinPlayers == minNumOfPlayers)
                    RoomsToPlayObsever.Add(room);


        }

        public void displayRoomsByMaxBuyIn(int maxBuyIn)
        {
            RoomsToPlayObsever.Clear();
            foreach (Room room in roomsToPlay)
                if (room.Game.GamePreferences.MaxBuyIn == maxBuyIn)
                    RoomsToPlayObsever.Add(room);


        }

        public void displayRoomsByMinBuyIn(int minBuyIn)
        {
            RoomsToPlayObsever.Clear();
            foreach (Room room in roomsToPlay)
                if (room.Game.GamePreferences.MinBuyIn == minBuyIn)
                    RoomsToPlayObsever.Add(room);


        }

        public void displayRoomsByBigBlind(int bigBlind)
        {
            RoomsToPlayObsever.Clear();
            foreach (Room room in roomsToPlay)
                if (room.Game.GamePreferences.BigBlind == bigBlind)
                    RoomsToPlayObsever.Add(room);


        }



        public bool isNumber(string input)
        {
            int num;
            return int.TryParse(input, out num);
        }
    }
}
