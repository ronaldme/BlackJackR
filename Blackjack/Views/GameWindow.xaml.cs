using System.Windows;
using Blackjack.Interfaces;
using Blackjack.Models;
using Blackjack.ViewModels;

namespace Blackjack.Views
{
    public partial class GameWindow : Window, IView
    {
        public GameWindow(string name)
        {
            InitializeComponent();
            this.DataContext = new GameViewModel(this, name);
        }

        /// <summary>
        /// Remove for release and set StartWindow.xaml to startup view
        /// </summary>
        public GameWindow()
        {
            InitializeComponent();
            this.DataContext = new GameViewModel(this, "TestModi");
        }

        public void AddCards(Player one, Player two)
        {
            one.Images.Add(Card1Player);
            one.Images.Add(Card2Player);
            one.Images.Add(Card3Player);
            one.Images.Add(Card4Player);
            one.Images.Add(Card5Player);
            one.Images.Add(Card6Player);

            two.Images.Add(Card1Computer);
            two.Images.Add(Card2Computer);
            two.Images.Add(Card3Computer);
            two.Images.Add(Card4Computer);
            two.Images.Add(Card5Computer);
            two.Images.Add(Card6Computer);
        }

        public void ShowResult(string result)
        {
            Result.Content = result;
        }

        public void ResetResult()
        {
            Result.Content = string.Empty;
        }


        public void DisplayMoney(Player player)
        {
            if (player.Name == "Computer")
            {
                ComputerMoney.Content = player.Money;
            }
            else
            {
                PlayerMoney.Content = player.Money;
            }
        }

        public void DisplayPoints(Player player)
        {
            if (player.Name == "Computer")
            {
                ComputerPoints.Content = player.CurrentScore;
            }
            else
            {
                PlayerPoints.Content = player.CurrentScore;
            } 
        }
    }
}
