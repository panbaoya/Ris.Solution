using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Ris.Tools
{
    /// <summary>
    /// 重写编码
    /// </summary>
    public class StringUTF8Writer : StringWriter
    {
        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }
    }

    public static class XmlUnit
    {

        /// <summary>
        /// 将XML字符串转换成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T XmlDeserialize<T>(this string str)
            where T : class
        {
            try
            {
                using (StringReader sr = new StringReader(str))
                {
                    XmlSerializer xmldes = new XmlSerializer(typeof(T)); 
                    return xmldes.Deserialize(sr) as T;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("将XML转换成实体对象异常", ex);
            }
        }

        /// <summary>
        /// 将对象转换为XML字符串
        /// </summary>
        /// <param name="ba"></param>
        /// <param name="omiDeclare">是否省略xml声明</param>
        /// <returns></returns>
        public static string XmlSerialize(this object ba, bool omiDeclare = false)
        {
            try
            {
                using (StringUTF8Writer sw = new StringUTF8Writer())
                {
                    XmlWriterSettings settings = new XmlWriterSettings();
                    settings.Indent = true;
                    settings.IndentChars = "    ";
                    settings.NewLineChars = "\r\n";
                    settings.Encoding = Encoding.UTF8;
                    settings.OmitXmlDeclaration = omiDeclare;
                    XmlWriter xw = XmlWriter.Create(sw, settings);
                    Type t = ba.GetType();
                    XmlSerializer serializer = new XmlSerializer(ba.GetType());
                    serializer.Serialize(xw, ba);
                    var ss = xw.ToString();
                    xw.Close();
                    sw.Close();
                    return sw.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("将实体对象转换成XML异常", ex);
            }
        }
    }
}
