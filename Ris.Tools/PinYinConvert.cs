using Microsoft.International.Converters.PinYinConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ris.Tools
{
    public class PinYinConvert
    {
        /// <summary>
        /// 获取拼音
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetPY(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }
            StringBuilder py = new StringBuilder();
            foreach (char item in str)
            {
                if (!ChineseChar.IsValidChar(item))
                {
                    py.Append(item);
                    continue;
                }
                    ChineseChar China = new ChineseChar(item);
                    string Temp = China.Pinyins[0].Substring(0, China.Pinyins[0].Length - 1);
                    py.Append(Temp.Substring(0, 1).ToUpper() + (China.Pinyins[0].Substring(1, China.Pinyins[0].Length - 2)).ToLower());
            }
            return py.ToString();
        }

        /// <summary>
        /// 获取拼音首字母
        /// </summary>
        public static string GetPYFirst(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            StringBuilder py = new StringBuilder();
            foreach (var ch in str)
            {
                if (!ChineseChar.IsValidChar(ch))
                {
                    py.Append(ch);
                    continue;
                }

                ChineseChar chineseChar = new ChineseChar(ch);
                py.Append(chineseChar.Pinyins[0].Substring(0, 1));
            }

            return py.ToString();
        }
    }
}
