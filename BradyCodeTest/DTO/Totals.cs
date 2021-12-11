using System.Xml.Serialization;

namespace BradyCodeTest.DTO
{
    public class Totals
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Total")]
        public decimal Total { get; set; }
    }
}
