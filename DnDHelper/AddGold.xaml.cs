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
    /// Interaction logic for AddGold.xaml
    /// </summary>
    public partial class AddGold : Window
    {
        Character _character;
        bool _addGold;

        public AddGold(Character character, bool added)
        {
            InitializeComponent();
            _character = character;
            _addGold = added;
            textBox1.Focus();
            textBox1.SelectAll();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int count = int.Parse(textBox1.Text);
                if (!_addGold)
                {
                    count = -count;
                }
                _character.Gold += count;
                DialogResult = true;
                Close();
            }
            catch
            {
            }
        }
    }
}
