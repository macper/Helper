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
    /// Interaction logic for NewMap.xaml
    /// </summary>
    public partial class NewMap : Window
    {
        public Map Map;

        public NewMap()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Map = new DnDHelper.WPF.Map( int.Parse(textBox1.Text), int.Parse(textBox2.Text));
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                return;
            }
            DialogResult = true;
            Close();
        }
    }
}
