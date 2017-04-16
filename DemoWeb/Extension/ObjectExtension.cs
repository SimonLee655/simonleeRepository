using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Extension
{
    /// <summary>
    /// 物件的擴充方法
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// 將物件轉換為Dictionary
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Dictionary<string, object> Ext_ToDictionary(this object obj)
        {
            if (obj.GetType() == typeof(Dictionary<string, object>))
                return obj as Dictionary<string, object>;
            
            Dictionary<string, object> dic = new Dictionary<string, object>();

            foreach(var prop in obj.GetType().GetProperties())
            {
                dic.Add(prop.Name, prop.GetValue(obj));
            }
            return dic;
        }
    }
}
