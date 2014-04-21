using System.ComponentModel;
using System.Windows.Input;
using Blackjack.Commands;
using Blackjack.Views;

namespace Blackjack.ViewModels
{
    public class StartViewModel : INotifyPropertyChanged
    {
        private readonly StartWindow startWindow;

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand StartCommand { get; private set; }
        public string Name { get; set; }

        public StartViewModel(StartWindow window)
        {
            startWindow = window;
            StartCommand = new StartCommand(this);
        }

        public void StartGame()
        {
            GameWindow game = new GameWindow(Name);
            game.Show();
            startWindow.Close();
        }
    }
}