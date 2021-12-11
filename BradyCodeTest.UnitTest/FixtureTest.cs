using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using BradyCodeTest.Helpers;
using BradyCodeTest.DTO;
using BradyCodeTest.Models;
using BradyCodeTest.Services;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace BradyCodeTest.UnitTest
{
    public class FixtureTest
    {

        public GenerationReportInputModel generationReport;
        public GenerationOutputModel generationOutputModel;

        public FixtureTest()
        {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var InputFile = Path.Combine(directory, @"TestXmlFiles\01-Basic-Test.xml");            
            ConfigHelper.GenerationOutputFilePath = Path.Combine(directory,@"\TestXmlFiles\TestOutPutGenaratedFiles\");
            ConfigHelper.ReferenceDataFilePath = Path.Combine(directory,@"TestXmlFiles\ReferenceData.xml");       
            generationReport = XMLHelper.ParseXML(InputFile);
        }

        public List<GenerationDay> GetColeDays()
        {
            return generationReport.CoalGenerators.Select(x => x.Days).FirstOrDefault().ToList();
        }

        public List<GenerationDay> GetGasDays()
        {
            return generationReport.GasGenerators.Select(x => x.Days).FirstOrDefault().ToList();
        }

        public List<GenerationDay> GetWindDays()
        {
            return generationReport.WindGenerators.Select(x => x.Days).FirstOrDefault().ToList();
        }

        public List<DailyEmissions> GetExpectedCoalDailyEmissions()
        {
            var dailyEmissions = new List<DailyEmissions>();
            dailyEmissions.Add(new DailyEmissions { Name = "Coal[1]", Date = Convert.ToDateTime("2017-01-01 T00:00:00+00:00"), Emission = 137.175004008M });
            dailyEmissions.Add(new DailyEmissions { Name = "Coal[1]", Date = Convert.ToDateTime("2017-01-02T00:00:00+00:00"), Emission = 136.440767624M });
            dailyEmissions.Add(new DailyEmissions { Name = "Coal[1]", Date = Convert.ToDateTime("2017-01-03T00:00:00+00:00"), Emission = 0.000000M });

            return dailyEmissions;
        }

        public List<DailyEmissions> GetExpectedGasDailyEmissions()
        {
            var dailyEmissions = new List<DailyEmissions>();
            dailyEmissions.Add(new DailyEmissions { Name = "Gas[1]", Date = Convert.ToDateTime("2017-01-01 T00:00:00+00:00"), Emission = 5.536222660M });
            dailyEmissions.Add(new DailyEmissions { Name = "Gas[1]", Date = Convert.ToDateTime("2017-01-02T00:00:00+00:00"), Emission = 5.039482100M });
            dailyEmissions.Add(new DailyEmissions { Name = "Gas[1]", Date = Convert.ToDateTime("2017-01-03T00:00:00+00:00"), Emission = 5.132380700M });

            return dailyEmissions;
        }     

        public GenerationOutputModel GetFileData()
        {
            // Here we can create fake data, for now I have used the service, But we cane create a fake data like GetExpectedGasDailyEmissions
            var outPutService = new OutputGenrationService(generationReport);
            return outPutService.generationOutput();
        }
    }
}

