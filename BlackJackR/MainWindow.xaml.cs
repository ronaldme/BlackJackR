using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace BlackJackR
{
    /// <summary>
    /// Button events.
    /// </summary>
    public partial class MainWindow : Window
    {
        private void SplitButton_OnClick(object sender, RoutedEventArgs e)
        {
            // split cards, needs to be implemented.
        }

        private void HitButton_OnClick(object sender, RoutedEventArgs e)
        {
            int min = CardValues.Min();
            int max = CardValues.Max() + 1;

            int randomCard = Ran.Next(min, max);
            ScorePlayer += randomCard;
            PlayerScoreLabel.Content = ScorePlayer.ToString();

            TextBoxPlayer[CurrentTextBoxPlayer].Text = randomCard.ToString();
            CurrentTextBoxPlayer++;

            if (CurrentTextBoxPlayer >= TextBoxPlayer.Count())
            {
                HitButton.Visibility = Visibility.Hidden;
                StandButton_OnClick(sender, new RoutedEventArgs());
            }

            if (ScorePlayer > 21)
            {
                EndGameMessageLabel.Content = "More then 21. You lost this round!";
                AddMoneyToPlayer(false);
                ResetGame();
                return;
            }
        }

        private void StandButton_OnClick(object sender, RoutedEventArgs e)
        {
            Min = CardValues.Min();
            Max = CardValues.Max() + 1;

            int randomCardFirst = Ran.Next(Min, Max);
            int randomCardTwo = Ran.Next(Min, Max);
            ScoreComputer = randomCardFirst + randomCardTwo;
            AIScoreLabel.Content = ScoreComputer.ToString();

            Card1AI.Text = randomCardFirst.ToString();
            Card2AI.Text = randomCardTwo.ToString();

            if (ScoreComputer < 17)
            {
                new Thread(CalculateAnotherCard).Start();
            }
            else
            {
                CalculateWinner(); 
            }
        }

        private void CalculateAnotherCard()
        {
            while (ScoreComputer < 17)
            {
                Thread.Sleep(1500);
                int randomCard = Ran.Next(Min, Max);

                CurrentTextBoxComputer++;
                ScoreComputer += randomCard;

                this.Dispatcher.Invoke((Action)(() =>
                {
                        TextBoxComputer[CurrentTextBoxComputer].Text = randomCard.ToString();
                        AIScoreLabel.Content = ScoreComputer.ToString();
                }));

                if (CurrentTextBoxComputer >= TextBoxComputer.Count())
                {
                    break;
                }
                else if (ScoreComputer >= 21)
                {
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        CalculateWinner();
                        return;
                    }));
                }
            }
        }

        private void DealButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (NotificationIsOpen) return;

            string betAmount = BetBox.Text;
            try
            {
                int bet = Convert.ToInt16(betAmount);

                if (bet < 100 || bet > 500)
                {
                    Notification = new NotificationWindow(this);
                    Notification.Show();
                    return;
                }
            }
            catch (FormatException exception)
            {
                Notification = new NotificationWindow(this);
                Notification.Show();
                return;
            }

            StartGame();

            int min = CardValues.Min();
            int max = CardValues.Max() + 1;

            int randomCardFirst = Ran.Next(min, max);
            int randomCardTwo = Ran.Next(min, max);

            ScorePlayer = randomCardFirst + randomCardTwo;
            PlayerScoreLabel.Content = ScorePlayer.ToString();

            if (randomCardFirst == randomCardTwo)
            {
                SplitButton.Visibility = Visibility.Visible;
            }

            if (ScorePlayer == 21)
            {
                // Blackjack
                BlackJackLabel.Visibility = Visibility.Visible;
                HitButton.Visibility = Visibility.Hidden;
            }

            Card1PL.Text = randomCardFirst.ToString();
            Card2PL.Text = randomCardTwo.ToString();
        }

        private void CalculateWinner()
        {
            if (ScoreComputer == ScorePlayer)
            {
                EndGameMessageLabel.Content = "Draw!";
            }
            else if (ScoreComputer > ScorePlayer && ScoreComputer <= 21)
            {
                EndGameMessageLabel.Content = "Computer won this round!";
                AddMoneyToPlayer(false);
            }
            else
            {
                EndGameMessageLabel.Content = "You won this round!";
                AddMoneyToPlayer(true);
            }

            ResetGame();
        }

        private void AddMoneyToPlayer(bool playerWon)
        {
            if (playerWon)
            {
                MoneyPlayer += Convert.ToInt16(BetBox.Text);
                MoneyPlayerLabel.Content = "€" + MoneyComputer.ToString();

                MoneyComputer -= Convert.ToInt16(BetBox.Text);
                MoneyComputerLabel.Content = "€" + MoneyPlayer.ToString();
            }
            else
            {
                MoneyComputer += Convert.ToInt16(BetBox.Text);
                MoneyComputerLabel.Content = "€" + MoneyComputer.ToString();

                MoneyPlayer -= Convert.ToInt16(BetBox.Text);
                MoneyPlayerLabel.Content = "€" + MoneyPlayer.ToString();
            }
        }
    }
}