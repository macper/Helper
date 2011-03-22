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
        protected Map _map;
        protected Block _activeBlock = new Block();
        private ActionType _activeAction;
        private const int fieldSize = 20;

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                _helper = Helper.LoadState();
                _battle = new Battle();
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
            listView1.Items.Refresh();
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
            if (listView1.SelectedItem != null)
            {
                if (MessageBox.Show("Czy na pewno usunąć ?", "Czy na pewno?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _battle.Members.Remove((Character)listView1.SelectedItem);
                    listView1.Items.Refresh();
                }
            }
        }

        private void button10_Click(object sender, RoutedEventArgs e)
        {
            if ((string)button10.Content == "Nowa")
            {
                button10.Content = "Start";
                NewBattle_Click(this, e);
            }
            else
            {
                _battle.Start();
                button10.Content = "Nowa";
                listView1.Items.Refresh();
                characterDetails2.Init(_helper, _battle.ActiveMember);
                label5.GetBindingExpression(Label.ContentProperty).UpdateTarget();
            }
        }

        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listView1.SelectedItem != null)
            {
                characterDetails2.Init(_helper, (Character)listView1.SelectedItem);
            }
        }

        private void RefreshMembers_Click(object sender, RoutedEventArgs e)
        {
            listView1.Items.Refresh();
        }

        private void button9_Click(object sender, RoutedEventArgs e)
        {
            _battle.NextMember();
            characterDetails2.Init(_helper, _battle.ActiveMember);
            listView1.Items.Refresh();
            label5.GetBindingExpression(Label.ContentProperty).UpdateTarget();
        }

        private void AttackMember_Click(object sender, RoutedEventArgs e)
        {
            if (listView1.SelectedItem != null)
            {
                Character defender = (Character)listView1.SelectedItem;
                AttackWindow wnd = new AttackWindow(_battle.ActiveMember, defender, _battle);
                if (wnd.ShowDialog() == true)
                {
                    listView1.Items.Refresh();
                }
            }
        }

        private void DoDamage_Click(object sender, RoutedEventArgs e)
        {
            if (listView1.SelectedItem != null)
            {
                Character defender = (Character)listView1.SelectedItem;
                DoDamageWindow wnd = new DoDamageWindow(defender, _battle);
                if (wnd.ShowDialog() == true)
                {
                    listView1.Items.Refresh();
                }
            }
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.N)
            {
                button9_Click(sender, e);
            }
        }

        private void AttackCustom_Click(object sender, RoutedEventArgs e)
        {
            AttackCustomWindow wnd = new AttackCustomWindow(_battle);
            wnd.Show();
        }

        private void ZapiszButton_Click(object sender, RoutedEventArgs e)
        {
            Cursor = Cursors.Wait;
            _helper.SaveState();
            Cursor = Cursors.Arrow;
        }

        private void WyjdzButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        #region Map
        private void button11_Click(object sender, RoutedEventArgs e)
        {
            NewMap newMapWnd = new NewMap();
            if (newMapWnd.ShowDialog() == true)
            {
                _map = newMapWnd.Map;
                DrawMap();
            }
        }

        private void DrawMap()
        {
            grid1.Children.Clear();
            grid1.Width = _map.Width * fieldSize;
            grid1.Height = _map.Height * fieldSize;
            grid1.Background = Brushes.White;
            for (int i = 0; i < _map.Height * fieldSize; i += fieldSize)
            {
                Line l = new Line();
                l.Stroke = Brushes.LightGray;
                l.StrokeThickness = 1;
                l.X1 = i;
                l.X2 = i;
                l.Y1 = 0;
                l.Y2 = _map.Width * fieldSize;
                grid1.Children.Add(l);
            }
            for (int j = 0; j < _map.Width * fieldSize; j += fieldSize)
            {
                Line l = new Line();
                l.Stroke = Brushes.LightGray;
                l.StrokeThickness = 1;
                l.X1 = 0;
                l.X2 = _map.Width * fieldSize;
                l.Y1 = j;
                l.Y2 = j;
                grid1.Children.Add(l);
            }

            for (int i = 0; i < _map.Height; i++)
            {
                for (int j = 0; j < _map.Width; j++)
                {
                    if (_map.BlockMap[i, j] != null)
                    {
                        Rectangle rec = new Rectangle();
                        rec.Fill = new SolidColorBrush(_map.BlockMap[i,j].Color);
                        rec.Width = fieldSize;
                        rec.Height = fieldSize;
                        rec.Margin = new Thickness(i * fieldSize, j * fieldSize, 0, 0);
                        rec.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        rec.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        grid1.Children.Add(rec);
                    }
                }
            }
        }

        private void slider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double Scale = 1;
            switch ((int)slider1.Value)
            {
                case 1:
                    Scale = 0.2;
                    break;

                case 2:
                    Scale = 0.5;
                    break;

                case 3:
                    Scale = 0.75;
                    break;

                case 5:
                    Scale = 1.5;
                    break;

                case 6:
                    Scale = 2;
                    break;

                case 7:
                    Scale = 3;
                    break;

                case 8:
                    Scale = 4;
                    break;

                case 9:
                case 10:
                    Scale = 5;
                    break;
            }
            grid1.RenderTransform = new ScaleTransform(Scale, Scale);
            if (_map != null)
            {
                grid1.Width = (int)(_map.Width * fieldSize * Scale);
                grid1.Height = (int)(_map.Height * fieldSize * Scale);
            }
        }

        private void button14_Click(object sender, RoutedEventArgs e)
        {
            WPFColorPickerLib.ColorDialog colDlg = new WPFColorPickerLib.ColorDialog();
            if (colDlg.ShowDialog() == true)
            {
                textBox2.Text = string.Format("{0}.{1}.{2}", colDlg.SelectedColor.R, colDlg.SelectedColor.G, colDlg.SelectedColor.B);
                _activeBlock.Color = colDlg.SelectedColor;
            }
        }

        private void grid1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (radioButton1.IsChecked == true)
            {
                int posX = (int)(e.GetPosition(grid1).X / fieldSize);
                int posY = (int)(e.GetPosition(grid1).Y / fieldSize);
                _map.BlockMap[posX, posY] = new Block() { Color = _activeBlock.Color, Name = _activeBlock.Name, Description = _activeBlock.Description };
            }
            DrawMap();
        }

        private void grid1_MouseMove(object sender, MouseEventArgs e)
        {
            int posX = (int)(e.GetPosition(grid1).X / fieldSize);
            int posY = (int)(e.GetPosition(grid1).Y / fieldSize);
            label11.Content = string.Format("{0},{1}", posX.ToString(), posY.ToString());
        }

        #endregion

        

        

        

        


    }

   public enum ActionType { None, AddBlock, RemoveBlock }
}
