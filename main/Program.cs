
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
                if (item.Key == "")
                {
                    continue;
                }
                var placeHolder = new CompanyInformationFormat { Name = item.Key, Id = contentCount };
                contentCount++;
                foreach (var line in item)
                {
                    /* TODO: Add proper handling for foreign currencies */
                    placeHolder.Currency = line.Currency;
                    if (line.TypeOfAction.Contains("Myynti"))
                    {
                        var sellAmount = Convert.ToSingle(line.TotalTransactionCost);
                        var trade = new TradeAction 
                        {
                            TypeOfAction = "A Sell",
                            RecordNumber = line.RecordNumber,
                            DateOfAction = DateTime.Parse(line.DateOfAction, cultureInfo),
                            Quantity = line.Quantity,
                            Rate = Convert.ToSingle(line.Rate),
                            TotalTransactionAmount = sellAmount
                        };

                        placeHolder.CurrentQuantity -= line.Quantity; 
                        placeHolder.AddSell(trade);
                        placeHolder.TotalSellAmount += sellAmount;
                    }
                    if  (line.TypeOfAction.Contains("Osto") || line.TypeOfAction.Contains("Directed Issue")){

                        var buyAmount = Convert.ToSingle(line.TotalTransactionCost);
                        var trade = new TradeAction 
                        {
                            TypeOfAction = "A Buy",
                            RecordNumber = line.RecordNumber,
                            DateOfAction = DateTime.Parse(line.DateOfAction, cultureInfo),
                            Quantity = line.Quantity,
                            Rate = Convert.ToSingle(line.Rate),
                            TotalTransactionAmount = buyAmount
                        };

                        placeHolder.CurrentQuantity += line.Quantity; 
                        placeHolder.AddBuy(trade);
                        placeHolder.TotalBuyAmount += buyAmount;
                    }
                    if  (line.TypeOfAction.Contains("Capitalization Issue")){

                        var buyAmount = 0;
                        var trade = new TradeAction 
                        {
                            TypeOfAction = "Capitalization Issue",
                            RecordNumber = line.RecordNumber,
                            DateOfAction = DateTime.Parse(line.DateOfAction, cultureInfo),
                            Quantity = line.Quantity,
                            Rate = Convert.ToSingle(line.Rate),
                            TotalTransactionAmount = buyAmount
                        };

                        placeHolder.CurrentQuantity += line.Quantity; 
                        placeHolder.AddBuy(trade);
                        placeHolder.TotalBuyAmount += buyAmount;
                    }

                    if  (line.TypeOfAction.Contains("Osinko")){
                        var dividendAmount = Convert.ToSingle(line.TotalTransactionCost);
                        var trade = new TradeAction 
                        {
                            TypeOfAction = "A Dividend",
                            TotalTransactionAmount = dividendAmount
                        };

                        placeHolder.AddDividend(trade);
                        placeHolder.TotalDividendAmount += dividendAmount;

                    }
                    
                }
                //Console.WriteLine(JsonSerializer.Serialize(placeHolder, opts));
                await File.WriteAllTextAsync(Directory.GetCurrentDirectory() + "/jsonData/" + placeHolder.Name + ".json", JsonSerializer.Serialize(placeHolder, opts));
            }
        }

    }

}
