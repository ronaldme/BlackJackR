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
                int randomCardOne = 11;//Ran.Next(2, 12);
                int randomCardTwo = 11;//Ran.Next(2, 12);

                AddAces(randomCardOne, true);
                AddAces(randomCardTwo, true);

                Card1PImage.Source = PickRandomCard(CardImages.First(x => x.Key == randomCardOne).Value);
                Card2PImage.Source = PickRandomCard(CardImages.First(x => x.Key == randomCardTwo).Value);

                ScorePlayer = randomCardOne + randomCardTwo;
                PlayerScoreLabel.Content = ScorePlayer.ToString();

                if (ScorePlayer > 21)
                {
                    PlayerHasAce();
                }
                if (ScorePlayer == 21)
                {
                    BlackJackLabel.Visibility = Visibility.Visible;
                    HitButton.Visibility = Visibility.Hidden;
                }
                else if (randomCardOne == randomCardTwo)
                {
                    SplitButton.Visibility = Visibility.Visible;
                    SplitValue = randomCardOne;
                    
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
                PlayerHasAce();

            SplitButton.Visibility = Visibility.Hidden;

            int randomCard = Ran.Next(2, 12);
            AddAces(randomCard, true);
            
            ScorePlayer += randomCard;
            PlayerScoreLabel.Content = ScorePlayer.ToString();
            ImagesPlayer[CurrentImagePlayer].Visibility = Visibility.Visible;
            ImagesPlayer[CurrentImagePlayer].Source = PickRandomCard(CardImages.First(x => x.Key == randomCard).Value);
            CurrentImagePlayer++;
             
            if (ScorePlayer > 21)
            {
                PlayerHasAce();
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
            StandButton.Visibility = Visibility.Hidden;
            SplitButton.Visibility = Visibility.Hidden;

            int randomCardOne = Ran.Next(2, 12);
            int randomCardTwo = Ran.Next(2, 12);
            AddAces(randomCardOne, false);
            AddAces(randomCardTwo, false);

            Card1CImage.Source = PickRandomCard(CardImages.First(x => x.Key == randomCardOne).Value);
            Card2CImage.Source = PickRandomCard(CardImages.First(x => x.Key == randomCardTwo).Value);
            ScoreComputer = randomCardOne + randomCardTwo;
            ComputerScoreLabel.Content = ScoreComputer.ToString();

            if (ScoreComputer > 21 && !ComputerHasAce())
                CalculateWinner();
            if (ScoreComputer < 17)
                new Thread(CalculateComputerCard).Start();
            else
                CalculateWinner();
        }

        private void SplitButton_OnClick(object sender, RoutedEventArgs e)
        {
            ShowSplitDeck();
            isSplitDeckActive = true;

            PlayerScoreSplitLeft = SplitValue;
            PlayerScoreSplitRight = SplitValue;
            PlayerScoreLabel.Content = PlayerScoreSplitLeft + "  :  " + PlayerScoreSplitRight;
        }

        private void HitButtonLeft_OnClick(object sender, RoutedEventArgs e)
        {
            int randomCard = Ran.Next(2, 12);

            AddAcesLeft(randomCard);
            PlayerScoreSplitLeft += randomCard;

            SplitDeckLeftImages[CurrentCountLeft].Visibility = Visibility.Visible;
            SplitDeckLeftImages[CurrentCountLeft].Source = PickRandomCard(CardImages.First(x => x.Key == randomCard).Value);
            CurrentCountLeft++;

            PlayerScoreLabel.Content = PlayerScoreSplitLeft + "  :  " + PlayerScoreSplitRight;
            
            if (CurrentCountLeft >= SplitDeckLeftImages.Count())
            {
                HitButtonLeft.Visibility = Visibility.Hidden;
                StandButtonLeft.Visibility = Visibility.Hidden;
                IsButtonHidden(HitButtonRight);
            }
            if (PlayerScoreSplitLeft > 21 && !CheckForAceSplitLeft())
            {
                ChangeSplitDeck(LabelLeft, HitButtonLeft, StandButtonLeft);
                IsButtonHidden(HitButtonRight);
            }
        }

        private void HitButtonRight_OnClick(object sender, RoutedEventArgs e)
        {
            int randomCard = Ran.Next(2, 12);

            AddAcesRight(randomCard);
            PlayerScoreSplitRight += randomCard;

            SplitDeckRightImages[CurrentCountRight].Visibility = Visibility.Visible;
            SplitDeckRightImages[CurrentCountRight].Source = PickRandomCard(CardImages.First(x => x.Key == randomCard).Value);
            CurrentCountRight++;

            PlayerScoreLabel.Content = PlayerScoreSplitLeft + "  :  " + PlayerScoreSplitRight;

            if (CurrentCountRight >= SplitDeckRightImages.Count())
            {
                HitButtonRight.Visibility = Visibility.Hidden;
                StandButtonRight.Visibility = Visibility.Hidden;
                IsButtonHidden(HitButtonLeft);
            }
            if (PlayerScoreSplitRight > 21 && !CheckForAceSplitRight())
            {
                ChangeSplitDeck(LabelLeft, HitButtonLeft, StandButtonLeft);
                IsButtonHidden(HitButtonLeft);
            }
        }

        private void StandButtonLeft_OnClick(object sender, RoutedEventArgs e)
        {
            StandButtonLeft.Visibility = Visibility.Hidden;
            HitButtonLeft.Visibility = Visibility.Hidden;
            IsButtonHidden(HitButtonRight);
        }

        private void StandButtonRight_OnClick(object sender, RoutedEventArgs e)
        {
            StandButtonRight.Visibility = Visibility.Hidden;
            HitButtonRight.Visibility = Visibility.Hidden;
            IsButtonHidden(HitButtonLeft);
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