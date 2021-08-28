using System;

namespace main
{
    static class ConsoleTableCreator
    {
        private const string TableFormat = "| {0,-40} | {1,-20} | {2,10} | {3,10} |";

        /* TODO CREATE HEADER LINE*/
        public static string CreateHeaderLine(){
            return string.Format(TableFormat, "Stock Name", "Action", "Amount", "Price");
        }

        /* TODO CREATE SEPARATOR LINE */
        public static string CreateSeparator(int lineLength){
            return new String('-', lineLength);
        }

        /* CREATE CONTENT LINE */
        public static string CreateContentLine(string arg0, string arg1, string arg2, string arg3){
            return string.Format(TableFormat, arg0, arg1, arg2, arg3);
        }

        public static void PrintLine(string line){
            Console.WriteLine(line);
        }
    }
}