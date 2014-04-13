using System;
using System.Windows.Input;
using Blackjack.ViewModels;

namespace Blackjack.Commands
{
    public class HitCommand : ICommand
    {
        private readonly GameViewModel viewModel;

        public HitCommand(GameViewModel model)
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
            if (viewModel.Player.CurrentImage >= viewModel.Player.Images.Count)
            {
                viewModel.Stand();
                return false;
            }
            else if (viewModel.BetPlaced)
            {
                return true;
            }
           
            return false;
        }

        public void Execute(object parameter)
        {
            viewModel.HitCard();
        }
    }
}
