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

        public Chair()
        {
            InitializeComponent();
        }

        public int ChairNum { get { return (int)GetValue(chairNum); } set { SetValue(chairNum, value); } }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!poker.isSelectChair)
            {
                MessageBox.Show("chair num " + ChairNum);
                poker.isSelectChair = true;
                Service.Instance.SitOnChair(poker.roomId+"", MainInfo.Instance.Player.Username, ChairNum+"");
            }       
        }

        public void Update()
        {
            if(player != null)
            {
                this.Dispatcher.Invoke(() =>
                {
                    Button.Visibility = Visibility.Hidden;
                    PlayerInfo.Visibility = Visibility.Visible;
                    if(player.Hand.Count == 2)
                    {
                        if (player.GetUsername().Equals(MainInfo.Instance.Player.Username))
                        {
                            String stringPath1 = "pack://application:,,,/PokerClient;component/gui/Images/Cards/" + player.Hand[0].ToString() + ".png";
                            String stringPath2 = "pack://application:,,,/PokerClient;component/gui/Images/Cards/" + player.Hand[1].ToString() + ".png";
                            Card1.Source = new BitmapImage(new Uri(stringPath1));
                            Card2.Source = new BitmapImage(new Uri(stringPath2));
                        }
                        Card1.Visibility = Visibility.Visible;
                        Card2.Visibility = Visibility.Visible;
                        PlayerMoney.Content = player.Money + "$";
                    }
                    PlayerName.Content = player.GetUsername();
                });
            }
        }
    }
}
