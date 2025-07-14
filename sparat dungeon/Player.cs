using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sparat_dungeon
{
    internal class Player
    {
        public int PlayerLevel { get; }
        public string PlayerName { get; }
        public string PlayerJob { get; }
        public int PlayerAtk { get; }
        public int PlayerDef { get; }
        public int PlayerHp { get; }
        public int PlayerGold { get; private set; }


        public int PlayerExtraAtk { get; private set; }
        public int PlayerExtraDef { get; private set; }

        //private List<Item> Inventory = new List<Item>();
        //private List<Item> EquipLIst = new List<Item>();

        //public int InventoryCount
        //{
        //    get
        //    {
        //        return Inventory.Count;
        //    }
        //}

        public Player(int playerLevel, string playerName, string playerJob, int playerAtk, int playerDef, int playerHp,int playerGold )
        {
            PlayerLevel = playerLevel;
            PlayerName = playerName;
            PlayerJob = playerJob;
            PlayerAtk = playerAtk;
            PlayerDef = playerDef;
            PlayerHp = playerHp;
            PlayerGold = playerGold;
        }
        public void Playerinfo()
        {
            Console.WriteLine($"레벨: {PlayerLevel}");
            Console.WriteLine($"이름: {PlayerName}");
            Console.WriteLine($"직업: {PlayerJob}");
            Console.WriteLine($"공격력: {PlayerAtk}");
            Console.WriteLine($"방어력: {PlayerDef}");
            Console.WriteLine($"체력: {PlayerHp}");
            Console.WriteLine($"골드: {PlayerGold} Gold");
        }
    }
}
