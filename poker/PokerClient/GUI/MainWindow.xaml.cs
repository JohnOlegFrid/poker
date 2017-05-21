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
using PokerClient.Communication;

namespace PokerClient.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Login log;
        Register reg;

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            MainInfo.Instance.MainWindow = this;
            log = new Login();
            reg = new Register();
            MainInfo.Instance.Login = log;

            mainContentControl.Content = new Login();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            this.Dispatcher.Invoke(() =>
            {
                Environment.Exit(0);
                Application.Current.Shutdown();
            });         
        }


        public void RegisterFaild(string registerMsg)
        {
            reg.RegisterFaild(registerMsg);
        }

        public void OpenMainMenu()
        {
            log.OpenMainMenu();
        }

        public static void ShowMessage(string msg)
        {
            MessageBox.Show(msg);
        }

    }
}
