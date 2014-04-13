using Blackjack.Models;

namespace Blackjack.Interfaces
{
    public interface IView
    {
        /// <summary>
        /// Add the cards to the players
        /// </summary>
        /// <param name="one"></param>
        /// <param name="two"></param>
        void AddCards(Player one, Player two);
        
        /// <summary>
        /// Show a message with the game result
        /// </summary>
        /// <param name="result"></param>
        void ShowResult(string result);
        
        /// <summary>
        /// Reset the results
        /// </summary>
        void ResetResult();

        /// <summary>
        /// Display the ammount of money
        /// </summary>
        /// <param name="player"></param>
        void DisplayMoney(Player player);

        /// <summary>
        /// Display the score of the game of the player
        /// </summary>
        /// <param name="player"></param>
        void DisplayPoints(Player player);
    }
}
