using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ExcelToJsonExtractor;
using NUnit.Framework;

namespace ExcelToJsonExtractorTest
{
    public class JsonDataWriterTests
    {

        private string targetDirectory => "/jsonData/";

        private JsonDataWriter _jsonWriter;

        [SetUp]
        public void Setup()
        {
            _jsonWriter = new JsonDataWriter();
        }

        [Test]
        public async Task ShouldNotCreateJsonDataForEmptyCollectionAsync()
        {
            var emptyList = new List<TradeInformation>();

            await _jsonWriter.WriteDataToFile(emptyList);

            var target = Directory.GetCurrentDirectory() + targetDirectory;

            var files = Directory.GetFiles(target);
            Assert.IsEmpty(files);
        }

        [TearDown]
        public void TearDown()
        {
            var dirs = Directory.GetDirectories(_jsonWriter.CurrentDirectory);
            var targetDirectory = "/jsonData/";
            foreach (var item in dirs)
            {
                if (item.Equals(targetDirectory))
                {
                    var targetpath = item + targetDirectory;
                    var files = Directory.GetFiles(targetpath).ToList();
                    if (files.Count() != 0)
                    {
                        try
                        {
                            foreach (var tempFile in files)
                            {
                                File.Delete(tempFile);
                            }
                        }
                        catch (System.Exception)
                        {
                            System.Console.WriteLine("Could not delete file under {0}", targetpath);
                            continue;
                        }
                    }
                    Directory.Delete(targetpath);
                }
            }
        }
    }
}