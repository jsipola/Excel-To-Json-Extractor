using System.Collections.Generic;

namespace main
{
    /* Create sub class for dividend*/
    class CompanyInformationFormat
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double CurrentQuantity { get; internal set; }

        public float TotalBuyAmount { get; set; }

        public float TotalSellAmount { get; set; }

        public float TotalDividendAmount { get; set; }

        public string Currency { get; set; }

        public IList<TradeAction> Buys { get; set; }
        
        public IList<TradeAction> Sells { get; set; }

        public IList<TradeAction> Dividends { get; set; }

        public CompanyInformationFormat()
        {
            Buys = new List<TradeAction>();
            Sells = new List<TradeAction>();
            Dividends = new List<TradeAction>();
            TotalBuyAmount = 0;
            TotalSellAmount = 0;
            TotalDividendAmount = 0;
            CurrentQuantity = 0;
            Currency = "EUR";
        }

        internal void AddBuy(TradeAction trade)
        {
            Buys.Add(trade);
        }

        internal void AddSell(TradeAction trade)
        {
            Sells.Add(trade);
        }
        
        internal void AddDividend(TradeAction trade)
        {
            Dividends.Add(trade);
        }
    }
}
