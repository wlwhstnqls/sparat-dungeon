using System;
using System.Reflection;
using System.Threading;
using static sparat_dungeon.Battle;
namespace sparat_dungeon

{
    public class Program
    {
        private static Player player;
        static List<Monster> monsters;
        public static int playerEnterHp;
        static void Main(string[] args)
        {
            //Battle.BattleSystem battleSystem = new Battle.BattleSystem();
            monsters = new List<Monster>();
            
            List<Quest> quests = new List<Quest>()
            {
                //new Quest(1, "마을을 위협하는 미니언 처치", false, QuestState.NotStarted),
                //new Quest(2, "장비를 장착해보자", false, QuestState.NotStarted),
                //new Quest(3, "더욱 더 강해지기", false, QuestState.NotStarted)
            };

            Console.WriteLine("스파르타 던전 게임에 오신 것을 환영합니다!");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("1.START");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("2.EXIT");
            Console.ResetColor();
            string gmaeStart = Console.ReadLine();
            if (gmaeStart == "1")
            {
                Console.WriteLine("게임을 시작합니다");

            }
            else if (gmaeStart == "2")
            {
                Console.WriteLine("게임을 종료합니다.");
                return;
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 게임을 종료합니다.");
                return;
            }
            Thread.Sleep(1000);
            Console.Clear();

            Console.Write("당신의 이름은? : ");
            string playerName = Console.ReadLine();
            Console.Write("당신의 직업은?(1.전사\n2.도적) : ");
            string jobSelect = Console.ReadLine();

            player = new Player(playerName, jobSelect);

            if (jobSelect == "1")
            {
                Console.WriteLine("전사를 선택하셨습니다.");
            }
            else if (jobSelect == "2")
            {
                Console.WriteLine("도적을 선택하셨습니다.");
            }
            else
            {
                Console.WriteLine("잘못된 직업입니다. 전사로 설정합니다.");
                jobSelect = "1";
            }

            while (true)
            {
                Console.Clear();
                SpawnMonster();
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n이제 전투를 시작할 수 있습니다.");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("1. 상태 보기");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("2. 인벤토리");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("3. 전투 시작");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("4. 퀘스트");
                Console.ResetColor();
                Console.WriteLine("");
                Console.WriteLine("원하는 행동을 입력해주세요.");

                string input = Console.ReadLine();
                monsters.Clear();
                if (input == "1")
                {
                    Console.WriteLine("상태보기\n캐릭터의 정보가 표시됩니다.");
                    Console.WriteLine("");
                    player.Playerinfo();
                    Console.WriteLine("");
                    //플레이어 상태를 여기에 추가
                    Console.WriteLine("상태보기에서 나가려면 0 키를 누르세요.");
                    string statusout = Console.ReadLine();
                    if (statusout == "0")
                    {
                        Console.WriteLine("상태보기를 종료합니다.");
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다. 상태보기를 종료합니다.");
                    }
                    Console.Clear();
                }
                else if (input == "2")
                {

                    //인벤토리
                    Item.ShowInventory();
                    string inventoryout = Console.ReadLine();
                    if (inventoryout == "2")
                    {
                        Player.ShowInventoryC();
                    }
                    else if (inventoryout == "1")
                    {
                        Player.ShowInventoryE();
                    }
                    if (inventoryout == "0")
                    {
                        Console.WriteLine("인벤토리를 종료합니다.");
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다. 인벤토리를 종료합니다.");
                    }
                    Console.Clear();
                }
                else if (input == "3")
                {
                    Console.Clear();
                    bool allDead = true;
                    foreach (var m in monsters)
                    {
                        if (!m.IsDead)
                        {
                            allDead = false;
                            break;
                        }
                    }

                    // 몬스터가 없거나 전부 죽었을 때만 새로 생성
                    if (monsters.Count == 0 || allDead)
                    {
                        monsters.Clear();  // 안전하게 초기화
                        SpawnMonster();
                    }
                    playerEnterHp = player.PlayerHp;
                    ShowBattleUI();
                }
                else if (input == "4")
                {
                    Console.Clear();
                    Console.WriteLine("Quest!!");
                    Console.WriteLine("");
                    Console.WriteLine("1. 마을을 위협하는 미니언 처치");
                    Console.WriteLine("2. 장비를 장착해보자");
                    Console.WriteLine("3. 더욱 더 강해지기");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("원하시는 퀘스트를 선택해주세요.");
                    Console.WriteLine("뒤로 가시려면 0을 입력해주세요.");
                    Console.Write(">> ");

                    int questinput = int.Parse(Console.ReadLine());

                    switch (questinput)
                    {
                        case 0:
                            // 메인 화면으로 이동
                            break;
                        case 1:
                            // 1번 퀘스트 출력
                            //quests[0].ShowQuestUI(1);
                            break;
                        case 2:
                            // 2번 퀘스트 출력
                            //quests[1].ShowQuestUI(2);
                            break;
                        case 3:
                            // 3번 퀘스트 출력
                            //quests[2].ShowQuestUI(3);
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                    Console.Clear();
                }
            }
        }
        static void ShowBattleUI()
        {
            Console.Clear();
            while (true)
            {
                Console.WriteLine("Battle!!");
                Console.WriteLine();
                for (int i = 0; i < monsters.Count; i++)
                {
                    if (monsters[i].IsDead == true)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"{i + 1} Lv.{monsters[i].Level} {monsters[i].Name} HP 0");
                    }
                    else
                    {
                        Console.WriteLine($"{i + 1} Lv.{monsters[i].Level} {monsters[i].Name} HP {monsters[i].Hp}");
                    }
                    Console.ResetColor();
                }
                Console.WriteLine();
                Console.WriteLine();

                // 플레이어 정보 가져오기
                Console.WriteLine("[내 정보]");
                Console.WriteLine($"Lv.{player.PlayerLevel} {player.PlayerName} ({player.PlayerJob})");
                Console.WriteLine($"HP {player.PlayerHp} / 100");
                Console.WriteLine();
                Console.WriteLine("0. 취소");
                Console.WriteLine();
                Console.WriteLine("대상을 선택해주세요.");
                Console.WriteLine();
                Console.Write(">> ");

                int result = CheckInput(0, monsters.Count);

                if (result == 0)
                {
                    // 전투 취소
                    return;
                }

                int monIdx = result - 1;
                if (!monsters[monIdx].IsDead)
                {
                    ShowPlayerAttackUI(monsters[monIdx]);
                    // 정상 공격 후 빠져나가기
                }
                else
                {
                    Console.WriteLine("이미 사망했습니다.");
                    Console.WriteLine("0. 다시 선택");
                    Console.Write(">> ");
                    Console.ReadLine(); // 사용자 확인 기다렸다가 다시 반복
                }
            }
        }
        static void SpawnMonster()
        {
            Random random = new Random();
            int spawnNum = random.Next(1, 5);
            

            for (int i = 0; i < spawnNum; i++)
            {
                int monsterSpawn = random.Next(3);
                switch (monsterSpawn)
                {
                    case 0:
                        monsters.Add(new Minion());
                        break;
                    case 1:
                        monsters.Add(new VoidBug());
                        break;
                    case 2:
                        monsters.Add(new CanonMinion());
                        break;
                }
            }
        }

