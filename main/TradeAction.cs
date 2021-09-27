namespace main
{
    public class TradeAction : TradeActionBase
    {
        public string RecordNumber { get; set; }

        public double Quantity { get; set; }

        public string DateOfAction { get; set; }
        
        public float Rate { get; set; }
    }
}
