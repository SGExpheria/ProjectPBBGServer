using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DarkRift;
using DarkRift.Server;

namespace ProjectPBBGPlugins.Plugins
{
    public class ChatPlugin : Plugin
    {
        public override bool ThreadSafe => false;
        public override Version Version => new Version(1, 0, 0);

        public ChatPlugin(PluginLoadData pluginLoadData) : base(pluginLoadData)
        {
            ClientManager.ClientConnected += OnClientConnect;
        }

        public void OnClientConnect(object sender, ClientConnectedEventArgs e)
        {

            e.Client.MessageReceived += OnClientMessageReceived;
        }

        public void OnClientMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            if (e.GetMessage().Tag == (ushort)Pkt.PKT_CLIENT_SENDCHATMESSAGE)
            {
                PKT_CHATMESSAGE _ChatMesage = e.GetMessage().Deserialize<PKT_CHATMESSAGE>();
                Account _MessageSender = AccountManager.ActiveAccounts[e.Client];

                string _RawMessageText = _MessageSender.Username + ": " + _ChatMesage.Message;
                if (_MessageSender.isDev)
                {
                    _RawMessageText = "<color=magenta>[DEV]</color> " + _MessageSender.Username + ": " + _ChatMesage.Message;
                } ChatManager.ChatMessages.Add(_RawMessageText);

                PKT_CHATMESSAGE _NewChatMessage = new PKT_CHATMESSAGE(_RawMessageText);
                foreach (IClient client in ClientManager.GetAllClients())
                    client.SendMessage(Message.Create((ushort)Pkt.PKT_CLIENT_RECEIVECHATMESSAGE, _NewChatMessage), SendMode.Reliable);
            }
        }
    }
}
