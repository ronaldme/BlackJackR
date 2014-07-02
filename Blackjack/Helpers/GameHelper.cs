using Blackjack.Models;

namespace Blackjack.Helpers
{
    public class GameHelper
    {
        public static void AddAces(Player player, int card)
        {
            if (card == 11)
            {
                player.Aces++;
            }
        }

        public static void AddAcesSplit(Player player, int card, bool leftDeck)
        {
            if (card == 11)
            {
                if (leftDeck)
                {
                    player.SplitDeck.AcesLeft++;
                }
                else
                {
                    player.SplitDeck.AcesRight++;
                }
            }
        }

        public static Player CalculateWinner(Player one, Player two)
        {
            if ((one.Score > two.Score || two.Score > 21 ) && one.Score <= 21)
            {
                return one;
            }
            if ((two.Score > one.Score || one.Score > 21) && two.Score <= 21)
            {
                return two;
            }

            return null;
        }

        /// <summary>
        /// When left and right score is higher than computer's score return one
        /// When both scores are lower than computer's score return two
        /// Other outcome is a draw, return null
        /// </summary>
        public static Player CalculateWinnerSplit(Player one, Player two)
        {
            int scoreLeft = one.SplitDeck.ScoreLeft;
            int scoreRight = one.SplitDeck.ScoreRight;

            if (((scoreLeft > two.Score && scoreRight > two.Score) || (two.Score > 21)) && scoreLeft <= 21 && scoreRight <= 21)
            {
                return one;
            }
            if (scoreLeft < two.Score && scoreRight < two.Score && two.Score <= 21)
            {
                return two;
            }

            return null;
        }

        public static bool HasAces(Player player)
        {
            if (player.Aces > 0)
            {
                player.Aces--;
                player.Score -= 10;
                return true;
            }

            return false;
        }

        public static bool HasAcesSplit(Player player, bool leftDeck)
        {
            if (leftDeck)
            {
                if (player.SplitDeck.AcesLeft > 0)
                {
                    player.SplitDeck.AcesLeft--;
                    player.SplitDeck.ScoreLeft -= 10;
                    return true;
                }

                return false;
            }
            
            if (player.SplitDeck.AcesRight > 0)
            {
                player.SplitDeck.AcesRight--;
                player.SplitDeck.ScoreRight -= 10;
                return true;
            }

            return false;
        }

        public static void ResetGame(Player one, Player two)
        {
            one.Reset();
            two.Reset();
            one.ResetSplitDeck();
        }

        public static void ResetImages(Player one, Player two)
        {
            one.Images.ForEach(x => x.Source = null);
            two.Images.ForEach(x => x.Source = null);

            two.Images[0].Source = two.ImageBack;
            two.Images[1].Source = two.ImageBack;
        }
    }
}