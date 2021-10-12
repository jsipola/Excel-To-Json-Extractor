
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ExcelToJsonExtractor
{
    class ExcelToJsonExtractor
    {
        static async Task Main(string[] args)
        {
            if (args.Count() == 0 || args[0].Contains("help"))
            {
                Console.WriteLine("\nNo file given\n");
                Console.WriteLine("Usage: ExcelToJsonExtractor.exe some_excel_file.xls\n");
                return;
            }

            var excelFileReader = new ExcelFileReader();
            if (args.Count() == 1 && args[0].EndsWith(".xls")) 
            {
                excelFileReader.ExcelFile = args[0];
            }

            excelFileReader.Run();
            var trades = excelFileReader.GetTradeCollection();

            var dataWriter = new JsonDataWriter();
            await dataWriter.WriteDataToFile(trades);
            if (args.Count() > 0 && args.Contains("write"))
            {
                var writer = new WriteDataToConsole();
                writer.CreateConsoleTable(trades);
            }
        }

    }

}
