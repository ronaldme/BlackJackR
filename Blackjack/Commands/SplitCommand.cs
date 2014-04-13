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
            if (viewModel.BetPlaced && viewModel.DoubleCards)
            {
                return true;
            }
            return false;
        }

        public void Execute(object parameter)
        {

        }
    }
}
