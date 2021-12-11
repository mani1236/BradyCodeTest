using BradyCodeTest.Services;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BradyCodeTest.Models
{
    [XmlRoot(ElementName = "GenerationReport")]
    public class GenerationReportInputModel
    {
        [XmlArray("Wind")]
        [XmlArrayItem("WindGenerator", typeof(WindService))]
        public List<WindService> WindGenerators = new List<WindService>();

        [XmlArray("Gas")]
        [XmlArrayItem("GasGenerator", typeof(GasService))]
        public List<GasService> GasGenerators = new List<GasService>();

        [XmlArray("Coal")]
        [XmlArrayItem("CoalGenerator", typeof(CoalService))]
        public List<CoalService> CoalGenerators = new List<CoalService>();
    }
}
