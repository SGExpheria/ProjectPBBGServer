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

        public int StackSize = 1;

        public void Use(Account account) { 
        

        }

        public Item() { }
        public Item(string name, bool isusable, bool isequippable, string applicabletype)
        {
            Name = name; isUsable = isusable; isEquippable = isequippable;
        }
    }

    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    public class Weapon : Item
    {

        public Weapon() { 
        
        }
        public Weapon(string name, bool isusable, bool isequippable, string applicabletype)
        {
            Name = name; isUsable = isusable; isEquippable = isequippable;
        }
    }

    [Serializable]
    [JsonObject(MemberSerialization.OptOut)]
    public class Tool : Weapon
    {
        public Tool() { }
        public Tool(string name, bool isusable, bool isequippable, string applicabletype)
        {
            Name = name; isUsable = isusable; isEquippable = isequippable;
        }
    }
}
