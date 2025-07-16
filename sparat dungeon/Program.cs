using System;
using System.Reflection;
using System.Threading;
using static sparat_dungeon.Battle;
using static sparat_dungeon.util;
using static System.Formats.Asn1.AsnWriter;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace sparat_dungeon

{
    public class Program
    {
        public static int state;
        public static int scene = 0;
        public static int scene_re = 0;
        public static Action mapFunc;

        private static Player player;
        private static int delay = 30;
        static List<Monster> monsters;
        public static int playerEnterHp;

        static int index_c;

        static Mercenary hiredMercenary = null;
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
            
            Title();
            LoopChoice();

        }


        static void LoopChoice()
        {
            while (true)
            {
                string choice = PlayerInput();

                if (state == 0)
                {
                    switch (scene)
                    {
                        case 0:
                            Title_choice(choice);
                            break;

                        case 1:
                            MainScene_choice(choice);
                            break;

                        case 2:

                            break;

                        case 3:

                            break;

                    }
                }
                else if (state == 1) // 상태창
                {
                    StatusScene_choice(choice);
                }
                else if (state == 2) // 인벤토리
                {
                    InventoryScene_choice(choice);
                }
                else
                {

                }
            }
        }

        static string PlayerInput()
        {
            while (Console.KeyAvailable) // 출력되는동안 텍스트 여러번 눌린거 초기화
            {
                Console.ReadKey(true);
            }
            Console.WriteLine();
            Console.SetCursorPosition(6, Console.CursorTop);
            Console.Write(": ");
            return Console.ReadLine();
        }

        public static void index_count()
        {
            index_c++;
            Console.SetCursorPosition(12, Console.CursorTop);
            Console.Write($"\u001b[38;2;255;194;89m{index_c}.\u001b[38;2;224;192;128m");
        }

        static void Title()
        {
            Console.CursorVisible = false;
            Console.WriteLine();
            Console.WriteLine();
            Util.Textcolor("검으로 선택지를 베어버렸다.",25,5);
            Console.WriteLine();
            Console.WriteLine();
            Thread.Sleep(500);
            Util.Textcolor("1.【입문하기 】", 10, 10);
            Util.Textcolor("2.【하산하기 】", 10, 10);
            Thread.Sleep(500);
            Console.ResetColor();
            Console.CursorVisible = true;
        }

        static void Title_choice(string choice)
        {
            if (choice == "1")
            {
                Console.WriteLine("게임을 시작합니다");
            }
            else if (choice == "2")
            {
                Console.WriteLine("게임을 종료합니다.");
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 게임을 종료합니다.");
            }

            Console.Clear();
            Console.CursorVisible = false;
            Console.WriteLine();
            Console.WriteLine();
            Util.Textcolor("당신의 이름을 알려주십시오, 협객이여.", 25, 5);
            Console.WriteLine();
            Thread.Sleep(500);
            Console.SetCursorPosition(6, Console.CursorTop);
            Console.Write(": ");
            Console.CursorVisible = true;
            string playerName = Console.ReadLine();

            Console.Clear();
            Console.CursorVisible = false;
            Console.WriteLine();
            Console.WriteLine();
            Util.Textcolor($"[{playerName}]…강호의 길에 들어선 당신.", 25, 5);
            Util.Textcolor("수련한 무공을 고르십시오.", 25, 5);
            Console.WriteLine();
            Thread.Sleep(500);
            Util.Textcolor("1.【검사 】", 10, 11);
            Util.Textcolor("2.【무승 】", 10, 11);
            Util.Textcolor("3.【기공사 】", 10, 11);
            Console.WriteLine();
            Thread.Sleep(500);
            Console.SetCursorPosition(6, Console.CursorTop);
            Console.Write(": ");
            Console.CursorVisible = true;
            string jobSelect = Console.ReadLine();
            string jobSelect_str = "검사";

            player = new Player(playerName, jobSelect);

            if (jobSelect == "1")
            {
                jobSelect_str = "검사";
            }
            else if (jobSelect == "2")
            {
                jobSelect_str = "무승";
            }
            else if (jobSelect == "3")
            {
                jobSelect_str = "기공사";
            }
            else
            {
                jobSelect = "1";
            }
            Console.Clear();
            Console.CursorVisible = false;
            Console.WriteLine();
            Console.WriteLine();
            Util.Textcolor($"이름: {playerName}", 10, 5);
            Util.Textcolor($"직업: {jobSelect_str}", 10, 5);
            Console.WriteLine();
            Util.Textcolor($"[{playerName}] 님, [{jobSelect_str}]의 길을 걷게 되었습니다.", 30, 5);
            Util.Textcolor($"강호의 운명이 당신을 기다리고 있습니다...", 30, 5);
            Thread.Sleep(1000);


            scene = 1;
            MainScene();
        }

        static void MainScene()
        {
            Console.Clear();
            index_c = 0;
            state = 0;
            SpawnMonster();
            Console.WriteLine();
            Console.WriteLine();
            Util.SetColor(224, 192, 128);
            Util.Write("스파르타 던전에 오신 여러분 환영합니다.",delay, 5);
            Util.Write("이제 전투를 시작할 수 있습니다.", delay, 5);
            Console.WriteLine();
            index_count();
            Util.Write("【상태 보기 】", delay, 15);
            index_count();
            Util.Write("【소지품 확인 】", delay, 15);
            index_count();
            Util.Write("【전투 시작 】", delay, 15);
            index_count();
            Util.Write("【퀘스트 】", delay, 15);
            index_count();
            Util.Write("【상점 】", delay, 15);
            index_count();
            Util.Write("【휴식 】", delay, 15);
            //Console.WriteLine("3. 전투 시작");
            //Console.WriteLine("4. 퀘스트"); // 촌장
            //Console.WriteLine("5. 상점"); // 용병
            //Console.WriteLine("6. 휴식"); // 객잔? 숙소? 여관?
            Console.ResetColor();
            Console.WriteLine("");
            monsters.Clear();
        }

        static void MainScene_choice(string choice)
        {
            if (choice == "1")
            {
                StatusScene();
            }
            else if (choice == "2")
            {
                InventoryScene();
            }
            else if (choice == "3")
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
                MainScene();
            }
            else if (choice == "4")
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
                MainScene();
            }
            else if (choice == "5")
            {
                Console.Clear();
                Console.WriteLine("상점에 오신것을 환영합니다.");
                Console.WriteLine("1. 용병");
                Console.WriteLine("0. 상점에서 나갑니다.");
                string shopInput = Console.ReadLine();
                if (shopInput == "1")
                {
                    if (player.PlayerGold >= 100)
                    {
                        player.PlayerGold -= 100;
                        hiredMercenary = new Mercenary("칼잡이 존", 15);
                        Console.WriteLine("용병 '칼잡이 존' 을 고용했습니다! (전투 1회용)");
                    }
                    else
                    {
                        Console.WriteLine("Gold가 부족합니다.");
                    }
                }
                else if (shopInput == "0")
                {
                    Console.WriteLine("상점을 나갑니다.");
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 상점을 종료합니다.");
                }
                Thread.Sleep(1000);
                Console.Clear();
                MainScene();
            }
            else if (choice == "6")
            {
                // 휴식 메서드 작동
                if (player.PlayerHp < 100)
                {
                    player.PlayerHp = 100;
                }
                else Console.WriteLine("\n당신은 휴식이 필요하지 않습니다.");
                Console.WriteLine($"\n현재체력 : {player.PlayerHp}");

                string restout = Console.ReadLine();
                if (restout == "0")
                {
                    Console.WriteLine("상태보기를 종료합니다.");
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 상태보기를 종료합니다.");
                }
                MainScene();
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                MainScene();
            }
        }

        static void StatusScene()
        {
            state = 1;
            Console.Clear();
            Console.WriteLine("상태보기\n캐릭터의 정보가 표시됩니다.");
            Console.WriteLine("");
            player.Playerinfo();
            Console.WriteLine("");
            Console.WriteLine("상태보기에서 나가려면 0 키를 누르세요.");
        }

        static void StatusScene_choice(string choice)
        {
            Console.WriteLine("상태보기를 종료합니다.");
            MainScene();
        }

        static void InventoryScene()
        {
            state = 2;
            Item.ShowInventory();
        }

        static void InventoryScene_choice(string choice)
        {
            //인벤토리
            if (choice == "1")
            {
                state = 3;
                EquipmentScene();
            }
            else if (choice == "2")
            {
                state = 4;
                ConsumableScene();
            }
            else
            {
                Console.WriteLine("인벤토리를 종료합니다.");
                MainScene();
            }
        }

        static void EquipmentScene()
        {
            Player.ShowInventoryE();
        }

        static void ConsumableScene()
        {
            Player.ShowInventoryC();
        }




        static void ShowBattleUI()
        {
            while (true)
            {
                Console.Clear();
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
            Random rand = new Random();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Battle!! - YourTurn");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine($"{player.PlayerName} 의 공격!!");
            int damage = player.PlayerDamageCalc();
            int avoid = rand.Next(1, 101);
            if (avoid < 11)
            {
                Console.WriteLine($"Lv.{monster.Level} {monster.Name} 을(를) 공격했지만 아무일도 일어나지 않았습니다.");
            }
            else
            {
                int critical = rand.Next(1, 101);
                if(critical < 16)
                {
                    damage = (int)(damage * 1.6f);
                    Console.WriteLine($"Lv.{monster.Level} {monster.Name} 을(를) 맞췄습니다. [데미지 : {damage}] - 치명타 공격!!");
                    Console.WriteLine();
                    Console.WriteLine($"Lv.{monster.Level} {monster.Name}");
                    Console.Write($"HP {monster.Hp}  -> ");
                    monster.TakeDamage(damage);
                    if (monster.IsDead == true)
                    {
                        Console.WriteLine("Dead");
                    }
                    else
                    {
                        Console.WriteLine($"{monster.Hp}");
                    }
                }
                else
                {
                    Console.WriteLine($"Lv.{monster.Level} {monster.Name} 을(를) 맞췄습니다. [데미지 : {damage}]");
                    Console.WriteLine();
                    Console.WriteLine($"Lv.{monster.Level} {monster.Name}");
                    Console.Write($"HP {monster.Hp}  -> ");
                    monster.TakeDamage(damage);
                    if (monster.IsDead == true)
                    {
                        Console.WriteLine("Dead");
                    }
                    else
                    {
                        Console.WriteLine($"{monster.Hp}");
                    }
                }
            }
            
            Console.WriteLine();

            Console.WriteLine("X. 다음");
            Console.WriteLine();
            Console.Write(">> ");

            Console.ReadLine();

            ShowMonsterPhaseUI();
            return;
        }

        static public void ShowMonsterPhaseUI()
        {
            bool allDead = true;

            foreach (Monster monster in monsters)
            {
                if (!monster.IsDead)
                {
                    allDead = false;
                }
            }
            if (allDead == false)
            {
                int playerEnterHp = player.PlayerHp;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Battle!! - Monster Turn");
                Console.ResetColor();
                Console.WriteLine();

                for (int i = 0; i < monsters.Count; i++)
                {
                    if (monsters[i].IsDead == false)
                    {
                        Console.WriteLine($"Lv.{monsters[i].Level} {monsters[i].Name} 의 공격!!");
                        Console.WriteLine($"{player.PlayerName}를 맞췄습니다. [데미지 : {monsters[i].DamageCalc()}]");
                        Console.Write(i == 0 ? $"HP {playerEnterHp} -> " : $"HP {player.PlayerHp} -> ");
                        monsters[i].ApplyDamage(player);
                        Console.WriteLine(player.PlayerHp <= 0 ? "Dead" : $"{player.PlayerHp}");
                        Console.WriteLine();
                    }
                }
                Console.WriteLine();
                Console.WriteLine("X. 다음");
                Console.WriteLine();
                Console.Write(">> ");

                Console.ReadLine();

            }
            if (player.PlayerHp <= 0)
            {
                ShowResultUI();  // 패배
                return;
            }
            else
            {
                if (player.PlayerHp > 0 && allDead)
                {
                    ShowResultUI(); // 승리
                }
            }
                //ShowBattleUI();  // 다시 공격 선택으로
                //return;                
        }

        static void ShowResultUI()
        {
            Console.Clear();
            // 플레이어 체력 0일 경우 패베 출력
            if (player.PlayerHp <= 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("You Lose - Result\n");
                Console.ResetColor();
                Console.WriteLine($"Lv.{player.PlayerLevel} {player.PlayerName}");
                Console.WriteLine($"HP {playerEnterHp} -> 0\n");
                Console.WriteLine("0. 다음\n");
                Console.Write(">> ");
                Console.ReadLine();
                Environment.Exit(0);
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
                int totalGold = 0;
                List<Item> droppedItems = new List<Item>();
                int MaxExp = player.GetExpToNextLevel();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Victory - Result\n");
                Console.ResetColor();
                Console.WriteLine($"던전에서 몬스터 {monsters.Count}마리를 잡았습니다.\n");
                foreach (var monster in monsters)
                {
                    totalExp += monster.MonsterExp();
                    totalGold += monster.DropGold();
                    player.PlayerGold += monster.DropGold();
                    
                    int itemIndex = monster.DropItem();
                    
                    if (itemIndex != -1)
                    {
                        Item droppedItem = Item.items[itemIndex];  
                        droppedItems.Add(droppedItem);            
                        Item.AddItem(itemIndex);                   
                        Console.WriteLine($"아이템 {droppedItem.Name}을(를) 획득했습니다.");
                    }
                }
                player.GainExp(totalExp);
                Console.WriteLine($"Lv.{player.PlayerLevel} {player.PlayerName}");
                Console.WriteLine($"HP {playerEnterHp} -> {player.PlayerHp}\n");
                Console.WriteLine("0. 다음\n");
                Console.Write(">> ");
                Console.ReadLine();
            }
            hiredMercenary = null;
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
