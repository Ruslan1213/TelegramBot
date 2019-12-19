using Introspekt_bot.Models;
using System.IO;
using System.Xml.Serialization;

namespace Introspekt_bot.Service
{
    public class Serializer
    {
        public static RootClass Deserialize(string xmlString)
        {
            var type = typeof(RootClass);

            var serializer = new XmlSerializer(type);

            using (var stringReader = new StringReader(xmlString))
            {
                return serializer.Deserialize(stringReader) as RootClass;
            }
        }
    }
}
