using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DailyApp.WPF.HttpClients
{
    /// <summary>
    /// MD5工具类
    /// </summary>
    internal class Md5Hepler
    {
        /// <summary>
        /// MD5  utf-8编码 32位
        /// </summary>
        /// <param name="content">明文</param>
        /// <returns>md5结果</returns>
        public static string GetMd5(string content)
        {
            return string.Join("",MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(content)).Select(x=>x.ToString("x2")));
        }
    }
}
