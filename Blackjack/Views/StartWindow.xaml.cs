using Blackjack.ViewModels;

namespace Blackjack.Views
{
    public partial class StartWindow
    {
        public StartWindow()
        {
            InitializeComponent();
            this.DataContext =  new StartViewModel(this);
        }
    }
}