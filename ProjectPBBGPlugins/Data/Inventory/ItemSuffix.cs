using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPBBGPlugins
{
    [Serializable]
    public class ItemSuffix : IItemSuffix
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Value { get; set; }
        public float MaxValue { get; set; }
        public int ID { get; set; }

        public ItemSuffix() { }
        public ItemSuffix(string name, string desc, float maxval)
        {
            Name = name; Description = desc; Value = 0; MaxValue = maxval;
        }
        public ItemSuffix(string name, string desc, float val, float maxval)
        {
            Name = name; Description = desc; Value = val; MaxValue = maxval;
        }
    }
}
