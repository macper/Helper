﻿using System;
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
    /// Interaction logic for AttackCustomWindow.xaml
    /// </summary>
    public partial class AttackCustomWindow : Window
    {
        Battle _battle;

        public AttackCustomWindow(Battle battle)
        {
            InitializeComponent();
            _battle = battle;
            comboBox1.ItemsSource = _battle.Members;
            comboBox2.ItemsSource = _battle.Members;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null)
            {
                AttackWindow wnd = new AttackWindow((Character)comboBox1.SelectedItem, (Character)comboBox2.SelectedItem, _battle);
                wnd.Show();
                Close();
            }
        }
    }
}