using poker.PokerGame;
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
    /// Interaction logic for Chair.xaml
    /// </summary>
    public partial class Chair : UserControl
    {

        public static readonly DependencyProperty chairNum =
        DependencyProperty.Register("ChairNum", typeof(int), typeof(Chair));
        public GamePlayer player = null;
        public Poker poker;
        public bool isActive = false;

        public Chair()
        {
            InitializeComponent();
        }

        public int ChairNum { get { return (int)GetValue(chairNum); } set { SetValue(chairNum, value); } }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!poker.isSelectChair)
            {
                    poker.isSelectChair = true;
                    Service.Instance.SitOnChair(poker.room.Id + "", MainInfo.Instance.Player.Username, ChairNum + "");
            }       
        }

        internal void SetAsActivePlayer(bool isActive)
        {
            Dispatcher.Invoke(() =>
            {
                if (isActive)
                {
                    Grid.Background = new SolidColorBrush(Colors.Yellow);
                }
                else
                {
                    var bc = new BrushConverter();
                    Grid.Background = (Brush)bc.ConvertFrom("#FF1DF903");
                }
            });
           
        }

        public void SetAsActivePlayer()
        {
            
        }

        public void Update()
        {
            if (player != null)
            {
                this.Dispatcher.Invoke(() =>
                {
                    Button.Visibility = Visibility.Hidden;
                    PlayerInfo.Visibility = Visibility.Visible;
                    if (player.Hand.Count >= 2)
                    {
                        if (player.GetUsername().Equals(MainInfo.Instance.Player.Username)
                            || (!((TexasGame)(poker.room.Game)).Active && ((TexasGame)(poker.room.Game)).Winners.Contains(player)))
                        {
                            String stringPath1 = "pack://application:,,,/PokerClient;component/gui/Images/Cards/" + player.Hand[0].ToString() + ".png";
                            String stringPath2 = "pack://application:,,,/PokerClient;component/gui/Images/Cards/" + player.Hand[1].ToString() + ".png";
                            Card1.Source = new BitmapImage(new Uri(stringPath1));
                            Card2.Source = new BitmapImage(new Uri(stringPath2));
                        }
                        else
                        {
                            String stringPath = "pack://application:,,,/PokerClient;component/gui/Images/Cards/Card_Back_Vertical_Red.png";
                            Card1.Source = new BitmapImage(new Uri(stringPath));
                            Card2.Source = new BitmapImage(new Uri(stringPath));
                        }
                        Card1.Visibility = Visibility.Visible;
                        Card2.Visibility = Visibility.Visible;
                        PlayerMoney.Content = player.CurrentBet + "$/" + player.Money + "$";
                    }
                    PlayerName.Content = player.GetUsername();
                    if (poker.room.Game.GetBigBlind() != null && poker.room.Game.GetBigBlind().Player.Equals(player.Player))
                    {
                        Blind.Content = "BB";
                        Blind.Visibility = Visibility.Visible;
                    }

                    if (poker.room.Game.GetSmallBlind() != null && poker.room.Game.GetSmallBlind().Player.Equals(player.Player))
                    {
                        Blind.Content = "SB";
                        Blind.Visibility = Visibility.Visible;
                    }
                    if (player != null && player.IsFold())
                        Grid.Opacity = 0.5;
                    else
                        Grid.Opacity = 1;
                });
            }
            else
            {
                this.Dispatcher.Invoke(() =>
                {
                    Button.Visibility = Visibility.Visible;
                    PlayerInfo.Visibility = Visibility.Hidden;
                });
            }
        }
    }
}
