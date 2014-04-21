using System;
using System.Windows.Input;
using Blackjack.ViewModels;

namespace Blackjack.Commands
{
    public class SplitCommand : ICommand
    {
        private readonly GameViewModel viewModel;

        public SplitCommand(GameViewModel model)
        {
            viewModel = model;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (viewModel.BetPlaced && viewModel.DoubleCards && viewModel.Player.CurrentImage < 3)
            {
                return true;
            }
            return false;
        }

        public void Execute(object parameter)
        {
            int score = 0;
            // Divide by two is the value of one of the double cards
            // Or if we have aces then score == 11
            if (viewModel.Player.Aces <= 0)
            {
                score = viewModel.Player.Score/2;
            }
            else
            {
                score = 11;
                viewModel.Player.SplitDeck.AcesLeft = 1;
                viewModel.Player.SplitDeck.AcesRight = 1;
            }
            viewModel.Player.SplitDeck.ScoreLeft = score;
            viewModel.Player.SplitDeck.ScoreRight= score;

            viewModel.View.SplitDeck(viewModel.Player, true);
            viewModel.View.DisplayPointsSplit(viewModel.Player);
            viewModel.SplitDeck = true;
        }
    }
}
