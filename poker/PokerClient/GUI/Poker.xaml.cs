using poker.PokerGame;
using PokerClient.Center;
using PokerClient.ServiceLayer;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System;
using System.Windows.Media.Imaging;
using PokerClient.Cards;

namespace PokerClient.GUI
{
    /// <summary>
    /// Interaction logic for Poker.xaml
    /// </summary>
    public partial class Poker : UserControl
    {
        public Chair[] chairs;
        public int activePlayers;
        public bool isSelectChair;
        public Room room;


        public Poker()
        {
            InitializeComponent();
            chairs = new Chair[8] { Chair0, Chair1, Chair2, Chair3, Chair4, Chair5, Chair6, Chair7 };
            activePlayers = 0;
            isSelectChair = false;
            OP.SetOp(this);
        }

   
        public void UpdateChairs()
        {
            for (int i = 0; i < ((TexasGame)room.Game).ChairsInGame.Length; i++)
            {
                chairs[i].poker = this;
                chairs[i].player = ((TexasGame)room.Game).ChairsInGame[i];
                chairs[i].Update();
                bool isActive = chairs[i].player != null && room.Game.GetActivePlayer() != null 
                    && chairs[i].player.Player.Equals(room.Game.GetActivePlayer().Player);
                chairs[i].SetAsActivePlayer(isActive);

            }
            for (int i = ((TexasGame)room.Game).GamePreferences.MaxPlayers; i < chairs.Length; i++)
            {
                this.Dispatcher.Invoke(() =>
                {
                    chairs[i].Visibility = Visibility.Hidden;
                });           
            }
            isSelectChair = room.Game.IsPlayerActiveInGame(MainInfo.Instance.Player);
        }
        public void UpdateGame()
        {
            ValidateAllThePlayersCanBeInRoom();
            UpdateChairs();
            OP.Update();
            UpdateCardsOfBoard();
            this.Dispatcher.Invoke(() =>
            {               
                this.StartGameButton.Visibility = ((TexasGame)room.Game).Active ? Visibility.Hidden : Visibility.Visible;
                this.StartGameButton.IsEnabled = !((TexasGame)room.Game).Active;
                this.PotLabel.Content = ((TexasGame)room.Game).Pot + "$";
            });
        }

        private void ValidateAllThePlayersCanBeInRoom()
        {
            if (!((TexasGame)room.Game).Active || ((TexasGame)room.Game).GamePreferences.AllowSpectating)
                return;
            if (room.Game.GetListActivePlayers().Exists(p => p.GetUsername().Equals(MainInfo.Instance.Player.Username)))
                return;
            MessageBox.Show("this room doesn't allow spectating!");
            this.Dispatcher.Invoke(() =>
            {
                room.RoomWindow.Close();
            });
        }

        private void UpdateCardsOfBoard()
        {
            if (((object)((TexasGame)room.Game).Board) == null)
                return;
            UpdateCard(Card1, 1);
            UpdateCard(Card2, 2);
            UpdateCard(Card3, 3);
            UpdateCard(Card4, 4);
            UpdateCard(Card5, 5);
        }

        private void UpdateCard(Image card, int i)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (((TexasGame)room.Game).Board.Count < i)
                {
                    card.Visibility = Visibility.Hidden;
                    return;
                }
                card.Visibility = Visibility.Visible;
                String stringPath = "pack://application:,,,/PokerClient;component/gui/Images/Cards/" + ((TexasGame)room.Game).Board[i-1].ToString() + ".png";
                card.Source = new BitmapImage(new Uri(stringPath));
            });
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            if (((TexasGame)room.Game).Active)
                return;
            if(((TexasGame)room.Game).GamePreferences.GetMinPlayers() > ((TexasGame)room.Game).GetListActivePlayers().Count)
            {
                MessageBox.Show("Not enough players to start game!");
                return;
            }
            this.Dispatcher.Invoke(() =>
            {
                StartGameButton.IsEnabled = false;
            });       
            Service.Instance.StartGame(room.Id+"");
        }

    }
}
