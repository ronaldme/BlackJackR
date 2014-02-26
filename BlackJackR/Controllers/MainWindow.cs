using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Media.Imaging;

namespace BlackJackR
{
    /// <summary>
    /// Initialize BlackJackR game. Set all Properties and Initialize the MainWindow.
    /// This class also adds all the cards and TextBoxes to the Lists.
    /// </summary>
    public partial class MainWindow
    {
        private Random Ran { get; set; }
        private RulesWindow RulesWindow { get; set; }
        private Dictionary<BitmapImage, int> CardImages { get; set; }
        private List<Image> ImagesPlayer { get; set; }
        private List<Image> ImagesComputer { get; set; }
        private NotificationWindow Notification { get; set; }
        private BitmapImage PlayerImageBack { get; set; }
        private BitmapImage ComputerImageBack { get; set; }
        private int PlayerAcesCount { get; set; }
        private int ComputerAcesCount { get; set; }
        private int MoneyComputer { get; set; }
        private int MoneyPlayer { get; set; }
        private int ScorePlayer { get; set; }
        private int ScoreComputer { get; set; }
        private int CurrentImagePlayer { get; set; }
        private int CurrentImageComputer { get; set; }
        public bool NotificationIsOpen { get; set; }
        public bool RulesWindowIsOpen { get; set; }

        // For split deck
        private int PlayerAcesCountLeft { get; set; }
        private int PlayerAcesCountRight { get; set; }
        private int PlayerScoreSplitLeft { get; set; }
        private int PlayerScoreSplitRight { get; set; }
        private int PressedStandButtons { get; set; }
        private int CurrentCountLeft { get; set; }
        private int CurrentCountRight { get; set; }
        private bool HasDeckLost { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Title = "BlackJackR - Reach €2000 to win!";
            MoneyComputer = 1000;
            MoneyPlayer = 1000;

            MoneyPlayerLabel.Content = "€" + MoneyPlayer.ToString();
            MoneyComputerLabel.Content = "€" + MoneyComputer.ToString();

            CardImages = new Dictionary<BitmapImage, int>();
            ImagesPlayer = new List<Image>();
            ImagesComputer = new List<Image>();
            Ran = new Random();

            AddBlackJackCards();
            InitImages();

            PlayerImageBack = CreateImage("backplayer");
            ComputerImageBack = CreateImage("backcomputer");
            Card1PImage.Source = PlayerImageBack;
            Card2PImage.Source = PlayerImageBack;
            Card1CImage.Source = PlayerImageBack;
            Card2CImage.Source = PlayerImageBack;
        }

        private BitmapImage CreateImage(string imageName)
        {
            return new BitmapImage(new Uri("/Images/" + imageName + ".png", UriKind.Relative));
        }

        private void AddBlackJackCards()
        {
            CardImages.Add(CreateImage("h2"), 2);
            CardImages.Add(CreateImage("h3"), 3);
            CardImages.Add(CreateImage("h4"), 4);
            CardImages.Add(CreateImage("h5"), 5);
            CardImages.Add(CreateImage("h6"), 6);
            CardImages.Add(CreateImage("h7"), 7);
            CardImages.Add(CreateImage("h8"), 8);
            CardImages.Add(CreateImage("h9"), 9);
            CardImages.Add(CreateImage("h10"), 10);
            CardImages.Add(CreateImage("hj"), 10);
            CardImages.Add(CreateImage("hk"), 10);
            CardImages.Add(CreateImage("hq"), 10);
            CardImages.Add(CreateImage("h1"), 11);
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

            //ShowTextInTextBoxArray(TextBoxPlayerSplitLeft);
            //ShowTextInTextBoxArray(TextBoxPlayerSplitRight);
        }

        private void InitImages()
        {
            ImagesPlayer.Add(Card3PImage);
            ImagesPlayer.Add(Card4PImage);
            ImagesPlayer.Add(Card5PImage);
            ImagesPlayer.Add(Card6PImage);

            ImagesComputer.Add(Card3CImage);
            ImagesComputer.Add(Card4CImage);
            ImagesComputer.Add(Card5CImage);
            ImagesComputer.Add(Card6CImage);
        }

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
            BetBox.IsReadOnly = false;
            HasDeckLost = false;
        }

        private void ResetImages()
        {
            ImagesPlayer.ForEach(x => x.Visibility = Visibility.Hidden);
            ImagesComputer.ForEach(x => x.Visibility = Visibility.Hidden);
        }

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
    }
}