using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Briscola.Tdd.Logic;
using Briscola.Tdd.Model;

namespace Briscola.Tdd.Interfaces
{
    public interface IGame
    {
        Dictionary<IPlayer, Card> TableCards { get; }
        IPlayer FirstToPlay { get; }
        List<IPlayer> WinnerPlayers { get; }
        List<IPlayer> PlayersList { get; }
        IEvaluator Evaluator { get; }

        void Start();
        void PlayHand();

        void PlayAllGame();
        void EndGame(bool finishedGame);
    }
}
