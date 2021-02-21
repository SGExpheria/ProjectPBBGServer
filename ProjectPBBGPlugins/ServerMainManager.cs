using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DarkRift;
using DarkRift.Server;

namespace ProjectPBBGPlugins
{
    class ServerMainManager : Plugin
    {
        public override Command[] Commands => new Command[]
        {
            new Command("Load", "Reloads all server files", "Load", Load),
            new Command("Save", "Saves all server files", "Save", Save),
            new Command("Quit", "Quits and saves", "Quit", Quit),
            new Command("BuildItemDatabase", "", "BuildItemDatabase", BuildItemDatabase),
            new Command("BuildItemSuffixDatabase", "", "BuildItemSuffixDatabase", BuildItemSuffixDatabase),
            new Command("BuildClassDatabase", "", "BuildClassDatabase", BuildClassDatabase),
            new Command("BuildCityDatabase", "", "BuildCityDatabase", BuildCityDatabase)
        };

        public override bool ThreadSafe => false;
        public override Version Version => new Version(1, 0, 0);

        public ServerMainManager(PluginLoadData pluginLoadData) : base (pluginLoadData)
        {
            TickManager.Init();
            ClientManager.ClientConnected += OnClientConnected;
            ClientManager.ClientDisconnected += OnClientDisconnect;
        }

        public void OnClientConnected(object sender, ClientConnectedEventArgs e)
        {
            PKT_DATABASE_REQUESTITEMDATABASE _ItemDatabaseRequest = new PKT_DATABASE_REQUESTITEMDATABASE(Database._ItemDatabase._ItemDatabase);
            e.Client.SendMessage(Message.Create((ushort)Pkt.PKT_DATABASE_REQUESTITEMDATABASE, _ItemDatabaseRequest), SendMode.Reliable);
        }

        public void OnClientDisconnect(object sender, ClientDisconnectedEventArgs e)
        {
            if (AccountManager.ActiveAccounts.ContainsKey(e.Client))
            {
                TickManager._Tick -= AccountManager.ActiveAccounts[e.Client].Tick;
                AccountManager.ActiveAccounts.Remove(e.Client);
            }
        }

        void BuildItemDatabase(object sender, CommandEventArgs e)
        {
            Item newOre = new Item("Copper Ore", false, false, "Crafting Material, Ore");

            Database._ItemDatabase.AddItem(newOre);
            Database._ItemDatabase.Save();
        }

        void BuildCityDatabase(object sender, CommandEventArgs e)
        {
            City newCity = new City();
            newCity.Name = "Coral Island";
            newCity._CityOres.Add(new Mining_Ore("Copper Ore"));

            Database._CityDatabase.AddCity(newCity);
            Database._CityDatabase.Save();
        }

        void BuildItemSuffixDatabase(object sender, CommandEventArgs e)
        {
            ItemSuffix newBaseSuffix = new ItemSuffix("_PhysicalDamage", "Physical Damage", float.MaxValue);
            ItemSuffix newBaseSuffix1 = new ItemSuffix("_MiningPower", "Determines amount of damage dealt to ore.", float.MaxValue);
            ItemSuffix newBaseSuffix2 = new ItemSuffix("_HealPower", "Determins how much health is gained upon consumption", float.MaxValue);
            Database._ItemSuffixDatabase.AddBaseSuffix(newBaseSuffix);
            Database._ItemSuffixDatabase.AddBaseSuffix(newBaseSuffix1);
            Database._ItemSuffixDatabase.AddBaseSuffix(newBaseSuffix2);
            Database._ItemSuffixDatabase.Save();
        }

        void BuildClassDatabase(object sender, CommandEventArgs e)
        {
            CharacterClass Farmer = new CharacterClass("Farmer", "This class benefits in Farming!" + "\n\n" + "25% more Crop yield" + "\n\n" + "2x Crop Growth Rate" + "\n\n" + "Class specific recipes.");
            CharacterClass Miner = new CharacterClass("Miner", "This class benefits in Mining!" + "\n\n" + "25% more Ore yield" + "\n\n" + "50% Chance to mine double the ore!" + "\n\n" + "Class specific recipes.");
            CharacterClass Lumberjack = new CharacterClass("Lumberjack", "This class benefits in Woodcutting!" + "\n\n" + "25% more Log yield" + "\n\n" + "Ability to chop down Bird Nests." + "\n\n" + "Class specific recipes.");

            Database._ClassDatabase.AddClass(Farmer);
            Database._ClassDatabase.AddClass(Miner);
            Database._ClassDatabase.AddClass(Lumberjack);
            Database._ClassDatabase.Save();
        }

        void Load(object sender, CommandEventArgs e)
        {
            AccountManager.Load();
            Database._ItemDatabase.Load();
            Database._ItemSuffixDatabase.Load();
        }
        void Save(object sender, CommandEventArgs e)
        {
            AccountManager.Save();
            Database._ItemDatabase.Save();
            Database._ItemSuffixDatabase.Save();
        }
        void Quit(object sender, CommandEventArgs e)
        {
            AccountManager.Save();
            Database._ItemDatabase.Save();
            Database._ItemSuffixDatabase.Save();
            System.Environment.Exit(1);
        }
    }
}
