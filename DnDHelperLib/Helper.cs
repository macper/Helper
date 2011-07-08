using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml;
using System.Runtime.Serialization;
using System.IO;

namespace DnDHelper
{
    [Serializable]
    public class Helper : INotifyPropertyChanged
    {
        private DateTime _currentTime = new DateTime(1300, 5, 13);
        public DateTime CurrentTime { get { return _currentTime; } set { _currentTime = value; OnPropertyChanged("CurrentTime"); } }
        public List<CharacterGroup> Groups;
        public List<Item> Items;
        public List<SpellDefinition> Spells;
        public List<Effect> Effects;
        public string Notes { get; set; }
        public int XP { get; set; }
        public int Version { get; set; }

        public Helper()
        {
            Groups = new List<CharacterGroup>();
            Items = new List<Item>();
            Effects = new List<Effect>();
        }

        public SpellDefinition GetSpell(string name)
        {
            return Spells.SingleOrDefault(s => s.Name == name);
        }

        public void SaveState()
        {
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(Helper));
            foreach (CharacterGroup chGr in Groups)
            {
                foreach (Character ch in chGr.Members)
                {
                    ch.SerializeSelf();
                }
            }
            using (FileStream fs = new FileStream("Helper.xml", FileMode.Create))
            {
                xmlSerializer.Serialize(fs, this);
            }
        }

        public static Helper LoadState()
        {
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(Helper));
            Helper helper;
            using (FileStream fs = new FileStream("Helper.xml", FileMode.Open))
            {
                helper = (Helper)xmlSerializer.Deserialize(fs);
            }
            foreach (CharacterGroup chGr in helper.Groups)
            {
                foreach (Character ch in chGr.Members)
                {
                    ch.DeserializeSelf(helper);
                }
            }
            return helper;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(null, new PropertyChangedEventArgs(propertyName));
            }
        }

        public bool GetVersionFromServer()
        {
            FTPVersionServer.VersionServer server = GetVersionServer();
            var versions = server.GetAllAppVersions();
            List<int> list = new List<int>();
            foreach (string vs in versions)
            {
                try
                {
                    list.Add(int.Parse(vs));
                }
                catch
                {
                }
            }
            list.Sort();
            list.Reverse();
            int latestVersion = list[0];
            if (latestVersion > Version)
            {
                server.GetFile("Helper.xml", latestVersion.ToString(), "Helper.xml");
                return true;
            }
            return false;
        }

        public void PutVersionOnServer()
        {
            FTPVersionServer.VersionServer server = GetVersionServer();
            Version++;
            SaveState();
            server.PutFile("Helper.xml", Version.ToString());
        }

        private FTPVersionServer.VersionServer GetVersionServer()
        {
            System.Collections.Specialized.NameValueCollection appSett = System.Configuration.ConfigurationManager.AppSettings;
            FTPVersionServer.VersionServer server = new FTPVersionServer.VersionServer(appSett["FTPServer"], appSett["FTPUser"], appSett["FTPPassword"], appSett["FTPAppDir"]);
            return server;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
