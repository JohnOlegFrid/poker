using poker.PokerGame;
using PokerClient.Center;
using PokerClient.ServiceLayer;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
namespace PokerClient.GUI
{
    /// <summary>
    /// Interaction logic for Poker.xaml
    /// </summary>
    public partial class Poker : UserControl
    {
        public Chair[] chairs;
        public int activePlayers;
        public bool isSelectChair;
        public Room room;


        public Poker()
        {
            InitializeComponent();
            chairs = new Chair[8] { Chair0, Chair1, Chair2, Chair3, Chair4, Chair5, Chair6, Chair7 };
            activePlayers = 0;
            isSelectChair = false;
        }

   
        public void UpdateChairs()
        {
            for (int i = 0; i < ((TexasGame)room.Game).ChairsInGame.Length; i++)
            {
                chairs[i].poker = this;
                chairs[i].player = ((TexasGame)room.Game).ChairsInGame[i];
                chairs[i].Update();
            }
            for (int i = ((TexasGame)room.Game).GamePreferences.MaxPlayers; i < chairs.Length; i++)
            {
                this.Dispatcher.Invoke(() =>
                {
                    chairs[i].Visibility = Visibility.Hidden;
                });           
            }             
        }
        internal void UpdateGame()
        {
            UpdateChairs();
            this.Dispatcher.Invoke(() =>
            {               
                this.StartGameButton.Visibility = ((TexasGame)room.Game).Active ? Visibility.Hidden : Visibility.Visible;
                this.PotLabel.Content = ((TexasGame)room.Game).Pot + "$";
            });       
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            if(((TexasGame)room.Game).GamePreferences.GetMinPlayers() > ((TexasGame)room.Game).GetListActivePlayers().Count)
            {
                MessageBox.Show("Not enough players to start game!");
                return;
            }
            this.Dispatcher.Invoke(() =>
            {
                StartGameButton.IsEnabled = false;
            });       
            Service.Instance.StartGame(room.Id+"");
        }

    }
}
