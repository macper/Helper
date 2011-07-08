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

namespace DnDHelper.WPF
{
    /// <summary>
    /// Interaction logic for AttackWindow.xaml
    /// </summary>
    public partial class AttackWindow : Window
    {
        Character _attacker;
        Character _defender;
        Battle _battle;

        public AttackWindow(Character attacker, Character defender, Battle battle)
        {
            InitializeComponent();
            _attacker = attacker;
            _defender = defender;
            _battle = battle;
            Refresh();
        }

        private void Refresh()
        {
            label3.Content = _attacker.Name;
            label4.Content = string.Format("{0} (KP:{1} PŻ:{2})", _defender.Name, _defender.CurrentStats.AC.ToString(), _defender.CurrentStats.HP.ToString());
            listBox1.Items.Clear();
            foreach (Attack at in _attacker.Attacks)
            {
                AttackInfo atInf = _battle.Atak(_defender, at);
                listBox1.Items.Add(string.Format("Atak: {0} - Trafienie: {1}", at.ToString(), atInf.ToHit.ToString()));
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _battle.DoDamage(_attacker, _defender, int.Parse(textBox1.Text));
                Refresh();
            }
            catch
            {
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.Interop.ComponentDispatcher.IsThreadModal)
            {
                DialogResult = true;
            }
            Close();
        }
    }
}
