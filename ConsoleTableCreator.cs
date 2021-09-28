using System;

namespace ExcelToJsonExtractor
{
    static class ConsoleTableCreator
    {
        public static string ColumnFormat = "|";

        private static int _numberOfColumns;

        public static void Init()
        {
            _numberOfColumns = 0;
        }

        public static void AddTableColumn(int width = -30)
        {
            ColumnFormat += $" {{{_numberOfColumns},{width}}} |";
            _numberOfColumns++;
        }

        public static string CreateHeaderLine(params string[] headers){
            return string.Format(ColumnFormat, headers);
        }

        public static string CreateSeparator(int lineLength){
            return new String('-', lineLength);
        }

        public static string CreateContentLine(params string[] args){
            return string.Format(ColumnFormat, args);
        }

        public static void PrintLine(string line){
            Console.WriteLine(line);
        }
    }
}
