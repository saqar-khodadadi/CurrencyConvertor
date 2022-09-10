using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Rules
{
    public  static class Rule
    {
        public static bool IsThreeLetters(this string str)
        {
            return (str.Length == 3 && str.All(Char.IsLetter));
        }
    }
}
