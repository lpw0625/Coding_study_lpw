using System.Linq.Expressions;

namespace Texas_Holdem
{
    public class Program
    {
        static void Main(string[] args)
        {

            // 1. 매니저와 플레이어 생성
            HoldemManager holdemManager = new HoldemManager();
            Player mySelf = new Player("플레이어1");
            Player cpu = new Player("컴퓨터(CPU)");

           string[] playerNames = { "나", "CPU1", "CPU2", "CPU3", "CPU4" };
           List<Player> allPlayers = new List<Player>();

           foreach (var name in playerNames)
            {
                Player p = new Player(name, 10000);
                holdemManager.AddPlayer(p);
                allPlayers.Add(p);
            }

            holdemManager.AddPlayer(mySelf);
            holdemManager.AddPlayer(cpu);

            // 2. 라운드 준비 (덱 셔플 등)
            holdemManager.PrepareGame();

            // 3. pre - flop : 각자 카드 2장씩 받기

            mySelf.ReceiveCard(holdemManager.DrawFromDeck());
            mySelf.ReceiveCard(holdemManager.DrawFromDeck());

            cpu.ReceiveCard(holdemManager.DrawFromDeck());
            cpu.ReceiveCard(holdemManager.DrawFromDeck());

           
            System.Console.WriteLine("=== [1. 프리플랍 : 손패 확인 ] ===");
            mySelf.ShowHand();
            cpu.ShowHand();


           // 4. Flop: 바닥 카드 3장 공개
           Console.WriteLine("\n=== [ 2. 플랍: 바닥 3장 공개 ] ===");
           holdemManager.AddCommunityCards(3);
           holdemManager.ShowBoard();
           

            // 5. Turn: 바닥 카드 1장 추가
            Console.WriteLine("=== [ 3. 턴: 바닥 1장 추가 ] ===");
            holdemManager.AddCommunityCards(1);
            holdemManager.ShowBoard();


            // 6. River: 바닥 카드 마지막 1장 추가
            Console.WriteLine("=== [ 4. 리버: 마지막 카드 공개 ] ===");
            holdemManager.AddCommunityCards(1);
            holdemManager.ShowBoard();

            System.Console.WriteLine("라운드 종료. 승자는 누굴까요? ");
      

          

        }
    }
}


// 택사스 홀덤 게임 기능 구현 

// 먼저 플레이어는 최소 5명이상으로 시작을 한다

// 텍사스 홀덤은 공유패가 5장 개인패 2장으로 하여 공유패와 개인패를 합쳐서 가장 높은 패를 가진 족보가 승리를 하게 된다.

// 플레이어는 가지고 있는 칩을 생각하여 레이즈,체크,콜,폴드,올인을 할 수가 있다.

// 플레이어는 각각 기본 금액 10,000원을 가지고 게임을 시작한다.

// 플레이어는 100원을 시작 금액으로 배팅하여 게임을 하게 한다.

// 레이즈 : 배팅 금액을 정하는 기능 : => 1.5배, 2배, 3배 버튼을 나눌 예정

// 콜 : 배팅한 플레이어의 금액만큼 지불하면 턴을 이어가게 된다.

// 체크 : 배팅을 하지 않고 공유패를 확인을 하는 과정 => 남아 있는 플레이어가 체크를 다 하게 될 경우 금액을 배팅하지 않고 공유패가 플랍하게 된다.

// 폴드 : 게임 포기

// 올인 : 플레어의 소지 금액을 전부 배팅을 하게 된다. 



// 카드 문양 순서

// 스페이드 >  다이아몬드  >  하트 > 클로버 >

// 카드 숫자 순서.

// 2 > 3 > 4 > 5 > 6 > 7 > 8 > 9 > 10 > J(11) > Q(12) > K(13) > A(14)



// 족보

// 로열스트레이트 플러쉬(5장 카드가 문양이 같고 카드의 숫자가 10, J, Q, K,A 만이 순차적으로 카드가 놓여질경우)

// 스트레이트 플러쉬(5장 카드가 문양이 같고 숫자가 순차적일경우)

// 포카드 (같은 숫자의 카드가 4장이 있을 경우)

// 풀하우스 (같은 숫자 3개 (트리플)과 같은 숫자 2개(원페어)로 되어 있을 경우

// 플러쉬(5장 카드가 문양이 같을 경우)

// 마운틴 (카드 문양은 같지 않지만 10, J, Q, K,A 만이 순차적으로 카드가 놓여질경우

// 스트레이트(문양은 같지 않지만 5장 카드가 순차적으로 가질 경우)

// 트리플 (같은 숫자 카드가 3장이 있을 경우)

// 투페어 (같은 숫자 두 개(원페어)가 두 쌍이 있을 경우

// 원페어 (같은 숫자 두 개가 있을 경우

// 하이 카드 5장 카드 중 위의 경우에 속하지 않고 5장 카드 중 가장 높은 숫자를 가진 카드 1장

// 단, 같은 족보의 패가 나올 경우 숫자가 높은 쪽이 이긴다.

// 단, 같은 숫자의 문양이 다른 원페어가 같이 나올 경우 나머지 3장의 카드의 숫자가 큰 사람이 이긴다.