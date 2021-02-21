using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DarkRift;
using DarkRift.Server;

namespace ProjectPBBGPlugins
{
    public enum Pkt
    {
        PKT_SERVER_SENDVAR = 1,
        PKT_SERVER_RECEIVEVAR,
        PKT_CLIENT_SENDVAR,
        PKT_CLIENT_RECEIVEVAR,

        PKT_SERVER_SENDNOTIFICATION = 50,
        PKT_CLIENT_RECEIVENOTIFICATION,

        PKT_SERVER_ACCOUNTERROR = 100,
        PKT_SERVER_ACCOUNTSUCCESS,
        PKT_CLIENT_ACCOUNTREGISTERINFO,
        PKT_CLIENT_ACCOUNTLOGININFO,
        PKT_SERVER_ACCOUNTLOGINSUCCESS,
        PKT_CLIENT_REQUESTCHARACTERCLASSDATA,

        PKT_SERVER_SENDCHARACTERCLASSDATA,
        PKT_CLIENT_SENDCHARACTERCREATEDATA,

        PKT_SERVER_SENDCHATMESSAGE = 200,
        PKT_CLIENT_RECEIVECHATMESSAGE,
        PKT_CLIENT_SENDCHATMESSAGE,

        PKT_DATABASE_REQUESTITEMDATABASE = 300
    }

    public class PKT_DATABASE_REQUESTITEMDATABASE : IDarkRiftSerializable
    {
        public int _ItemDatabaseSize = 0;
        public List<Item> _ItemDatabase;
        public void Deserialize(DeserializeEvent e)
        {
            _ItemDatabaseSize = e.Reader.ReadInt32();
            while (e.Reader.Position < e.Reader.Length)
            {
                Item _newItem = new Item();
                _newItem.ID = e.Reader.ReadInt32();
                _newItem.Name = e.Reader.ReadString();
                _newItem.ApplicableType = e.Reader.ReadString();
                _ItemDatabase.Add(_newItem);
            }
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(_ItemDatabase.Count);
            foreach (Item _item in _ItemDatabase)
            {
                e.Writer.Write(_item.ID);
                e.Writer.Write(_item.Name);
                e.Writer.Write(_item.ApplicableType);
            }
        }

        public PKT_DATABASE_REQUESTITEMDATABASE() { }
        public PKT_DATABASE_REQUESTITEMDATABASE(List<Item> _Database)
        {
            _ItemDatabase = _Database;
        }
    }

    public class PKT_CHATMESSAGE : IDarkRiftSerializable
    {
        public string Message;
        public void Deserialize(DeserializeEvent e)
        {
            Message = e.Reader.ReadString();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Message);
        }

        public PKT_CHATMESSAGE() { }
        public PKT_CHATMESSAGE(string message)
        {
            Message = message;
        }
    }

    public class PKT_CLIENT_SENDCHARACTERCREATEDATA : IDarkRiftSerializable
    {
        public int ClassID;
        public void Deserialize(DeserializeEvent e)
        {
            ClassID = e.Reader.ReadInt32();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(ClassID);
        }

        public PKT_CLIENT_SENDCHARACTERCREATEDATA() { }
        public PKT_CLIENT_SENDCHARACTERCREATEDATA(int id)
        {
            ClassID = id;
        }
    }

    public class PKT_SERVER_SENDCHARACTERCLASSDATA : IDarkRiftSerializable
    {
        public List<CharacterClass> Classes;
        public void Deserialize(DeserializeEvent e)
        {
            while (e.Reader.Position < e.Reader.Length)
            {
                int id = e.Reader.ReadInt32();
                string name = e.Reader.ReadString();
                string description = e.Reader.ReadString();

                CharacterClass newClass = new CharacterClass(name, description);
                newClass.ID = id;

                Classes.Add(newClass);
            }
        }

        public void Serialize(SerializeEvent e)
        {
            foreach (CharacterClass Class in Classes)
            {
                e.Writer.Write(Class.ID);
                e.Writer.Write(Class.Name);
                e.Writer.Write(Class.Description);
            }
        }

        public PKT_SERVER_SENDCHARACTERCLASSDATA() { }
        public PKT_SERVER_SENDCHARACTERCLASSDATA(List<CharacterClass> classes)
        {
            Classes = classes;
        }
    }

    public class PKT_SERVER_ACCOUNTLOGINSUCCESS : IDarkRiftSerializable
    {
        public string MenuName;
        public void Deserialize(DeserializeEvent e)
        {
            MenuName = e.Reader.ReadString();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(MenuName);
        }

        public PKT_SERVER_ACCOUNTLOGINSUCCESS() { }
        public PKT_SERVER_ACCOUNTLOGINSUCCESS(string menuname)
        {
            MenuName = menuname;
        }
    }

    public class PKT_SERVER_ACCOUNTERROR : IDarkRiftSerializable
    {
        public string ErrorText;
        public void Deserialize(DeserializeEvent e)
        {
            ErrorText = e.Reader.ReadString();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(ErrorText);
        }

        public PKT_SERVER_ACCOUNTERROR() { }
        public PKT_SERVER_ACCOUNTERROR(string _ErrorText)
        {
            ErrorText = _ErrorText;
        }
    }

    public class PKT_SERVER_ACCOUNTSUCCESS : IDarkRiftSerializable
    {
        public string SuccessText;

        public void Deserialize(DeserializeEvent e)
        {
            SuccessText = e.Reader.ReadString();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(SuccessText);
        }

        public PKT_SERVER_ACCOUNTSUCCESS()
        {
        }

        public PKT_SERVER_ACCOUNTSUCCESS(string _SuccessText)
        {
            SuccessText = _SuccessText;
        }
    }

    public class PKT_CLIENT_ACCOUNTREGISTERINFO : IDarkRiftSerializable
    {
        public string Username;
        public string Password;
        public string Email;
        public string Referral;

        public void Deserialize(DeserializeEvent e)
        {
            Username = e.Reader.ReadString();
            Password = e.Reader.ReadString();
            Email = e.Reader.ReadString();
            Referral = e.Reader.ReadString();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Username);
            e.Writer.Write(Password);
            e.Writer.Write(Email);
            e.Writer.Write(Referral);
        }

        public PKT_CLIENT_ACCOUNTREGISTERINFO() { }
        public PKT_CLIENT_ACCOUNTREGISTERINFO(string username, string password, string email, string referral)
        {
            Username = username;
            Password = password;
            Email = email;
            Referral = referral;
        }
    }
    public class PKT_CLIENT_ACCOUNTLOGININFO : IDarkRiftSerializable
    {
        public string Username;
        public string Password;

        public void Deserialize(DeserializeEvent e)
        {
            Username = e.Reader.ReadString();
            Password = e.Reader.ReadString();
        }

        public void Serialize(SerializeEvent e)
        {
            e.Writer.Write(Username);
            e.Writer.Write(Password);
        }

        public PKT_CLIENT_ACCOUNTLOGININFO() { }
        public PKT_CLIENT_ACCOUNTLOGININFO(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }

}
