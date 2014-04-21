using System.Windows;
using Blackjack.Helpers;
using Blackjack.Interfaces;
using Blackjack.Models;
using Blackjack.ViewModels;

namespace Blackjack.Views
{
    public partial class GameWindow : IView
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
            this.DataContext = new GameViewModel(this, "Ronald");
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
            ComputerPoints.Content = 0;
            PlayerPoints.Content = 0;
        }

        public void DisplayMoney(Player one, Player two)
        {
            PlayerMoney.Content = one.Money;   
            ComputerMoney.Content = two.Money;
        }

        public void DisplayPoints(Player player)
        {
            if (player.Name == "Computer")
            {
                ComputerPoints.Content = player.Score;
            }
            else
            {
                PlayerPoints.Content = player.Score;
            } 
        }

        public void DisplayPointsSplit(Player player)
        {
            PlayerPoints.Content = player.SplitDeck.ScoreLeft + " : " + player.SplitDeck.ScoreRight;
        }

        public void EndGame(Player one, Player two, Player winner, int bet)
        {
            if (winner == null)
            {
                ShowResult("Draw!");
            }
            else 
            {
                ShowResult(winner.Name + " won the game!");
                if (winner == one)
                {
                    one.Money += bet;
                    two.Money -= bet;
                }
                else
                {
                    two.Money += bet;
                    one.Money -= bet;
                }
            }
            
            DisplayMoney(one, two);

            if (one.Money <= 0)
            {
                new EndWindow(two.Name).Show();
                this.Close();
            }
            else if (two.Money <= 0)
            {
                new EndWindow(one.Name).Show(); ;
                this.Close();
            }
        }

        public void SplitDeck(Player player, bool activate)
        {
            if (activate)
            {
                BorderSplit.Visibility = Visibility.Visible;
                HitLeft.Visibility = Visibility.Visible;
                HitRight.Visibility = Visibility.Visible;
                StandLeft.Visibility = Visibility.Visible;
                StandRight.Visibility = Visibility.Visible;

                Hit.Visibility = Visibility.Hidden;
                Stand.Visibility = Visibility.Hidden;
                Split.Visibility = Visibility.Hidden;
                player.SplitDeck.ImagesLeft.Add(Card3Player);
                player.SplitDeck.ImagesLeft.Add(Card5Player);
                player.SplitDeck.ImagesLeft.Add(CardLeft1);
                player.SplitDeck.ImagesLeft.Add(CardLeft2);
                player.SplitDeck.ImagesLeft.Add(CardLeft3);

                player.SplitDeck.ImagesRight.Add(Card4Player);
                player.SplitDeck.ImagesRight.Add(Card6Player);
                player.SplitDeck.ImagesRight.Add(CardRight1);
                player.SplitDeck.ImagesRight.Add(CardRight2);
                player.SplitDeck.ImagesRight.Add(CardRight3);
            }
            else
            {
                BorderSplit.Visibility = Visibility.Hidden;
                HitLeft.Visibility = Visibility.Hidden;
                HitRight.Visibility = Visibility.Hidden;
                StandLeft.Visibility = Visibility.Hidden;
                StandRight.Visibility = Visibility.Hidden;

                Hit.Visibility = Visibility.Visible;
                Stand.Visibility = Visibility.Visible;
                Split.Visibility = Visibility.Visible;
            }
        }
    }
}