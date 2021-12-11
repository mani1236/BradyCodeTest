using BradyCodeTest.Abstrations;
using BradyCodeTest.DTO;
using BradyCodeTest.Helpers;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BradyCodeTest
{
    public class GasService : GeneratorBase
    {
        [XmlElement("EmissionsRating")]
        public decimal EmissionsRating { get; set; }

        /// <summary>
        /// /// This method will perform the calculation for WindGenarator on daywise.
        /// </summary>
        /// <returns>returns the WindGenarator day wise Total in decimal.</returns>
        public override decimal GetTotals()
        {
            foreach (var Day in Days)
            {
                Total = Total + Day.Price * Day.Energy * ReferenceDataHelper.ValueFactorMedium;
            }
            //The below decimal can be rounded, For now i followed as per the requirement and given output file.  
            return Total;
        }

        /// <summary>
        /// /// This method will perform the calculation for gas DailyEmissions. 
        /// </summary>
        /// <returns>returns the List of DailyEmissions.</returns>
        public List<DailyEmissions> GetDailyEmissions()
        {
            List<DailyEmissions> dailyEmissions = new List<DailyEmissions>();

            foreach (var Day in Days)
            {
                dailyEmissions.Add(new DailyEmissions { Name = Name, Date = Day.Date, Emission = Day.Energy * EmissionsRating * ReferenceDataHelper.EmissionFactorMedium });
            }

            return dailyEmissions;
        }
    }
}
