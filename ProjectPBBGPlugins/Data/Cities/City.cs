using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPBBGPlugins
{
    [Serializable]
    public class City
    {
        public int ID;
        public string Name;

        public List<Mining_Ore> _CityOres = new List<Mining_Ore>();
    }
}
