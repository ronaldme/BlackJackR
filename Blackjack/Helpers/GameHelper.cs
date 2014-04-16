using Blackjack.Models;

namespace Blackjack.Helpers
{
    public class GameHelper
    {
        public static void AddAces(Player player, int card)
        {
            if (card == 11)
            {
                player.PlayerAcesCount++;
            }
        }

        public static Player CalculateWinner(Player one, Player two)
        {
            if ((one.CurrentScore > two.CurrentScore || two.CurrentScore > 21 ) && one.CurrentScore <= 21)
            {
                return one;
            }
            else if ((two.CurrentScore > one.CurrentScore || one.CurrentScore > 21) && two.CurrentScore <= 21)
            {
                return two;
            }

            return null;
        }

        public static bool HasAces(Player player)
        {
            if (player.PlayerAcesCount > 0)
            {
                player.PlayerAcesCount--;
                player.CurrentScore -= 10;

                return true;
            }
            return false;
        }

        public static void ResetGame(Player one, Player two)
        {
            one.CurrentImage = 2;
            one.PlayerAcesCount = 0;
            one.CurrentScore = 0;

            two.CurrentImage = 2;
            two.PlayerAcesCount = 0;
            two.CurrentScore = 0;
        }

        public static void ResetImages(Player one, Player two)
        {
            one.Images.ForEach(x => x.Source = null);
            two.Images.ForEach(x => x.Source = null);
        }
    }
}
