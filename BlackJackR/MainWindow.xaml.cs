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
        private void DealButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (NotificationIsOpen || !CheckUserInput()) return;

            StartGame();

            int min = CardValues.Min();
            int max = CardValues.Max() + 1;

            int randomCardFirst = Ran.Next(min, max);
            int randomCardTwo = Ran.Next(min, max);

            if (randomCardFirst == 11) PlayerAces.Add(randomCardFirst);
            if (randomCardTwo == 11) PlayerAces.Add(randomCardTwo);

            ScorePlayer = randomCardFirst + randomCardTwo;
            PlayerScoreLabel.Content = ScorePlayer.ToString();

            if (randomCardFirst == randomCardTwo)
            {
                SplitButton.Visibility = Visibility.Visible;
            }

            if (ScorePlayer == 21)
            {
                BlackJackLabel.Visibility = Visibility.Visible;
                HitButton.Visibility = Visibility.Hidden;
            }
            else if (ScorePlayer > 21)
            {
                CheckPlayerCardsForAce();
            }

            Card1PL.Text = randomCardFirst.ToString();
            Card2PL.Text = randomCardTwo.ToString();
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

            if(randomCard == 11) PlayerAces.Add(randomCard);

            if (CurrentTextBoxPlayer >= TextBoxPlayer.Count())
            {
                HitButton.Visibility = Visibility.Hidden;
                StandButton_OnClick(sender, new RoutedEventArgs());
            }

            if (ScorePlayer > 21)
            {
                CheckPlayerCardsForAce();
            }
        }

        private void StandButton_OnClick(object sender, RoutedEventArgs e)
        {
            Min = CardValues.Min();
            Max = CardValues.Max() + 1;

            int randomCardFirst = Ran.Next(Min, Max);
            int randomCardTwo = Ran.Next(Min, Max);
            if (randomCardFirst == 11) ComputerAces.Add(randomCardFirst);
            if (randomCardTwo == 11) ComputerAces.Add(randomCardTwo);

            ScoreComputer = randomCardFirst + randomCardTwo;
            ComputerScoreLabel.Content = ScoreComputer.ToString();

            Card1AI.Text = randomCardFirst.ToString();
            Card2AI.Text = randomCardTwo.ToString();

            
            if (ScoreComputer > 21)
            {
                CheckComputerCardsForAce();
            }

            if (ScoreComputer < 17)
            {
                new Thread(CalculateComputerCard).Start();
            }
            else
            {
                CalculateWinner();
            }
        }

        private void SplitButton_OnClick(object sender, RoutedEventArgs e)
        {
            // split cards, needs to be implemented.
        }

        private void CalculateComputerCard()
        {
            while (ScoreComputer < 17)
            {
                Thread.Sleep(1000);
                int randomCard = Ran.Next(Min, Max);
                if(randomCard == 11) ComputerAces.Add(randomCard);

                ScoreComputer += randomCard;

                this.Dispatcher.Invoke((Action)(() =>
                {
                        TextBoxComputer[CurrentTextBoxComputer].Text = randomCard.ToString();
                        CurrentTextBoxComputer++;
                        ComputerScoreLabel.Content = ScoreComputer.ToString();
                }));

                if (ScoreComputer >= 21)
                {
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        CheckComputerCardsForAce();
                    }));
                }
                if (CurrentTextBoxComputer >= TextBoxComputer.Count())
                {
                    Thread.Yield();
                }
            }
            this.Dispatcher.Invoke((Action)(() =>
            {
                CalculateWinner();
                Thread.Yield();
            }));
        }
    }
}