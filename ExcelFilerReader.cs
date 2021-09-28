using System.Collections.Generic;
using System.IO;
using System.Linq;
using ExcelDataReader;

namespace ExcelToJsonExtractor
{
    class ExcelFileReader
    {

        private IList<TradeInformation> _collectionOfTrades;

        public IList<TradeInformation> GetTradeCollection()
        {
            return _collectionOfTrades;
        }

        private void SetTradeCollection(IList<TradeInformation> trades)
        {
            _collectionOfTrades = trades;
        }

        public ExcelFileReader()
        {
            // Required for ExcelReaderFactory
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            Run();
        }

        private void Run()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var files = Directory.GetFiles(currentDirectory + "/data/").Where(filename => filename.EndsWith(".xls"));
            
            var collection = files.SelectMany(currentFile => ReadExcelFileContent(currentFile));
         
            _collectionOfTrades = collection.OrderBy(a => a.Name).ToList();
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