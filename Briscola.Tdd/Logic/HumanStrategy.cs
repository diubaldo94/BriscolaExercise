using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Briscola.Tdd.Interfaces;
using Briscola.Tdd.Model;

namespace Briscola.Tdd.Logic
{
    public class HumanStrategy : IStrategy
    {
        public Card ChooseCard(List<Card> cards, IGameState gameState, IEvaluator evaluator = null)
        {
            int cardIndex;
            gameState.Display();
            while (true)
            {
                if (cards.Count == 1)
                {
                    Console.WriteLine("Essendo rimasta solo la carta {0} di {1}, la giochiamo.", 
                        cards[0].Value,
                        cards[0].Seed);
                    return cards[0];
                }
                else
                {
                    Console.WriteLine("Scegli una di queste carte digitando la lettera corrispondente.");
                    Console.WriteLine("1) {0} di {1}", cards[0].Value, cards[0].Seed);
                    Console.WriteLine("2) {0} di {1}", cards[1].Value, cards[1].Seed);
                    if (cards.Count == 3)
                        Console.WriteLine("3) {0} di {1}", cards[2].Value, cards[2].Seed);
                }
                cardIndex = Convert.ToInt32(Console.ReadLine());
                if (cardIndex==1||cardIndex==2||(cardIndex==3&&cards.Count==3))
                {
                    return cards[cardIndex-1];
                }
                else
                {
                    Console.WriteLine("L'input immesso non è valido.");
                }
             }

          }
        }

        
    }

