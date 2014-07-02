using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Blackjack.Helpers;
using Blackjack.Interfaces;
using Blackjack.Models;
using Blackjack.ViewModels;

namespace Blackjack.Views
{
    public partial class GameWindow : IView
    {
        private readonly Button[] defaultButtons;
        private readonly Button[] splitButtons;

        public GameWindow(string name)
        {
            InitializeComponent();
            defaultButtons = new[] { Hit, Stand, Split };
            splitButtons = new[] { HitLeft, HitRight, StandLeft, StandRight };
            DataContext = new GameViewModel(this, name);
        }

        public GameWindow()
        {
            InitializeComponent();
            defaultButtons = new[] { Hit, Stand, Split };
            splitButtons = new[] { HitLeft, HitRight, StandLeft, StandRight };
            DataContext = new GameViewModel(this, "temp");
        }
 
        public void AddCards(Player one, Player two)
        {
            AddImages(one, PlayerImages);
            AddImages(two, ComputerImages);
        }

        private void AddImages(Player player, Grid grid)
        {
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(grid); i++)
            {
                var child = VisualTreeHelper.GetChild(grid, i);
                player.Images.Add((Image)child);
            }
        }

        public void DealButton(bool show)
        {
            Deal.Visibility = show ? Visibility.Visible : Visibility.Hidden;
        }

        public void DisplayName(string name)
        {
            Name1.Content = name + ":";
            Name2.Content = name + ":";
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

        public void EndGame(Player one, Player two, int bet)
        {
            Player winner = GameHelper.CalculateWinner(one, two);
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
        }

        public void EndGameSplit(Player one, Player two, int bet)
        {
            Player winner = GameHelper.CalculateWinnerSplit(one, two);
            if (winner == null)
            {
                ShowResult("Draw!");
            }
            else
            {
                ShowResult(winner.Name + " won the game!");
                // Multiply by two because we doubled our wins / loses
                if (winner == one)
                {
                    one.Money += bet * 2;
                    two.Money -= bet * 2;
                }
                else
                {
                    two.Money += bet * 2;
                    one.Money -= bet * 2;
                }
            }
        }

        public void CheckMoney(Player one, Player two)
        {
            if (one.Money <= 0)
            {
                new EndWindow(two.Name).Show();
                Close();
            }
            else if (two.Money <= 0)
            {
                new EndWindow(one.Name).Show(); ;
                Close();
            }
        }

        public void SplitDeck(Player player, bool activate)
        {
            if (activate)
            {
                BorderSplit.Visibility = Visibility.Visible;
                defaultButtons.ToList().ForEach(x => x.Visibility = Visibility.Hidden);
                splitButtons.ToList().ForEach(x => x.Visibility = Visibility.Visible);
            }
            else
            {
                BorderSplit.Visibility = Visibility.Hidden;
                defaultButtons.ToList().ForEach(x => x.Visibility = Visibility.Visible);
                splitButtons.ToList().ForEach(x => x.Visibility = Visibility.Hidden);
            }
        }

        public void AddSplitDeckCards(Player player)
        {
            player.SplitDeck.ImagesLeft.AddRange(new List<Image>() { Card3Player, Card5Player, CardLeft1, CardLeft2, CardLeft3 });
            player.SplitDeck.ImagesRight.AddRange(new List<Image>() { Card4Player, Card6Player, CardRight1, CardRight2, CardRight3 });
        }
    }
}