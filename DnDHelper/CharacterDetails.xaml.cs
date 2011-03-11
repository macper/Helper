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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DnDHelper
{
    /// <summary>
    /// Interaction logic for CharacterDetails.xaml
    /// </summary>
    public partial class CharacterDetails : UserControl
    {
        private Character _character;
        private Helper _helper;

        public CharacterDetails()
        {
            InitializeComponent();
        }

        public void Init(Helper help, Character chr)
        {
            _character = chr;
            _helper = help;
            listView1.ItemsSource = _character.Attacks;
            listView2.ItemsSource = _character.Effects;
            listView3.ItemsSource = _character.KnownSpells;
            listView4.ItemsSource = _character.AvailableCastings;
            listView6.ItemsSource = _character.Spells.Where(f => f.IsCasted == false).OrderByDescending(k => k.Definition.Level);
            comboBox2.ItemsSource = _character.AvailableCastings;
            comboBox1.ItemsSource = SpellEditWindow.Types;
            ContentGrid.DataContext = _character;
        }

        

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ItemsWindow itWnd = new ItemsWindow(_helper);
            itWnd.Show();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            listView1.Items.Refresh();
        }

        private void DodajAtak_Click(object sender, RoutedEventArgs e)
        {
            Attack attack = new Attack();
            _character.Attacks.Add(attack);
            AttackEditWindow wnd = new AttackEditWindow(attack);
            if (wnd.ShowDialog() == true)
            {
                Refresh();
            }
        }

        private void EdytujAtak_Click(object sender, RoutedEventArgs e)
        {
            if (listView1.SelectedItem == null)
            {
                MessageBox.Show("Wybierz jakiś atak żeby go edytować");
                return;
            }
            Attack attack = (Attack)listView1.SelectedItem;
            AttackEditWindow wnd = new AttackEditWindow(attack);
            if (wnd.ShowDialog() == true)
            {
                Refresh();
            }
        }

        private void UsunAtak_Click(object sender, RoutedEventArgs e)
        {
            if (listView1.SelectedItem == null)
            {
                MessageBox.Show("Wybierz jakiś atak żeby go usunąć");
                return;
            }
            _character.Attacks.Remove((Attack)listView1.SelectedItem);
            Refresh();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            ItemsWindow itWnd = new ItemsWindow(_helper, 1, _character);
            if (itWnd.ShowDialog() == true)
            {
                textBox2.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            ItemsWindow itWnd = new ItemsWindow(_helper, 2, _character);
            if (itWnd.ShowDialog() == true)
            {
                textBox3.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            }
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            ItemsWindow itWnd = new ItemsWindow(_helper, 3, _character);
            if (itWnd.ShowDialog() == true)
            {
                textBox4.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
            }
        }

        private void DodajEfekt_Click(object sender, RoutedEventArgs e)
        {
            Effect newEfekt = new DnDHelper.Effect();
            EffectEditWindow efWnd = new EffectEditWindow(newEfekt);
            if (efWnd.ShowDialog() == true)
            {
                _character.Effects.Add(newEfekt);
                listView2.Items.Refresh();
            }
        }

        private void EdytujEfekt_Click(object sender, RoutedEventArgs e)
        {
            if (listView2.SelectedItem == null)
            {
                MessageBox.Show("Coś byś se wybrał najpierw");
                return;
            }
            DnDHelper.Effect efekt = (DnDHelper.Effect)listView2.SelectedItem;
            EffectEditWindow efWnd = new EffectEditWindow(efekt);
            if (efWnd.ShowDialog() == true)
            {
                listView2.Items.Refresh();
            }
        }

        private void UsunEfekt_Click(object sender, RoutedEventArgs e)
        {
            if (listView2.SelectedItem == null)
            {
                MessageBox.Show("Coś trza wybrać, nie ma rady");
                return;
            }
            DnDHelper.Effect efekt = (DnDHelper.Effect)listView2.SelectedItem;
            _character.Effects.Remove(efekt);
            listView2.Items.Refresh();
        }

        private void DodajCzar_Click(object sender, RoutedEventArgs e)
        {
            SpellEditWindow spellWnd = new SpellEditWindow(_helper, _character);
            if (spellWnd.ShowDialog() == true)
            {
                listView3.Items.Refresh();
            }
        }

        private void listView3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView3.SelectedItem != null)
            {
                textBox5.Text = listView3.SelectedItem.ToString();
            }
        }

        private void DodajCzarPoziom_Click(object sender, RoutedEventArgs e)
        {
            SpellCasting sCast = new SpellCasting();
            _character.AvailableCastings.Add(sCast);
            listView4.Items.Refresh();
        }

        private void listView4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView4.SelectedItem != null)
            {
                CzaryPoziomContent.DataContext = listView4.SelectedItem;
            }
        }

        private void UsunCzarPoziom_Click(object sender, RoutedEventArgs e)
        {
            if (listView4.SelectedItem != null)
            {
                _character.AvailableCastings.Remove((SpellCasting)listView4.SelectedItem);
                listView4.Items.Refresh();
            }
        }

        private void comboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                SpellCasting sCast = (SpellCasting)comboBox2.SelectedItem;
                var spells = _character.KnownSpells.Where(f => f.Type == sCast.Type && f.Level == sCast.Level);
                comboBox3.DataContext = spells;
                comboBox3.ItemsSource = spells;
                comboBox3.DisplayMemberPath = "Name";
                listView5.ItemsSource = _character.Spells.Where(f => f.Definition.Type == sCast.Type && f.Definition.Level == sCast.Level);
            }
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                SpellCasting sCast = (SpellCasting)comboBox2.SelectedItem;
                var spells = _character.Spells.Where(f => f.Definition.Type == sCast.Type && f.Definition.Level == sCast.Level);
                if (spells.Count() == sCast.Count)
                {
                    MessageBox.Show("Osiągnięo limit czarów");
                    return;
                }
                if (comboBox3.SelectedItem == null)
                {
                    MessageBox.Show("Wybierz czar");
                    return;
                }
                Spell spell = new Spell() { Definition = (SpellDefinition)comboBox3.SelectedItem };
                _character.Spells.Add(spell);
                listView5.ItemsSource = _character.Spells.Where(f => f.Definition.Type == sCast.Type && f.Definition.Level == sCast.Level);
                listView5.Items.Refresh();
            }
        }

        private void UsunCzarPrzygotowany_Click(object sender, RoutedEventArgs e)
        {
            if (listView5.SelectedItem != null)
            {
                _character.Spells.Remove((Spell)listView5.SelectedItem);
                SpellCasting sCast = (SpellCasting)comboBox2.SelectedItem;
                listView5.ItemsSource = _character.Spells.Where(f => f.Definition.Type == sCast.Type && f.Definition.Level == sCast.Level);
                listView5.Items.Refresh();
            }
        }

        private void CzarRzuc_Click(object sender, RoutedEventArgs e)
        {
            if (listView6.SelectedItem != null)
            {
                ((Spell)listView6.SelectedItem).IsCasted = true;
                listView6.ItemsSource = _character.Spells.Where(f => f.IsCasted == false).OrderByDescending(k => k.Definition.Level);
            }
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            _character.CurrentStats.HP = _character.OriginalStats.HP;
            foreach (Spell spell in _character.Spells)
            {
                spell.IsCasted = false;
            }
            Init(_helper, _character);
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            _character.CurrentStats.AC = _character.OriginalStats.AC;
            _character.CurrentStats.AttackSkill = _character.OriginalStats.AttackSkill;
            _character.CurrentStats.Constitution = _character.OriginalStats.Constitution;
            _character.CurrentStats.Dexterity = _character.OriginalStats.Dexterity;
            _character.CurrentStats.Inteligence = _character.OriginalStats.Inteligence;
            _character.CurrentStats.ReflexThrow = _character.OriginalStats.ReflexThrow;
            _character.CurrentStats.Strength = _character.OriginalStats.Strength;
            _character.CurrentStats.StrongThrow = _character.OriginalStats.StrongThrow;
            _character.CurrentStats.WillThrow = _character.OriginalStats.WillThrow;
            _character.CurrentStats.Wisdom = _character.OriginalStats.Wisdom;
            Init(_helper, _character);
        }


        
    }
}
