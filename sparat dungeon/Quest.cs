using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace sparat_dungeon
{
    // 퀘스트를 추상클래스
    // 상속받은 3가지 퀘스트
    // 스크립트를 출력해주는 메서드
    // 달성 조건을 검사해주는 메서드가 있어 
    // 메인에서 퀘스트List에 인스턴스해서 추가해줌
    //   =========================================
    // 퀘스트 장면에서 출력
    // 퀘스트를 수락하면 IsAccept true로
    // 달성 조건에 값 감시
    // 달성시에 달성조건을 검사해서 달성조건 메서드 안에서 직접적으로 보상 수여
    // 플레이어에게 퀘스트 리스트 만들어주고
    // 퀘스트를 수락하면 추가 -> IsAccept같은 변수를 true
    // 플레이어에서 KillCount변수를 만들고 감시
    // 능력치나 장비는 get으로 다 받아와서 검사
    // 퀘스트를 완료하면 Completed 변수를 또 true로 만들고 보상 수여
    // IsAccept변수와 Completed변수가
    // 둘다 true면 다시 퀘스트 수주를 할 수 없게 막으면 되고 ㅇㅈ? 어 ㅇㅈ
    // 퀘스트 이름
    // 퀘스트 아이디
    // 수락 했는지
    // 완료했는지

    // 결론 메서드 3가지
    // 퀘스트 스크립트 출력 메서드
    // 완료 조건 감시 메서드
    // 보상 주는 메서드 (완료조건 감시 안에 존재)

    public class Quest
    {
        // 퀘스트 이름
        // 퀘스트 설명
        // 퀘스트 달성 조건
        // 보상

        

        public int Id;
        public string Title;
        public bool IsAccept;
        public bool IsCompleted;

        public virtual void ShowQuestUI() { }
        
        public virtual void AcceptQuest()
        {
            IsAccept = true;
        }

        public virtual void CheckComplete() { }
        public virtual void CheckComplete(Player player) { }
        public virtual void CheckComplete(Minion minion) { }

        public virtual void GiveReward() { }
    }

    public class KillQuest : Quest
    {
        public Player playerCh { get; set; }

        public KillQuest(Player player)
        {
            Id = 0;
            Title = "마을의 평화를 위해";
            IsAccept = false;
            IsCompleted = false;
            playerCh = player;
        }

        public override void ShowQuestUI()
        {
            Console.Clear();
            Console.WriteLine(Title);
            Console.WriteLine();
            Console.WriteLine("소협! 이 루탄촌 주변에 도적놈들과 파렴치한 놈들이 많소");
            Console.WriteLine("우리 마을에 젊은 장정들과 아녀자들이 잡혀가고 상인들도 털리오..");
            Console.WriteLine("지푸라기라도 잡는 심정으로 청하오.");
            Console.WriteLine("도적 뗴로부터 이 설명촌을 구해주시오!");
            Console.WriteLine();
            Console.WriteLine($"- 주변 적들 5명 처치"); // 조건 달기 (미니언 죽을때마다 숫자 올려주기)
            Console.WriteLine();
            Console.WriteLine("-보상-");
            Console.WriteLine("??"); // 괜찮은 걸로 설정 하기
            Console.WriteLine();
            Console.WriteLine();
            if(IsAccept)
            {
                Console.WriteLine("1. 보상 수령(미구현)");
                Console.WriteLine("2. 뒤로 가기");
            }
            else
            {
                Console.WriteLine("1. 수락");
                Console.WriteLine("2. 거절");
            }
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            string input = Console.ReadLine();
            if (input == "1")
            {
                // 만약 퀘스트 수락 중이면
                if(IsAccept)
                {
                    // 조건 검사후
                    CheckComplete();

                    // 조건 결과에 따라 보상 지급
                    if (!IsCompleted)
                    {
                        Console.WriteLine("아직 의뢰 조건을 채우지 못했소");
                    }
                    else if (IsCompleted)
                    {
                        GiveReward();
                    }
                }
                // 만약 수락중이 아니라면
                else
                {
                    // 퀘스트 수락
                    AcceptQuest();
                }

            }
            else if (input == "2")
            {
                // 뒤로 가기
            }
            else
            {
                Console.WriteLine("없는 선택이오..");
            }
        }
        public override void CheckComplete()
        {
            // 플레이어에 킬카운트 만들고 가져와서 충족요건 확인하기
            if(playerCh.QuestKillCount >= 5)
            {
                IsCompleted = true;
            }
        }

        public override void GiveReward()
        {
            Console.WriteLine("1번 퀘 완");
            
        }
    }

    public class EquipQuest : Quest
    {
        public Player playerCh { get; set; }
        public EquipQuest(Player player)
        {
            Id = 1;
            Title = "장비 장착 해보기";
            IsAccept = false;
            IsCompleted = false;
            playerCh = player;
        }

        public override void ShowQuestUI()
        {
            Console.Clear();
            Console.WriteLine(Title);
            Console.WriteLine();
            Console.WriteLine("더 강해지고 싶지 않소?");
            Console.WriteLine("대장간에서 장비를 구해 장착하면 더 강해질 수 있을거요.");
            Console.WriteLine("한번 시도 해보시오. 후회하지 않을거요.");
            Console.WriteLine();
            Console.WriteLine($"- 무기, 방어구 둘 다 장착하기"); // 조건 달기 (장착 관리에서 데이터 받기)
            Console.WriteLine();
            Console.WriteLine("-보상-");
            Console.WriteLine("??");  // 보상 생각하기
            Console.WriteLine();
            Console.WriteLine();
            if (IsAccept)
            {
                Console.WriteLine("1. 보상 수령(미구현)");
                Console.WriteLine("2. 뒤로 가기");
            }
            else
            {
                Console.WriteLine("1. 수락");
                Console.WriteLine("2. 거절");
            }
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            string input = Console.ReadLine();
            if (input == "1")
            {
                // 만약 퀘스트 수락 중이면
                if (IsAccept)
                {
                    // 조건 검사후
                    CheckComplete();

                    // 조건 결과에 따라 보상 지급
                    if (!IsCompleted)
                    {
                        Console.WriteLine("아직 의뢰 조건을 채우지 못했소");
                    }
                    else if (IsCompleted)
                    {
                        GiveReward();
                    }
                }
                // 만약 수락중이 아니라면
                else
                {
                    // 퀘스트 수락
                    AcceptQuest();
                }

            }
            else if (input == "2")
            {
                // 뒤로 가기
            }
            else
            {
                Console.WriteLine("없는 선택이오..");
            }
        }

        public override void CheckComplete()
        {
            // 장비 장착 중인지 확인
            if(Player.EquipWepon != null && Player.EquipArmor != null)
            {
                IsCompleted = true;
            }

        }

        public override void GiveReward()
        {
            Console.WriteLine("2번 퀘 완");
        }
    }

    public class StatusUpQuest : Quest
    {
        public Player playerCh { get; set; }
        public StatusUpQuest(Player player)
        {
            Id = 2;
            Title = "능력치 올려 보기";
            IsAccept = false;
            IsCompleted = false;
            playerCh = player;
        }
        public override void ShowQuestUI()
        {
            Console.Clear();
            Console.WriteLine(Title);
            Console.WriteLine();
            Console.WriteLine("이제 제법 무림인 같아 보이는 구만!");
            Console.WriteLine("더욱 강해지기 위해서는 공력을 올려야 한다고 들었소.");
            Console.WriteLine("공력를 올리기 위해서는 실전을 통해 수련해야 하지 않겠소?");
            Console.WriteLine("공력을 올려서 강해져보시오!");
            Console.WriteLine();
            Console.WriteLine("여러 전투를 통해서 공력 올릴 수 있습니다.");
            Console.WriteLine($"- 공격력 ?? , 방어력 ?? 달성하기"); // 조건 달기(+된 공격력 방어력 받아오기)
            Console.WriteLine();
            Console.WriteLine("-보상-");
            Console.WriteLine($"???"); // 보상 생각하기
            Console.WriteLine();
            Console.WriteLine();
            if (IsAccept)
            {
                Console.WriteLine("1. 보상 수령(미구현)");
                Console.WriteLine("2. 뒤로 가기");
            }
            else
            {
                Console.WriteLine("1. 수락");
                Console.WriteLine("2. 거절");
            }
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            string input = Console.ReadLine();
            if (input == "1")
            {
                // 만약 퀘스트 수락 중이면
                if (IsAccept)
                {
                    // 조건 검사후
                    CheckComplete();

                    // 조건 결과에 따라 보상 지급
                    if (!IsCompleted)
                    {
                        Console.WriteLine("아직 의뢰 조건을 채우지 못했소");
                    }
                    else if (IsCompleted)
                    {
                        GiveReward();
                    }
                }
                // 만약 수락중이 아니라면
                else
                {
                    // 퀘스트 수락
                    AcceptQuest();
                }

            }
            else if (input == "2")
            {
                // 뒤로 가기
            }
            else
            {
                Console.WriteLine("없는 선택이오..");
            }
        }

        public override void CheckComplete()
        {
            // 현재 공격력 몇인지 체크 
            //player.PlayerAtk

            //if(Player.PlayerAtk > 20 && player.PlayerDef > 20)
            //{
            //    IsCompleted = true;
            //}
        }

        public override void GiveReward()
        {
            // 보상 정해서 넣어주기
            Console.WriteLine("3번 퀘 완");
        }
    }
}
