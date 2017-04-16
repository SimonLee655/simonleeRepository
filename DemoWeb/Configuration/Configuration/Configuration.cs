using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Demo.Configuration
{
    /// <summary>
    /// 存xml設定檔的物件
    /// 內含load設定檔的方法
    /// </summary>
    public class Configuration
    {

        public static readonly string ConfigKey = "SettingConfig";

        public static Dictionary<string, string> InitConfigMethods
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    { ConfigKey, "InitSettingConfig" }
                };
            }
        }

        /// <summary>
        /// 初始化 Configuration 物件
        /// </summary>
        /// <param name="configPath"></param>
        /// <param name="publishProfile"></param>
        public static void InitConfiguration(string configPath, string publishProfile = "")
        {
            Configuration config = new Configuration();
            config.LoadConfig(configPath, publishProfile);
        }

        private Dictionary<string, IConfigParser> configParsers = new Dictionary<string, IConfigParser>();

        /// <summary>
        /// 載入 Config 檔 (SettingConfig.xml)
        /// </summary>
        /// <param name="configPath"></param>
        /// <param name="publishProfile"></param>
        private void LoadConfig(string configPath, string publishProfile)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(configPath);

            XmlNode root = xmldoc.DocumentElement;

            //這兩個設定跟SettingConfig.xml的結構有關
            string parserConfigSection = "ParserConfigs";
            string parserDefinesSection = "ParserDefines";

            //待確認 什麼是PublishProfile
            if (publishProfile.Length > 0)
                parserConfigSection += "-" + publishProfile;

            configParsers.Clear();

            foreach(XmlNode node in root.ChildNodes)
            {
                if (node.NodeType == XmlNodeType.Comment) continue;

                if (node.LocalName.Equals(parserDefinesSection))
                    LoadParserDefine(node);
                else if (node.LocalName.Equals(parserConfigSection))
                    LoadParserConfigs(node);
            }

        }

        /// <summary>
        /// 解析 ParserDefine 節點
        /// </summary>
        /// <param name="DefineNode"></param>
        private void LoadParserDefine(XmlNode DefineNode)
        {

        }

        /// <summary>
        /// 解析 ParserConfigs 節點
        /// </summary>
        /// <param name="ConfigNode"></param>
        private void LoadParserConfigs(XmlNode ConfigNode)
        {
            foreach(XmlNode node in ConfigNode.ChildNodes)
            {
                if (node.NodeType == XmlNodeType.Comment) continue;
                //此處邏輯還要再看一下
                string nodeName = node.LocalName;
                IConfigParser parser = configParsers[nodeName];
                parser.ParseConfigSection(node);
            }
        }

        #region 未用到的區塊
        //private void LoadConfigEx(string key, string configPath, string publishProfile)
        //{
        //    XmlDocument xmldoc = new XmlDocument();
        //    xmldoc.Load(configPath);
        //    this.LoadConfigEx(key, xmldoc.DocumentElement, publishProfile);
        //}

        //private void LoadConfigEx(string key, XmlNode root, string publishProfile)
        //{
        //    string parseConfig = "ParserConfigs";
        //    string parserDefing = "ParserDefines";

        //    if (publishProfile.Length > 0)
        //        parseConfig += "-" + publishProfile;
        //    //目前沒有customer....
        //    //customerParsers.Clear();
        //    //this.LoadParserDefineEx()...
        //    //this.LoadParserConfigsEx()...
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="configPath"></param>
        /// <param name="publishProfile"></param>
        //public static void InitSettingConfig(string key, string configPath, string publishProfile = "")
        //{
        //    Configuration config = new Configuration();
        //    config.LoadConfig(configPath, publishProfile);
        //}
        #endregion
    }
}
