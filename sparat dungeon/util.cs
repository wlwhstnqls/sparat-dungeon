using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sparat_dungeon
{
    internal class util
    {



        public static class Util
        {
            public static void Write(string text, int delay, int index)
            {
                Console.SetCursorPosition(index, Console.CursorTop);
                if (delay < 0) 
                {
                    delay = 0;
                }
                foreach (char c in text) 
                {
                    Console.Write(c); 
                    if (Program.debug == 0)
                    {
                        Thread.Sleep(delay);
                    }
                }
                Console.WriteLine();
            }

            public static void Textcolor(string text, int delay, int index)
            {
                Console.SetCursorPosition(index, Console.CursorTop);

                int colori = 50;
                int top = Console.CursorTop;
                for (int i = 0; i < colori; i++)
                {
                    int bright = 20 + (int)((235.0 / (colori - 1)) * i);
                    string color = $"\u001b[38;2;{bright};{bright};{bright}m";
                    Console.SetCursorPosition(index, top);
                    Console.Write(color + text + "\u001b[0m");
                    if (Program.debug == 0) 
                    {
                        Thread.Sleep(delay);
                    }
                }
                Console.WriteLine();
            }

            public static void False_()
            {
                Console.SetCursorPosition(5, Console.CursorTop);
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, Console.CursorTop - 1);
            }

            public static void SetColor(int r, int g, int b)
            {
                Console.Write($"\x1b[38;2;{r};{g};{b}m");
            }

        }


    }
}
