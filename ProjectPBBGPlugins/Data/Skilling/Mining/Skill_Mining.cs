using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DarkRift;
using DarkRift.Server;

namespace ProjectPBBGPlugins
{
    [Serializable]
    public class Skill_Mining : Skill
    {
        public Mining_Ore SkillOre;

        public override void Action()
        {
            if (_Account == null)
                return;

            CurrentExperience += SkillOre.Experience;

            _Account.Inventory.AddItem(Database._ItemDatabase.GetItemByName(SkillOre.Ore.Name), SkillOre.Amount);

            Debug.Log("[Skill] " + _Account.Username + " Mined " + SkillOre.Ore.Name + " with a stack size of " + SkillOre.Amount, ConsoleColor.Magenta);
        }

        public Skill_Mining() { }
        public Skill_Mining(string name) { Name = name; }
    }
}
