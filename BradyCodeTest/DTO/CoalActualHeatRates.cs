using System.Xml.Serialization;

namespace BradyCodeTest.DTO
{
    public class CoalActualHeatRates
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("HeatRate")]
        public decimal HeatRate { get; set; }
    }
}
