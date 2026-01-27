using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texas_Holdem
{
    public class HoldemManager
    {
        private Deck deck;
        private List<Player> players;

        private List<Card> communityCards; // 바닥에 깔리는 5장의 카드 공유

        public HoldemManager( )
        {
         deck = new Deck();
         players = new List<Player>();
         communityCards = new List<Card>();
        }

        public void PrepareGame() 
        {
            deck = new Deck(); // // 새로운 카드로 교체
            deck.Shuffle();
            communityCards.Clear();

            // players = new List<Player>();
            foreach (var p in players) p.ClearHand();

            // foreach: 하나씩 돌아가면서 처리
            // var p in players : 플레이어들(players)이라는 명단에서 한 명씩 꺼내서, 잠시 p라고 부르겠다
            // p.ClearHand() 그 p(플레이어)에게 ClearHand라는 명령(메서드)을 내려라

        
            System.Console.WriteLine(">> 새로운 라운드를 준비합니다. 덱을 섞었습니다.");
            
        }

        public Card DrawFromDeck()
        {
            return deck.DrawCard();
        }

        public void AddPlayer(Player p)
        {
            players.Add(p);
        }

        public void AddCommunityCards(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Card drawn = deck.DrawCard();
                if (drawn != null)
                {

                communityCards.Add(drawn);
                System.Console.WriteLine($"보드에 추가됨 : {drawn.GetCardName()}");

                }
                else
                {
                    System.Console.WriteLine("경고 : 덱에서 카드를 뽑을 수 없습니다.");
                }
            }
        }

        public void ShowBoard()
        {
            Console.WriteLine($"\n[현재 보드 상황 - 카드 수: {communityCards.Count}장]");
            
            System.Console.Write("\n[현재 보드 상황]");
            if (communityCards.Count == 0)

            {
            System.Console.Write("아직 카드가 깔리지 않았습니다.");
            }

            foreach (var card in communityCards)
            {
                System.Console.Write($"{card.GetCardName()} ");
            }
            System.Console.WriteLine();
        } 
    }
}
