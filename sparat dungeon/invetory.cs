using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static sparat_dungeon.util;

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
            new Item("헤진 옷", "세월과 싸움이 스민 너덜너덜한 무복.", 0, 5, 0, 1000, ItemType.장비, SlotType.방어구),
            new Item("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 9, 0, 2000, ItemType.장비, SlotType.방어구),
            new Item("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 0, 15, 0, 3500, ItemType.장비, SlotType.방어구),
            new Item("낡은 검", "녹슬었지만 아직 날을 잃지 않은 오래된 검.", 2, 0, 0, 600, ItemType.장비, SlotType.무기),
            new Item("청동 도끼", "어디선가 사용됐던거 같은 도끼입니다.", 5, 0, 0, 1500, ItemType.장비, SlotType.무기),
            new Item("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 7, 0, 0, 2500, ItemType.장비, SlotType.무기),
            new Item("약초", "소량의 체력을 회복시켜줍니다.", 0, 0, 0, 100, ItemType.소비, SlotType.없음),
        };

        public class Inventory
        {
            public Item Item { get; set; }
            public int Quantity { get; set; }

            public Inventory(Item item, int quantity)
            {
                Item = item;
                Quantity = quantity;
            }
        }

        public static Dictionary<int, Inventory> inventory = new Dictionary<int, Inventory>
        {
            { 0, new Inventory(items[0], 1) },
            { 1, new Inventory(items[3], 1) },
            { 2, new Inventory(items[6], 3) },
        };

        public const int MaxInventory = 10;


        //인벤 아이템 배열
        //public static int[] inventory = new int[10]
        //{
        //    1, 2, 3, 4, 5, 6, 0, 0, 0, 0
        //};

        public static int[] inventoryE = new int[MaxInventory];

        public static void AddItem(int itemIndex)
        {
            Item itemToAdd = items[itemIndex];

            if (itemToAdd.Type != ItemType.장비)
            {
                foreach (KeyValuePair<int, Inventory> entry in inventory)
                {
                    if (entry.Value.Item == itemToAdd)
                    {
                        entry.Value.Quantity++;
                        return;
                    }
                }
            }


            for (int i = 0; i < MaxInventory; i++)
            {
                if (!inventory.ContainsKey(i))
                {
                    inventory[i] = new Inventory(itemToAdd, 1);
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
            for (int i = 0; i < MaxInventory; i++)
            {
                if (!inventory.ContainsKey(i))
                {
                    Console.SetCursorPosition(5, Console.CursorTop);
                    Console.WriteLine($"\u001b[38;2;135;206;250m-\u001b[38;2;240;248;255m 빈 슬롯 ");
                }
                else
                {
                    Inventory slot = inventory[i];
                    Item item = slot.Item;
                    int qty = slot.Quantity;

                    if (Player.EquipWepon == item || Player.EquipArmor == item)
                    {
                        str = $"\u001b[\u001b[38;2;65;105;225m[E]\u001b[38;2;240;248;255m {item.Name} ";
                    }
                    else
                    {
                        str = $"\u001b[38;2;135;206;250m-\u001b[38;2;240;248;255m {item.Name} ";
                    }

                    Console.SetCursorPosition(5, Console.CursorTop);
                    Console.Write(str);

                    if (item.DMG > 0)
                    {
                        str = $"{FL} 공격력 +{item.DMG} ";
                    }
                    if (item.DF > 0)
                    {
                        str = $"{FL} 방어력 +{item.DF} ";
                    }
                    if (item.HP > 0)
                    {
                        str = $"{FL} 최대 체력 +{item.DMG} ";
                    }
                    if (item.Name == "약초")
                    {
                        str = $"{FL} 체력회복 +30 ";
                    }

                    Console.SetCursorPosition(25, Console.CursorTop);
                    Console.Write(str);

                    str = $"{FL} {item.Ex}";

                    Console.SetCursorPosition(42, Console.CursorTop);
                    Console.Write(str);

                    if (item.Type == ItemType.소비)
                    {
                        Console.SetCursorPosition(102, Console.CursorTop);
                        Console.Write($"{FL} {slot.Quantity}개");
                    }
                    else
                    {
                        Console.SetCursorPosition(102, Console.CursorTop);
                        Console.Write($"{FL}");
                    }
                    Console.SetCursorPosition(112, Console.CursorTop);
                    Console.Write($"{FL}");
                    Console.WriteLine();

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
                    int itemIndex = inventoryE[number - 1];

                    if (itemIndex >= 0 && itemIndex < items.Count)
                    {
                        Item item = inventory[itemIndex].Item;
                        Player.EquipItem(item);

                        Util.False_();
                        Console.Write("\u001b[38;2;255;200;82m");
                        Util.Write($"[장착 완료] {item.Name}을(를) 장착했습니다!", 20, 5);
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.ResetColor();

                        Thread.Sleep(1000);
                        ShowInventory();
                    }
                    else
                    {
                        Console.WriteLine("잘못된 아이템입니다.");
                        Item.Inven_eq(Console.ReadLine(), index);
                    }
                }
                else
                {
                    ShowInventory();
                }
            }
        }

        public static void Inven_con(string choice, int index)
        {
            int number;

            if (int.TryParse(choice, out number))
            {
                if (number > 0 && number <= index)
                {
                    int itemIndex = inventoryE[number - 1];

                    if (itemIndex >= 0 && itemIndex < items.Count)
                    {
                        Inventory slot = inventory[itemIndex];
                        Item item = slot.Item;


                       
                        Util.False_();
                        Console.Write("\u001b[38;2;255;200;82m");
                        Util.Write($"[아이템 사용] {item.Name}을(를) 사용했습니다!", 20, 5);
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.ResetColor();

                        slot.Quantity--;
                        if (slot.Quantity <= 0)
                        {
                            inventory.Remove(itemIndex);
                        }

                        Thread.Sleep(1000);
                        ShowInventory();
                    }
                    else
                    {
                        Console.WriteLine("잘못된 아이템입니다.");
                        Item.Inven_eq(Console.ReadLine(), index);
                    }
                }
                else
                {
                    ShowInventory();
                }
            }
        }



    }






}
