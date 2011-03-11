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

namespace DnDHelper
{
    /// <summary>
    /// Interaction logic for SpellEditWindow.xaml
    /// </summary>
    public partial class SpellEditWindow : Window
    {
        Helper _helper;
        Character _character;
        public static readonly string [] Types = new string [] { "Mage", "Cleric", "Druid", "Bard" };

        public SpellEditWindow(Helper helper, Character character)
        {
            InitializeComponent();
            _helper = helper;
            _character = character;
            listView1.ItemsSource = _helper.Spells;
            listView2.ItemsSource = _character.KnownSpells;
            comboBox1.ItemsSource = comboBox2.ItemsSource = Types;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            SpellDefinition newSpell = new SpellDefinition() { Name = "Nowy czar" };
            _helper.Spells.Add(newSpell);
            ContentGrid.DataContext = newSpell;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            listView1.Items.Refresh();
        }

        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView1.SelectedItem != null)
            {
                SpellDefinition spDef = (SpellDefinition)listView1.SelectedItem;
                ContentGrid.DataContext = spDef;
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (listView1.SelectedItem != null)
            {
                SpellDefinition spDef = (SpellDefinition)listView1.SelectedItem;
                _helper.Spells.Remove(spDef);
                listView1.Items.Refresh();
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            if (listView1.SelectedItem != null)
            {
                SpellDefinition spDef = (SpellDefinition)listView1.SelectedItem;
                _character.KnownSpells.Add(spDef);
                listView2.Items.Refresh();
            }
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            if (listView2.SelectedItem != null)
            {
                SpellDefinition spDef = (SpellDefinition)listView2.SelectedItem;
                _character.KnownSpells.Remove(spDef);
                listView2.Items.Refresh();
            }
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
