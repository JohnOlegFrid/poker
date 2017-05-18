﻿using poker.PokerGame;
using PokerClient.Center;
using PokerClient.ServiceLayer;
using System;
using System.Collections.Generic;
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
        public RoomWindow(Room room)
        {
            this.room = room;
            InitializeComponent();
            this.InfoText.Content = room;
            PokerTable.game = (TexasGame)room.Game;
            PokerTable.roomId = room.Id;
            PokerTable.UpdateChairs();
            Service.Instance.AddPlayerToRoom(room.Id+"", MainInfo.Instance.Player.Username);
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            room.RoomWindow = null;
            Service.Instance.RemovePlayerFromRoom(room.Id + "", MainInfo.Instance.Player.Username);
            base.OnClosing(e);
        }

        public void UpdateRooms(object sender, RoutedEventArgs e)
        {
            Service.Instance.RequestAllRoomsToPlay();
        }
    }
}
