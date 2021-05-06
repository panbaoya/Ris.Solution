using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ris.Tools
{
    public class BirthDayUnit
    {
        /// <summary>
        /// 获取生日
        /// </summary>
        /// <param name="IDCard">身份证号码</param>
        /// <returns>获取生日</returns>
        public static string GetBirthday(string IDCard)
        {
            if (!string.IsNullOrWhiteSpace(IDCard) && IDCard.Length >= 14)
            {
                return IDCard.Substring(6, 4) + "-" + IDCard.Substring(10, 2) + "-" + IDCard.Substring(12, 2);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取性别
        /// </summary>
        /// <param name="IDCard">身份证号码</param>
        /// <returns>获取性别</returns>
        public static string GetGender(string IDCard)
        {
            if (!string.IsNullOrWhiteSpace(IDCard) && IDCard.Length > 17)
            {
                return Convert.ToBoolean(Convert.ToInt32(IDCard.Substring(16, 1)) & 1) ? "男" : "女";
            }
            else
            {
                return "未知";
            }
        }

        /// <summary>
        /// 获取年龄，没精确到月
        /// </summary>
        /// <param name="IDCard">身份证号码</param>
        /// <returns>获取年龄</returns>
        public static string GetAge(string IDCard)
        {
            if (!string.IsNullOrWhiteSpace(IDCard))
            {
                var strBirthDay = GetBirthday(IDCard);
                var birthDay = Convert.ToDateTime(strBirthDay);
                if (birthDay.Year == 1)
                {
                    return "未知";
                }
                else if(birthDay.Year==DateTime.Now.Year)
                {
                    if (birthDay.Month==DateTime.Now.Month)
                    {
                        return (DateTime.Now.Day - birthDay.Day).ToString() + "天";
                    }
                    else
                    {
                        return (DateTime.Now.Month - birthDay.Month).ToString() + "月";
                    }
                }
                else
                {
                    return (DateTime.Now.Year - birthDay.Year).ToString() + "岁";
                }
            }
            else
            {
                return "未知";
            }
        }
    }
}
