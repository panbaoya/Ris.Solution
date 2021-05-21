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
        private static Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);//将当前应用程序的配置文件作为 System.Configuration.Configuration 对象打开。
        public static string ConnectionString => ConfigurationManager.AppSettings["ConnectionString"];
        /// <summary>
        /// 医院名称
        /// </summary>
        public static string HospitalName => config.AppSettings.Settings["HospitalName"]?.Value;

        /// <summary>
        /// 医院代码
        /// </summary>
        public static string HospitalCode => config.AppSettings.Settings["HospitalCode"]?.Value;

        /// <summary>
        /// 密钥
        /// </summary>
        public static string AesKey => ConfigurationManager.AppSettings["AesKey"];

        /// <summary>
        /// 邮箱正则
        /// </summary>
        public static string EmailRegex => ConfigurationManager.AppSettings["EmailRegex"];

        public static string PostAddress => ConfigurationManager.AppSettings["PostAddress"];
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
        public static string PacsIP => ConfigurationManager.AppSettings["PacsIP"];

        /// <summary>
        /// 修改配置信息
        /// </summary>
        /// <param name="setName"></param>
        /// <param name="setValue"></param>
        public static void SaveSettiongs(string setName, string setValue)
        {
            if (config.AppSettings.Settings.AllKeys.Contains(setName))
            {
                config.AppSettings.Settings[setName].Value = setValue;
            }
            else
            {
                config.AppSettings.Settings.Add(setName, setValue);
            }
            config.Save();
            ConfigurationManager.RefreshSection(setName);
        }
    }
}
