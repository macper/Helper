using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DnDHelper
{
    public class Battle
    {
        public int Turn { get; set; }
        public List<Character> Members { get; set; }
        public Character ActiveMember { get; set; }

        public Battle()
        {
            Turn = 0;
            Members = new List<Character>();
        }

        public void AddMember(Character character)
        {
            Members.Add(character);
            Members.Sort();
        }

        public void Start()
        {
            Turn = 1;
            ActiveMember = Members[0];
        }

        public void NextMember()
        {
            int index = Members.IndexOf(ActiveMember);
            if (index == Members.Count - 1)
            {
                NewTurn();
                return;
            }
            ActiveMember = Members[index++];
        }

        private void NewTurn()
        {
            Turn++;
            ActiveMember = Members[0];
            foreach (Character character in Members)
            {
                foreach (Effect efekt in character.Effects)
                {
                    if (efekt.Duration != null)
                    {
                        efekt.Duration--;
                        if (efekt.Duration == 0)
                        {
                            character.Effects.Remove(efekt);
                        }
                    }
                }
            }
        }

        public AttackInfo Atak(Character target, Attack atak)
        {
            return new AttackInfo() { ToHit = (target.CurrentStats.AC - atak.ToHit).ToString() };
        }

        public void DoDamage(Character target, int damage)
        {
            target.CurrentStats.HP -= damage;
        }
    }

    public class AttackInfo
    {
        public string ToHit { get; set; }
    }
}
