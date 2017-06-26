using PokerClient.Communication;
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
using System.Windows.Shapes;

namespace PokerClient.GUI
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public EditWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            emailBox.Text = MainInfo.Instance.getPlayerEmail();
            usernameBox.Text = MainInfo.Instance.getPlayerUsername();
            IPadress.Text = Client.GetLocalIPAddress();

        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            String pass = passwordBox.Password;
            String confPass = confirmPasswordBox.Password;

            
            if (pass.CompareTo(confPass) != 0)
            {
                MessageBox.Show("The Passwords aren't the same.\nRe-enter Please.", "Wrong Password", MessageBoxButton.OK, MessageBoxImage.Warning);
                MainInfo.Instance.EditWindow = null;
                // RegisterButton.IsEnabled = true;
            }
            else
            {
                if (emailBox.Text != "" && pass.CompareTo("") != 0)
                    Service.Instance.UpdatePlayerInfo(usernameBox.Text,pass, emailBox.Text);
                else
                {
                    MessageBox.Show("You need to fill all text boxes.", "Wrong Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                    
                }
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            MainInfo.Instance.EditWindow = null;
        }
    }
}
