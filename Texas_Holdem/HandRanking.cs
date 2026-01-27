using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Texas_Holdem
{
    public enum HandType
    {
        None = -1,
        HighCard = 0,
        OnePair,
        TwoPair,
        Triple,
        Straight,
        BackStraight,
        Mountain,
        Flush,
        FullHouse,
        FourCard,
        StraightFlush,
        RoyalStraightFlush
    }
    public  class HandRanking
    {

        // 1. 세부 족보 판정 로직 (Property/Methods)

        private bool IsOnePair(List<Card> cards)
        {
            for (int i = 0; i < cards.Count - 1; i++)
            {
                if (cards[i].CardRank == cards[i + 1].CardRank)

                return true;
            }

            return false;
        }

        private bool IsTwoPair(List<Card> cards)
        {
            Rank firstPair = Rank.None;
            Rank secondPair = Rank.None;

            for (int i = 0; i <cards.Count - 1; i++)
            {
                if(cards[i].CardRank == cards[i + 1].CardRank)
                {
                    firstPair = cards[i].CardRank;
                    break;
                }
            }
           
           if (firstPair != Rank.None)
            {
                for (int i = 0; i < cards.Count - 1; i++)
                {
                    if (cards[i].CardRank != firstPair && cards[i].CardRank == cards[i + 1].CardRank)
                    {
                        secondPair = cards[i].CardRank;
                        break;
                    }
                }
            }
            return secondPair != Rank.None; // 최종적으로 두 번째 페어까지 찾아야 투페어 
        }

        private bool IsTriple(List<Card> cards)
        {
            Rank foundTriple = Rank.None;
            for (int i = 0; i < cards.Count - 2; i++)
            {
                if (cards[i].CardRank == cards[i + 2].CardRank)
                {
                    foundTriple = cards[i].CardRank;
                    break;
                }
            }

            return foundTriple != Rank.None;
         
        }

        private HandType IsStraight(List<Card> _cards)
        {
            // 중복 숫자 제거 (스트레이트 판정 필수)
            List<int> ranks = new List<int>(); 
            foreach (var card in _cards)
            {
                int r = (int)card.CardRank;
                if(!ranks.Contains(r)) ranks.Add(r);
            }

            if (ranks.Count < 5)

            return HandType.None;
            

            // 1. 마운틴 (A, K, Q, J, 10)

            if (ranks.Contains(14) && ranks.Contains(13) && ranks.Contains(12) && ranks.Contains(11) && ranks.Contains(10))
            return HandType.Mountain;

            
            if(ranks.Contains(14) && ranks.Contains(5) && ranks.Contains(4) && ranks.Contains(3) && ranks.Contains(2))
            return HandType.BackStraight;

            for (int i = 0; i <= ranks.Count - 5; i++)
            {
                if (ranks[i] - ranks[i + 4] == 4)

                return HandType.Straight;
            }

            return HandType.None;

        }

        private bool IsFlush(List<Card> _cards)
        {
            int s = 0;
            int d = 0;
            int h = 0;
            int c = 0;

            foreach (var card in _cards)
            {
                if (card.CardSuit == Suit.Spade) 
                s++;

                else if (card.CardSuit == Suit.Diamond)
                d++;

                else if (card.CardSuit == Suit.Heart)
                h++;
                
                else if (card.CardSuit == Suit.Clover)
                c++;

            }

            return (s >= 5 || d >= 5 || h >= 5 || c >= 5);
        }

        private bool IsFullHouse(List<Card> _card)
        {
            Rank foundTriple = Rank.None;

            for (int i = 0; i <_card.Count - 2; i++)
            {
                if (_card[i].CardRank == _card[i + 2].CardRank)
                {
                    foundTriple = _card[i].CardRank;
                    break;
                }
            }

            if(foundTriple != Rank.None)
            {
                for(int i = 0; i < _card.Count - 1; i++)
                {
                    if(_card[i].CardRank != foundTriple && _card[i].CardRank == _card[i + 1].CardRank)

                    return true;
                }
            }
            return false;
        } 

        private bool IsFourCard(List<Card> _card)
        {
            Rank foundFourCard = Rank.None;

            for (int i = 0; i < _card.Count - 3; i++)
            {
                if (_card[i].CardRank == _card[i + 3].CardRank)
                {
                    foundFourCard = _card[i].CardRank;
                    break;
                }
            }

            return foundFourCard != Rank.None;
        }

        private HandType IsCheckStraightFlush(List<Card> _cards)
        {
            // 1. 무늬별로 그룹화 (문양별로 리스트를 따로 만듦)
            var groups = _cards.GroupBy(c => c.CardSuit);

            foreach (var group in groups)
            {
                if (group.Count() >= 5)
                {


                    // 1. 같은 무늬끼리 모으고 (group)
                    // 2. 숫자가 높은 것부터 정렬해서 (OrderByDescending)
                    // 3. 리스트로 딱 저장한다! (ToList)
                    // 해당 무늬 카드들만 점수 내림차순으로 정렬
                    List<Card> flushCards = group.OrderByDescending(c => (int)c.CardRank).ToList();

                    // 그 무늬 안에서 스트레이트 판정 호출
                    HandType result = IsStraight(flushCards);

                    if (result == HandType.Mountain) 

                    return HandType.RoyalStraightFlush;

                    if (result == HandType.BackStraight || result == HandType.Straight)

                    return HandType.StraightFlush;

                }
            }

            return HandType.None;
        }

        public HandType ComPareHandRank(List<Card> _myCards, List<Card> _communityCards)
        {
            List<Card> allCards = new List<Card>(_myCards);
            allCards.AddRange(_communityCards);
            var sorted = allCards.OrderByDescending(c => (int)c.CardRank).ToList();

            HandType shuffleResult = IsCheckStraightFlush(sorted);

            if (shuffleResult != HandType.None)

                return shuffleResult; 

            if (IsFourCard(sorted))

                return HandType.FourCard;

            if(IsFullHouse(sorted))

                return HandType.FullHouse;

            if(IsFlush(sorted))

                return HandType.Flush;

            HandType straightResult = IsStraight(sorted);

            if (straightResult != HandType.None)

                    return straightResult;

            if (IsTriple(sorted))

                return HandType.Triple;

            if (IsTwoPair(sorted))

                    return HandType.TwoPair;

            if (IsOnePair(sorted))

                return HandType.OnePair;


            return HandType.HighCard;


        }

    
      
    }
}
