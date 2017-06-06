using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Briscola.Tdd.Logic;

namespace Briscola.Tdd.Interfaces
{
    public interface IDeckFactory
    {
        IDeck CreateDeck();
    }
}
