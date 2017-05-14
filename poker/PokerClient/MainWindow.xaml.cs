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
using ClientPoker.ClientFiles;

namespace ClientPoker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainInfo.Instance.MainWindow = this;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (!MainInfo.Instance.ConnectToServer())
            {
                MessageBox.Show("Cannot Connect To Server...");
                return;
            }
                
            Command command = new Command("Login", new String[2] { usernameBox.Text, passwordBox.Password });
            MainInfo.Instance.SendMessage(command);
            loginButton.IsEnabled = false;
        }

        public void DoLogin()
        {
            this.Dispatcher.Invoke(() =>
            {
                MessageBox.Show(MainInfo.Instance.Player.Username + " is logged");
            });       
        }

        public void LoginFaild()
        {
            this.Dispatcher.Invoke(() =>
            {
                ShowMessage("Error with loggin, please try again");
                loginButton.IsEnabled = true;
            });
            
        }

        public void ShowMessage(string msg)
        {
            MessageBox.Show(msg);
        }

    }
}
