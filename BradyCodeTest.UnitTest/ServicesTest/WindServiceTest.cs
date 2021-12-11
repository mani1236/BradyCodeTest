using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BradyCodeTest.UnitTest.ServicesTest
{
    public class WindServiceTest : IClassFixture<FixtureTest>
    {
        private readonly FixtureTest _fixtureTest;
        private readonly WindService windService;
        public WindServiceTest(FixtureTest fixtureTest)
        {
            _fixtureTest = fixtureTest;
            windService = new WindService();
            windService.Days = _fixtureTest.GetWindDays();
        }

        [Fact]
        public void GetTotals_ShouldGetOnshoreTotals()
        {
            //Arrange
            decimal expectedTotal = 5935.230579762M;
            windService.Location = "Onshore";

            //Act
            var actualTotal = windService.GetTotals();

            //Assert
            Assert.Equal(expectedTotal, actualTotal);
        }

        [Fact]
        public void GetTotals_ShouldGetOffshoreTotals()
        {
            //Arrange
            decimal expectedTotal = 1662.617445705M;
            windService.Location = "Offshore";

            //Act
            var actualTotal = windService.GetTotals();

            //Assert
            Assert.Equal(expectedTotal, actualTotal);
        }
    }
}
