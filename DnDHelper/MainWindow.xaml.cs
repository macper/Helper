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
            listBox3.DisplayMemberPath = "Name";
            comboBox1.ItemsSource = new string[] { "Block", "TextBlock" };
            textBox2.DataContext = _helper;
            textBox5.DataContext = _helper;
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
                listBox2.ItemsSource = ((CharacterGroup)listBox1.SelectedItem).Members.OrderBy(o => o.Name);
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
            if (_battle == null)
            {
                return;
            }
            if (_battle.Members.Count == 0)
            {
                MessageBox.Show("Brak memberów");
                return;
            }
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
            AttackCustomWindow wnd = null;
            if (listView1.SelectedItems.Count == 2)
            {
                wnd = new AttackCustomWindow(_battle, (Character)listView1.SelectedItems[0], (Character)listView1.SelectedItems[1]);
            }
            else
            wnd = new AttackCustomWindow(_battle);
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
                listBox3.ItemsSource = _map.AllNamedBlocks;
            }
        }

        private void DrawMap()
        {
            grid1.Children.Clear();
            grid1.Width = _map.Width * fieldSize;
            grid1.Height = _map.Height * fieldSize;
            grid1.Background = Brushes.White;
            for (int i = 0; i < _map.Width * fieldSize; i += fieldSize)
            {
                Line l = new Line();
                l.Stroke = Brushes.LightGray;
                l.StrokeThickness = 1;
                l.X1 = i;
                l.X2 = i;
                l.Y1 = 0;
                l.Y2 = _map.Height * fieldSize;
                grid1.Children.Add(l);
            }
            for (int j = 0; j < _map.Height * fieldSize; j += fieldSize)
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

            for (int i = 0; i < _map.Width; i++)
            {
                for (int j = 0; j < _map.Height; j++)
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
                        if (_map.BlockMap[i, j].Name != null)
                        {
                            TextBlock text = new TextBlock();
                            text.FontSize = 8;
                            text.Text = _map.BlockMap[i, j].Name;
                            text.Margin = new Thickness(i * fieldSize + 25, j * fieldSize + 10, 0, 0);
                            grid1.Children.Add(text);
                            if (_map.BlockMap[i, j] == _activeBlock)
                            {
                                text.Foreground = Brushes.Red;
                            }
                        }
                    }
                    if (_map.BlockMap[i,j] == _activeBlock)
                    {
                        Rectangle actRec = new Rectangle();
                        actRec.Stroke = Brushes.Red;
                        actRec.StrokeThickness = 2;
                        actRec.Margin = new Thickness(i * fieldSize, j * fieldSize, 0, 0);
                        actRec.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                        actRec.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                        actRec.Width = fieldSize;
                        actRec.Height = fieldSize;
                        grid1.Children.Add(actRec);
                    }
                }
            }

            foreach (StringBlock sBlock in _map.TextBlocks)
            {
                TextBlock txtBlock = new TextBlock();
                txtBlock.Foreground = new SolidColorBrush(sBlock.Color);
                txtBlock.Text = sBlock.Text;
                txtBlock.FontSize = 12;
                txtBlock.Margin = new Thickness(sBlock.Position.X, sBlock.Position.Y, 0, 0);
                grid1.Children.Add(txtBlock);
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
                rectangle1.Fill = new SolidColorBrush(colDlg.SelectedColor);
            }
        }

        private void grid1_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if ((string)comboBox1.SelectedItem == "Block")
            {
                int posX = (int)(e.GetPosition(grid1).X / fieldSize);
                int posY = (int)(e.GetPosition(grid1).Y / fieldSize);

                if (radioButton1.IsChecked == true)
                {
                    if (e.LeftButton == MouseButtonState.Pressed)
                    {
                        _map.BlockMap[posX, posY] = new Block() { Color = ((SolidColorBrush)rectangle1.Fill).Color, Name = textBox3.Text != string.Empty ? textBox3.Text : null, Description = textBox4.Text != string.Empty ? textBox4.Text : null };
                    }
                    else if (e.RightButton == MouseButtonState.Pressed)
                    {
                        _map.BlockMap[posX, posY] = null;
                    }

                }
                else if (radioButton2.IsChecked == true)
                {
                    _activeBlock = _map.BlockMap[posX, posY];
                    if (_activeBlock != null)
                    {
                        SetActiveBlock();
                    }
                }
                DrawMap();
                listBox3.ItemsSource = _map.AllNamedBlocks;
            }
            else if ((string)comboBox1.SelectedItem == "TextBlock")
            {
                _map.TextBlocks.Add(new StringBlock() { Color = ((SolidColorBrush)rectangle1.Fill).Color, Position = e.GetPosition(grid1), Text = textBox3.Text, Description = textBox4.Text });
                DrawMap();
                listBox3.ItemsSource = _map.TextBlocks;
            }
        }

        private void SetActiveBlock()
        {
            rectangle1.Fill = new SolidColorBrush(_activeBlock.Color);
            if (_activeBlock.Name != null)
            textBox3.Text = _activeBlock.Name;
            if (_activeBlock.Description != null)
            textBox4.Text = _activeBlock.Description;
        }

        private void grid1_MouseMove(object sender, MouseEventArgs e)
        {
            int posX = (int)(e.GetPosition(grid1).X / fieldSize);
            int posY = (int)(e.GetPosition(grid1).Y / fieldSize);
            label11.Content = string.Format("{0},{1}", posX.ToString(), posY.ToString());
        }

        private void button15_Click(object sender, RoutedEventArgs e)
        {
            if (_activeBlock != null)
            {
                _activeBlock.Color = ((SolidColorBrush)rectangle1.Fill).Color;
                _activeBlock.Name = textBox3.Text;
                _activeBlock.Description = textBox4.Text;
            }
        }

        private void button13_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveDlg1 = new Microsoft.Win32.SaveFileDialog();
            saveDlg1.DefaultExt = ".xml";
            saveDlg1.Filter = "Pliki XML|*.xml";
            try
            {
                if (saveDlg1.ShowDialog() == true)
                {
                    using (System.IO.FileStream fs = new System.IO.FileStream(saveDlg1.FileName, System.IO.FileMode.Create))
                    {
                        System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(SerializableMap));
                        serializer.Serialize(fs, _map.Serialize());
                        MessageBox.Show("Zapisano pomyślnie");
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void button12_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fileDlg = new Microsoft.Win32.OpenFileDialog();

            fileDlg.DefaultExt = ".xml";
            fileDlg.Filter = "Pliki XML|*.xml";
            try
            {
                if (fileDlg.ShowDialog() == true)
                {
                    using (System.IO.FileStream fs = new System.IO.FileStream(fileDlg.FileName, System.IO.FileMode.Open))
                    {
                        System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(SerializableMap));
                        SerializableMap sMap = (SerializableMap)serializer.Deserialize(fs);
                        _map = Map.Deserialize(sMap);
                        DrawMap();
                        listBox3.ItemsSource = _map.AllNamedBlocks;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
        }

        private void listBox3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox3.SelectedItem != null)
            {
                if ((string)comboBox1.SelectedItem == "Block")
                {
                    _activeBlock = (Block)listBox3.SelectedItem;
                    DrawMap();
                    SetActiveBlock();
                }
                else if ((string)comboBox1.SelectedItem == "TextBlock")
                {
                    StringBlock sBlock = (StringBlock)listBox3.SelectedItem;
                    rectangle1.Fill = new SolidColorBrush(sBlock.Color);
                    textBox3.Text = sBlock.Text;
                    textBox4.Text = sBlock.Description;
                }
            }
        }

        #endregion

        private void button16_Click(object sender, RoutedEventArgs e)
        {
            if (listBox2.SelectedItem != null)
            {
                Character chToCopy = (Character)listBox2.SelectedItem;
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(Character));
                    xmlSerializer.Serialize(ms, chToCopy);
                    ms.Position = 0;
                    Character newChar = (Character)xmlSerializer.Deserialize(ms);
                    newChar.Name = "Kopia_" + chToCopy.Name;
                    CharacterGroup chGr = (CharacterGroup)listBox1.SelectedItem;
                    chGr.Members.Add(newChar);
                    characterDetails1.Init(_helper, newChar);
                    listBox2.ItemsSource = chGr.Members.OrderBy(o => o.Name);
                }
            }
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((string)comboBox1.SelectedItem == "Block")
            {
                listBox3.ItemsSource = _map.AllNamedBlocks;
                listBox3.DisplayMemberPath = "Name";
            }
            else if ((string)comboBox1.SelectedItem == "TextBlock")
            {
                listBox3.ItemsSource = _map.TextBlocks;
                listBox3.DisplayMemberPath = "Text";
            }
        }

        private void UsunTextBlockClick(object sender, RoutedEventArgs e)
        {
            if ((string)comboBox1.SelectedItem == "TextBlock")
            {
                if (listBox3.SelectedItem != null)
                {
                    _map.TextBlocks.Remove((StringBlock)listBox3.SelectedItem);
                    listBox3.ItemsSource = _map.TextBlocks;
                    DrawMap();
                }
            }
        }

        private void grid1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((string)comboBox1.SelectedItem == "TextBlock")
            {
                if (listBox3.SelectedItem != null)
                {
                    StringBlock sb = (StringBlock)listBox3.SelectedItem;
                    if (e.Key == Key.W)
                    {
                        sb.Position = new Point(sb.Position.X, sb.Position.Y - 2);
                    }
                    else if (e.Key == Key.S)
                    {
                        sb.Position = new Point(sb.Position.X, sb.Position.Y + 2);
                    }
                    else if (e.Key == Key.A)
                    {
                        sb.Position = new Point(sb.Position.X - 2, sb.Position.Y);
                    }
                    else if (e.Key == Key.D)
                    {
                        sb.Position = new Point(sb.Position.X + 2, sb.Position.Y);
                    }
                    DrawMap();
                }
            }
        }

        private void button17_Click(object sender, RoutedEventArgs e)
        {
            AddXP wnd = new AddXP();
            if (wnd.ShowDialog() == true)
            {
                _helper.XP += wnd.XP;
                textBox2.Text = _helper.XP.ToString();
            }
        }
    }

   public enum ActionType { None, AddBlock, RemoveBlock }
}
