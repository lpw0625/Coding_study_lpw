using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texas_Holdem
{
    public enum Suit
    {

        Clover = 1,
        Heart  = 2,
        Diamond = 3,
        Spade  = 4

    }

    public enum Rank
    {
        None = 0,
        Two = 2,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13,
        Ace = 14,

    }
    public class Card
    {
        public Suit CardSuit { get; private set; }
        public Rank CardRank { get; private set; }

        public Card(Suit _suit, Rank _rank)

        {
            CardSuit = _suit;
            CardRank = _rank;

        }


        public int GetCardScore()
        {
            return ((int)CardRank * 10) + (int)CardSuit;


            // 예: 스페이드 A = (14 * 10) + 4 = 144점
            //     클로버 A = (14 * 10) + 1 = 141점

            // 10을 곱하는 이유 = > 자릿수 나누기
            // 텍사스 홀덤 특성상 카드(Rank)로 승부가 니뉘어지기 때문. 
            // 숫자가 높은 카드를 이기게 하기 위함.
        }


        public string GetCardName()
        {
            string suitText;

            switch (CardSuit)
            {
                case Suit.Spade:
                    suitText = "♠";
                    break;

                case Suit.Diamond:
                    suitText = "◆";
                    break;

                case Suit.Heart:
                    suitText = "♥";
                    break;

                case Suit.Clover:
                    suitText = "♣";
                    break;

                default:
                    suitText = "";
                    break;
            }

            string rankText;
            switch (CardRank)
            {
                case Rank.Ace:
                    rankText = "A";
                    break;

                case Rank.King:
                    rankText = "K";
                    break;

                case Rank.Queen:
                    rankText = "Q";
                    break;

                case Rank.Jack:
                    rankText = "J";
                    break;

                default:

                    rankText = ((int)CardRank).ToString();
                    break;
            }

            // 2~10은 숫자 그대로 문자열로 변환

            return suitText + rankText;

           // 최종 반환 : 문양과 숫자를 합침.
        }
    }
}
