using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Briscola.Tdd.Model;

namespace Briscola.Tdd.Interfaces
{
    public interface IPlayer
    {
        List<Card> TakenCards { get; }
        List<Card> HandCards { get; }
        string Name { get; }
        bool NewGame();
        Card PlayCard(IGameState gameState);
        void GetCard(Card newCard);
        void TakeCards(List<Card> newCardList);
        bool ThereAreHandCards();
        void Reset();


    }
}
