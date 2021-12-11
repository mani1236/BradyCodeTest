using BradyCodeTest.Models;
using System;
using System.IO;
using System.Xml.Serialization;

namespace BradyCodeTest.Helpers
{
    public static class XMLHelper
    {

        /// <summary>
        /// This method takes the given file path and parse the file and deserialize into the model object.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static GenerationReportInputModel ParseXML(string path)
        {
            GenerationReportInputModel GenerationReport;

            XmlSerializer serializer = new XmlSerializer(typeof(GenerationReportInputModel));

            using (var reader = new StreamReader(path))
            {
                GenerationReport = (GenerationReportInputModel)serializer.Deserialize(reader);
            }
            return GenerationReport;

        }

        /// <summary>
        ///  This Method creats the xml file and placed into the output folder that was configured in app.config
        ///  I have give Unit test switch to check the test cases 
        /// </summary>
        /// <param name="generationOutput"></param>
        /// <param name="filePath"></param>
        public static void CreateXML(GenerationOutputModel generationOutput, string fileName)
        {
            try
            {
                /* As per the requirment I need to save the file by appedning a result at end to inputfile(ex: 01-Basic.xml -> 01-Basic-Result.xml),
                 but we have a problem here if file already existed in the output folder with current logic it  just overrides. 
                 To over come wit aobove mentioned point we can save the file with some uniq id  into output folder(ex: 01-Basic.xml -> 01-Basic-Result.xml).*/
                var requiredFilename = fileName + "-Result.xml";

                XmlSerializerNamespaces xmlNamespaces = new XmlSerializerNamespaces();
                xmlNamespaces.Add("", "");
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(GenerationOutputModel));

                using (var writer = new StreamWriter(ConfigHelper.GenerationOutputFilePath + requiredFilename))
                {
                    xmlSerializer.Serialize(writer, generationOutput, xmlNamespaces);
                }

            }

            catch (FileNotFoundException ex)
            {
                Console.WriteLine("File not found ", ex.Message);
            }

            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("You do not have permission to create this file.", ex.Message);
            }

            catch (IOException ex)
            {
                Console.WriteLine("An exception occurred while Creating a file", ex.Message);
            }
        }

        /// <summary>
        /// This method moves the processed file to processed folder to avoid redeundecy.  
        /// </summary>
        /// <param name="fileName"></param>
        public static void MoveFileAfterProcess(string filePath, string fileName)
        {
            string sourceFile = filePath;
            string destinationFile = ConfigHelper.ProcessedFilesPath + fileName;
            if (File.Exists(destinationFile + ".xml"))
            {
                /* I have used random number for getting some uniq number to move a procssed file into a proccssed folder, but we can use some uniq id as GUID*/
                Random random = new Random();
                var Uniqnumber = random.Next(1000);

                File.Move(sourceFile, destinationFile + fileName + "-" + Uniqnumber + ".xml");
            }
            else
            {
                File.Move(sourceFile, destinationFile + ".xml");
            }
        }
    }
}
