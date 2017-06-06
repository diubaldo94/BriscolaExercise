using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Briscola.Tdd.Interfaces;
using Briscola.Tdd.Model;

namespace Briscola.Tdd.Logic
{
    public class DeckFactory : IDeckFactory
    {
        private readonly string[] _seeds;
        private readonly Range _valueRange;
        private readonly int _flag;

        public DeckFactory(string[] seeds, Range valueRange, int flag=0)
        {
            this._seeds = seeds;
            this._valueRange = valueRange;
            this._flag = flag;
        }
        public IDeck CreateDeck()
        {
            return new Deck(_seeds, _valueRange, _flag);
        }
    }
}
