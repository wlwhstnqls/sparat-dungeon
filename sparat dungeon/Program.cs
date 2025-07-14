using System;
using System.Reflection;
using System.Threading;
namespace sparat_dungeon

{
    public class Program
    {

        static void Main(string[] args)
        {
            
            Player player = new Player(1, "김용사", "용사", 10, 5, 100, 0);
            Battle.BattleSystem battleSystem = new Battle.BattleSystem();

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
            while (true)
            {
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
                    if (inventoryout == "1")
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
                    battleSystem.StartBattle();
                    //전투 기능을 여기에 추가
                }
                else if (input == "4")
                {
                    Console.Clear();
                    Console.WriteLine("퀘스트 수락.");
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                    Console.Clear();
                }
            }
        }
    }
}
