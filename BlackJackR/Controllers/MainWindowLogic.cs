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
            EndGameMessageLabel.Content = "Draw!";

            if ((ScoreComputer > ScorePlayer && ScoreComputer <= 21) || ScorePlayer > 21)
            {
                EndGameMessageLabel.Content = "Computer won this round!";
                AddMoney(0, 1);
            }
            else
            {
                EndGameMessageLabel.Content = "You won this round!";
                AddMoney(1, 0);
            }

            // Start EndGame window if player or computer loses
            if (MoneyPlayer <= 0) 
            {
                StartEndGame("You Lost!");
            }
            else if (MoneyComputer <= 0)
            {
                StartEndGame("You won!");
            }

            ResetGame();
        }

        private void StartEndGame(string message)
        {
            Endgame endgame = new Endgame();
            endgame.WinMessageLabel.Content = message;
            endgame.RetryNameLabel.Content = this.NameLabel1.Content;
            endgame.Show();
            this.Close();
        }

        private void IsButtonHidden(Button button)
        {
            if (button.Visibility == Visibility.Hidden)
            {
                StandButton_OnClick(this, new RoutedEventArgs());
            }
        }

        /// <summary>
        /// Enter 1 if you won of 0 if you lost
        /// </summary>
        /// <param name="player"></param>
        /// <param name="computer"></param>
        private void AddMoney(int player, int computer)
        {
            MoneyPlayer += Convert.ToInt16(BetBox.Text) * player;
            MoneyPlayerLabel.Content = "€" + MoneyPlayer.ToString();

            MoneyComputer += Convert.ToInt16(BetBox.Text) * computer;
            MoneyComputerLabel.Content = "€" + MoneyComputer.ToString();
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

        private void AddAcesRight(int card)
        {
            if (card == 11)
            {
                PlayerAcesCountRight++;
            }
        }

        private void AddAcesLeft(int card)
        {
            if (card == 11)
            {
                PlayerAcesCountLeft++;
            }
        }

        private bool PlayerHasAce()
        {
            if (PlayerAcesCount > 0)
            {
                PlayerAcesCount--;
                ScorePlayer -= 10;
                PlayerScoreLabel.Content = ScorePlayer.ToString();

                return true;
            }
            return false;
        }

        private bool ComputerHasAce()
        {
            if (ComputerAcesCount > 0)
            {
                ComputerAcesCount--;
                ScoreComputer -= 10;
                ComputerScoreLabel.Content = ScoreComputer.ToString();

                return true;
            }
            return false;
        }

        private void ChangeSplitDeck(Label label, Button hit, Button stand)
        {
            label.Visibility = Visibility.Visible;
            label.Content = "This deck lost!";
            HitButtonLeft.Visibility = Visibility.Hidden;
            StandButtonLeft.Visibility = Visibility.Hidden;
            MoneyPlayer -= Convert.ToInt16(BetBox.Text);
            MoneyPlayerLabel.Content = "€" + MoneyPlayer;
        }

        private bool CheckForAceSplitLeft()
        {
            if (PlayerAcesCountLeft > 0)
            {
                PlayerAcesCountLeft--;
                PlayerScoreSplitLeft -= 10;
                PlayerScoreLabel.Content = PlayerScoreSplitLeft + "  :  " + PlayerScoreSplitRight;

                return true;
            }
            return false;
        }

        private bool CheckForAceSplitRight()
        {
            if (PlayerAcesCountRight > 0)
            {
                PlayerAcesCountRight--;
                PlayerScoreSplitRight -= 10;
                PlayerScoreLabel.Content = PlayerScoreSplitLeft + "  :  " + PlayerScoreSplitRight;

                return true;
            }
            return false;
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
                    if (ComputerHasAce())
                        Thread.Yield();
                    else
                        CalculateWinner();
                }));
            }
        }
    }
}