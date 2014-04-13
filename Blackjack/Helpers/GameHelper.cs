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

        /// <summary>
        /// Calculate the winner and return that Player
        /// </summary>
        /// <returns>Return null when both players have the same score</returns>
        public static Player CalculateWinner(Player one, Player two)
        {
            if (one.CurrentScore > two.CurrentImage)
            {
                return one;
            }
            else if (two.CurrentScore > one.CurrentImage)
            {
                return one;
            }

            return two;
        }

        public static void ResetGame(Player one, Player two)
        {
            one.CurrentImage = 2;
            one.PlayerAcesCount = 0;

            two.CurrentImage = 2;
            two.PlayerAcesCount = 0;
        }
    }
}
