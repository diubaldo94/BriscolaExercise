using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Briscola.Tdd.Interfaces;
using Briscola.Tdd.Model;

namespace Briscola.Tdd.Logic
{
    public class Briscola : IGame
    {
        public IDeck _Deck { get; }

        public List<IPlayer> PlayersList { get; }
        public Card BriscolaCard { get; private set; }

        public Dictionary<IPlayer, Card> TableCards { get; private set; }

        public IPlayer FirstToPlay { get; private set; }

        public List<IPlayer> WinnerPlayers { get; private set; }

        public IEvaluator Evaluator { get; private set; }


        public Briscola(List<IPlayer> players, IDeck mazzo)
        {
            WinnerPlayers = null;
            _Deck = mazzo;
            PlayersList = players;
            if (PlayersList.Any(player => !player.NewGame()))
            {
                this.EndGame(false);
                throw new Exception("Uno o più giocatori sono impegnati in un'altra partita");
            }
        }
        
        public void Start()
        {
            TableCards = new Dictionary<IPlayer, Card>();
            _Deck.Shuffle();
            foreach (var player in PlayersList)
            {
                player.GetCard(_Deck.Pop());
                player.GetCard(_Deck.Pop());
                player.GetCard(_Deck.Pop());
            }
            BriscolaCard = _Deck.PeekCard();
            FirstToPlay = PlayersList.First();
            Evaluator= new BriscolaEvaluator(BriscolaCard.Seed);

        }

        public void PlayHand()
        {
            TableCards.Clear();
            if (PlayersList.All(i=> !i.ThereAreHandCards()))
            {
                EndGame(true);
            }
            else
            {
                int counter;
                int playersNumber = PlayersList.Count;
                int index = counter = PlayersList.IndexOf(FirstToPlay);
                while(counter<playersNumber+index)
                {
                    TableCards.Add(
                        PlayersList[counter % playersNumber], 
                        PlayersList[counter++ % playersNumber].PlayCard(new BriscolaState(TableCards, BriscolaCard)));
                }
                FirstToPlay = Evaluator.GetHandWinnerPlayer(new BriscolaState(TableCards, BriscolaCard));
                Console.WriteLine("{0} ha vinto l'ultima mano, quindi sarà il primo a giocare nella prossima", FirstToPlay.Name);
                Console.WriteLine();
                FirstToPlay.TakeCards(TableCards.Values.ToList());
                
                if (_Deck.Count() >= PlayersList.Count)
                {
                    foreach (var player in PlayersList)
                    {
                        player.GetCard(_Deck.Pop());
                    }
                }
            }

        }

        public void PlayAllGame()
        {
            while (WinnerPlayers==null)
            {
                PlayHand();
            }
        }

        
        public void EndGame(bool finishedGame)
        {
            if (finishedGame)
            {
                if (PlayersList.Count==4)
                {
                    WinnerPlayers = Evaluator.GetGameWinnerPlayers(PlayersList);
                }
                else
                {
                    WinnerPlayers=new List<IPlayer>();
                    WinnerPlayers.Add(Evaluator.GetGameWinnerPlayer(PlayersList));
                }
            }
            foreach (var player in PlayersList)
            {
                player.Reset();
            }
        }
        
        
    }
}
