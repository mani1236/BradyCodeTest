using BradyCodeTest.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using BradyCodeTest.Models;

namespace BradyCodeTest.UnitTest.ServicesTest
{
   public class OutputGenrationServiceTest : IClassFixture<FixtureTest>
    {
        private readonly FixtureTest _FixtureTest;

       public  OutputGenrationService outputGenrationService;
        public OutputGenrationServiceTest(FixtureTest fixtureTest)
        {
            _FixtureTest = fixtureTest;            
        }

        [Fact]

        public void GenerationOutput_ShouldGetGenerationOutput()
        {             
            //Act 
            outputGenrationService = new OutputGenrationService(_FixtureTest.generationReport);
            var results = outputGenrationService.generationOutput();

            //Asert
            Assert.IsType<GenerationOutputModel>(results);

        }

        [Fact]
        public void GenerationOutput_ShouldCheckWithZeroValues()
        {
            //Arrange 
            outputGenrationService = new OutputGenrationService(new GenerationReportInputModel());
            var results = outputGenrationService.generationOutput();

            //Asert
            Assert.True(results.GeneratorCoalActualHeatRates.Count == 0);
            Assert.True(results.GeneratorMaxEmissions.Count == 0);
            Assert.True(results.GeneratorTotals.Count == 0);
        }
    }
}
