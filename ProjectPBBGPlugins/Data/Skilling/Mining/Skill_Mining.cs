using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPBBGPlugins
{
    [Serializable]
    public class Skill_Mining : Skill
    {
        public Mining_Ore SkillOre;

        public override void Action()
        {
            if (_AccountInventory == null)
                return;

            CurrentExperience += SkillOre.Experience;
            _AccountInventory.AddItem(Database._ItemDatabase.GetItemByName(SkillOre.Ore.Name));
        }
    }
}
