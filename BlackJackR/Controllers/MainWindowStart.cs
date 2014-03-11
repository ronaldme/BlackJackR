using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Media.Imaging;

namespace BlackJackR
{
    public partial class MainWindow
    {
        #region Reset
        private void ResetGame()
        {
            DealButton.Visibility = Visibility.Visible;
            SplitButton.Visibility = Visibility.Hidden;
            HitButton.Visibility = Visibility.Hidden;
            StandButton.Visibility = Visibility.Hidden;
            CurrentImagePlayer = 0;
            CurrentImageComputer = 0;
            CurrentCountLeft = 0;
            CurrentCountRight = 0;
            ScorePlayer = 0;
            ScoreComputer = 0;
            PressedStandButtons = 0;
            PlayerScoreSplitLeft = 0;
            PlayerScoreSplitRight = 0;
            BetBox.IsReadOnly = false;
            HasDeckLost = false;
            isSplitDeckActive = false;
        }

        private void ResetImages()
        {
            SplitDeckLeftImages.ForEach(x => x.Visibility = Visibility.Hidden);
            SplitDeckRightImages.ForEach(x => x.Visibility = Visibility.Hidden);
            ImagesPlayer.ForEach(x => x.Visibility = Visibility.Hidden);
            ImagesComputer.ForEach(x => x.Visibility = Visibility.Hidden);
        }
        #endregion

        private void StartGame()
        {
            ResetImages();
            Card1CImage.Source = PlayerImageBack;
            Card2CImage.Source = PlayerImageBack;

            BetBox.IsReadOnly = true;
            PlayerScoreLabel.Content = "0";
            ComputerScoreLabel.Content = "0";
            EndGameMessageLabel.Content = "";
            LabelLeft.Visibility = Visibility.Hidden;
            LabelRight.Visibility = Visibility.Hidden;
            BlackJackLabel.Visibility = Visibility.Hidden;
            DealButton.Visibility = Visibility.Hidden;
            HitButton.Visibility = Visibility.Visible;
            StandButton.Visibility = Visibility.Visible;

            // reset the split deck
            BorderSplit.Visibility = Visibility.Hidden;
            HitButtonLeft.Visibility = Visibility.Hidden;
            HitButtonRight.Visibility = Visibility.Hidden;
            StandButtonLeft.Visibility = Visibility.Hidden;
            StandButtonRight.Visibility = Visibility.Hidden;
        }

        private void ShowSplitDeck()
        {
            BorderSplit.Visibility = Visibility.Visible;
            HitButtonLeft.Visibility = Visibility.Visible;
            HitButtonRight.Visibility = Visibility.Visible;
            StandButtonLeft.Visibility = Visibility.Visible;
            StandButtonRight.Visibility = Visibility.Visible;
            HitButton.Visibility = Visibility.Hidden;
            StandButton.Visibility = Visibility.Hidden;
            SplitButton.Visibility = Visibility.Hidden;
        }
    }
}
