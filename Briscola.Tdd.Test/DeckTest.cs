using System.Collections.Generic;
using System.Linq;
using Briscola.Tdd.Interfaces;
using Briscola.Tdd.Logic;
using Briscola.Tdd.Model;
using SharpTestsEx;
using Xunit;

namespace Briscola.Tdd.Test
{
    public class DeckTest
    {
        public DeckTest()
        {
            var seeds = new[] {"Cuori", "Picche", "Quadri", "Fiori"};
            var valueRange = new Range(1, 13);
            _sut = new Deck(seeds, valueRange, 0);
        }

        private readonly IDeck _sut;


        [Fact]
        public void Deck_should_be_IEnumerable_of_cards()
        {
            var enumerable = _sut as IEnumerable<Card>;

            enumerable.Should().Not.Be.Null();
        }

        [Fact]
        public void Deck_should_initialize_dynamilcally_with_seeds_and_values()
        {
            var seeds = new[] {"Coppe", "Spade"};
            var valueRange = new Range(1, 3);

            IDeck sut = new Deck(seeds, valueRange, 0);

            sut.Should()
                .Have.SameValuesAs(new Card("Coppe", 1),
                    new Card("Coppe", 2),
                    new Card("Coppe", 3),
                    new Card("Spade", 1),
                    new Card("Spade", 2),
                    new Card("Spade", 3));
        }

        [Fact]
        public void Shuffle_should_change_cards_order_at_every_time()
        {
            _sut.Shuffle();
            var seq1 = _sut.ToArray();

            _sut.Shuffle();
            var seq2 = _sut.ToArray();

            seq1.Should().Not.Have.SameSequenceAs(seq2);
        }


        [Fact]
        public void Pop_should_take_first_card_of_Deck_removing_it()
        {
            var seeds = new[] { "Coppe", "Spade" };
            var valueRange = new Range(1, 3);
            IDeck sut = new Deck(seeds, valueRange, 0);
            var expected = sut.First();

            var card = sut.Pop();

            card.Should().Be(expected);
            sut.Should().Not.Contain(card);
            sut.Count().Should().Be(5);


        }
    }
}