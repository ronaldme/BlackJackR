using System;
using System.Windows.Input;
using Blackjack.ViewModels;
using Blackjack.Views;

namespace Blackjack.Commands
{
    public class RulesCommand : ICommand
    {
        private readonly GameViewModel viewModel;
        private RulesWindow rulesWindow;

        public RulesCommand(GameViewModel viewModel)
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
            if (rulesWindow == null || !rulesWindow.IsVisible)
            {
                return true;
            }
            return false;
        }

        public void Execute(object parameter)
        {
            rulesWindow = new RulesWindow();
            rulesWindow.Show();
        }
    }
}
