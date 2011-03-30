using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DnDHelper
{
    [Serializable]
    public class Character : IComparable
    {
        public string Name { get; set; }
        public string Race { get; set; }
        public int Level { get; set; }
        public string Class { get; set; }
        public string Appearance { get; set; }
        public Stats CurrentStats { get; set; }
        public Stats OriginalStats { get; set; }
        public int Initiative { get; set; }
        public int BaseInitiative { get; set; }
        public int Speed { get; set; }
        public List<Attack> Attacks { get; set; }
        public Item RightHand { get; set; }
        public Item LeftHand { get; set; }
        public Item Torso { get; set; }
        public string Items { get; set; }
        public int Gold { get; set; }
        public List<Effect> Effects { get; set; }
        [System.Xml.Serialization.XmlIgnore]
        public List<SpellDefinition> KnownSpells { get; set; }
        [System.Xml.Serialization.XmlIgnore]
        public List<Spell> Spells { get; set; }
        public List<string> KnownSpellsS { get; set; }
        public List<SpellS> SpellsS { get; set; }
        public List<SpellCasting> AvailableCastings { get; set; }
        public List<Skill> Skills { get; set; }

        // Chujowy hack bo już mnie kurwica bierze z tym WPF-em pierdolonym
        public bool IsActiveMember { get; set; }
        public bool IsAlive { get { return CurrentStats.HP > 0; } }

        public Character()
        {
            Attacks = new List<Attack>();
            CurrentStats = new Stats();
            OriginalStats = new Stats();
            Effects = new List<Effect>();
            KnownSpells = new List<SpellDefinition>();
            Spells = new List<Spell>();
            Skills = new List<Skill>();
            KnownSpellsS = new List<string>();
            SpellsS = new List<SpellS>();
        }

        public void DeserializeSelf(Helper _helper)
        {
            foreach (string spellDef in KnownSpellsS)
            {
                SpellDefinition sd = _helper.GetSpell(spellDef);
                if (sd != null)
                {
                    KnownSpells.Add(sd);
                }
            }
            foreach (SpellS spell in SpellsS)
            {
                SpellDefinition sd = _helper.GetSpell(spell.Name);
                Spells.Add(new Spell() { Definition = sd, IsCasted = spell.IsCasted });
            }
        }

        public void SerializeSelf()
        {
            KnownSpellsS = new List<string>();
            foreach (SpellDefinition sd in KnownSpells)
            {
                KnownSpellsS.Add(sd.Name);
            }
            SpellsS = new List<SpellS>();
            foreach (Spell spell in Spells)
            {
                SpellsS.Add(new SpellS() { Name = spell.Definition.Name, IsCasted = spell.IsCasted });
            }
        }

        public string Description
        {
            get
            {
                string strEffects ="";
                foreach (Effect ef in Effects)
                {
                    strEffects += string.Format("{0} ({1}) ", ef.Name, ef.Duration.ToString());
                }

                return string.Format("PŻ:{0} Efekty: {1}", CurrentStats.HP.ToString(), strEffects);
            }
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            return ((Character)obj).Initiative - Initiative;
        }

        #endregion
    }

    [Serializable]
    public class Stats
    {
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Wisdom { get; set; }
        public int Inteligence { get; set; }
        public int Charisma { get; set; }
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

    public enum BaseTypes { LightBlade, HeavyBlade, Axe, Bow, Crossbow, Spear, Blunt, LightArmor, MediumArmor, HeavyArmor, Shield, Ring, Necklease, Boots, Other }

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
        public string Name { get; set; }
        public int ToHit { get; set; }
        public string Damage { get; set; }

        public override string ToString()
        {
            return string.Format("({2}) +{0} {1}", ToHit.ToString(), Damage.ToString(), Name);
        }
    }

    [Serializable]
    public class Effect
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Duration { get; set; }
        [System.Xml.Serialization.XmlIgnore]
        public bool IsPermanent { get { return Duration == null; } }
    }

    [Serializable]
    public class SpellDefinition
    {
        public string Name { get; set; }
        public string Types { get; set; }
        [System.Xml.Serialization.XmlIgnore]
        public string[] Types_Array { get { return Types == null ? new string [] {} : Types.Split(','); } }
        public string Shool { get; set; }
        public int Level { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return string.Format(@"Nazwa: {0}
Typ: {1}
Poziom: {2}
Szkoły: {3}
Opis:
{4}", Name, Types, Level.ToString(), Shool, Description);
        }
    }

    public class Spell
    {
        public SpellDefinition Definition { get; set; }
        public bool IsCasted { get; set; }
    }

    [Serializable]
    public class SpellS
    {
        public string Name { get; set; }
        public bool IsCasted { get; set; }
    }
    [Serializable]
    public class SpellCasting
    {
        public int Level { get; set; }
        public int Count { get; set; }
        public string Type { get; set; }

        public override string ToString()
        {
            return string.Format("{0} - poz.{1}({2})", Type, Level.ToString(), Count.ToString());
        }
    }
}
