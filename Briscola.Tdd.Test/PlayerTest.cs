using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Briscola.Tdd.Interfaces;
using Briscola.Tdd.Logic;
using Briscola.Tdd.Model;
using Castle.Components.DictionaryAdapter;
using Moq;
using SharpTestsEx;
using Xunit;

namespace Briscola.Tdd.Test
{
    public class PlayerTest
    {
        private Mock<IDeck> deckMock;
        private readonly Mock<IGameState> _gameStateMock;
        //private readonly Mock<IStrategy> strategyMock;
        private readonly IPlayer _sut;
        private readonly List<Card> _cardList ;

        public PlayerTest()
        {
            deckMock=new Mock<IDeck>();
            _sut= new Player("Pippo");
            _cardList = new List<Card>()
            {
                new Card("foo", 1),
                new Card("foo", 2),
                new Card("foo", 3)

            };
            var tableCardsToTest = new Dictionary<IPlayer, Card> {{new Player("Pluto"), new Card("Spade", 3)}};
            deckMock.SetupSequence(i => i.Pop())
                .Returns(new Card("foo", 1))
                .Returns(new Card("foo", 2))
                .Returns(new Card("foo", 3));
            _gameStateMock = new Mock<IGameState>();

            _gameStateMock.Setup(i => i.TableCards).Returns(tableCardsToTest);
            _gameStateMock.Setup(i => i.BriscolaCard).Returns(new Card("Coppe", 9));
        }

        [Fact]
        public void TestNewGame()
        {
            bool attempt1 = _sut.NewGame();
            bool attempt2 = _sut.NewGame();
            attempt1.Should().Be.True();
            attempt2.Should().Be.False();
        }

        [Fact]
        public void GetCardsInHand()
        {
            _sut.NewGame();
            _sut.GetCard(deckMock.Object.Pop());
            _sut.GetCard(deckMock.Object.Pop());
            _sut.GetCard(deckMock.Object.Pop());
            _sut.HandCards
                .Should()
                .Have
                .SameSequenceAs(new List<Card>()
                {new Card("foo", 1), new Card("foo", 2), new Card("foo", 3)});
        }

        [Fact]
        public void TakeCardsForScore()
        {
            _sut.NewGame();
            _sut.TakeCards(new List<Card>() { deckMock.Object.Pop(), deckMock.Object.Pop(), deckMock.Object.Pop()} );
            _sut.TakenCards
                .Should()
                .Have
                .SameSequenceAs(new List<Card>()
                {new Card("foo", 1), new Card("foo", 2), new Card("foo", 3)});
        }

        [Fact]
        public void PlayCard()
        {
            _sut.NewGame();
            _sut.GetCard(deckMock.Object.Pop());
            _sut.GetCard(deckMock.Object.Pop());
            _sut.GetCard(deckMock.Object.Pop());
            _cardList.Should().Contain(_sut.PlayCard(_gameStateMock.Object));
        }

        [Fact]
        public void TestResetPlayerData()
        {
            _sut.NewGame();
            _sut.GetCard(deckMock.Object.Pop());
            _sut.GetCard(deckMock.Object.Pop());
            _sut.GetCard(deckMock.Object.Pop());
            _sut.TakeCards(_cardList);
            _sut.Reset();
            bool testNewGameAfterReset = _sut.NewGame();

            _sut.HandCards.Should().Be.Empty();
            _sut.TakenCards.Should().Be.Empty();
            testNewGameAfterReset.Should().Be.True();
        }
    }
}
