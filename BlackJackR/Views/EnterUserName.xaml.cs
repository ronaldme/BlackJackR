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
    /// Interaction logic for EnterUserName.xaml
    /// </summary>
    public partial class EnterUserName : Window
    {
        private List<string> PlayerNames = new List<string>() { "Failing", "No Name Man", "Noob", "Skywalker", "Testing" };

        public EnterUserName()
        {
            InitializeComponent();
            Title = "Enter your name!";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();

            if (PlayerNameInput.Text.Length > 3 && PlayerNameInput.Text.Length < 25)
            {
                window.NameLabel1.Content = PlayerNameInput.Text + " :";
                window.NameLabel2.Content = PlayerNameInput.Text + " :";
            }
            else
            {
                Random r = new Random();
                int ran = r.Next(0, 4);
                
                window.NameLabel1.Content = PlayerNames[ran] + " :";
                window.NameLabel2.Content = PlayerNames[ran] + " :";
            }
            this.Close();
        }
    }
}
