using System;

namespace main
{
    class ConsoleTableCreator
    {
        private const string TableFormat = "| {0,-40} | {1,-20} | {2,10} | {3,10} |";

        public static string ColumnFormat = "|";

        private static int _numberOfColumns;

        public static int NumberOfColumns
        {
            get => _numberOfColumns;
            set => _numberOfColumns = value;
        }

        public ConsoleTableCreator()
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