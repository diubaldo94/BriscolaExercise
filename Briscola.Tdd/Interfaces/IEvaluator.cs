using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Briscola.Tdd.Logic;
using Briscola.Tdd.Model;

namespace Briscola.Tdd.Interfaces
{
    public interface IEvaluator
    {
        Dictionary<IPlayer, int> PlayerPointsDictionary { get; }
        IPlayer GetHandWinnerPlayer(IGameState gameState);
        IPlayer GetGameWinnerPlayer(List<IPlayer> playerList);
        List<IPlayer> GetGameWinnerPlayers(List<IPlayer> playerList);
        Card GetHandWinningCard(IGameState gameState);
    }
}
