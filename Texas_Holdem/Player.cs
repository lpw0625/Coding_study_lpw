using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texas_Holdem
{
    public class Player
    {
        public string PlayerName { get; private set; }
        public int GameMoney { get; private set; }

        public HandType FinalHand { get; set; }

        // 플레이어가 손에 들고 있는 카드 리스트 (텍사스 홀덤 => 2장) 
        public List<Card> Hand { get; private set; }

        public bool IsFolded { get; set; }

        public Player (string _playerName, int _ininitialMoney = 10000)
        {
            PlayerName = _playerName;
            GameMoney = _ininitialMoney;
            Hand = new List<Card>();
            IsFolded = false; // 기본값은 게임 참여로 한다는 가정.

        }

        public void ReceiveCard(Card _card)
        {
            if (_card != null)
            {
                Hand.Add(_card);
            }
        }

        public bool Betting (int amount)
        {
            if (amount <= GameMoney)
            {
                GameMoney -= amount;
                return true;
            }
            else
            {
                Console.WriteLine($"{PlayerName}님, 잔액이 부족합니다.");
                return false; // 배팅 실패
            }
        }

        public void ShowHand()
        {
            Console.WriteLine($"{PlayerName}의 카드: ");
            foreach (Card _card in Hand)
            {
                Console.Write($"{_card.GetCardName()}");
            }
            Console.WriteLine();
        }

        public void ClearHand()
        {
            
            Hand.Clear();
            IsFolded = false; // 새 라운드가 시작되면 기권 상태를 초기화.
        }
    }
}
