using PokerClient.Center;
using PokerClient.ServiceLayer;
using System;
using System.Collections.Generic;
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
          
            string rooms = "";
            foreach (Room room in MainInfo.Instance.RoomsToPlayObsever)
                rooms = " " + rooms + room.Id;
            //RoomsList.DataContext = MainInfo.Instance.RoomsToPlayObsever;
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
    }

    public class User
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string Mail { get; set; }
    }
}
