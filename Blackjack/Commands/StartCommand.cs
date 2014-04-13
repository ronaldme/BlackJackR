using System;
using System.Windows.Input;
using Blackjack.ViewModels;

namespace Blackjack.Commands
{
    public class StartCommand : ICommand
    {
        private readonly StartViewModel viewModel;

        public StartCommand(StartViewModel model)
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
            if (!string.IsNullOrWhiteSpace(viewModel.PlayerName))
            {
                return true;
            }
            return false;
        }

        public void Execute(object parameter)
        {
            viewModel.StartGame();
        }
    }
}