using System;
using System.Xml.Serialization;

namespace BradyCodeTest.DTO
{
    public class GenerationDay
    {
        [XmlElement("Date")]
        public DateTime Date { get; set; }

        [XmlElement("Energy")]
        public decimal Energy { get; set; }

        [XmlElement("Price")]
        public decimal Price { get; set; }
    }
}
