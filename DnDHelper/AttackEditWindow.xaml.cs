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
    /// Interaction logic for AttackEditWindow.xaml
    /// </summary>
    public partial class AttackEditWindow : Window
    {
        Attack _attack;

        public AttackEditWindow(Attack at)
        {
            InitializeComponent();
            _attack = at;
            textBox1.Text = at.ToHit.ToString();
            textBox2.Text = at.Damage;
            textBox3.Text = at.Name;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _attack.ToHit = int.Parse(textBox1.Text);
                _attack.Damage = textBox2.Text;
                _attack.Name = textBox3.Text;
            }
            catch
            {
                MessageBox.Show("Dane nieprawidłowe!");
                return;
            }
            DialogResult = true;
            Close();
        }


    }
}
