using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Ris.Tools
{
    /// <summary>
    /// 枚举操作类
    /// </summary>
    public class EnumUnit
    {
        /// <summary>
        /// 获取枚举列表https://www.cnblogs.com/wendj/p/6768539.html
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<object> EnumToList<T>()
        {
            var list = new List<object>();
            foreach (var e in Enum.GetValues(typeof(T)))
            {
                string name = string.Empty;
                object[] disArr = e.GetType().GetField(e.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (disArr != null && disArr.Length > 0)
                {
                    DescriptionAttribute da = disArr[0] as DescriptionAttribute;
                    name = da.Description;
                }
                var item = new
                {
                    Name = name,
                    Value = Convert.ToInt32(e)
                };
                list.Add(item);
            }
            return list;
        }
        /// <summary>
        /// 获取枚举Description
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetDescription(Enum obj)
        {
            var _enumType = obj.GetType();
                var description = _enumType.GetField(Enum.GetName(_enumType,obj)).GetCustomAttributes(typeof(DescriptionAttribute),true);
            if (description != null && description.Length > 0)
            {
                DescriptionAttribute da = description[0] as DescriptionAttribute;
               return  da.Description;
            }
            else
            {
                return null;
            }
        }
    }
}
