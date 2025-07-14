using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sparat_dungeon
{
    public enum ItemType
    {
        기타,
        장비,
        소비
    }

    public enum SlotType
    {
        없음,
        무기,
        방어구
    }


    public class Item
    {
        public string Name { get; set; }
        public string Ex { get; set; }
        public int DMG { get; set; }
        public int DF { get; set; }
        public int HP { get; set; }
        public int Gold { get; set; }
        public ItemType Type { get; set; }
        public SlotType Slot { get; set; }

        public Item(string name, string ex, int dmg, int df, int hp, int gold, ItemType type, SlotType slot)
        {
            Name = name;
            Ex = ex;
            DMG = dmg;
            DF = df;
            HP = hp;
            Gold = gold;
            Type = type;
            Slot = slot;
        }

    }




}
