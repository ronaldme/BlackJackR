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
            if (NotificationIsOpen || !CheckUserInput())
            {
                return;
            }
            else
            {
                StartGame();
                int randomCardOne = Ran.Next(Min, Max);
                int randomCardTwo = Ran.Next(Min, Max);

                AddAces(randomCardOne, Card1PL, true);
                AddAces(randomCardTwo, Card2PL, true);

                Card1PL.Text = randomCardOne.ToString();
                Card2PL.Text = randomCardTwo.ToString();

                ScorePlayer = randomCardOne + randomCardTwo;
                PlayerScoreLabel.Content = ScorePlayer.ToString();

                if (ScorePlayer == 21)
                {
                    BlackJackLabel.Visibility = Visibility.Visible;
                    HitButton.Visibility = Visibility.Hidden;
                }
                else if (randomCardOne == randomCardTwo)
                {
                    SplitButton.Visibility = Visibility.Visible;
                }
            }
        }

        private void HitButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (ScorePlayer > 21)
                CheckPlayerCardsForAce();

            SplitButton.Visibility = Visibility.Hidden;

            int randomCard = Ran.Next(Min, Max);
            AddAces(randomCard, TextBoxPlayer[CurrentTextBoxPlayer], true);
            
            ScorePlayer += randomCard;
            PlayerScoreLabel.Content = ScorePlayer.ToString();
            TextBoxPlayer[CurrentTextBoxPlayer].Text = randomCard.ToString();
            CurrentTextBoxPlayer++;

            if (ScorePlayer > 21)
            {
                CheckPlayerCardsForAce();
            }
            if (CurrentTextBoxPlayer >= TextBoxPlayer.Count())
            {
                HitButton.Visibility = Visibility.Hidden;
                StandButton_OnClick(sender, new RoutedEventArgs());
            }
        }

        private void StandButton_OnClick(object sender, RoutedEventArgs e)
        {
            int randomcardOne = Ran.Next(Min, Max);
            int randomCardTwo = Ran.Next(Min, Max);
            AddAces(randomcardOne, Card1AI, false);
            AddAces(randomCardTwo, Card2AI, false);

            ScoreComputer = randomcardOne + randomCardTwo;
            ComputerScoreLabel.Content = ScoreComputer.ToString();

            Card1AI.Text = randomcardOne.ToString();
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
            ShowSplitDeck();

            int score = Convert.ToInt16(Card1PL.Text);
            PlayerScoreSplitLeft = score;
            PlayerScoreSplitRight = score;
            PlayerScoreLabel.Content = PlayerScoreSplitLeft + "  :  " + PlayerScoreSplitRight;
        }

        private void RulesButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!RulesWindowIsOpen)
            {
                RulesWindow = new RulesWindow(this);
                RulesWindow.Show();
                RulesWindowIsOpen = true;
            }
        }

        private void HitButtonLeft_OnClick(object sender, RoutedEventArgs e)
        {
            int randomCard = Ran.Next(Min, Max);
            if (randomCard == 11) PlayerAcesLeft.Add(TextBoxPlayerSplitLeft[CurrentCountLeft], randomCard);
            PlayerScoreSplitLeft += randomCard;

            TextBoxPlayerSplitLeft[CurrentCountLeft].Text = randomCard.ToString();
            CurrentCountLeft++;
            PlayerScoreLabel.Content = PlayerScoreSplitLeft + "  :  " + PlayerScoreSplitRight;

            if (CurrentCountLeft >= TextBoxPlayerSplitLeft.Count())
            {
                HitButtonLeft.Visibility = Visibility.Hidden;
                StandButtonLeft.Visibility = Visibility.Hidden;
                PressedStandButtons++;
                CheckDone();
            }
            if (PlayerScoreSplitLeft > 21)
                CheckPlayerCardsForAceLeft();
        }

        private void HitButtonRight_OnClick(object sender, RoutedEventArgs e)
        {
            int randomCard = Ran.Next(Min, Max);
            if (randomCard == 11) PlayerAcesRight.Add(TextBoxPlayerSplitRight[CurrentCountRight], randomCard);
            PlayerScoreSplitRight += randomCard;

            TextBoxPlayerSplitRight[CurrentCountRight].Text = randomCard.ToString();
            CurrentCountRight++;
            PlayerScoreLabel.Content = PlayerScoreSplitRight + "  :  " + PlayerScoreSplitRight;

            if (CurrentCountRight >= TextBoxPlayerSplitRight.Count())
            {
                HitButtonRight.Visibility = Visibility.Hidden;
                StandButtonRight.Visibility = Visibility.Hidden;
                PressedStandButtons++;
                CheckDone();
            }
            if (PlayerScoreSplitRight> 21)
                CheckPlayerCardsForAceRight();
        }

        private void StandButtonLeft_OnClick(object sender, RoutedEventArgs e)
        {
            StandButtonLeft.Visibility = Visibility.Hidden;
            HitButtonLeft.Visibility = Visibility.Hidden;
            PressedStandButtons++;
            CheckDone();
        }

        private void StandButtonRight_OnClick(object sender, RoutedEventArgs e)
        {
            StandButtonRight.Visibility = Visibility.Hidden;
            HitButtonRight.Visibility = Visibility.Hidden;
            PressedStandButtons++;
            CheckDone();
        }
    }
}