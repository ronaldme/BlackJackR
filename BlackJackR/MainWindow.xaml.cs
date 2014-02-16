using System;
using System.Collections.Generic;
using System.Linq;
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
                ResetGame();
                return;
            }
        }

        private void StandButton_OnClick(object sender, RoutedEventArgs e)
        {
            int min = CardValues.Min();
            int max = CardValues.Max() + 1;

            int randomCardFirst = Ran.Next(min, max);
            int randomCardTwo = Ran.Next(min, max);
            ScoreComputer = randomCardFirst + randomCardTwo;

            Card1AI.Text = randomCardFirst.ToString();
            Card2AI.Text = randomCardTwo.ToString();

            while (ScoreComputer < 17)
            {
                int randomCard = Ran.Next(min, max);

                TextBoxComputer[CurrentTextBoxComputer].Text = randomCard.ToString();
                CurrentTextBoxComputer++;
                ScoreComputer += randomCard;
                AIScoreLabel.Content = ScoreComputer.ToString();

                if (CurrentTextBoxComputer >= TextBoxComputer.Count())
                {
                    break;
                }
                else if (ScoreComputer >= 21)
                {
                    CalculateWinner();
                    return;
                }
            }

            AIScoreLabel.Content = ScoreComputer.ToString();
            CalculateWinner();
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
                MoneyComputer += Convert.ToInt16(BetBox.Text);
                MoneyComputerLabel.Content = "€" + MoneyComputer.ToString();

                MoneyPlayer -= Convert.ToInt16(BetBox.Text);
                MoneyPlayerLabel.Content = "€" + MoneyPlayer.ToString();
            }
            else
            {
                EndGameMessageLabel.Content = "You won this round!";
                MoneyPlayer += Convert.ToInt16(BetBox.Text);
                MoneyPlayerLabel.Content = "€" + MoneyPlayer.ToString();

                MoneyComputer -= Convert.ToInt16(BetBox.Text);
                MoneyComputerLabel.Content = "€" + MoneyComputer.ToString();
            }

            ResetGame();
        }
    }
}