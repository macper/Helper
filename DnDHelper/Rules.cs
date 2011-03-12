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
    }
}
