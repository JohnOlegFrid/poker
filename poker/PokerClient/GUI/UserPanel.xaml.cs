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
            userNameTB.Text = "Username: " + MainInfo.Instance.Player.Username;
            userRankTB.Text = "Rank: " + MainInfo.Instance.Player.Rank;
            userMoneyTB.Text = MainInfo.Instance.Player.GetEmail();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainInfo.Instance.Logout();
        }
    }
}