using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sparat_dungeon
{
    public class Skill
    {
        public string SkillName { get; set; }
        public int SkillDamage { get; set; }
        public int SkillMana { get; set; }

        public Skill(string skillName, int skillDamage, int skillMana)
        {
            SkillName = skillName;
            SkillDamage = skillDamage;
            SkillMana = skillMana;
        }

        public void UseSkill(Player player, Monster target)
        {
            if(player.PlayerMp < SkillMana )
            {
                Console.WriteLine("마나가 부족합니다.");
                return;
            }

            player.PlayerMp -= SkillMana;
            target.TakeDamage(SkillDamage);
        }

    }
}
