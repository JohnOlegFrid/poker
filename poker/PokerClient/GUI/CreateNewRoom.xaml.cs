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
    /// Interaction logic for CreateNewRoom.xaml
    /// </summary>
    public partial class CreateNewRoom : UserControl
    {
        public enum GameTypePolicy { LIMIT, NO_LIMIT, POT_LIMIT };

        public CreateNewRoom()
        {
            InitializeComponent();
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            string maxPlayers = System.Convert.ToString(maxNumOfPlayersSlider.Value);
            string minPlayers = System.Convert.ToString(minNumOfPlayersSlider.Value);
            string maxBuyIn = maxBuyInTextBox.Text;
            string minBuyIn = minBuyInTextBox.Text;
            string allowSpec=allowSpectCheckBox.IsChecked.Value.ToString() ;
            string bigBlind = bigBlindTextBox.Text;
            string type = "LIMIT";
            Service.Instance.SendCreateRoom(type , maxPlayers, minPlayers, minBuyIn, maxBuyIn, allowSpec, bigBlind);
        }

        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            // MainInfo.Instance.MainWindow.OpenMainMenu();
            this.Content = new MainPanel();
            Service.Instance.RequestAllRoomsToPlay();
            
        }
    }
}
