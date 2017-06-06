using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Briscola.Tdd.Interfaces;
using Briscola.Tdd.Model;

namespace Briscola.Tdd.Logic
{
    public class Deck : IDeck
    {
        private List<Card> _cards;

        public Deck(string[] seeds, Range valueRange, int flag)
        {
            _cards = new List<Card>();
            foreach (var seed in seeds)
                for (var value = valueRange.Min; value <= valueRange.Max; value++)
                    _cards.Add(new Card(seed, value));
            if (flag == 1)
            {
                RemoveACardOfSelectedCard(2);
            }

        }

        private bool RemoveACardOfSelectedCard(int value)
        {
            return _cards.Remove(_cards.First(i => i.Value == value));
        }

        public IEnumerable<Card> Cards => _cards;

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Shuffle()
        {
            _cards = _cards.OrderBy(x => Guid.NewGuid()).ToList();
        }

        public Card Pop()
        {
            var card = _cards.First();
            _cards.Remove(card);
            return card;
        }

        public IEnumerator<Card> GetEnumerator()
        {
            return _cards.GetEnumerator();
        }

        public int Count()
        {
            return _cards.Count;
        }

        public Card PeekCard()
        {
            var cardToPeek = Pop();
            _cards.Add(cardToPeek);
            return cardToPeek;
        }
        
    }
}