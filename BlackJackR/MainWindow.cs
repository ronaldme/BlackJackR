﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace BlackJackR
{
    /// <summary>
    /// Initialize BlackJackR game. Set all Properties and Initialize the MainWindow.
    /// This class also adds all the cards and TextBoxes to the Lists.
    /// </summary>
    public partial class MainWindow
    {
        private Random Ran { get; set; }
        private RulesWindow RulesWindow { get; set; }
        private List<int> CardValues { get; set; }
        private TextBox[] TextBoxPlayer { get; set; }
        private TextBox[] TextBoxPlayerSplitLeft { get; set; }
        private TextBox[] TextBoxPlayerSplitRight { get; set; }
        private TextBox[] TextBoxComputer { get; set; }
        private NotificationWindow Notification { get; set; }
        private Dictionary<TextBox, int> PlayerAces { get; set; }
        private Dictionary<TextBox, int> PlayerAcesLeft { get; set; }
        private Dictionary<TextBox, int> PlayerAcesRight { get; set; }
        private Dictionary<TextBox, int> ComputerAces { get; set; }
        private int MoneyComputer { get; set; }
        private int MoneyPlayer { get; set; }
        private int ScorePlayer { get; set; }
        private int ScoreComputer { get; set; }
        private int PlayerScoreSplitLeft { get; set; }
        private int PlayerScoreSplitRight { get; set; }
        private int PressedStandButtons { get; set; }
        private int CurrentCountLeft { get; set; }
        private int CurrentCountRight { get; set; }
        private int CurrentTextBoxPlayer { get; set; }
        private int CurrentTextBoxComputer { get; set; }
        public bool NotificationIsOpen { get; set; }
        public bool RulesWindowIsOpen { get; set; }
        private int Min { get; set; }
        private int Max { get; set; }
        private bool HasDeckLost { get; set; }

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
            PlayerAces = new Dictionary<TextBox, int>();
            PlayerAcesLeft = new Dictionary<TextBox, int>();
            PlayerAcesRight = new Dictionary<TextBox, int>();
            ComputerAces = new Dictionary<TextBox, int>();
            AddAllCards();
            InitTextboxes();

            CurrentTextBoxPlayer = 2;
            CurrentTextBoxComputer = 2;
            Min = CardValues.Min();
            Max = CardValues.Max() + 1;
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

        private void ShowSplitDeck()
        {
            BorderSplit.Visibility = Visibility.Visible;
            HitButtonLeft.Visibility = Visibility.Visible;
            HitButtonRight.Visibility = Visibility.Visible;
            StandButtonLeft.Visibility = Visibility.Visible;
            StandButtonRight.Visibility = Visibility.Visible;
            HitButton.Visibility = Visibility.Hidden;
            StandButton.Visibility = Visibility.Hidden;
            SplitButton.Visibility = Visibility.Hidden;

            foreach (TextBox textBox in TextBoxPlayerSplitLeft)
            {
                textBox.Visibility = Visibility.Visible;
            }
            foreach (TextBox textBox in TextBoxPlayerSplitRight)
            {
                textBox.Visibility = Visibility.Visible;
            }
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

            TextBoxPlayerSplitLeft = new TextBox[5];
            TextBoxPlayerSplitLeft[0] = Card3PL;
            TextBoxPlayerSplitLeft[1] = Card5PL;
            TextBoxPlayerSplitLeft[2] = CardSplitA1;
            TextBoxPlayerSplitLeft[3] = CardSplitA2;
            TextBoxPlayerSplitLeft[4] = CardSplitA3;

            TextBoxPlayerSplitRight = new TextBox[5];
            TextBoxPlayerSplitRight[0] = Card4PL;
            TextBoxPlayerSplitRight[1] = Card6PL;
            TextBoxPlayerSplitRight[2] = CardSplitB1;
            TextBoxPlayerSplitRight[3] = CardSplitB2;
            TextBoxPlayerSplitRight[4] = CardSplitB3;

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
            CurrentCountLeft = 0;
            CurrentCountRight = 0;
            ScorePlayer = 0;
            ScoreComputer = 0;
            PressedStandButtons = 0;
            BetBox.IsReadOnly = false;
            HasDeckLost = false;
            PlayerAces.Clear();
            ComputerAces.Clear();
        }

        private void StartGame()
        {
            foreach (TextBox textBox in TextBoxPlayerSplitLeft)
            {
                textBox.Visibility = Visibility.Hidden;
                textBox.Text = string.Empty;
            }
            foreach (TextBox textBox in TextBoxPlayerSplitRight)
            {
                textBox.Visibility = Visibility.Hidden;
                textBox.Text = string.Empty;
            }

            foreach (TextBox textBox in TextBoxPlayer)
            {
                textBox.Text = string.Empty;
                textBox.Visibility = Visibility.Visible;
            }
            foreach (TextBox textBox in TextBoxComputer)
            {
                textBox.Text = string.Empty;
                textBox.Visibility = Visibility.Visible;
            }
            LabelLeft.Visibility = Visibility.Hidden;
            LabelRight.Visibility = Visibility.Hidden;
            PlayerScoreLabel.Content = "0";
            ComputerScoreLabel.Content = "0";
            EndGameMessageLabel.Content = "";
            BlackJackLabel.Visibility = Visibility.Hidden;
            DealButton.Visibility = Visibility.Hidden;
            HitButton.Visibility = Visibility.Visible;
            StandButton.Visibility = Visibility.Visible;
            BetBox.IsReadOnly = true;

            // reset the split deck
            BorderSplit.Visibility = Visibility.Hidden;
            HitButtonLeft.Visibility = Visibility.Hidden;
            HitButtonRight.Visibility = Visibility.Hidden;
            StandButtonLeft.Visibility = Visibility.Hidden;
            StandButtonRight.Visibility = Visibility.Hidden;
        }
    }
}