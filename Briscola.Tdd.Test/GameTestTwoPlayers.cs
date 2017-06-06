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
using Briscola = Briscola.Tdd.Logic.Briscola;

namespace Briscola.Tdd.Test
{
    public class GameTestTwoPlayers
    {
        private readonly IGame _sut;
        private IPlayer _p1, _p2;
        private readonly Mock<IDeck> deckMock;
        private readonly Card briscolaCard;
        private readonly List<IPlayer> players;
        private readonly List<Card> p1Cards, p2Cards; 

        public GameTestTwoPlayers()
        {
            p1Cards = new List<Card>()
            {
                new Card("Coppe", 1), new Card("Coppe", 3), new Card("Coppe", 4)
            };
            p2Cards = new List<Card>()
            {
                new Card("Spade", 1), new Card("Spade", 2), new Card("Spade", 7)
            };

            briscolaCard =new Card("Coppe", 3);
            _p1=new Player("Pippo");
            _p2=new Player("Pluto");
            players=new List<IPlayer>() {_p1, _p2};
            deckMock= new Mock<IDeck>();
            deckMock.SetupSequence(i => i.Pop())
                .Returns(new Card("Coppe", 1)).Returns(new Card("Coppe", 3))
                .Returns(new Card("Coppe", 4)).Returns(new Card("Spade", 1))
                .Returns(new Card("Spade", 2)).Returns(new Card("Spade", 7));
            deckMock.Setup(i => i.PeekCard()).Returns(briscolaCard);
            _sut = new Logic.Briscola(players, deckMock.Object);
            _sut.Start();
        }

        [Fact]
        public void StartTest()
        {
            _sut.PlayersList[0].HandCards.Count.Should().Be.EqualTo(3);
            _sut.PlayersList[1].HandCards.Count.Should().Be.EqualTo(3);

            p1Cards.Should().Have.SameSequenceAs(_sut.PlayersList[0].HandCards);
            p2Cards.Should().Have.SameSequenceAs(_sut.PlayersList[1].HandCards);
            _sut.TableCards.Should().Be.Empty();
            _sut.FirstToPlay.Should().Be.EqualTo(_p1);
            _sut.PlayersList[0].TakenCards.Count.Should().Be.EqualTo(0);
            _sut.PlayersList[1].TakenCards.Count.Should().Be.EqualTo(0);
        }

        [Fact]
        public void PlayHandTest()
        {
            _sut.PlayHand();

            _sut.PlayersList[0].HandCards.Count.Should().Be.EqualTo(2);
            _sut.PlayersList[1].HandCards.Count.Should().Be.EqualTo(2);

            p1Cards.Should().Contain(_sut.PlayersList[0].HandCards.First());
            p1Cards.Should().Contain(_sut.PlayersList[0].HandCards.Last());
            p2Cards.Should().Contain(_sut.PlayersList[1].HandCards.First());
            p2Cards.Should().Contain(_sut.PlayersList[1].HandCards.Last());
            _sut.TableCards.Keys
                .Should().Have.SameSequenceAs(new List<IPlayer>() { _p1, _p2 });
            p1Cards.Should().Contain(_sut.TableCards[_p1]);
            p2Cards.Should().Contain(_sut.TableCards[_p2]);
            _sut.FirstToPlay.Should().Be.EqualTo(_p1);
            _sut.PlayersList[0].TakenCards.Count.Should().Be.EqualTo(2);
            _sut.PlayersList[1].TakenCards.Count.Should().Be.EqualTo(0);
        }

        [Fact]
        public void EndTest()
        {
            _sut.PlayAllGame();

            _sut.WinnerPlayers.Count.Should().Be.EqualTo(1);
            _sut.WinnerPlayers.First().Should().Be.EqualTo(_p1);
            _sut.Evaluator.PlayerPointsDictionary[_p1].Should().Be.EqualTo(32);
            _sut.Evaluator.PlayerPointsDictionary[_p2].Should().Be.EqualTo(0);

        }


    }
}
