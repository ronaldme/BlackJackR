using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Blackjack.Commands;
using Blackjack.Helpers;
using Blackjack.Interfaces;
using Blackjack.Models;

namespace Blackjack.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand DealCommand { get; private set; }
        public ICommand HitCommand { get; private set; }
        public ICommand StandCommand { get; private set; }
        public ICommand SplitCommand { get; private set; }
        public ICommand RulesCommand { get; private set; }
        public IView View { get; set; }
        public string BetAmount { get; set; }
        public Player Player { get; set; }
        public Player Computer { get; set; }
        public TaskFactory TaskFactory { get; private set; }
        public bool BetPlaced { get; set; }
        public bool DoubleCards { get; set; }

        private Dictionary<int, BitmapImage[]> CardImages { get; set; }

        public GameViewModel(IView view, string playerName)
        {
            View = view;
            DealCommand = new DealCommand(this);
            HitCommand = new HitCommand(this);
            StandCommand = new StandCommand(this);
            SplitCommand = new SplitCommand(this);
            RulesCommand = new RulesCommand(this);

            Player = new Player(playerName, 1000, ImagesHelper.CreateImage("player"), 2);
            Computer = new Player("Computer", 1000, ImagesHelper.CreateImage("computer"), 2);
            view.DisplayMoney(Player, Computer);
            BetAmount = "500";

            CardImages = ImagesHelper.GetBlackJackCards();
            view.AddCards(Player, Computer);
        }

        public void DealCards()
        {
            GameHelper.ResetImages(Player, Computer);
            View.ResetResult();
            BetPlaced = true;

            Random ran = new Random();
            int cardOne = ran.Next(2, 12);
            int cardTwo = ran.Next(2, 12);
            GameHelper.AddAces(Player, cardOne);
            GameHelper.AddAces(Player, cardTwo);

            Player.Images[0].Source = ImagesHelper.RandomColorCard(CardImages.First(x => x.Key == cardOne).Value);
            Player.Images[1].Source = ImagesHelper.RandomColorCard(CardImages.First(x => x.Key == cardTwo).Value);
            Player.CurrentScore = cardOne + cardTwo;
            View.DisplayPoints(Player);

            if (cardOne == cardTwo)
            {
                DoubleCards = true;
            }
        }

        public void HitCard()
        {
            Random ran = new Random();
            int card = ran.Next(2, 12);
            GameHelper.AddAces(Player, card);
            
            Player.Images[Player.CurrentImage].Source = ImagesHelper.RandomColorCard(CardImages.First(x => x.Key == card).Value);
            Player.CurrentImage++;
            Player.CurrentScore += card;
            View.DisplayPoints(Player);

            if (Player.CurrentScore > 21)
            {
                BetPlaced = false;
                View.EndGame(Player, Computer, Convert.ToInt16(BetAmount));
            }
        }

        public void Stand()
        {
            Random ran = new Random();
            int cardOne = ran.Next(2, 12);
            int cardTwo = ran.Next(2, 12);
            GameHelper.AddAces(Computer, cardOne);
            GameHelper.AddAces(Computer, cardTwo);

            Computer.Images[0].Source =
                ImagesHelper.RandomColorCard(CardImages.First(x => x.Key == cardOne).Value);
            Computer.Images[1].Source =
                ImagesHelper.RandomColorCard(CardImages.First(x => x.Key == cardTwo).Value);

            Computer.CurrentScore = cardOne + cardTwo;
            View.DisplayPoints(Computer);

            if (Computer.CurrentScore < 17)
            {
                TaskFactory = new TaskFactory(TaskScheduler.FromCurrentSynchronizationContext());
                new Thread(HitCardComputer).Start();
            }
            else
            {
                View.EndGame(Player, Computer, Convert.ToInt16(BetAmount));
            }
            BetPlaced = false;
       }

        public void HitCardComputer()
        {
            while (Computer.CurrentScore < 17 && Computer.CurrentImage < Computer.Images.Count())
            {
                // Pretend to be thinking
                Thread.Sleep(800);

                Random ran = new Random();
                int card = ran.Next(2, 12);
                GameHelper.AddAces(Computer, card);

                Computer.CurrentScore += card;
                TaskFactory.StartNew((() => DisplayCard(card)));
            }
            TaskFactory.StartNew(EndGame);
        }

        private void DisplayCard(int card)
        {
            Computer.Images[Computer.CurrentImage].Source =
                ImagesHelper.RandomColorCard(CardImages.First(x => x.Key == card).Value);
            Computer.CurrentImage++;
            View.DisplayPoints(Computer);
        }

        private void EndGame()
        {
            View.EndGame(Player, Computer, Convert.ToInt16(BetAmount));
        }
    }
}