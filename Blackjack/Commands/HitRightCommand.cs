﻿using System;
using System.Windows.Input;
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

            if (viewModel.Player.SplitDeck.CurrentImageRight >= viewModel.Player.SplitDeck.ImagesRight.Count)
            {
                viewModel.Player.SplitDeck.FinishedRight = true;

                if (viewModel.Player.SplitDeck.FinishedLeft)
                {
                    viewModel.Stand();
                }
            }
        }
    }
}
