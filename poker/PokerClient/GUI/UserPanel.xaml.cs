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
    /// Interaction logic for UserPanel.xaml
    /// </summary>
    public partial class UserPanel : UserControl
    {
        public UserPanel()
        {
            InitializeComponent();
            Update();
        }


        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainInfo.Instance.Logout();

        }

        public void Update()
        {
            this.Dispatcher.Invoke(() =>
            {
                userNameTB.Text = "Username: " + MainInfo.Instance.Player.Username;
                userRankTB.Text = "Rank: " + MainInfo.Instance.Player.Rank;
                userMoneyTB.Text = "Money: " + MainInfo.Instance.Player.Money;
            });
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditWindow ew = new EditWindow();
            ew.Show();
        }
    }
}