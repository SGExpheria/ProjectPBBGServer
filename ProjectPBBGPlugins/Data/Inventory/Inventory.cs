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

        public void AddItem(Item itemToAdd, int amount)
        {
            if (Items.Where(i => i.ID == itemToAdd.ID).Any() == false)
            {
                Items.Add(itemToAdd);
                return;
            }

            switch (itemToAdd.isStackable)
            {
                case true:
                    Items.Where(i => i.ID == itemToAdd.ID).FirstOrDefault().StackSize += amount;
                    break;
                case false:
                    Items.Add(itemToAdd);
                    break;
            }
            Debug.Log("Added item " + itemToAdd.Name + " with a stack size of " + amount, ConsoleColor.Cyan);
        }

        public Item GetItemByName(string name)
        {
            return Items.Where(i => i.Name == name).FirstOrDefault();
        }

        public void Save(string accountPath)
        {
            JSON.Serialize(accountPath + @"\Inventory.json", this);
        }
    }
}
