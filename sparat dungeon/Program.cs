using System;
using System.Reflection;
using System.Threading;

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
        public static int debug = 0;

        private static Player player;
        private static int delay = 30;
        static List<Monster> monsters = new List<Monster>();
        static List<Quest> quests = new List<Quest>();
        public static int playerEnterHp;

        static int index_c;

        static Mercenary hiredMercenary = null;
        static void Main(string[] args)
        {
            SetQuest();
            Title();
            LoopChoice();
        }





        static void LoopChoice()
        {
            while (true)
            {
                string choice;

                if (state == 0)
                {
                    switch (scene)
                    {
                        case 0:
                            choice = PlayerInput();
                            Title_choice(choice);
                            break;

                        case 1:
                            choice = PlayerInput();
                            MainScene_choice(choice);
                            break;

                        case 2:
                            Rest();
                            choice = PlayerInput();
                            Rest_choice(choice);
                            break;

                        case 3:
                            choice = PlayerInput();
                            WhiteSwordMan1_choice(choice);
                            break;

                        case 4:
                            QuestScene();
                            choice = PlayerInput();
                            QuestScene_choice(choice);
                            break;

                        case 5:
                            choice = PlayerInput();
                            WhiteSwordMan2_choice(choice);
                            break;

                    }
                }
                else if (state == 1) // 상태창
                {
                    choice = PlayerInput();
                    StatusScene_choice(choice);
                }
                else if (state == 2) // 인벤토리
                {
                    choice = PlayerInput();
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
            Util.Textcolor("검으로 선택지를 베어버렸다.", 25, 5);
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
            }
            else if (choice == "2")
            {
                return;
            }
            else if (choice == "3") // 디버그 모드
            {
                Console.WriteLine("Debug mode");
                debug = 1;
            }
            else
            {
                return;
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
            Util.Textcolor("1.【검호 】", 10, 11);
            Util.Textcolor("2.【자객 】", 10, 11);
            Util.Textcolor("3.【도인 】", 10, 11);
            Console.WriteLine();
            Thread.Sleep(500);
            Console.SetCursorPosition(6, Console.CursorTop);
            Console.Write(": ");
            Console.CursorVisible = true;
            string jobSelect = Console.ReadLine();
            string jobSelect_str = "검호";

            player = new Player(playerName, jobSelect);

            if (jobSelect == "1")
            {
                jobSelect_str = "검호";
            }
            else if (jobSelect == "2")
            {
                jobSelect_str = "자객";
            }
            else if (jobSelect == "3")
            {
                jobSelect_str = "도인";
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
            player.SetSkill();
        }

        static void MainScene()
        {
            Console.Clear();
            index_c = 0;
            SpawnMonster();
            Console.WriteLine();
            Console.WriteLine();
            Util.SetColor(224, 192, 128);
            Util.Write("당신의 눈앞에, 자그마한 마을이 모습을 드러냅니다.", delay, 5);
            Thread.Sleep(500);
            Console.WriteLine();
            Util.Write("마을 입구에는 비바람에 닳아빠진 낡은 현판이 서 있습니다.", delay, 5);
            Util.Write("현판에는 '루탄촌(淚歎村)'이라는 글씨가 희미하게 남아, 세월의 흔적을 말해줍니다.", delay, 5);
            Thread.Sleep(500);
            Console.WriteLine();
            Util.Write("허리춤의 낡은 검 한 자루와 해진 옷이 당신의 행색 전부.", delay, 5);
            Util.Write("이제 무엇을 하시겠습니까?", delay, 5);
            Thread.Sleep(500);
            Console.WriteLine();
            Console.WriteLine();
            index_count();
            Util.Write("【촌장과 대화 】", delay - 20, 15);
            index_count();
            Util.Write("【객잔으로 이동 】", delay - 20, 15);
            index_count();
            Util.Write("【숲으로 향한다 】", delay - 20, 15);
            index_count();
            Util.Write("【 상태 보기  】", delay - 20, 15);
            index_count();
            Util.Write("【소지품 확인 】", delay - 20, 15);
            Console.ResetColor();
            monsters.Clear();
        }

        static void MainScene_choice(string choice)
        {
            if (choice == "1")
            {
                QuestScene();
            }
            else if (choice == "2")
            {
                scene = 2;
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
                StatusScene();
            }
            else if (choice == "5")
            {
                InventoryScene();
            }
            else if (choice == "6")
            {
                WhiteSwordMan1();
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
            state = 0;
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
                state = 0;
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


        static void Rest()
        {
            Console.Clear();
            index_c = 0;
            Console.WriteLine();
            Console.WriteLine();
            Util.SetColor(224, 192, 128);
            Util.Write("당신의 눈앞에 허름하지만 정겨워 보이는 객잔이 나타납니다.", delay, 5);
            Thread.Sleep(500);
            Console.WriteLine();
            Util.Write("문틈으로 시끌벅적한 사람들의 목소리와 맛있는 음식 냄새가 흘러나옵니다.", delay, 5);
            Util.Write("당신은 잠시 망설이다 객잔으로 발걸음을 옮깁니다.", delay, 5);
            Thread.Sleep(500);
            Console.WriteLine();
            Util.Write("딸그랑, 소리를 내며 문을 열자 객잔 안의 모든 시선이 당신에게로 쏠립니다.", delay, 5);
            Thread.Sleep(500);
            Console.WriteLine();
            Console.WriteLine();
            index_count();
            Util.Write("【음식을 시킨다 】", delay - 20, 15);
            index_count();
            Util.Write("【주변을 살펴본다 】", delay - 20, 15);
            index_count();
            Util.Write("【객잔에서 나가기 】", delay - 20, 15);
            Console.ResetColor();
            scene = 2;
        }

        static void Rest_choice(string choice)
        {
            if (choice == "1")
            {
                if (player.PlayerHp < 100)
                {
                    player.PlayerHp = 100;
                }
                else
                Console.WriteLine("당신은 휴식이 필요하지 않습니다.");
                Console.WriteLine($"\n현재체력 : {player.PlayerHp}");
            }
            else if (choice == "2")
            {
                WhiteSwordMan1();
                scene = 3;
            }
            else
            {
                MainScene();
                scene = 1;
            }

            


            
        }

        static void WhiteSwordMan1()
        {
            Console.Clear();
            index_c = 0;
            Console.WriteLine();
            Console.WriteLine();
            Util.SetColor(224, 192, 128);
            Util.Write("시선이 쏠린 것도 잠시, 사람들은 다시 자신의 술잔으로 고개를 돌립니다.", delay, 5);
            Util.Write("하지만 그중에서도 유독 한 사람.", delay, 5);
            Thread.Sleep(500);
            Console.WriteLine();
            Util.Write("객잔 구석, 어두운 자리에 홀로 앉은 사내가 있습니다.", delay, 5);
            Util.Write("온통 새하얀 옷차림은 시끌벅적한 이곳과 어울리지 않게 기묘한 분위기를 자아냅니다.", delay, 5);
            Util.Write("그의 옆에는 상아로 만든 듯한 흰 검 한 자루가 조용히 놓여 있습니다.", delay, 5);
            Thread.Sleep(500);
            Console.WriteLine();
            Console.WriteLine();
            index_count();
            Util.Write("【음식 주문하기 】", delay - 20, 15);
            index_count();
            Util.Write("【흰 옷의 사내에게 다가가기 】", delay - 20, 15);
            index_count();
            Util.Write("【객잔에서 나가기 】", delay - 20, 15);
            Console.ResetColor();
            scene = 3;
            delay = 30;
        }

        static void WhiteSwordMan2()
        {
            Console.Clear();
            index_c = 0;
            Console.WriteLine();
            Console.WriteLine();
            Util.SetColor(224, 192, 128);
            Util.Write("온통 흰 옷을 입고 있지만, 풍기는 분위기는 왠지 모르게 서늘합니다.", delay, 5);
            Util.Write("그의 옆에는 백옥처럼 흰 검이 놓여 있습니다.", delay, 5);
            Thread.Sleep(500);
            Util.Write("주변 사람들은 그를 '백검귀(白劍鬼)'라 부르며 수군거립니다.", delay, 5);
            Util.Write("당신이 다가가자, 백검귀는 술잔을 든 채 힐끗 당신을 쳐다봅니다.", delay, 5);
            Thread.Sleep(500);
            Console.WriteLine();
            Util.SetColor(227, 255, 254);
            Util.Write(" \"무슨 볼일이지?\"", delay, 5);
            Util.SetColor(224, 192, 128);
            Thread.Sleep(500);
            Console.WriteLine();
            Util.Write("그의 목소리는 얼음장처럼 차갑게 느껴집니다.", delay, 5);
            Thread.Sleep(500);
            Console.WriteLine();
            Console.WriteLine();
            index_count();
            Util.Write("【조심스럽게 도움을 요청한다 】", delay - 20, 15);
            index_count();
            Util.Write("【말없이 돈으로 거래를 시도한다 】", delay - 20, 15);
            index_count();
            Util.Write("【그의 명성을 언급하며 대화를 시작한다 】", delay - 20, 15);
            index_count();
            Util.Write("【조용히 그의 옆자리에 앉아 술을 따른다 】", delay - 20, 15);
            Console.ResetColor();
            scene = 5;
            delay = 30;
        }

        static void WhiteSwordMan1_choice(string choice)
        {
            if (choice == "1")
            {
                if (player.PlayerHp < 100)
                {
                    player.PlayerHp = 100;
                }
                else
                    Console.WriteLine("당신은 휴식이 필요하지 않습니다.");
                Console.WriteLine($"\n현재체력 : {player.PlayerHp}");
            }
            else if (choice == "2")
            {
                WhiteSwordMan2();
                scene = 5;
            }
            else
            {
                MainScene();
                scene = 1;
            }
        }

        static void WhiteSwordMan2_choice(string choice)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            if (choice == "1")
            {
                Util.SetColor(224, 192, 128);
                Util.Write("백검귀는 당신을 위아래로 훑어보더니, 코웃음을 칩니다.", delay, 5);
                Thread.Sleep(500);
                Console.WriteLine();
                Util.SetColor(227, 255, 254);
                Util.Write(" \"도움? 세상에 공짜는 없는 법. 내 검은 특히 더 비싸지.\"", delay, 5);
                Util.SetColor(224, 192, 128);
                Thread.Sleep(500);
                Console.WriteLine();
                Util.Write("그는 더 이상 당신에게 눈길도 주지 않고 다시 술잔을 기울입니다.", delay, 5);
                Thread.Sleep(500);
                Console.WriteLine();
                Util.SetColor(227, 255, 254);
                Util.Write(" \"돌아가라.\"", delay, 5);
                Util.SetColor(224, 192, 128);
                Thread.Sleep(500);
                Console.WriteLine();
                Util.SetColor(181, 53, 53);
                Util.Write("[백검귀는 당신의 요청을 무시합니다.]", delay-20, 5);
                Util.SetColor(224, 192, 128);
            }
            else if (choice == "2")
            {
                Util.SetColor(224, 192, 128);
                Util.Write("당신이 말없이 은자가 든 주머니를 테이블에 올려놓자, \u001b[38;2;135;206;250m'달그락'\u001b[38;2;224;192;128m 소리와 함께 그의 시선이 아주 잠시 머뭅니다.", delay, 5);
                Util.Write("그는 돈을 세어보지도 않고, 다시 당신을 봅니다. 이전보다 눈빛이 조금 달라져 있습니다.", delay, 5);
                Thread.Sleep(500);
                Console.WriteLine();
                Util.SetColor(227, 255, 254);
                Util.Write(" \"...\"", delay, 5);
                Thread.Sleep(500);
                Util.Write(" \"이 정도로는... 딱 한 번이다.\"", delay, 5);
                Util.SetColor(224, 192, 128);
                Thread.Sleep(500);
                Console.WriteLine();
                Util.Write("그가 나지막이 말합니다.", delay, 5);
                Thread.Sleep(500);
                Console.WriteLine();
                Util.SetColor(227, 255, 254);
                Util.Write(" \"어디서, 누구의 피를 보면 되는가?\"", delay, 5);
                Util.SetColor(224, 192, 128);
                Thread.Sleep(500);
                Console.WriteLine();
                Util.SetColor(255, 207, 105);
                Util.Write("[거래가 성립되었습니다. 백검귀가 다음 1회의 전투에서 당신을 돕습니다.]", delay - 20, 5);
                Util.SetColor(224, 192, 128);
                if (player.PlayerGold >= 100)
                {
                    player.PlayerGold -= 100;
                    hiredMercenary = new Mercenary("백검귀", 15);
                }
                else
                {
                    // 돈부족
                }
            }
            else if (choice == "3")
            {
                Util.SetColor(224, 192, 128);
                Util.Write("백검귀는 미동도 없이 말합니다.", delay, 5);
                Thread.Sleep(500);
                Console.WriteLine();
                Util.SetColor(227, 255, 254);
                Util.Write(" \"내 이름값을 하려면, 그만한 대가가 필요한 법.\"", delay, 5);
                Util.SetColor(224, 192, 128);
                Thread.Sleep(500);
                Console.WriteLine();
                Util.Write("그가 차가운 눈으로 당신을 쏘아봅니다.", delay, 5);
                Thread.Sleep(500);
                Console.WriteLine();
                Util.SetColor(227, 255, 254);
                Util.Write(" \"용건이 있다면 돈과 함께 말하고, 아니라면 술맛 떨어지게 하지 말고 사라져라.\"", delay, 5);
                Util.SetColor(224, 192, 128);
                Thread.Sleep(500);
                Console.WriteLine();
                Util.SetColor(181, 53, 53);
                Util.Write("[백검귀는 흥미를 보이지 않습니다.]", delay - 20, 5);
                Util.SetColor(224, 192, 128);
            }
            else
            {
                Util.SetColor(224, 192, 128);
                Util.Write("당신이 술을 따르려 하자, 백검귀가 손으로 당신의 술병을 막아섭니다.", delay, 5);
                Thread.Sleep(500);
                Console.WriteLine();
                Util.SetColor(227, 255, 254);
                Util.Write(" \"나는 벗을 사귀러 온 것이 아니다.\"", delay, 5);
                Util.SetColor(224, 192, 128);
                Thread.Sleep(500);
                Console.WriteLine();
                Util.Write("그의 목소리에 서릿발 같은 경고가 담겨 있습니다.", delay, 5);
                Thread.Sleep(500);
                Console.WriteLine();
                Util.SetColor(227, 255, 254);
                Util.Write(" \"...꺼져.\"", delay, 5);
                Util.SetColor(224, 192, 128);
                Thread.Sleep(500);
                Console.WriteLine();
                Util.SetColor(181, 53, 53);
                Util.Write("[백검귀가 당신을 노골적으로 적대합니다.]", delay - 20, 5);
                Util.SetColor(224, 192, 128);
            }
            PlayerInput();
            delay = 0;
            WhiteSwordMan1();
            scene = 3;
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
                Console.WriteLine($"MP {player.PlayerMp}");
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

            Console.WriteLine("1. 일반 공격");
            Console.WriteLine("2. 스킬 사용");
            Console.WriteLine("선택 : ");
            string AttackChoice = Console.ReadLine();
            if (AttackChoice == "1")
            {
                NomalAttack(monster, rand);
            }
            else if (AttackChoice == "2")
            {
                SkillAttack(monster);
            }
            else 
            {
                Console.WriteLine("잘못된 입력입니다.");
            }

            Console.WriteLine($"{player.PlayerName} 의 공격!!");
            static void NomalAttack(Monster monster, Random rand)
            { 
              int damage = player.PlayerDamageCalc();
              int avoid = rand.Next(1, 101);
                if (avoid < 11)
                {
                    Console.WriteLine($"Lv.{monster.Level} {monster.Name} 을(를) 공격했지만 아무일도 일어나지 않았습니다.");
                }
                else
                {
                    int critical = rand.Next(1, 101);
                    if (critical < 16)
                    {
                        damage = (int)(damage * 1.6f);
                        Console.WriteLine($"Lv.{monster.Level} {monster.Name} 을(를) 맞췄습니다. [데미지 : {damage}] - 치명타 공격!!");
                        Console.WriteLine();
                        Console.WriteLine($"Lv.{monster.Level} {monster.Name}");
                        Console.Write($"HP {monster.Hp}  -> ");
                        monster.TakeDamage(damage);
                        if (monster.IsDead == true)
                        {
                            player.QuestKillCount++;
                            Console.WriteLine(player.QuestKillCount);
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
            }

            static void SkillAttack(Monster monster)
            {
                Console.WriteLine("사용할 스킬을 선택하세요:");
                for (int i = 0; i < player.Skills.Count; i++)
                {
                    var skill = player.Skills[i];
                    Console.WriteLine($"{i + 1}. {skill.SkillName} (데미지: {skill.SkillDamage}, MP 소모: {skill.SkillMana})");
                }

                Console.Write("선택: ");
                if (int.TryParse(Console.ReadLine(), out int skillIndex) && skillIndex >= 1 && skillIndex <= player.Skills.Count)
                {
                    player.UseSkill(skillIndex - 1, monster);
                }
                else
                {
                    Console.WriteLine("잘못된 스킬 선택입니다.");
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

        static void QuestScene()
        {
            Console.Clear();
            scene = 4;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Quest!!");
            Console.ResetColor();
            Console.WriteLine();
            for (int i = 0; i < quests.Count; i++)
            {
                Console.WriteLine(quests[i].IsAccept ? $"{i + 1}. {quests[i].Title} [수행 중]" : $"{i + 1}. {quests[i].Title}");
            }
            Console.WriteLine();   
            Console.WriteLine($"원하시는 퀘스트를 선택해주세요.");
        }

        static void QuestScene_choice(string choice)
        {
            int input = int.Parse(choice);

            switch(input)
            {
                case 0:
                    MainScene();
                    scene = 1;
                    break;
                default:
                    int questIdx = input - 1;
                    quests[questIdx].ShowQuestUI();
                    break;
            }
        }
          
        static void SetQuest()
        {
            quests.Add(new KillQuest(player));
            quests.Add(new EquipQuest(player));
            quests.Add(new StatusUpQuest(player));
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
