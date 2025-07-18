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
        public static int ev = 1;
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
            FakeTitle();
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
            Console.Write(">> ");
            return Console.ReadLine();
        }

        public static void index_count()
        {
            index_c++;
            Console.SetCursorPosition(12, Console.CursorTop);
            Console.Write($"\u001b[38;2;255;194;89m{index_c}.\u001b[38;2;224;192;128m");
        }

        static void FakeTitle()
        {
            bool fake = true;
            int i = 0;
            int i2 = 30;

            while (fake)
            {
                i += 1;
                Console.WriteLine();
                Console.WriteLine();
                Console.SetCursorPosition(40, Console.CursorTop);
                Console.WriteLine("스파르타 마을애 오신 여러분 환영합니다.");
                Console.WriteLine();
                Console.SetCursorPosition(35, Console.CursorTop);
                Console.WriteLine("이곳애서 던전으로 들어가기전 활동을 할 수 잇습니다.");
                Console.WriteLine();
                Console.SetCursorPosition(52, Console.CursorTop);
                Console.WriteLine("1. 상태 보기");
                Console.SetCursorPosition(52, Console.CursorTop);
                Console.WriteLine("2. 인벤토리");
                Console.SetCursorPosition(52, Console.CursorTop);
                Console.WriteLine("3. 상점");
                if (i >= 5)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(53, Console.CursorTop);
                    Console.WriteLine("4. 텍스트를 벤다.");
                    Console.ResetColor();
                }
                Console.WriteLine();
                Console.SetCursorPosition(43, Console.CursorTop);
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.WriteLine();
                Console.SetCursorPosition(52, Console.CursorTop);
                Console.Write(">> ");
                string choice = Console.ReadLine();
                if (choice == "4" || choice == "벤다")
                {
                    fake = false;
                }
            }

            Console.CursorVisible = false;
            Thread.Sleep(500);

            while ( i2 >= 1 )
            {
                i2 = i2-1;
                Console.SetCursorPosition(20, i2);
                Util.Textcolor("\u001b[38;2;225;25;25m │ ", 0, 56);
                //Console.WriteLine("│");
                Thread.Sleep(0);
            }

            Console.SetCursorPosition(0, 0);


            Thread.Sleep(500);
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();

            Thread.Sleep(50);

            Title();
            LoopChoice();
            Console.CursorVisible = true;

        }

        static void Title()
        {
            Console.CursorVisible = false;
            Thread.Sleep(1);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Thread.Sleep(3000);
            Console.WriteLine();
            Console.WriteLine();
            Util.Textcolor("검으로 \u001b[38;2;225;25;25m텍스트\u001b[38;2;255;255;255m를 베어버렸다.", 25, 5);
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
            Util.Textcolor("실례가 안된다면 귀공의 성함을 알려주십시오.", 25, 5);
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
            Util.Textcolor($"[\u001b[38;2;135;206;250m{playerName}\u001b[38;2;240;248;255m]님이시군요… 강호의 길에 들어선 당신.", 25, 5);
            Console.WriteLine();
            Util.Textcolor("행색을 보아하니... 귀공께선....", 25, 5);
            Console.WriteLine();
            Thread.Sleep(500);
            Util.Textcolor("1.【검호이다 】", 10, 11);
            Util.Textcolor("2.【자객이다 】", 10, 11);
            Util.Textcolor("3.【도인이다 】", 10, 11);
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
            Util.Textcolor($"[\u001b[38;2;135;206;250m{playerName}\u001b[38;2;240;248;255m] 님, 역시 [\u001b[38;2;135;206;250m{jobSelect_str}\u001b[38;2;240;248;255m]이시리라 생각했습니다.", 30, 5);
            Console.WriteLine();
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
            Util.Write("허리춤의 목검 한 자루와 해진 옷이 당신의 행색 전부.", delay, 5);
            Util.Write("이제 무엇을 하시겠습니까?", delay, 5);
            Thread.Sleep(500);
            Console.WriteLine();
            Console.WriteLine();
            index_count();
            Util.Write("【촌장과 대화 】", delay - 20, 15);
            index_count();
            Util.Write("【객잔으로 이동 】", delay - 20, 15);
            index_count();
            Util.Write("【협행을 떠난다 】", delay - 20, 15);
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
                delay = 30;
                QuestScene();
            }
            else if (choice == "2")
            {
                delay = 30;
                Rest();
            }
            else if (choice == "3")
            {
                if (ev >= 2)
                {
                    monsters.Clear();
                    monsters.Add(new Boss());
                    ShowBattleUI();
                }
                else
                {
                    ev++;
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
                    delay = 0;
                    MainScene();
                }
            }
            else if (choice == "4")
            {
                StatusScene();
            }
            else if (choice == "5")
            {
                InventoryScene();
            }
            else
            {
                Util.False_();
                Console.SetCursorPosition(0, Console.CursorTop - 2);
                Console.Write("\x1b[38;2;181;53;53m");
                Util.Write("[!] 잘못된 입력입니다. 다시 시도해주세요.", 20, 5);
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.ResetColor();
            }
        }

        static void StatusScene()
        {
            state = 1;
            Console.Clear();
            player.Playerinfo();
        }

        static void StatusScene_choice(string choice)
        {
            delay = 0;
            state = 0;
            MainScene();
        }

        static void InventoryScene()
        {
            delay = 30;
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
                delay = 0;
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
                    Util.False_();
                    Console.SetCursorPosition(0, Console.CursorTop - 2);
                    Console.Write("\x1b[38;2;25;207;105m");
                    Util.Write("[따뜻한 음식이 들어가자, 기분 좋은 포만감과 함께 온몸이 개운해졌습니다.]", 20, 5);
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.ResetColor();
                }
                else
                {
                    Util.False_();
                    Console.SetCursorPosition(0, Console.CursorTop - 2);
                    Console.Write("\x1b[38;2;181;53;53m");
                    Util.Write("[배가 고프지 않습니다.]", 20, 5);
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.ResetColor();
                }
            }
            else if (choice == "2")
            {
                WhiteSwordMan1();
                scene = 3;
            }
            else
            {
                delay = 0;
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
            Console.WriteLine();
            Thread.Sleep(500);
            Util.Write("주변 사람들은 그를 '백검귀(白劍鬼)'라 부르며 수군거립니다.", delay, 5);
            Console.WriteLine();
            Thread.Sleep(500);
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
                    Util.False_();
                    Console.SetCursorPosition(0, Console.CursorTop - 2);
                    Console.Write("\x1b[38;2;25;207;105m");
                    Util.Write("[따뜻한 음식이 들어가자, 기분 좋은 포만감과 함께 온몸이 개운해졌습니다.]", 20, 5);
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.ResetColor();
                }
                else
                {
                    Util.False_();
                    Console.SetCursorPosition(0, Console.CursorTop - 2);
                    Console.Write("\x1b[38;2;181;53;53m");
                    Util.Write("[배가 고프지 않습니다.]", 20, 5);
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.ResetColor();
                }
            }
            else if (choice == "2")
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine();
                WhiteSwordMan2();
                scene = 5;
            }
            else
            {
                delay = 0;
                MainScene();
                scene = 1;
            }
        }

        static void WhiteSwordMan2_choice(string choice)
        {
            if (choice == "1")
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine();
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
                PlayerInput();
                delay = 0;
                WhiteSwordMan1();
                scene = 3;
            }
            else if (choice == "2")
            {
                
                if (player.PlayerGold >= 1000)
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine();
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
                    player.PlayerGold -= 2000;
                    hiredMercenary = new Mercenary("백검귀", 15);
                    PlayerInput();
                    delay = 0;
                    WhiteSwordMan1();
                    scene = 3;
                }
                else
                {
                    Util.False_();
                    Console.SetCursorPosition(0, Console.CursorTop - 2);
                    Console.Write("\x1b[38;2;181;53;53m");
                    Util.Write("[!] 소지금이 부족합니다.", 20, 5);
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.ResetColor();
                }
            }
            else if (choice == "3")
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine();
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
                PlayerInput();
                delay = 0;
                WhiteSwordMan1();
                scene = 3;
            }
            else
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine();
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
                PlayerInput();
                delay = 0;
                WhiteSwordMan1();
                scene = 3;
            }
            
        }




        static void ShowBattleUI()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(5, Console.CursorTop);
                Console.WriteLine("전투");
                Console.WriteLine();
                Console.WriteLine();
                Console.SetCursorPosition(6, Console.CursorTop);
                Console.WriteLine("\u001b[38;2;225;25;25m【적 상태】\u001b[38;2;255;255;255m");
                Console.WriteLine();
                for (int i = 0; i < monsters.Count; i++)
                {
                    if (monsters[i].IsDead == true)
                    {
                        Console.SetCursorPosition(5, Console.CursorTop);
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        //Console.WriteLine($"{i + 1}. {monsters[i].Level}급 {monsters[i].Name} 0");
                        Console.Write($"{i + 1}. {monsters[i].Level}급 {monsters[i].Name}");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write($"0\n");
                    }
                    else
                    {
                        Console.SetCursorPosition(5, Console.CursorTop);
                        Console.WriteLine($"{i + 1}. {monsters[i].Level}급 {monsters[i].Name} 체력 {monsters[i].Hp}");
                    }
                    Console.ResetColor();
                }
                Console.WriteLine();
                Console.WriteLine();

                // 플레이어 정보 가져오기
                Console.SetCursorPosition(6, Console.CursorTop);
                Console.WriteLine("\u001b[38;2;135;206;250m【내 상태】\u001b[38;2;240;248;255m");
                Console.WriteLine();
                Console.SetCursorPosition(5, Console.CursorTop);
                Console.Write($"{player.PlayerLevel}급 ");
                //Console.SetCursorPosition(5, Console.CursorTop);
                Console.Write($"\u001b[38;2;135;206;250m{player.PlayerName}\u001b[38;2;240;248;255m({player.PlayerJob}) ");
                //Console.SetCursorPosition(5, Console.CursorTop);
                Console.Write($"체력 {player.PlayerHp}");
                //Console.SetCursorPosition(5, Console.CursorTop);
                Console.WriteLine($" / 내력 {player.PlayerMp}");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.SetCursorPosition(5, Console.CursorTop);
                Console.WriteLine("0. 마을로 돌아가기");
                Console.WriteLine();
                Console.SetCursorPosition(5, Console.CursorTop);
                Console.WriteLine("대상을 선택해주세요.");
                Console.WriteLine();
                Console.SetCursorPosition(5, Console.CursorTop);
                Console.Write(": ");

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
                    Console.SetCursorPosition(5, Console.CursorTop);
                    Console.WriteLine("이미 \u001b[38;2;225;25;25m사망\u001b[38;2;255;255;255m했습니다.");
                    Console.SetCursorPosition(5, Console.CursorTop);
                    Console.WriteLine("0. 다시 선택");
                    Console.SetCursorPosition(5, Console.CursorTop);
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
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(6, Console.CursorTop);
            Console.WriteLine("전투 - 공격하세요!");
            Console.ResetColor();
            Console.WriteLine();

            Console.SetCursorPosition(5, Console.CursorTop);
            Console.WriteLine("1. 일반 공격");
            Console.SetCursorPosition(5, Console.CursorTop);
            Console.WriteLine("2. 초식 사용");
            Console.SetCursorPosition(5, Console.CursorTop);
            Console.Write("선택 : ");
            string AttackChoice = Console.ReadLine();
            Console.SetCursorPosition(15, Console.CursorTop);
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

            Console.SetCursorPosition(5, Console.CursorTop);
            //Console.WriteLine($"{player.PlayerName} 의 공격!!");
            static void NomalAttack(Monster monster, Random rand)
            { 
              int damage = player.PlayerDamageCalc();
              int avoid = rand.Next(1, 101);
                if (avoid < 11)
                {
                    Console.SetCursorPosition(5, Console.CursorTop);
                    Console.WriteLine($"{monster.Level}급 {monster.Name}을(를) 공격했지만 아무일도 일어나지 않았습니다.");
                }
                else
                {
                    int critical = rand.Next(1, 101);
                    if (critical < 16)
                    {
                        damage = (int)(damage * 1.6f);
                        Console.SetCursorPosition(5, Console.CursorTop);
                        Console.WriteLine($"{monster.Level}급 {monster.Name}을(를) 맞췄습니다. [피해 : {damage}] - 치명타 공격!!");
                        Console.WriteLine();
                        Console.SetCursorPosition(5, Console.CursorTop);
                        Console.WriteLine($"{monster.Level}급 {monster.Name}");
                        Console.SetCursorPosition(5, Console.CursorTop);
                        Console.Write($"체력 {monster.Hp}  -> ");
                        monster.TakeDamage(damage);
                        Console.SetCursorPosition(5, Console.CursorTop);
                        if (monster.IsDead == true)
                        {
                            player.QuestKillCount++;
                            Console.WriteLine(player.QuestKillCount);
                            Console.WriteLine("-> \u001b[38;2;225;25;25m사망\u001b[38;2;255;255;255m");
                        }
                        else
                        {
                            Console.WriteLine($"-> {monster.Hp}");
                        }
                    }
                    else
                    {
                        Console.SetCursorPosition(5, Console.CursorTop);
                        Console.WriteLine($"{monster.Level}급 {monster.Name}을(를) 맞췄습니다. [피해 : {damage}]");
                        Console.WriteLine();
                        Console.SetCursorPosition(5, Console.CursorTop);
                        Console.WriteLine($"{monster.Level}급 {monster.Name}");
                        Console.SetCursorPosition(5, Console.CursorTop);
                        Console.Write($"체력 {monster.Hp}  -> ");
                        monster.TakeDamage(damage);
                        if (monster.IsDead == true)
                        {
                            Console.WriteLine("\u001b[38;2;225;25;25m사망\u001b[38;2;255;255;255m");
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
                Console.WriteLine();
                Console.WriteLine();
                Console.SetCursorPosition(6, Console.CursorTop);
                Console.WriteLine("사용할 초식을 선택하세요:");
                for (int i = 0; i < player.Skills.Count; i++)
                {
                    Console.SetCursorPosition(5, Console.CursorTop);
                    var skill = player.Skills[i];
                    Console.WriteLine($"{i + 1}. {skill.SkillName} (피해: {skill.SkillDamage}, 내력 소모: {skill.SkillMana})");
                }

                Console.SetCursorPosition(5, Console.CursorTop);
                Console.Write("선택 : ");
                Console.SetCursorPosition(12, Console.CursorTop);
                if (int.TryParse(Console.ReadLine(), out int skillIndex) && skillIndex >= 1 && skillIndex <= player.Skills.Count)
                {
                    player.UseSkill(skillIndex - 1, monster);
                }
                else
                {
                    Console.WriteLine("잘못된 초식 선택입니다.");
                }
            }
            if (hiredMercenary != null)
            {
                Console.SetCursorPosition(5, Console.CursorTop);
                Monster target = monsters.FirstOrDefault(m => !m.IsDead);
                if (target != null)
                {
                    hiredMercenary.Attack(target);
                }
            }

            Console.WriteLine();

            Console.SetCursorPosition(5, Console.CursorTop);
            Console.WriteLine("X. 다음");
            Console.WriteLine();
            Console.Write(": ");

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
                Console.WriteLine();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(6, Console.CursorTop);
                Console.WriteLine("전투 - \u001b[38;2;225;25;25m적\u001b[38;2;255;255;255m들이 공격합니다.");
                Console.ResetColor();
                Console.WriteLine();

                for (int i = 0; i < monsters.Count; i++)
                {
                    if (monsters[i].IsDead == false)
                    {
                        Console.SetCursorPosition(5, Console.CursorTop);
                        Console.WriteLine($"{monsters[i].Level}급 {monsters[i].Name} 의 공격!!");
                        Console.WriteLine();
                        Console.SetCursorPosition(5, Console.CursorTop);
                        Console.WriteLine($"\u001b[38;2;135;206;250m{player.PlayerName}\u001b[38;2;240;248;255m을(를) 맞췄습니다. (피해 : {monsters[i].DamageCalc()})");
                        Console.WriteLine();
                        Console.SetCursorPosition(5, Console.CursorTop);
                        Console.Write(i == 0 ? $"내 남은 체력 {playerEnterHp} -> " : $"내 남은 체력 {player.PlayerHp} -> ");
                        monsters[i].ApplyDamage(player);
                        Console.WriteLine(player.PlayerHp <= 0 ? "\u001b[38;2;225;25;25m사망\u001b[38;2;255;255;255m" : $"{player.PlayerHp}");
                        Console.WriteLine();
                    }
                }
                Console.WriteLine();
                Console.SetCursorPosition(5, Console.CursorTop);
                Console.WriteLine("X. 다음");
                Console.WriteLine();
                Console.SetCursorPosition(5, Console.CursorTop);
                
                Console.Write(": ");

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
            Console.WriteLine();
            Console.WriteLine();
            //Console.SetCursorPosition(6, Console.CursorTop);
            // 플레이어 체력 0일 경우 패베 출력
            if (player.PlayerHp <= 0)
            {
                for (int i = 0; i < 10; i++)
                { 
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("패배했습니다.   패배했습니다.   패배했습니다.   패배했습니다.   패배했습니다.   패배했습니다.   패배했습니다.   패배했습니다.   패배했습니다.");
                }
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine();
                Console.SetCursorPosition(5, Console.CursorTop);
                Console.WriteLine($"급수.{player.PlayerLevel}급 {player.PlayerName}({player.PlayerJob})");
                Console.SetCursorPosition(5, Console.CursorTop);
                Console.WriteLine($"체력 {playerEnterHp} -> 0 (\u001b[38;2;225;25;25m사망\u001b[38;2;255;255;255m)");
                Console.SetCursorPosition(5, Console.CursorTop);
                Console.WriteLine("여행 종료...\n");
                Console.SetCursorPosition(5, Console.CursorTop);
                Console.Write(": ");
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
                //Console.WriteLine();
                //Console.WriteLine();
                Console.SetCursorPosition(5, Console.CursorTop);
                int totalExp = 0;
                int totalGold = 0;
                List<Item> droppedItems = new List<Item>();
                int MaxExp = player.GetExpToNextLevel();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition(5, Console.CursorTop);
                Console.WriteLine("승리했습니다!!\n");
                Console.ResetColor();
                Console.SetCursorPosition(5, Console.CursorTop);
                Console.WriteLine($"총 적들 {monsters.Count}과 무위를 겨루고 \u001b[38;2;135;206;250m승리\u001b[38;2;240;248;255m했습니다.\n");
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
                        Console.SetCursorPosition(5, Console.CursorTop);
                        Console.WriteLine($"도구 : \u001b[38;2;135;206;250m{droppedItem.Name}\u001b[38;2;240;248;255m을(를) 획득했습니다.\n");
                    }
                }
                player.GainExp(totalExp);
                Console.WriteLine();
                Console.SetCursorPosition(5, Console.CursorTop);
                Console.WriteLine($"【내 상태】");
                Console.SetCursorPosition(5, Console.CursorTop);
                Console.WriteLine($"\u001b[38;2;135;206;250m{player.PlayerName}\u001b[38;2;240;248;255m({player.PlayerJob})");
                Console.SetCursorPosition(5, Console.CursorTop);
                Console.WriteLine($"급수.{player.PlayerLevel}급");
                Console.SetCursorPosition(5, Console.CursorTop);
                Console.WriteLine($"체력 {playerEnterHp} -> {player.PlayerHp}\n");
                Console.SetCursorPosition(5, Console.CursorTop);
                Console.WriteLine("0. 다음\n");
                Console.SetCursorPosition(5, Console.CursorTop);
                Console.Write(">> ");
                Console.ReadLine();
            }
            hiredMercenary = null;
        }

        static void QuestScene()
        {
            Console.Clear();
            scene = 4;
            Console.WriteLine();
            Console.WriteLine();
            Console.SetCursorPosition(6, Console.CursorTop);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("");
            Console.ResetColor();
            Console.WriteLine();
            for (int i = 0; i < quests.Count; i++)
            {
                Console.SetCursorPosition(5, Console.CursorTop);
                Console.WriteLine(quests[i].IsAccept ? $"{i + 1}. {quests[i].Title} [수행 중]" : $"{i + 1}. {quests[i].Title}");
            }
            Console.WriteLine();   
            Console.SetCursorPosition(5, Console.CursorTop);
            Console.WriteLine($"원하시는 퀘스트를 선택해주세요.");
        }

        static void QuestScene_choice(string choice)
        {
            int input = int.Parse(choice);

            switch(input)
            {
                case 0:
                    delay = 0;
                    MainScene();
                    scene = 1;
                    break;
                case 1:
                case 2:
                case 3:
                    if(input >0 && input < quests.Count + 1)
                    {
                        int questIdx = input - 1;
                        quests[questIdx].ShowQuestUI();
                    }
                    break;
                default:
                    { Console.WriteLine("잘못된 입력이오."); }
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
