namespace sparat_dungeon
{
    internal class Program
    {
        static void Main(string[] args)
        {
          while (true)
            {
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n이제 전투를 시작할 수 있습니다.");
                Console.WriteLine("");
                Console.WriteLine("1. 상태 보기\n2. 전투 시작");
                Console.WriteLine("");
                Console.WriteLine("원하는 행동을 입력해주세요.");

                string input = Console.ReadLine();

                if (input == "1")
                {
                    Console.WriteLine("상태보기\n캐릭터의 정보가 표시됩니다.");
                    //플레이어 상태를 여기에 추가
                    break;
                }
                else if (input == "2")
                {
                    Console.WriteLine("Battle!!");
                    // 전투 시작 로직을 여기에 추가
                   break;
                }
                else
                {
                    Console.WriteLine("잘못된 입력입니다. 다시 시도해주세요.");
                }
            }
        }
    }
}
