
using System;
using System.Linq;

namespace main
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var fileReader = new ExcelFileReader();
            var trades = fileReader.GetTradeCollection();

            var writer = new WriteDataToConsole();
            writer.CreateConsoleTable(trades);
        }

    }

}
