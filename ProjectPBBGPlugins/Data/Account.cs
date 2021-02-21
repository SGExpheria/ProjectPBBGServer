using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProjectPBBGPlugins
{
    [Serializable]
    public class Account
    {
        public int ID;

        public string Username;
        public string Password;
        public string Email;
        public string Referral;

        public bool isNew = true;
        public bool isBanned = false;
        public bool isDev = false;

        [JsonIgnore]
        public bool isSkillsSetup = false;

        public string CityName;

        [JsonIgnore]
        public List<Skill> Skills = new List<Skill>();

        [JsonIgnore]
        public City City;

        [JsonIgnore]
        private Inventory _Inventory = null;
        [JsonIgnore]
        public Inventory Inventory
        {
            get
            {
                if (!File.Exists(Path.AccountDirectory + Username + @"\Inventory.json"))
                {
                    Inventory _NewInventory = new Inventory();
                    //Add starter items if needed.
                    JSON.Serialize<Inventory>(Path.AccountDirectory + Username + @"\Inventory.json", _NewInventory);
                }
                if (_Inventory == null)
                    _Inventory = JSON.Deserialize<Inventory>(Path.AccountDirectory + Username + @"\Inventory.json");
                return _Inventory;
            }
        }

        [JsonIgnore]
        private CharacterClass _Class = null;
        [JsonIgnore]
        public CharacterClass Class
        {
            get
            {
                if (!File.Exists(Path.AccountDirectory + Username + @"\Class.json"))
                {
                    CharacterClass _NewClass = new CharacterClass();
                    _NewClass.ID = -1;
                    _NewClass.Name = "Not Set";
                    _NewClass.Description = "Not Set";
                    JSON.Serialize(Path.AccountDirectory + Username + @"\Class.json", _NewClass);
                }
                if (_Class == null)
                    _Class = JSON.Deserialize<CharacterClass>(Path.AccountDirectory + Username + @"\Class.json");

                return _Class;
            }
        }

        public void SaveInventory()
        {
            Inventory.Save(Path.AccountDirectory + Username);
        }
        public void SaveClass()
        {
            JSON.Serialize(Path.AccountDirectory + Username + @"\Class.json", Class);
        }

        public void Save()
        {
            if (!Directory.Exists(Path.AccountDirectory + Username))
            {
                Directory.CreateDirectory(Path.AccountDirectory + Username);
            }
            JSON.Serialize(Path.AccountDirectory + Username + @"\Account.json", this);
            SaveInventory();
            SaveClass();
        }

        public void Tick()
        {
            if (!isSkillsSetup)
                SetupSkills();
        }

        public void SetupSkills()
        {
            foreach (Skill skill in Skills)
            {
                skill._AccountInventory = this.Inventory;
            }
            isSkillsSetup = true;
        }

        public Account() { }
        public Account(string username, string password, string email, string referral)
        {
            Username = username; Password = password; Email = email; Referral = referral;
        }
    }

}
