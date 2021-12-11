using System;
using System.Xml.Serialization;

namespace BradyCodeTest.DTO
{
    public class MaxEmissionGenerators
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Date")]
        public DateTime Date { get; set; }

        [XmlElement("Emission")]
        public decimal Emission { get; set; }
    }
}
