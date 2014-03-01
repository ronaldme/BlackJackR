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
        private Dictionary<int, BitmapImage[]> CardImages { get; set; }
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

        // Split deck
        private List<Image> SplitDeckLeftImages { get; set; }
        private List<Image> SplitDeckRightImages { get; set; }
        private int PlayerAcesCountLeft { get; set; }
        private int PlayerAcesCountRight { get; set; }
        private int PlayerScoreSplitLeft { get; set; }
        private int PlayerScoreSplitRight { get; set; }
        private int PressedStandButtons { get; set; }
        private int CurrentCountLeft { get; set; }
        private int CurrentCountRight { get; set; }
        private bool HasDeckLost { get; set; }
        private int SplitValue { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Title = "BlackJackR - Reach €2000 to win!";
            MoneyComputer = 1000;
            MoneyPlayer = 1000;

            MoneyPlayerLabel.Content = "€" + MoneyPlayer.ToString();
            MoneyComputerLabel.Content = "€" + MoneyComputer.ToString();

            CardImages = new Dictionary<int, BitmapImage[]>();
            ImagesPlayer = new List<Image>();
            ImagesComputer = new List<Image>();
            SplitDeckLeftImages = new List<Image>();
            SplitDeckRightImages = new List<Image>();
            Ran = new Random();

            AddBlackJackCards();
            InitImages();
            InitImagesSplitDeck();

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

        private BitmapImage[] CreateImagesArray(BitmapImage one, BitmapImage two, BitmapImage three, BitmapImage four)
        {
            return new BitmapImage[4] { one, two, three, four };
        }

        private void AddBlackJackCards()
        {
            CardImages.Add(2, CreateImagesArray(CreateImage("h2"), CreateImage("c2"), CreateImage("d2"), CreateImage("s2")));
            CardImages.Add(3, CreateImagesArray(CreateImage("h3"), CreateImage("c3"), CreateImage("d3"), CreateImage("s3")));
            CardImages.Add(4, CreateImagesArray(CreateImage("h4"), CreateImage("c4"), CreateImage("d4"), CreateImage("s4")));
            CardImages.Add(5, CreateImagesArray(CreateImage("h5"), CreateImage("c5"), CreateImage("d5"), CreateImage("s5")));
            CardImages.Add(6, CreateImagesArray(CreateImage("h6"), CreateImage("c6"), CreateImage("d6"), CreateImage("s6")));
            CardImages.Add(7, CreateImagesArray(CreateImage("h7"), CreateImage("c7"), CreateImage("d7"), CreateImage("s7")));
            CardImages.Add(8, CreateImagesArray(CreateImage("h8"), CreateImage("c8"), CreateImage("d8"), CreateImage("s8")));
            CardImages.Add(9, CreateImagesArray(CreateImage("h9"), CreateImage("c9"), CreateImage("d9"), CreateImage("s9")));
            CardImages.Add(11, CreateImagesArray(CreateImage("h1"), CreateImage("c1"), CreateImage("d1"), CreateImage("s1")));

            BitmapImage[] tenCards = new BitmapImage[16] { CreateImage("h10"), CreateImage("c10"), CreateImage("d10"),
                                        CreateImage("s10"), CreateImage("hj"), CreateImage("cj"), CreateImage("dj"), CreateImage("sj"), CreateImage("hq"),
                                        CreateImage("cq"), CreateImage("dq"), CreateImage("sq"), CreateImage("hk"), CreateImage("ck"), CreateImage("dk"),
                                        CreateImage("sk") };
            CardImages.Add(10, tenCards);
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

        private void InitImagesSplitDeck()
        {
            SplitDeckLeftImages.Add(Card3PImage);
            SplitDeckLeftImages.Add(Card5PImage);
            SplitDeckLeftImages.Add(CardLeft1);
            SplitDeckLeftImages.Add(CardLeft2);
            SplitDeckLeftImages.Add(CardLeft3);

            SplitDeckRightImages.Add(Card4PImage);
            SplitDeckRightImages.Add(Card6PImage);
            SplitDeckRightImages.Add(CardRight1);
            SplitDeckRightImages.Add(CardRight2);
            SplitDeckRightImages.Add(CardRight3);
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
            PlayerScoreSplitLeft = 0;
            PlayerScoreSplitRight = 0;
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