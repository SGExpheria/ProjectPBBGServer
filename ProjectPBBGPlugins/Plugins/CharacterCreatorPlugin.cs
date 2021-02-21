using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DarkRift;
using DarkRift.Server;

namespace ProjectPBBGPlugins
{
    public class CharacterCreatorPlugin : Plugin
    {
        public override bool ThreadSafe => false;
        public override Version Version => new Version(1, 0, 0);

        public CharacterCreatorPlugin(PluginLoadData pluginLoadData) : base(pluginLoadData)
        {
            ClientManager.ClientConnected += OnClientConnect;
        }
        public void OnClientConnect(object sender, ClientConnectedEventArgs e)
        {
            e.Client.MessageReceived += OnClientMessageReceived;
        }

        public void OnClientMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            if (e.GetMessage().Tag == (ushort)Pkt.PKT_CLIENT_REQUESTCHARACTERCLASSDATA)
            {
                PKT_SERVER_SENDCHARACTERCLASSDATA _SendClassData = new PKT_SERVER_SENDCHARACTERCLASSDATA(Database._ClassDatabase._ClassDatabase);
                e.Client.SendMessage(Message.Create((ushort)Pkt.PKT_SERVER_SENDCHARACTERCLASSDATA, _SendClassData), SendMode.Reliable);
            }
            if (e.GetMessage().Tag == (ushort)Pkt.PKT_CLIENT_SENDCHARACTERCREATEDATA)
            {
                PKT_CLIENT_SENDCHARACTERCREATEDATA _CharacterCreateData = e.GetMessage().Deserialize<PKT_CLIENT_SENDCHARACTERCREATEDATA>();
                AccountManager.ActiveAccounts[e.Client].Class.ID = Database._ClassDatabase.GetClassByID(_CharacterCreateData.ClassID).ID;
                AccountManager.ActiveAccounts[e.Client].Class.Name = Database._ClassDatabase.GetClassByID(_CharacterCreateData.ClassID).Name;
                AccountManager.ActiveAccounts[e.Client].Class.Description = Database._ClassDatabase.GetClassByID(_CharacterCreateData.ClassID).Description;
                AccountManager.ActiveAccounts[e.Client].isNew = false;
                AccountManager.ActiveAccounts[e.Client].Save();

                PKT_SERVER_ACCOUNTLOGINSUCCESS _newLoginSuccessMessage = new PKT_SERVER_ACCOUNTLOGINSUCCESS("Game");
                e.Client.SendMessage(Message.Create((ushort)Pkt.PKT_SERVER_ACCOUNTLOGINSUCCESS, _newLoginSuccessMessage), SendMode.Reliable);
            }
        }

    }
}
