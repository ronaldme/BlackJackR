using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Blackjack.Models
{
    public class Player
    {
        public List<Image> Images { get; set; }
        public BitmapImage ImageBack { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int Money { get; set; }
        public int CurrentScore { get; set; }
        public int PlayerAcesCount { get; set; }
        public int CurrentImage { get; set; }

        public Player(int id, string name, int money, BitmapImage backSide, int currentImage)
        {
            Images = new List<Image>();
            this.Id = id;
            this.Name = name;
            this.Money = money;
            this.ImageBack = backSide;
            this.CurrentImage = currentImage;
        }
    }
}
