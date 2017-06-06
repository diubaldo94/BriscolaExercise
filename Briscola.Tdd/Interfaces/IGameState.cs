using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Briscola.Tdd.Logic;
using Briscola.Tdd.Model;

namespace Briscola.Tdd.Interfaces
{
    public interface IGameState
    {
        Dictionary<IPlayer, Card> TableCards { get; }
        Card BriscolaCard { get; }
        void Display();
    }
}
