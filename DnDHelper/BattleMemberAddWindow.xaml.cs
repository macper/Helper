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
            listBox3.ItemsSource = _battle.Members;
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
                try
                {
                    if (_battle.Members.Contains(character))
                    {
                        MessageBox.Show("Ten członek został już dodany:)");
                        return;
                    }
                    character.IsActiveMember = false;
                    character.Initiative = int.Parse(textBox1.Text) + Rules.GetStandardBonus(character.CurrentStats.Dexterity);
                    _battle.AddMember(character);
                    listBox3.Items.Refresh();
                    
                }
                catch
                {
                    MessageBox.Show("Wartość rzutu inicjatywy nieprawidłowa!");
                }
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                Character character = (Character)listBox2.SelectedItem;
                try
                {
                    
                    using (MemoryStream ms = new MemoryStream())
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Character));
                        xmlSerializer.Serialize(ms, character);
                        ms.Position = 0;
                        Character newCharacter = (Character)xmlSerializer.Deserialize(ms);
                        newCharacter.Name = textBox2.Text;
                        if (_battle.Members.FirstOrDefault(f => f.Name == newCharacter.Name) != null)
                        {
                            int counter = 2;
                            IEnumerable<Character> lst = _battle.Members.Where(f => f.Name.Contains('_'));
                            if (lst.Count() > 0)
                            {
                                string[] parts = lst.OrderByDescending(o => o.Name).ToArray().First().Name.Split('_');
                                if (parts.Length > 1)
                                {
                                    try
                                    {
                                        int number = int.Parse(parts[1]);
                                        counter = ++number;
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                            newCharacter.Name += "_" + counter.ToString();
                        }
                        newCharacter.Initiative = int.Parse(textBox1.Text) + Rules.GetStandardBonus(newCharacter.CurrentStats.Dexterity);
                        _battle.AddMember(newCharacter);
                        listBox3.Items.Refresh();
                    }
                }
                catch
                {
                    MessageBox.Show("Nieprawidłowa inicjatywa!");
                }
            }
        }

        private void listBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                textBox2.Text = ((Character)listBox2.SelectedItem).Name;
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
