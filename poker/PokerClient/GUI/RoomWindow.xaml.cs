using poker.PokerGame;
using PokerClient.Center;
using PokerClient.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for RoomWindow.xaml
    /// </summary>
    public partial class RoomWindow : Window
    {
        Room room;
        public ObservableCollection<string> gameLog;
        public RoomWindow(Room room)
        {
            this.room = room;
            InitializeComponent();
            this.topMainPanel.Content = new UserPanel();
            this.InfoText.Content = room;
            PokerTable.room = room;
            this.gameLog = new ObservableCollection<string>();
            GameLog.DataContext = gameLog;
            Chat.DataContext = room.Chat.GetMessages();
            PokerTable.UpdateChairs();
            Service.Instance.AddPlayerToRoom(room.Id+"", MainInfo.Instance.Player.Username);
            MsgBox.KeyDown += new KeyEventHandler(MsgBox_KeyDown);
        }

        private void MsgBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                SendButton_Click(sender,null);
            }
        }


        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            room.RoomWindow = null;
            Service.Instance.RemovePlayerFromRoom(room.Id + "", MainInfo.Instance.Player.Username);
            base.OnClosing(e);
        }

        public void SetLog(List<string> logs)
        {
            Application.Current.Dispatcher.Invoke(() => {
                gameLog.Clear();
                foreach (string s in logs)
                    this.gameLog.Add(s);
                if(gameLog.Count > 0)
                    GameLog.ScrollIntoView(gameLog.Last());
            });          
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string txt = MsgBox.Text;
            if (txt.Trim().Equals(""))
                return;
            string username = MainInfo.Instance.Player.Username;
            Service.Instance.SendChatMessage(room.Id + "", username, txt, room.Game.IsPlayerActiveInGame(MainInfo.Instance.Player) +"");
            MsgBox.Text = "";
        }

        public void ScrollDownChat(Message msg)
        {
            Application.Current.Dispatcher.Invoke(() => {
                Chat.ScrollIntoView(msg);
            });
        }
    }
}
