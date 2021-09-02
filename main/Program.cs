using System;
using System.IO;
using System.Linq;

namespace main
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            var fileReader = new ExcelFileReader();
        }
    }

}
