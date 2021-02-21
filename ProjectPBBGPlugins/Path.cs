using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPBBGPlugins
{
    public static class Path
    {
        public static string LocalDirectory
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }
        public static string DatabaseDirectory
        {
            get
            {
                if (!Directory.Exists(LocalDirectory + @"Database"))
                {
                    Directory.CreateDirectory(LocalDirectory + @"Database");
                }
                return LocalDirectory + @"Database\";
            }
        }

        public static string CityDatabaseDirectory
        {
            get
            {
                if (!Directory.Exists(DatabaseDirectory + "Cities"))
                {
                    Directory.CreateDirectory(DatabaseDirectory + "Cities");
                }
                return DatabaseDirectory + @"Cities\";
            }
        }

        public static string ItemDatabaseDirectory
        {
            get
            {
                if (!Directory.Exists(DatabaseDirectory + @"ItemDatabase"))
                {
                    Directory.CreateDirectory(DatabaseDirectory + @"ItemDatabase");
                }
                return DatabaseDirectory + @"ItemDatabase\";
            }
        }

        public static string AccountDirectory
        {
            get
            {
                if (!Directory.Exists(DatabaseDirectory + @"Accounts"))
                {
                    Directory.CreateDirectory(DatabaseDirectory + @"Accounts");
                }
                return DatabaseDirectory + @"Accounts\";
            }
        }
    }
}
