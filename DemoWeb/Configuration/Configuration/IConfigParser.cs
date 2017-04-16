using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Demo.Configuration
{
    public interface IConfigParser
    {
        string configId { get; }
        void ParseConfigSection(XmlNode settingNode);
    }
}
