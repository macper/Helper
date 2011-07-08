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
    /// Interaction logic for AttackCustomWindow.xaml
    /// </summary>
    public partial class AttackCustomWindow : Window
    {
        Battle _battle;

        public AttackCustomWindow(Battle battle)
        {
            InitializeComponent();
            _battle = battle;
            comboBox1.ItemsSource = _battle.Members;
            comboBox2.ItemsSource = _battle.Members;
        }

        public AttackCustomWindow(Battle battle, Character attacker, Character defender)
        {
            InitializeComponent();
            _battle = battle;
            comboBox1.ItemsSource = _battle.Members;
            comboBox2.ItemsSource = _battle.Members;
            comboBox1.SelectedItem = attacker;
            comboBox2.SelectedItem = defender;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null)
            {
                AttackWindow wnd = new AttackWindow((Character)comboBox1.SelectedItem, (Character)comboBox2.SelectedItem, _battle);
                wnd.Show();
                Close();
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Character tmp = (Character)comboBox1.SelectedItem;
            comboBox1.SelectedItem = comboBox2.SelectedItem;
            comboBox2.SelectedItem = tmp;
        }
    }
}
