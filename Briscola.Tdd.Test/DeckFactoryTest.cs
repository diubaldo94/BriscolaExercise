using Briscola.Tdd.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Briscola.Tdd.Logic;
using Briscola.Tdd.Model;
using SharpTestsEx;
using Xunit;

namespace Briscola.Tdd.Test
{
    class DeckFactoryTest
    {
        private readonly IDeckFactory _sut;

        public DeckFactoryTest()
        {
            _sut = new DeckFactory(new[] { "Coppe", "Spade" }, new Range(1, 3));
        }

        [Fact]
        public void DeckFactoryCreateDecksCorrectly()
        {
            var deck = _sut.CreateDeck();
            deck.Should()
                .Have.SameValuesAs(new Card("Coppe", 1),
                    new Card("Coppe", 2),
                    new Card("Coppe", 3),
                    new Card("Spade", 1),
                    new Card("Spade", 2),
                    new Card("Spade", 3));
        }
    }
}
