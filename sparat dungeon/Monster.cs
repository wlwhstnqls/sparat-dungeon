using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sparat_dungeon
{
    public class Monster
    {
        public int Level { get; set; }
        public string Name { get; set; }
        public int Hp { get; set; }
        public int Atk { get; set; }
        public bool IsDead { get; set; }

        public Random random = new Random();

        public int DamageCalc()
        {
            int damage = random.Next(Atk - (int)Math.Ceiling((Atk * 0.1)), (int)Math.Ceiling(Atk + (Atk * 0.1)));

            return damage;
        }

        public void ApplyDamage(Player player)
        {
            player.PlayerHp -= DamageCalc();
        }

        public void TakeDamage(int damage)
        {
            if (Hp >= 0)
            {
                Hp -= damage;
            }
            OnDie();
        }

        public void OnDie()
        {
            if (Hp <= 0)
            {
                IsDead = true;
            }
        }
        public int MonsterExp()
        {
            return Level*2;
        }

        public int DropGold()
        {
            Random random = new Random();
            return random.Next(10, 50);
        }

        public int DropItem()
        {
            Random random = new Random();
            int dropChance = random.Next(1, 101);
            if (dropChance <= 100)
            {
                return random.Next(0, Item.items.Count);
            }
            return -1; 
        }

    }
    public class Minion : Monster
    {
        public Minion()
        {
            Level = 2;
            Name = "\u001b[38;2;225;25;25m도적\u001b[38;2;255;255;255m";
            Hp = 15;
            Atk = 5;
            IsDead = false;
        }
    }
    public class VoidBug : Monster
    {
        public VoidBug()
        {
            Level = 3;
            Name = "\u001b[38;2;225;25;25m녹림채 잔당\u001b[38;2;255;255;255m";
            Hp = 10;
            Atk = 9;
            IsDead = false;
        }
    }
    public class CanonMinion : Monster
    {
        public CanonMinion()
        {
            Level = 5;
            Name = "\u001b[38;2;225;25;25m살인광 낭인\u001b[38;2;255;255;255m";
            Hp = 25;
            Atk = 8;
            IsDead = false;
        }
    }
    public class Boss : Monster
    {
        public Boss()
        {
            Level = 10;
            Name = "\u001b[38;2;225;25;25m녹림의 왕 마광팔\u001b[38;2;255;255;255m";
            Hp = 2000;
            Atk = 50;
        }
    }


   


}
