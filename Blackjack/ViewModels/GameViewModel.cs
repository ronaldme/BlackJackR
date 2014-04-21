using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Blackjack.Helpers;
using Blackjack.Interfaces;
using Blackjack.Models;

namespace Blackjack.ViewModels
{
    public class GameViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public CommandsManager Commands { get; set; }
        private Dictionary<int, BitmapImage[]> CardImages { get; set; }
        public Player Player { get; set; }
        public Player Computer { get; set; }
        public TaskFactory TaskFactory { get; private set; }
        public IView View { get; set; }
        public string BetAmount { get; set; }
        public bool BetPlaced { get; set; }
        public bool DoubleCards { get; set; }
        public bool SplitDeck { get; set; }

        public GameViewModel(IView view, string name)
        {
            View = view;
            Commands = new CommandsManager(this);

            Player = new Player(name, 1000, ImagesHelper.CreateImage("player"), 2);
            Computer = new Player("Computer", 1000, ImagesHelper.CreateImage("computer"), 2);
            view.DisplayMoney(Player, Computer);
            view.DisplayName(name);

            CardImages = ImagesHelper.GetBlackJackCards();
            view.AddCards(Player, Computer);
            Player.ShowBackside();
            Computer.ShowBackside();
        }

        public void DealCards()
        {
            // Reset the board
            GameHelper.ResetImages(Player, Computer);
            GameHelper.ResetGame(Player, Computer);
            View.ResetResult();
            View.SplitDeck(Player, false);

            Random ran = new Random();
            BetPlaced = true;

            int cardOne = ran.Next(2, 12);
            int cardTwo = ran.Next(2, 12);
            GameHelper.AddAces(Player, cardOne);
            GameHelper.AddAces(Player, cardTwo);

            Player.Images[0].Source = ImagesHelper.RandomColorCard(CardImages.First(x => x.Key == cardOne).Value);
            Player.Images[1].Source = ImagesHelper.RandomColorCard(CardImages.First(x => x.Key == cardTwo).Value);
            Player.Score = cardOne + cardTwo;
            View.DisplayPoints(Player);

            if (cardOne == cardTwo)
            {
                DoubleCards = true;
            }
            if (Player.Score > 21)
            {
                GameHelper.HasAces(Player);
                View.DisplayPoints(Player);
            }
        }

        public void HitCard()
        {
            Random ran = new Random();
            int card = ran.Next(2, 12);
            GameHelper.AddAces(Player, card);

            Player.Images[Player.CurrentImage].Source = ImagesHelper.RandomColorCard(CardImages.First(x => x.Key == card).Value);
            Player.CurrentImage++;
            Player.Score += card;
            
            if (Player.Score > 21 && !GameHelper.HasAces(Player))
            {
                BetPlaced = false;
                End(false);  
            }
            View.DisplayPoints(Player);
        }

        public void HitCardSplit(bool leftDeck)
        {
            Random ran = new Random();
            int card = ran.Next(2, 12);
            GameHelper.AddAcesSplit(Player, card, leftDeck);

            if (leftDeck)
            {
                Player.SplitDeck.ImagesLeft[Player.SplitDeck.CurrentImageLeft].Source =
                    ImagesHelper.RandomColorCard(CardImages.First(x => x.Key == card).Value);
                Player.SplitDeck.CurrentImageLeft++;
                Player.SplitDeck.ScoreLeft += card;
                
                if (Player.SplitDeck.ScoreLeft > 21 && !GameHelper.HasAcesSplit(Player, true))
                {
                    Player.SplitDeck.FinishedLeft = true;
                    if (Player.SplitDeck.FinishedRight)
                    {
                        Stand(true);
                    }
                }
                View.DisplayPointsSplit(Player);
            }
            else
            {
                Player.SplitDeck.ImagesRight[Player.SplitDeck.CurrentImageRight].Source =
                    ImagesHelper.RandomColorCard(CardImages.First(x => x.Key == card).Value);
                Player.SplitDeck.CurrentImageRight++;
                Player.SplitDeck.ScoreRight += card;

                if (Player.SplitDeck.ScoreRight > 21 && !GameHelper.HasAcesSplit(Player, false))
                {
                    Player.SplitDeck.FinishedRight = true;
                    if (Player.SplitDeck.FinishedLeft)
                    {
                        Stand(true);
                    }
                }
                View.DisplayPointsSplit(Player);
            }
        }

        public void Stand(bool isSplit)
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

            Computer.Score = cardOne + cardTwo;
            View.DisplayPoints(Computer);

            if (Computer.Score < 17 || (Computer.Score > 21 && GameHelper.HasAces(Computer)))
            {
                TaskFactory = new TaskFactory(TaskScheduler.FromCurrentSynchronizationContext());
                new Thread(() => HitCardComputer(isSplit)).Start();
            }
            else
            {
                End(isSplit);
            }
            BetPlaced = false;
       }

        public void HitCardComputer(bool isSplit)
        {
            TaskFactory.StartNew(() => View.DealButton(false));
            while (Computer.Score < 17 && Computer.CurrentImage < Computer.Images.Count())
            {
                // Pretend to be thinking
                Thread.Sleep(800);

                Random ran = new Random();
                int card = ran.Next(2, 12);
                GameHelper.AddAces(Computer, card);

                Computer.Score += card;
                TaskFactory.StartNew((() => DisplayCard(card)));

                if (Computer.Score > 21)
                {
                    GameHelper.HasAces(Computer);
                }
            }
            TaskFactory.StartNew(() => End(isSplit));
            TaskFactory.StartNew(() => View.DealButton(true));
            Thread.Yield();
        }

        private void DisplayCard(int card)
        {
            Computer.Images[Computer.CurrentImage].Source =
                ImagesHelper.RandomColorCard(CardImages.First(x => x.Key == card).Value);
            Computer.CurrentImage++;
            View.DisplayPoints(Computer);
        }

        private void End(bool isSplit)
        {
            if (isSplit)
            {
                View.EndGameSplit(Player, Computer, Convert.ToInt16(BetAmount));
            }
            else
            {
                View.EndGame(Player, Computer, Convert.ToInt16(BetAmount));
            }

            View.DisplayMoney(Player, Computer);
            View.CheckMoney(Player, Computer);
        }
    }
}