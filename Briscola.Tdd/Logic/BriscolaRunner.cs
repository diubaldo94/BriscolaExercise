using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Briscola.Tdd.Interfaces;

namespace Briscola.Tdd.Logic
{
    public class BriscolaRunner : IGameRunner
    {
        private readonly List<IPlayer> _playerList ;
        private IPlayer _player;
        private int _playerNumber;
        private IGame _briscola;
        private readonly IGameFactory _briscolaFactory;
        public BriscolaRunner()
        {
            _playerList= new List<IPlayer>();
            ChooseYourName();
            ChooseNumberOfPlayers();
            bool otherPlayersNames = ChooseYesOrNot("Vuoi scegliere i nomi degli altri giocatori?");
            ChooseMoreNames(!otherPlayersNames);
            _briscolaFactory=new BriscolaFactory(this._playerList);
        }
        public void Initialize()
        {
            _briscola = _briscolaFactory.CreateGame();
            _briscola.Start();
        }

        public void Play()
        {
            _briscola.PlayAllGame();
        }

        public bool Continue()
        {
            if (ChooseYesOrNot("Vuoi continuare a giocare con gli stessi giocatori?"))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Grazie per aver giocato!");
                return false;
            }
        }

        public void Reset()
        {
            
        }

        private void ChooseYourName()
        {
            Console.WriteLine("Inserisci il tuo nome per poter giocare a _briscola");
            var playerName = Console.ReadLine();
            _player = new Player(playerName, new HumanStrategy());
            _playerList.Add(_player);
        }

        private void ChooseMoreNames(bool defaultNames)
        {
            ChooseName(2, defaultNames? "Pippo" : null);
            if (_playerNumber>2) ChooseName(3, defaultNames ? "Pluto" : null);
            if (_playerNumber==4) ChooseName(4, defaultNames ? "Paperino" : null);
        }

        private void ChooseName(int pos, string defaultName = null)
        {
            string playerName;
            if (defaultName == null)
            {
                if (pos == 2)
                    Console.WriteLine("Inserisci il nome del secondo giocatore.");
                if (pos == 3)
                    Console.WriteLine("Inserisci il nome del terzo giocatore.");
                if (pos == 4)
                    Console.WriteLine("Inserisci il nome del quarto giocatore.");
                playerName = Console.ReadLine();
            }
            else
            {
                playerName = defaultName;
            }
            _playerList.Add(new Player(playerName, new RandomStrategy()));
        }
        private void ChooseNumberOfPlayers()
        {
            while (true)
            {
                Console.WriteLine("Scegli il numero di giocatori (2, 3 o 4 giocatori).");
                _playerNumber = Convert.ToInt32(Console.ReadLine());
                if (_playerNumber == 2 || _playerNumber == 3 || _playerNumber == 4)
                    break;
                else
                {
                    Console.WriteLine("Input non valido.");
                }
            }
        }

        private static bool ChooseYesOrNot(string question)
        {
            while (true)
            {
                Console.WriteLine("{0} [Y/N]", question);
                var otherPlayersNames = Console.ReadLine();
                if (otherPlayersNames == "Y")
                    return true;
                else if (otherPlayersNames == "N")
                    return false;
                else
                    Console.WriteLine("Input non valido");
            }
        }
    }
}
