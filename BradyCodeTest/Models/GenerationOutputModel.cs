using BradyCodeTest.DTO;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BradyCodeTest.Models
{

    [XmlRoot(ElementName = "GenerationOutput")]
    public class GenerationOutputModel
    {
        [XmlArray("Totals")]
        [XmlArrayItem("Generator", typeof(Totals))]
        public List<Totals> GeneratorTotals { get; set; }

        [XmlArray("MaxEmissionGenerators")]
        [XmlArrayItem("Day", typeof(MaxEmissionGenerators))]
        public List<MaxEmissionGenerators> GeneratorMaxEmissions { get; set; }

        [XmlArray("ActualHeatRates")]
        [XmlArrayItem("ActualHeatRate",typeof(CoalActualHeatRates))]
        public List<CoalActualHeatRates> GeneratorCoalActualHeatRates { get; set; }

    }
}
