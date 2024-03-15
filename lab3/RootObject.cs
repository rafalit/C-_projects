using System.Xml.Serialization;

namespace program
{
    [XmlRoot("data")]
    public class RootObject
    {
        [XmlElement("tweet")]
        public List<User> data { get; set; }
    }
}