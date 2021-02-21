using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPBBGPlugins
{
    [Serializable]
    public class CharacterClass
    {
        public int ID;
        public string Name;
        public string Description;

        public CharacterClass() { }
        public CharacterClass(string name, string description)
        {
            Name = name; Description = description;
        }
    }
}
