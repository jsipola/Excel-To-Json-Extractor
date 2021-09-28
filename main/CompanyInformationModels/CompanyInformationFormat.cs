using System.Collections.Generic;

namespace CompanyInformationModels
{
    class CompanyInformationFormat
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double CurrentQuantity { get; internal set; }

        public float TotalBuyAmount { get; set; }

        public float TotalSellAmount { get; set; }

        public float TotalDividendAmount { get; set; }

        public string Currency { get; set; }

        public IList<TradeActionEvent> Buys { get; set; }
        
        public IList<TradeActionEvent> Sells { get; set; }

        public IList<TradeActionDividend> Dividends { get; set; }

        public CompanyInformationFormat()
        {
            Buys = new List<TradeActionEvent>();
            Sells = new List<TradeActionEvent>();
            Dividends = new List<TradeActionDividend>();
            TotalBuyAmount = 0;
            TotalSellAmount = 0;
            TotalDividendAmount = 0;
            CurrentQuantity = 0;
            Currency = "EUR";
        }

        internal void AddBuy(TradeActionEvent trade)
        {
            Buys.Add(trade);
        }

        internal void AddSell(TradeActionEvent trade)
        {
            Sells.Add(trade);
        }
        
        internal void AddDividend(TradeActionDividend trade)
        {
            Dividends.Add(trade);
        }
    }
}
