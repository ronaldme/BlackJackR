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

namespace BlackJackR
{
    /// <summary>
    /// Interaction logic for NotificationWindow.xaml
    /// </summary>
    public partial class NotificationWindow : Window
    {
        private MainWindow Window { get; set; }

        public NotificationWindow(MainWindow window)
        {
            InitializeComponent();
            Window = window;
            window.NotificationIsOpen = true;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Window.NotificationIsOpen = false;
        }
    }
}
