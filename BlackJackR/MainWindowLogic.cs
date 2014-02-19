using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
