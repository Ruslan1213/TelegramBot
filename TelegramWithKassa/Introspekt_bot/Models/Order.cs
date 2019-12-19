
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace Introspekt_bot.Models
{
    public class Order
    {
        public long ChatId { get; set; }
        
        [XmlElement("id")]
        public int Id { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("email")]
        public string Email { get; set; }

        [XmlElement("amount")]
        public int Amount { get; set; }

        [XmlElement("date")]
        public string Date { get; set; }
    }

    [XmlRoot("root")]
    public class RootClass
    {
        [XmlArray("action_data")]
        [XmlArrayItem("item")]
        public Collection<Order> CollectionProperty;
    }
}
