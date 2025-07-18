using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sparat_dungeon
{
    internal class Mercenary
    {
        public string Name { get; set; }
        public int Damage { get; set; }
        public bool IsDead { get; set; }

        public Mercenary(string name, int damage)
        {
            Name = name;
            Damage = damage;
            IsDead = false;
        }

        public void Attack(Monster monster)
        {
            Console.SetCursorPosition(5, Console.CursorTop);
            Console.WriteLine($"용병 \u001b[38;2;135;206;250m{Name}\u001b[38;2;240;248;255m 이(가) Lv.{monster.Level} {monster.Name} 을(를) 공격합니다! (데미지 : {Damage})");
            monster.TakeDamage(Damage);
            if (monster.IsDead)
            {
                Console.SetCursorPosition(5, Console.CursorTop);
                Console.WriteLine($"Lv.{monster.Level} {monster.Name} 이(가) 쓰러졌습니다!");
            }
        }
    }

}
