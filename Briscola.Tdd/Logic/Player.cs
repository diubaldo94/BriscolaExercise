using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Briscola.Tdd.Interfaces;
using Briscola.Tdd.Model;

namespace Briscola.Tdd.Logic
{
    public class Player : IPlayer
    {
        private bool _playing;
        private readonly IStrategy _strategy;
        public string Name { get; }
        public List<Card> HandCards { get; }
        public List<Card> TakenCards { get; }


        public Player(string name, IStrategy strategy = null,List<Card> cards = null)
        {
            Name = name;
            _strategy = strategy ?? new RandomStrategy();
            TakenCards= new List<Card>();
            HandCards = cards ?? new List<Card>();
            _playing = false;
        }

        public bool NewGame()
        {
            if (!_playing)
            {
                _playing = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public Card PlayCard(IGameState gameState)
        {
            if (_playing)
            {
                
                var card = _strategy.ChooseCard(HandCards, gameState);
                HandCards.Remove(card);
                return card;
            }
            else 
                throw new Exception("Questo giocatore non sta giocando");
        }

        public void GetCard(Card newCard)
        {
            if (_playing)
                HandCards.Add(newCard);
            else
                throw new Exception("Questo giocatore non sta giocando");
        }

        public void TakeCards(List<Card> newCardList)
        {
            if(_playing)
                foreach (var card in newCardList)
                    TakenCards.Add(card);
            else
                throw new Exception("Questo giocatore non sta giocando");
        }

        public bool ThereAreHandCards()
        {
            return HandCards.Count != 0;
        }

        public void Reset()
        {
            HandCards.Clear();
            TakenCards.Clear();
            _playing = false;
        }
        
    }
}
