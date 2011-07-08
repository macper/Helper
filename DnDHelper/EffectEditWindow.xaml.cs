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
    /// Interaction logic for EffectEditWindow.xaml
    /// </summary>
    public partial class EffectEditWindow : Window
    {
        Effect _effect;
        Helper _helper;

        public EffectEditWindow(Effect effect, Helper helper)
        {
            InitializeComponent();
            _effect = effect;
            _helper = helper;
            GridContent.DataContext = _effect;
            comboBox1.ItemsSource = _helper.Effects;
            comboBox1.DisplayMemberPath = "Name";
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            _effect.Name = textBox1.Text;
            _effect.Description = textBox2.Text;
            if (checkBox1.IsChecked == false)
            {
                _effect.Duration = int.Parse(textBox3.Text);
            }
            if (checkBox2.IsChecked == true)
            {
                if (_helper.Effects.Any(f => f.Name == _effect.Name))
                {
                    MessageBox.Show("Istnieje już efekt o tej nazwie");
                    return;
                }
                _helper.Effects.Add(_effect);
            }
            DialogResult = true;
            Close();
        }

        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {
            if (textBox3 != null)
            {
                textBox3.IsEnabled = false;
            }
        }

        private void checkBox1_Unchecked(object sender, RoutedEventArgs e)
        {
            if (textBox3 != null)
            {
                textBox3.IsEnabled = true;
            }
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                GridContent.DataContext = comboBox1.SelectedItem;
            }
        }
    }
}
