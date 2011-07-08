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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddXP : Window
    {
        public int XP { get; set; }
        public AddXP()
        {
            InitializeComponent();
            textBox1.Focus();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                XP = int.Parse(textBox1.Text);
            }
            catch
            {
                return;
            }
            DialogResult = true;
            Close();
        }
    }
}
