using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;

namespace DnDHelper
{
    /// <summary>
    /// Interaction logic for BattleMemberAddWindow.xaml
    /// </summary>
    public partial class BattleMemberAddWindow : Window
    {
        Battle _battle;
        Helper _helper;

        public BattleMemberAddWindow(Helper helper, Battle battle)
        {
            InitializeComponent();
            _helper = helper;
            _battle = battle;
            listBox1.ItemsSource = _helper.Groups;
            listBox1.DisplayMemberPath = "GroupName";
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                listBox2.ItemsSource = ((CharacterGroup)listBox1.SelectedItem).Members;
                listBox2.DisplayMemberPath = "Name";
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                Character character = (Character)listBox2.SelectedItem;
                character.Initiative = int.Parse(textBox1.Text) + character.CurrentStats.Dexterity;
                _battle.AddMember(character);

                DialogResult = true;
                Close();
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                Character character = (Character)listBox2.SelectedItem;
                character.Initiative = int.Parse(textBox1.Text) + character.CurrentStats.Dexterity;
                using (MemoryStream ms = new MemoryStream())
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(Character));
                    xmlSerializer.Serialize(ms, character);
                    ms.Position = 0;
                    Character newCharacter = (Character)xmlSerializer.Deserialize(ms);
                    _battle.AddMember(newCharacter);
                }
                DialogResult = true;
                Close();
            }
        }
    }
}
