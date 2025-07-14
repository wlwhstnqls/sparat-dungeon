using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sparat_dungeon
{
    internal class Monster
    {
        public int MonsterLevel { get; }
        public string MonsterName { get; }
        public int MonsterAtk { get; }
        public int MonsterHp { get; }


        public Monster(int monsterLevel, string monsterName, int monsterAtk, int monsterHp )
        {
            MonsterLevel = monsterLevel;
            MonsterName = monsterName;
            MonsterAtk = monsterAtk;
            MonsterHp = monsterHp;
        }
    }
}
