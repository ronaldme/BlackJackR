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
            // Divide by two is the value of one of the double cards
            int score = viewModel.Player.Score / 2;
            viewModel.Player.SplitDeck.ScoreLeft = score;
            viewModel.Player.SplitDeck.ScoreRight= score;

            viewModel.View.SplitDeck(viewModel.Player, true);
            viewModel.View.DisplayPointsSplit(viewModel.Player);
            viewModel.SplitDeck = true;
        }
    }
}
