using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPBBGPlugins
{
    public class SkillDatabase
    {
        public List<Skill> _Skills = new List<Skill>();

        public void Save()
        {
            Debug.Log("[Database] Saving " + _Skills.Count.ToString() + " skills to Database", ConsoleColor.Cyan);
            JSON.Serialize(Path.DatabaseDirectory + "SkillDatabase.json", this);
            Debug.Log("[Database] Saved Skill Database", DebugColors.SavedColor);
        }
        public void Load()
        {
            if (!File.Exists(Path.DatabaseDirectory + @"SkillDatabase.json"))
            {
                Debug.Log("[Database] Could not load SkillDatabase", ConsoleColor.Red);
                return;
            }

            Debug.Log("[Database] Loading Skill Database", ConsoleColor.Green);
            SkillDatabase _LoadingDatabase = JSON.Deserialize<SkillDatabase>(Path.DatabaseDirectory + "SkillDatabase.json");
            foreach (Skill skill in _LoadingDatabase._Skills)
            {
                _Skills.Add(skill);
                Debug.Log("[Skill Database] Loaded skill " + skill.Name + " into _Skills", ConsoleColor.DarkYellow);
            }
            Debug.Log("[Database] Finished Loading Skill Database", DebugColors.LoadedColor);
        }

        public void AddSkill(Skill skillToAdd)
        {
            skillToAdd.ID = _Skills.Count + 1;
            _Skills.Add(skillToAdd);
            Debug.Log("[Skill Database] Added skill " + skillToAdd.Name + " to database.", ConsoleColor.Cyan);
        }

        public Skill GetSkillByName(string name)
        {
            return _Skills.Where(i => i.Name == name).FirstOrDefault();
        }
    }
}
