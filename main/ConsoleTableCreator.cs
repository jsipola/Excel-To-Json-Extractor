using System;

namespace main
{
    class ConsoleTableCreator
    {
        private const string TableFormat = "| {0,-40} | {1,-20} | {2,10} | {3,10} |";

        private string ColumnFormat = "| ";

        private int _numberOfColumns;

        public int NumberOfColumns
        {
            get => _numberOfColumns;
            set => _numberOfColumns = value;
        }

        public void AddTableColumnWidths(int width = -30)
        {
            ColumnFormat += string.Format("{{0}, {1} |}", NumberOfColumns, width);
            NumberOfColumns =+ 1;
        }

        public void AddEndColumnPostFix()
        {
            ColumnFormat += "|";
        }

        public static string CreateHeaderLine(){
            return string.Format(TableFormat, "Stock Name", "Action", "Amount", "Price");
        }

        public static string CreateSeparator(int lineLength){
            return new String('-', lineLength);
        }

        public static string CreateContentLine(string arg0, string arg1, string arg2, string arg3){
            return string.Format(TableFormat, arg0.TruncateLength(36), arg1, arg2, arg3);
        }

        public static void PrintLine(string line){
            Console.WriteLine(line);
        }
    }
}