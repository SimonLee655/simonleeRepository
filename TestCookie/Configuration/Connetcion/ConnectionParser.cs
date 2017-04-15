using Configuration.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Configuration.Connetcion
{
    public class ConnectionParser : IConfigParser
    {
        public string configId
        {
            get { return "Connection"; }
        }

        /// <summary>
        /// 解析 Connection 節點下的系統參數
        /// </summary>
        /// <param name="settingNode"></param>
        public void ParseConfigSection(XmlNode settingNode)
        {
            ConnectionSetting.Setting = ConnectionModel.Parse(settingNode);
        }
    }
}
