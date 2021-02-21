using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DarkRift;
using DarkRift.Server;

namespace ProjectPBBGPlugins
{
    [Serializable]
    public class Skill_Mining : Skill
    {
        public Mining_Ore SkillOre;

        public override void Action()
        {
            if (_Account == null)
                return;

            CurrentExperience += SkillOre.Experience;

            Random r = new Random();
            int randomInt = r.Next(0, 100);
            int randomAmount = r.Next(3, 250);

            if (randomInt >= 50)
            {
                Debug.Log("[Skill] " + _Account.Username + " Mined a large cache of " + SkillOre.Ore.Name + "!", ConsoleColor.Cyan);
                SkillOre.Amount = randomAmount;

                PKT_CHATMESSAGE _NewChatMessage = new PKT_CHATMESSAGE(_Account.Username + "<color=magenta> discovered a large cache of " + SkillOre.Ore.Name + "!</color> (x" + SkillOre.Amount + ")");
                foreach (IClient client in AccountManager.ActiveAccounts.Keys)
                    client.SendMessage(Message.Create((ushort)Pkt.PKT_CLIENT_RECEIVECHATMESSAGE, _NewChatMessage), SendMode.Reliable);
            }
            else
            {
                SkillOre.Amount = 1;
                PKT_CHATMESSAGE _NewChatMessage = new PKT_CHATMESSAGE("<color=magenta>" + _Account.Username + " mined a single " + SkillOre.Ore.Name + "!</color> (x" + SkillOre.Amount + ")");
                foreach (IClient client in AccountManager.ActiveAccounts.Keys)
                    client.SendMessage(Message.Create((ushort)Pkt.PKT_CLIENT_RECEIVECHATMESSAGE, _NewChatMessage), SendMode.Reliable);
            }

            _Account.Inventory.AddItem(Database._ItemDatabase.GetItemByName(SkillOre.Ore.Name), SkillOre.Amount);

            Debug.Log("[Skill] " + _Account.Username + " Mined " + SkillOre.Ore.Name + " with a stack size of " + SkillOre.Amount + " Weight: " + randomInt.ToString(), ConsoleColor.Magenta);
        }
    }
}
