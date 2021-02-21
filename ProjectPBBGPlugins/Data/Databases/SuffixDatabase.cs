using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPBBGPlugins
{
    [Serializable]
    public class SuffixDatabase
    {
        public List<ItemSuffix> _BaseSuffixDatabase = new List<ItemSuffix>();
        public List<ItemSuffix> _SuffixDatabase = new List<ItemSuffix>();

        public void Save()
        {
            Debug.Log("[Database] Saving " + _SuffixDatabase.Count.ToString() + " Item Suffixes to Database", ConsoleColor.Cyan);
            JSON.Serialize(Path.ItemDatabaseDirectory + "ItemSuffixDatabase.json", this);
            Debug.Log("[Database] Saved Suffix Database", DebugColors.SavedColor);
        }

        public void Load()
        {
            if (!File.Exists(Path.ItemDatabaseDirectory + @"ItemSuffixDatabase.json"))
            {
                Debug.Log("[Database] Could not load Item Suffix Database", ConsoleColor.Red);
                return;
            }

            Debug.Log("[Database] Loading Item Suffix Database", ConsoleColor.Green);
            SuffixDatabase _LoadingDatabase = JSON.Deserialize<SuffixDatabase>(Path.ItemDatabaseDirectory + "ItemSuffixDatabase.json");
            foreach (ItemSuffix suffix in _LoadingDatabase._SuffixDatabase)
            {
                _SuffixDatabase.Add(suffix);
                Debug.Log("[Item Suffix Database] Loaded item " + suffix.Name + " into SuffixDatabase", ConsoleColor.DarkYellow);
            }
            foreach (ItemSuffix basesuffix in _LoadingDatabase._BaseSuffixDatabase)
            {
                _SuffixDatabase.Add(basesuffix);
                Debug.Log("[Item Suffix Database] Loaded item " + basesuffix.Name + " into SuffixDatabase", ConsoleColor.DarkYellow);
            }
            Debug.Log("[Database] Finished Loading Item Suffix Database", DebugColors.LoadedColor);
        }

        public void AddSuffix(ItemSuffix suffixToAdd)
        {
            suffixToAdd.ID = _SuffixDatabase.Count + 1;
            _SuffixDatabase.Add(suffixToAdd);
            Debug.Log("[Item Suffix Database] Added suffix " + suffixToAdd.Name + " to database.", ConsoleColor.Cyan);
        }
        public void AddBaseSuffix(ItemSuffix suffixToAdd)
        {
            suffixToAdd.ID = _BaseSuffixDatabase.Count + 1;
            _BaseSuffixDatabase.Add(suffixToAdd);
            Debug.Log("[Item Suffix Database] Added base suffix " + suffixToAdd.Name + " to database.", ConsoleColor.Cyan);
        }

        public ItemSuffix GetBaseSuffix(string name)
        {
            foreach (ItemSuffix suffix in _BaseSuffixDatabase)
            {
                if (suffix.Name == name)
                    return suffix;
            }
            return null;
        }
    }
}
