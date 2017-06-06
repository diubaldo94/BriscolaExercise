using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Briscola.Tdd.Interfaces;
using Briscola.Tdd.Model;

namespace Briscola.Tdd.Logic
{
    public class BriscolaFactory : IGameFactory
    {
        /*Per ogni nuovo gioco creare una classe GiocoFactory che implementi 
         * IGameFactory e la rispettiva classe Gioco, che implementa IGame con l'effettivo 
         * funzionamento */
        public List<IPlayer> PlayersList;
        public IDeckFactory DeckFactory;

        public BriscolaFactory(List<IPlayer> playersList)
        {
            DeckFactory=new DeckFactory(new string[]{ "Denari", "Coppe", "Bastoni", "Spade"}, new Range(1,10), playersList.Count==3? 1 : 0);
            if (CorrectParameters(playersList))
            {
                PlayersList = playersList;
            }
            else
            {
                throw new Exception("Numero giocatori non valido");
            }
        }

        public IGame CreateGame()
        {
            return new Briscola(PlayersList, DeckFactory.CreateDeck());
        }
        
        private bool CorrectParameters(List<IPlayer> playerList)
        {
            if (playerList.Count() == 2 || playerList.Count()==3 || playerList.Count() == 4)
                return true;
            else
                return false;
        }
    }
}
