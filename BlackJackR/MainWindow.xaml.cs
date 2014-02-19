using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

            if (randomCardFirst == 11) PlayerAces.Add(Card1PL, randomCardFirst);
            if (randomCardTwo == 11) PlayerAces.Add(Card2PL, randomCardTwo);

            Card1PL.Text = randomCardFirst.ToString();
            Card2PL.Text = randomCardTwo.ToString();

            ScorePlayer = randomCardFirst + randomCardTwo;
            PlayerScoreLabel.Content = ScorePlayer.ToString();

            if (randomCardFirst == randomCardTwo)
            {
                // split not implementer yet
                // SplitButton.Visibility = Visibility.Visible;
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
        }

        private void HitButton_OnClick(object sender, RoutedEventArgs e)
        {
            int min = CardValues.Min();
            int max = CardValues.Max() + 1;

            int randomCard = Ran.Next(min, max);
            if (randomCard == 11) PlayerAces.Add(TextBoxPlayer[CurrentTextBoxPlayer], randomCard);
            
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
                CheckPlayerCardsForAce();
        }

        private void StandButton_OnClick(object sender, RoutedEventArgs e)
        {
            Min = CardValues.Min();
            Max = CardValues.Max() + 1;

            int randomCardFirst = Ran.Next(Min, Max);
            int randomCardTwo = Ran.Next(Min, Max);
            if (randomCardFirst == 11) ComputerAces.Add(Card1AI, randomCardFirst);
            if (randomCardTwo == 11) ComputerAces.Add(Card2AI, randomCardTwo);

            ScoreComputer = randomCardFirst + randomCardTwo;
            ComputerScoreLabel.Content = ScoreComputer.ToString();

            Card1AI.Text = randomCardFirst.ToString();
            Card2AI.Text = randomCardTwo.ToString();
            
            if (ScoreComputer > 21)
                CheckComputerCardsForAce();
            if (ScoreComputer < 17)
                new Thread(CalculateComputerCard).Start();
            else
                CalculateWinner();
        }

        private void SplitButton_OnClick(object sender, RoutedEventArgs e)
        {
            // split cards, needs to be implemented.
        }

        private void CalculateComputerCard()
        {
            while (ScoreComputer < 17 && CurrentTextBoxComputer < TextBoxComputer.Count())
            {
                // Let the computer act like its thinking
                Thread.Sleep(1000);
                int randomCard = Ran.Next(Min, Max);
                if(randomCard == 11) ComputerAces.Add(TextBoxComputer[CurrentTextBoxComputer], randomCard);

                ScoreComputer += randomCard;
                Dispatch(randomCard);
            }
            this.Dispatcher.Invoke((Action)(() =>
            {
                CalculateWinner();
                Thread.Yield();
            }));
        }

        public void Dispatch(int card)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                TextBoxComputer[CurrentTextBoxComputer].Text = card.ToString();
                CurrentTextBoxComputer++;
                ComputerScoreLabel.Content = ScoreComputer.ToString();
            }));

            if (ScoreComputer > 21)
            {
                this.Dispatcher.Invoke((Action)(() =>
                {
                    if (ComputerAces.Any())
                        CheckComputerCardsForAce();
                    else
                        Thread.Yield();
                }));
            }
        }
    }
}