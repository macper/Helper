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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DnDHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected Helper _helper;
        protected Battle _battle;

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                _helper = Helper.LoadState();
            }
            catch
            {
                _helper = new Helper();
            }
            _helper.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_helper_PropertyChanged);
        }

        void _helper_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "CurrentTime":
                    label1.Content = _helper.CurrentTime.ToString("yyyy-MM-dd HH:mm");
                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listBox1.ItemsSource = _helper.Groups;
            listBox1.DisplayMemberPath = "GroupName";
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            _helper.CurrentTime = _helper.CurrentTime.AddDays(1);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            _helper.CurrentTime = _helper.CurrentTime.AddHours(1);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            _helper.CurrentTime = _helper.CurrentTime.AddHours(-1);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                _helper.SaveState();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            textBox1.Text = "Nowa grupa";
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            string newGroupName = textBox1.Text;
            if (_helper.Groups.Any(el => el.GroupName == newGroupName))
            {
                MessageBox.Show("Już istnieje grupa o takiej nazwie");
                return;
            }
            CharacterGroup newGroup = new CharacterGroup() { GroupName = newGroupName };
            _helper.Groups.Add(newGroup);
            listBox1.Items.Refresh();
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Wybierz jakis element do usuniecia");
            }
            try
            {
                _helper.Groups.Remove((CharacterGroup)listBox1.SelectedItem);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            listBox1.Items.Refresh();
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            ItemsWindow itemsWnd = new ItemsWindow(_helper);
            itemsWnd.Show();
        }

        private void button7_Click_1(object sender, RoutedEventArgs e)
        {
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show("Wybierz jakąś grupę");
                return;
            }
            CharacterGroup chGr = (CharacterGroup)listBox1.SelectedItem;
            Character newCharacter = new Character() { Name = "Nowa postać" };
            chGr.Members.Add(newCharacter);
            characterDetails1.Init(_helper, newCharacter);
            listBox2.Items.Refresh();
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                listBox2.ItemsSource = ((CharacterGroup)listBox1.SelectedItem).Members;
                listBox2.DisplayMemberPath = "Name";
                listBox2.Items.Refresh();
            }
        }

        private void listBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                characterDetails1.Init(_helper, (Character)listBox2.SelectedItem);
            }
            listBox2.Items.Refresh();
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                CharacterGroup chG = (CharacterGroup)listBox1.SelectedItem;
                chG.Members.Remove((Character)listBox2.SelectedItem);
            }
            listBox2.Items.Refresh();
        }

        private void NewBattle_Click(object sender, RoutedEventArgs e)
        {
            _battle = new Battle();
            ContentWalka.DataContext = _battle;
            listView1.ItemsSource = _battle.Members;
        }

        private void BattleBegin_Click(object sender, RoutedEventArgs e)
        {
            _battle.Start();
        }

        private void AddMember_Click(object sender, RoutedEventArgs e)
        {
            BattleMemberAddWindow wnd = new BattleMemberAddWindow(_helper, _battle);
            if (wnd.ShowDialog() == true)
            {
                listView1.Items.Refresh();
            }
        }

        private void RemoveMember_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
