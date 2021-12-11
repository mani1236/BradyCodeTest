using BradyCodeTest.Abstrations;
using BradyCodeTest.DTO;
using BradyCodeTest.Helpers;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BradyCodeTest.Services
{
    public class CoalService : GeneratorBase
    {
        [XmlElement("TotalHeatInput")]
        public decimal TotalHeatInput { get; set; }

        [XmlElement("ActualNetGeneration")]
        public decimal ActualNetGeneration { get; set; }

        [XmlElement("EmissionsRating")]
        public decimal EmissionsRating { get; set; }


        /// <summary>
        /// /// This method will perform the calculation for Coal on daywise 
        /// </summary>
        /// <returns>returns the Coal day wise Total in decimal</returns>
        public override decimal GetTotals()
        {
            foreach (var Day in Days)
            {
                Total = Total + Day.Price * Day.Energy * ReferenceDataHelper.ValueFactorMedium;
            }

            //The below decimal can be rounded, For now i followed as per the requirement.  
            return Total;
        }

        /// <summary>
        /// /// This method will perform the calculation for coal DailyEmissions  
        /// </summary>
        /// <returns>returns the List of DailyEmissions</returns>
        public List<DailyEmissions> GetDailyEmissions()
        {
            var dailyEmissions = new List<DailyEmissions>();

            foreach (var Day in Days)
            {
                dailyEmissions.Add(new DailyEmissions { Name = Name, Date = Day.Date, Emission = Day.Energy * EmissionsRating * ReferenceDataHelper.EmissionFactorHigh });
            }

            return dailyEmissions;
        }

        /// <summary>
        ///  This method will perform the calculation for ActualHeatRate  
        /// </summary>
        /// <returns>returns the ActualHeatRate in decimal </returns>
        public decimal GetActualHeatRate()
        {
            return TotalHeatInput / ActualNetGeneration;
        }
    }
}
