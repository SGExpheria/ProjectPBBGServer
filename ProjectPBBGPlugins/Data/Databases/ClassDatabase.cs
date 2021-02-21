using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPBBGPlugins
{
    [Serializable]
    public class ClassDatabase
    {
        public List<CharacterClass> _ClassDatabase = new List<CharacterClass>();

        public void Save()
        {
            Debug.Log("[Database] Saving " + _ClassDatabase.Count.ToString() + " Classes to Class Database", ConsoleColor.Cyan);
            JSON.Serialize(Path.DatabaseDirectory + "ClassDatabase.json", this);
            Debug.Log("[Database] Saved Class Database", DebugColors.SavedColor);
        }
        public void Load()
        {
            if (!File.Exists(Path.DatabaseDirectory + @"ClassDatabase.json"))
            {
                Debug.Log("[Database] Could not load Class Database", ConsoleColor.Red);
                return;
            }

            Debug.Log("[Database] Loading Class Database", ConsoleColor.Green);
            ClassDatabase _LoadingDatabase = JSON.Deserialize<ClassDatabase>(Path.DatabaseDirectory + "ClassDatabase.json");
            foreach (CharacterClass item in _LoadingDatabase._ClassDatabase)
            {
                _ClassDatabase.Add(item);
                Debug.Log("[Class Database] Loaded Class " + item.Name + " into _ClassDatabase", ConsoleColor.DarkYellow);
            }
            Debug.Log("[Database] Finished Loading Class Database", DebugColors.LoadedColor);
        }

        public void AddClass(CharacterClass classToAdd)
        {
            classToAdd.ID = _ClassDatabase.Count + 1;
            _ClassDatabase.Add(classToAdd);
            Debug.Log("[Class Database] Added item " + classToAdd.Name + " to database.", ConsoleColor.Cyan);
        }

        public CharacterClass GetClassByID(int id)
        {
            return _ClassDatabase.Where(i => i.ID == id).FirstOrDefault();
        }
    }
}
