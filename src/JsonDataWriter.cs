using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using CompanyInformationModels;

namespace ExcelToJsonExtractor
{
    public class JsonDataWriter
    {
        private static IFormatProvider cultureInfo => new System.Globalization.CultureInfo("fi-FI");

        public string CurrentDirectory { get; }
        private string _fullPath { get; }

        public JsonDataWriter()
        {
            CurrentDirectory = Directory.GetCurrentDirectory();
            _fullPath = CurrentDirectory + "/jsonData/";

            if (!Directory.Exists(_fullPath))
            {
                Directory.CreateDirectory(_fullPath);
            }

        }

        public async Task WriteDataToFile(IList<TradeInformation> trades)
        {
            var groupedList = trades.GroupBy(trade => trade.Name, item => item);
            var serializationOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                IgnoreNullValues = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(UnicodeRanges.All)
            };
            var contentCount = 0;

            foreach (var item in groupedList)
            {
                if (item.Key == "")
                {
                    continue;
                }
                var companyTradeData = new CompanyInformationFormat { Name = item.Key, Id = contentCount };
                contentCount++;
                foreach (var line in item)
                {
                    /* TODO: check the math for foreign currencies */
                    companyTradeData.Currency = line.Currency;
                    if (line.TypeOfAction.Contains("Myynti"))
                    {
                        TradeActionEvent trade = CreateTradeAction(line);
                        companyTradeData.CurrentQuantity -= trade.Quantity;
                        companyTradeData.AddSell(trade);
                        companyTradeData.TotalSellAmount += trade.TotalTransactionAmount;
                    }
                    if  (line.TypeOfAction.Contains("Osto") || line.TypeOfAction.Contains("Directed Issue")){

                        TradeActionEvent trade = CreateTradeAction(line);
                        companyTradeData.CurrentQuantity += trade.Quantity; 
                        companyTradeData.AddBuy(trade);
                        companyTradeData.TotalBuyAmount += trade.TotalTransactionAmount;
                    }
                    if  (line.TypeOfAction.Contains("Capitalization Issue")){

                        TradeActionEvent trade = CreateTradeAction(line);
                        trade.TypeOfAction = "A Capitalization Issue";
                        companyTradeData.CurrentQuantity += line.Quantity; 
                        companyTradeData.AddBuy(trade);
                    }

                    if  (line.TypeOfAction.Contains("Osinko")){
                        var dividendAmount = Convert.ToSingle(line.TotalTransactionCost);
                        var trade = new TradeActionDividend
                        {
                            TypeOfAction = "A Dividend",
                            PaymentDate = line.PaymentDate,
                            TotalTransactionAmount = dividendAmount
                        };

                        companyTradeData.AddDividend(trade);
                        companyTradeData.TotalDividendAmount += dividendAmount;
                    }
                }
                await File.WriteAllTextAsync(_fullPath + companyTradeData.Name + ".json",
                                             JsonSerializer.Serialize(companyTradeData, serializationOptions));
                Console.WriteLine($"Data written to : /jsonData/{companyTradeData.Name}.json");
            }
        }

        private static TradeActionEvent CreateTradeAction(TradeInformation line)
        {
            return new TradeActionEvent
            {
                RecordNumber = line.RecordNumber,
                DateOfAction = line.DateOfAction,
                Quantity = line.Quantity,
                Rate = Convert.ToSingle(line.Rate * line.ExchangeCurrency),
                TotalTransactionAmount = Convert.ToSingle(line.TotalTransactionCost)
            };
        }
    }
}
