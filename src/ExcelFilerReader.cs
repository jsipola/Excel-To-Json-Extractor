using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExcelDataReader;

namespace ExcelToJsonExtractor
{
    public class ExcelFileReader
    {

        private static string xlsDataFolder => "/data/";
        
        private IList<TradeInformation> _collectionOfTrades;

        public string ExcelFile { get; set;}

        private string _currentDirectory;

        public ExcelFileReader()
        {
            // Required for ExcelReaderFactory
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            _collectionOfTrades = new List<TradeInformation>();
            ExcelFile = string.Empty;
            
            _currentDirectory = Directory.GetCurrentDirectory();
            if (!Directory.Exists(_currentDirectory + xlsDataFolder))
            {
                Directory.CreateDirectory(_currentDirectory + xlsDataFolder);
            }
        }

        public void Run()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var collection = Enumerable.Empty<TradeInformation>();
            if (ExcelFile.Equals(string.Empty) || !File.Exists(ExcelFile))
            {
                Console.WriteLine("No specified file found!");
                Console.WriteLine("Searching under ´{0}´", xlsDataFolder);
                var fullPath = _currentDirectory + xlsDataFolder;
                var files = Directory.GetFiles(fullPath).Where(filename => filename.EndsWith(".xls"));
                
                if (files.Count() == 0)
                {
                    Console.WriteLine("No ´.xls´ files found under {0}", fullPath);
                    return;
                }
                Console.Write("Using file(s): ");
                foreach(var item in files)
                {
                    Console.Write(item + " ");
                }
                collection = files.SelectMany(currentFile => ReadExcelFileContent(currentFile));
    
            } else {
                collection = ReadExcelFileContent(ExcelFile);
            }
         
            _collectionOfTrades = collection.ToList();
        }
        
        public IList<TradeInformation> GetTradeCollection()
        {
            return _collectionOfTrades;
        }

        private IEnumerable<TradeInformation> ReadExcelFileContent(string fileName)
        {
            var collection = new List<TradeInformation>();
            
            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    reader.Read();
                    do
                    {
                        while (reader.Read())
                        {
                            var info = new TradeInformation(recordNumber: reader.GetString(0),
                                                            typeOfAction: reader.GetString(1),
                                                            name: reader.GetString(2),
                                                            isinCode: reader.GetString(3),
                                                            tradeIdentifier: reader.GetString(4),
                                                            stockExchange: reader.GetString(5),
                                                            dateOfAction: reader.GetString(6),
                                                            paymentDate: reader.GetString(7),
                                                            quantity: reader.GetValue(8),
                                                            rate: reader.GetDouble(9),
                                                            currency: reader.GetString(10),
                                                            exchangeCurrency: reader.GetDouble(11),
                                                            marketValue: reader.GetValue(12),
                                                            commision: reader.GetValue(13),
                                                            totalTransactionCost: reader.GetValue(14));

                            collection.Add(info);
                        }
                    } while (reader.NextResult());
                }
            }
            
            return collection;
        }
    }
}
