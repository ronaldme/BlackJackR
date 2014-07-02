using System.Windows.Input;
using Blackjack.Commands;
using Blackjack.ViewModels;

namespace Blackjack.Models
{
    public class Commands
    {
        public ICommand DealCommand { get; private set; }
        public ICommand HitCommand { get; private set; }
        public ICommand StandCommand { get; private set; }
        public ICommand SplitCommand { get; private set; }
        public ICommand RulesCommand { get; private set; }
        public ICommand HitLeftCommand { get; private set; }
        public ICommand StandLeftCommand { get; private set; }
        public ICommand HitRightCommand { get; private set; }
        public ICommand StandRightCommand { get; private set; }

        public Commands(GameViewModel viewModel)
        {
            DealCommand = new DealCommand(viewModel);
            HitCommand = new HitCommand(viewModel);
            StandCommand = new StandCommand(viewModel);
            SplitCommand = new SplitCommand(viewModel);
            RulesCommand = new RulesCommand(viewModel);
            HitLeftCommand = new HitLeftCommand(viewModel);
            StandLeftCommand = new StandLeftCommand(viewModel);
            HitRightCommand = new HitRightCommand(viewModel);
            StandRightCommand = new StandRightCommand(viewModel);
        }
    }
}
