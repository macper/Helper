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
            try
            {
                _character = chr;
                _helper = help;
                listView1.ItemsSource = _character.Attacks;
                listView2.ItemsSource = _character.Effects;
                listView3.ItemsSource = _character.KnownSpells;
                listView4.ItemsSource = _character.AvailableCastings;
                listView6.ItemsSource = _character.Spells.Where(f => f.IsCasted == false).OrderByDescending(k => k.Definition.Level);
                listView7.ItemsSource = _character.Skills;
                comboBox2.ItemsSource = _character.AvailableCastings;
                comboBox1.ItemsSource = SpellEditWindow.Types;
                comboBox4.ItemsSource = Rules.SkillsDefinition;
                comboBox4.DisplayMemberPath = "Name";
                ContentGrid.DataContext = _character;
                textBox18.Text = _helper.CurrentTime.Date.AddDays(-7).ToString();
                if (_character.ImagePath != null)
                {
                    image1.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + _character.ImagePath));
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Inicjalizacja się nie powiodła:" + exc.ToString());
            }
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
            EffectEditWindow efWnd = new EffectEditWindow(newEfekt, _helper);
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
            EffectEditWindow efWnd = new EffectEditWindow(efekt, _helper);
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
            if (_character.AvailableCastings == null)
            {
                _character.AvailableCastings = new List<SpellCasting>();
            }
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
                var spells = _character.KnownSpells.Where(f => f.Types_Array.Contains(sCast.Type) && f.Level == sCast.Level);
                comboBox3.DataContext = spells;
                comboBox3.ItemsSource = spells;
                comboBox3.DisplayMemberPath = "Name";
                listView5.ItemsSource = _character.Spells.Where(f => f.Definition.Types_Array.Contains(sCast.Type) && f.Definition.Level == sCast.Level);
            }
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                SpellCasting sCast = (SpellCasting)comboBox2.SelectedItem;
                var spells = _character.Spells.Where(f => f.Definition.Types_Array.Contains(sCast.Type) && f.Definition.Level == sCast.Level);
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
                listView5.ItemsSource = _character.Spells.Where(f => f.Definition.Types_Array.Contains(sCast.Type) && f.Definition.Level == sCast.Level);
                listView5.Items.Refresh();
            }
        }

        private void UsunCzarPrzygotowany_Click(object sender, RoutedEventArgs e)
        {
            if (listView5.SelectedItem != null)
            {
                _character.Spells.Remove((Spell)listView5.SelectedItem);
                SpellCasting sCast = (SpellCasting)comboBox2.SelectedItem;
                listView5.ItemsSource = _character.Spells.Where(f => f.Definition.Types_Array.Contains(sCast.Type) && f.Definition.Level == sCast.Level);
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
            RefreshCharacter();
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
            RefreshCharacter();
        }

        private void AddSkill_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox4.SelectedItem != null)
            {
                Skill skill = new Skill() { Name = ((SkillDefinition)comboBox4.SelectedItem).Name };
                switch (((SkillDefinition)comboBox4.SelectedItem).BonusProperty)
                {
                    case BaseAttribute.Charisma:
                        skill.BonusValue += Rules.GetStandardBonus(_character.CurrentStats.Charisma);
                        break;

                    case BaseAttribute.Constitution:
                        skill.BonusValue += Rules.GetStandardBonus(_character.CurrentStats.Constitution);
                        break;

                    case BaseAttribute.Dexterity:
                        skill.BonusValue += Rules.GetStandardBonus(_character.CurrentStats.Dexterity);
                        break;

                    case BaseAttribute.Inteligence:
                        skill.BonusValue += Rules.GetStandardBonus(_character.CurrentStats.Inteligence);
                        break;

                    case BaseAttribute.Strength:
                        skill.BonusValue += Rules.GetStandardBonus(_character.CurrentStats.Strength);
                        break;

                    case BaseAttribute.Wisdom:
                        skill.BonusValue += Rules.GetStandardBonus(_character.CurrentStats.WillThrow);
                        break;
                }
                _character.Skills.Add(skill);
                listView7.Items.Refresh();
            }
        }

        private void listView7_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView7.SelectedItem != null)
            {
                SkillContent.DataContext = listView7.SelectedItem;
                comboBox4.SelectedItem = Rules.SkillsDefinition.First(f => f.Name == ((Skill)listView7.SelectedItem).Name);
                textBox17.Text = Rules.SkillsDefinition.First(f => f.Name == ((Skill)listView7.SelectedItem).Name).Description;
            }
        }

        private void RemoveSkill_Click(object sender, RoutedEventArgs e)
        {
            if (listView7.SelectedItem != null)
            {
                _character.Skills.Remove((Skill)listView7.SelectedItem);
            }
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            if (Rules.RaceTable.ContainsKey(tbRace.Text) && Rules.ClassTable.ContainsKey(tbClass.Text))
            {
                int level;
                if (!int.TryParse(tbLevel.Text, out level))
                {
                    MessageBox.Show("Nieprawidłowy poziom");
                    return;
                }

                _character.OriginalStats.AttackSkill = Rules.ClassTable[tbClass.Text].AttackPerLevel[level];
                _character.OriginalStats.StrongThrow = Rules.ClassTable[tbClass.Text].ThrowPerLevel[level].EnduranceThrow;
                _character.OriginalStats.ReflexThrow = Rules.ClassTable[tbClass.Text].ThrowPerLevel[level].ReflexThrow;
                _character.OriginalStats.WillThrow = Rules.ClassTable[tbClass.Text].ThrowPerLevel[level].WillThrow;
                _character.OriginalStats.HP = (int)((Rules.ClassTable[tbClass.Text].PW / 2) * level) + (int)(Rules.ClassTable[tbClass.Text].PW / 2);

                    foreach (BaseAttribute key in Rules.RaceTable[tbRace.Text].Bonuses.Keys)
                    {
                        switch (key)
                        {
                            case BaseAttribute.Strength:
                                _character.OriginalStats.Strength += Rules.RaceTable[tbRace.Text].Bonuses[BaseAttribute.Strength];
                                break;

                            case BaseAttribute.Dexterity:
                                _character.OriginalStats.Dexterity += Rules.RaceTable[tbRace.Text].Bonuses[BaseAttribute.Dexterity];
                                break;

                            case BaseAttribute.Constitution:
                                _character.OriginalStats.Constitution += Rules.RaceTable[tbRace.Text].Bonuses[BaseAttribute.Constitution];
                                break;

                            case BaseAttribute.Inteligence:
                                _character.OriginalStats.Inteligence += Rules.RaceTable[tbRace.Text].Bonuses[BaseAttribute.Inteligence];
                                break;

                            case BaseAttribute.Wisdom:
                                _character.OriginalStats.Wisdom += Rules.RaceTable[tbRace.Text].Bonuses[BaseAttribute.Wisdom];
                                break;
                        }
                    }
                    _character.OriginalStats.ReflexThrow += Rules.GetStandardBonus(_character.OriginalStats.Dexterity);
                    _character.OriginalStats.StrongThrow += Rules.GetStandardBonus(_character.OriginalStats.Constitution);
                    _character.OriginalStats.HP += level * Rules.GetStandardBonus(_character.OriginalStats.Constitution);
                    _character.OriginalStats.WillThrow += Rules.GetStandardBonus(_character.OriginalStats.Wisdom);

                Class cl = Rules.ClassTable[tbClass.Text];
                _character.AvailableCastings = new List<SpellCasting>();
                if (cl.SpellsPerLevel != null)
                {
                    foreach (SpellCasting sc in cl.SpellsPerLevel[level])
                    {
                        _character.AvailableCastings.Add(sc);
                    }
                }
                
                MessageBox.Show("Policzono");
                RefreshCharacter();
            }
            MessageBox.Show("Nie udało się obliczyć - nie znaleziono rasy albo klasy");
        }

        private void button10_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int baseKP = 10;
                int panalties = 0;
                int maxBonus = int.MaxValue;
                if (_character.LeftHand != null && _character.LeftHand.Type == "Zbroja")
                {
                    baseKP += _character.LeftHand.AC;
                    panalties += _character.LeftHand.Panalty;
                    if (_character.LeftHand.MaxDexterityBonus < maxBonus)
                    {
                        maxBonus = _character.LeftHand.MaxDexterityBonus;
                    }
                }
                if (_character.Torso != null)
                {
                    baseKP += _character.Torso.AC;
                    panalties += _character.Torso.Panalty;
                    if (_character.Torso.MaxDexterityBonus < maxBonus)
                    {
                        maxBonus = _character.Torso.MaxDexterityBonus;
                    }
                }
                int dexterityBonus = Rules.GetStandardBonus(_character.OriginalStats.Dexterity);
                if (dexterityBonus > maxBonus)
                {
                    dexterityBonus = maxBonus;
                }
                baseKP += dexterityBonus;
                _character.OriginalStats.AC = baseKP;
                _character.CurrentStats.AC = baseKP;

                string[] attacks = null;
                if (_character.OriginalStats.AttackSkill.Contains("\\"))
                {
                    attacks = _character.OriginalStats.AttackSkill.Split('\\');
                }
                else
                {
                    attacks = new string[1] { _character.OriginalStats.AttackSkill };
                }
                foreach (string attackSkill in attacks)
                {
                    Attack atak = new Attack();
                    atak.ToHit = int.Parse(attackSkill);
                    atak.ToHit += Rules.GetStandardBonus(_character.OriginalStats.Strength);
                    if (_character.RightHand != null)
                    {
                        if (_character.RightHand.Damage.Contains("+"))
                        {
                            string[] dmgs = _character.RightHand.Damage.Split('+');
                            int bonus = int.Parse(dmgs[1]);
                            bonus += Rules.GetStandardBonus(_character.OriginalStats.Strength);
                            atak.Damage = dmgs[0] + "+" + bonus.ToString();
                        }
                        else
                        {
                            atak.Damage = _character.RightHand.Damage + "+" + Rules.GetStandardBonus(_character.OriginalStats.Strength).ToString();
                        }
                    }
                    else
                    {
                        atak.Damage = "K4" + "+" + Rules.GetStandardBonus(_character.OriginalStats.Strength).ToString();
                    }
                    _character.Attacks.Add(atak);
                }
                MessageBox.Show("Policzono");
                RefreshCharacter();
            }
            catch (Exception exc)
            {
                MessageBox.Show("Nie udało się policzyć: " + exc.ToString());
            }
        }

        private void button11_Click(object sender, RoutedEventArgs e)
        {
            AddGold wnd = new AddGold(_character, true);
            if (wnd.ShowDialog() == true)
            {
                RefreshCharacter();
            }
        }

        private void RefreshCharacter()
        {
            Character tmp = _character;
            Init(_helper, new Character());
            Init(_helper, tmp);
        }

        private void button12_Click(object sender, RoutedEventArgs e)
        {
            AddGold wnd = new AddGold(_character, false);
            if (wnd.ShowDialog() == true)
            {
                RefreshCharacter();
            }
        }

        private void listView6_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView6.SelectedItem != null)
            {
                textBox15.Text = ((Spell)listView6.SelectedItem).Definition.Description;
            }
        }

        private void button13_Click(object sender, RoutedEventArgs e)
        {
            ImageWindow wnd = new ImageWindow(_character);
            wnd.ShowDialog();
        }

        private void button14_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                IEnumerable<KilledCreature> list = _character.Kills.Where(s => s.Date >= DateTime.Parse(textBox18.Text));
                listView8.ItemsSource = list;
                listView8.Items.Refresh();
            }
            catch
            {
            }
        }

        private void button15_Click(object sender, RoutedEventArgs e)
        {
            PopUp.HealWindow healWnd = new PopUp.HealWindow(_character);
            healWnd.ShowDialog();
            RefreshCharacter();
        }
        
    }
}
