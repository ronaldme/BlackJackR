using System;
using System.Windows.Input;
using Blackjack.ViewModels;

namespace Blackjack.Commands
{
    public class DealCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private readonly GameViewModel viewModel;

        public DealCommand(GameViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            if (viewModel.BetPlaced)
            {
                return false;
            }

            int bet;
            bool parsed = Int32.TryParse(viewModel.BetAmount, out bet);

            if (parsed)
            {
                if (bet >= 100 && bet <= 500)
                {
                    return true;
                }
                
            }
            return false;
        }

        public void Execute(object parameter)
        {
            viewModel.DealCards();
        }
    }
}