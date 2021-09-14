using System;
using System.Collections.Generic;
using System.Linq;

namespace main
{
    class WriteDataToConsole
    {
        public WriteDataToConsole()
        {
            
        }
        
        public void CreateConsoleTable(IList<TradeInformation> trades)
        {
            var collection = trades;

            var consoleTable = new ConsoleTableCreator();

            var headerLine = ConsoleTableCreator.CreateHeaderLine("Stock Name", "Action", "Amount", "Price");
            var separatorLine = ConsoleTableCreator.CreateSeparator(headerLine.Count());

            ConsoleTableCreator.AddTableColumn(-40);
            ConsoleTableCreator.AddTableColumn(-20);
            ConsoleTableCreator.AddTableColumn(10);
            ConsoleTableCreator.AddTableColumn(10);

            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.WriteLine(headerLine);
            Console.WriteLine(separatorLine);

            var groupedList = collection.GroupBy(a => a.Name, a => a);
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
            var contentLine = ConsoleTableCreator.CreateContentLineAlt(info.Name.TruncateLength(35),
                                                                    info.TypeOfAction,
                                                                    info.Quantity.ToString(),
                                                                    info.Price.ToString());
            ConsoleTableCreator.PrintLine(contentLine);
        }
    }
}