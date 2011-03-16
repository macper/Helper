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
            ActiveMember.IsActiveMember = true;
        }

        public void NextMember()
        {
            int index = Members.IndexOf(ActiveMember);
            if (index == Members.Count - 1)
            {
                NewTurn();
                return;
            }
            ActiveMember.IsActiveMember = false;
            ActiveMember = Members[++index];
            if (!ActiveMember.IsAlive)
            {
                NextMember();
            }
            ActiveMember.IsActiveMember = true;
        }

        private void NewTurn()
        {
            Turn++;
            ActiveMember.IsActiveMember = false;
            ActiveMember = Members[0];
            ActiveMember.IsActiveMember = true;
            foreach (Character character in Members)
            {
                for (int i = 0; i < character.Effects.Count; i++)
                {
                    Effect efekt = character.Effects[i];
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
