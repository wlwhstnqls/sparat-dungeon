using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
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
        public static string FL = "\u001b[38;2;135;206;250m|\u001b[38;2;240;248;255m";
        public string Name { get; set; }
        public string Ex { get; set; }
        public int DMG { get; set; }
        public int DF { get; set; }
        public int HP { get; set; }
        public int Gold { get; set; }
        public ItemType Type { get; set; }
        public SlotType Slot { get; set; }
        public static Item EquipWepon { get; set; }
        public static Item EquipArmor { get; set; }

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

        public static List<Item> items = new List<Item>
        {
            new Item("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", 0, 5, 0, 1000, ItemType.장비, SlotType.방어구),
            new Item("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 9, 0, 2000, ItemType.장비, SlotType.방어구),
            new Item("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 0, 15, 0, 3500, ItemType.장비, SlotType.방어구),
            new Item("낡은 검", "쉽게 볼 수 있는 낡은 검 입니다.", 2, 0, 0, 600, ItemType.장비, SlotType.무기),
            new Item("청동 도끼", "어디선가 사용됐던거 같은 도끼입니다.", 5, 0, 0, 1500, ItemType.장비, SlotType.무기),
            new Item("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 7, 0, 0, 2500, ItemType.장비, SlotType.무기),
        };

        //인벤 아이템 배열
        public static int[] inventory = new int[10]
        {
            1, 2, 3, 4, 5, 6, 0, 0, 0, 0
        };

        public static int[] inventoryE = new int[10];
        public static void AddItem(int itemIndex)
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == 0)
                {
                    inventory[i] = itemIndex + 1;
                    return;
                }
            }
            Console.WriteLine("소지품이 가득 찼습니다.");
        }


        public static void ShowInventory()
        {
            string str;

            Console.Clear();
            Console.SetCursorPosition(6, 2);
            Console.WriteLine("\u001b[38;2;135;206;250m[소지품 목록]\u001b[38;2;240;248;255m");
            Console.WriteLine("");
            for (int i = 0; i < inventory.Length; i++)
            {
                int idx = inventory[i];
                if (idx == 0)
                {
                    Console.SetCursorPosition(5, Console.CursorTop);
                    Console.WriteLine($"\u001b[38;2;135;206;250m-\u001b[38;2;240;248;255m 빈 슬롯 ");
                }
                else
                {
                    Item item = items[idx - 1];

                    if (EquipWepon == item || EquipArmor == item)
                    {
                        str = $"\u001b[\u001b[38;2;65;105;225m[E]\u001b[38;2;240;248;255m {item.Name} ";
                    }
                    else
                    {
                        str = $"\u001b[38;2;135;206;250m-\u001b[38;2;240;248;255m {item.Name} ";
                    }

                    if (item.DMG > 0)
                    {
                        str = str + $"{FL} 공격력 +{item.DMG} ";
                    }
                    if (item.DF > 0)
                    {
                        str = str + $"{FL} 방어력 +{item.DF} ";
                    }
                    if (item.HP > 0)
                    {
                        str = str + $"{FL} 최대 체력 +{item.DMG} ";
                    }

                    str = str + $"{FL} {item.Ex}";

                    Console.SetCursorPosition(5, Console.CursorTop);
                    Console.WriteLine(str);
                }
            }
            Console.WriteLine("");
            Console.SetCursorPosition(6, Console.CursorTop);
            Console.WriteLine($"\x1b[38;2;135;206;250m1.\x1b[38;2;240;248;255m  장비 장착");
            Console.SetCursorPosition(6, Console.CursorTop);
            Console.WriteLine($"\x1b[38;2;135;206;250m2.\x1b[38;2;240;248;255m  아이템 사용");
            Console.SetCursorPosition(6, Console.CursorTop);
            Console.WriteLine($"\x1b[38;2;135;206;250m0.\x1b[38;2;240;248;255m  돌아가기");
        }


        public static void Inven_eq(string choice, int index)
        {
            int number;

            if (int.TryParse(choice, out number))
            {
                if (number > 0 && number <= index)
                {
                    int itemIndex = inventoryE[number - 1] - 1;

                    if (itemIndex >= 0 && itemIndex < items.Count)
                    {
                        Player.EquipItem(items[itemIndex]);

                        Console.Write(new string(' ', Console.WindowWidth));
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.Write("\u001b[38;2;255;200;82m");
                        Console.Write($"[장착 완료] {items[itemIndex].Name}을(를) 장착했습니다!");

                        Thread.Sleep(1000);
                    }
                    else
                    {
                        Console.WriteLine("잘못된 아이템입니다.");
                        Item.Inven_eq(Console.ReadLine(), index);
                    }
                }
                else
                {
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write("\u001b[38;2;181;53;53m");
                    Console.Write("[!] 잘못된 번호입니다.");
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Item.Inven_eq(Console.ReadLine(), index);
                }
            }
            else
            {
            }
        }



    }






}
