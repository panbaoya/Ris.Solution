using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ris.Tools
{
    public class MakeID
    {
        private MakeID() { }

        private static readonly Random Random = new Random(~unchecked((int)DateTime.Now.Ticks));

        private static char[] CharList = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' }; // remove I & O

        public static string MakeGenderID(int length)
        {
            return "xb" + GetRandom(length);
        }

        private static string GetRandom(int length)
        {
            string result = null;
            for (int i = 0; i < length; i++)
            {
                int rnd = Random.Next(0, CharList.Length);
                result += CharList[rnd];
            }
            return result;
        }
    }
}
