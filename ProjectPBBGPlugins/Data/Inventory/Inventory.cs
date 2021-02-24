using DarkRift;
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

    public class PKT_UPDATE_PLAYERINVENTORY : IDarkRiftSerializable
    {
        public int _ItemDatabaseSize = 0;
        public List<Item> _InventoryItems;
        public void Deserialize(DeserializeEvent e)
        {
            if (_InventoryItems.Count > 0)
                _InventoryItems.Clear();

            _ItemDatabaseSize = e.Reader.ReadInt32();
            while (e.Reader.Position < e.Reader.Length)
            {
                Item newInventoryItem = new Item();
                newInventoryItem.ID = e.Reader.ReadInt32();
                newInventoryItem.Name = e.Reader.ReadString();
                newInventoryItem.StackSize = e.Reader.ReadInt32();
                _InventoryItems.Add(newInventoryItem);
            }
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(_InventoryItems.Count);
            foreach (Item _item in _InventoryItems)
            {
                e.Writer.Write(_item.ID);
                e.Writer.Write(_item.Name);
                e.Writer.Write(_item.StackSize);
            }
            if (ServerMain.isDebug) Debug.Log("[Inventory] Sent " + _InventoryItems.Count + " items.", ConsoleColor.Green);
        }

        public PKT_UPDATE_PLAYERINVENTORY() { }
        public PKT_UPDATE_PLAYERINVENTORY(List<Item> _Inv)
        {
            _InventoryItems = _Inv;
        }
    }
}
