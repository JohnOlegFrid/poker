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
        int activePlayers;
        public Poker()
        {
            InitializeComponent();
            chairs = new Chair[8] { Chair0, Chair1, Chair2, Chair3, Chair4, Chair5, Chair6, Chair7 };
            activePlayers = 0;          
        }

        public void Init()
        {
            for (int i = game.GamePreferences.MaxPlayers; i < chairs.Length; i++)
                chairs[i].IsEnabled = false;
            MessageBox.Show(game.GamePreferences.MaxPlayers.ToString());
        }
    }
}
