using PokerClient.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PokerClient.GUI
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        public UserPanel userPanel;
        public MainPanel mainPanel;
        public MainMenu()
        {
            InitializeComponent();
            userPanel = new UserPanel();
            this.topMainPanel.Content = userPanel;
            mainPanel = new MainPanel();
            this.bottomMainPanel.Content = mainPanel;
        }
    }
}
