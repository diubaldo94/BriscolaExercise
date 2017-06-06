using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Briscola.Tdd.Interfaces;
using Briscola.Tdd.Model;

namespace Briscola.Tdd.Logic
{
    public class BriscolaEvaluator : IEvaluator
    {
        private readonly string _briscolaSeed;
        private readonly List<int> _cardNumberScale;
        private readonly Dictionary<int, int> _pointForNumber;
        private readonly Dictionary<IPlayer, int> _playerPointsDictionary; 

        public Dictionary<IPlayer, int> PlayerPointsDictionary { get { return _playerPointsDictionary;} }
        
        public BriscolaEvaluator(string briscolaSeed)
        {
            _playerPointsDictionary=new Dictionary<IPlayer, int>();
            _briscolaSeed = briscolaSeed;
            _cardNumberScale= new List<int>() {2,4,5,6,7,8,9,3,1};
            _pointForNumber= new Dictionary<int, int>();
            _pointForNumber.Add(8,2);
            _pointForNumber.Add(9,3);
            _pointForNumber.Add(10,4);
            _pointForNumber.Add(3,10);
            _pointForNumber.Add(1,11);
            
        }

        public IPlayer GetGameWinnerPlayer(List<IPlayer> playerList)
        {
            foreach (var player in playerList)
            {
                _playerPointsDictionary.Add(player,0);
                foreach (var card in player.TakenCards)
                {
                    int valuePoint;
                    if (_pointForNumber.TryGetValue(card.Value, out valuePoint))
                        _playerPointsDictionary[player] += valuePoint;
                }
            }
            IPlayer winner = _playerPointsDictionary.OrderBy(i => i.Value).Last().Key;
            int score = _playerPointsDictionary.OrderBy(i => i.Value).Last().Value;
            Console.WriteLine("Congratulazioni a {0} per aver vinto questa partita con punteggio {1}", winner.Name, score);
            return winner;
        }

        public IPlayer GetHandWinnerPlayer(IGameState gameState)
        {
            var mainSeed = gameState.TableCards.Values.Select(i => i.Seed).Contains(_briscolaSeed) ? _briscolaSeed : gameState.TableCards.First().Value.Seed;
            gameState.Display();
            return gameState.TableCards.Where(i => i.Value.Seed == mainSeed).OrderBy(i => _cardNumberScale.IndexOf(i.Value.Value)).Last().Key;
        }

        public Card GetHandWinningCard(IGameState gameState)
        {
            string mainSeed;
            if (gameState.TableCards.Values.Select(i => i.Seed).Contains(_briscolaSeed))
                mainSeed = _briscolaSeed;
            else
                mainSeed = gameState.TableCards.First().Value.Seed;
            gameState.Display();
            return gameState.TableCards.Where(i => i.Value.Seed == mainSeed).OrderBy(i => _cardNumberScale.IndexOf(i.Value.Value)).Last().Value;
        }

        public List<IPlayer> GetGameWinnerPlayers(List<IPlayer> playerList)
        {
            foreach (var player in playerList)
            {
                _playerPointsDictionary.Add(player, 0);
                foreach (var card in player.TakenCards)
                {
                    int valuePoint;
                    if (_pointForNumber.TryGetValue(card.Value, out valuePoint))
                        _playerPointsDictionary[player] += valuePoint;
                }
            }
            int scoreFirstAndThirdPlayer = _playerPointsDictionary[playerList[0]] +
                                           _playerPointsDictionary[playerList[2]];
            int scoreSecondAndFourthPlayer = _playerPointsDictionary[playerList[1]] +
                                             _playerPointsDictionary[playerList[3]];
            if (scoreSecondAndFourthPlayer > scoreFirstAndThirdPlayer)
            {
                Console.WriteLine("Congratulazioni a {0} e {1} per aver vinto questa partita con punteggio {2}",
                    playerList[1].Name, playerList[3].Name, scoreSecondAndFourthPlayer);
                return new List<IPlayer>() {playerList[1], playerList[3]};
            }
            else
            {
                Console.WriteLine("Congratulazioni a {0} e {1} per aver vinto questa partita con punteggio {2}",
                    playerList[0].Name, playerList[2].Name, scoreFirstAndThirdPlayer);
                return new List<IPlayer>() { playerList[0], playerList[2] };
            }
        }
    }
}
