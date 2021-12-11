using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using BradyCodeTest.Services;

namespace BradyCodeTest.UnitTest.ServicesTest
{
    public class CoalServiceTest : IClassFixture<FixtureTest>
    {
        private readonly FixtureTest _fixtureTest;
        private readonly CoalService coalService;
        public CoalServiceTest(FixtureTest fixtureTest)
        {            
             coalService = new CoalService();                     
            _fixtureTest = fixtureTest;
            coalService.Days = _fixtureTest.GetColeDays();
            coalService.Name = "Coal[1]";
           
        }

        [Fact]        
        public void GetTotals_ShouldGetCoalTotals()
        {
            //Arrange
            var expectedTotal = 5341.716526632M;            

            //Act
            var actualTotal  = coalService.GetTotals();

            //Assert
            Assert.Equal(expectedTotal, actualTotal);

        }

        [Fact]
        public void GetDailyEmissions_ShouldGetDailyEmissions()
        {
            //Arrange
            var expectedDailyEmissions = _fixtureTest.GetExpectedCoalDailyEmissions();
            coalService.EmissionsRating = 0.482M;

            //Act
            var atcualDailyEmissions = coalService.GetDailyEmissions();

            //Assert
            expectedDailyEmissions.Should().BeEquivalentTo(atcualDailyEmissions);            
        }

        [Fact]
        public void GetDailyEmissions_ShouldGetActualHeatRate()
        {
            //Arrange
            decimal expectedHeatRate = 1;
            coalService.TotalHeatInput = 11.815M;
            coalService.ActualNetGeneration = 11.815M;
            
            //Act
            var atcualHeatRate = coalService.GetActualHeatRate();

            //Assert
            Assert.Equal(expectedHeatRate, atcualHeatRate);

        }

       

    }
}
