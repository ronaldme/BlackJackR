
using System.Collections.Generic;
using System.Windows.Controls;

namespace Blackjack.Models
{
    public class SplitDeck
    {
        public List<Image> ImagesLeft { get; set; }
        public List<Image> ImagesRight { get; set; }
        public int ScoreLeft { get; set; }
        public int ScoreRight { get; set; }
        public int CurrentImageLeft { get; set; }
        public int CurrentImageRight { get; set; }
        public int AcesLeft { get; set; }
        public int AcesRight { get; set; }
        public bool FinishedLeft { get; set; }
        public bool FinishedRight { get; set; }

        public SplitDeck()
        {
            ImagesLeft = new List<Image>();
            ImagesRight = new List<Image>();
        }
    }
}
