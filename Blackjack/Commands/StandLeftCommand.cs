using System;
using System.Windows.Input;
using Blackjack.ViewModels;

namespace Blackjack.Commands
{
    public class StandLeftCommand : ICommand
    {
        private readonly GameViewModel viewModel;

        public StandLeftCommand(GameViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (viewModel.Player.SplitDeck.FinishedLeft)
            {
                return false;
            }
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.Player.SplitDeck.FinishedLeft = true;
            if (viewModel.Player.SplitDeck.FinishedRight)
            {
                viewModel.Stand(true);
            }
        }
    }
}
