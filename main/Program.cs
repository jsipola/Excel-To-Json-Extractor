
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace main
{
    class Program
    {
        private static IFormatProvider cultureInfo => new System.Globalization.CultureInfo("fi-FI");

        static async Task Main(string[] args)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var fileReader = new ExcelFileReader();
            var trades = fileReader.GetTradeCollection();

            var writer = new WriteDataToConsole();
//            writer.CreateConsoleTable(trades);

            var groupedList = trades.GroupBy(trade => trade.Name, item => item);
            var opts = new JsonSerializerOptions { WriteIndented = true };
            var contentCount = 0;

            foreach (var item in groupedList)
            {
                var placeHolder = new CompanyInformationFormat { Name = item.Key, Id = contentCount };
                contentCount++;
                foreach (var line in item)
                {
                    if (line.TypeOfAction.Contains("Myynti"))
                    {
                        var trade = new TradeAction 
                        {
                            TypeOfAction = "A Sell",
                            RecordNumber = line.RecordNumber,
                            DateOfAction = DateTime.Parse(line.DateOfAction, cultureInfo),
                            Quantity = line.Quantity,
                            Price = Convert.ToSingle(line.Price),
                            TotalTransactionCost = Convert.ToSingle(line.TotalTransactionCost)
                        };
                        placeHolder.AddSell(trade);
                    }
                    if  (line.TypeOfAction.Contains("Osto")){
                        var trade = new TradeAction 
                        {
                            TypeOfAction = "A Buy",
                            RecordNumber = line.RecordNumber,
                            DateOfAction = DateTime.Parse(line.DateOfAction, cultureInfo),
                            Quantity = line.Quantity,
                            Price = Convert.ToSingle(line.Price),
                            TotalTransactionCost = Convert.ToSingle(line.TotalTransactionCost)
                        };
                        placeHolder.AddBuy(trade);
                    }
                }
                //Console.WriteLine(JsonSerializer.Serialize(placeHolder, opts));
                await File.WriteAllTextAsync(Directory.GetCurrentDirectory() + "/jsonData/" + placeHolder.Name + ".json", JsonSerializer.Serialize(placeHolder, opts));
            }
        }

    }

}
