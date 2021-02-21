using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPBBGPlugins
{
    public class CityDatabase
    {
        public List<City> _Cities = new List<City>();

        public void Save()
        {
            Debug.Log("[Database] Saving " + _Cities.Count.ToString() + " Cities to Database", ConsoleColor.Cyan);

            foreach (City _City in _Cities)
            {
                JSON.Serialize(Path.CityDatabaseDirectory + _City.Name, _City);
            }

            Debug.Log("[Database] Saved City Database", DebugColors.SavedColor);
        }
        public void Load()
        {
            if (!File.Exists(Path.DatabaseDirectory + @"CityDatabase.json"))
            {
                Debug.Log("[Database] Could not load CityDatabase", ConsoleColor.Red);
                return;
            }

            Debug.Log("[Database] Loading City Database", ConsoleColor.Green);
            foreach (string path in Directory.GetFiles(Path.CityDatabaseDirectory))
            {
                City _City = JSON.Deserialize<City>(path);
                _Cities.Add(_City);
                Debug.Log("[City Database] Loaded City " + _City.Name + " into City Database", ConsoleColor.DarkYellow);
            }
            Debug.Log("[Database] Finished Loading City Database", DebugColors.LoadedColor);
        }

        public void AddCity(City cityToAdd)
        {
            cityToAdd.ID = _Cities.Count + 1;
            _Cities.Add(cityToAdd);
            Debug.Log("[City Database] Added city " + cityToAdd.Name + " to database.", ConsoleColor.Cyan);
        }

        public City GetCityByName(string name)
        {
            return _Cities.Where(i => i.Name == name).FirstOrDefault();
        }
    }
}
