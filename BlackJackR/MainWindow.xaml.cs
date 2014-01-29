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

namespace BlackJackR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int currentMoneyAI = 1000;
        private int currentMoneyPlayer = 1000;
        private TextBox[] playerBoxes;
        private TextBox[] aiBoxes;

        public MainWindow()
        {
            InitializeComponent();

            MoneyPlayerLabel.Content += currentMoneyPlayer.ToString();
            MoneyAILabel.Content += currentMoneyAI.ToString();

            Title = "BlackJackR - Reach €2000 to win!";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // draw an extra card
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // Check who has the highest card
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // deal cards

        }
    }
}
