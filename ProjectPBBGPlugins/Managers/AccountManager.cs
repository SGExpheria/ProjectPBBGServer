using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DarkRift;
using DarkRift.Client;
using DarkRift.Server;

namespace ProjectPBBGPlugins
{
    [Serializable]
    public static class AccountManager
    {
        public static Dictionary<IClient, Account> ActiveAccounts = new Dictionary<IClient, Account>();
        public static List<Account> Accounts = new List<Account>();

        public static Account GetAccount(string username)
        {
            try {
                Account acc = Accounts.Where(a => a.Username == username).First();
                if (acc != null)
                    Debug.Log("Got Account " + username, ConsoleColor.Green);
                return acc;
            } catch {
                Debug.Log("Failed to get Account " + username, ConsoleColor.DarkRed);
            }
            return null;
        }
        public static void AddAccount(Account acc)
        {
            Accounts.Add(acc);
            Debug.Log("Added account " + acc.Username, ConsoleColor.Green);
        }
        public static void LoginAccount(IClient client, Account acc)
        {
            ActiveAccounts.Add(client, acc);
            Debug.Log(acc.Username + " has logged in.", ConsoleColor.Green);
        }
        public static bool isLoggedIn(string username)
        {
            foreach (Account _account in ActiveAccounts.Values)
            {
                if (_account.Username == username)
                    return true;
            }
            return false;
        }

        public static void SaveAccount(Account acc)
        {
            Accounts.Where(a => a.Username == acc.Username).FirstOrDefault().Save();
        }

        public static void Save()
        {
            Debug.Log("[Account Database] Saving " + Accounts.Count.ToString() + " accounts", DebugColors.SavedColor);
            foreach (Account account in Accounts)
            {
                if (!Directory.Exists(Path.AccountDirectory + account.Username))
                {
                    Directory.CreateDirectory(Path.AccountDirectory + account.Username);
                }
                JSON.Serialize(Path.AccountDirectory + account.Username + @"\Account.json", account);
            }
        }

        public static void Load()
        {
            Debug.Log("[Account Database] Loading " + Directory.GetDirectories(Path.AccountDirectory).Length.ToString() + " accounts", DebugColors.SavedColor);
            foreach (string path in Directory.GetDirectories(Path.AccountDirectory)){
                Account _account = JSON.Deserialize<Account>(path + @"\Account.json");
                Accounts.Add(_account);
                Debug.Log("[Account Database] Loaded account " + _account.Username + " into AccountManager", ConsoleColor.DarkYellow);
            }
        }
    }
}
