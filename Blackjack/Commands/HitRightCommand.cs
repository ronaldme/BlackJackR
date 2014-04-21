using System;
using System.Windows.Input;
using Blackjack.Models;
using Blackjack.ViewModels;

namespace Blackjack.Commands
{
    public class HitRightCommand : ICommand
    {
        private readonly GameViewModel viewModel;

        public HitRightCommand(GameViewModel viewModel)
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
            if (viewModel.Player.SplitDeck.FinishedRight)
            {
                return false;
            }
            return true;
        }

        public void Execute(object parameter)
        {
            viewModel.HitCardSplit(false);

            SplitDeck splitDeck = viewModel.Player.SplitDeck;

            if (splitDeck.CurrentImageRight >= splitDeck.ImagesRight.Count)
            {
                splitDeck.FinishedRight = true;

                if (splitDeck.FinishedLeft)
                {
                    viewModel.Stand(true);
                }
            }
        }
    }
}
