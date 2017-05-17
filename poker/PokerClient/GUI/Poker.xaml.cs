using poker.PokerGame;
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
    /// Interaction logic for Poker.xaml
    /// </summary>
    public partial class Poker : UserControl
    {
        public Chair[] chairs;
        public TexasGame game;
        public int activePlayers;
        public bool isSelectChair;
        public int roomId;

        public Poker()
        {
            InitializeComponent();
            chairs = new Chair[8] { Chair0, Chair1, Chair2, Chair3, Chair4, Chair5, Chair6, Chair7 };
            activePlayers = 0;
            isSelectChair = false;
        }

   
        public void Init()
        {
            for (int i = 0; i < game.ChairsInGame.Length; i++)
            {
                chairs[i].poker = this;
                chairs[i].player = game.ChairsInGame[i];
                chairs[i].Init();
            }
            for (int i = game.GamePreferences.MaxPlayers; i < chairs.Length; i++)
            {
                this.Dispatcher.Invoke(() =>
                {
                    chairs[i].Visibility = Visibility.Hidden;
                });
                
            }
                
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            if(game.GamePreferences.GetMinPlayers() > game.GetListActivePlayers().Count)
            {
                MessageBox.Show("Not enough players to start game!");
                return;
            }

        }
    }
}
