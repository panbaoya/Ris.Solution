using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ris.Tools
{
    public class AesUnit
    {
        /// <summary>
        /// AES加密算法
        /// </summary>
        /// <param name="plainText">明文字符串</param>
        /// <returns>将加密后的密文转换为Base64编码，以便显示</returns>
        public static string AESEncrypt(string plainText, string key)
        {
            //分组加密算法
            SymmetricAlgorithm des = Rijndael.Create();
            des.Padding = PaddingMode.PKCS7;
            des.Mode = CipherMode.CBC;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(plainText);//得到需要加密的字节数组
            des.KeySize = 128;
            des.BlockSize = 128;
            //设置密钥及密钥向量
            byte[] _key1 = Encoding.UTF8.GetBytes(key);
            des.Key = _key1;
            des.IV = _key1;
            byte[] cipherBytes = null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cipherBytes = ms.ToArray();//得到加密后的字节数组
                    cs.Close();
                    ms.Close();
                }
            }
            return Convert.ToBase64String(cipherBytes);
        }


        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="encryptStr">密文字符串</param>
        /// <returns>明文</returns>
        public static string AESDecrypt(string encryptStr, string key)
        {
            string decrypt = "";
            Rijndael aes = Rijndael.Create();
            try
            {
                byte[] bKey = Encoding.UTF8.GetBytes(key);
                byte[] bIV = Encoding.UTF8.GetBytes(key);
                byte[] byteArray = Convert.FromBase64String(encryptStr);

                aes.KeySize = 128;
                aes.BlockSize = 128;
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;

                using (MemoryStream mStream = new MemoryStream())
                {
                    using (CryptoStream cStream = new CryptoStream(mStream, aes.CreateDecryptor(bKey, bIV), CryptoStreamMode.Write))
                    {
                        cStream.Write(byteArray, 0, byteArray.Length);
                        cStream.FlushFinalBlock();
                        decrypt = Encoding.UTF8.GetString(mStream.ToArray());
                    }
                }
            }
            catch
            {
            }
            aes.Clear();

            return decrypt;
        }
    }
}
