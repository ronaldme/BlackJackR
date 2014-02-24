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

namespace BlackJackR
{
    /// <summary>
    /// Interaction logic for Endgame.xaml
    /// </summary>
    public partial class Endgame : Window
    {
        public Endgame()
        {
            InitializeComponent();
        }

        private void Retry(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.NameLabel1.Content = this.RetryNameLabel.Content;
            window.NameLabel2.Content = this.RetryNameLabel.Content;
            window.Show();
            this.Close();
        }

        private void QuitGame(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
