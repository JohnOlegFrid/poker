using poker.PokerGame;
using PokerClient.Center;
using PokerClient.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PokerClient.GUI
{
    /// <summary>
    /// Interaction logic for MainPanel.xaml
    /// </summary>
    public partial class MainPanel : UserControl
    {
        public MainPanel()
        {
            InitializeComponent();
            MainInfo.Instance.MainPanel = this;
            string rooms = "";
            foreach (Room room in MainInfo.Instance.RoomsToPlayObsever)
                rooms = " " + rooms + room.Id;
            //RoomsList.DataContext = MainInfo.Instance.RoomsToPlayObsever;
            MainInfo.Instance.RoomsToPlayObsever = filterByTypeAndSpect(MainInfo.Instance.RoomsToPlayObsever);
            RoomsList.ItemsSource = MainInfo.Instance.RoomsToPlayObsever;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Room selcted = (Room)RoomsList.SelectedItem;
            if (selcted == null) return;
            if (selcted.RoomWindow != null)
            {
                selcted.RoomWindow.Activate();
                return;
            }
            RoomWindow roomWindow = new RoomWindow(selcted);
            selcted.RoomWindow = roomWindow;
            roomWindow.Show();
            Service.Instance.RequestUpdateGame(selcted.Id + "");
        }

        private void newRoomButtton_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new CreateNewRoom();
            
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            Service.Instance.RequestAllRoomsToPlay();
        }

       

        private void filterByMaxNumOfPlayersButton_Click(object sender, RoutedEventArgs e)
        {
            int maxNumOfPlayers = 0;
            if (maxNumOfPlayersTextBox.Text.CompareTo("") == 0 || !MainInfo.Instance.isNumber(maxNumOfPlayersTextBox.Text))
                MessageBox.Show("Wrong input, Please Enter some value again.", "wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                try
                {
                    maxNumOfPlayers = int.Parse(maxNumOfPlayersTextBox.Text);
                }
                catch
                {
                    MessageBox.Show("Wrong input, Please Enter some value again.", "wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
              
                MainInfo.Instance.displayRoomsByMaxNumOfPlayers(maxNumOfPlayers);
            }
        }

        private void filterByMaxBuyInButton_Click(object sender, RoutedEventArgs e)
        {
            int maxBuyIn = 0;
            if (MaxBuyInTextBox.Text.CompareTo("") == 0 || !MainInfo.Instance.isNumber(MaxBuyInTextBox.Text))
                MessageBox.Show("Wrong input, Please Enter some value again.", "wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                maxBuyIn = int.Parse(MaxBuyInTextBox.Text);
             
                MainInfo.Instance.displayRoomsByMaxBuyIn(maxBuyIn);
            }
            filterByTypeAndSpect(MainInfo.Instance.RoomsToPlayObsever);
        }

        private void filterByBigBlindButton_Click(object sender, RoutedEventArgs e)
        {
            int bigBlind = 0;
            if (BigBlindTextBox.Text.CompareTo("") == 0 || !MainInfo.Instance.isNumber(BigBlindTextBox.Text))
                MessageBox.Show("Wrong input, Please Enter some value again.", "wrong input", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                bigBlind = int.Parse(BigBlindTextBox.Text);

                MainInfo.Instance.displayRoomsByBigBlind(bigBlind);
            }
        }

        public ObservableCollection<Room> filterByTypeAndSpect(ObservableCollection<Room> roomsList)
        {
            string roomsNoSpec = "";
            string roomsYesSpec = "";
            string roomsLimit = "";
            string roomsNoLimit = "";
            string roomsPotLimit = "";
            if (!noSpectatingCheckBox.IsChecked.Value)
            {
                
                for (int i = 0; i < roomsList.Count; i++)
                {
                    if (roomsList.ElementAt(i).Game.GamePreferences.AllowSpectating == false)
                    {
                        roomsNoSpec = roomsNoSpec + roomsList.ElementAt(i).print() + "\n";
                        roomsList.RemoveAt(i);
                        
                    }
                        
                }
            }
            if (!yesSpectatingCheckBox.IsChecked.Value)
            {
                
                for (int i = 0; i < roomsList.Count; i++)
                {
                    if (roomsList.ElementAt(i).Game.GamePreferences.AllowSpectating == true)
                    {
                        roomsYesSpec = roomsYesSpec + roomsList.ElementAt(i).print() + "\n";
                        roomsList.RemoveAt(i);
                    }
                        
                }
            }
            if (!limitCheckBox.IsChecked.Value)
            {
                
                for (int i=0; i < roomsList.Count; i++)
                {
                    if (roomsList.ElementAt(i).Game.GamePreferences.GameTypePolicy1 == GamePreferences.GameTypePolicy.LIMIT)
                        roomsList.RemoveAt(i);
                }
                
            }

            if (!noLimitCheckBox.IsChecked.Value)
            {
                
                for (int i=0; i < roomsList.Count; i++)
                {
                    if (roomsList.ElementAt(i).Game.GamePreferences.GameTypePolicy1 == GamePreferences.GameTypePolicy.NO_LIMIT)
                        roomsList.RemoveAt(i);
                }
            }
            if (!potLimitCheckBox.IsChecked.Value)
            {
                
                for (int i = 0; i < roomsList.Count; i++)
                {
                    if (roomsList.ElementAt(i).Game.GamePreferences.GameTypePolicy1 == GamePreferences.GameTypePolicy.POT_LIMIT)
                        roomsList.RemoveAt(i);
                }
            }
            MessageBox.Show("no Spec :\n " + roomsNoSpec+ "\n yes spec: \n"+roomsYesSpec);
            return roomsList;

        }
       
    }
}