﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

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
        public string Items { get; set; }
        public int Gold { get; set; }

        public Character()
        {
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
        public string AttackSkill { get; set; }
        public int WillThrow { get; set; }
        public int ReflexThrow { get; set; }
        public int StrongThrow { get; set; }

        public Stats()
        {
            Strength = Dexterity = Constitution = Wisdom = Inteligence = 10;
        }
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
        public override string ToString()
        {
            if (Type == "Broń")
            {
                return string.Format("{0} - {1} {2}", Name, Damage, Specials);
            }
            else
            {
                return string.Format("{0} - +{1}KP, Max. ZR {2}, Kary -{3}", Name, AC.ToString(), MaxDexterityBonus.ToString(), Panalty.ToString());
            }
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
