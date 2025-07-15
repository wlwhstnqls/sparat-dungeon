using System;
using System.Collections;
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
        public int PlayerHp { set; get; }
        public int PlayerGold { get; private set; }
        public int PlayerExp {get;}

        


        public static int PlayerExtraAtk { get; set; }
        public static int PlayerExtraDef { get; set; }
        public int TotalAtk => PlayerAtk + PlayerExtraAtk;
        public int TotalDef => PlayerDef + PlayerExtraDef;


        public static Item EquipWepon { get; set; }
        public static Item EquipArmor { get; set; }

        public Random random = new Random();

        public Player(string playerName, string jobSelect)
        {
            

            if (jobSelect == "1" || jobSelect == "전사")
            {
                PlayerLevel = 1;
                PlayerName = playerName;
                PlayerJob = "전사";
                PlayerHp = 100;
                PlayerAtk = 10;
                PlayerDef = 5;
                PlayerGold = 1500;
                
            }
            else if (jobSelect == "2" || jobSelect == "도적")
            {
                PlayerLevel = 1;
                PlayerName = playerName;
                PlayerJob = "도적";
                PlayerHp = 80;
                PlayerAtk = 8;
                PlayerDef = 3;
                PlayerGold = 1500;
                
            }
        }

        public void Playerinfo()
        {
            Console.WriteLine($"레벨: {PlayerLevel}");
            Console.WriteLine($"이름: {PlayerName}");
            Console.WriteLine($"직업: {PlayerJob}");
            Console.WriteLine(PlayerExtraAtk > 0 ? $"공격력: {PlayerAtk} +{PlayerExtraAtk}" : $"공격력: {PlayerAtk}");
            Console.WriteLine(PlayerExtraDef > 0 ? $"방어력: {PlayerDef} +{PlayerExtraDef}" : $"방어력: {PlayerDef}");
            Console.WriteLine($"체력: {PlayerHp}");
            Console.WriteLine($"골드: {PlayerGold} Gold");
           
        }

        public static void EquipItem(Item item)
        {

            if (item.Type != ItemType.장비)
            {
                Console.WriteLine("장비 아이템이 아닙니다.");
                return;
            }

            if (item.Slot == SlotType.무기)
            {
                if (EquipWepon == null)
                {
                    EquipWepon = item;
                    PlayerExtraAtk = PlayerExtraAtk + EquipWepon.DMG;
                }
                else
                {
                    PlayerExtraAtk = PlayerExtraAtk - EquipWepon.DMG;
                    EquipWepon = item;
                    PlayerExtraAtk = PlayerExtraAtk + EquipWepon.DMG;
                }


            }
            else if (item.Slot == SlotType.방어구)
            {
                if (EquipArmor == null)
                {
                    EquipArmor = item;
                    PlayerExtraDef = PlayerExtraDef + EquipArmor.DF;
                }
                else
                {
                    PlayerExtraDef = PlayerExtraDef - EquipArmor.DF;
                    EquipArmor = item;
                    PlayerExtraDef = PlayerExtraDef + EquipArmor.DF;
                }
            }

        }

        public static void ShowInventoryE()
        {
            string str;
            int index = 0;

            Console.Clear();
            Console.SetCursorPosition(6, 2);
            Console.WriteLine("\u001b[38;2;135;206;250m[장착 가능 장비]\u001b[38;2;240;248;255m");
            Console.WriteLine("");
            for (int i = 0; i < Item.inventory.Length; i++)
            {
                int idx = Item.inventory[i];

                if (idx > 0)
                {
                    Item item = Item.items[idx - 1];

                    if (ItemType.장비 == item.Type)
                    {
                        Item.inventoryE[index] = Item.inventory[i];
                        index += 1;
                        if (EquipWepon == item || EquipArmor == item)
                        {
                            str = $"\u001b[\u001b[38;2;65;105;225m[E]\u001b[38;2;240;248;255m {item.Name} ";
                        }
                        else
                        {
                            str = $"\u001b[38;2;135;206;250m[{index}]\u001b[38;2;240;248;255m {item.Name} ";
                        }

                        if (item.DMG > 0)
                        {
                            str = str + $"{Item.FL} 공격력 +{item.DMG} ";
                        }
                        if (item.DF > 0)
                        {
                            str = str + $"{Item.FL} 방어력 +{item.DF} ";
                        }
                        if (item.HP > 0)
                        {
                            str = str + $"{Item.FL} 최대 체력 +{item.HP} ";
                        }

                        str = str + $"{Item.FL} {item.Ex}";

                        Console.SetCursorPosition(5, Console.CursorTop);
                        Console.WriteLine(str);
                    }
                }


            }

            if (index == 0)
            {
                Item.ShowInventory();
                Console.Write("\x1b[38;2;181;53;53m");
                Console.Write("[!] 장착 가능한 장비가 없습니다.");
            }
            else
            {
                Console.WriteLine("");
                Console.SetCursorPosition(6, Console.CursorTop);
                Console.Write($"\x1b[38;2;135;206;250m0.\x1b[38;2;240;248;255m");
                Console.Write("뒤로가기");
                Console.WriteLine("", 30, 10);
                Item.Inven_eq(Console.ReadLine(), index);
            }


        }

        public int PlayerDamageCalc()
        {
            int damage = random.Next (PlayerAtk - (int)Math.Ceiling((PlayerAtk * 0.1)), (int)Math.Ceiling(PlayerAtk + (PlayerAtk * 0.1)));

            return damage;
        }
    }
}
