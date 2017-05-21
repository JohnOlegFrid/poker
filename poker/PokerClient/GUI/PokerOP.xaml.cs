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
    /// Interaction logic for PokerOP.xaml
    /// </summary>
    public partial class PokerOP : UserControl
    {
        private Poker poker;

        public PokerOP()
        {
            InitializeComponent();
            IsEnabled = false;
        }

        public void Update()
        {
            this.Dispatcher.Invoke(() =>
            {
                if (poker.room.Game.GetActivePlayer() == null)
                    return;
                IsEnabled = poker.room.Game.GetActivePlayer().Player.Equals(MainInfo.Instance.Player);
                if (IsEnabled)
                {
                    Slider.Maximum = poker.room.Game.GetActivePlayer().Money;
                    Slider.Minimum =  poker.room.Game.GetLastMove().Amount - poker.room.Game.GetActivePlayer().CurrentBet;
                }
            });       
        }

        public void SetOp(Poker poker)
        {
            this.poker = poker;
        }

        private void FoldButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CheckButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CallButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RaiseButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
