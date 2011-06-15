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

namespace DnDHelper.PopUp
{
    /// <summary>
    /// Interaction logic for ChangeGroupWindow.xaml
    /// </summary>
    public partial class ChangeGroupWindow : Window
    {
        private Character _char;
        private CharacterGroup _orygGroup;
        private Helper _helper;

        public ChangeGroupWindow(Character ch, CharacterGroup gr, Helper help)
        {
            InitializeComponent();
            _char = ch;
            _orygGroup = gr;
            _helper = help;
            comboBox1.ItemsSource = _helper.Groups;
            comboBox1.DisplayMemberPath = "GroupName";
            comboBox1.SelectedItem = gr;
            comboBox2.ItemsSource = _helper.Groups;
            comboBox2.DisplayMemberPath = "GroupName";
            textBox1.Text = ch.Name;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox2.SelectedItem != null)
            {
                CharacterGroup g = (CharacterGroup)comboBox2.SelectedItem;
                if (g == _orygGroup)
                {
                    MessageBox.Show("Nowa grupa nie może być taka sama jak oryginalna");
                    return;
                }
                _orygGroup.Members.Remove(_char);
                g.Members.Add(_char);
                DialogResult = true;
                Close();
            }
        }


    }
}
