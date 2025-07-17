using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static sparat_dungeon.Item;

namespace sparat_dungeon
{
   
    public class Player
    {
        public int PlayerLevel { get; set; }
        public string PlayerName { get; set; }
        public string PlayerJob { get; set; }
        public int PlayerAtk { get; set; }
        public int PlayerDef { get; set; }
        public int PlayerHp { set; get; }
        public int PlayerMp { get; set; }  
        public int PlayerGold { get; set; }
        public int PlayerExp { get; set; }

        List<int> ExpTable = new List<int> { 8, 15, 20, 35 };

        public List<Skill> Skills { get; set; } = new List<Skill>();
       

        public static int PlayerExtraAtk { get; set; }
        public static int PlayerExtraDef { get; set; }
        public int TotalAtk => PlayerAtk + PlayerExtraAtk;
        public int TotalDef => PlayerDef + PlayerExtraDef;

        public int QuestKillCount { get; set; }
        public static Item EquipWepon { get; set; }
        public static Item EquipArmor { get; set; }

        public Random random = new Random();

        public Player(string playerName, string jobSelect)
        {
            

            if (jobSelect == "1" || jobSelect == "검호")
            {
                PlayerLevel = 1;
                PlayerExp = 0;
                PlayerName = playerName;
                PlayerJob = "검호";
                PlayerHp = 100;
                PlayerMp = 50;
                PlayerAtk = 10;
                PlayerDef = 7;
                PlayerGold = 1500;
            }
            else if (jobSelect == "2" || jobSelect == "자객")
            {
                PlayerLevel = 1;
                PlayerExp = 0;
                PlayerName = playerName;
                PlayerJob = "자객";
                PlayerHp = 120;
                PlayerMp = 50;
                PlayerAtk = 12;
                PlayerDef = 5;
                PlayerGold = 1500;
            }
            else if (jobSelect == "3" || jobSelect == "도인")
            {
                PlayerLevel = 1;
                PlayerExp = 0;
                PlayerName = playerName;
                PlayerJob = "도인";
                PlayerHp = 80;
                PlayerMp = 100;
                PlayerAtk = 8;
                PlayerDef = 3;
                PlayerGold = 1500;
            }
        }

        public void Playerinfo()
        {
            Console.WriteLine($"레벨: {PlayerLevel}");
            Console.WriteLine($"경험치: {PlayerExp}");
            Console.WriteLine($"이름: {PlayerName}");
            Console.WriteLine($"직업: {PlayerJob}");
            Console.WriteLine(PlayerExtraAtk > 0 ? $"공격력: {PlayerAtk} +{PlayerExtraAtk}" : $"공격력: {PlayerAtk}");
            Console.WriteLine(PlayerExtraDef > 0 ? $"방어력: {PlayerDef} +{PlayerExtraDef}" : $"방어력: {PlayerDef}");
            Console.WriteLine($"체력: {PlayerHp}");
            Console.WriteLine($"마나: {PlayerMp}");
            Console.WriteLine($"골드: {PlayerGold} Gold");
            
        }

        public void GainHp(int healing)
        {
            PlayerHp += healing;
        }

        public void GainExp(int exp)
        {
            PlayerExp += exp;
            while (PlayerLevel - 1 < ExpTable.Count && PlayerExp >= ExpTable[PlayerLevel - 1])
            {
                LevelUp();
            }
        }

        public int GetExpToNextLevel()
        {
            if (PlayerLevel - 1 < ExpTable.Count)
                return ExpTable[PlayerLevel - 1];
            else
                return -1; 

        }

        public void LevelUp()
        {
            PlayerExp -= ExpTable[PlayerLevel - 1];
            PlayerLevel++;
            PlayerAtk += 2;
            PlayerDef += 1;
            
            Console.WriteLine($"\n{PlayerName} 님이 레벨업 하셨습니다! 현재 레벨: {PlayerLevel}");

            Advancement();
        }
        //스킬
        public void SetSkill()
        {
            Skills.Clear();

            switch (PlayerJob)
            {
                case "검호":
                case "검존":
                case "검성":
                    Skills.Add(new Skill("참격", 20, 5));
                    Skills.Add(new Skill("유성참", 30, 10));
                    break;

                case "자객":
                case "귀객":
                case "유협":
                    Skills.Add(new Skill("암살", 25, 8));
                    Skills.Add(new Skill("절개", 15, 4));
                    break;

                case "도인":
                case "도사":
                case "현인":
                    Skills.Add(new Skill("화염구", 20, 6));
                    Skills.Add(new Skill("화염폭풍", 35, 15));
                    break;

            }
        }
        public void UseSkill(int skillIndex, Monster monster)
        {
            if (skillIndex < 0 || skillIndex >= Skills.Count)
            {
                Console.WriteLine("잘못된 스킬 번호입니다.");
                return;
            }

            Skill skill = Skills[skillIndex];

            if (PlayerMp < skill.SkillMana)
            {
                Console.WriteLine("MP가 부족하여 스킬을 사용할 수 없습니다.");
                return;
            }

            PlayerMp -= skill.SkillMana;

            Console.WriteLine($"{PlayerName} 이(가) {skill.SkillName} 스킬을 사용했습니다!");
            Console.WriteLine($"→ Lv.{monster.Level} {monster.Name} 에게 {skill.SkillDamage}의 데미지!");

            monster.TakeDamage(skill.SkillDamage);

            Console.Write($"→ 남은 몬스터 HP: ");
            if (monster.IsDead == true)
            {
                QuestKillCount++;
                Console.WriteLine("Dead");
            }
            else
            {
                Console.WriteLine(monster.Hp.ToString());
            }

        }


