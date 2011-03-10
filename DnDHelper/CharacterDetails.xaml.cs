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
    /// Interaction logic for CharacterDetails.xaml
    /// </summary>
    public partial class CharacterDetails : UserControl
    {
        private Character _character;
        private Helper _helper;

        public CharacterDetails()
        {
            InitializeComponent();
        }

        public void Init(Helper help, Character chr)
        {
            _character = chr;
            _helper = help;
            cbLeftHand.ItemsSource = _helper.Items;
            cbLeftHand.DisplayMemberPath = "Name";
            cbRightHand.ItemsSource = _helper.Items;
            cbRightHand.DisplayMemberPath = "Name";
            cbTorso.ItemsSource = _helper.Items;
            cbTorso.DisplayMemberPath = "Name";
            FillData();
        }

        public void FillData()
        {
            tbName.Text = _character.Name;
            tbRace.Text = _character.Race;
            tbClass.Text = _character.Class;
            tbLevel.Text = _character.Level.ToString();
            tbStrength.Text = _character.CurrentStats.Strength.ToString();
            tbStrengthOryg.Text = _character.OriginalStats.Strength.ToString();
            tbDexterity.Text = _character.CurrentStats.Dexterity.ToString();
            tbDexterityOryg.Text = _character.OriginalStats.Dexterity.ToString();
            tbConstitution.Text = _character.CurrentStats.Constitution.ToString();
            tbConstitutionOryg.Text = _character.OriginalStats.Constitution.ToString();
            tbWisdom.Text = _character.CurrentStats.Wisdom.ToString();
            tbWisdomOryg.Text = _character.OriginalStats.Wisdom.ToString();
            tbInteligence.Text = _character.CurrentStats.Inteligence.ToString();
            tbInteligenceOryg.Text = _character.OriginalStats.Inteligence.ToString();
            tbWill.Text = _character.CurrentStats.WillThrow.ToString();
            tbWillOryg.Text = _character.OriginalStats.WillThrow.ToString();
            tbReflex.Text = _character.CurrentStats.ReflexThrow.ToString();
            tbReflexOryg.Text = _character.OriginalStats.ReflexThrow.ToString();
            tbWytrwalosc.Text = _character.CurrentStats.StrongThrow.ToString();
            tbWytrwaloscOryg.Text = _character.OriginalStats.StrongThrow.ToString();
            tbAC.Text = _character.CurrentStats.AC.ToString();
            tbACOryg.Text = _character.OriginalStats.AC.ToString();
            tbHP.Text = _character.CurrentStats.HP.ToString();
            tbHPOryg.Text = _character.OriginalStats.HP.ToString();
            tbAttack.Text = _character.CurrentStats.AttackSkill.ToString();
            tbAttackOryg.Text = _character.OriginalStats.AttackSkill.ToString();
            Refresh();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ItemsWindow itWnd = new ItemsWindow(_helper);
            itWnd.Show();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            cbLeftHand.Items.Refresh();
            cbRightHand.Items.Refresh();
            cbTorso.Items.Refresh();
        }
    }
}
