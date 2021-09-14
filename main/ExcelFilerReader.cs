using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExcelDataReader;

namespace main
{
    class ExcelFileReader
    {

        private IList<TradeInformation> _collectionOfTrades;

        public IList<TradeInformation> CollectionOfTrades
        {
            get => _collectionOfTrades;
            set => _collectionOfTrades = value; 
        }

        public ExcelFileReader()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var files = Directory.GetFiles(currentDirectory + "/data/").Where(filename => filename.EndsWith(".xls"));
            
            var collection = files.SelectMany(currentFile => ReadExcelFileContent(currentFile));
         
            Console.WriteLine("Number of items : " + collection.Count());
            _collectionOfTrades = new List<TradeInformation>();
            Run();
        }

        public void Run()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var files = Directory.GetFiles(currentDirectory + "/data/").Where(filename => filename.EndsWith(".xls"));
            
            var collection = files.SelectMany(currentFile => ReadExcelFileContent(currentFile));
         
            _collectionOfTrades = collection.OrderBy(a => a.Name).ToList();
        }

        public IList<TradeInformation> GetTradeCollection()
        {
            return _collectionOfTrades;
        }
        
        public IEnumerable<TradeInformation> ReadExcelFileContent(string fileName)
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
                                                            price: reader.GetValue(9),
                                                            currency: reader.GetDouble(9),
                                                            currencyRate: reader.GetString(10),
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
