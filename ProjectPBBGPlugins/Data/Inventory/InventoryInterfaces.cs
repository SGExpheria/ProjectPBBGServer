using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPBBGPlugins
{
    public interface IItem
    {
        float Rarity { get; set; }
        string CraftedBy { get; set; }
        string Name { get; set; }
        int ID { get; set; }

        bool isStackable { get; set; }
        bool isUsable { get; set; } //Potions, food etc.
        bool isEquippable { get; set; } //Armor, weapons etc.

        void Use(Account account);
    }

    public interface IItemSuffix
    {
        string Name { get; set; }
        string Description { get; set; }
        float Value { get; set; }
        int ID { get; set; }
    }
}
