using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Briscola.Tdd.Interfaces;
using Briscola.Tdd.Logic;
using Moq;
using SharpTestsEx;
using Xunit;
using Briscola = Briscola.Tdd.Logic.Briscola;

namespace Briscola.Tdd.Test
{
    public class GameFactoryTest
    {
        private IGameFactory _sut;
        private readonly Mock<IPlayer> _p1Mock, _p2Mock, _p3Mock; 
        private Exception _exception;
        public GameFactoryTest()
        {
            _exception = null;
            _p1Mock=new Mock<IPlayer>();
            _p2Mock=new Mock<IPlayer>();
            _p3Mock=new Mock<IPlayer>();
            _p1Mock.Setup(x => x.NewGame()).Returns(true);
            _p2Mock.Setup(x => x.NewGame()).Returns(true);
            _p3Mock.Setup(x => x.NewGame()).Returns(true);
        }

        [Fact]
        public void GameFactoryGeneratesException()
        {
            try
            {
                _sut = new BriscolaFactory(new List<IPlayer>() {_p1Mock.Object});
            }
            catch (Exception e)
            {
                _exception = e;
            }
            _exception.Should().Not.Be.Null();
        }

        [Fact]
        public void GameFactoryCreatesGamesCorrectly()
        {
            try
            {
                _sut = new BriscolaFactory(new List<IPlayer>() { _p1Mock.Object, _p2Mock.Object });
            }
            catch (Exception e)
            {
                _exception = e;
            }
            _exception.Should().Be.Null();
            _sut.CreateGame().Should().Be.OfType<Logic.Briscola>();
        }

    }
}
