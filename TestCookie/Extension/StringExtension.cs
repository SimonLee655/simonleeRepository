using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extension
{
    public static class StringExtension
    {
        /// <summary>
        /// 總覺的這個就是寫在後面的string.Format()而已啊...
        /// </summary>
        /// <param name="str"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Ext_Format(this string str, params object[] args)
        {
            return string.Format(str, args);
        }

        /// <summary>
        /// 寫在後面的string.IsNullOrEmpty()
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool Ext_IsNullOrEmpty(this string str)
        {
            str = str ?? "";
            return string.IsNullOrEmpty(str.Trim());
        }
    }
}
