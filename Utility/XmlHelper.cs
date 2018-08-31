using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Utility
{
    public static class XmlHelper
    {
        public static T Deserialize<T>(string xml, Encoding encoding) where T : class
        {
            T t = default(T);

            if (!string.IsNullOrEmpty(xml) && encoding != null)
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

                using (MemoryStream memoryStream = new MemoryStream(encoding.GetBytes(xml)))
                {
                    using (StreamReader streamReader = new StreamReader(memoryStream, encoding))
                    {
                        t = xmlSerializer.Deserialize(streamReader).To<T>();
                    }
                }
            }

            return t;
        }

        public static T DeserializeFromFile<T>(string xmlPath, Encoding encoding) where T : class
        {
            T t = default(T);

            if (!string.IsNullOrEmpty(xmlPath) && encoding != null)
            {
                string xml = File.ReadAllText(xmlPath, encoding);
                if (!string.IsNullOrEmpty(xml))
                {
                    t = Deserialize<T>(xml, encoding);
                }
            }

            return t;
        }
    }
}