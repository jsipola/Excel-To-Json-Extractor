using System;
using System.Collections.Generic;
using System.Linq;

namespace ExcelToJsonExtractor
{
    class WriteDataToConsole
    {
        public WriteDataToConsole()
        {
        }
        
        public void CreateConsoleTable(IList<TradeInformation> trades)
        {
            ConsoleTableCreator.Init();
            ConsoleTableCreator.AddTableColumn(-40);
            ConsoleTableCreator.AddTableColumn(-20);
            ConsoleTableCreator.AddTableColumn(10);
            ConsoleTableCreator.AddTableColumn(10);
            
            var headerLine = ConsoleTableCreator.CreateHeaderLine("Stock Name", "Action", "Amount", "Price");
            var separatorLine = ConsoleTableCreator.CreateSeparator(headerLine.Count());

            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.WriteLine(headerLine);
            Console.WriteLine(separatorLine);

            /* TODO: improve this to order the trades alphabetically */
            var groupedList = trades.OrderBy(info => info.Name).GroupBy(trade => trade.Name, item => item);
            foreach (var item in groupedList)
            {
                var contentCount = 0;
                foreach (var line in item)
                {
                    if (line.TypeOfAction.Contains("Myynti") || line.TypeOfAction.Contains("Osto"))
                    {
                        CreateContent(line);
                        contentCount++;
                    }
                }
                if (contentCount > 0)
                {
                    Console.WriteLine(separatorLine);
                }
            }
        }
        
        private void CreateContent(TradeInformation info)
        {
            var contentLine = ConsoleTableCreator.CreateContentLine(info.Name.TruncateLength(35),
                                                                    info.TypeOfAction,
                                                                    info.Quantity.ToString(),
                                                                    info.Rate.ToString());
            ConsoleTableCreator.PrintLine(contentLine);
        }
    }
}
