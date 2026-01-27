using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texas_Holdem
{
    public class Deck
    {

        // 카드를 담아둘 리스트

        private List<Card> PockerCards;
        private Random random = new Random();


        public Deck()
        {

            PockerCards = new List<Card>();

            GenerateDeck();


        }

        private void GenerateDeck()
        {
            PockerCards.Clear();

            foreach (Suit s in Enum.GetValues(typeof(Suit)))
            {

                foreach (Rank r in Enum.GetValues(typeof(Rank)))
                {
                    if (r == Rank.None)

                        continue;

                    PockerCards.Add(new Card(s, r));
                }
            }
        }

        public void Shuffle()
        {
            if(PockerCards.Count == 0)
            GenerateDeck();

            PockerCards = PockerCards.OrderBy(x => random.Next()).ToList();
        }

        public Card DrawCard()
        {
            if (PockerCards.Count == 0)

                return null; // 카드가 없으면 Null 반환

            Card cardToDraw = PockerCards[0];
            PockerCards.RemoveAt(0);

            return cardToDraw;
        }

    }
}
 
