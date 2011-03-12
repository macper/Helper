using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DnDHelper
{
    public static class Rules
    {
        public static int GetStandardBonus(int value)
        {
            return (int)((value - 10) / 2);
        }

        public static List<SkillDefinition> GetSkillsDefinitions()
        {
            List<SkillDefinition> skList = new List<SkillDefinition>();
            skList.Add(new SkillDefinition() { Name = "Przeszukiwanie", Description = "Używane przy znajdowaniu pułapek, ukrytych drzwi", BonusProperty = BaseAttribute.Inteligence });
            skList.Add(new SkillDefinition() { Name = "Ciche poruszanie się", Description = "Wiadomo", BonusProperty = BaseAttribute.Dexterity });
            skList.Add(new SkillDefinition() { Name = "Dyplomacja", Description = "Wiadomo", BonusProperty = BaseAttribute.Charisma});
            skList.Add(new SkillDefinition() { Name = "Koncentracja", Description = "ST = 10 + zadane obrażenia", BonusProperty = BaseAttribute.Constitution });
            skList.Add(new SkillDefinition() { Name = "Nasłuchiwanie", Description = "Test sporny z Cichym poruszaniem się. ST = 5 (Osoba w zbroi), ST = 10 (bez zbroi)", BonusProperty = BaseAttribute.Wisdom });
            skList.Add(new SkillDefinition() { Name = "Otwieranie zamków", Description = "ST zamków: 20 - prosty, 30 - niezły, 40 - świetny", BonusProperty = BaseAttribute.Dexterity });
            skList.Add(new SkillDefinition() { Name = "Leczenie", Description = "ST: 15 (pierwsza pomoc)", BonusProperty = BaseAttribute.Wisdom });
            skList.Add(new SkillDefinition() { Name = "Pływanie", Description = "Spokojna woda(10), Wzburzona woda(15), Sztorm(20)", BonusProperty = BaseAttribute.Strength });
            skList.Add(new SkillDefinition() { Name = "Skakanie", Description = "5 / 1,5 m", BonusProperty = BaseAttribute.Strength });
            skList.Add(new SkillDefinition() { Name = "Spostrzegawczość", Description = "Test sporny z Ukrywaniem", BonusProperty = BaseAttribute.Wisdom });
            skList.Add(new SkillDefinition() { Name = "Szacowanie", Description = "Znane przedmioty: ST:15", BonusProperty = BaseAttribute.Inteligence });
            skList.Add(new SkillDefinition() { Name = "Ukrywanie się", Description = "Gdy niewidzialny premia +20", BonusProperty = BaseAttribute.Dexterity });
            skList.Add(new SkillDefinition() { Name = "Unieszkodliwanie mechanizmów", Description = "Proste ST:10", BonusProperty = BaseAttribute.Inteligence });
            skList.Add(new SkillDefinition() { Name = "Używanie liny", Description = "Wiadomo", BonusProperty = BaseAttribute.Dexterity });
            skList.Add(new SkillDefinition() { Name = "Wspinaczka", Description = "ST: 5 (z liną), ST: 10 (nierówna ściana), ST:15 (tylko ręce)", BonusProperty = BaseAttribute.Strength });
            return skList;
        }

        public static Dictionary<string, Class> ClassTable
        {
            get
            {
                Dictionary<string, Class> retTable = new Dictionary<string, Class>();
                retTable["Wojownik"] = Warrior;
                retTable["Barbarzyńca"] = Barbarian;
                retTable["Tropiciel"] = Ranger;
                retTable["Mag"] = Mage;
                return retTable;
            }
        }

        public static Dictionary<string, Race> RaceTable
        {
            get
            {
                Dictionary<string, Race> retTable = new Dictionary<string, Race>();
                retTable["Człowiek"] = Human;
                retTable["Elf"] = Elf;
                retTable["Półelf"] = HalfElf;
                retTable["Krasnolud"] = Dwarf;
                retTable["Gnom"] = Gnome;
                retTable["Niziołek"] = Hafling;
                retTable["Półork"] = Halforc;
                return retTable;
            }
        }

        public static Class Warrior
        {
            get
            {
                return new Class()
                {
                    Name = "Wojownik",
                    AttackPerLevel = GetAttackHigh(),
                    ThrowPerLevel = GetWarriorThrowTable(),
                    PW = 10
                };
            }

        }

        public static Class Barbarian
        {
            get
            {
                return new Class()
                {
                    Name = "Barbarzyńca",
                    AttackPerLevel = GetAttackHigh(),
                    ThrowPerLevel = GetWarriorThrowTable(),
                    PW = 12

                };
            }
        }

        public static Class Ranger
        {
            get
            {
                return new Class()
                {
                    Name = "Tropiciel",
                    AttackPerLevel = GetAttackHigh(),
                    ThrowPerLevel = GetRangerThrowTable(),
                    PW = 8
                };
            }
        }

        public static Class Mage
        {
            get
            {
                return new Class()
                {
                    Name = "Mag",
                    AttackPerLevel = GetAttackLow(),
                    ThrowPerLevel = GetMageThrowTable(),
                    SpellsPerLevel = GetMageSpellCastings(),
                    PW = 4
                };
            }
        }

        public static Race Human
        {
            get
            {
                return new Race()
                {
                    Name = "Człowiek"
                };
            }
        }

        public static Race Elf
        {
            get
            {
                Race ret = new Race();
                ret.Name = "Elf";
                ret.Bonuses.Add(BaseAttribute.Dexterity, 2);
                ret.Bonuses.Add(BaseAttribute.Constitution, -2);
                return ret;
            }
        }

        public static Race HalfElf
        {
            get
            {
                return new Race()
                {
                    Name = "Półelf"
                };
            }
        }

        public static Race Dwarf
        {
            get
            {
                Race ret = new Race() { Name = "Krasnolud" };
                ret.Bonuses.Add(BaseAttribute.Constitution, 2);
                ret.Bonuses.Add(BaseAttribute.Charisma, -2);
                return ret;
            }
        }

        public static Race Gnome
        {
            get
            {
                Race ret = new Race() { Name = "Gnom" };
                ret.Bonuses.Add(BaseAttribute.Constitution, 2);
                ret.Bonuses.Add(BaseAttribute.Strength, -2);
                return ret;
            }
        }

        public static Race Hafling
        {
            get
            {
                Race ret = new Race() { Name = "Niziołek" };
                ret.Bonuses.Add(BaseAttribute.Dexterity, 2);
                ret.Bonuses.Add(BaseAttribute.Strength, -2);
                return ret;
            }
        }

        public static Race Halforc
        {
            get
            {
                Race ret = new Race() { Name = "Półork" };
                ret.Bonuses.Add(BaseAttribute.Strength, 2);
                ret.Bonuses.Add(BaseAttribute.Inteligence, -2);
                ret.Bonuses.Add(BaseAttribute.Charisma, -2);
                return ret;
            }
        }

        public static Dictionary<int, string> GetAttackHigh()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            dict[1] = "1";
            dict[2] = "2";
            dict[3] = "3";
            dict[4] = "4";
            dict[5] = "5";
            dict[6] = "6\\1";
            dict[7] = "7\\2";
            dict[8] = "8\\3";
            dict[9] = "9\\4";
            dict[10] = "10\\5";
            return dict;
        }

        public static Dictionary<int, string> GetAttackLow()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            dict[1] = "0";
            dict[2] = "1";
            dict[3] = "1";
            dict[4] = "2";
            dict[5] = "2";
            dict[6] = "3";
            dict[7] = "3";
            dict[8] = "4";
            dict[9] = "4";
            dict[10] = "5";
            return dict;
        }

        public static Dictionary<int, Throw> GetWarriorThrowTable()
        {
            Dictionary<int, Throw> dict = new Dictionary<int,Throw>();
            dict[1] = new Throw() { EnduranceThrow = 2 };
            dict[2] = new Throw() { EnduranceThrow = 3 };
            dict[3] = new Throw() { EnduranceThrow = 3, ReflexThrow = 1, WillThrow = 1 };
            dict[4] = new Throw() { EnduranceThrow = 4, ReflexThrow = 1, WillThrow = 1 };
            dict[5] = new Throw() { EnduranceThrow = 4, ReflexThrow = 1, WillThrow = 1 };
            dict[6] = new Throw() { EnduranceThrow = 5, ReflexThrow = 2, WillThrow = 2 };
            dict[7] = new Throw() { EnduranceThrow = 5, ReflexThrow = 2, WillThrow = 2 };
            dict[8] = new Throw() { EnduranceThrow = 6, ReflexThrow = 2, WillThrow = 2 };
            dict[9] = new Throw() { EnduranceThrow = 6, ReflexThrow = 3, WillThrow = 3 };
            dict[10] = new Throw() { EnduranceThrow = 7, ReflexThrow = 3, WillThrow = 3 };
            return dict;
        }

        public static Dictionary<int, Throw> GetRangerThrowTable()
        {
            Dictionary<int, Throw> dict = new Dictionary<int, Throw>();
            dict[1] = new Throw() { EnduranceThrow = 2, ReflexThrow = 2, WillThrow = 0 };
            dict[2] = new Throw() { EnduranceThrow = 3, ReflexThrow = 3 };
            dict[3] = new Throw() { EnduranceThrow = 3, ReflexThrow = 3, WillThrow = 1 };
            dict[4] = new Throw() { EnduranceThrow = 4, ReflexThrow = 4, WillThrow = 1 };
            dict[5] = new Throw() { EnduranceThrow = 4, ReflexThrow = 4, WillThrow = 1 };
            dict[6] = new Throw() { EnduranceThrow = 5, ReflexThrow = 5, WillThrow = 2 };
            dict[7] = new Throw() { EnduranceThrow = 5, ReflexThrow = 5, WillThrow = 2 };
            dict[8] = new Throw() { EnduranceThrow = 6, ReflexThrow = 6, WillThrow = 2 };
            dict[9] = new Throw() { EnduranceThrow = 6, ReflexThrow = 6, WillThrow = 3 };
            dict[10] = new Throw() { EnduranceThrow = 7, ReflexThrow = 7, WillThrow = 3 };
            return dict;
        }

        public static Dictionary<int, Throw> GetMageThrowTable()
        {
            Dictionary<int, Throw> dict = new Dictionary<int, Throw>();
            dict[1] = new Throw() { EnduranceThrow = 0, ReflexThrow = 0, WillThrow = 2 };
            dict[2] = new Throw() { EnduranceThrow = 0, ReflexThrow = 0, WillThrow = 3 };
            dict[3] = new Throw() { EnduranceThrow = 1, ReflexThrow = 1, WillThrow = 3 };
            dict[4] = new Throw() { EnduranceThrow = 1, ReflexThrow = 1, WillThrow = 4 };
            dict[5] = new Throw() { EnduranceThrow = 1, ReflexThrow = 1, WillThrow = 4 };
            dict[6] = new Throw() { EnduranceThrow = 2, ReflexThrow = 2, WillThrow = 5 };
            dict[7] = new Throw() { EnduranceThrow = 2, ReflexThrow = 2, WillThrow = 5 };
            dict[8] = new Throw() { EnduranceThrow = 2, ReflexThrow = 2, WillThrow = 6 };
            dict[9] = new Throw() { EnduranceThrow = 3, ReflexThrow = 3, WillThrow = 6 };
            dict[10] = new Throw() { EnduranceThrow = 3, ReflexThrow = 3, WillThrow = 7 };
            return dict;
        }

        public static Dictionary<int, List<SpellCasting>> GetMageSpellCastings()
        {
            Dictionary<int, List<SpellCasting>> dict = new Dictionary<int, List<SpellCasting>>();
            dict[1] = new List<SpellCasting>();
            dict[1].Add(new SpellCasting() { Type = "Mage", Level = 1, Count = 2 });
            dict[2] = new List<SpellCasting>();
            dict[2].Add(new SpellCasting() { Type = "Mage", Level = 1, Count = 3 });
            dict[3] = new List<SpellCasting>();
            dict[3].Add(new SpellCasting() { Type = "Mage", Level = 1, Count = 3 });
            dict[3].Add(new SpellCasting() { Type = "Mage", Level = 2, Count = 2 });
            dict[4] = new List<SpellCasting>();
            dict[4].Add(new SpellCasting() { Type = "Mage", Level = 1, Count = 4 });
            dict[4].Add(new SpellCasting() { Type = "Mage", Level = 2, Count = 3 });
            dict[5] = new List<SpellCasting>();
            dict[5].Add(new SpellCasting() { Type = "Mage", Level = 1, Count = 4 });
            dict[5].Add(new SpellCasting() { Type = "Mage", Level = 2, Count = 3 });
            dict[5].Add(new SpellCasting() { Type = "Mage", Level = 3, Count = 2 });
            dict[6] = new List<SpellCasting>();
            dict[6].Add(new SpellCasting() { Type = "Mage", Level = 1, Count = 4 });
            dict[6].Add(new SpellCasting() { Type = "Mage", Level = 2, Count = 4 });
            dict[6].Add(new SpellCasting() { Type = "Mage", Level = 3, Count = 3 });
            dict[7] = new List<SpellCasting>();
            dict[7].Add(new SpellCasting() { Type = "Mage", Level = 1, Count = 5 });
            dict[7].Add(new SpellCasting() { Type = "Mage", Level = 2, Count = 4 });
            dict[7].Add(new SpellCasting() { Type = "Mage", Level = 3, Count = 3 });
            dict[7].Add(new SpellCasting() { Type = "Mage", Level = 4, Count = 2 });
            dict[8] = new List<SpellCasting>();
            dict[8].Add(new SpellCasting() { Type = "Mage", Level = 1, Count = 5 });
            dict[8].Add(new SpellCasting() { Type = "Mage", Level = 2, Count = 4 });
            dict[8].Add(new SpellCasting() { Type = "Mage", Level = 3, Count = 4 });
            dict[8].Add(new SpellCasting() { Type = "Mage", Level = 4, Count = 3 });
            dict[9] = new List<SpellCasting>();
            dict[9].Add(new SpellCasting() { Type = "Mage", Level = 1, Count = 5 });
            dict[9].Add(new SpellCasting() { Type = "Mage", Level = 2, Count = 5 });
            dict[9].Add(new SpellCasting() { Type = "Mage", Level = 3, Count = 4 });
            dict[9].Add(new SpellCasting() { Type = "Mage", Level = 4, Count = 3 });
            dict[9].Add(new SpellCasting() { Type = "Mage", Level = 5, Count = 2 });
            dict[10] = new List<SpellCasting>();
            dict[10].Add(new SpellCasting() { Type = "Mage", Level = 1, Count = 5 });
            dict[10].Add(new SpellCasting() { Type = "Mage", Level = 2, Count = 5 });
            dict[10].Add(new SpellCasting() { Type = "Mage", Level = 3, Count = 4 });
            dict[10].Add(new SpellCasting() { Type = "Mage", Level = 4, Count = 4 });
            dict[10].Add(new SpellCasting() { Type = "Mage", Level = 5, Count = 3 });
            return dict;
        }
    }

    public enum BaseAttribute { Strength, Dexterity, Constitution, Wisdom, Inteligence, Charisma };

    [Serializable]
    public class SkillDefinition
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public BaseAttribute BonusProperty { get; set; }
    }

    [Serializable]
    public class Skill
    {
        public string Name { get; set; }
        public int BaseValue { get; set; }
        public int BonusValue { get; set; }
        public int Value { get { return BaseValue + BonusValue; } }
    }

    public class Class
    {
        public string Name { get; set; }
        public int PW { get; set; }
        public Dictionary<int, string> AttackPerLevel { get; set; }
        public Dictionary<int, Throw> ThrowPerLevel { get; set; }
        public Dictionary<int, List<SpellCasting>> SpellsPerLevel { get; set; }
    }

    public class Throw
    {
        public int WillThrow { get; set; }
        public int ReflexThrow { get; set; }
        public int EnduranceThrow { get; set; }
    }

    public class Race
    {
        public string Name { get; set; }
        public Dictionary<BaseAttribute, int> Bonuses { get; set; }

        public Race()
        {
            Bonuses = new Dictionary<BaseAttribute, int>();
        }
    }
        
}
