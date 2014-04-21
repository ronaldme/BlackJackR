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
            SplitDeck = new SplitDeck();
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
    }
}
