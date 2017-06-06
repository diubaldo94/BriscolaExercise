using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Briscola.Tdd.Model;

namespace Briscola.Tdd.Interfaces
{
    public interface IStrategy
    {
        Card ChooseCard(List<Card> cards,  IGameState gameState, IEvaluator evaluator = null);
    }
}
