using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

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

        private void CheckPlayerCardsForAce()
        {
            if (PlayerAces.Any())
            {
                PlayerAces.Keys.First().Text = "1";
                PlayerAces.Remove(PlayerAces.Keys.First());
                ScorePlayer -= 10;
                PlayerScoreLabel.Content = ScorePlayer.ToString();

                return;
            }

            CalculateWinner();
        }

        private  void CheckComputerCardsForAce()
        {
            if (ComputerAces.Any())
            {
                ComputerAces.Keys.First().Text = "1";
                ComputerAces.Remove(ComputerAces.Keys.First());
                ScoreComputer -= 10;
                ComputerScoreLabel.Content = ScoreComputer.ToString();

                return;
            }

            CalculateWinner();
        }

        private void CheckPlayerCardsForAceLeft()
        {
            if (PlayerAcesLeft.Any())
            {
                PlayerAcesLeft.Keys.First().Text = "1";
                PlayerAcesLeft.Remove(PlayerAcesLeft.Keys.First());
                PlayerScoreSplitLeft -= 10;

                PlayerScoreLabel.Content = PlayerScoreSplitLeft + "  :  " + PlayerScoreSplitRight;
                return;
            }

            LabelLeft.Visibility = Visibility.Visible;
            LabelLeft.Content = "Left deck lost!";
            HitButtonLeft.Visibility = Visibility.Hidden;
            StandButtonLeft.Visibility = Visibility.Hidden;

            if (HasDeckLost == true)
            {
                ResetGame();
            }
            HasDeckLost = true;
        }

        private void CheckPlayerCardsForAceRight()
        {
            if (PlayerAcesRight.Any())
            {
                PlayerAcesRight.Keys.First().Text = "1";
                PlayerAcesRight.Remove(PlayerAcesRight.Keys.First());
                PlayerScoreSplitRight -= 10;

                PlayerScoreLabel.Content = PlayerScoreSplitLeft + "  :  " + PlayerScoreSplitRight;
                return;
            }

            LabelRight.Visibility = Visibility.Visible;
            LabelRight.Content = "Right deck lost!";
            HitButtonRight.Visibility = Visibility.Hidden;
            StandButtonRight.Visibility = Visibility.Hidden;

            if (HasDeckLost == true)
            {
                ResetGame();
            }
            HasDeckLost = true;
        }

        private void CalculateComputerCard()
        {
            while (ScoreComputer < 17 && CurrentTextBoxComputer < TextBoxComputer.Count())
            {
                // Let the computer act like it's thinking
                Thread.Sleep(1000);
                int randomCard = Ran.Next(Min, Max);
                if (randomCard == 11) ComputerAces.Add(TextBoxComputer[CurrentTextBoxComputer], randomCard);

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

        private void CheckDone()
        {
            if (PressedStandButtons == 2)
            {
                StandButton_OnClick(this, new RoutedEventArgs());
            }
        }
    }
}
