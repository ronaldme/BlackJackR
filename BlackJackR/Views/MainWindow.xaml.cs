using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
                int randomCardOne = Ran.Next(2, 12);
                int randomCardTwo = Ran.Next(2, 12);

                AddAces(randomCardOne, true);
                AddAces(randomCardTwo, true);
                Card1PImage.Source = CardImages.First(x => x.Value == randomCardOne).Key;
                Card2PImage.Source = CardImages.First(x => x.Value == randomCardTwo).Key;

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
                    
                    if (randomCardOne == 11)
                    {
                        PlayerAcesCountLeft++;
                        PlayerAcesCountRight++;
                    }
                }
            }
        }

        private void HitButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (ScorePlayer > 21)
                CheckPlayerCardsForAce();

            SplitButton.Visibility = Visibility.Hidden;

            int randomCard = Ran.Next(2, 12);
            AddAces(randomCard, true);
            
            ScorePlayer += randomCard;
            PlayerScoreLabel.Content = ScorePlayer.ToString();
            ImagesPlayer[CurrentImagePlayer].Visibility = Visibility.Visible;
            ImagesPlayer[CurrentImagePlayer].Source = CardImages.First(x => x.Value == randomCard).Key;
            CurrentImagePlayer++;
             
            if (ScorePlayer > 21)
            {
                CheckPlayerCardsForAce();
            }
            if (CurrentImagePlayer >= ImagesPlayer.Count())
            {
                HitButton.Visibility = Visibility.Hidden;
                StandButton_OnClick(sender, new RoutedEventArgs());
            }
        }

        private void StandButton_OnClick(object sender, RoutedEventArgs e)
        {
            HitButton.Visibility = Visibility.Hidden;

            int randomCardOne = Ran.Next(2, 12);
            int randomCardTwo = Ran.Next(2, 12);
            AddAces(randomCardOne, false);
            AddAces(randomCardTwo, false);

            Card1CImage.Source = CardImages.First(x => x.Value == randomCardOne).Key;
            Card2CImage.Source = CardImages.First(x => x.Value == randomCardTwo).Key;
            ScoreComputer = randomCardOne + randomCardTwo;
            ComputerScoreLabel.Content = ScoreComputer.ToString();
            
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

            //int score = Convert.ToInt16(Card1PL.Text);
            //PlayerScoreSplitLeft = score;
            //PlayerScoreSplitRight = score;
            PlayerScoreLabel.Content = PlayerScoreSplitLeft + "  :  " + PlayerScoreSplitRight;
        }

        private void HitButtonLeft_OnClick(object sender, RoutedEventArgs e)
        {
            int randomCard = Ran.Next(2, 12);

           // if (randomCard == 11) PlayerAcesLeft.Add(TextBoxPlayerSplitLeft[CurrentCountLeft], randomCard);
            PlayerScoreSplitLeft += randomCard;

            //TextBoxPlayerSplitLeft[CurrentCountLeft].Text = randomCard.ToString();
            CurrentCountLeft++;
            PlayerScoreLabel.Content = PlayerScoreSplitLeft + "  :  " + PlayerScoreSplitRight;
            /*
            if (CurrentCountLeft >= TextBoxPlayerSplitLeft.Count())
            {
                HitButtonLeft.Visibility = Visibility.Hidden;
                StandButtonLeft.Visibility = Visibility.Hidden;
                PressedStandButtons++;
                CheckDone();
            }
            if (PlayerScoreSplitLeft > 21)
                CheckPlayerCardsForAceLeft();
             */
        }

        private void HitButtonRight_OnClick(object sender, RoutedEventArgs e)
        {
            int randomCard = Ran.Next(2, 12);
            //if (randomCard == 11) PlayerAcesRight.Add(TextBoxPlayerSplitRight[CurrentCountRight], randomCard);
            PlayerScoreSplitRight += randomCard;

            //TextBoxPlayerSplitRight[CurrentCountRight].Text = randomCard.ToString();
            CurrentCountRight++;
            PlayerScoreLabel.Content = PlayerScoreSplitRight + "  :  " + PlayerScoreSplitRight;
/*
            if (CurrentCountRight >= TextBoxPlayerSplitRight.Count())
            {
                HitButtonRight.Visibility = Visibility.Hidden;
                StandButtonRight.Visibility = Visibility.Hidden;
                PressedStandButtons++;
                CheckDone();
            }
 */
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

        private void RulesButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!RulesWindowIsOpen)
            {
                RulesWindow = new RulesWindow(this);
                RulesWindow.Show();
                RulesWindowIsOpen = true;
            }
        }
    }
}