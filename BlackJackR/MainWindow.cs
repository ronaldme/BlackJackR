using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace BlackJackR
{
    /// <summary>
    /// Initialize BlackJackR game. Set all Properties and Initialize the MainWindow.
    /// This class also adds all the cards and TextBoxes to the Lists.
    /// </summary>
    public partial class MainWindow
    {
        private int MoneyComputer { get; set; }
        private int MoneyPlayer { get; set; }
        private int ScorePlayer { get; set; }
        private int ScoreComputer { get; set; }
        private Random Ran { get; set; }
        private RulesWindow RulesWindow { get; set; }
        private List<int> CardValues { get; set; }
        private TextBox[] TextBoxPlayer { get; set; }
        private TextBox[] TextBoxComputer { get; set; }
        private int CurrentTextBoxPlayer { get; set; }
        private int CurrentTextBoxComputer { get; set; }
        private NotificationWindow Notification { get; set; }
        public bool NotificationIsOpen { get; set; }
        public bool RulesWindowIsOpen { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Title = "BlackJackR - Reach €2000 to win!";
            MoneyComputer = 1000;
            MoneyPlayer = 1000;

            MoneyPlayerLabel.Content = "€" + MoneyPlayer.ToString();
            MoneyComputerLabel.Content = "€" + MoneyComputer.ToString();

            Ran = new Random();
            CardValues = new List<int>();
            AddAllCards();
            InitTextboxes();

            CurrentTextBoxPlayer = 2;
            CurrentTextBoxComputer = 2;
        }

        private void RulesButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!RulesWindowIsOpen)
            {
                RulesWindow = new RulesWindow(this);
                RulesWindow.Show();
                RulesWindowIsOpen = true;
            }
        }

        private void AddAllCards()
        {
            for (int i = 2; i < 12; i++)
            {
                CardValues.Add(i);
            }
            CardValues.Add(10);
            CardValues.Add(10);
            CardValues.Add(10);
        }

        private void InitTextboxes()
        {
            TextBoxPlayer = new TextBox[6];
            TextBoxPlayer[0] = Card1PL;
            TextBoxPlayer[1] = Card2PL;
            TextBoxPlayer[2] = Card3PL;
            TextBoxPlayer[3] = Card4PL;
            TextBoxPlayer[4] = Card5PL;
            TextBoxPlayer[5] = Card6PL;

            TextBoxComputer = new TextBox[6];
            TextBoxComputer[0] = Card1AI;
            TextBoxComputer[1] = Card2AI;
            TextBoxComputer[2] = Card3AI;
            TextBoxComputer[3] = Card4AI;
            TextBoxComputer[4] = Card5AI;
            TextBoxComputer[5] = Card6AI;
        }

        private void ResetGame()
        {
            DealButton.Visibility = Visibility.Visible;
            SplitButton.Visibility = Visibility.Hidden;
            HitButton.Visibility = Visibility.Hidden;
            StandButton.Visibility = Visibility.Hidden;
            CurrentTextBoxPlayer = 2;
            CurrentTextBoxComputer = 2;
            ScorePlayer = 0;
            ScoreComputer = 0;
            BetBox.IsReadOnly = false;
        }

        private void StartGame()
        {
            foreach (TextBox textBox in TextBoxPlayer)
            {
                textBox.Text = string.Empty;
            }
            foreach (TextBox textBox in TextBoxComputer)
            {
                textBox.Text = string.Empty;
            }

            PlayerScoreLabel.Content = "0";
            AIScoreLabel.Content = "0";
            EndGameMessageLabel.Content = "";
            BlackJackLabel.Visibility = Visibility.Hidden;
            DealButton.Visibility = Visibility.Hidden;
            HitButton.Visibility = Visibility.Visible;
            StandButton.Visibility = Visibility.Visible;
            BetBox.IsReadOnly = true;
        }
    }
}