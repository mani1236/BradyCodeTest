using BradyCodeTest.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FluentAssertions;
using System.IO;
using BradyCodeTest.Models;

namespace BradyCodeTest.UnitTest.HelperTest
{
    public  class XMLHelperTest : IClassFixture<FixtureTest>
    {
        private readonly FixtureTest _FixtureTest;

        public XMLHelperTest(FixtureTest fixtureTest)
        {
            _FixtureTest = fixtureTest;
        }        

        [Fact]
        public void ParseXML_ShouldParseTheGivenFile()
        {         

            //Acct
            var data = XMLHelper.ParseXML(@"C:\ManiTestProjects\BradyCodeTest\BradyCodeTest.UnitTest\TestXmlFiles\01-Basic-Test.xml");

            //Assert
            Assert.IsType<GenerationReportInputModel>(data);

        }

        [Fact]

        public void ParseXML_ShouldTestFileNotFoundException()
        {
            //Assert
            Assert.Throws<FileNotFoundException>(() => XMLHelper.ParseXML(@"C:\ManiTestProjects\BradyCodeTest\BradyCodeTest.UnitTest\TestXmlFiles\Test34.txt"));
        }

        [Fact]
        public void ParseXML_ShouldTestInvalidOperationException()
        {               
            Assert.Throws<InvalidOperationException>(() => XMLHelper.ParseXML(@"C:\ManiTestProjects\BradyCodeTest\BradyCodeTest.UnitTest\TestXmlFiles\XmlFileWithDiffrentDataStractures.xml"));
        }

        [Fact]
        public void ParseXML_ShouldGenratetheOutfile()
        {
            //Arrange 
            var generationOutputModel = _FixtureTest.GetFileData();            

            //Acct
             XMLHelper.CreateXML(generationOutputModel, "01-Basic-Test");

            //Assert
            Assert.True(File.Exists(@"C:\ManiTestProjects\BradyCodeTest\BradyCodeTest.UnitTest\TestXmlFiles\TestOutPutGenaratedFiles\01-Basic-Test-Result.xml"));

        }
     
    }
}
