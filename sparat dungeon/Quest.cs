using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sparat_dungeon
{
    internal abstract class Quest
    {
        // 퀘스트 이름
        // 퀘스트 설명
        // 퀘스트 달성 조건
        // 보상

        public int Id;
        public string Title;
        public bool IsComplited;

        public abstract void ShowQuestUI();

        public abstract void CheckComplete(Player player);

        public  void ApplyReward()
        {
            // 퀘스트 달성시 보상
        }

        public  void StateChange()
        {
            // 퀘스트 상태 변경
        }

        public virtual void ShowCompleteUI()
        {
            // 완료시 출력
        }

    }

    internal class KillQuest : Quest
    {
        public override void ShowQuestUI()
        {
            Console.Clear();
            Console.WriteLine(Title);
            Console.WriteLine();
            Console.WriteLine("이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?");
            Console.WriteLine("마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!");
            Console.WriteLine("모험가인 자네가 좀 처치해주게!");
            Console.WriteLine();
            Console.WriteLine($"- 미니언 5마리 처치"); // 조건 달기 (미니언 죽을때마다 숫자 올려주기)
            Console.WriteLine();
            Console.WriteLine("-보상-");
            Console.WriteLine("쓸만한 방패 x 1");
            Console.WriteLine("5 G");
            Console.WriteLine();
            Console.WriteLine("1. 수락");
            Console.WriteLine("2. 거절");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            int input = int.Parse(Console.ReadLine());

        }

        public override void CheckComplete(Player player)
        {
            // 플레이어에 킬카운트 만들고 가져와서 충족요건 확인하기

        }

    }

    internal class EquipQuest : Quest
    {
        public override void ShowQuestUI()
        {
            Console.Clear();
            Console.WriteLine(Title);
            Console.WriteLine();
            Console.WriteLine("강해질 필요를 느끼지 않나?");
            Console.WriteLine("장비를 장착하면 좀 더 강해질 수 있을걸세!");
            Console.WriteLine("한번 시도 해보시게나!");
            Console.WriteLine($"- 무기, 방어구 둘 다 장착하기"); // 조건 달기 (장착 관리에서 데이터 받기)
            Console.WriteLine();
            Console.WriteLine("-보상-");
            Console.WriteLine("??");  // 보상 생각하기
            Console.WriteLine();
            Console.WriteLine("1. 수락");
            Console.WriteLine("2. 거절");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            int input = int.Parse(Console.ReadLine());
        }

        public override void CheckComplete(Player player)
        {
            // 플레이어에 킬카운트 만들고 가져와서 충족요건 확인하기

        }
    }

    internal class StatusUpQuest : Quest
    {
        public override void ShowQuestUI()
        {
            Console.Clear();
            Console.WriteLine(Title);
            Console.WriteLine();
            Console.WriteLine("이제 모험가의 태가 제법 나오는 구만");
            Console.WriteLine("더욱 더 강해지기 위해서는 레벨업을 해 능력치를 올려야 하네!");
            Console.WriteLine("더 강해지기 위해 노력해 보시게나!");
            Console.WriteLine($"- 공격력 ?? , 방어력 ?? 달성하기"); // 조건 달기(+된 공격력 방어력 받아오기)
            Console.WriteLine();
            Console.WriteLine("-보상-");
            Console.WriteLine($"???"); // 보상 생각하기
            Console.WriteLine();
            Console.WriteLine("1. 수락");
            Console.WriteLine("2. 거절");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            int input = int.Parse(Console.ReadLine());
        }

        public override void CheckComplete(Player player)
        {
            // 플레이어에 킬카운트 만들고 가져와서 충족요건 확인하기

        }
    }
}