        static void ShowPlayerAttackUI(Monster monster)
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();
            Console.WriteLine($"{player.PlayerName} 의 공격!!");
            Console.WriteLine($"Lv.{monster.Level} {monster.Name} 을(를) 맞췄습니다. [데미지 : {player.PlayerDamageCalc()}]");
            Console.WriteLine();
            Console.WriteLine($"Lv.{monster.Level} {monster.Name}");
            Console.Write($"HP {monster.Hp}  -> ");
            monster.TakeDamage(player.PlayerDamageCalc());
            if (monster.IsDead == true)
            {
                Console.WriteLine("Dead");
            }
            else
            {
                Console.WriteLine($"{monster.Hp}");
            }
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();
            Console.Write(">> ");

            int input = int.Parse(Console.ReadLine());

            if (input == 0)
            {
                ShowMonsterPhaseUI();
                return;
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }

        static public void ShowMonsterPhaseUI()
        {

            for (int i = 0; i < monsters.Count; i++)
            {
                int playerEnterHp = player.PlayerHp;
                Console.Clear();
                Console.WriteLine("Battle!!");
                Console.WriteLine();

                if (monsters[i].IsDead == false)
                {
                    Console.WriteLine($"Lv.{monsters[i].Level} {monsters[i].Name} 의 공격!!");
                    Console.WriteLine($"{player.PlayerName}를 맞췄습니다. [데미지 : {monsters[i].DamageCalc()}]");

                    Console.Write($"HP {playerEnterHp} -> ");
                    monsters[i].ApplyDamage(player);
                    Console.WriteLine(player.PlayerHp <= 0 ? "Dead" : $"{player.PlayerHp}");
                    
                    Console.WriteLine();
                    Console.WriteLine("0. 다음");
                    Console.WriteLine();
                    Console.Write(">> ");

                    int result = CheckInput(0, 0);


                    if (result == 0)
                    {
                        if (player.PlayerHp <= 0)
                        {
                            ShowResultUI();  // 패배
                            return;
                        }

                        if (i == monsters.Count - 1)
                        {
                            //ShowBattleUI();  // 다시 공격 선택으로
                            return;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다.");
                    }                   
                }
            }

            bool allDead = true;
            foreach (var m in monsters)
            {
                if (!m.IsDead)
                {
                    allDead = false;
                    break;
                }
            }

            if (player.PlayerHp > 0 && allDead)
            {
                ShowResultUI(); // 승리
            }
        }

        static void ShowResultUI()
        {
            Console.Clear();
            // 플레이어 체력 0일 경우 패베 출력
            if (player.PlayerHp <= 0)
            {
                Console.WriteLine("You Lose\n");
                Console.WriteLine($"Lv.{player.PlayerLevel} {player.PlayerName}");
                Console.WriteLine($"HP {playerEnterHp} -> 0\n");
                Console.WriteLine("0. 다음\n");
                Console.Write(">> ");
                Console.ReadLine();
                // 게임 종료?
            }

            bool allDead = true;
            foreach (Monster monster in monsters)
            {
                if (!monster.IsDead)
                {
                    allDead = false;
                    break;
                }
            }

            if (allDead)
            {
                int totalExp = 0;
                int MaxExp = player.GetExpToNextLevel();
                foreach (var monster in monsters)
                {
                    totalExp += monster.MonsterExp();
                }
                player.GainExp(totalExp);
                Console.WriteLine("Victory\n");
                Console.WriteLine($"던전에서 몬스터 {monsters.Count}마리를 잡았습니다.\n");
                Console.WriteLine($"Lv.{player.PlayerLevel} {player.PlayerName}");
                Console.WriteLine($"경험치 {totalExp} / {MaxExp}");
                Console.WriteLine($"HP {playerEnterHp} -> {player.PlayerHp}\n");
                Console.WriteLine("0. 다음\n");
                Console.Write(">> ");
                Console.ReadLine();
            }            
        }

        static int CheckInput(int min, int max)
        {
            int result;

            while (true)
            {
                string input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out result);
                if (isNumber)
                {
                    if (result >= min && result <= max)
                    {
                        return result;
                    }
                }
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
}