        // 승급
        public void Advancement()
        {
            // 1차 전직
            if (PlayerLevel == 2 && PlayerJob == "검호")
            {
                PlayerJob = "검존";

                Console.WriteLine($"\n {PlayerName} 님이 승급 하여 {PlayerJob}이(가) 되셨습니다!");
            }
            else if(PlayerLevel == 2 && PlayerJob == "자객")
            {
                PlayerJob = "귀객";

                Console.WriteLine($"\n{PlayerName} 님이 승급 하여 {PlayerJob}이(가) 되셨습니다!");
            }
            else if (PlayerLevel == 2 && PlayerJob == "도인")
            {
                PlayerJob = "도사";

                Console.WriteLine($"\n{PlayerName} 님이 승급 하여 {PlayerJob}이(가) 되셨습니다!");
            }

            // 2차 전직
            else if (PlayerLevel == 3 && PlayerJob == "검존")
            {
                PlayerJob = "검성";

                Console.WriteLine($"\n{PlayerName} 님이 승급 하여 {PlayerJob}이(가) 되셨습니다!");
            }
            else if (PlayerLevel == 3 && PlayerJob == "귀객")
            {
                PlayerJob = "유협";

                Console.WriteLine($"\n {PlayerName} 님이 승급 하여 {PlayerJob}이(가) 되셨습니다!");
            }
            else if (PlayerLevel == 3 && PlayerJob == "도사")
            {
                PlayerJob = "현인";

                Console.WriteLine($"\n {PlayerName} 님이 승급 하여 {PlayerJob}이(가) 되셨습니다!");
            }
        }

        //public void UseSkill(int skillIndex, Monster target)
        //{
        //    if (skillIndex < 0 || skillIndex >= Skills.Count)
        //    {
        //        Console.WriteLine("잘못된 스킬 선택입니다.");
        //        return;
        //    }
        //    Skills[skillIndex].UseSkill(this, target);
        //}


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
            for (int i = 0; i < Item.MaxInventory; i++)

                if (Item.inventory.ContainsKey(i))
                {
                    Inventory slot = Item.inventory[i];
                    Item item = slot.Item;

                    if (ItemType.장비 == item.Type)
                    {
                        Item.inventoryE[index] = i;
                        index++;
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


            if (index == 0)
            {
                Item.ShowInventory();
                Console.Write("\x1b[38;2;181;53;53m");
                Console.Write("[!] 장착 가능한 장비가 없습니다.");
                Program.state = 2;
            }
            else
            {
                Console.WriteLine("");
                Console.SetCursorPosition(6, Console.CursorTop);
                Console.Write($"\x1b[38;2;135;206;250m0.\x1b[38;2;240;248;255m");
                Console.Write("뒤로가기");
                Console.WriteLine("", 30, 10);
                Item.Inven_eq(Console.ReadLine(), index);
                Program.state = 2;
            }


        }

        public int PlayerDamageCalc()
        {
            int damage = random.Next(TotalAtk - (int)Math.Ceiling((TotalAtk * 0.1)), (int)Math.Ceiling(TotalAtk + (TotalAtk * 0.1)));

            return damage;
        }


        public static void ShowInventoryC()
        {
            string str;
            int index = 0;

            Console.Clear();
            Console.SetCursorPosition(6, 2);
            Console.WriteLine("\u001b[38;2;135;206;250m[사용 가능 아이템]\u001b[38;2;240;248;255m");
            Console.WriteLine("");
            for (int i = 0; i < Item.MaxInventory; i++)

                if (Item.inventory.ContainsKey(i))
                {
                    Inventory slot = Item.inventory[i];
                    Item item = slot.Item;

                    if (ItemType.소비 == item.Type)
                    {
                        Item.inventoryE[index] = i;
                        index++;

                        str = $"\u001b[38;2;135;206;250m[{index}]\u001b[38;2;240;248;255m {item.Name} ";

                        str = str + $"{Item.FL} {item.Ex}";

                        Console.SetCursorPosition(5, Console.CursorTop);
                        Console.WriteLine(str);
                    }
                }


            if (index == 0)
            {
                Item.ShowInventory();
                Console.Write("\x1b[38;2;181;53;53m");
                Console.Write("[!] 사용 가능한 아이템이 없습니다.");
                Program.state = 2;
            }
            else
            {
                Console.WriteLine("");
                Console.SetCursorPosition(6, Console.CursorTop);
                Console.Write($"\x1b[38;2;135;206;250m0.\x1b[38;2;240;248;255m");
                Console.Write("뒤로가기");
                Console.WriteLine("", 30, 10);
                Item.Inven_con(Console.ReadLine(), index);
                Program.state = 2;
            }


        }
    }
}
