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
        #region Properties
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
        #endregion

        #region SplitDeck properties
        private List<Image> SplitDeckLeftImages { get; set; }
        private List<Image> SplitDeckRightImages { get; set; }
        private bool isSplitDeckActive { get; set; }
        private int PlayerAcesCountLeft { get; set; }
        private int PlayerAcesCountRight { get; set; }
        private int PlayerScoreSplitLeft { get; set; }
        private int PlayerScoreSplitRight { get; set; }
        private int PressedStandButtons { get; set; }
        private int CurrentCountLeft { get; set; }
        private int CurrentCountRight { get; set; }
        private bool HasDeckLost { get; set; }
        private int SplitValue { get; set; }
        #endregion

        #region Initialization
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

            CardImages.Add(10, new BitmapImage[16] { CreateImage("h10"), CreateImage("c10"), CreateImage("d10"),
                                        CreateImage("s10"), CreateImage("hj"), CreateImage("cj"), CreateImage("dj"), CreateImage("sj"), CreateImage("hq"),
                                        CreateImage("cq"), CreateImage("dq"), CreateImage("sq"), CreateImage("hk"), CreateImage("ck"), CreateImage("dk"),
                                        CreateImage("sk") });
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
        #endregion
    }
}