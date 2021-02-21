using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPBBGPlugins
{
    [Serializable]
    public class ItemDatabase
    {
        public List<Item> _ItemDatabase = new List<Item>();

        public void Save()
        {
            Debug.Log("[Database] Saving " + _ItemDatabase.Count.ToString() + " items to Database", ConsoleColor.Cyan);
            JSON.Serialize(Path.ItemDatabaseDirectory + "ItemDatabase.json", this);
            Debug.Log("[Database] Saved Item Database", DebugColors.SavedColor);
        }
        public void Load()
        {
            if (!File.Exists(Path.ItemDatabaseDirectory + @"ItemDatabase.json"))
            {
                Debug.Log("[Database] Could not load ItemDatabase", ConsoleColor.Red);
                return;
            }

            Debug.Log("[Database] Loading Item Database", ConsoleColor.Green);
            ItemDatabase _LoadingDatabase = JSON.Deserialize<ItemDatabase>(Path.ItemDatabaseDirectory + "ItemDatabase.json");
            foreach (Item item in _LoadingDatabase._ItemDatabase)
            {
                _ItemDatabase.Add(item);
                Debug.Log("[Item Database] Loaded item " + item.Name + " into _ItemDatabase", ConsoleColor.DarkYellow);
            }
            Debug.Log("[Database] Finished Loading Item Database", DebugColors.LoadedColor);
        }

        public void AddItem(Item itemToAdd)
        {
            itemToAdd.ID = _ItemDatabase.Count + 1;
            _ItemDatabase.Add(itemToAdd);
            Debug.Log("[Item Database] Added item " + itemToAdd.Name + " to database.", ConsoleColor.Cyan);
        }

        public Item GetItemByName(string name)
        {
            return _ItemDatabase.Where(i => i.Name == name).FirstOrDefault();
        }
    }
}
