using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BlackJackR
{
    public partial class MainWindow
    {
        private bool CheckUserInput()
        {
            string betAmount = BetBox.Text;
            try
            {
                int bet = Convert.ToInt16(betAmount);

                if (bet < 100 || bet > 500)
                {
                    Notification = new NotificationWindow(this);
                    Notification.Show();
                    return false;
                }

                return true;
            }
            catch (FormatException exception)
            {
                Notification = new NotificationWindow(this);
                Notification.Show();
                return false;
            }
        }

        private BitmapImage PickRandomCard(BitmapImage[] images)
        {
            int random = Ran.Next(0, images.Count());

            return images[random];
        }

        private void CalculateWinner()
        {
            if (ScoreComputer == ScorePlayer)
            {
                EndGameMessageLabel.Content = "Draw!";
            }
            else if ((ScoreComputer > ScorePlayer && ScoreComputer <= 21) || ScorePlayer > 21)
            {
                EndGameMessageLabel.Content = "Computer won this round!";
                AddMoneyToPlayer(false);
            }
            else
            {
                EndGameMessageLabel.Content = "You won this round!";
                AddMoneyToPlayer(true);
            }

            CheckMoney();
            ResetGame();
        }

        private void CheckMoney()
        {
            if (MoneyPlayer <= 0)
            {
                Endgame endgame = new Endgame();
                endgame.WinMessageLabel.Content = "You lost!";
                endgame.RetryNameLabel.Content = this.NameLabel1.Content;
                endgame.Show();
                this.Close();
            }
            else if (MoneyComputer <= 0)
            {
                Endgame endgame = new Endgame();
                endgame.WinMessageLabel.Content = "You won!";
                endgame.RetryNameLabel.Content = this.NameLabel1.Content;
                endgame.Show();
                this.Close();
            }
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

        private void AddAces(int card, bool player)
        {
            if (card == 11)
            {
                if (player)
                    PlayerAcesCount++;
                else
                    ComputerAcesCount++;
            }
        }

        private void CheckPlayerCardsForAce()
        {
            if (PlayerAcesCount > 0)
            {
                PlayerAcesCount--;
                ScorePlayer -= 10;
                PlayerScoreLabel.Content = ScorePlayer.ToString();

                return;
            }

            CalculateWinner();
        }

        private  void CheckComputerCardsForAce()
        {
            if (ComputerAcesCount > 0)
            {
                ComputerAcesCount--;
                ScoreComputer -= 10;
                ComputerScoreLabel.Content = ScoreComputer.ToString();

                return;
            }

            CalculateWinner();
        }

        private void CheckForAceSplitLeft()
        {
            if (PlayerAcesCountLeft > 0)
            {
                PlayerAcesCountLeft--;
                PlayerScoreSplitLeft -= 10;
                PlayerScoreLabel.Content = PlayerScoreSplitLeft + "  :  " + PlayerScoreSplitRight;

                return;
            }
            ChangeDeckLeft();
        }

        private void ChangeDeckLeft()
        {
            LabelLeft.Visibility = Visibility.Visible;
            LabelLeft.Content = "Left deck lost!";
            HitButtonLeft.Visibility = Visibility.Hidden;
            StandButtonLeft.Visibility = Visibility.Hidden;
            MoneyPlayer -= Convert.ToInt16(BetBox.Text);
            MoneyPlayerLabel.Content = "€" + MoneyPlayer;

            if (HasDeckLost == true)
            {
                ResetGame();
            }
            HasDeckLost = true;
        }

        private void CheckForAceSplitRight()
        {
            if (PlayerAcesCountRight > 0)
            {
                PlayerAcesCountRight--;
                PlayerScoreSplitRight-= 10;
                PlayerScoreLabel.Content = PlayerScoreSplitLeft + "  :  " + PlayerScoreSplitRight;

                return;
            }
            ChangeDeckRight();
        }

        private void ChangeDeckRight()
        {
            LabelRight.Visibility = Visibility.Visible;
            LabelRight.Content = "Right deck lost!";
            HitButtonRight.Visibility = Visibility.Hidden;
            StandButtonRight.Visibility = Visibility.Hidden;
            MoneyPlayer -= Convert.ToInt16(BetBox.Text);
            MoneyPlayerLabel.Content = "€" + MoneyPlayer;

            if (HasDeckLost == true)
            {
                ResetGame();
            }
            HasDeckLost = true;
        }

        private void CalculateComputerCard()
        {
            while (ScoreComputer < 17 && CurrentImageComputer < ImagesComputer.Count())
            {
                // Let the computer act like it's thinking
                Thread.Sleep(1000);
                int randomCard = Ran.Next(2, 11);
                AddAces(randomCard, false);
                
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
                ImagesComputer[CurrentImageComputer].Visibility = Visibility.Visible;
                ImagesComputer[CurrentImageComputer].Source = PickRandomCard(CardImages.First(x => x.Key == card).Value);
                CurrentImageComputer++;
                ComputerScoreLabel.Content = ScoreComputer.ToString();
            }));

            if (ScoreComputer > 21)
            {
                this.Dispatcher.Invoke((Action)(() =>
                {
                    if (ComputerAcesCount > 0)
                        CheckComputerCardsForAce();
                    else
                        Thread.Yield();
                }));
            }
        }

        private void CheckDone()
        {
            if (PressedStandButtons == 2)
            {
                StandButton_OnClick(this, new RoutedEventArgs());
            }
        }
    }
}