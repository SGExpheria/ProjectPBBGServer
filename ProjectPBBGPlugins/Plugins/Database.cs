using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DarkRift;
using DarkRift.Server;

namespace ProjectPBBGPlugins
{
    public class Database : Plugin
    {
        public override bool ThreadSafe => false;
        public override Version Version => new Version(1, 0, 0);

        public static CityDatabase _CityDatabase = new CityDatabase();
        public static ItemDatabase _ItemDatabase = new ItemDatabase();
        public static SuffixDatabase _ItemSuffixDatabase = new SuffixDatabase();
        public static ClassDatabase _ClassDatabase = new ClassDatabase();
        public static SkillDatabase _SkillDatabase = new SkillDatabase();

        public Database(PluginLoadData pluginLoadData) : base(pluginLoadData)
        {
            _CityDatabase.Load();
            _ClassDatabase.Load();
            _ItemSuffixDatabase.Load();
            _ItemDatabase.Load();
            _SkillDatabase.Load();

            AccountManager.Load();
        }
    }
}
