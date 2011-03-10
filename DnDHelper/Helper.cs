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

        public Helper()
        {
            Groups = new List<CharacterGroup>();
            Items = new List<Item>();
        }

        public void SaveState()
        {
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(Helper));
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
            return helper;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(null, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
