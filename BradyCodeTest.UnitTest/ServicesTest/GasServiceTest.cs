using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
namespace BradyCodeTest.UnitTest.ServicesTest
{
    public class GasServiceTest : IClassFixture<FixtureTest>
    {
        private readonly FixtureTest _fixtureTest;
        private readonly GasService gasService;

        public GasServiceTest(FixtureTest fixtureTest)
        {
            _fixtureTest = fixtureTest;
             gasService = new GasService();
             gasService.Days= _fixtureTest.GetGasDays();
             gasService.Name = "Gas[1]";
        }

        [Fact]
        public void GetTotals_ShouldGetTotals()
        {
            //Arrange 
            decimal expectedTotal = 8512.254605520M;
            //Act
            var actualTotal = gasService.GetTotals();

            //Assert
            Assert.Equal(expectedTotal, actualTotal);

        }

        [Fact]
        public void GetDailyEmissions_ShouldGetDailyEmissions()
        {
            //Arrange 
            var expectedDailyEmissions = _fixtureTest.GetExpectedGasDailyEmissions();
            gasService.EmissionsRating = 0.038M;

            //Act
            var actualDailyEmissions = gasService.GetDailyEmissions();

            //Assert
            expectedDailyEmissions.Should().BeEquivalentTo(actualDailyEmissions);

        }

    }
}
