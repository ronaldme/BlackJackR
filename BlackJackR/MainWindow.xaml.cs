using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlackJackR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int CurrentMoneyAI { get; set; }
        private int CurrentMoneyPlayer { get; set; }
        private int ScorePlayer { get; set; }
        private int ScoreAI { get; set; }
        private Random Ran { get; set; }
        private RulesWindow RulesWindow { get; set; }
        public bool RulesWindowIsOpen { get; set; }
        private List<int> CardValues { get; set; }

        private TextBox[] TextBoxPlayer { get; set; }
        private TextBox[] TextBoxAI { get; set; }
        private int CurrentTextBoxPlayer { get; set; }
        private int CurrentTextBoxAI { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Title = "BlackJackR - Reach €2000 to win!";
            CurrentMoneyAI = 1000;
            CurrentMoneyPlayer = 1000;

            MoneyPlayerLabel.Content = "€" + CurrentMoneyPlayer.ToString();
            MoneyAILabel.Content = "€" + CurrentMoneyAI.ToString();

            Ran = new Random();
            CardValues = new List<int>();
            AddAllCards();
            InitTextboxes();

            CurrentTextBoxPlayer = 2;
            CurrentTextBoxAI = 2;
        }

        private void SplitButton_OnClick(object sender, RoutedEventArgs e)
        {
            // split cards
        }

        private void HitButton_OnClick(object sender, RoutedEventArgs e)
        {
            int min = CardValues.Min();
            int max = CardValues.Max() + 1;

            int randomCard = Ran.Next(min, max);
            ScorePlayer += randomCard;
            PlayerScoreLabel.Content = ScorePlayer.ToString();

            TextBoxPlayer[CurrentTextBoxPlayer].Text = randomCard.ToString();
            CurrentTextBoxPlayer++;

            if (CurrentTextBoxPlayer >= TextBoxPlayer.Count())
            {
                HitButton.Visibility = Visibility.Hidden;
                StandButton_OnClick(sender, new RoutedEventArgs());
            }

            if (ScorePlayer > 21)
            {
                LostMessageLabel.Visibility = Visibility.Visible;
                HitButton.Visibility = Visibility.Hidden;
                DealButton.Visibility = Visibility.Visible;
                return;
            }
        }

        private void StandButton_OnClick(object sender, RoutedEventArgs e)
        {
            // deal computer cards
            // when computer has less than 17, take another card

            int min = CardValues.Min();
            int max = CardValues.Max() + 1;

            int randomCardFirst = Ran.Next(min, max);
            int randomCardTwo = Ran.Next(min, max);

            Card1AI.Text = randomCardFirst.ToString();
            Card2AI.Text = randomCardTwo.ToString();

            if (randomCardFirst + randomCardTwo < 17)
            {
                int randomCardThree = Ran.Next(min, max);
                Card3AI.Visibility = Visibility.Visible;
                Card3AI.Text = randomCardThree.ToString();
            }

        }

        private void DealButton_OnClick(object sender, RoutedEventArgs e)
        {
            ResetOnDraw();
            DealButton.Visibility = Visibility.Hidden;

            int min = CardValues.Min();
            int max = CardValues.Max() + 1;

            int randomCardFirst = Ran.Next(min, max);
            int randomCardTwo = Ran.Next(min, max);

            ScorePlayer = randomCardFirst + randomCardTwo;
            PlayerScoreLabel.Content = ScorePlayer.ToString();

            if (randomCardFirst == randomCardTwo)
            {
                SplitButton.Visibility = Visibility.Visible;
            }

            if (ScorePlayer == 21)
            {
                // Blackjack
                BlackJackLabel.Visibility = Visibility.Visible;
                HitButton.Visibility = Visibility.Hidden;
            }

            Card1PL.Text = randomCardFirst.ToString();
            Card2PL.Text = randomCardTwo.ToString();
        }

        private void ResetOnDraw()
        {
            Card1PL.Text = string.Empty;
            Card2PL.Text = string.Empty;
            HitButton.Visibility = Visibility.Visible;
            SplitButton.Visibility = Visibility.Hidden;
        }

        private void AddAllCards()
        {
            CardValues.Add(2);
            CardValues.Add(3);
            CardValues.Add(4);
            CardValues.Add(5);
            CardValues.Add(6);
            CardValues.Add(7);
            CardValues.Add(8);
            CardValues.Add(9);
            CardValues.Add(10);
            CardValues.Add(10);
            CardValues.Add(10);
            CardValues.Add(10);
            CardValues.Add(11);
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

            TextBoxAI = new TextBox[6];
            TextBoxAI[0] = Card1AI;
            TextBoxAI[1] = Card2AI;
            TextBoxAI[2] = Card3AI;
            TextBoxAI[3] = Card4AI;
            TextBoxAI[4] = Card5AI;
            TextBoxAI[5] = Card6AI;
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
    }
}