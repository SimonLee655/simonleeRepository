using Configuration.Connetcion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration
{
    public class ConnectionHelper
    {
        /// <summary>
        /// 取得 Connection 中的 Content 資訊
        /// </summary>
        /// <returns></returns>
        public static ConnectionModel GetContentSetting()
        {
            return ConnectionSetting.Setting;
        }
    }
}
