using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uzabase.Module1.Interface;

namespace Uzabase.RSSConversion
{
    /// <summary>
    /// This class implements the IRSSConverion interface
    /// </summary>
    public class RSSConverter : IRSSConversion
    {
        /// <summary>
        /// Implement the conversion function.
        /// Download string from the specified url and replace to excludes the word "NewsPicks"
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string convertRSS(string url)
        {
            var result = string.Empty;
            using (var webClient = new System.Net.WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                result = webClient.DownloadString(url);
            }
            result = result.Replace("NewsPicks", "");
            return result;
        }
    }
}
