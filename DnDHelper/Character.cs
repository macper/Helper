using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DnDHelper
{
    [Serializable]
    public class Character
    {
        public string Name { get; set; }
        public string Race { get; set; }
        public int Level { get; set; }
        public string Class { get; set; }
        public string Appearance { get; set; }
        public Stats CurrentStats { get; set; }
        public Stats OriginalStats { get; set; }
        public int Initiative { get; set; }
        public List<Attack> Attacks { get; set; }
        public Item RightHand { get; set; }
        public Item LeftHand { get; set; }
        public Item Torso { get; set; }
        public List<Item> Items { get; set; }
        public int Gold { get; set; }

        public Character()
        {
            Items = new List<Item>();
            Attacks = new List<Attack>();
            CurrentStats = new Stats();
            OriginalStats = new Stats();
        }
    }

    [Serializable]
    public class Stats
    {
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Wisdom { get; set; }
        public int Inteligence { get; set; }
        public int HP { get; set; }
        public int AC { get; set; }
        public int AttackSkill { get; set; }
        public int WillThrow { get; set; }
        public int ReflexThrow { get; set; }
        public int StrongThrow { get; set; }
    }

    [Serializable]
    public class Item
    {
        public string Name { get; set; }
        public string Specials { get; set; }
        public int Cost { get; set; }
        public string Damage { get; set; }
        public string Range { get; set; }
        public string BaseType { get; set; }
        public int AC { get; set; }
        public int MaxDexterityBonus { get; set; }
        public int Panalty { get; set; }
        public string Type
        {
            get { return Damage != null ? "Broń" : "Zbroja"; }
        }
    }

    [Serializable]
    public class CharacterGroup
    {
        public string GroupName { get; set; }
        public List<Character> Members { get; set; }

        public CharacterGroup()
        {
            Members = new List<Character>();
        }
    }

    [Serializable]
    public class Attack
    {
        public int ToHit { get; set; }
        public string Damage { get; set; }
    }

    [Serializable]
    public class Effect
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
    }
}
