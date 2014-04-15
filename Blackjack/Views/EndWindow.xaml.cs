using System.Windows;

namespace Blackjack.Views
{
    public partial class EndWindow
    {
        public EndWindow(string name)
        {
            InitializeComponent();
            WinMessage.Content = name + " won!";
        }

        private void Retry(object sender, RoutedEventArgs e)
        {
            new StartWindow().Show();
            this.Close();
        }

        private void Quit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
