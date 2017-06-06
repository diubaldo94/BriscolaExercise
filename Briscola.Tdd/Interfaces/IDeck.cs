using System.Collections.Generic;
using Briscola.Tdd.Model;

namespace Briscola.Tdd.Interfaces
{
    public interface IDeck : IEnumerable<Card>
    {
        void Shuffle();
        Card Pop();
        int Count();
        Card PeekCard();
    }
}