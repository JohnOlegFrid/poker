using PokerClient.Center;
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
            RoomsList.DataContext = MainInfo.Instance.RoomsToPlayObsever;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Room selcted = (Room)RoomsList.SelectedItem;
            if (selcted.RoomWindow != null)
            {
                selcted.RoomWindow.Activate();
                return;
            }
            RoomWindow roomWindow = new RoomWindow(selcted);
            selcted.RoomWindow = roomWindow;
            
            roomWindow.Show();
        }
    }
}
