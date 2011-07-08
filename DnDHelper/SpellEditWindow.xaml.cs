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
using System.ComponentModel;

namespace DnDHelper.WPF
{
    /// <summary>
    /// Interaction logic for SpellEditWindow.xaml
    /// </summary>
    public partial class SpellEditWindow : Window
    {

        Helper _helper;
        Character _character;
        public static readonly string [] Types = new string [] { "Mage", "Cleric", "Druid", "Bard" };
        public static readonly string[] Levels = new string[] { "Wszystkie", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        public SpellEditWindow(Helper helper, Character character)
        {
            InitializeComponent();
            _helper = helper;
            _character = character;
            comboBox1.ItemsSource = Types;
            comboBox2.ItemsSource = Levels;
            listView1.ItemsSource = _helper.Spells.OrderBy(s => s.Name);
            listView2.ItemsSource = _character.KnownSpells;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            textBox2.Text = "Nowy czar";
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SpellDefinition sd = _helper.GetSpell(textBox2.Text);
                if (sd == null)
                {
                    sd = new SpellDefinition() { Name = textBox2.Text };
                    _helper.Spells.Add(sd);
                }
                sd.Level = int.Parse(textBox3.Text);
                sd.Shool = textBox4.Text;
                sd.Description = textBox5.Text;
                sd.Types = textBox6.Text;
            }
            catch
            {
                MessageBox.Show("Błąd walidacji");
            }
            listView1.ItemsSource = _helper.Spells.OrderBy(s => s.Name);
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

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            if ((string)comboBox2.SelectedItem == "Wszystkie")
            {
                listView1.ItemsSource = _helper.Spells.Where(f => f.Types.Contains((string)comboBox1.SelectedItem)).OrderBy(o => o.Name);
            }
            else
            {
                listView1.ItemsSource = _helper.Spells.Where(f => f.Level == int.Parse((string)comboBox2.SelectedItem) && f.Types.Contains((string)comboBox1.SelectedItem)).OrderBy(o => o.Name);
            }
        }

       

    }
}
