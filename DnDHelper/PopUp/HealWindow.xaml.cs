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

namespace DnDHelper.WPF.PopUp
{
    /// <summary>
    /// Interaction logic for HealWindow.xaml
    /// </summary>
    public partial class HealWindow : Window
    {
        private Character _char;

        public HealWindow(Character ch)
        {
            InitializeComponent();
            _char = ch;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _char.CurrentStats.HP += int.Parse(textBox1.Text);
                if (_char.CurrentStats.HP > _char.OriginalStats.HP)
                {
                    _char.CurrentStats.HP = _char.OriginalStats.HP;
                }
                DialogResult = true;
                Close();
            }
            catch
            {
            }
        }
    }
}
