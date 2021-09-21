using System;

namespace main
{
    public class TradeAction
    {
        public string TypeOfAction { get; set; }

        public string RecordNumber { get; set; }
        
        public DateTime DateOfAction { get; set; }

        public double Quantity { get; set; }

        public float Rate { get; set; }

/*         public double Currency { get; set; }

        public float Commision { get; set; } */
        
        public float TotalTransactionAmount { get; set; }

    }
}
