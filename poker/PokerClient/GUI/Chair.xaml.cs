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
    /// Interaction logic for Chair.xaml
    /// </summary>
    public partial class Chair : UserControl
    {

        public static readonly DependencyProperty chairNum =
        DependencyProperty.Register("ChairNum", typeof(int), typeof(Chair));

        private string playerId;
        public Chair()
        {
            InitializeComponent();
        }

        public int ChairNum { get { return (int)GetValue(chairNum); } set { SetValue(chairNum, value); } }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("chair num " + ChairNum);
        }
    }
}
