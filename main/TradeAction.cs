using System;

namespace main
{
    public class TradeAction : TradeActionBase
    {
        public string RecordNumber { get; set; }

        public double Quantity { get; set; }

        /* Change to string ? */
        public DateTime DateOfAction { get; set; }
        
        public float Rate { get; set; }
    }
}
