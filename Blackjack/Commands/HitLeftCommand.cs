using System;
using System.Windows.Input;
using Blackjack.ViewModels;

namespace Blackjack.Commands
{
    public class HitLeftCommand : ICommand
    {
        private readonly GameViewModel viewModel;

        public HitLeftCommand(GameViewModel viewModel)
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
        }
    }
}
