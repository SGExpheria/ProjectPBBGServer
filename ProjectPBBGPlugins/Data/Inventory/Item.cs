using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using DarkRift;
using DarkRift.Client;
using DarkRift.Server;

namespace ProjectPBBGPlugins
{

    [Serializable]
    public class Item : IItem
    {
        public string CraftedBy { get; set; }
        public string Name { get; set; }

        public float Rarity { get; set; }
        public int ID { get; set; }

        public bool isStackable { get; set; }
        public bool isUsable { get; set; }
        public bool isEquippable { get; set; }

        public void Use(Account account) { 
        

        }

        public string ApplicableType = "Consumable";

        public List<ItemSuffix> BaseSuffixes = new List<ItemSuffix>();
        public List<ItemSuffix> Suffixes = new List<ItemSuffix>();

        public Item() { }
        public Item(string name, bool isusable, bool isequippable, string applicabletype, List<ItemSuffix> basesuffixes)
        {
            Name = name; isUsable = isusable; isEquippable = isequippable; ApplicableType = applicabletype; BaseSuffixes = basesuffixes;
        }
    }

    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    public class Weapon : Item
    {
        public string DamageType = "Physical";
        public Weapon() { 
        
        }
        public Weapon(string name, bool isusable, bool isequippable, string applicabletype, List<ItemSuffix> basesuffixes)
        {
            Name = name; isUsable = isusable; isEquippable = isequippable; ApplicableType = applicabletype; BaseSuffixes = basesuffixes;
        }
    }

    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    public class Tool : Weapon
    {
        public Tool() { }
        public Tool(string name, bool isusable, bool isequippable, string applicabletype, List<ItemSuffix> basesuffixes)
        {
            Name = name; isUsable = isusable; isEquippable = isequippable; ApplicableType = applicabletype; BaseSuffixes = basesuffixes;
        }
    }
}
