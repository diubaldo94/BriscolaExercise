using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Briscola.Tdd.Interfaces;
using Briscola.Tdd.Model;

namespace Briscola.Tdd.Logic
{
    public class AiStrategy : IStrategy
    {
        private List<string> playerNameList;
        public AiStrategy(List<string> playerNameList)
        {
            this.playerNameList = playerNameList;
        }
        public Card ChooseCard(List<Card> cards, IGameState gameState, IEvaluator evaluator)
        {
            throw new NotImplementedException();
        }
    }
}
