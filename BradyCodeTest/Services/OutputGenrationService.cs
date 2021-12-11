using BradyCodeTest.DTO;
using BradyCodeTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BradyCodeTest.Services
{
    public class OutputGenrationService
    {
        private GenerationReportInputModel generationReport;
        public OutputGenrationService(GenerationReportInputModel generationReport)
        {
            this.generationReport = generationReport;
        }
        public GenerationOutputModel generationOutput()
        {
            var generationOutputModel = new GenerationOutputModel();
            generationOutputModel.GeneratorTotals = GetGeneratorTotals();
            generationOutputModel.GeneratorMaxEmissions = GetGeneratorMaxEmissions();
            generationOutputModel.GeneratorCoalActualHeatRates = GetActualHeatRates();
            return generationOutputModel;
        }

        /// <summary>
        /// This method process and gets the DailyEmissions
        /// </summary>
        /// <returns>return the list of DailyEmissions</returns>
        private List<DailyEmissions> GetDailyEmissions()
        {
            var DailyEmissions = new List<DailyEmissions>();

            foreach (var CoalGenerator in generationReport.CoalGenerators)
            {
                DailyEmissions.AddRange(CoalGenerator.GetDailyEmissions());
            }

            foreach (var GasGenerator in generationReport.GasGenerators)
            {
                DailyEmissions.AddRange(GasGenerator.GetDailyEmissions());
            }

            return DailyEmissions;
        }

        /// <summary>
        /// This method process and gets the ActualHeatRates
        /// </summary>
        /// <returns>Returns the list of CoalActualHeatRates</returns>
        private List<CoalActualHeatRates> GetActualHeatRates()
        {
            var coalActualHeatRates = new List<CoalActualHeatRates>();
            foreach (var CoalGenerator in generationReport.CoalGenerators)
            {
                coalActualHeatRates.Add(new CoalActualHeatRates { Name = CoalGenerator.Name, HeatRate = CoalGenerator.GetActualHeatRate() });
            }
            return coalActualHeatRates;
        }

        /// <summary>
        /// This method process and gets the GeneratorMaxEmissions
        /// </summary>
        /// <returns>return the list of MaxEmissionGenerators</returns>
        private List<MaxEmissionGenerators> GetGeneratorMaxEmissions()
        {
            var generatorMaxEmissions = new List<MaxEmissionGenerators>();
            var DailyEmissions = GetDailyEmissions();
            var generatorDays = DailyEmissions.Where(r => r.Date != null)
                        .Select(r => r.Date)
                        .Distinct();


            foreach (var generatorDay in generatorDays)
            {
                var employee = DailyEmissions.Where(x => x.Date == generatorDay);
                var maxemission = employee.Aggregate((x, y) => x.Emission > y.Emission ? x : y);
                generatorMaxEmissions.Add(new MaxEmissionGenerators { Name = maxemission.Name, Date = maxemission.Date, Emission = maxemission.Emission });

            }

            return generatorMaxEmissions;
        }

        /// <summary>
        /// This method process and gets the GeneratorTotals
        /// </summary>
        /// <returns>return the list of Totals</returns>
        private List<Totals> GetGeneratorTotals()
        {
            var generatorTotals = new List<Totals>();
            foreach (var Wind in generationReport.WindGenerators)
            {
                generatorTotals.Add(new Totals { Name = Wind.Name, Total = Wind.GetTotals() });
            }

            foreach (var Gas in generationReport.GasGenerators)
            {
                generatorTotals.Add(new Totals { Name = Gas.Name, Total = Gas.GetTotals() });
            }

            foreach (var Coal in generationReport.CoalGenerators)
            {
                generatorTotals.Add(new Totals { Name = Coal.Name, Total = Coal.GetTotals() });
            }

            return generatorTotals;

        }
    }
}
