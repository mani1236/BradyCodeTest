using BradyCodeTest.Abstrations;
using BradyCodeTest.Helpers;
using System.Xml.Serialization;

namespace BradyCodeTest
{
    public class WindService : GeneratorBase
    {
        [XmlElement("Location")]
        public string Location { get; set; }

        /// <summary>
        /// This method will perform the calculation for WindGenarator(Onshore/Offshore) on day wise.
        /// </summary>
        /// <returns>returns the WindGenarator day wise Total in decimal.</returns>
        public override decimal GetTotals()
        {
            if (Location == "Onshore")
            {
                foreach (var Day in Days)
                {
                    Total = Total + Day.Price * Day.Energy * ReferenceDataHelper.ValueFactorHigh;
                }
                //The below decimal can be rounded, For now i followed as per the requirement and given output file.  
                return Total;
            }
            else
            {
                foreach (var Day in Days)
                {
                    Total = Total + Day.Price * Day.Energy * ReferenceDataHelper.ValueFactorLow;
                }
                //The below decimal can be rounded, For now i followed as per the requirement and given output file.  
                return Total;
            }
        }
    }
}
