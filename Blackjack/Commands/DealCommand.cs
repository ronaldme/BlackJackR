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

            try
            {
                int bet = Convert.ToInt16(viewModel.BetAmount);
                if (bet >= 100 && bet <= 500)
                {
                    return true;
                }
            }
            catch (FormatException formatException)
            {
                return false;
            }
            catch (OverflowException overflowException)
            {
                return false;
            }

            return false;
        }

        public void Execute(object parameter)
        {
            viewModel.DealCards();
        }
    }
}