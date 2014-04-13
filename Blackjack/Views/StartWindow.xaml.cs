using System.Windows;
using Blackjack.ViewModels;

namespace Blackjack.Views
{
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
            this.DataContext =  new StartViewModel(this);
        }
    }
}