using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Briscola.Tdd.Interfaces;
using Briscola.Tdd.Model;

namespace Briscola.Tdd.Logic
{
    public class RandomStrategy : IStrategy
    {
        private static readonly Random Random;

        static RandomStrategy()
        {
            Random=new Random();
        }

        public RandomStrategy()
        {
            
        }
        public Card ChooseCard(List<Card> cards, IGameState gameState, IEvaluator evaluator=null)
        {
            return cards[Random.Next(0, cards.Count-1)];
        }
    }
}
