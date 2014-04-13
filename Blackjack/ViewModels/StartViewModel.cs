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
        public ICommand UpdateCommand { get; private set; }
        public string PlayerName { get; set; }

        public StartViewModel(StartWindow window)
        {
            startWindow = window;
            UpdateCommand = new StartCommand(this);
        }

        public void StartGame()
        {
            GameWindow game = new GameWindow(PlayerName);
            game.Show();
            startWindow.Close();
        }
    }
}
