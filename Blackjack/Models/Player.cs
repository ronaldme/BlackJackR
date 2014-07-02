using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Blackjack.Models
{
    public class Player
    {
        public List<Image> Images { get; set; }
        public BitmapImage ImageBack { get; set; }
        public SplitDeck SplitDeck { get; set; }
        public string Name { get; set; }
        public int Money { get; set; }
        public int Score { get; set; }
        public int Aces { get; set; }
        public int CurrentImage { get; set; }

        public Player(string name, int money, BitmapImage backSide, int currentImage)
        {
            Images = new List<Image>();
            Name = name;
            Money = money;
            ImageBack = backSide;
            CurrentImage = currentImage;
        }

        public void ShowBackside()
        {
            Images[0].Source = ImageBack;
            Images[1].Source = ImageBack;
        }

        public void CreateSplitDeck()
        {
            SplitDeck = new SplitDeck();
        }

        public void Reset()
        {
            CurrentImage = 2;
            Aces = 0;
            Score = 0;
        }

        public void ResetSplitDeck()
        {
            SplitDeck.AcesLeft = 0;
            SplitDeck.AcesRight = 0;
            SplitDeck.ScoreLeft = 0;
            SplitDeck.ScoreRight = 0;
            SplitDeck.CurrentImageLeft = 0;
            SplitDeck.CurrentImageRight = 0;
            SplitDeck.FinishedLeft = false;
            SplitDeck.FinishedRight = false;
        }
    }
}