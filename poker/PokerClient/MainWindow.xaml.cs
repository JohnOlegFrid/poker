using Newtonsoft.Json;
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

namespace ClientPoker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Client client = null;
        public MainWindow()
        {
            InitializeComponent();
        }

        public bool ConnectToServer()
        {
            if (client != null)
                return true;       
            String ip = Client.GetLocalIPAddress();
            int port = 5555;
            try
            {
                client = new Client(ip, port);
            }
            catch (Exception e)
            {
                MessageBox.Show("Cannot Connect To Server...");
                return false;
            }
            return true;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ConnectToServer())
                return;
            Command command = new Command("Login", new String[2] { usernameBox.Text, passwordBox.Password });
            String answer = client.SendMessage(command);

            MessageBox.Show(answer);
        }

    }
}
