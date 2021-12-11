using BradyCodeTest.Helpers;
using BradyCodeTest.Models;
using BradyCodeTest.Services;
using System;
using System.IO;
using System.Threading;

namespace BradyCodeTest
{
    class Program
    {

        /* Brady File listner can be Created as job, where based on business times we can look weather folder has files or not and then process it
        but for now I have used FileSystemWatcher where any files placed into the given directory the watcher picks that file and process it for every 2 seonds. */
        static void Main()
        {
            try
            {
                Console.WriteLine("Brady file proccessing systm started.");
                Console.WriteLine("------------------");
                ConfigHelper.GetConfigFile();
                var files = Directory.GetFiles(ConfigHelper.GenerationReportInputFilePath);
                if (files.Length > 0)
                {
                    ProcessFiles(files);
                }
                else
                {
                    Console.WriteLine(string.Format("Files Not Available in the directory : {0}", ConfigHelper.GenerationReportInputFilePath));
                }

                Console.WriteLine(string.Format("Application Listinging for the files : {0}", ConfigHelper.GenerationReportInputFilePath));

                using var watcher = new FileSystemWatcher(ConfigHelper.GenerationReportInputFilePath);
                watcher.NotifyFilter = NotifyFilters.Attributes
                                     | NotifyFilters.CreationTime
                                     | NotifyFilters.DirectoryName
                                     | NotifyFilters.FileName
                                     | NotifyFilters.LastAccess
                                     | NotifyFilters.LastWrite
                                     | NotifyFilters.Security
                                     | NotifyFilters.Size;


                watcher.Created += OnCreated;
                watcher.Filter = "*.xml";
                watcher.EnableRaisingEvents = true;
                Console.WriteLine("Press enter to exit.");
                Console.ReadLine();
            }

            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Exception occured while Processing File {0}", ex.Message));
            }
            finally
            {
                Console.WriteLine("Click Any Key to Exit...");
                Console.ReadLine();
            }
        }

        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            try
            {
                //Based on the load we can increase or decrease is thread time.
                Thread.Sleep(1000);
                ProcessFile(e.FullPath);

            }

            catch (Exception ex)
            {
               Console.WriteLine(string.Format("The file was not proccessed due to incorrect format or data : {0}", ex.Message));
            }
                       
        }

        private static void ProcessFile(string filePath)
        {
            Console.WriteLine("---------------- ");
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            Console.WriteLine($"Started Processing File : {fileName}");
            GenerationReportInputModel GenerationReport = XMLHelper.ParseXML(filePath);
            var GenerationOutput = new OutputGenrationService(GenerationReport);
            XMLHelper.CreateXML(GenerationOutput.generationOutput(), fileName);
            Console.WriteLine("Completed File Process.");         
            Console.WriteLine($"Moving a procssed file into Proccssed into { filePath} ");
            XMLHelper.MoveFileAfterProcess(filePath, fileName);
            Console.WriteLine("Moved Proccesed file.");
            Console.WriteLine("---------------- ");
        }

        private static void ProcessFiles(string[] files)
        {

            foreach (var file in files)
            {
                ProcessFile(file);
            }
        }
    }

}
