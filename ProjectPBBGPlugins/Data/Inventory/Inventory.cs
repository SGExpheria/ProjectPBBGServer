using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPBBGPlugins
{
    [Serializable]
    public class Inventory
    {
        public double Gold = 500;
        public double Crystals = 0;
        public List<Item> Items = new List<Item>();

        public void AddItem(Item itemToAdd)
        {
            Items.Add(itemToAdd);
        }

        public Item GetItemByName(string name)
        {
            return Items.Where(i => i.Name == name).FirstOrDefault();
        }

        public void Save(string accountPath)
        {
            JSON.Serialize<Inventory>(accountPath + @"\Inventory.json", this);
        }
    }
}
