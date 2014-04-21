using Blackjack.Models;

namespace Blackjack.Interfaces
{
    public interface IView
    {
        /// <summary>
        /// Add the cards to the players
        /// </summary>
        void AddCards(Player one, Player two);

        /// <summary>
        /// Show a message with the game result
        /// </summary>
        void ShowResult(string result);
        
        /// <summary>
        /// Reset the results
        /// </summary>
        void ResetResult();

        /// <summary>
        /// Display the ammount of money
        /// </summary>
        void DisplayMoney(Player one, Player two);

        /// <summary>
        /// Display the score of the game of the player
        /// </summary>
        void DisplayPoints(Player player);

        /// <summary>
        /// Display the score of the game of the player when in splitdeck
        /// </summary>
        void DisplayPointsSplit(Player player);

        /// <summary>
        /// End the game, update the view and money of the players
        /// </summary>
        void EndGame(Player one, Player two, Player winner, int bet);

        /// <summary>
        /// Activate or deactivate the SplitDeck
        /// </summary>
        void SplitDeck(Player one, bool activate);
    }
}
