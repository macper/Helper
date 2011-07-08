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
    /// Interaction logic for DoDamageWindow.xaml
    /// </summary>
    public partial class DoDamageWindow : Window
    {
        Character _target;
        Battle _battle;

        public DoDamageWindow(Character target, Battle battle)
        {
            InitializeComponent();
            _target = target;
            _battle = battle;
            textBox1.Focus();
            textBox1.SelectAll();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _battle.DoDamage(_target, int.Parse(textBox1.Text));
                DialogResult = true;
                Close();
            }
            catch
            {
            }
        }
    }
}
