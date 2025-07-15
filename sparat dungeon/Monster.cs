using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sparat_dungeon
{
    internal class Monster
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
            return Level;
        }

    }
    internal class Minion : Monster
    {
        public Minion()
        {
            Level = 2;
            Name = "미니언";
            Hp = 15;
            Atk = 5;
            IsDead = false;
        }
    }
    internal class VoidBug : Monster
    {
        public VoidBug()
        {
            Level = 3;
            Name = "공허충";
            Hp = 10;
            Atk = 9;
            IsDead = false;
        }
    }
    internal class CanonMinion : Monster
    {
        public CanonMinion()
        {
            Level = 5;
            Name = "대포미니언";
            Hp = 25;
            Atk = 8;
            IsDead = false;
        }

       
    }


}
