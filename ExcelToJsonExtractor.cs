
using System.Linq;
using System.Threading.Tasks;

namespace ExcelToJsonExtractor
{
    class ExcelToJsonExtractor
    {
        static async Task Main(string[] args)
        {
            var excelFileReader = new ExcelFileReader();
            var trades = excelFileReader.GetTradeCollection();

            var dataWriter = new JsonDataWriter();
            await dataWriter.WriteDataToFile(trades);
            if (args.Count() > 0 && args[0] == "write")
            {
                var writer = new WriteDataToConsole();
                writer.CreateConsoleTable(trades);
            }
        }

    }

}
