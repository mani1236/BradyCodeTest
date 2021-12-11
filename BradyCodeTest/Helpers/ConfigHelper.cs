using System.Configuration;

namespace BradyCodeTest.Helpers
{
    public static class ConfigHelper
    {
        public static string GenerationReportInputFilePath;

        public static string GenerationOutputFilePath;

        public static string ReferenceDataFilePath;

        public static string ProcessedFilesPath;

        /// <summary>
        /// This Methods gets the files that was configured in the App.config 
        /// </summary>
        public static void GetConfigFile()
        {
            GenerationReportInputFilePath = ConfigurationManager.AppSettings["GenerationReportInputFilePath"];
            GenerationOutputFilePath = ConfigurationManager.AppSettings["GenerationOutputFilePath"];
            ReferenceDataFilePath = ConfigurationManager.AppSettings["ReferenceDataFilePath"];
            ProcessedFilesPath = ConfigurationManager.AppSettings["ProcessedFilesPath"];

        }
    }
}
