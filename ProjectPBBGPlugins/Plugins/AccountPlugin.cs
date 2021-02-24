using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DarkRift;
using DarkRift.Server;

namespace ProjectPBBGPlugins
{
    public class AccountPlugin : Plugin
    {
        public override Command[] Commands => new Command[]
        {
            new Command("ban", "Bans account from server", "ban (-username=<string>) (-time=<string>) (-reason=<string>)", BanAccount)
        };
        public override bool ThreadSafe => false;
        public override Version Version => new Version(1, 0, 0);

        public AccountPlugin(PluginLoadData pluginLoadData) : base(pluginLoadData)
        {
            ClientManager.ClientConnected += OnClientConnect;
        }

        public void OnClientConnect(object sender, ClientConnectedEventArgs e)
        {

            e.Client.MessageReceived += OnClientMessageReceived;
        }

        public void OnClientMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            using (Message message = e.GetMessage())
            {
                if (message.Tag == (ushort)Pkt.PKT_CLIENT_ACCOUNTREGISTERINFO)
                {
                    PKT_CLIENT_ACCOUNTREGISTERINFO registerInfo = message.Deserialize<PKT_CLIENT_ACCOUNTREGISTERINFO>();

                    //Check if account already exists
                    if (AccountManager.GetAccount(registerInfo.Username) != null)
                    {
                        PKT_SERVER_ACCOUNTERROR _newError = new PKT_SERVER_ACCOUNTERROR("an account with this username already exists");
                        e.Client.SendMessage(Message.Create((ushort)Pkt.PKT_SERVER_ACCOUNTERROR, _newError), SendMode.Reliable);
                        return;
                    }

                    Account _newAccount = new Account(registerInfo.Username, registerInfo.Password, registerInfo.Email, registerInfo.Referral);
                    _newAccount.ID = AccountManager.Accounts.Count + 1;

                    AccountManager.AddAccount(_newAccount);
                    AccountManager.Save();

                    //Tell them they registered.
                    PKT_SERVER_ACCOUNTSUCCESS _newRegisterMessage = new PKT_SERVER_ACCOUNTSUCCESS("Registered, welcome to Project PBBG <color=white>" + registerInfo.Username + "</color>!");
                    e.Client.SendMessage(Message.Create((ushort)Pkt.PKT_SERVER_ACCOUNTSUCCESS, _newRegisterMessage), SendMode.Reliable);
                }//Register
                if (message.Tag == (ushort)Pkt.PKT_CLIENT_ACCOUNTLOGININFO)
                {
                    PKT_CLIENT_ACCOUNTLOGININFO loginInfo = message.Deserialize<PKT_CLIENT_ACCOUNTLOGININFO>();

                    if (AccountManager.GetAccount(loginInfo.Username) == null) //Account doesnt exist
                    {
                        PKT_SERVER_ACCOUNTERROR _newError = new PKT_SERVER_ACCOUNTERROR("account with this username doesnt exist");
                        e.Client.SendMessage(Message.Create((ushort)Pkt.PKT_SERVER_ACCOUNTERROR, _newError), SendMode.Reliable);
                        return;
                    }

                    if (AccountManager.GetAccount(loginInfo.Username).isBanned) //Account is banned
                    {
                        PKT_SERVER_ACCOUNTERROR _newError = new PKT_SERVER_ACCOUNTERROR("account is banned date: length: reason:");
                        e.Client.SendMessage(Message.Create((ushort)Pkt.PKT_SERVER_ACCOUNTERROR, _newError), SendMode.Reliable);
                        return;
                    }

                    if (AccountManager.isLoggedIn(loginInfo.Username)) //Account is already logged into the game.
                    {
                        PKT_SERVER_ACCOUNTERROR _newError = new PKT_SERVER_ACCOUNTERROR("account already logged in");
                        e.Client.SendMessage(Message.Create((ushort)Pkt.PKT_SERVER_ACCOUNTERROR, _newError), SendMode.Reliable);
                        return;
                    }

                    AccountManager.LoginAccount(e.Client, AccountManager.GetAccount(loginInfo.Username));
                    AccountManager.ActiveAccounts[e.Client].NetID = e.Client;
                    TickManager._Tick += AccountManager.ActiveAccounts[e.Client].Tick;

                    if (AccountManager.GetAccount(loginInfo.Username).isNew)
                    {
                        PKT_SERVER_ACCOUNTLOGINSUCCESS _newLoginSuccessMessage = new PKT_SERVER_ACCOUNTLOGINSUCCESS("Character Creation");
                        e.Client.SendMessage(Message.Create((ushort)Pkt.PKT_SERVER_ACCOUNTLOGINSUCCESS, _newLoginSuccessMessage), SendMode.Reliable);
                    }
                    else
                    {
                        PKT_SERVER_ACCOUNTLOGINSUCCESS _newLoginSuccessMessage = new PKT_SERVER_ACCOUNTLOGINSUCCESS("Game");
                        e.Client.SendMessage(Message.Create((ushort)Pkt.PKT_SERVER_ACCOUNTLOGINSUCCESS, _newLoginSuccessMessage), SendMode.Reliable);
                    }
                }//Login
            }
        }

        void BanAccount(object sender, CommandEventArgs e)
        {
            string username = e.Arguments[0];
            string time = e.Arguments[1];
            string reason = e.Arguments[2];

            try
            {
                AccountManager.GetAccount(username).isBanned = true;
                Debug.Log("Banned account " + username + " -> [" + time + "] -> [" + reason + "]", ConsoleColor.Red);
            }
            catch
            {
                Debug.Log("Could not ban " + username + " (Reason: username does not exist)", ConsoleColor.Red);
            }
        }
    }
}
