using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPBBGPlugins
{
    [Serializable]
    public class Skill
    {
        public int ID;
        public string Name;

        public int Level = 1;
        public int MaxLevel = 99;
        public float CurrentExperience;
        public float MaxExperience = 100;

        [JsonIgnore]
        public Account _Account = null;

        public virtual void Action() { }
        public void Update()
        {
            LevelCheck();
        }
        public void LevelCheck()
        {
            if (CurrentExperience >= MaxExperience)
            {
                CurrentExperience = 0;
                MaxExperience = MaxExperience * 1.5f;
                Level++;
            }
        }

        public Skill() { }
        public Skill(string name) { Name = name; }
    }
}
