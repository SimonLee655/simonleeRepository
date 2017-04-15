using Extension;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace Configuration.Connetcion
{
    public class ConnectionModel
    {
        public ConnectionContent[] Contents { get; private set; }

        public static ConnectionModel Parse(XmlNode node)
        {
            ConnectionModel model = new ConnectionModel();

            List<ConnectionContent> contents = new List<ConnectionContent>();
            ConnectionContent content;

            foreach(XmlNode childNode in node.ChildNodes)
            {
                if (childNode.NodeType != XmlNodeType.Element) continue;

                content = ConnectionContent.Parse(childNode);
                contents.Add(content);
            }

            model.Contents = contents.ToArray();
            return model;
        }
    }

    public class ConnectionContent
    {
        public string Name { get; private set; }
        public string Provider { get; private set; }
        public ConnectionDetail Detail { get; private set; }
        public string ConnectionString { get { return this.Detail.ToString(); } }

        public ConnectionContent(string name, string connectionString, string provider)
        {
            this.Name = name;
            this.Provider = provider;
            this.Detail = new ConnectionDetail(connectionString);
        }

        public static ConnectionContent Parse(XmlNode node)
        {
            string name = node.Attributes["name"].Value;
            string connectionString = node.Attributes["connectionString"].Value;
            string provider = node.Attributes["provider"].Value;

            return new ConnectionContent(name, connectionString, provider);
        }
    }

    public class ConnectionDetail
    {
        public string DataSource { get; private set; }
        public string InitialCatalog { get; private set; }
        public string UserId { get; set; }
        private string _Password { get; set; }

        public string Password {
            get
            {
                return this._Password;
            }
            private set
            {
                this._Password = this.GetPassword(value);
            }
        }
        internal ConnectionDetail(string connectionString)
        {
            Regex rgx;
            string groupName;
            foreach(var prop in this.GetType().GetProperties())
            {
                groupName = prop.Name;
                rgx = new Regex(@"(?i:{0}=)(?<{1}>[^;]+);".Ext_Format(this.GetDescriptionValue(prop), groupName));

                prop.SetValue(this, rgx.Match(connectionString).Groups[groupName].Value);
            }
        }

        public override string ToString()
        {
            return string.Join("", this.GetType().GetProperties().Select(i => @"{0}={1};".Ext_Format(this.GetDescriptionValue(i), i.GetValue(this))));
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private string GetPassword(string password)
        {
            //加密的功能可以做在這裡
            return password;
        }

        /// <summary>
        /// 取得Descriptions Attribute的值
        /// </summary>
        /// <param name="prop"></param>
        /// <returns></returns>
        private string GetDescriptionValue(PropertyInfo prop)
        {
            //下面這句有點難啊
            DescriptionAttribute attr = prop.GetCustomAttributes(typeof(DescriptionAttribute), true).First() as DescriptionAttribute;
            return attr.Description;
        }
    }
}
