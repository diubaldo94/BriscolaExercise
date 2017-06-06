using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Briscola.Tdd.Interfaces
{
    public interface IGameFactory
    {
        IGame CreateGame();
    }
}
