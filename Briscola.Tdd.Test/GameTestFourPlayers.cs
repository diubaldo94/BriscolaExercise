using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Briscola.Tdd.Interfaces;
using Briscola.Tdd.Logic;
using Briscola.Tdd.Model;
using Moq;
using SharpTestsEx;
using Xunit;

namespace Briscola.Tdd.Test
{
    public class GameTestFourPlayers
    {
        private readonly IGame _sut;
        private IPlayer _p1, _p2, _p3, _p4;
        private Mock<IDeck> deckMock;
        private Card briscolaCard;
        private readonly List<IPlayer> players;
        private readonly List<Card> p1Cards, p2Cards, p3Cards, p4Cards;

        public GameTestFourPlayers()
        {
            p1Cards = new List<Card>()
            {
                new Card("Coppe", 1), new Card("Coppe", 3), new Card("Coppe", 4)
            };
            p2Cards = new List<Card>()
            {
                new Card("Spade", 1), new Card("Spade", 2), new Card("Spade", 7)
            };
            p3Cards = new List<Card>()
            {
                new Card("Denari", 1), new Card("Denari", 3), new Card("Denari", 4)
            };
            p4Cards = new List<Card>()
            {
                new Card("Bastoni", 1), new Card("Bastoni", 2), new Card("Bastoni", 7)
            };

            briscolaCard = new Card("Bastoni", 2);
            _p1 = new Player("Pippo");
            _p2 = new Player("Pluto");
            _p3 = new Player("Paperino");
            _p4 = new Player("Topolino");
            players = new List<IPlayer>() { _p1, _p2, _p3, _p4 };
            deckMock = new Mock<IDeck>();
            deckMock.SetupSequence(i => i.Pop())
                .Returns(new Card("Coppe", 1)).Returns(new Card("Coppe", 3))
                .Returns(new Card("Coppe", 4)).Returns(new Card("Spade", 1))
                .Returns(new Card("Spade", 2)).Returns(new Card("Spade", 7))
                .Returns(new Card("Denari", 1)).Returns(new Card("Denari", 3))
                .Returns(new Card("Denari", 4)).Returns(new Card("Bastoni", 1))
                .Returns(new Card("Bastoni", 2)).Returns(new Card("Bastoni", 7));
            deckMock.Setup(i => i.PeekCard()).Returns(briscolaCard);
            _sut = new Logic.Briscola(players, deckMock.Object);
            _sut.Start();
        }

        [Fact]
        public void StartTest()
        {
            _sut.PlayersList[0].HandCards.Count.Should().Be.EqualTo(3);
            _sut.PlayersList[1].HandCards.Count.Should().Be.EqualTo(3);
            _sut.PlayersList[2].HandCards.Count.Should().Be.EqualTo(3);
            _sut.PlayersList[3].HandCards.Count.Should().Be.EqualTo(3);

            p1Cards.Should().Have.SameSequenceAs(_sut.PlayersList[0].HandCards);
            p2Cards.Should().Have.SameSequenceAs(_sut.PlayersList[1].HandCards);
            p3Cards.Should().Have.SameSequenceAs(_sut.PlayersList[2].HandCards);
            p4Cards.Should().Have.SameSequenceAs(_sut.PlayersList[3].HandCards);
            _sut.TableCards.Should().Be.Empty();
            _sut.FirstToPlay.Should().Be.EqualTo(_p1);
            _sut.PlayersList[0].TakenCards.Count.Should().Be.EqualTo(0);
            _sut.PlayersList[1].TakenCards.Count.Should().Be.EqualTo(0);
            _sut.PlayersList[2].TakenCards.Count.Should().Be.EqualTo(0);
            _sut.PlayersList[3].TakenCards.Count.Should().Be.EqualTo(0);
        }

        [Fact]
        public void PlayHandTest()
        {
            _sut.PlayHand();

            _sut.PlayersList[0].HandCards.Count.Should().Be.EqualTo(2);
            _sut.PlayersList[1].HandCards.Count.Should().Be.EqualTo(2);
            _sut.PlayersList[2].HandCards.Count.Should().Be.EqualTo(2);
            _sut.PlayersList[3].HandCards.Count.Should().Be.EqualTo(2);

            p1Cards.Should().Contain(_sut.PlayersList[0].HandCards.First());
            p1Cards.Should().Contain(_sut.PlayersList[0].HandCards.Last());
            p2Cards.Should().Contain(_sut.PlayersList[1].HandCards.First());
            p2Cards.Should().Contain(_sut.PlayersList[1].HandCards.Last());
            p3Cards.Should().Contain(_sut.PlayersList[2].HandCards.First());
            p3Cards.Should().Contain(_sut.PlayersList[2].HandCards.Last());
            p4Cards.Should().Contain(_sut.PlayersList[3].HandCards.First());
            p4Cards.Should().Contain(_sut.PlayersList[3].HandCards.Last());
            _sut.TableCards.Keys
                .Should().Have.SameSequenceAs(new List<IPlayer>() { _p1, _p2, _p3, _p4 });
            p1Cards.Should().Contain(_sut.TableCards[_p1]);
            p2Cards.Should().Contain(_sut.TableCards[_p2]);
            p3Cards.Should().Contain(_sut.TableCards[_p3]);
            p4Cards.Should().Contain(_sut.TableCards[_p4]);
            _sut.FirstToPlay.Should().Be.EqualTo(_p4);
            _sut.PlayersList[0].TakenCards.Count.Should().Be.EqualTo(0);
            _sut.PlayersList[1].TakenCards.Count.Should().Be.EqualTo(0);
            _sut.PlayersList[2].TakenCards.Count.Should().Be.EqualTo(0);
            _sut.PlayersList[3].TakenCards.Count.Should().Be.EqualTo(4);
        }

        [Fact]
        public void EndTest()
        {
            _sut.PlayAllGame();

            _sut.WinnerPlayers.Count.Should().Be.EqualTo(2);

            _sut.WinnerPlayers.First().Should().Be.EqualTo(_p2);
            _sut.WinnerPlayers.Last().Should().Be.EqualTo(_p4);
            _sut.Evaluator.PlayerPointsDictionary[_p1].Should().Be.EqualTo(0);
            _sut.Evaluator.PlayerPointsDictionary[_p2].Should().Be.EqualTo(0);
            _sut.Evaluator.PlayerPointsDictionary[_p3].Should().Be.EqualTo(0);
            _sut.Evaluator.PlayerPointsDictionary[_p4].Should().Be.EqualTo(64);

        }

    }
}
