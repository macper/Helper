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
    }
}
