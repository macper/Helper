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
    /// Interaction logic for ImageWindow.xaml
    /// </summary>
    public partial class ImageWindow : Window
    {
        Character _character;

        public ImageWindow(Character character)
        {
            InitializeComponent();
            _character = character;
            if (_character.ImagePath != null)
            {
                try
                {
                    image1.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + _character.ImagePath));
                }
                catch
                {
                    MessageBox.Show("Nie udało się wczytać:" + Application.Current.StartupUri + "\\Images\\" + _character.ImagePath);
                }
                textBox1.Text = _character.ImagePath;
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog open1 = new Microsoft.Win32.OpenFileDialog();
            if (open1.ShowDialog() == true)
            {
                try
                {
                    System.IO.File.Copy(open1.FileName, AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + open1.FileName.Substring(open1.FileName.LastIndexOf('\\')));
                    _character.ImagePath = open1.FileName.Substring(open1.FileName.LastIndexOf('\\'));
                    image1.Source = new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + _character.ImagePath));
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.ToString());
                }
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _character.ImagePath = null;
                System.IO.File.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\Images\\" + _character.ImagePath);
                textBox1.Text = "";
            }
            catch
            {
            }
        }
    }
}
