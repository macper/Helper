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
    /// Interaction logic for EffectEditWindow.xaml
    /// </summary>
    public partial class EffectEditWindow : Window
    {
        Effect _effect;

        public EffectEditWindow(Effect effect)
        {
            InitializeComponent();
            _effect = effect;
            textBox1.Text = effect.Name;
            textBox2.Text = effect.Description;
            if (effect.Duration == null)
            {
                checkBox1.IsChecked = true;
                textBox3.IsEnabled = false;
            }
            else
            {
                textBox3.Text = effect.Duration.ToString();
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            _effect.Name = textBox1.Text;
            _effect.Description = textBox2.Text;
            if (checkBox1.IsChecked == false)
            {
                _effect.Duration = int.Parse(textBox3.Text);
            }
            DialogResult = true;
            Close();
        }

        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {
            textBox3.IsEnabled = false;
        }

        private void checkBox1_Unchecked(object sender, RoutedEventArgs e)
        {
            textBox3.IsEnabled = true;
        }
    }
}
