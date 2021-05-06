using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ris.Tools
{
    /// <summary>
    /// 读取配置类
    /// </summary>
    public class AppConfSetting
    {
        public static string ConnectionString => ConfigurationManager.AppSettings["ConnectionString"];
        /// <summary>
        /// 医院名称
        /// </summary>
        public static string HospitalName => ConfigurationManager.AppSettings["HospitalName"];

        /// <summary>
        /// 医院代码
        /// </summary>
        public static string HospitalCode => ConfigurationManager.AppSettings["HospitalCode"];

        /// <summary>
        /// 密钥
        /// </summary>
        public static string AesKey => ConfigurationManager.AppSettings["AesKey"];

        /// <summary>
        /// 邮箱正则
        /// </summary>
        public static string EmailRegex => ConfigurationManager.AppSettings["EmailRegex"];

        /// <summary>
        /// 全局字体大小
        /// </summary>
        public static int GlobalFontSize
        {
            get
            {
                int size = 9;
                if (ConfigurationManager.AppSettings["GlobalFontSize"]!=null)
                {
                    int.TryParse(ConfigurationManager.AppSettings["GlobalFontSize"], out size);
                }
                return size;
            }
        }

        public static int Port => Convert.ToInt32(ConfigurationManager.AppSettings["Port"]);

        /// <summary>
        /// 修改配置信息
        /// </summary>
        /// <param name="setName"></param>
        /// <param name="setValue"></param>
        public static void SaveSettiongs(string setName, string setValue)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);//将当前应用程序的配置文件作为 System.Configuration.Configuration 对象打开。
            if (config.AppSettings.Settings.AllKeys.Contains(setName))
            {
                config.AppSettings.Settings[setName].Value = setValue;
            }
            else
            {
                config.AppSettings.Settings.Add(setName, setValue);
            }

            var sss=config.AppSettings.Settings[setName];
            config.Save();
        }
    }
}
