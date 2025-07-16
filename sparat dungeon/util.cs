using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sparat_dungeon
{
    internal class util
    {

        public static int map = 0;
        public static int map_r = 0;
        public static Action mapFunc;

        public enum State
        {
            메인,
            상태창,
            인벤토리,
            장착,
            소비
        }


        public static class Textd
        {
            public static void Write(string text, int delay, int index)
            {
                Console.SetCursorPosition(index, Console.CursorTop);
                if (delay < 0) // 딜레이가 0보다 작으면 바로 출력되게 0으로 조정
                {
                    delay = 0;
                }
                foreach (char c in text) // 순회문 루프문이랑 비슷함 text안에 텍스트를 하나씩 꺼내와서 c에 추가함
                {
                    Console.Write(c); // 출력 텍스트
                    Thread.Sleep(delay); // 딜레이
                }
                Console.WriteLine();
            }
        }

        public static void Set(int r, int g, int b)
        {
            Console.Write($"\x1b[38;2;{r};{g};{b}m");
        }

        public static void Reset()
        {
            Console.Write("\x1b[0m");
        }

    }
}
