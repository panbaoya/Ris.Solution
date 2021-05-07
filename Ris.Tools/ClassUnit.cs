using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ris.Tools
{
    public static class ClassUnit
    {
        public static string ToJson(this object str, bool isIncludeNull = true)
        {
            JsonSerializerSettings setting = new JsonSerializerSettings
            {
                // NULL值处理
                NullValueHandling = isIncludeNull ? NullValueHandling.Include : NullValueHandling.Ignore,
            };
            return JsonConvert.SerializeObject(str, setting);
        }

        /// <summary>
        /// 将json字符串转换成实体
        /// </summary>
        /// <typeparam name="T">类</typeparam>
        /// <param name="str">json</param>
        /// <param name="isIncludeNull">是否包含空</param>
        /// <returns></returns>
        public static T JsonToObj<T>(this string str, bool isIncludeNull = true)
            where T : class
        {
            JsonSerializerSettings setting = new JsonSerializerSettings
            {
                // NULL值处理
                NullValueHandling = isIncludeNull ? NullValueHandling.Include : NullValueHandling.Ignore,
            };
            return JsonConvert.DeserializeObject<T>(str, setting);
        }
    }
}
