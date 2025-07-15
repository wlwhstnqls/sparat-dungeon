using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace sparat_dungeon
{
    public class GameData
    {
        public PlayerSaveData Player { get; set; }
        public List<InventorySaveData> Inventory { get; set; } 
        public ItemSaveData EquippedWeapon { get; set; }
        public ItemSaveData EquippedArmor { get; set; }
    }

    public class PlayerSaveData
    {
        public int PlayerLevel { get; set; }
        public string PlayerName { get; set; }
        public string PlayerJob { get; set; }
        public int PlayerAtk { get; set; }
        public int PlayerDef { get; set; }
        public int PlayerHp { get; set; }
        public int PlayerGold { get; set; }
        public int PlayerExp { get; set; }
    }

    public class InventorySaveData
    {
        public string ItemName { get; set; }
        public int Quantity { get; set; }
    }

    public class ItemSaveData
    {
        public string ItemName { get; set; }
    }

    public class DataManager
    {
       
    }


}
