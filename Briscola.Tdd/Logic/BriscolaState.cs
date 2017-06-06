using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Briscola.Tdd.Interfaces;
using Briscola.Tdd.Model;

namespace Briscola.Tdd.Logic
{
    public class BriscolaState : IGameState
    {
        public Dictionary<IPlayer, Card> TableCards { get; }
        public Card BriscolaCard { get; }
        public int Turno { get; private set; }

        public BriscolaState(Dictionary<IPlayer, Card> tableCards, Card briscolaCard)
        {
            Turno = 0;
            TableCards = tableCards;
            BriscolaCard = briscolaCard;
        }

        public void Display()
        {
            Console.WriteLine("La briscola è {0} di {1}", BriscolaCard.Value, BriscolaCard.Seed);
            if (TableCards.Any())
            {
                Console.WriteLine("Sul tavolo ci sono le seguenti carte:");
                int counter = 1;
                foreach (var tableCard in TableCards)
                {
                    Console.WriteLine("{0}) {1} di {2} , lanciato da {3}", counter++, tableCard.Value.Value, tableCard.Value.Seed, tableCard.Key.Name);
                }
            }
            else
            {
                Console.WriteLine("Non ci sono carte sul tavolo");
                Console.WriteLine();
            }
            Turno++;
        }

    }
}
