using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Texas_Holdem
{
    public  class BettingManager
    {
        private int currentCallAmount = 100; // 기본 배팅 금액 100으로 설정.
        

        public bool ProcessBettingRound(List<Player> players)
        {
            Console.WriteLine($"\n[배팅 정보] 현재 콜 금액: {currentCallAmount}원");

            Player mySelf = players[0];

            Console.WriteLine($"[{mySelf.PlayerName}]의 차례! 잔액: {mySelf.GameMoney}원");
            Console.WriteLine("1. 콜(계속)  2. 폴드(기권)");



            // 1. 플레이어(나)의 결정
            // 리스트의 첫 2번


        }
    }
}
