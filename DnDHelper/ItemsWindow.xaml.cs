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
    /// Interaction logic for ItemsWindow.xaml
    /// </summary>
    public partial class ItemsWindow : Window
    {
        Helper _helper;
        int? _local;
        Character _character;
        static string[] _baseTypes = Enum.GetNames(typeof(BaseTypes));

        public ItemsWindow(Helper help)
        {
            InitializeComponent();
            _helper = help;
            listView1.ItemsSource = _helper.Items;
            comboBox1.ItemsSource = new string[] { "Broń", "Zbroja" };
            comboBox2.ItemsSource = _baseTypes;
            comboBox3.ItemsSource = _baseTypes;
        }

        public ItemsWindow(Helper help, int? local, Character ch)
        {
            InitializeComponent();
            _helper = help;
            listView1.ItemsSource = _helper.Items;
            comboBox1.ItemsSource = new string[] { "Broń", "Zbroja" };
            comboBox2.ItemsSource = _baseTypes;
            comboBox3.ItemsSource = _baseTypes;
            _local = local;
            _character = ch;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Text = "Nowy przedmiot";
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Item newIt = _helper.Items.FirstOrDefault(el => el.Name == textBox1.Text);
            if (newIt == null)
            {
                newIt = new Item();
                _helper.Items.Add(newIt);
            }
            
            try
            {
                if (comboBox1.SelectedIndex == 0)
                {
                    newIt.Damage = textBox3.Text.ToUpper();
                }
                else
                {
                    newIt.AC = int.Parse(textBox4.Text);
                    newIt.MaxDexterityBonus = int.Parse(textBox5.Text);
                    newIt.Panalty = int.Parse(textBox6.Text);
                }
                newIt.BaseType = (string)comboBox2.SelectedItem;
                newIt.Name = textBox1.Text;
                newIt.Cost = int.Parse(textBox2.Text);
                newIt.Specials = textBox7.Text;
                listView1.Items.Refresh();
            }
            catch
            {
                MessageBox.Show("Błąd walidacji");
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Wybierz coś");
                return;
            }
            _helper.Items.Remove(listView1.SelectedItem as Item);
            listView1.Items.Refresh();
        }

        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Item it = (Item)listView1.SelectedItem;
                comboBox1.SelectedItem = it.Type;
                comboBox2.SelectedItem = it.BaseType;
                textBox1.Text = it.Name;
                textBox2.Text = it.Cost.ToString();
                textBox3.Text = it.Damage;
                textBox4.Text = it.AC.ToString();
                textBox5.Text = it.MaxDexterityBonus.ToString();
                textBox6.Text = it.Panalty.ToString();
                textBox7.Text = it.Specials;
                comboBox1.SelectedValue = it.Type;
            }
            catch
            {
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            if (_local != null)
            {
                Item item = null;
                if (listView1.SelectedItem != null)
                {
                    item = (Item)listView1.SelectedItem;
                }
                
                DialogResult = true;
                if (_local == 1)
                {
                    _character.RightHand = item;
                }
                else if (_local == 2)
                {
                    _character.LeftHand = item;
                }
                else
                {
                    _character.Torso = item;
                }
            }
            Close();
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox3.SelectedItem != null)
            {
                listView1.ItemsSource = _helper.Items.Where(f => f.BaseType == (string)comboBox3.SelectedItem).OrderBy(o => o.Name);
            }
        }
    }
}
