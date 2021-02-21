using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPBBGPlugins
{
    [Serializable]
    public class Mining_Ore
    {
        public string Name;
        public Item Ore;
        public float Experience;
        public Mining_Ore() { }
        public Mining_Ore(string name) { Name = name; }
    }
}
