using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sparat_dungeon
{
    public class Battle
    {
        public class Monster
        {
            public int Level;
            public string Name;
            public int HP;

            public Monster(int level, string name, int hp)
            {
                Level = level;
                Name = name;
                HP = hp;
            }
            public override string ToString()
            {
                return $"LV.:{Level} {Name} {HP}";
            }
        }
        public class BattleSystem
        {
            List<Monster> _enemies = new List<Monster>();
            Random _ranmdoms = new Random();

            public void StartBattle()
            {
                Console.WriteLine("Battle!!\n");

                GenerateMonsters();

                foreach (var monster in _enemies)
                {
                    Console.WriteLine(monster);
                }

                PrintPlayerInfo();

                Console.WriteLine("1. 공격");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">> ");
            }
            public void GenerateMonsters()
            {
                int monstercount = _ranmdoms.Next(1, 4);

                List<Monster> _monsterkind = new List<Monster>();//Lv.2 미니언  HP 15 // Lv.5 대포미니언 HP 25 // LV.3 공허충 HP 10
                {
                    new Monster(2, "미니언", 15);
                    new Monster(5, "대포미니언", 25);
                    new Monster(3, "공허충", 10);
                }
            }
            public void PrintPlayerInfo()
            {
                Console.WriteLine("\n[내정보]");
                Console.WriteLine("LV.1,[Chad](전사)");
                Console.WriteLine("HP 100/100");
            }
        }
    }
}
